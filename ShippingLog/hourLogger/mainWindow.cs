using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using MySql.Data.MySqlClient;
using hourLogger;
using System.Globalization;
using System.IO;


namespace ShippingLog
{
    public partial class mainWindow : Form
    {
        RRSDataReader rrsdatareader;
        static BackgroundWorker _bw3;
        private string currentAddDriver;
        MySQL_Invoices mysql_invoices;
        public mainWindow()
        {
            InitializeComponent();
            //populateTYRyan(); //This gets the drivers from the wrong place
            logDate_ValueChanged(this,null);
            populateAddDriverSelect();
        }

        private void populateAddDriverSelect()
        {
            selectDriverBox.Items.Clear();
            MySQL MySQLHandle = new MySQL(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
            MySqlConnection sqlReader = MySQLHandle.Connect();
            MySqlDataReader dataReader = MySQLHandle.Select("SELECT `DRV_NAME`, `DRV_NUMBER` FROM drivers;", sqlReader);
            while (dataReader.Read())
            {
                selectDriverBox.Items.Add(new Item(dataReader.GetString(0), dataReader.GetInt32(1)));
            }
            selectDriverBox.SelectedIndex = 0;
            sqlReader.Close();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            

        }

        private void topTableContainer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void topTableContainer_Paint(object sender, PaintEventArgs e)
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

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void addDriverSearchFilter_Click(object sender, EventArgs e)
        {
        }

        private void addDriverSearchFilter_Click_1(object sender, EventArgs e)
        {
            hourLogger.editHours editHoursInstance = new hourLogger.editHours();
            editHoursInstance.Show();
        }

        private void addDriverToDriverHourLog(string driverName)
        {
            
            DateTime date = logDate.Value;
            driverInstanceDriverHourLog newDriver = new driverInstanceDriverHourLog(driverName,date);
            driversContainer.Controls.Add(newDriver);
            newDriver.Show();
        }

        private void populateTYRyan()
        {
            
            THRyanFlowPanel.Controls.Clear();
            GregorianCalendar gc = new GregorianCalendar();
            int weekno = gc.GetWeekOfYear(logDate.Value, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);
            MySQL MySQLHandle = new MySQL(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
            MySqlConnection sqlReader = MySQLHandle.Connect();
            string stringdate = logDate.Value.Year.ToString() + weekno.ToString();
            MySqlDataReader dataReader = MySQLHandle.Select("SELECT * FROM `th_ryan_invoices` WHERE date = " + stringdate + ";", sqlReader);
            Console.WriteLine(weekno.ToString());
            for (int rowCounter = 0; dataReader.Read(); rowCounter++)
            {
                string id = dataReader.GetString(0);
                string billedHours = dataReader.GetString(1);
                string billedMiles = dataReader.GetString(2);
                string cost = dataReader.GetString(3);
                string review = dataReader.GetString(4);
                string driver = dataReader.GetString(5);
                //MySQLHandle.Insert("INSERT INTO `rrs`.`th_ryan_invoies` (`id`, `billed_hours`, `billed_miles`, `cost`, `review`, `driver`) VALUES (NULL, '', '', '', '', '');", sqlReader);
                addDriverToTHRyan(driver,logDate.Value);
            }
            dataReader.Close();
            addTHRyanDriver1 = new addTHRyanDriver();
            THRyanFlowPanel.Controls.Add(addTHRyanDriver1);
        }

        private void clearDrivers()
        {
            for (int i = 0; i < driversContainer.Controls.Count; i++)
            {
                driversContainer.Controls[i].Dispose();
            }
            driversContainer.Controls.Clear();
        }
        private void updateDriversDisplay(DateTime date)
        {
            try{

                for (int i = 0; i < driversContainer.Controls.Count; i++)
                {
                    driverInstanceDriverHourLog temp = (driverInstanceDriverHourLog)driversContainer.Controls[i];
                    temp.refreshDriverInfo(date);
                    
                }
            }catch{
            }
        }
        private void addDriverToTHRyan(string driverName,DateTime date)
        {
            WeeklyLog newDriver = new WeeklyLog(driverName,date);
            THRyanFlowPanel.Controls.Add(newDriver);
            newDriver.Show();
        }

        private void refreshData()
        {
            if (tabControl1.SelectedIndex == 0)
            {
                clearDrivers();
                string date = "" + logDate.Value.ToString("yyyy-MM-dd");
                MySQL MySQLHandle = new MySQL(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
                MySqlConnection sqlReader = MySQLHandle.Connect();
                
                MySqlDataReader dataReader = MySQLHandle.Select("SELECT `run_driver` FROM `shipping_log` WHERE `date_delivered` LIKE '" + date + "'", sqlReader);
                List<String> drivers = new List<String>();

                for (int rowCounter = 0; dataReader.Read(); rowCounter++)
                {
                    drivers.Add(dataReader[0].ToString());
                }
                drivers = drivers.Distinct().ToList();
                for (int i = 0; i < drivers.Count; i++)
                {
                    addDriverToDriverHourLog(drivers[i]);
                }
                dataReader.Close();
                sqlReader.Close();
                MySQLHandle.Disconnect();
            }
            if (tabControl1.SelectedIndex == 1)
            {
                populateTYRyan();
            }
        }

        private void logDate_ValueChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                int diff = logDate.Value.DayOfWeek - DayOfWeek.Sunday;
                if (diff < 0)
                {
                    diff += 7;
                }
                logDate.Value=logDate.Value.AddDays(-1 * diff);
            }
            refreshData();
            
        }

        private void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            int diff = logDate.Value.DayOfWeek - DayOfWeek.Sunday;
            if (diff < 0)
            {
                diff += 7;
            }
            logDate.Value = logDate.Value.AddDays(-1 * diff);
            refreshData();
        }

