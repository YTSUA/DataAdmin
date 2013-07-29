using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAdmin.DataAdminServices;
using DataAdmin.DataNetLogger;
using DataAdmin.DbDataManager;
using DataAdmin.DbDataManager.Structs;
using DataAdmin.Properties;
using DataAdmin.InfoDisplayers.UserDetailManager;
using DataAdminCommonLib;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.ScsServices.Service;

namespace DataAdmin.Forms
{
    public partial class FormMain : MetroAppForm
    {
        private readonly MetroBillCommands _commands;
        private readonly StartControl _startControl;
        private AddUserControl _addUserControl;
        private EditUserControl _editUserControl;
        private ControlAddList _addListControl;
        private EditListControl _editListControl;
        private FormSettings _frmSettings;
        private List<UserModel> _users = new List<UserModel>();
        private List<SymbolModel> _symbols =  new List<SymbolModel>();
        private List<GroupModel> _groups = new List<GroupModel>(); 
        private List<LogModel> _logs = new List<LogModel>(); 

        #region Basic function (Constructor, Load, Show, Closing, Resize, Notify)

        public FormMain()
        {
            InitializeComponent();
            metroShellMain.SelectedTab = metroTabItem_home;
           
            ToastNotification.ToastBackColor = Color.SteelBlue;
            ToastNotification.DefaultToastPosition = eToastPosition.BottomCenter;

            SuspendLayout();

            _commands = new MetroBillCommands
            {
                StartControlCommands = { Logon = new Command(), Exit = new Command() },
                AddUserControlCommands = { Add = new Command(), Cancel = new Command() },
                EditUserControlCommands = { SaveChanges = new Command(), Cancel = new Command() },
                AddListCommands = { Save = new Command (), Cancel = new Command() },
                EditListCommands = { Save = new Command(), Cancel = new Command() }
            };            
            //**
            _commands.StartControlCommands.Logon.Executed += StartControl_LogonClick;
            _commands.StartControlCommands.Exit.Executed += StartControl_ExitClick;

            _commands.AddUserControlCommands.Add.Executed += AddNewUserControl_AddClick;
            _commands.AddUserControlCommands.Cancel.Executed += AddNewUserControl_CancelClick;

            _commands.EditUserControlCommands.SaveChanges.Executed += EditUserControl_SaveClick;
            _commands.EditUserControlCommands.Cancel.Executed += EditUserControl_CancelClick;

            _commands.AddListCommands.Cancel.Executed += AddListControl_CancelClick;
            _commands.AddListCommands.Save.Executed += AddListControl_SaveClick;

            _commands.EditListCommands.Cancel.Executed += EditListControl_CancelClick;
            _commands.EditListCommands.Save.Executed += EditListControl_SaveClick;

            //**
            _startControl = new StartControl {Commands = _commands};
            //_addUserControl = new AddUserControl {Commands = _commands, Tag = 0};

            Controls.Add(_startControl);
            _startControl.BringToFront();            
            _startControl.SlideSide = DevComponents.DotNetBar.Controls.eSlideSide.Right;

            
            ResumeLayout(false);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            UpdateControlsSizeAndLocation();
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            var color = Color.SteelBlue;
            ui_home_labelX1.ForeColor = color;
            ui_home_labelX2.ForeColor = color;
            ui_home_labelX3.ForeColor = color;
            ui_home_labelX4.ForeColor = color;

            ui_user_labelX1.ForeColor = color;
            ui_user_labelX2.ForeColor = color;
            ui_symbols_labelX_Symbols.ForeColor = color;
            ui_symbols_labelX_SymbolLists.ForeColor = color;
            ui_symbols_labelX_SListDetails.ForeColor = color;
            ui_logs_labelX_logs.ForeColor = color;

            notifyIcon.Icon = Icon;
            if (_startControl!=null)
                _startControl.ui_textBoxX_login.Focus();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_server != null)
            {
                _adminService.listChanged -= RefreshClientList;
                _server.Stop();

            }
            Hide();
            e.Cancel = false;
            Settings.Default.Save();
            /*
            if (MessageBox.Show(@"Do you really want to exit program?", @"DataAdmin", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                Hide();
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
                //
            }*/

         
        }

