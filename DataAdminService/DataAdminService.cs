using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Hik.Collections;
using Hik.Communication.ScsServices.Service;
using DataAdminCommonLib;
using DataAdmin.DataClients;
using DataAdmin.DbDataManager;
using DataAdmin.DbDataManager.Structs;

namespace DataAdmin.DataAdminServices
{
    public class DataAdminService : ScsService, IDataAdminService
    {

        public readonly ThreadSafeSortedList<long, DataClient> _clients;
        public delegate void RaiseClientListChange();

        public delegate void RaiseClientFailedLoginLog(MessageFactory.LogMessage msg,string msgMain);

        public delegate void RaiseClientLoggedIn(MessageFactory.LogMessage msg,string msgMain);
        public event RaiseClientFailedLoginLog loginFailedLog;
        public event RaiseClientLoggedIn loggedInLog;
        public event RaiseClientListChange listChanged;
        
        


        public void OnUserListChanged()
        {
            //TODO:Implement task "Who is online"
            if (listChanged != null)
                listChanged();

        }

        public DataAdminService()
        {
            _clients = new ThreadSafeSortedList<long, DataClient>();
        
        }


        #region LOGIN IMPLEMENTATION

        public void Login(MessageFactory.LoginMessage loginParams)
        {
            var usr = loginParams.UsernameMD5;
            var psw = loginParams.PasswordMD5;
            var tempUser = new UserModel();
            string serverMessage = "";
            string msgfail = "";

         var  _users = DataManager.GetUsers();
       if (_users.Exists(a => a.Name == usr))// if user in DB
       {        tempUser = _users.Find(a => a.Name == usr);

            if(tempUser.Password == psw) // if user psw == db.psw
            {
                string ipAddress = "";
                var match = Regex.Match(CurrentClient.RemoteEndPoint.ToString(), @"\b(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})\b");
                if (match.Success)
                    ipAddress = match.Captures[0].Value;
                if ((ipAddress== tempUser.IpAdress && tempUser.AllowAnyIp == false) || (tempUser.AllowAnyIp))// if with Ip adress is all good
                {
                    if (loginParams.NetType == 'd' && tempUser.AllowDataNet)
                    {

                        if (!(_clients.GetAllItems().Exists(a => a.UserName == loginParams.UsernameMD5)))//if client not in list and he/she want to connected with dnet
                           
                        {
                            AddClient(CurrentClient, loginParams.NetType, tempUser);//add client
                            return;

                        }
                        var clientInList = _clients.GetAllItems().Find(a => a.UserName == loginParams.UsernameMD5);
                        if (clientInList.IsDatanetConnected == false)
                            clientInList.IsDatanetConnected = true;
                        return;
                    }
                    if (loginParams.NetType == 't' && tempUser.AllowTickNet)
                    {

                        if (!(_clients.GetAllItems().Exists(a => a.UserName == loginParams.UsernameMD5)))
                        {
                            AddClient(CurrentClient, loginParams.NetType, tempUser);
                            return;
                        }
                        var clientInList = _clients.GetAllItems().Find(a => a.UserName == loginParams.UsernameMD5);
                        if (clientInList.IsTickNetConnected == false)
                            clientInList.IsDatanetConnected = true;
                        return;
                    }
                    if (loginParams.NetType == 'd' && tempUser.AllowDataNet == false)
                    {
                        serverMessage = "YOUR DATANET CLIENT BLOCKED BY ADMIN";
                       
                            msgfail = "Client trying to connect from " + CurrentClient.RemoteEndPoint + " but his client blocked by Admin.";
                           

                    }
                    if (loginParams.NetType == 't' && tempUser.AllowTickNet == false)
                    {
                        serverMessage = "YOUR TICKNET CLIENT BLOCKED BY ADMIN";
                       
                       msgfail = DateTime.Now.Date.ToShortDateString()+ ": Client trying to connect from " + CurrentClient.RemoteEndPoint + " but  his client blocked by Admin.";
                }

                }
                else
                {
                    serverMessage = "YOUR IP ADDRESS IS NOT ALLOWED"; 
                 
                      msgfail =DateTime.Now.Date.ToShortDateString()+ ": Client trying connect to server from "+CurrentClient.RemoteEndPoint+" but the IP adress is blocked by Admin.";
                      
                        
                    
                }
           }
            else
            {
                serverMessage = "ENTER A CORRECT PASSWORD"; 
            }
       }
       else
       {
           serverMessage = "YOUR USERNAME IS INCORRECT";
       }

       if (loginFailedLog != null)
       {
           var msg = new MessageFactory.LogMessage();
           msg.OperationStatus = MessageFactory.LogMessage.Status.Failed;
           msg.LogType = MessageFactory.LogMessage.Log.Login;
           msg.Time = DateTime.Now.Date;
           
           loginFailedLog(new MessageFactory.LogMessage(),msgfail);
       }
            var client = CurrentClient;

       //Get a proxy object to call methods of client when needed
       var clientProxy = client.GetClientProxy<IDataAdminService>();
            var loginFailed = new MessageFactory.LoginMessage("", "", 'd') {ServerMessage = serverMessage};
            clientProxy.Login(loginFailed);


      

        }

public void AddClient(IScsServiceClient newClient,char listflag,UserModel usrModel)
{

    
    
    var client = newClient;

    //Get a proxy object to call methods of client when needed
    var clientProxy = client.GetClientProxy<IDataAdminService>();
    //Create a DataClient and store it in a collection
    bool dnet = listflag == 'd';
    bool tnet = listflag == 't';
    var dataClient = new DataClient(usrModel.Name,usrModel.Id, client, clientProxy,dnet,tnet)
                         {ClientProxy = clientProxy};

    _clients[client.ClientId] = dataClient;
    
    //Register to Disconnected event to know when user connection is closed
    client.Disconnected += Client_Disconnected;
    //Start a new task to send user list to mainform

    if (loggedInLog != null)
    {
        var msg = new MessageFactory.LogMessage();
        msg.OperationStatus = MessageFactory.LogMessage.Status.Finished;
        msg.LogType = MessageFactory.LogMessage.Log.Login;
        msg.Time = DateTime.Now.Date;
        msg.UserID = usrModel.Id;
        var msgMain = "Client " + usrModel.Name + " connected from " + usrModel.IpAdress;
        loggedInLog(msg,msgMain);
    }
      

     OnClientLogon(usrModel);
    Task.Factory.StartNew(OnUserListChanged
                  );
}
 
public void OnClientLogon(UserModel tempUser)
        {
           
            var privileges = new MessageFactory.ChangePrivilage(tempUser.AllowDataNet,tempUser.AllowTickNet,tempUser.AllowRemoteDb,tempUser.AllowLocalDb,tempUser.AllowAnyIp,tempUser.AllowMissBars,tempUser.AllowCollectFrCqg);
            var cl = FindClientByUserName(tempUser.Name);
            var xEle = new XElement("ConnectionString", new XAttribute("Host", Properties.Settings.Default.connectionHost),
                new XAttribute("dbName", Properties.Settings.Default.connectionDB),
                new XAttribute("userName", Properties.Settings.Default.connectionUser),
                new XAttribute("password", Properties.Settings.Default.connectionPassword));
            StringWriter sw = new StringWriter();
            XmlTextWriter tx = new XmlTextWriter(sw);
            xEle.WriteTo(tx);

            string str = sw.ToString();

            privileges.ServerMessage = str;
            cl.ClientProxy.onLogon(true, privileges);
     
          SendToClientSymbolGroupList(tempUser.Name);

        }
public void onLogon(bool logged, MessageFactory.ChangePrivilage getprivilages)
        {
          
            throw new NotImplementedException();
        }

