namespace ShippingLog
{
    partial class mainWindow
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
            this.printLog = new System.Windows.Forms.PrintDialog();
            this.logDate = new System.Windows.Forms.DateTimePicker();
            this.dateOfLogLabel = new System.Windows.Forms.Label();
            this.printLogButton = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.THRyanFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.driverHourLogTabPage = new System.Windows.Forms.TabPage();
            this.driversContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.htmlParserBrowser = new System.Windows.Forms.WebBrowser();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.selectDriverBox = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.mileageRateTextbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.hourlyRateTextbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.driverNameTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.updateInvoices = new System.Windows.Forms.Button();
            this.tabPage2.SuspendLayout();
            this.driverHourLogTabPage.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // printLog
            // 
            this.printLog.UseEXDialog = true;
            // 
            // logDate
            // 
            this.logDate.Location = new System.Drawing.Point(92, 3);
            this.logDate.Name = "logDate";
            this.logDate.Size = new System.Drawing.Size(200, 20);
            this.logDate.TabIndex = 1;
            this.logDate.ValueChanged += new System.EventHandler(this.logDate_ValueChanged);
            // 
            // dateOfLogLabel
            // 
            this.dateOfLogLabel.AutoSize = true;
            this.dateOfLogLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateOfLogLabel.Location = new System.Drawing.Point(11, 6);
            this.dateOfLogLabel.Name = "dateOfLogLabel";
            this.dateOfLogLabel.Size = new System.Drawing.Size(78, 13);
            this.dateOfLogLabel.TabIndex = 2;
            this.dateOfLogLabel.Text = "Date of Log:";
            // 
            // printLogButton
            // 
            this.printLogButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.printLogButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.printLogButton.Image = global::hourLogger.Properties.Resources.printIcon;
            this.printLogButton.Location = new System.Drawing.Point(998, 13);
            this.printLogButton.Name = "printLogButton";
            this.printLogButton.Size = new System.Drawing.Size(40, 23);
            this.printLogButton.TabIndex = 7;
            this.printLogButton.UseVisualStyleBackColor = false;
            this.printLogButton.Click += new System.EventHandler(this.printLogButton_Click_1);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tabPage2.Controls.Add(this.THRyanFlowPanel);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1042, 445);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "T.H. Ryan Weekly Invoices";
            // 
            // THRyanFlowPanel
            // 
            this.THRyanFlowPanel.AutoScroll = true;
            this.THRyanFlowPanel.AutoSize = true;
            this.THRyanFlowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.THRyanFlowPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.THRyanFlowPanel.Location = new System.Drawing.Point(3, 3);
            this.THRyanFlowPanel.Name = "THRyanFlowPanel";
            this.THRyanFlowPanel.Size = new System.Drawing.Size(1036, 439);
            this.THRyanFlowPanel.TabIndex = 16;
            this.THRyanFlowPanel.WrapContents = false;
            // 
            // driverHourLogTabPage
            // 
            this.driverHourLogTabPage.AutoScroll = true;
            this.driverHourLogTabPage.BackColor = System.Drawing.SystemColors.ControlLight;
            this.driverHourLogTabPage.Controls.Add(this.driversContainer);
            this.driverHourLogTabPage.Controls.Add(this.htmlParserBrowser);
            this.driverHourLogTabPage.Location = new System.Drawing.Point(4, 22);
            this.driverHourLogTabPage.Name = "driverHourLogTabPage";
            this.driverHourLogTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.driverHourLogTabPage.Size = new System.Drawing.Size(1042, 445);
            this.driverHourLogTabPage.TabIndex = 0;
            this.driverHourLogTabPage.Text = "Driver Hour Log";
            // 
            // driversContainer
            // 
            this.driversContainer.AutoScroll = true;
            this.driversContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.driversContainer.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.driversContainer.Location = new System.Drawing.Point(3, 3);
            this.driversContainer.Name = "driversContainer";
            this.driversContainer.Size = new System.Drawing.Size(1036, 439);
            this.driversContainer.TabIndex = 0;
            this.driversContainer.WrapContents = false;
            this.driversContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.driversContainer_Paint);
            // 
            // htmlParserBrowser
            // 
            this.htmlParserBrowser.Location = new System.Drawing.Point(249, -10);
            this.htmlParserBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.htmlParserBrowser.Name = "htmlParserBrowser";
            this.htmlParserBrowser.Size = new System.Drawing.Size(20, 250);
            this.htmlParserBrowser.TabIndex = 0;
            this.htmlParserBrowser.Visible = false;
            this.htmlParserBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.htmlParserBrowser_DocumentCompleted);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.driverHourLogTabPage);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl1.Location = new System.Drawing.Point(0, 31);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1050, 471);
            this.tabControl1.TabIndex = 9;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tabPage1.Controls.Add(this.button3);
            this.tabPage1.Controls.Add(this.selectDriverBox);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.mileageRateTextbox);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.hourlyRateTextbox);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.driverNameTextbox);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1042, 445);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Manage Drivers";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(266, 121);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(115, 23);
            this.button3.TabIndex = 9;
            this.button3.Text = "Remove Driver";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // selectDriverBox
            // 
            this.selectDriverBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectDriverBox.FormattingEnabled = true;
            this.selectDriverBox.Location = new System.Drawing.Point(11, 8);
            this.selectDriverBox.Name = "selectDriverBox";
            this.selectDriverBox.Size = new System.Drawing.Size(265, 21);
            this.selectDriverBox.TabIndex = 8;
            this.selectDriverBox.SelectedIndexChanged += new System.EventHandler(this.selectDriverBox_SelectedIndexChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(136, 121);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(115, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Add New Driver";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(7, 121);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Update Current Driver";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // mileageRateTextbox
            // 
            this.mileageRateTextbox.Location = new System.Drawing.Point(104, 93);
            this.mileageRateTextbox.Name = "mileageRateTextbox";
            this.mileageRateTextbox.Size = new System.Drawing.Size(172, 20);
            this.mileageRateTextbox.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Mileage Rate:";
            // 
            // hourlyRateTextbox
            // 
            this.hourlyRateTextbox.Location = new System.Drawing.Point(105, 64);
            this.hourlyRateTextbox.Name = "hourlyRateTextbox";
            this.hourlyRateTextbox.Size = new System.Drawing.Size(172, 20);
            this.hourlyRateTextbox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Hourly Rate:";
            // 
            // driverNameTextbox
            // 
            this.driverNameTextbox.Location = new System.Drawing.Point(104, 36);
            this.driverNameTextbox.Name = "driverNameTextbox";
            this.driverNameTextbox.Size = new System.Drawing.Size(172, 20);
            this.driverNameTextbox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Driver name: ";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "ID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 25;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "AMOUNT";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 200;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "RUN DRIVER";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 250;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "CUSTOMER NAME";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 300;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "INVOICE";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 200;
            // 
            // updateInvoices
            // 
            this.updateInvoices.Location = new System.Drawing.Point(862, 13);
            this.updateInvoices.Name = "updateInvoices";
            this.updateInvoices.Size = new System.Drawing.Size(125, 23);
            this.updateInvoices.TabIndex = 10;
            this.updateInvoices.Text = "Update Invoices";
            this.updateInvoices.UseVisualStyleBackColor = true;
            this.updateInvoices.Click += new System.EventHandler(this.updateInvoices_Click);
            // 
            // mainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 502);
            this.Controls.Add(this.updateInvoices);
            this.Controls.Add(this.printLogButton);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.dateOfLogLabel);
            this.Controls.Add(this.logDate);
            this.Name = "mainWindow";
            this.Text = "Driver Log";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.mainWindow_Resize);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.driverHourLogTabPage.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.PrintDialog printLog;
        private hourLogger.addTHRyanDriver addTHRyanDriver1;
        private System.Windows.Forms.DateTimePicker logDate;
        private System.Windows.Forms.Label dateOfLogLabel;
        private System.Windows.Forms.Button printLogButton;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.FlowLayoutPanel THRyanFlowPanel;
        private System.Windows.Forms.TabPage driverHourLogTabPage;
        private System.Windows.Forms.FlowLayoutPanel driversContainer;
        private System.Windows.Forms.WebBrowser htmlParserBrowser;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ComboBox selectDriverBox;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox mileageRateTextbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox hourlyRateTextbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox driverNameTextbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button updateInvoices;
    }
}

