namespace RRS
{
    partial class DeliverySchedule
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.nextDay = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textCostPercentage = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.textTotalMiles = new System.Windows.Forms.TextBox();
            this.textValueMain = new System.Windows.Forms.TextBox();
            this.textEstimatedCost = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.deleteRoute = new System.Windows.Forms.Button();
            this.addRoute = new System.Windows.Forms.Button();
            this.listCompanyDrivers = new System.Windows.Forms.ListBox();
            this.listScheduledDrivers = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.listInvoices = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.findAddress = new System.Windows.Forms.Button();
            this.listItems = new System.Windows.Forms.ListBox();
            this.label14 = new System.Windows.Forms.Label();
            this.AddInvoice = new System.Windows.Forms.Button();
            this.textState = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textValue = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.textCusomter = new System.Windows.Forms.TextBox();
            this.textCustomer1 = new System.Windows.Forms.Label();
            this.textZip = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textCity = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textStreet = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textInvoice = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.update = new System.Windows.Forms.Button();
            this.Exit = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.configureRoute = new System.Windows.Forms.Button();
            this.textMaitenance = new System.Windows.Forms.TextBox();
            this.textFuel = new System.Windows.Forms.TextBox();
            this.textHourly = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.textOvertime = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.Calendar = new System.Windows.Forms.DateTimePicker();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.webkitBrowser2 = new WebKit.WebKitBrowser();
            this.checkDisplayRoute = new System.Windows.Forms.CheckBox();
            this.buttonPrint = new System.Windows.Forms.Button();
            this.buttonSwap = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // nextDay
            // 
            this.nextDay.Location = new System.Drawing.Point(747, 9);
            this.nextDay.Margin = new System.Windows.Forms.Padding(0);
            this.nextDay.Name = "nextDay";
            this.nextDay.Size = new System.Drawing.Size(228, 40);
            this.nextDay.TabIndex = 2;
            this.nextDay.Text = "Next Day >";
            this.nextDay.UseVisualStyleBackColor = true;
            this.nextDay.Click += new System.EventHandler(this.nextDay_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textCostPercentage);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.textTotalMiles);
            this.groupBox1.Controls.Add(this.textValueMain);
            this.groupBox1.Controls.Add(this.textEstimatedCost);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(447, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(545, 47);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General Information";
            // 
            // textCostPercentage
            // 
            this.textCostPercentage.Enabled = false;
            this.textCostPercentage.Location = new System.Drawing.Point(360, 19);
            this.textCostPercentage.Name = "textCostPercentage";
            this.textCostPercentage.Size = new System.Drawing.Size(38, 20);
            this.textCostPercentage.TabIndex = 9;
            this.textCostPercentage.Text = "50%";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(274, 22);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(86, 13);
            this.label18.TabIndex = 8;
            this.label18.Text = "Cost Percentage";
            // 
            // textTotalMiles
            // 
            this.textTotalMiles.Enabled = false;
            this.textTotalMiles.Location = new System.Drawing.Point(469, 19);
            this.textTotalMiles.Name = "textTotalMiles";
            this.textTotalMiles.Size = new System.Drawing.Size(53, 20);
            this.textTotalMiles.TabIndex = 7;
            this.textTotalMiles.Text = "2000 Mi";
            // 
            // textValueMain
            // 
            this.textValueMain.Enabled = false;
            this.textValueMain.Location = new System.Drawing.Point(198, 19);
            this.textValueMain.Name = "textValueMain";
            this.textValueMain.Size = new System.Drawing.Size(70, 20);
            this.textValueMain.TabIndex = 6;
            this.textValueMain.Text = "$100,000";
            // 
            // textEstimatedCost
            // 
            this.textEstimatedCost.Enabled = false;
            this.textEstimatedCost.Location = new System.Drawing.Point(86, 19);
            this.textEstimatedCost.Name = "textEstimatedCost";
            this.textEstimatedCost.Size = new System.Drawing.Size(70, 20);
            this.textEstimatedCost.TabIndex = 3;
            this.textEstimatedCost.Text = "$50,000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(162, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Value";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Estimated Cost";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(405, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Total Miles";
            // 
            // deleteRoute
            // 
            this.deleteRoute.Location = new System.Drawing.Point(612, 120);
            this.deleteRoute.Margin = new System.Windows.Forms.Padding(0);
            this.deleteRoute.Name = "deleteRoute";
            this.deleteRoute.Size = new System.Drawing.Size(45, 121);
            this.deleteRoute.TabIndex = 23;
            this.deleteRoute.Text = ">";
            this.deleteRoute.UseVisualStyleBackColor = true;
            this.deleteRoute.Click += new System.EventHandler(this.deleteRoute_Click);
            // 
            // addRoute
            // 
            this.addRoute.Location = new System.Drawing.Point(571, 120);
            this.addRoute.Margin = new System.Windows.Forms.Padding(0);
            this.addRoute.Name = "addRoute";
            this.addRoute.Size = new System.Drawing.Size(41, 121);
            this.addRoute.TabIndex = 22;
            this.addRoute.Text = "<";
            this.addRoute.UseVisualStyleBackColor = true;
            this.addRoute.Click += new System.EventHandler(this.addRoute_Click);
            // 
            // listCompanyDrivers
            // 
            this.listCompanyDrivers.FormattingEnabled = true;
            this.listCompanyDrivers.Items.AddRange(new object[] {
            "Ron - Trailer"});
            this.listCompanyDrivers.Location = new System.Drawing.Point(660, 120);
            this.listCompanyDrivers.Name = "listCompanyDrivers";
            this.listCompanyDrivers.Size = new System.Drawing.Size(121, 121);
            this.listCompanyDrivers.TabIndex = 21;
            this.listCompanyDrivers.SelectedIndexChanged += new System.EventHandler(this.listCompanyDrivers_SelectedIndexChanged);
            // 
            // listScheduledDrivers
            // 
            this.listScheduledDrivers.FormattingEnabled = true;
            this.listScheduledDrivers.Items.AddRange(new object[] {
            "Frank - Trailer",
            "Pete - Trailer"});
            this.listScheduledDrivers.Location = new System.Drawing.Point(447, 120);
            this.listScheduledDrivers.Name = "listScheduledDrivers";
            this.listScheduledDrivers.Size = new System.Drawing.Size(121, 121);
            this.listScheduledDrivers.TabIndex = 20;
            this.listScheduledDrivers.SelectedIndexChanged += new System.EventHandler(this.listScheduledDrivers_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(444, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Scheduled Drivers";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(660, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "Company Drivers";
            // 
            // listInvoices
            // 
            this.listInvoices.FormattingEnabled = true;
            this.listInvoices.Items.AddRange(new object[] {
            "5491321 - Frank",
            "7491513 - Unassigned",
            "6491512 - Unassigned"});
            this.listInvoices.Location = new System.Drawing.Point(9, 19);
            this.listInvoices.Name = "listInvoices";
            this.listInvoices.Size = new System.Drawing.Size(238, 290);
            this.listInvoices.TabIndex = 27;
            this.listInvoices.SelectedIndexChanged += new System.EventHandler(this.listInvoices_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonRemove);
            this.groupBox2.Controls.Add(this.findAddress);
            this.groupBox2.Controls.Add(this.listItems);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.AddInvoice);
            this.groupBox2.Controls.Add(this.textState);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.textValue);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.buttonEdit);
            this.groupBox2.Controls.Add(this.textCusomter);
            this.groupBox2.Controls.Add(this.textCustomer1);
            this.groupBox2.Controls.Add(this.textZip);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.textCity);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.textStreet);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.textInvoice);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.update);
            this.groupBox2.Controls.Add(this.listInvoices);
            this.groupBox2.Location = new System.Drawing.Point(447, 284);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(545, 346);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Invoices To Ship";
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(401, 313);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(140, 25);
            this.buttonRemove.TabIndex = 54;
            this.buttonRemove.Text = "Remove Invoice";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // findAddress
            // 
            this.findAddress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.findAddress.Image = global::RRS.Properties.Resources.redx;
            this.findAddress.Location = new System.Drawing.Point(518, 85);
            this.findAddress.Name = "findAddress";
            this.findAddress.Size = new System.Drawing.Size(23, 23);
            this.findAddress.TabIndex = 53;
            this.findAddress.UseVisualStyleBackColor = true;
            this.findAddress.Click += new System.EventHandler(this.findAddress_Click);
            // 
            // listItems
            // 
            this.listItems.FormattingEnabled = true;
            this.listItems.Items.AddRange(new object[] {
            "3/4 X 11-1/4 POPLAR S4S",
            "5/8 X 49 X 109 A35 HARDROCK MAPLE VINYL 2SDS PBC"});
            this.listItems.Location = new System.Drawing.Point(253, 163);
            this.listItems.Name = "listItems";
            this.listItems.Size = new System.Drawing.Size(288, 147);
            this.listItems.TabIndex = 52;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(252, 147);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(55, 13);
            this.label14.TabIndex = 50;
            this.label14.Text = "Line Items";
            // 
            // AddInvoice
            // 
            this.AddInvoice.Location = new System.Drawing.Point(8, 313);
            this.AddInvoice.Name = "AddInvoice";
            this.AddInvoice.Size = new System.Drawing.Size(115, 25);
            this.AddInvoice.TabIndex = 48;
            this.AddInvoice.Text = "Add Invoice";
            this.AddInvoice.UseVisualStyleBackColor = true;
            this.AddInvoice.Click += new System.EventHandler(this.AddInvoice_Click);
            // 
            // textState
            // 
            this.textState.Enabled = false;
            this.textState.Location = new System.Drawing.Point(494, 114);
            this.textState.Name = "textState";
            this.textState.Size = new System.Drawing.Size(47, 20);
            this.textState.TabIndex = 47;
            this.textState.Text = "IL";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(449, 117);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(32, 13);
            this.label13.TabIndex = 46;
            this.label13.Text = "State";
            // 
            // textValue
            // 
            this.textValue.Enabled = false;
            this.textValue.Location = new System.Drawing.Point(464, 10);
            this.textValue.Name = "textValue";
            this.textValue.Size = new System.Drawing.Size(77, 20);
            this.textValue.TabIndex = 45;
            this.textValue.Text = "$1824";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(424, 13);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(34, 13);
            this.label12.TabIndex = 44;
            this.label12.Text = "Value";
            // 
            // buttonEdit
            // 
            this.buttonEdit.Location = new System.Drawing.Point(253, 313);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(140, 25);
            this.buttonEdit.TabIndex = 41;
            this.buttonEdit.Text = "Edit Invoice";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.button10_Click);
            // 
            // textCusomter
            // 
            this.textCusomter.Enabled = false;
            this.textCusomter.Location = new System.Drawing.Point(341, 36);
            this.textCusomter.Name = "textCusomter";
            this.textCusomter.Size = new System.Drawing.Size(200, 20);
            this.textCusomter.TabIndex = 39;
            this.textCusomter.Text = "CRAFTWOOD COMPANY INC";
            // 
            // textCustomer1
            // 
            this.textCustomer1.AutoSize = true;
            this.textCustomer1.Location = new System.Drawing.Point(250, 39);
            this.textCustomer1.Name = "textCustomer1";
            this.textCustomer1.Size = new System.Drawing.Size(82, 13);
            this.textCustomer1.TabIndex = 38;
            this.textCustomer1.Text = "Customer Name";
            // 
            // textZip
            // 
            this.textZip.Enabled = false;
            this.textZip.Location = new System.Drawing.Point(317, 114);
            this.textZip.Name = "textZip";
            this.textZip.Size = new System.Drawing.Size(85, 20);
            this.textZip.TabIndex = 37;
            this.textZip.Text = "60126";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(252, 117);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(22, 13);
            this.label9.TabIndex = 36;
            this.label9.Text = "Zip";
            // 
            // textCity
            // 
            this.textCity.Enabled = false;
            this.textCity.Location = new System.Drawing.Point(316, 88);
            this.textCity.Name = "textCity";
            this.textCity.Size = new System.Drawing.Size(196, 20);
            this.textCity.TabIndex = 35;
            this.textCity.Text = "Elmhurst";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(250, 91);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(24, 13);
            this.label8.TabIndex = 34;
            this.label8.Text = "City";
            // 
            // textStreet
            // 
            this.textStreet.Enabled = false;
            this.textStreet.Location = new System.Drawing.Point(317, 62);
            this.textStreet.Name = "textStreet";
            this.textStreet.Size = new System.Drawing.Size(224, 20);
            this.textStreet.TabIndex = 33;
            this.textStreet.Text = "499 WEST WRIGHTWOOD AVENUE";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(250, 65);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 32;
            this.label7.Text = "Street";
            // 
            // textInvoice
            // 
            this.textInvoice.Enabled = false;
            this.textInvoice.Location = new System.Drawing.Point(341, 10);
            this.textInvoice.Name = "textInvoice";
            this.textInvoice.Size = new System.Drawing.Size(72, 20);
            this.textInvoice.TabIndex = 31;
            this.textInvoice.Text = "5491321";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(250, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 13);
            this.label6.TabIndex = 30;
            this.label6.Text = "Invoice Number";
            // 
            // update
            // 
            this.update.Location = new System.Drawing.Point(132, 313);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(115, 25);
            this.update.TabIndex = 29;
            this.update.Text = "Update Invoices";
            this.update.UseVisualStyleBackColor = true;
            this.update.Click += new System.EventHandler(this.update_Click);
            // 
            // Exit
            // 
            this.Exit.Location = new System.Drawing.Point(447, 672);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(522, 35);
            this.Exit.TabIndex = 43;
            this.Exit.Text = "Back";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(798, 244);
            this.button13.Margin = new System.Windows.Forms.Padding(0);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(190, 40);
            this.button13.TabIndex = 44;
            this.button13.Text = "Optimize Daily Route";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // configureRoute
            // 
            this.configureRoute.Location = new System.Drawing.Point(447, 244);
            this.configureRoute.Margin = new System.Windows.Forms.Padding(0);
            this.configureRoute.Name = "configureRoute";
            this.configureRoute.Size = new System.Drawing.Size(121, 40);
            this.configureRoute.TabIndex = 45;
            this.configureRoute.Text = "Configure Route";
            this.configureRoute.UseVisualStyleBackColor = true;
            this.configureRoute.Click += new System.EventHandler(this.ConfigureRoute_Click);
            // 
            // textMaitenance
            // 
            this.textMaitenance.Enabled = false;
            this.textMaitenance.Location = new System.Drawing.Point(914, 207);
            this.textMaitenance.Name = "textMaitenance";
            this.textMaitenance.Size = new System.Drawing.Size(74, 20);
            this.textMaitenance.TabIndex = 51;
            this.textMaitenance.Text = ".35";
            // 
            // textFuel
            // 
            this.textFuel.Enabled = false;
            this.textFuel.Location = new System.Drawing.Point(914, 181);
            this.textFuel.Name = "textFuel";
            this.textFuel.Size = new System.Drawing.Size(74, 20);
            this.textFuel.TabIndex = 50;
            this.textFuel.Text = ".49";
            // 
            // textHourly
            // 
            this.textHourly.Enabled = false;
            this.textHourly.Location = new System.Drawing.Point(914, 129);
            this.textHourly.Name = "textHourly";
            this.textHourly.Size = new System.Drawing.Size(74, 20);
            this.textHourly.TabIndex = 49;
            this.textHourly.Text = "52.74";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(795, 210);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(121, 13);
            this.label15.TabIndex = 48;
            this.label15.Text = "Maintenance Surcharge";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(795, 184);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(79, 13);
            this.label16.TabIndex = 47;
            this.label16.Text = "Fuel Surcharge";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(795, 132);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(63, 13);
            this.label17.TabIndex = 46;
            this.label17.Text = "Hourly Rate";
            // 
            // textOvertime
            // 
            this.textOvertime.Enabled = false;
            this.textOvertime.Location = new System.Drawing.Point(914, 155);
            this.textOvertime.Name = "textOvertime";
            this.textOvertime.Size = new System.Drawing.Size(74, 20);
            this.textOvertime.TabIndex = 53;
            this.textOvertime.Text = ".49";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(795, 158);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 13);
            this.label10.TabIndex = 52;
            this.label10.Text = "Overtime Rate";
            // 
            // Calendar
            // 
            this.Calendar.Cursor = System.Windows.Forms.Cursors.Default;
            this.Calendar.CustomFormat = "";
            this.Calendar.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Calendar.Location = new System.Drawing.Point(8, 19);
            this.Calendar.Margin = new System.Windows.Forms.Padding(0);
            this.Calendar.Name = "Calendar";
            this.Calendar.Size = new System.Drawing.Size(207, 20);
            this.Calendar.TabIndex = 10;
            this.Calendar.ValueChanged += new System.EventHandler(this.Calendar_ValueChanged);
            this.Calendar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Calendar_KeyPress);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Calendar);
            this.groupBox3.Location = new System.Drawing.Point(447, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox3.Size = new System.Drawing.Size(225, 46);
            this.groupBox3.TabIndex = 54;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Jump to day";
            // 
            // webkitBrowser2
            // 
            this.webkitBrowser2.BackColor = System.Drawing.Color.White;
            this.webkitBrowser2.Location = new System.Drawing.Point(12, 12);
            this.webkitBrowser2.Name = "webkitBrowser2";
            this.webkitBrowser2.Size = new System.Drawing.Size(429, 695);
            this.webkitBrowser2.TabIndex = 55;
            this.webkitBrowser2.Url = null;
            // 
            // checkDisplayRoute
            // 
            this.checkDisplayRoute.AutoSize = true;
            this.checkDisplayRoute.Location = new System.Drawing.Point(571, 257);
            this.checkDisplayRoute.Name = "checkDisplayRoute";
            this.checkDisplayRoute.Size = new System.Drawing.Size(92, 17);
            this.checkDisplayRoute.TabIndex = 56;
            this.checkDisplayRoute.Text = "Display Route";
            this.checkDisplayRoute.UseVisualStyleBackColor = true;
            this.checkDisplayRoute.Click += new System.EventHandler(this.checkDisplayRoute_Click);
            // 
            // buttonPrint
            // 
            this.buttonPrint.Location = new System.Drawing.Point(447, 636);
            this.buttonPrint.Name = "buttonPrint";
            this.buttonPrint.Size = new System.Drawing.Size(522, 35);
            this.buttonPrint.TabIndex = 57;
            this.buttonPrint.Text = "Print Summary";
            this.buttonPrint.UseVisualStyleBackColor = true;
            this.buttonPrint.Click += new System.EventHandler(this.buttonPrint_Click);
            // 
            // buttonSwap
            // 
            this.buttonSwap.Location = new System.Drawing.Point(660, 244);
            this.buttonSwap.Margin = new System.Windows.Forms.Padding(0);
            this.buttonSwap.Name = "buttonSwap";
            this.buttonSwap.Size = new System.Drawing.Size(121, 40);
            this.buttonSwap.TabIndex = 58;
            this.buttonSwap.Text = "Swap Driver";
            this.buttonSwap.UseVisualStyleBackColor = true;
            this.buttonSwap.Click += new System.EventHandler(this.buttonSwap_Click);
            // 
            // DeliverySchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 712);
            this.Controls.Add(this.buttonSwap);
            this.Controls.Add(this.buttonPrint);
            this.Controls.Add(this.checkDisplayRoute);
            this.Controls.Add(this.webkitBrowser2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.textOvertime);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textMaitenance);
            this.Controls.Add(this.textFuel);
            this.Controls.Add(this.textHourly);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.configureRoute);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.Exit);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.addRoute);
            this.Controls.Add(this.listCompanyDrivers);
            this.Controls.Add(this.listScheduledDrivers);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.nextDay);
            this.Controls.Add(this.deleteRoute);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "DeliverySchedule";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.Load += new System.EventHandler(this.DeliverySchedule_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button nextDay;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textTotalMiles;
        private System.Windows.Forms.TextBox textValueMain;
        private System.Windows.Forms.TextBox textEstimatedCost;
        private System.Windows.Forms.Button deleteRoute;
        private System.Windows.Forms.Button addRoute;
        private System.Windows.Forms.ListBox listCompanyDrivers;
        private System.Windows.Forms.ListBox listScheduledDrivers;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox listInvoices;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button update;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.TextBox textCusomter;
        private System.Windows.Forms.Label textCustomer1;
        private System.Windows.Forms.TextBox textZip;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textCity;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textStreet;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textInvoice;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.TextBox textState;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textValue;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button configureRoute;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textMaitenance;
        private System.Windows.Forms.TextBox textFuel;
        private System.Windows.Forms.TextBox textHourly;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ListBox listItems;
        private System.Windows.Forms.TextBox textCostPercentage;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox textOvertime;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.Button AddInvoice;
        private System.Windows.Forms.DateTimePicker Calendar;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button findAddress;
        private WebKit.WebKitBrowser webkitBrowser2;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.CheckBox checkDisplayRoute;
        private System.Windows.Forms.Button buttonPrint;
        private System.Windows.Forms.Button buttonSwap;
    }
}