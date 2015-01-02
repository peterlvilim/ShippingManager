using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ShippingLog;
using hourLogger;
using MySql.Data.MySqlClient;
using System.IO;

namespace ShippingLog
{
    public partial class mainWindow : Form
    {
        RRSDataReader rrsdatareader;
        static BackgroundWorker _bw3;
        MySQL_Invoices mysql_invoices;
        public mainWindow()
        {
            InitializeComponent();
            viewDriverLogDataGrid.sendDataGridNewDate(currentLogDate.Value);
            populateViewLogTable();
            autoUpdateRows.Start();
            searchDataGrid.stopUserDrag();
            searchAndPopulate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

        }

        private void topTableContainer_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
        }

        private void printLogButton_Click(object sender, EventArgs e)
        {
            printLog.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            searchQueryWindow newSearchWindow = new searchQueryWindow();
            newSearchWindow.Show();
        }




        private void addDriverSearchFilter_Click_1(object sender, EventArgs e)
        {
            searchRunDriverFlowContainer.Controls.Add(new filterBox(searchRunDriverText.Text));
            searchRunDriverText.Text = "";
            searchAndPopulate();
        }

        private void populateViewLogTable()
        {
            try
            {
               
                    viewDriverLogDataGrid.clearRows();
                    int endRowCounter = 0;
                    string currentDate = currentLogDate.Value.ToString("yyyy-MM-dd");
                    MySQL MySQLHandle = new MySQL(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
                    MySQL MySQLHandle2 = new MySQL(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
                    MySqlConnection sqlReader2 = MySQLHandle2.Connect();
                    MySqlConnection sqlReader = MySQLHandle.Connect();
                    MySqlDataReader dataReader = MySQLHandle.Select("SELECT * FROM shipping_log WHERE date_delivered='" + currentDate + "' ORDER BY `order` ASC;", sqlReader);
                    for (int rowCounter = 0; dataReader.Read(); rowCounter++)
                    {
                        endRowCounter++;
                        string id = dataReader.GetString(0);
                        string dateDelivered = dataReader.GetString(1);
                        string runDriver = dataReader.GetString(2);
                        string customerName = dataReader.GetString(3);
                        string invoice = dataReader.GetString(4);

                        if (customerName == "" && invoice != "")
                        {

                            try
                            {
                                MySqlDataReader dataReader2 = MySQLHandle2.Select("SELECT `INV_CUSTOMER_NAME` FROM invoice_data WHERE `INV_NUMBER`=" + invoice + ";", sqlReader2);
                                dataReader2.Read();
                                if (dataReader2.HasRows == true)
                                {
                                    customerName = dataReader2.GetString(0);

                                }
                                dataReader2.Close();
                            }
                            catch { }

                        }
                        viewDriverLogDataGrid.addRow(dateDelivered, runDriver, customerName, invoice, id);
                    }
                    if (viewDriverLogDataGrid.returnRows().Count == 0)
                    {
                        viewDriverLogDataGrid.addBlankRow(currentLogDate.Value);
                    }
                    dataReader.Close();
                    sqlReader.Close();
                    sqlReader2.Close();
                
            }
            catch { }
        }

        private void autoUpdateRows_Tick(object sender, EventArgs e)
        {
            autoUpdateRowsFunction();
        }

        private void autoUpdateRowsFunction()
        {
            if (viewDriverLogDataGrid.editing == false)
            {
               
                viewDriverLogDataGrid.setOldRows();
                
                populateViewLogTable();
                viewDriverLogDataGrid.selectOldCells();
               
            }
        }

        private void currentLogDate_ValueChanged(object sender, EventArgs e)
        {
            viewDriverLogDataGrid.sendDataGridNewDate(currentLogDate.Value);
            populateViewLogTable();
        }

        private void searchCustomerNameFilter_Click(object sender, EventArgs e)
        {
            searchCustomerNameFlowContainer.Controls.Add(new filterBox(searchCustomerNameText.Text));
            searchCustomerNameText.Text = "";
            searchAndPopulate();
        }

        private void searchFilterButton_Click(object sender, EventArgs e)
        {
            searchInvoiceFlowContainer.Controls.Add(new filterBox(searchInvoiceText.Text));
            searchInvoiceText.Text = "";
            searchAndPopulate();
        }

        private void searchAndPopulate()
        {
            searchDataGrid.clearRows();
            string startDate = searchFromDatePicker.Value.ToString("yyyy-MM-dd");
            string endDate = searchToDatePicker.Value.ToString("yyyy-MM-dd");
            string filterMasterString = "";
            foreach (filterBox filterInstance in searchRunDriverFlowContainer.Controls)
            {
                string filterInstanceString=filterInstance.getFilter();
                filterMasterString += (" && (run_driver='" + filterInstanceString + "')");
            }

            foreach (filterBox filterInstance in searchCustomerNameFlowContainer.Controls)
            {
                string filterInstanceString = filterInstance.getFilter();
                filterMasterString += (" && (customer_name='" + filterInstanceString + "')");
            }

            foreach (filterBox filterInstance in searchInvoiceFlowContainer.Controls)
            {
                string filterInstanceString = filterInstance.getFilter();
                filterMasterString += (" && (invoice='" + filterInstanceString + "')");
            }


            MySQL MySQLHandle = new MySQL(GlobalVar.sqlhost,GlobalVar.sqlport,GlobalVar.sqldatabase,GlobalVar.sqlusername,"");
            MySqlConnection sqlReader = MySQLHandle.Connect();
            MySqlDataReader dataReader = MySQLHandle.Select("SELECT * FROM shipping_log WHERE (date_delivered BETWEEN DATE('" + startDate + "') AND DATE('" + endDate + "'))" + filterMasterString + ";", sqlReader);
            for (int rowCounter = 0; dataReader.Read(); rowCounter++)
            {
                string id = dataReader.GetString(0);
                string dateDelivered = dataReader.GetString(1);
                string runDriver = dataReader.GetString(2);
                string customerName = dataReader.GetString(3);
                string invoice = dataReader.GetString(4);
                searchDataGrid.addRow(dateDelivered, runDriver, customerName, invoice, id);
            }
            dataReader.Close();
            sqlReader.Close();
            searchDataGrid.stopUserEdit();
        }

        private void searchFromDatePicker_ValueChanged(object sender, EventArgs e)
        {
            searchAndPopulate();
        }

        private void searchToDatePicker_ValueChanged(object sender, EventArgs e)
        {
            searchAndPopulate();
        }

        private void searchRunDriverFlowContainer_ControlRemoved(object sender, ControlEventArgs e)
        {
            searchAndPopulate();
        }

        private void searchCustomerNameFlowContainer_ControlRemoved(object sender, ControlEventArgs e)
        {
            searchAndPopulate();
        }

        private void searchInvoiceFlowContainer_ControlRemoved(object sender, ControlEventArgs e)
        {
            searchAndPopulate();
        }

        private void printLogButton_Click_1(object sender, EventArgs e)
        {
            viewDriverLogDataGrid.sortByInvoices();
            string currentdirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            currentdirectory = currentdirectory.Replace("\\", "/");
            currentdirectory = Uri.EscapeUriString(currentdirectory);
            string tovisit = "file:///" + currentdirectory + "/" + "toprint.html";
            string localPath = new Uri(tovisit).LocalPath;
            StreamWriter sw = new StreamWriter(localPath, false);
            sw.WriteLine("<body style=\"font-family:calibri\">");
            sw.WriteLine("<div style=\"width:21.59cm\">");
            sw.WriteLine("<strong>Driver log for" + currentLogDate.Value.Date.ToShortDateString() + "</strong>");
            sw.WriteLine("<TABLE border=1>");
            sw.WriteLine("<TR>");
            sw.WriteLine("<TD>");
            sw.WriteLine("Run Driver");
            sw.WriteLine("</TD>");
            sw.WriteLine("<TD>");
            sw.WriteLine("Customer Name");
            sw.WriteLine("</TD>");
            sw.WriteLine("<TD>");
            sw.WriteLine("Invoice");
            sw.WriteLine("</TD>");
            sw.WriteLine("</TR>");
            foreach (DataGridViewRow currentRow in viewDriverLogDataGrid.returnRows())
            {
                sw.WriteLine("<TR>");
                if (currentRow.Cells[1].Value != null)
                    sw.WriteLine("<TD>" + currentRow.Cells[1].Value.ToString() + "</TD>");
                if (currentRow.Cells[2].Value != null)
                    sw.WriteLine("<TD>" + currentRow.Cells[2].Value.ToString() + "</TD>");
                if (currentRow.Cells[3].Value != null)
                    sw.WriteLine("<TD>" + currentRow.Cells[3].Value.ToString() + "</TD>");
                sw.WriteLine("</TR>");
            }
            sw.WriteLine("</TABLE></b>");
            sw.Close();

            Console.WriteLine(tovisit);
            htmlParserBrowser.Navigate(tovisit);
            htmlParserBrowser.ShowPrintDialog();
            populateViewLogTable();
        }

        private void htmlParserBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser wb = (WebBrowser)sender;
            if (wb.ReadyState.Equals(WebBrowserReadyState.Complete))
                wb.ShowPrintDialog();
        }

        private void mainWindow_Resize(object sender, EventArgs e)
        {
            viewDriverLogDataGrid.Height = this.Height - 105;
        }

        /*private void viewDriverLogDataGrid_CellValidating(object sender, EventArgs e)
        {

        }*/

        private void updateInvoices_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            updateInvoices.Text = "Updating invoices...";
            _bw3 = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            _bw3.DoWork += bw3_DoWork;
            _bw3.RunWorkerCompleted += bw3_RunWorkerCompleted;

            _bw3.RunWorkerAsync(true);
        }

