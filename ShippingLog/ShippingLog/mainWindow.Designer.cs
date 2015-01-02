using System;

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
            this.components = new System.ComponentModel.Container();
            this.printLog = new System.Windows.Forms.PrintDialog();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.searchInvoiceContainer = new System.Windows.Forms.GroupBox();
            this.searchInvoiceFlowContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.searchFilterButton = new System.Windows.Forms.Button();
            this.searchInvoiceText = new System.Windows.Forms.TextBox();
            this.searchCustomerNameContainer = new System.Windows.Forms.GroupBox();
            this.searchCustomerNameFlowContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.searchCustomerNameFilter = new System.Windows.Forms.Button();
            this.searchCustomerNameText = new System.Windows.Forms.TextBox();
            this.searchRunDriverContainer = new System.Windows.Forms.GroupBox();
            this.searchRunDriverFlowContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.addDriverSearchFilter = new System.Windows.Forms.Button();
            this.searchRunDriverText = new System.Windows.Forms.TextBox();
            this.searchToDateLabel = new System.Windows.Forms.Label();
            this.searchFromDateLabel = new System.Windows.Forms.Label();
            this.searchToDatePicker = new System.Windows.Forms.DateTimePicker();
            this.searchFromDatePicker = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.printLogButton = new System.Windows.Forms.Button();
            this.currentLogDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.autoUpdateRows = new System.Windows.Forms.Timer(this.components);
            this.printLogSheet = new System.Drawing.Printing.PrintDocument();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.htmlParserBrowser = new System.Windows.Forms.WebBrowser();
            this.viewDriverLogDataGrid = new ShippingLog.customDataGridShippingLog();
            this.searchDataGrid = new ShippingLog.customDataGridShippingLog();
            this.calendarColumn1 = new ShippingLog.CalendarColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.updateInvoices = new System.Windows.Forms.Button();
            this.tabPage2.SuspendLayout();
            this.searchInvoiceContainer.SuspendLayout();
            this.searchCustomerNameContainer.SuspendLayout();
            this.searchRunDriverContainer.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // printLog
            // 
            this.printLog.UseEXDialog = true;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.searchInvoiceContainer);
            this.tabPage2.Controls.Add(this.searchCustomerNameContainer);
            this.tabPage2.Controls.Add(this.searchRunDriverContainer);
            this.tabPage2.Controls.Add(this.searchToDateLabel);
            this.tabPage2.Controls.Add(this.searchFromDateLabel);
            this.tabPage2.Controls.Add(this.searchToDatePicker);
            this.tabPage2.Controls.Add(this.searchFromDatePicker);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.searchDataGrid);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(675, 476);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Search Driver Log";
            // 
            // searchInvoiceContainer
            // 
            this.searchInvoiceContainer.Controls.Add(this.searchInvoiceFlowContainer);
            this.searchInvoiceContainer.Controls.Add(this.searchFilterButton);
            this.searchInvoiceContainer.Controls.Add(this.searchInvoiceText);
            this.searchInvoiceContainer.Location = new System.Drawing.Point(13, 173);
            this.searchInvoiceContainer.Name = "searchInvoiceContainer";
            this.searchInvoiceContainer.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.searchInvoiceContainer.Size = new System.Drawing.Size(1061, 55);
            this.searchInvoiceContainer.TabIndex = 25;
            this.searchInvoiceContainer.TabStop = false;
            this.searchInvoiceContainer.Text = "Search Invoice";
            // 
            // searchInvoiceFlowContainer
            // 
            this.searchInvoiceFlowContainer.Dock = System.Windows.Forms.DockStyle.Right;
            this.searchInvoiceFlowContainer.Location = new System.Drawing.Point(261, 13);
            this.searchInvoiceFlowContainer.Margin = new System.Windows.Forms.Padding(0);
            this.searchInvoiceFlowContainer.Name = "searchInvoiceFlowContainer";
            this.searchInvoiceFlowContainer.Size = new System.Drawing.Size(797, 39);
            this.searchInvoiceFlowContainer.TabIndex = 1;
            this.searchInvoiceFlowContainer.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.searchInvoiceFlowContainer_ControlRemoved);
            // 
            // searchFilterButton
            // 
            this.searchFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.searchFilterButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchFilterButton.Location = new System.Drawing.Point(133, 17);
            this.searchFilterButton.Name = "searchFilterButton";
            this.searchFilterButton.Size = new System.Drawing.Size(122, 23);
            this.searchFilterButton.TabIndex = 14;
            this.searchFilterButton.Text = "Add Filter";
            this.searchFilterButton.UseVisualStyleBackColor = false;
            this.searchFilterButton.Click += new System.EventHandler(this.searchFilterButton_Click);
            // 
            // searchInvoiceText
            // 
            this.searchInvoiceText.Location = new System.Drawing.Point(6, 19);
            this.searchInvoiceText.Name = "searchInvoiceText";
            this.searchInvoiceText.Size = new System.Drawing.Size(121, 20);
            this.searchInvoiceText.TabIndex = 0;
            // 
            // searchCustomerNameContainer
            // 
            this.searchCustomerNameContainer.Controls.Add(this.searchCustomerNameFlowContainer);
            this.searchCustomerNameContainer.Controls.Add(this.searchCustomerNameFilter);
            this.searchCustomerNameContainer.Controls.Add(this.searchCustomerNameText);
            this.searchCustomerNameContainer.Location = new System.Drawing.Point(13, 111);
            this.searchCustomerNameContainer.Name = "searchCustomerNameContainer";
            this.searchCustomerNameContainer.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.searchCustomerNameContainer.Size = new System.Drawing.Size(1061, 56);
            this.searchCustomerNameContainer.TabIndex = 21;
            this.searchCustomerNameContainer.TabStop = false;
            this.searchCustomerNameContainer.Text = "Search Customer Name";
            // 
            // searchCustomerNameFlowContainer
            // 
            this.searchCustomerNameFlowContainer.Dock = System.Windows.Forms.DockStyle.Right;
            this.searchCustomerNameFlowContainer.Location = new System.Drawing.Point(261, 13);
            this.searchCustomerNameFlowContainer.Margin = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.searchCustomerNameFlowContainer.Name = "searchCustomerNameFlowContainer";
            this.searchCustomerNameFlowContainer.Size = new System.Drawing.Size(797, 40);
            this.searchCustomerNameFlowContainer.TabIndex = 1;
            this.searchCustomerNameFlowContainer.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.searchCustomerNameFlowContainer_ControlRemoved);
            // 
            // searchCustomerNameFilter
            // 
            this.searchCustomerNameFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.searchCustomerNameFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchCustomerNameFilter.Location = new System.Drawing.Point(133, 17);
            this.searchCustomerNameFilter.Name = "searchCustomerNameFilter";
            this.searchCustomerNameFilter.Size = new System.Drawing.Size(122, 23);
            this.searchCustomerNameFilter.TabIndex = 14;
            this.searchCustomerNameFilter.Text = "Add Filter";
            this.searchCustomerNameFilter.UseVisualStyleBackColor = false;
            this.searchCustomerNameFilter.Click += new System.EventHandler(this.searchCustomerNameFilter_Click);
            // 
            // searchCustomerNameText
            // 
            this.searchCustomerNameText.Location = new System.Drawing.Point(6, 19);
            this.searchCustomerNameText.Name = "searchCustomerNameText";
            this.searchCustomerNameText.Size = new System.Drawing.Size(121, 20);
            this.searchCustomerNameText.TabIndex = 0;
            // 
            // searchRunDriverContainer
            // 
            this.searchRunDriverContainer.Controls.Add(this.searchRunDriverFlowContainer);
            this.searchRunDriverContainer.Controls.Add(this.addDriverSearchFilter);
            this.searchRunDriverContainer.Controls.Add(this.searchRunDriverText);
            this.searchRunDriverContainer.Location = new System.Drawing.Point(13, 51);
            this.searchRunDriverContainer.Name = "searchRunDriverContainer";
            this.searchRunDriverContainer.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.searchRunDriverContainer.Size = new System.Drawing.Size(1061, 60);
            this.searchRunDriverContainer.TabIndex = 20;
            this.searchRunDriverContainer.TabStop = false;
            this.searchRunDriverContainer.Text = "Search Run Driver";
            // 
            // searchRunDriverFlowContainer
            // 
            this.searchRunDriverFlowContainer.Dock = System.Windows.Forms.DockStyle.Right;
            this.searchRunDriverFlowContainer.Location = new System.Drawing.Point(261, 13);
            this.searchRunDriverFlowContainer.Margin = new System.Windows.Forms.Padding(0);
            this.searchRunDriverFlowContainer.Name = "searchRunDriverFlowContainer";
            this.searchRunDriverFlowContainer.Size = new System.Drawing.Size(797, 44);
            this.searchRunDriverFlowContainer.TabIndex = 1;
            this.searchRunDriverFlowContainer.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.searchRunDriverFlowContainer_ControlRemoved);
            // 
            // addDriverSearchFilter
            // 
            this.addDriverSearchFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.addDriverSearchFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addDriverSearchFilter.Location = new System.Drawing.Point(133, 17);
            this.addDriverSearchFilter.Name = "addDriverSearchFilter";
            this.addDriverSearchFilter.Size = new System.Drawing.Size(122, 23);
            this.addDriverSearchFilter.TabIndex = 14;
            this.addDriverSearchFilter.Text = "Add Filter";
            this.addDriverSearchFilter.UseVisualStyleBackColor = false;
            this.addDriverSearchFilter.Click += new System.EventHandler(this.addDriverSearchFilter_Click_1);
            // 
            // searchRunDriverText
            // 
            this.searchRunDriverText.Location = new System.Drawing.Point(6, 18);
            this.searchRunDriverText.Name = "searchRunDriverText";
            this.searchRunDriverText.Size = new System.Drawing.Size(121, 20);
            this.searchRunDriverText.TabIndex = 0;
            // 
            // searchToDateLabel
            // 
            this.searchToDateLabel.AutoSize = true;
            this.searchToDateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchToDateLabel.Location = new System.Drawing.Point(300, 11);
            this.searchToDateLabel.Name = "searchToDateLabel";
            this.searchToDateLabel.Size = new System.Drawing.Size(70, 13);
            this.searchToDateLabel.TabIndex = 19;
            this.searchToDateLabel.Text = "Search To:";
            // 
            // searchFromDateLabel
            // 
            this.searchFromDateLabel.AutoSize = true;
            this.searchFromDateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchFromDateLabel.Location = new System.Drawing.Point(8, 11);
            this.searchFromDateLabel.Name = "searchFromDateLabel";
            this.searchFromDateLabel.Size = new System.Drawing.Size(82, 13);
            this.searchFromDateLabel.TabIndex = 18;
            this.searchFromDateLabel.Text = "Search From:";
            // 
            // searchToDatePicker
            // 
            this.searchToDatePicker.Location = new System.Drawing.Point(370, 8);
            this.searchToDatePicker.Name = "searchToDatePicker";
            this.searchToDatePicker.Size = new System.Drawing.Size(200, 20);
            this.searchToDatePicker.TabIndex = 17;
            this.searchToDatePicker.ValueChanged += new System.EventHandler(this.searchToDatePicker_ValueChanged);
            // 
            // searchFromDatePicker
            // 
            this.searchFromDatePicker.Location = new System.Drawing.Point(91, 8);
            this.searchFromDatePicker.Name = "searchFromDatePicker";
            this.searchFromDatePicker.Size = new System.Drawing.Size(200, 20);
            this.searchFromDatePicker.TabIndex = 16;
            this.searchFromDatePicker.ValueChanged += new System.EventHandler(this.searchFromDatePicker_ValueChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::ShippingLog.Properties.Resources.printIcon1;
            this.button1.Location = new System.Drawing.Point(1188, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(40, 23);
            this.button1.TabIndex = 23;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.updateInvoices);
            this.tabPage1.Controls.Add(this.viewDriverLogDataGrid);
            this.tabPage1.Controls.Add(this.printLogButton);
            this.tabPage1.Controls.Add(this.currentLogDate);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(675, 476);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "View Driver Log";
            // 
            // printLogButton
            // 
            this.printLogButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.printLogButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.printLogButton.Image = global::ShippingLog.Properties.Resources.printIcon1;
            this.printLogButton.Location = new System.Drawing.Point(497, 9);
            this.printLogButton.Name = "printLogButton";
            this.printLogButton.Size = new System.Drawing.Size(40, 23);
            this.printLogButton.TabIndex = 6;
            this.printLogButton.UseVisualStyleBackColor = false;
            this.printLogButton.Click += new System.EventHandler(this.printLogButton_Click_1);
            // 
            // currentLogDate
            // 
            this.currentLogDate.Location = new System.Drawing.Point(72, 12);
            this.currentLogDate.Name = "currentLogDate";
            this.currentLogDate.Size = new System.Drawing.Size(200, 20);
            this.currentLogDate.TabIndex = 3;
            this.currentLogDate.Value = new System.DateTime(2013, 7, 9, 11, 8, 12, 652);
            this.currentLogDate.ValueChanged += new System.EventHandler(this.currentLogDate_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Log Date: ";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(683, 502);
            this.tabControl1.TabIndex = 16;
            // 
            // autoUpdateRows
            // 
            this.autoUpdateRows.Interval = 45000;
            this.autoUpdateRows.Tick += new System.EventHandler(this.autoUpdateRows_Tick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // htmlParserBrowser
            // 
            this.htmlParserBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.htmlParserBrowser.Location = new System.Drawing.Point(0, 0);
            this.htmlParserBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.htmlParserBrowser.Name = "htmlParserBrowser";
            this.htmlParserBrowser.Size = new System.Drawing.Size(683, 502);
            this.htmlParserBrowser.TabIndex = 17;
            this.htmlParserBrowser.Visible = false;
            this.htmlParserBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.htmlParserBrowser_DocumentCompleted);
            // 
            // viewDriverLogDataGrid
            // 
            this.viewDriverLogDataGrid.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.viewDriverLogDataGrid.Location = new System.Drawing.Point(3, 38);
            this.viewDriverLogDataGrid.Name = "viewDriverLogDataGrid";
            this.viewDriverLogDataGrid.Size = new System.Drawing.Size(669, 435);
            this.viewDriverLogDataGrid.TabIndex = 7;
            // 
            // searchDataGrid
            // 
            this.searchDataGrid.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.searchDataGrid.Location = new System.Drawing.Point(3, 248);
            this.searchDataGrid.Name = "searchDataGrid";
            this.searchDataGrid.Size = new System.Drawing.Size(669, 225);
            this.searchDataGrid.TabIndex = 24;
            // 
            // calendarColumn1
            // 
            this.calendarColumn1.HeaderText = "DATE DELIVERED";
            this.calendarColumn1.Name = "calendarColumn1";
            this.calendarColumn1.Width = 200;
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
            this.updateInvoices.Location = new System.Drawing.Point(544, 9);
            this.updateInvoices.Name = "updateInvoices";
            this.updateInvoices.Size = new System.Drawing.Size(123, 23);
            this.updateInvoices.TabIndex = 7;
            this.updateInvoices.Text = "Update Invoices";
            this.updateInvoices.UseVisualStyleBackColor = true;
            this.updateInvoices.Click += new System.EventHandler(this.updateInvoices_Click);
            // 
            // mainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 502);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.htmlParserBrowser);
            this.Name = "mainWindow";
            this.Text = "Shipping Log";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.mainWindow_Resize);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.searchInvoiceContainer.ResumeLayout(false);
            this.searchInvoiceContainer.PerformLayout();
            this.searchCustomerNameContainer.ResumeLayout(false);
            this.searchCustomerNameContainer.PerformLayout();
            this.searchRunDriverContainer.ResumeLayout(false);
            this.searchRunDriverContainer.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private CalendarColumn calendarColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.PrintDialog printLog;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox searchRunDriverContainer;
        private System.Windows.Forms.TextBox searchRunDriverText;
        private System.Windows.Forms.Button addDriverSearchFilter;
        private System.Windows.Forms.Label searchToDateLabel;
        private System.Windows.Forms.Label searchFromDateLabel;
        private System.Windows.Forms.DateTimePicker searchToDatePicker;
        private System.Windows.Forms.DateTimePicker searchFromDatePicker;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker currentLogDate;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button printLogButton;
        private customDataGridShippingLog viewDriverLogDataGrid;
        private customDataGridShippingLog searchDataGrid;
        private System.Windows.Forms.Timer autoUpdateRows;
        private System.Windows.Forms.FlowLayoutPanel searchRunDriverFlowContainer;
        private System.Windows.Forms.GroupBox searchInvoiceContainer;
        private System.Windows.Forms.FlowLayoutPanel searchInvoiceFlowContainer;
        private System.Windows.Forms.Button searchFilterButton;
        private System.Windows.Forms.TextBox searchInvoiceText;
        private System.Windows.Forms.GroupBox searchCustomerNameContainer;
        private System.Windows.Forms.FlowLayoutPanel searchCustomerNameFlowContainer;
        private System.Windows.Forms.Button searchCustomerNameFilter;
        private System.Windows.Forms.TextBox searchCustomerNameText;
        private System.Drawing.Printing.PrintDocument printLogSheet;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.WebBrowser htmlParserBrowser;
        private System.Windows.Forms.Button updateInvoices;
    }
}