        private void driversContainer_Paint(object sender, PaintEventArgs e)
        {

        }

        private void addTHRyanDriver1_Load(object sender, EventArgs e)
        {
            
        }

        private void printLogButton_Click_1(object sender, EventArgs e)
        {
            StreamWriter sw=null;
            try
            {
                string currentdirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                currentdirectory = currentdirectory.Replace("\\", "/");
                currentdirectory = Uri.EscapeUriString(currentdirectory);
                string tovisit = "file:///" + currentdirectory + "/" + "toprint.html";
                string localPath = new Uri(tovisit).LocalPath;
                sw = new StreamWriter(localPath, false);
                sw.WriteLine("<body style=\"font-family:calibri\">");
                sw.WriteLine("<div style=\"width:21.59cm\">");
                sw.WriteLine("<strong style='font-size:24px'>Driver log for" + logDate.Value.Date.ToShortDateString() + "</strong><br>");
                MySQL MySQLHandle = new MySQL(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
                MySqlConnection sqlReader = MySQLHandle.Connect();
                foreach (driverInstanceDriverHourLog currentInstance in driversContainer.Controls)
                {
                        int hourrate = 0;
                        int milerate = 0;
                        try
                        {
                            string selectcmd="SELECT `DRV_HOUR_RATE`, `DRV_FUEL_SURCH` FROM drivers WHERE `DRV_NAME` LIKE '" + currentInstance.getDriverName() + "';";
                            MySqlDataReader dataReader = MySQLHandle.Select(selectcmd, sqlReader);
                            dataReader.Read();
                            if (dataReader.HasRows == true)
                            {
                                hourrate = Convert.ToInt32(dataReader[0].ToString());
                                milerate = Convert.ToInt32(dataReader[1].ToString());

                                
                            }
                            dataReader.Close();
                        }
                        catch { }
                    sw.WriteLine("<div style='font-size:18px; font-weight:bold'>" + currentInstance.getDriverName() + " - Hours: " + currentInstance.billedhoursTextbox.Text + "&nbsp;&nbsp;&nbsp;Miles: " + currentInstance.billedmilesTextbox.Text + "&nbsp;&nbsp;&nbsp;Gross on Truck: " + currentInstance.textGOT.Text + "&nbsp;&nbsp;&nbsp;Net on Truck: " + currentInstance.textNOT.Text);
                    
                    List<string> customers = new List<string>();
                    List<string> towrite = new List<string>();
                    towrite.Add("<table border=1'>");
                    towrite.Add("<tr><td style='font-weight:bold'>CUSTOMER NAME</td><td style='font-weight:bold'>INVOICE</td><td style='font-weight:bold'>LOCATION</td><td style='font-weight:bold'>GROSS ON TRUCK</td><td style='font-weight:bold'>COG ON TRUCK</td><td style='font-weight:bold'>NET ON TRUCK</td></tr>");
                    int grosstotal = 0;
                    foreach (DataGridViewRow currentRow in currentInstance.getDataGrid())
                    {
                        int invoicenumber = -1;
                        try
                        {
                            invoicenumber = Convert.ToInt32(currentRow.Cells[1].Value.ToString());
                        }
                        catch { }
                        
                        int salesamount=0;
                        int shipping=0;
                        int tax=0;
                        int total=0;
                        int cost=0;
                        int net = 0;
                        string stringsalesamount = "";
                        string stringshipping = "";
                        string stringtax = "";
                        string stringtotal = "";
                        string stringcost = "";
                        string stringnet = "";
                        try
                        {
                            MySqlDataReader dataReader2 = MySQLHandle.Select("SELECT `INV_SALES_AMOUNT`, `INV_SHIPPING`, `INV_TAX`,`INV_TOTAL`,`INV_COST` FROM invoice_data WHERE `INV_NUMBER`=" + invoicenumber+ ";", sqlReader);
                            dataReader2.Read();
                            if (dataReader2.HasRows)
                            {
                                salesamount = Convert.ToInt32(dataReader2[0].ToString());
                                grosstotal += salesamount;
                                shipping = Convert.ToInt32(dataReader2[1].ToString());
                                tax = Convert.ToInt32(dataReader2[2].ToString());
                                total = Convert.ToInt32(dataReader2[3].ToString());
                                cost = Convert.ToInt32(dataReader2[4].ToString());
                                
                                net = Convert.ToInt32(salesamount - cost);
                                stringsalesamount = DataFormat.FormatMoneyToString(salesamount);
                                stringshipping = DataFormat.FormatMoneyToString(shipping);
                                stringtax = DataFormat.FormatMoneyToString(tax);
                                stringtotal = DataFormat.FormatMoneyToString(total);
                                stringcost = DataFormat.FormatMoneyToString(cost);
                                stringnet = DataFormat.FormatMoneyToString(net);
                            }
                            dataReader2.Close();
                            
                        }
                        catch { }


                        customers.Add(currentRow.Cells[0].Value.ToString());
                        towrite.Add("<tr><td>" + currentRow.Cells[0].Value.ToString() + "</td><td>" + currentRow.Cells[1].Value.ToString() + "</td><td>" + currentRow.Cells[3].Value.ToString() + "</td><td>" + stringsalesamount + "</td><td>" + stringcost + "</td><td>" + stringnet + "</td></tr>");
                    }
                    customers=customers.Distinct().ToList();
                    int stopcount = customers.Count;
                    double hours = currentInstance.getHours();
                    double miles = currentInstance.getMiles();
                    double truckcost = hourrate * hours + milerate * miles;
                    double costperstop = (double)truckcost / (double)stopcount;
                    double shippercentsales = (double)truckcost / (double)grosstotal;
                    string stringtruckcost = DataFormat.FormatMoneyToString((int)truckcost);
                    string stringcostperstop = DataFormat.FormatMoneyToString((int)costperstop);
                    string stringshippercentsales = shippercentsales.ToString("P");
                    sw.WriteLine("<div style='font-size:18px; font-weight:bold'>Cost of Truck: " + stringtruckcost + "&nbsp;&nbsp;&nbsp;Cost per Stop: " + stringcostperstop + "&nbsp;&nbsp;&nbsp;Shipping as % of Sales: " + stringshippercentsales);
                    foreach (string output in towrite)
                    {
                        sw.WriteLine(output);
                    }

                    sw.WriteLine("</table><br><br>");
                    sw.WriteLine("</div>");
                }
                sqlReader.Close();
                MySQLHandle.Disconnect();
                sw.WriteLine("</div></body>");
                sw.Close();

                Console.WriteLine(tovisit);
                htmlParserBrowser.Navigate(tovisit);
                //htmlParserBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(htmlParserBrowser_DocumentCompleted);
                //htmlParserBrowser.ShowPrintDialog();
            }
            catch { if (sw != null) { sw.Close(); } }
        }

        private void htmlParserBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser wb = (WebBrowser)sender;
            if (wb.ReadyState.Equals(WebBrowserReadyState.Complete))
                wb.ShowPrintDialog();
        }

