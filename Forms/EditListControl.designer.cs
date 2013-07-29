namespace DataAdmin.Forms
{
    partial class EditListControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cmbContinuationType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cmbHistoricalPeriod = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.comboItem4 = new DevComponents.Editors.ComboItem();
            this.comboItem5 = new DevComponents.Editors.ComboItem();
            this.comboItem6 = new DevComponents.Editors.ComboItem();
            this.comboItem7 = new DevComponents.Editors.ComboItem();
            this.comboItem8 = new DevComponents.Editors.ComboItem();
            this.comboItem9 = new DevComponents.Editors.ComboItem();
            this.comboItem10 = new DevComponents.Editors.ComboItem();
            this.comboItem11 = new DevComponents.Editors.ComboItem();
            this.comboItem12 = new DevComponents.Editors.ComboItem();
            this.comboItem13 = new DevComponents.Editors.ComboItem();
            this.comboItem14 = new DevComponents.Editors.ComboItem();
            this.comboItem15 = new DevComponents.Editors.ComboItem();
            this.btnRemovAll = new DevComponents.DotNetBar.ButtonX();
            this.btnRemov = new DevComponents.DotNetBar.ButtonX();
            this.btnAddAll = new DevComponents.DotNetBar.ButtonX();
            this.btnAdd = new DevComponents.DotNetBar.ButtonX();
            this.saveButton = new DevComponents.DotNetBar.ButtonX();
            this.checkBoxUseTI = new System.Windows.Forms.CheckBox();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.lbSelList = new System.Windows.Forms.ListBox();
            this.lbAvbList = new System.Windows.Forms.ListBox();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.grbTimeInterval = new System.Windows.Forms.GroupBox();
            this.endTimeCollect = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.startTimeCollect = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.cancelButton = new DevComponents.DotNetBar.ButtonX();
            this.labelXTitle = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.textBoxXListName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.grbTimeInterval.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.endTimeCollect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.startTimeCollect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbContinuationType
            // 
            this.cmbContinuationType.DisplayMember = "Text";
            this.cmbContinuationType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbContinuationType.FormattingEnabled = true;
            this.cmbContinuationType.ItemHeight = 21;
            this.cmbContinuationType.Location = new System.Drawing.Point(488, 78);
            this.cmbContinuationType.Name = "cmbContinuationType";
            this.cmbContinuationType.Size = new System.Drawing.Size(209, 27);
            this.cmbContinuationType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbContinuationType.TabIndex = 76;
            // 
            // cmbHistoricalPeriod
            // 
            this.cmbHistoricalPeriod.DisplayMember = "Text";
            this.cmbHistoricalPeriod.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbHistoricalPeriod.FormattingEnabled = true;
            this.cmbHistoricalPeriod.ItemHeight = 21;
            this.cmbHistoricalPeriod.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2,
            this.comboItem3,
            this.comboItem4,
            this.comboItem5,
            this.comboItem6,
            this.comboItem7,
            this.comboItem8,
            this.comboItem9,
            this.comboItem10,
            this.comboItem11,
            this.comboItem12,
            this.comboItem13,
            this.comboItem14,
            this.comboItem15});
            this.cmbHistoricalPeriod.Location = new System.Drawing.Point(173, 76);
            this.cmbHistoricalPeriod.Name = "cmbHistoricalPeriod";
            this.cmbHistoricalPeriod.Size = new System.Drawing.Size(169, 27);
            this.cmbHistoricalPeriod.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbHistoricalPeriod.TabIndex = 75;
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "1 minute";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "2 minutes";
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "3 minutes";
            // 
            // comboItem4
            // 
            this.comboItem4.Text = "5 minutes";
            // 
            // comboItem5
            // 
            this.comboItem5.Text = "10 minutes";
            // 
            // comboItem6
            // 
            this.comboItem6.Text = "15 minutes";
            // 
            // comboItem7
            // 
            this.comboItem7.Text = "30 minutes";
            // 
            // comboItem8
            // 
            this.comboItem8.Text = "60 minutes";
            // 
            // comboItem9
            // 
            this.comboItem9.Text = "240 minutes";
            // 
            // comboItem10
            // 
            this.comboItem10.Text = "Daily";
            // 
            // comboItem11
            // 
            this.comboItem11.Text = "Weekly";
            // 
            // comboItem12
            // 
            this.comboItem12.Text = "Monthly";
            // 
            // comboItem13
            // 
            this.comboItem13.Text = "Quarterly";
            // 
            // comboItem14
            // 
            this.comboItem14.Text = "Semiannual";
            // 
            // comboItem15
            // 
            this.comboItem15.Text = "Yearly";
            // 
            // btnRemovAll
            // 
            this.btnRemovAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRemovAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnRemovAll.Location = new System.Drawing.Point(356, 356);
            this.btnRemovAll.Name = "btnRemovAll";
            this.btnRemovAll.Size = new System.Drawing.Size(75, 24);
            this.btnRemovAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnRemovAll.TabIndex = 74;
            this.btnRemovAll.Text = "<<";
            this.btnRemovAll.Click += new System.EventHandler(this.btnRemovAll_Click);
            // 
            // btnRemov
            // 
            this.btnRemov.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRemov.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnRemov.Location = new System.Drawing.Point(356, 326);
            this.btnRemov.Name = "btnRemov";
            this.btnRemov.Size = new System.Drawing.Size(75, 24);
            this.btnRemov.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnRemov.TabIndex = 73;
            this.btnRemov.Text = "<";
            this.btnRemov.Click += new System.EventHandler(this.btnRemov_Click);
            // 
            // btnAddAll
            // 
            this.btnAddAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAddAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnAddAll.Location = new System.Drawing.Point(356, 294);
            this.btnAddAll.Name = "btnAddAll";
            this.btnAddAll.Size = new System.Drawing.Size(75, 24);
            this.btnAddAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnAddAll.TabIndex = 72;
            this.btnAddAll.Text = ">>";
            this.btnAddAll.Click += new System.EventHandler(this.btnAddAll_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAdd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnAdd.Location = new System.Drawing.Point(356, 264);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 24);
            this.btnAdd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnAdd.TabIndex = 71;
            this.btnAdd.Text = ">";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // saveButton
            // 
            this.saveButton.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.saveButton.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.saveButton.Location = new System.Drawing.Point(483, 434);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(92, 31);
            this.saveButton.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.saveButton.TabIndex = 70;
            this.saveButton.Text = "Save";
            // 
            // checkBoxUseTI
            // 
            this.checkBoxUseTI.AutoSize = true;
            this.checkBoxUseTI.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxUseTI.Location = new System.Drawing.Point(147, 124);
            this.checkBoxUseTI.Name = "checkBoxUseTI";
            this.checkBoxUseTI.Size = new System.Drawing.Size(142, 24);
            this.checkBoxUseTI.TabIndex = 69;
            this.checkBoxUseTI.Text = "Use time interval";
            this.checkBoxUseTI.UseVisualStyleBackColor = true;
            this.checkBoxUseTI.CheckStateChanged += new System.EventHandler(this.checkBoxUseTI_CheckStateChanged);
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelX5.Location = new System.Drawing.Point(470, 192);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(131, 21);
            this.labelX5.TabIndex = 68;
            this.labelX5.Text = "Selected symbols:";
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelX4.Location = new System.Drawing.Point(103, 192);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(131, 21);
            this.labelX4.TabIndex = 67;
            this.labelX4.Text = "Available symbols:";
            // 
            // lbSelList
            // 
            this.lbSelList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSelList.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbSelList.FormattingEnabled = true;
            this.lbSelList.ItemHeight = 20;
            this.lbSelList.Location = new System.Drawing.Point(466, 215);
            this.lbSelList.Name = "lbSelList";
            this.lbSelList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbSelList.Size = new System.Drawing.Size(229, 204);
            this.lbSelList.TabIndex = 58;
            this.lbSelList.Click += new System.EventHandler(this.lbSelList_Click);
            this.lbSelList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbSelList_DrawItem);
            this.lbSelList.SelectedIndexChanged += new System.EventHandler(this.lbSelList_SelectedIndexChanged);
            // 
            // lbAvbList
            // 
            this.lbAvbList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbAvbList.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbAvbList.FormattingEnabled = true;
            this.lbAvbList.ItemHeight = 20;
            this.lbAvbList.Location = new System.Drawing.Point(101, 215);
            this.lbAvbList.Name = "lbAvbList";
            this.lbAvbList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbAvbList.Size = new System.Drawing.Size(219, 204);
            this.lbAvbList.Sorted = true;
            this.lbAvbList.TabIndex = 57;
            this.lbAvbList.Click += new System.EventHandler(this.lbAvbList_Click);
            this.lbAvbList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbAvbList_DrawItem);
            this.lbAvbList.SelectedIndexChanged += new System.EventHandler(this.lbAvbList_SelectedIndexChanged);
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelX3.Location = new System.Drawing.Point(348, 82);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(134, 21);
            this.labelX3.TabIndex = 56;
            this.labelX3.Text = "Continuation Types:";
            this.labelX3.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelX2.Location = new System.Drawing.Point(92, 82);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(75, 21);
            this.labelX2.TabIndex = 55;
            this.labelX2.Text = "Timeframe:";
            this.labelX2.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // grbTimeInterval
            // 
            this.grbTimeInterval.Controls.Add(this.endTimeCollect);
            this.grbTimeInterval.Controls.Add(this.startTimeCollect);
            this.grbTimeInterval.Controls.Add(this.labelX7);
            this.grbTimeInterval.Controls.Add(this.labelX6);
            this.grbTimeInterval.Enabled = false;
            this.grbTimeInterval.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.grbTimeInterval.Location = new System.Drawing.Point(296, 108);
            this.grbTimeInterval.Name = "grbTimeInterval";
            this.grbTimeInterval.Size = new System.Drawing.Size(376, 78);
            this.grbTimeInterval.TabIndex = 50;
            this.grbTimeInterval.TabStop = false;
            this.grbTimeInterval.Text = "Time interval";
            // 
            // endTimeCollect
            // 
            // 
            // 
            // 
            this.endTimeCollect.BackgroundStyle.Class = "DateTimeInputBackground";
            this.endTimeCollect.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.endTimeCollect.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.endTimeCollect.ButtonDropDown.Visible = true;
            this.endTimeCollect.IsPopupCalendarOpen = false;
            this.endTimeCollect.Location = new System.Drawing.Point(197, 41);
            // 
            // 
            // 
            this.endTimeCollect.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.endTimeCollect.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.endTimeCollect.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.endTimeCollect.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.endTimeCollect.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.endTimeCollect.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.endTimeCollect.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.endTimeCollect.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.endTimeCollect.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.endTimeCollect.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.endTimeCollect.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.endTimeCollect.MonthCalendar.DisplayMonth = new System.DateTime(2013, 7, 1, 0, 0, 0, 0);
            this.endTimeCollect.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday;
            this.endTimeCollect.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.endTimeCollect.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.endTimeCollect.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.endTimeCollect.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.endTimeCollect.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.endTimeCollect.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.endTimeCollect.MonthCalendar.TodayButtonVisible = true;
            this.endTimeCollect.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.endTimeCollect.Name = "endTimeCollect";
            this.endTimeCollect.Size = new System.Drawing.Size(161, 27);
            this.endTimeCollect.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.endTimeCollect.TabIndex = 72;
            // 
            // startTimeCollect
            // 
            // 
            // 
            // 
            this.startTimeCollect.BackgroundStyle.Class = "DateTimeInputBackground";
            this.startTimeCollect.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.startTimeCollect.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.startTimeCollect.ButtonDropDown.Visible = true;
            this.startTimeCollect.IsPopupCalendarOpen = false;
            this.startTimeCollect.Location = new System.Drawing.Point(16, 41);
            // 
            // 
            // 
            this.startTimeCollect.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.startTimeCollect.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.startTimeCollect.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.startTimeCollect.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.startTimeCollect.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.startTimeCollect.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.startTimeCollect.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.startTimeCollect.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.startTimeCollect.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.startTimeCollect.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.startTimeCollect.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.startTimeCollect.MonthCalendar.DisplayMonth = new System.DateTime(2013, 7, 1, 0, 0, 0, 0);
            this.startTimeCollect.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday;
            this.startTimeCollect.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.startTimeCollect.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.startTimeCollect.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.startTimeCollect.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.startTimeCollect.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.startTimeCollect.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.startTimeCollect.MonthCalendar.TodayButtonVisible = true;
            this.startTimeCollect.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.startTimeCollect.Name = "startTimeCollect";
            this.startTimeCollect.Size = new System.Drawing.Size(158, 27);
            this.startTimeCollect.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.startTimeCollect.TabIndex = 71;
            // 
            // labelX7
            // 
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Location = new System.Drawing.Point(197, 19);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(110, 21);
            this.labelX7.TabIndex = 70;
            this.labelX7.Text = "End time point:";
            // 
            // labelX6
            // 
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(16, 19);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(109, 21);
            this.labelX6.TabIndex = 69;
            this.labelX6.Text = "Start time point:";
            // 
            // cancelButton
            // 
            this.cancelButton.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cancelButton.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cancelButton.Location = new System.Drawing.Point(585, 434);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(92, 31);
            this.cancelButton.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cancelButton.TabIndex = 21;
            this.cancelButton.Text = "Cancel";
            this.toolTip1.SetToolTip(this.cancelButton, "Return without saving");
            // 
            // labelXTitle
            // 
            // 
            // 
            // 
            this.labelXTitle.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelXTitle.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelXTitle.Location = new System.Drawing.Point(101, 4);
            this.labelXTitle.Name = "labelXTitle";
            this.labelXTitle.Size = new System.Drawing.Size(239, 34);
            this.labelXTitle.TabIndex = 19;
            this.labelXTitle.Text = "EDIT SYMBOLS LIST";
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelX1.Location = new System.Drawing.Point(92, 44);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(75, 21);
            this.labelX1.TabIndex = 12;
            this.labelX1.Text = "List Name:";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // textBoxXListName
            // 
            this.textBoxXListName.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.textBoxXListName.Border.Class = "TextBoxBorder";
            this.textBoxXListName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxXListName.ForeColor = System.Drawing.Color.Black;
            this.textBoxXListName.Location = new System.Drawing.Point(173, 44);
            this.textBoxXListName.Name = "textBoxXListName";
            this.textBoxXListName.Size = new System.Drawing.Size(524, 27);
            this.textBoxXListName.TabIndex = 11;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DataAdmin.Properties.Resources.backbutton1;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(44, 44);
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox1, "Cancel");
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // EditListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.cmbContinuationType);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.cmbHistoricalPeriod);
            this.Controls.Add(this.btnRemovAll);
            this.Controls.Add(this.labelXTitle);
            this.Controls.Add(this.btnRemov);
            this.Controls.Add(this.textBoxXListName);
            this.Controls.Add(this.btnAddAll);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.grbTimeInterval);
            this.Controls.Add(this.checkBoxUseTI);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.lbAvbList);
            this.Controls.Add(this.lbSelList);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "EditListControl";
            this.Size = new System.Drawing.Size(800, 500);
            this.Load += new System.EventHandler(this.EditListControl_Load);
            this.grbTimeInterval.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.endTimeCollect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.startTimeCollect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal DevComponents.DotNetBar.Controls.TextBoxX textBoxXListName;
        internal DevComponents.DotNetBar.ButtonX cancelButton;
        internal DevComponents.DotNetBar.LabelX labelXTitle;
        internal DevComponents.DotNetBar.LabelX labelX1;
        internal System.Windows.Forms.GroupBox grbTimeInterval;
        internal DevComponents.DotNetBar.LabelX labelX3;
        internal DevComponents.DotNetBar.LabelX labelX2;
        internal System.Windows.Forms.ListBox lbSelList;
        internal System.Windows.Forms.ListBox lbAvbList;
        internal System.Windows.Forms.PictureBox pictureBox1;
        internal DevComponents.DotNetBar.LabelX labelX5;
        internal DevComponents.DotNetBar.LabelX labelX4;
        internal DevComponents.DotNetBar.LabelX labelX7;
        internal DevComponents.DotNetBar.LabelX labelX6;
        internal System.Windows.Forms.CheckBox checkBoxUseTI;
        internal DevComponents.DotNetBar.ButtonX saveButton;
        private System.Windows.Forms.ToolTip toolTip1;
        private DevComponents.DotNetBar.ButtonX btnRemovAll;
        private DevComponents.DotNetBar.ButtonX btnRemov;
        private DevComponents.DotNetBar.ButtonX btnAddAll;
        private DevComponents.DotNetBar.ButtonX btnAdd;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.ComboItem comboItem3;
        private DevComponents.Editors.ComboItem comboItem4;
        private DevComponents.Editors.ComboItem comboItem5;
        private DevComponents.Editors.ComboItem comboItem6;
        private DevComponents.Editors.ComboItem comboItem7;
        private DevComponents.Editors.ComboItem comboItem8;
        private DevComponents.Editors.ComboItem comboItem9;
        private DevComponents.Editors.ComboItem comboItem10;
        private DevComponents.Editors.ComboItem comboItem11;
        private DevComponents.Editors.ComboItem comboItem12;
        private DevComponents.Editors.ComboItem comboItem13;
        private DevComponents.Editors.ComboItem comboItem14;
        private DevComponents.Editors.ComboItem comboItem15;
        internal DevComponents.Editors.DateTimeAdv.DateTimeInput startTimeCollect;
        internal DevComponents.DotNetBar.Controls.ComboBoxEx cmbContinuationType;
        internal DevComponents.DotNetBar.Controls.ComboBoxEx cmbHistoricalPeriod;
        internal DevComponents.Editors.DateTimeAdv.DateTimeInput endTimeCollect;

    }
}