        private void metroShell1_Resize(object sender, EventArgs e)
        {
            UpdateControlsSizeAndLocation();
            if (WindowState == FormWindowState.Minimized)
            {
                ShowInTaskbar = Settings.Default.ShowInTaskBar;
            }
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
                ShowInTaskbar = true;
            }
        }

        #endregion

        #region DATA ADMIN SERVICE VARIABLES
        private DataAdminService _adminService;
        private DataNetLogService _logService;
        private IScsServiceApplication _server;
        #endregion
      

        #region UI + SERVER
        /// <summary>
        /// Called when we have success logining to DB 
        /// </summary>
        private void Logined()
        {
            _startControl.IsOpen = false;



            _server = ScsServiceBuilder.CreateService(new ScsTcpEndPoint(10048));
            _adminService = new DataAdminService();
            //Add Phone Book Service to service application
            _adminService.listChanged += RefreshClientList;
            _adminService.loggedInLog += ClientLoggedLog;
            _adminService.loginFailedLog += ClientFailedLoginLog;
            _logService = new DataNetLogService();
            _logService.abortedOperation += AbortedOperationLog;
            _logService.finishedOperation += FinishedOperationLog;
            _logService.startedOperation += StartedOperationLog;
            _logService.simpleMessage += SimpleMessageLog;

            _server.AddService<IDataAdminService, DataAdminService>(_adminService);
            _server.AddService<IDataNetLogService, DataNetLogService>(_logService);

            _server.ClientConnected += server_ClientConnected;
            _server.ClientDisconnected += server_ClientDisconnected;
            //Start server
            _server.Start();


            UpdateAllTables();
        }

        private void ClientFailedLoginLog(MessageFactory.LogMessage msg, string msgMain)
        {
            Invoke((Action)delegate
            {
                uiLastMessages.Items.Add(msgMain);

            });
        }

        private void ClientLoggedLog(MessageFactory.LogMessage msg, string msgMain)
        {
            //todo save this log into DB
            uiLastMessages.Items.Add(msgMain);
            var logmodel = new LogModel
                               {
                                   Date = msg.Time,
                                   Group = msg.Group,
                                   UserId = msg.UserID,
                                   MsgType = Convert.ToInt32(msg.LogType),
                                   Status = Convert.ToInt32(msg.OperationStatus),
                                   Symbol = msg.Symbol
                               };

            DataManager.AddNewLog(logmodel);
        }

        private void SimpleMessageLog(object sender, MessageFactory.LogMessage msg)
        {
            
            var logmodel = new LogModel
            {
                Date = msg.Time,
                Group = msg.Group,
                UserId = msg.UserID,
                MsgType = Convert.ToInt32(msg.LogType),
                Status = Convert.ToInt32(msg.OperationStatus),
                Symbol = msg.Symbol
            };

            DataManager.AddNewLog(logmodel);
        }

        private void StartedOperationLog(object sender, MessageFactory.LogMessage msg)
        {
          
            var logmodel = new LogModel
            {
                Date = msg.Time,
                Group = msg.Group,
                UserId = msg.UserID,
                MsgType = Convert.ToInt32(msg.LogType),
                Status = Convert.ToInt32(msg.OperationStatus),
                Symbol = msg.Symbol
            };

            DataManager.AddNewLog(logmodel);
        }

        private void FinishedOperationLog(object sender, MessageFactory.LogMessage msg)
        {
            var logmodel = new LogModel
            {
                Date = msg.Time,
                Group = msg.Group,
                UserId = msg.UserID,
                MsgType = Convert.ToInt32(msg.LogType),
                Status = Convert.ToInt32(msg.OperationStatus),
                Symbol = msg.Symbol
            };

            DataManager.AddNewLog(logmodel);
        }

        private void AbortedOperationLog(object sender, MessageFactory.LogMessage msg)
        {
            var logmodel = new LogModel
            {
                Date = msg.Time,
                Group = msg.Group,
                UserId = msg.UserID,
                MsgType = Convert.ToInt32(msg.LogType),
                Status = Convert.ToInt32(msg.OperationStatus),
                Symbol = msg.Symbol
            };

            DataManager.AddNewLog(logmodel);
        }

        private void RefreshClientList()
        {
            Invoke((MethodInvoker)delegate
            {
                if (_adminService.OnlineClients == null) return;
                var listClients = _adminService.OnlineClients;
                if (uiWhoIsOnline == null) return;
                uiWhoIsOnline.Items.Clear();
                foreach (var user in listClients.GetAllItems())
                {
                    uiWhoIsOnline.Items.Add(user.UserName);
                }
            });

        }

        private void server_ClientDisconnected(object sender, ServiceClientEventArgs e)
        {
            Invoke((MethodInvoker)delegate
            {

                var listClients = _adminService.OnlineClients;
                uiWhoIsOnline.Items.Clear();
                foreach (var user in listClients.GetAllItems())
                {
                    uiWhoIsOnline.Items.Add(user.UserName + " IP address:" + e.Client.RemoteEndPoint + " Status:" + e.Client.CommunicationState);
                }
            });
        }

        private void server_ClientConnected(object sender, ServiceClientEventArgs e)
        {
           

        }

        


        #region JUST UI
        private void StartControl_ExitClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void StartControl_LogonClick(object sender, EventArgs e)
        {

            Settings.Default.connectionUser = _startControl.ui_textBoxX_login.Text;
            Settings.Default.connectionPassword = _startControl.ui_textBoxX_password.Text;
            Settings.Default.connectionHost = _startControl.ui_textBoxX_host.Text;
            Settings.Default.connectionDB = _startControl.ui_textBoxX_db.Text;

            if (DataManager.Initialize(Settings.Default.connectionHost, Settings.Default.connectionDB, Settings.Default.connectionUser, Settings.Default.connectionPassword))
            {
                Logined();
            }
            else
            {
                ToastNotification.Show(_startControl, @"Wrong login or password");
            }
        }

        /// <summary>
        /// Called when we have success logining to DB 
        /// </summary>
 
        private void UpdateAllTables()
        {
            UpdateSymbolsTable();
            UpdateUsersTable();
            UpdateGroupsTable();
            UpdateLogsTable();

            //TODO: UPDATE ALL OTHER TABLES
        }

        private void UpdateSymbolsTable()
        {
            ui_symbols_listBox_symbols.Items.Clear();
            _symbols = DataManager.GetSymbols();
            foreach (var symbol in _symbols)
            {
                ui_symbols_listBox_symbols.Items.Add(symbol.SymbolName);
            }
        }

        private void UpdateUsersTable()
        {
            ui_users_dgridX_users.Rows.Clear();
            _users = DataManager.GetUsers();
            foreach (var userModel in _users)
            {
                ui_users_dgridX_users.Rows.Add(userModel.Name, userModel.FullName);
            }                           
        }

        private void UpdateGroupsTable()
        {
            ui_symbols_listBox_sumbolsLists.Items.Clear();
            _groups = DataManager.GetGroups();
            foreach (var group in _groups)
            {
                ui_symbols_listBox_sumbolsLists.Items.Add(group.GroupName);
            }
        }

        private void UpdateLogsTable()
        {
            _logs = DataManager.GetLogBetweenDates(DateTime.Now.AddDays(-2), DateTime.Now);
            ui_logs_DTime_StartFilter.Value = DateTime.Now.AddDays(-2);
            ui_logs_DTime_EndFilter.Value = DateTime.Now;

            ui_logs_dGridX_Logs.Rows.Clear();
            foreach (var log in _logs)
            {
                var userName = _users.Find(a => a.Id == log.UserId).Name;
                ui_logs_dGridX_Logs.Rows.Add(log.Date, userName, log.MsgType, log.Symbol, log.Group, "", log.Status);
            }
        }

        private void metroShell1_LogOutButtonClick(object sender, EventArgs e)
        {
            if (AnyControlsIsOpen()) return;

            _startControl.IsOpen = true;
            if (_server != null)
            {
                _server.Stop();
                ToastNotification.Show(this, "TURNING OFF THE SERVER...", 300);
            }
        }
        
        private bool AnyControlsIsOpen()
        {
            return _addUserControl!=null && _addUserControl.IsOpen;
        }

        private Rectangle GetStartControlBounds()
        {
            var captionHeight = metroShellMain.MetroTabStrip.GetCaptionHeight() + 2;
            var borderThickness = GetBorderThickness();
            return new Rectangle((int)borderThickness.Left, captionHeight, Width - (int)borderThickness.Horizontal, Height - captionHeight - 1);
        }

        private void UpdateControlsSizeAndLocation()
        {
            if (_startControl != null)
            {
                if (!_startControl.IsOpen)
                    _startControl.OpenBounds = GetStartControlBounds();
                else
                    _startControl.Bounds = GetStartControlBounds();
                if (!IsModalPanelDisplayed)
                    _startControl.BringToFront();
            }
        }
        
        private void metroShell1_SettingsButtonClick(object sender, EventArgs e)
        {            
            if (_frmSettings == null)
            {
                _frmSettings = new FormSettings();                
            }
            _frmSettings.ShowDialog();
            
        }

        #endregion

        #endregion

        #region MAIN

        #endregion


        #region SYMBOLS

        private void ui_Symbols_ButtonX_Add_Click(object sender, EventArgs e)
        {
            var fAdd = new FormSymbolAdd
            {
                Location = ui_symbols_listBox_symbols.PointToScreen(new Point(ui_symbols_listBox_symbols.Width / 2 - 122, 40))
            };

            DialogResult dr = fAdd.ShowDialog();
            switch (dr)
            {
                case DialogResult.OK:
                    {
                        var symbol = fAdd.ui_textBoxX_SymbolName.Text;
                        if (!_symbols.Exists(a => a.SymbolName == symbol))
                        {
                            DataManager.AddNewSymbol(symbol);
                            _adminService.SymbolListChanged();
                            UpdateSymbolsTable();
                        }
                        else
                        {
                            ToastNotification.Show(ui_symbols_listBox_symbols, @"Can't add symbol. This symbol already exists.", 1000, eToastPosition.TopCenter);
                        }
                        break;
                    }
            }
        }

        private void ui_Symbols_ButtonX_Edit_Click(object sender, EventArgs e)
        {
            if (ui_symbols_listBox_symbols.SelectedItems.Count == 0) return;

            var oldName = ui_symbols_listBox_symbols.SelectedItems[0].ToString();
            var fEdit = new FormSymbolEdit
                {
                    Location = ui_symbols_listBox_symbols.PointToScreen(new Point(ui_symbols_listBox_symbols.Width/2 - 122, 40)),
                    ui_textBoxX_SymbolName = { Text = oldName }
                };

            var dr = fEdit.ShowDialog();
            switch (dr)
            {
                case DialogResult.OK:
                    {
                        string symbol = fEdit.ui_textBoxX_SymbolName.Text;
                        if (!_symbols.Exists(a => a.SymbolName == symbol))
                        {
                            DataManager.EditSymbol(oldName, symbol);
                            _adminService.SymbolListChanged();

                            UpdateSymbolsTable();
                        }
                        else
                        {
                            ToastNotification.Show(ui_symbols_listBox_symbols, "Can't edit symbol. This symbol already exists.");
                        }

                        break;
                    }
            }
        }

        private void ui_Symbols_ButtonX_Delete_Click(object sender, EventArgs e)
        {
            if (ui_symbols_listBox_symbols.SelectedItems.Count > 0)
            {
                if (MessageBox.Show(@"Do you realy want to delete selected symbols with all data?", @"Deleting symbol", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    foreach (object item in ui_symbols_listBox_symbols.SelectedItems)
                    {
                        DataManager.DeleteSymbol(item.ToString());
                        _adminService.SymbolListChanged();

                    }
                    UpdateSymbolsTable();
                }
            }
            else
            {
                ToastNotification.Show(ui_symbols_listBox_symbols, @"Please, select symbol.", 1000, eToastPosition.TopCenter);
            }
        }

        #endregion


        #region USERS

        private void ui_users_buttonX_edit_Click(object sender, EventArgs e)
        {
            if (ui_users_dgridX_users.SelectedRows.Count == 0) return;

            var name = ui_users_dgridX_users.SelectedRows[0].Cells["ui_users_dGridCol_Login"].Value.ToString();
            var oldUserInfo = _users.Find(a => a.Name == name);
            
            ui_users_buttonX_edit.Enabled = false;

            _editUserControl = new EditUserControl
                {
                    ui_textBoxX_login = { Text = oldUserInfo.Name },
                    ui_textBoxX_name = { Text = oldUserInfo.FullName },
                    ui_textBoxX_phone = { Text = oldUserInfo.Phone },
                    ui_textBoxX_email = { Text = oldUserInfo.Email },
                    ui_textBoxX_password = { Text = oldUserInfo.Password },
                    ui_textBoxX_repassword = { Text = oldUserInfo.Password },
                    ui_textBoxX_ip = { Text = oldUserInfo.IpAdress },
                    ui_switchButton_allowCollecting = { Value = oldUserInfo.AllowCollectFrCqg},
                    ui_switchButton_allowUser = { Value = !oldUserInfo.Blocked },
                    ui_switchButton_any_Ip = { Value = oldUserInfo.AllowAnyIp },
                    ui_switchButton_allwoMissingBar = { Value = oldUserInfo.AllowMissBars },
                    ui_switchButton_enableDataNet = { Value = oldUserInfo.AllowDataNet },
                    ui_switchButton_enableTickNet = { Value = oldUserInfo.AllowTickNet },
                    ui_switchButton_local = { Value = oldUserInfo.AllowLocalDb },
                    ui_switchButton_share = { Value = oldUserInfo.AllowRemoteDb },
                    Commands = _commands, 
                    Tag = 0
                };
            ShowModalPanel(_editUserControl, DevComponents.DotNetBar.Controls.eSlideSide.Right);  
        }

        private void EditUserControl_SaveClick(object sender, EventArgs e)
        {
            var userModel = new UserModel
            {
                FullName = _editUserControl.ui_textBoxX_name.Text,
                Name = _editUserControl.ui_textBoxX_login.Text,
                Password = _editUserControl.ui_textBoxX_password.Text,
                Email = _editUserControl.ui_textBoxX_email.Text,
                Phone = _editUserControl.ui_textBoxX_phone.Text,
                IpAdress = _editUserControl.ui_textBoxX_ip.Text,
                Blocked = !_editUserControl.ui_switchButton_allowUser.Value,
                AllowDataNet = _editUserControl.ui_switchButton_enableDataNet.Value,
                AllowTickNet = _editUserControl.ui_switchButton_enableTickNet.Value,
                AllowLocalDb = _editUserControl.ui_switchButton_local.Value,
                AllowRemoteDb = _editUserControl.ui_switchButton_share.Value,
                AllowAnyIp = _editUserControl.ui_switchButton_any_Ip.Value,
                AllowMissBars = _editUserControl.ui_switchButton_allwoMissingBar.Value,
                AllowCollectFrCqg = _editUserControl.ui_switchButton_allowCollecting.Value
            };

            var oldUserName = _editUserControl.OldUserLogin;

            if ((!_users.Exists(a => a.Name == userModel.Name) && _users.Exists(a => a.Name == oldUserName)) || (userModel.Name == oldUserName && _users.Exists(a => a.Name == oldUserName)))
            {
                var userId = _users.Find(a => a.Name == oldUserName).Id;
                DataManager.EditUser(userId, userModel);
                UpdateUsersTable();
                CloseEditUserControl();
                if(_adminService.OnlineClients.GetAllItems().Exists(a => a.UserName == userModel.Name))
                _adminService.ChangePrivilege(userModel.Name, new MessageFactory.ChangePrivilage(userModel.AllowDataNet,
                userModel.AllowTickNet, userModel.AllowRemoteDb, userModel.AllowLocalDb, userModel.AllowAnyIp, userModel.AllowMissBars, userModel.AllowCollectFrCqg));
            
            }
            else
            {
                ToastNotification.Show(_editUserControl, @"User with this login is already exists!");
            }
        }

        private void EditUserControl_CancelClick(object sender, EventArgs e)
        {
            CloseEditUserControl();
        }

        private void CloseEditUserControl()
        {
            CloseModalPanel(_editUserControl, DevComponents.DotNetBar.Controls.eSlideSide.Right);
            _editUserControl.Dispose();
            _editUserControl = null;
            ui_users_buttonX_edit.Enabled = true; 
        }

        private void ui_users_buttonX_add_Click(object sender, EventArgs e)
        {
            ui_users_buttonX_add.Enabled = false;

            _addUserControl = new AddUserControl { Commands = _commands, Tag = 0 };
            ShowModalPanel(_addUserControl, DevComponents.DotNetBar.Controls.eSlideSide.Right);                            
        }

        private void AddNewUserControl_AddClick(object sender, EventArgs e)
        {
            var userModel = new UserModel
                {
                    FullName = _addUserControl.ui_textBoxX_name.Text,
                    Name = _addUserControl.ui_textBoxX_login.Text,
                    Password = _addUserControl.ui_textBoxX_password.Text,
                    Email = _addUserControl.ui_textBoxX_email.Text,
                    Phone = _addUserControl.ui_textBoxX_phone.Text,
                    IpAdress = _addUserControl.ui_textBoxX_ip.Text,
                    Blocked = ! _addUserControl.ui_switchButton_allowUser.Value,
                    AllowDataNet = _addUserControl.ui_switchButton_enableDataNet.Value,
                    AllowTickNet = _addUserControl.ui_switchButton_enableTickNet.Value,
                    AllowLocalDb = _addUserControl.ui_switchButton_local.Value,
                    AllowRemoteDb = _addUserControl.ui_switchButton_share.Value,
                    AllowAnyIp = _addUserControl.ui_switchButton_any_Ip.Value,
                    AllowMissBars = _addUserControl.ui_switchButton_allwoMissingBar.Value,
                    AllowCollectFrCqg = _addUserControl.ui_switchButton_allowCollecting.Value
                };

            
            if (!_users.Exists(a=>a.Name == userModel.Name))
            {
                DataManager.AddNewUser(userModel);
                UpdateUsersTable();
                CloseAddUserControl();
            }
            else
            {
                ToastNotification.Show(_addUserControl, @"User with this login is already exists!");
            }
        }

        private void AddNewUserControl_CancelClick(object sender, EventArgs e)
        {
            CloseAddUserControl();
        }

        private void CloseAddUserControl()
        {
            CloseModalPanel(_addUserControl, DevComponents.DotNetBar.Controls.eSlideSide.Right);
            _addUserControl.Dispose();
            _addUserControl = null;
            ui_users_buttonX_add.Enabled = true;     
        }

        private void ui_users_buttonX_delete_Click(object sender, EventArgs e)
        {
            if (ui_users_dgridX_users.SelectedRows.Count > 0)
            {
                if (MessageBox.Show(@"Do you realy want to delete selected users", @"Deleting user", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    foreach (DataGridViewRow item in ui_users_dgridX_users.SelectedRows)
                    {
                        var userId = _users.Find(a => a.Name == item.Cells["ui_users_dGridCol_Login"].Value.ToString()).Id;
                        var username =
                            _users.Find(a => a.Name == item.Cells["ui_users_dGridCol_Login"].Value.ToString()).Name;
                        DataManager.DeleteUser(userId);


                        _adminService.DeletedUser(username);//send to client request to logging out
                    }
                    UpdateUsersTable();
                }
            }
            else
            {
                ToastNotification.Show(ui_symbols_listBox_symbols, @"Please, select user.", 1000, eToastPosition.TopCenter);
            }
        }

        #endregion


        #region USER DETAILS
        private void ui_users_dgridX_users_SelectionChanged(object sender, EventArgs e)
        {
            var usersId = new List<int>();
            
            foreach (DataGridViewRow row in ui_users_dgridX_users.SelectedRows)
            {
                usersId.Add(_users.Find(a=>a.Name == row.Cells["ui_users_dGridCol_Login"].Value.ToString()).Id);
            }

            ui_users_superGridControl_details.PrimaryGrid.DataSource = UserDetailDisplayer.GetUserDetailsDataSet(usersId);
        }

        private void ui_users_superGridControl_users_DataBindingComplete(object sender, DevComponents.DotNetBar.SuperGrid.GridDataBindingCompleteEventArgs e)
        {
            ui_users_superGridControl_details.PrimaryGrid.Columns["Id"].CellStyles.Default.TextColor = Color.White;
            ui_users_superGridControl_details.PrimaryGrid.Columns["Id"].FillWeight = 20;
            ui_users_superGridControl_details.PrimaryGrid.Columns["Id"].Width = 20;
            ui_users_superGridControl_details.PrimaryGrid.Columns["Id"].HeaderText = "";
        }

        private void ui_users_superGridControl_details_AfterExpand(object sender, DevComponents.DotNetBar.SuperGrid.GridAfterExpandEventArgs e)
        {
            e.GridContainer.Rows[0].GridPanel.RowHeaderWidth = 0;
            e.GridContainer.Rows[0].GridPanel.Columns["Id"].Width = 20;
            e.GridContainer.Rows[0].GridPanel.Columns["Id"].HeaderText = "";
            e.GridContainer.Rows[0].GridPanel.DefaultVisualStyles.ColumnHeaderStyles.Default.Font = new Font("Segoe UI", (float)7.8, FontStyle.Bold);
            if (e.GridContainer.Rows[0].GridPanel.Columns["id_User"] != null)
            {
                e.GridContainer.Rows[0].GridPanel.ColumnHeader.RowHeight = 30;
                e.GridContainer.Rows[0].GridPanel.Columns["id_User"].Visible = false;
                e.GridContainer.Rows[0].GridPanel.AllowEdit = false;
            }
            if (e.GridContainer.Rows[0].GridPanel.Columns["id_Group"] != null)
            {
                e.GridContainer.Rows[0].GridPanel.ColumnHeader.RowHeight = 30;
                e.GridContainer.Rows[0].GridPanel.Columns["id"].Visible = false;
                e.GridContainer.Rows[0].GridPanel.Columns["id_Group"].Visible = false;
                e.GridContainer.Rows[0].GridPanel.AllowEdit = false;
            }
        }
        #endregion


        #region SYMBOLS LISTS
        private void ui_Symbols_ButtonX_AddList_Click(object sender, EventArgs e)
        {
            _addListControl = new ControlAddList { Commands = _commands, Tag = 0 };
            ShowModalPanel(_addListControl, DevComponents.DotNetBar.Controls.eSlideSide.Right);
            ui_Symbols_ButtonX_AddList.Enabled = false;
        }

        private void AddListControl_CancelClick(object sender, EventArgs e)
        {
            CloseAddListControl();
        }

        private void AddListControl_SaveClick(object sender, EventArgs e)
        {
            var group = new GroupModel
            {
                GroupName = _addListControl.textBoxXListName.Text,
                TimeFrame = _addListControl.cmbHistoricalPeriod.SelectedItem.ToString(),
                Start = _addListControl.startTimeCollect.Value,
                End = _addListControl.endTimeCollect.Value,
                CntType = _addListControl.cmbContinuationType.SelectedItem.ToString()
            };

            if (!_groups.Exists(a => a.GroupName == group.GroupName))
            {
                if (DataManager.AddGroupOfSymbols(group))
                {
                    UpdateGroupsTable();
                    var groupId = _groups.Find(a => a.GroupName == group.GroupName).GroupId;
                    foreach (var item in _addListControl.lbSelList.Items)
                    {
                        if (_symbols.Exists(a => a.SymbolName == item.ToString()))
                        {
                            var symbol = _symbols.Find(a => a.SymbolName == item.ToString());
                            DataManager.AddSymbolIntoGroup(groupId, symbol);
                        }
                    }
                    _adminService.GroupChanged();

                    CloseAddListControl();
                }
            }
            else
            {
                ToastNotification.Show(_addListControl, @"List with this name is already exists!");
            }

        }

        private void CloseAddListControl()
        {
            CloseModalPanel(_addListControl, DevComponents.DotNetBar.Controls.eSlideSide.Right);
            _addListControl.Dispose();
            _addListControl = null;
            ui_Symbols_ButtonX_AddList.Enabled = true;
        }

       private void ui_Symbols_ButtonX_EditList_Click(object sender, EventArgs e)
        {
            if (ui_symbols_listBox_sumbolsLists.SelectedItems.Count == 0) return;
            ui_Symbols_ButtonX_EditList.Enabled = false;
            var groupName = ui_symbols_listBox_sumbolsLists.SelectedItems[0].ToString();
            var oldGroupInfo = _groups.Find(a => a.GroupName == groupName);

            _editListControl = new EditListControl
                {
                    Commands = _commands,
                    textBoxXListName = { Text = oldGroupInfo.GroupName },
                    startTimeCollect = {Value = oldGroupInfo.Start},
                    endTimeCollect = {Value = oldGroupInfo.End},
                    checkBoxUseTI = {CheckState = CheckState.Checked}
                };

            foreach (var item in _editListControl.cmbHistoricalPeriod.Items)
            {
                if (item.ToString() == oldGroupInfo.TimeFrame)
                {
                    _editListControl.cmbHistoricalPeriod.SelectedItem = item;
                    _editListControl.cmbHistoricalPeriod.Text = item.ToString();
                }
                    
            }

            foreach (var item in _editListControl.cmbContinuationType.Items)
            {
                if (item.ToString() == oldGroupInfo.CntType)
                {
                    _editListControl.cmbContinuationType.SelectedItem = item;
                    _editListControl.cmbContinuationType.Text = item.ToString();
                }
            }

            var symbols = DataManager.GetSymbolsInGroup(oldGroupInfo.GroupId);

            foreach (var symbol in symbols)
            {
                _editListControl.lbSelList.Items.Add(symbol.SymbolName);
            }

            ShowModalPanel(_editListControl, DevComponents.DotNetBar.Controls.eSlideSide.Right);
        }

        private void EditListControl_SaveClick(object sender, EventArgs e)
        {
            var group = new GroupModel
            {
                GroupName = _editListControl.textBoxXListName.Text,
                TimeFrame = _editListControl.cmbHistoricalPeriod.SelectedItem.ToString(),
                Start = _editListControl.startTimeCollect.Value,
                End = _editListControl.endTimeCollect.Value,
                CntType = _editListControl.cmbContinuationType.SelectedItem.ToString()
            };

            var oldGroupName = _editListControl.OldGroupName;
            var groupName = _editListControl.textBoxXListName.Text;
            if ((!_groups.Exists(a => a.GroupName == group.GroupName) && _groups.Exists(a => a.GroupName == oldGroupName)) || (group.GroupName == oldGroupName && _groups.Exists(a => a.GroupName == oldGroupName)))
            {
                var groupId = _groups.Find(a => a.GroupName == oldGroupName).GroupId;
                DataManager.EditGroupOfSymbols(groupId, group);
                var symbolsInGroup = DataManager.GetSymbolsInGroup(groupId);
                foreach (var item in _editListControl.lbSelList.Items)
                {
                    if (!symbolsInGroup.Exists(a => a.SymbolName == item.ToString()) && _symbols.Exists(a => a.SymbolName == item.ToString()))
                    {
                        var symbol = _symbols.Find(a => a.SymbolName == item.ToString());
                        DataManager.AddSymbolIntoGroup(groupId, symbol);
                    }
                }

                symbolsInGroup = DataManager.GetSymbolsInGroup(groupId);
                foreach (var symbol in symbolsInGroup)
                {
                    var exist = false;
                    foreach (var item in _editListControl.lbSelList.Items)
                    {
                        if (symbol.SymbolName == item.ToString()) exist = true;
                    }
                    if (!exist) DataManager.DeleteSymbolFromGroup(groupId, symbol.SymbolId);
                }

                UpdateGroupsTable();
                CloseEditListControl();
                _adminService.GroupChanged();
            }
            else
            {
                ToastNotification.Show(_editListControl, @"List with this name is already exists!");
            }
        }

        private void EditListControl_CancelClick(object sender, EventArgs e)
        {
            CloseEditListControl();
        }

        private void CloseEditListControl()
        {
            CloseModalPanel(_editListControl, DevComponents.DotNetBar.Controls.eSlideSide.Right);
            _editListControl.Dispose();
            _editListControl = null;
            ui_Symbols_ButtonX_EditList.Enabled = true;
        }

        private void ui_Symbols_ButtonX_DeleteList_Click(object sender, EventArgs e)
        {
            if (ui_symbols_listBox_sumbolsLists.SelectedItems.Count > 0)
            {
                if (MessageBox.Show(@"Do you realy want to delete selected lists", @"Deleting list.", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    foreach (var item in ui_symbols_listBox_sumbolsLists.SelectedItems)
                    {
                        var groupId = _groups.Find(a => a.GroupName == item.ToString()).GroupId;
                        DataManager.DeleteGroupOfSymbols(groupId);
                        _adminService.GroupChanged();

                    }
                    UpdateGroupsTable();
                }
            }
            else
            {
                ToastNotification.Show(ui_symbols_listBox_symbols, @"Please, select list.", 1000, eToastPosition.TopCenter);
            }
        }
        #endregion


        #region SYMBOL DETAILS
        private void ui_symbols_listBox_sumbolsLists_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ui_symbols_listBox_sumbolsLists.SelectedItems.Count == 0) return;

            UpdateAllowedUsersTable();
        }

        private void UpdateAllowedUsersTable()
        {
            ui_symbols_comboBox_NotAllowedUsers.Items.Clear();
            ui_symbols_dgrid_AllowedUsers.Rows.Clear();
            ui_symbols_comboBox_NotAllowedUsers.Text = "";

            var groupId = _groups.Find(a => a.GroupName == ui_symbols_listBox_sumbolsLists.SelectedItems[0].ToString()).GroupId;

            var usersForGroup = DataManager.GetUsersForGroup(groupId);

            foreach (var userModel in usersForGroup)
            {
                ui_symbols_dgrid_AllowedUsers.Rows.Add(userModel.Name, userModel.FullName,"X");
           
                }

            foreach (var user in _users)
            {
                var exist = false;
                foreach (var userInGroup in usersForGroup)
                {
                    if (user.Name == userInGroup.Name) exist = true;
                }
                if (!exist) ui_symbols_comboBox_NotAllowedUsers.Items.Add(user.Name);
            }
            if (ui_symbols_comboBox_NotAllowedUsers.Items.Count > 0)
            {
                ui_symbols_comboBox_NotAllowedUsers.SelectedIndex = 0;
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (ui_symbols_listBox_sumbolsLists.SelectedItems.Count == 0 || ui_symbols_comboBox_NotAllowedUsers.SelectedIndex < 0) return;

            var userId = _users.Find(a => a.Name == ui_symbols_comboBox_NotAllowedUsers.SelectedItem.ToString()).Id;

            var group = _groups.Find(a => a.GroupName == ui_symbols_listBox_sumbolsLists.SelectedItems[0].ToString());
            var username = ui_symbols_comboBox_NotAllowedUsers.SelectedItem.ToString();
            DataManager.AddGroupForUser(userId, group);

            Task.Factory.StartNew((Action)
                                  (() => _adminService.SendToClientSymbolGroupList(username)));
            
          
            UpdateAllowedUsersTable();
        }

        private void ui_symbols_dgrid_AllowedUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ui_symbols_dgrid_AllowedUsers.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                DeleteUserFromAllowedUsers(e.RowIndex);
            }
        }

        private void DeleteUserFromAllowedUsers(int rowIndex)
        {
            var userLogin = ui_symbols_dgrid_AllowedUsers[0, rowIndex].Value.ToString();
            var userId = _users.Find(a => a.Name == userLogin).Id;
            var groupId = _groups.Find(a => a.GroupName == ui_symbols_listBox_sumbolsLists.SelectedItems[0].ToString()).GroupId;

     
            if (DataManager.DeleteGroupForUser(userId, groupId))
            {
                UpdateAllowedUsersTable();
            }
            Task.Factory.StartNew((Action)
                                  (() => _adminService.SendToClientSymbolGroupList(userLogin)));
                                   
                
        }

        private void metroTabItem_users_Click(object sender, EventArgs e)
        {
            ui_users_dgridX_users_SelectionChanged(sender, e);
        }

        private void ui_logs_buttonX_Find_Click(object sender, EventArgs e)
        {
            var dateStart = ui_logs_DTime_StartFilter.Value;
            var dateEnd = ui_logs_DTime_EndFilter.Value;

            var logs = DataManager.GetLogBetweenDates(dateStart, dateEnd);
            if (logs.Count > 0)
            {
                //TODO: ui_logs_dGridX_Logs.Rows.Clear();
                foreach (var log in logs)
                {
                    var userName = _users.Find(a => a.Id == log.UserId).Name;
                    ui_logs_dGridX_Logs.Rows.Add(log.Date, userName, log.MsgType, log.Symbol, log.Group, "", log.Status);
                }
            }
            else
            {
                ToastNotification.Show(ui_logs_dGridX_Logs, @"There are no logs for these dates.");
            }
        }
        #endregion


        #region LOGS

        #endregion

        bool ExistTable()
        {
            return false?false:true;
        }
    }
}