using System;
using System.Collections.Generic;
using DataAdminCommonLib;
using Hik.Communication.ScsServices.Service;

namespace DataAdmin.DataClients
{
    public class DataClient
    {
        private string _username;
        private int _idDatabase;
        private bool _datanet;
        private bool _ticknet;

        public IScsServiceClient Client { get; set; }
        public IDataAdminService ClientProxy { get; set; }

        #region Properties
        public string UserName
        {
            get { return _username; }
        }
        public bool IsDatanetConnected
        {
            get { return _datanet; }
            set { _datanet = value; }
        }
        public bool IsTickNetConnected
        {
            get { return _ticknet; }
            set { _ticknet = value; }
        }
        public int DBId
        {
            get { return _idDatabase; }
        }
        #endregion
        public DataClient(string username,int idDB, IScsServiceClient client, IDataAdminService clientProxy,bool datanet, bool ticknet)
        {
            _username = username;
            Client = client;
            ClientProxy = clientProxy;
            _idDatabase = idDB;
            _datanet = datanet;
            _ticknet = ticknet;
        }
    }
}