        void bw3_DoWork(object sender, DoWorkEventArgs e)
        {
            DateTime viewdate = currentLogDate.Value;
            try
            {
                //get invoices for current day
                MySQL MySQLHandle = new MySQL(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
                MySqlConnection sqlReader = MySQLHandle.Connect();
                string selectquery = "SELECT `invoice` FROM shipping_log WHERE `date_delivered` LIKE '" + currentLogDate.Value.ToString("yyyy-MM-dd") + "';";
                MySqlDataReader dataReader = MySQLHandle.Select(selectquery, sqlReader);
                List<int> invoices = new List<int>();
                for (int i = 0; dataReader.Read(); i++)
                {

                    invoices.Add(dataReader.GetInt32(0));
                }
                dataReader.Close();
                sqlReader.Close();
                rrsdatareader = new RRSDataReader(GlobalVar.sqlsettings.RRSHeaderFile, GlobalVar.sqlsettings.RRSLinesFile);
                rrsdatareader.ReadInvoices(viewdate);

                List<Invoice> filteredinvoices = rrsdatareader.FilterInvoices(viewdate, invoices);

                //List<LineItem> filteredlineitems = rrsdatareader.FilterLineItems(filteredinvoices);
                List<Invoice> newfilteredinvoices = new List<Invoice>(filteredinvoices);
                mysql_invoices = new MySQL_Invoices(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
                newfilteredinvoices = mysql_invoices.AddInvoices(newfilteredinvoices, false);
                mysql_invoices.UpdateInvoices(filteredinvoices, viewdate, true);
                dataReader.Close();
                sqlReader.Close();
                MySQLHandle.Disconnect();
                for (int i = 0; i < newfilteredinvoices.Count; i++)
                {
                    MySqlConnection sqlWriter = MySQLHandle.Connect();
                    String updatecmd = "UPDATE `shipping_log` SET customer_name='" + newfilteredinvoices[i].customername + "' WHERE `invoice`=" + newfilteredinvoices[i].number + ";";
                    MySQLHandle.Update(updatecmd, sqlWriter);
                    MySQLHandle.Disconnect();
                }
            }
            catch { }
        }

        void bw3_RunWorkerCompleted(object sender,
                           RunWorkerCompletedEventArgs e)
        {
            this.Enabled = true;
            updateInvoices.Text = "Update Invoices";
            populateViewLogTable();

        }

    }
}