        private void mainWindow_Resize(object sender, EventArgs e)
        {
            tabControl1.Height = this.Height - 69;
        }

        private class Item
        {
            public string Name;
            public int Value;
            public Item(string name, int value)
            {
                Name = name; Value = value;
            }
            public override string ToString()
            {
                // Generates the text shown in the combo box
                return Name;
            }
        }

        private void updateAddDriver()
        {
            MySQL MySQLHandle = new MySQL(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
            MySqlConnection sqlReader = MySQLHandle.Connect();
            MySQLHandle.Update("UPDATE `rrs`.`drivers` SET `DRV_NAME` = '" + driverNameTextbox.Text + "', `DRV_HOUR_RATE` = '" + hourlyRateTextbox.Text + "', `DRV_FUEL_SURCH` = '" + mileageRateTextbox.Text + "' WHERE `drivers`.`DRV_NAME` ='" + currentAddDriver + "';", sqlReader);
            sqlReader.Close();
        }

        private void selectDriverBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentAddDriver = selectDriverBox.Text;
            driverNameTextbox.Text = selectDriverBox.Text;
            MySQL MySQLHandle = new MySQL(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
            MySqlConnection sqlReader = MySQLHandle.Connect();
            MySqlDataReader dataReader = MySQLHandle.Select("SELECT `DRV_HOUR_RATE`, `DRV_FUEL_SURCH` FROM drivers WHERE `DRV_NAME`='" + currentAddDriver + "';", sqlReader);
            dataReader.Read();
            hourlyRateTextbox.Text = dataReader.GetString(0);
            mileageRateTextbox.Text = dataReader.GetString(1);
            sqlReader.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            updateAddDriver();
            populateAddDriverSelect();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MySQL MySQLHandle = new MySQL(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
            MySqlConnection sqlReader = MySQLHandle.Connect();
            MySQLHandle.Insert("INSERT INTO `rrs`.`drivers` (`DRV_NUMBER`, `DRV_NAME`, `DRV_FLAT`, `DRV_TRAILER`, `DRV_HOUR_RATE`, `DRV_OVERTIME`, `DRV_FUEL_SURCH`, `DRV_MAINT_SURCH`, `DRV_NOTES`) VALUES (NULL, '" + driverNameTextbox.Text + "', '1', '0', '" + hourlyRateTextbox.Text + "', '', '" + mileageRateTextbox.Text + "', '0', '0');", sqlReader);
            sqlReader.Close();
            populateAddDriverSelect();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MySQL MySQLHandle = new MySQL(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
            MySqlConnection sqlReader = MySQLHandle.Connect();
            MySQLHandle.Delete("DELETE FROM `rrs`.`drivers` WHERE `drivers`.`DRV_NAME` = '" + selectDriverBox.Text + "';", sqlReader);
            sqlReader.Close();
            populateAddDriverSelect();
        }

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
            DateTime viewdate = logDate.Value;
            try
            {
                //get invoices for current day
                MySQL MySQLHandle = new MySQL(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
                MySqlConnection sqlReader = MySQLHandle.Connect();
                string selectquery = "SELECT `invoice` FROM shipping_log WHERE `date_delivered` LIKE '" + logDate.Value.ToString("yyyy-MM-dd") + "';";
                MySqlDataReader dataReader = MySQLHandle.Select(selectquery, sqlReader);
                List<int> invoices = new List<int>();
                for (int i = 0; dataReader.Read();i++ )
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

            }
            catch { }
        }

        void bw3_RunWorkerCompleted(object sender,
                           RunWorkerCompletedEventArgs e)
        {
            this.Enabled = true;
            updateInvoices.Text = "Update Invoices";
            refreshData();

        }

    }
}