        #endregion

        #region Blocking Client


        public void BlockClient(string destinationUser)
        {
            var receiverClient = FindClientByUserName(destinationUser);
            if (receiverClient == null)
            {
                throw new ApplicationException("There is no online " +
                                               "user  " + destinationUser);
            }


            receiverClient.ClientProxy.BlockClient(destinationUser);

        }

        private DataClient FindClientByUserName(string name)
        {

            return _clients.GetAllItems().Find(a => a.UserName == name);
        }

        #endregion

        #region Changing User Privilages

        public void ChangePrivilege(string name, MessageFactory.ChangePrivilage newprivilege)
        {
            var receiverClient = FindClientByUserName(name);
            if (receiverClient == null)
            {
                throw new ApplicationException("There is no online " +
                                               "user  " + name);
            }

            newprivilege.ServerMessage = "DATABASE=dataadmin_db; UID=root; PASSWORD=1111";
            
            receiverClient.ClientProxy.ChangePrivilege(name, newprivilege);
        }

        #endregion

        #region LogoutImplementation
        public void DeletedUser(string name)
 {
     var receiverClient = FindClientByUserName(name);
     if (receiverClient == null)
     {
         throw new ApplicationException("There is no online " +
                                        "user  " + name);
     }

  receiverClient.ClientProxy.DeletedByAdmin();
 }
        public void Logout()
        {
            ClientLogout(CurrentClient.ClientId);
        }

         /// <summary>
        /// Handles Disconnected event of all clients.
        /// </summary>
        /// <param name="sender">Client object that is disconnected</param>
        /// <param name="e">Event arguments (not used in this event)</param>
        private void Client_Disconnected(object sender, EventArgs e)
        {
            //Get client object
            var client = (IScsServiceClient) sender;

            //Perform logout (so, if client did not call Logout method before close,
            //we do logout automatically.
            ClientLogout(client.ClientId);
        }

        /// <summary>
        /// This method is called when a client calls
        /// the Logout method of service or a client
        /// connection fails.
        /// </summary>
        /// <param name="clientId">Unique Id of client that is logged out</param>
        private void ClientLogout(long clientId)
        {
            //Get client from client list, if not in list do not continue
            var client = _clients[clientId];
            if (client == null)
            {
                return;
            }

            //Remove client from online clients list
            _clients.Remove(client.Client.ClientId);

            //Unregister to Disconnected event (not needed really)
            client.Client.Disconnected -= Client_Disconnected;

            //todo raise event on mainform
            Task.Factory.StartNew(OnUserListChanged);

        }

        #endregion

        #region SEND SYMBOL LIST TO CLIENT
   

        public void SendToClientSymbolGroupList(string username)
        {
            if (!_clients.GetAllItems().Exists(a => a.UserName == username)) return;

            var dclient = _clients.GetAllItems().Find(a => a.UserName == username);
            //var symblist = DataManager.GetGroupsForUser(dclient.DBId);
            var xEle =   new XElement("UserID", new XAttribute("ID",FindClientByUserName(username).DBId));
          
            StringWriter sw = new StringWriter();
            XmlTextWriter tx = new XmlTextWriter(sw);
            xEle.WriteTo(tx);

            string str = sw.ToString();
            try
            {
                dclient.ClientProxy.SendAllowedSymbolGroups(str);
            }

            catch(Exception e)
            {
             
            }
        }
        public void SendAllowedSymbolList(object symbolList)//dont use this function
        {
         
        }

        public void SendAllowedSymbolGroups(object symbGroupList)//also call when admin changed symbol permissions
        {
            throw new NotImplementedException();
        }

        public void SymbolListChanged()
        {
            foreach (var client in _clients.GetAllItems())
            {
                client.ClientProxy.onSymbolListRecieved("");
            }
        }

        public void onSymbolListRecieved(string symbolList)
        {
            throw new NotImplementedException();
        }


        public void GroupChanged()
        {
            foreach (var client in _clients.GetAllItems())
            {
                client.ClientProxy.onSymbolGroupListRecieved("");
            }
        }
        public void onSymbolGroupListRecieved(string symbolGroupList)
        {
            
        }

        public void DeletedByAdmin()
        {
            throw new NotImplementedException();
        }

        public void SendConnectionString(string connectionString)
        {
            throw new NotImplementedException();
        }

        public void SendMessagageToClient(string message)
        {
            throw new NotImplementedException();
        }

        public void SendMessageToServer(string message)
        {
            throw new NotImplementedException();
        }

        public void Ping()
        {
            throw new NotImplementedException();
        }

     
        #endregion


        #region Properties

        public ThreadSafeSortedList<long, DataClient> OnlineClients
        {
            get { return _clients; }
        }

    }
    #endregion
}


