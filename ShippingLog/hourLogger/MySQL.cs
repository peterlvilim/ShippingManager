using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using ShippingLog;
using System.IO;
namespace hourLogger
{
    public class Driver
    {

        public String name;
        public double hours;
        public double miles;
        public String reason;
        public DateTime date;
        public Driver()
        {
            this.name = "";
            this.hours = 0;
            this.miles = 0;
            this.reason = "";
            this.date = DateTime.Now;
        }
        public Driver(String name, double hours, double miles, String reason, DateTime date)
        {
            this.name = name;
            this.hours = hours;
            this.miles = miles;
            this.reason = reason;
            this.date = date;
        }
    }
    public class MySQL
    {
        string host;//localhost
        int port;//3306
        string database;//RRS
        string user;//root
        string password;//da39a3ee5e6b4b0d3255bfef95601890afd80709
        MySqlConnection connection;
        public MySQL(string host, int port, string database, string user, string password)
        {
            this.host = host;
            this.port = port;
            this.database = database;
            this.user = user;
            this.password = password;
            connection = null;
        }
        public MySqlConnection Connect()
        {
            try
            {
                string connStr = "Server=" + GlobalVar.sqlhost + ";Uid=" + GlobalVar.sqlusername + ";Pwd=;Database=" + GlobalVar.sqldatabase + ";";
                connection = new MySqlConnection(connStr);
                connection.Open();
                return connection;
            }
            catch
            {
                return null;
            }
        }
        public bool Disconnect()
        {
            try
            {
                if (connection != null)
                {
                    connection.Close();
                    connection = null;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public MySqlDataReader Select(string command, MySqlConnection connection)
        {

            MySqlCommand cmd = new MySqlCommand(command, connection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            return rdr;
        }
        public long Insert(string command, MySqlConnection connection)
        {
            try
            {

                MySqlCommand myCommand = new MySqlCommand(command);
                myCommand.Connection = connection;
                myCommand.ExecuteNonQuery();

                return myCommand.LastInsertedId;
            }
            catch
            {
                return (-1);
            }
        }
        public bool Update(string command, MySqlConnection connection)
        {

            try
            {
                MySqlCommand myCommand = new MySqlCommand(command);
                myCommand.Connection = connection;
                myCommand.ExecuteNonQuery();

                return true;
            }
            catch
            {
                return (false);
            }
        }
        public bool Delete(string command, MySqlConnection connection)
        {

            try
            {
                MySqlCommand myCommand = new MySqlCommand(command);
                myCommand.Connection = connection;
                myCommand.ExecuteNonQuery();

                return true;
            }
            catch
            {
                return (false);
            }
        }

        public bool addBlankShippingLogInvoice(DataGridViewRow rowToAdd, MySqlConnection connection)
        {
            try
            {
                MySqlCommand myCommand = new MySqlCommand("INSERT INTO `rrs`.`shipping_log` (`id`, `date_delivered`, `run_driver`, `customer_name`, `invoice`, `order`) VALUES (NULL, '', '', '', '', '');");
                myCommand.Connection = connection;
                myCommand.ExecuteNonQuery();
                rowToAdd.Cells[4].Value = myCommand.LastInsertedId.ToString();

                return true;
            }
            catch
            {
                return (false);
            }
        }

        public bool addShippingLogInvoice(DataGridViewRow rowToAdd, MySqlConnection connection)//out date
        {
            try
            {
                string dateDelivered = rowToAdd.Cells[0].FormattedValue.ToString() + "";
                string runDriver = rowToAdd.Cells[1].Value.ToString() + "";
                string customerName = rowToAdd.Cells[2].Value.ToString() + "";
                string invoice = rowToAdd.Cells[3].Value.ToString() + "";
                Debug.WriteLine("testtesttest");
                MySqlCommand myCommand = new MySqlCommand("INSERT INTO `rrs`.`shipping_log` (`id`, `date_delivered`, `run_driver`, `customer_name`, `invoice`, `order`) VALUES (NULL, '" + dateDelivered + "', '" + runDriver + "', '" + customerName + "', '" + customerName + "', '" + rowToAdd.Index.ToString() + "');");
                myCommand.Connection = connection;
                myCommand.ExecuteNonQuery();

                return true;
            }
            catch
            {
                return (false);
            }
        }

        public AutoCompleteStringCollection returnAutoComplete()
        {
            AutoCompleteStringCollection autoCompleteColection = new AutoCompleteStringCollection();
            MySqlDataReader dataReader = Select("SELECT `DRV_NAME` FROM drivers;", connection);
            while (dataReader.Read())
            {
                autoCompleteColection.Add(dataReader.GetString(0));
            }
            return autoCompleteColection;
        }

        public int updateShippingLogInvoice(DataGridViewRow rowToAdd,bool rowadded,bool dosearch)
        {
            string dateDelivered = "";
            string runDriver = "";
            string customerName = "";
            string invoice = "";
            string id = "";
            string orderNumber = "";
            if (rowToAdd.Cells[4].Value == null)
                addBlankShippingLogInvoice(rowToAdd, connection);
            if (rowToAdd.Cells[0].FormattedValue.ToString() != null)
                dateDelivered = rowToAdd.Cells[0].FormattedValue.ToString();
            if (rowToAdd.Cells[1].Value != null)
                runDriver = rowToAdd.Cells[1].Value.ToString();
            if (rowToAdd.Cells[2].Value != null)
            {
                customerName = rowToAdd.Cells[2].Value.ToString();
            }
      
                if (rowToAdd.Cells[3].Value != null&&dosearch==true)
                {
                    MySQL MySQLHandle = new MySQL(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
                    MySqlConnection sqlReader = MySQLHandle.Connect();
                    MySqlDataReader dataReader = MySQLHandle.Select("SELECT `INV_CUSTOMER_NAME` FROM invoice_data WHERE `INV_NUMBER`='" + rowToAdd.Cells[3].Value + "';", sqlReader);
                    if (dataReader.Read())
                    {
                        rowToAdd.Cells[2].Value = dataReader.GetString(0);
                        customerName = rowToAdd.Cells[2].Value.ToString();
                    }
                    dataReader.Close();
                }
                
            
            
            if (rowToAdd.Cells[3].Value != null)
                invoice = rowToAdd.Cells[3].Value.ToString();
            if (rowToAdd.Cells[4].Value != null)
                id = rowToAdd.Cells[4].Value.ToString();
            orderNumber = rowToAdd.Index.ToString();
            MySQL MySQLHandle2 = new MySQL(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
            MySqlConnection sqlWriter = MySQLHandle2.Connect();
            string insertquery = "INSERT INTO `shipping_log` (`id`, `date_delivered`, `run_driver`, `customer_name`, `invoice`, `order`) VALUES (NULL,'" + dateDelivered + "','" + runDriver + "','" + customerName + "','" + invoice + "'," + orderNumber + ");";
            if (rowadded == true)
            {
                
                long temp=Insert(insertquery, sqlWriter);
                id = temp.ToString();
            }
            Update("UPDATE `rrs`.`shipping_log` SET `date_delivered` = '" + dateDelivered + "', `run_driver` = '" + runDriver + "', `customer_name` = '" + customerName + "', `invoice` = '" + invoice + "', `order` = '" + orderNumber + "' WHERE `shipping_log`.`id` = " + id + ";", sqlWriter);
            
            return Convert.ToInt32(id);
        }

        public bool updateDriverLogOrder(DataGridViewRow rowToAdd, MySqlConnection connection)
        {
            MySqlCommand myCommand = new MySqlCommand("UPDATE `rrs`.`shipping_log` SET `order` = '" + rowToAdd.Index.ToString() + "' WHERE `shipping_log`.`id` = " + rowToAdd.Cells[2].Value.ToString() + ";");
            myCommand.Connection = connection;
            myCommand.ExecuteNonQuery();
            Console.WriteLine(rowToAdd.Index.ToString());
            return true;
        }



        //peter change
        /*public bool addDriverLogInvoice(DataGridViewRow rowToAdd, MySqlConnection connection)
        {
            try
            {
                string amount = rowToAdd.Cells[0].Value.ToString() + "";
                string runDriver = rowToAdd.Cells[1].Value.ToString() + "";
                string customerName = rowToAdd.Cells[2].Value.ToString() + "";
                string invoice = rowToAdd.Cells[3].Value.ToString() + "";
                string location = rowToAdd.Cells[4].Value.ToString() + "";
                MySqlCommand myCommand = new MySqlCommand("INSERT INTO `rrs`.`driver_log` (`id`, `amount`, `run_driver`, `customer_name`, `invoice`, `location`) VALUES (NULL, '" + amount + "', '" + runDriver + "', '" + customerName + "', '" + invoice + "', '" + location + "');");
                myCommand.Connection = connection;
                myCommand.ExecuteNonQuery();

                return true;
            }
            catch
            {
                return (false);
            }
        }

        public bool updateDriverLogInvoice(DataGridViewRow rowToAdd, MySqlConnection connection, string id)
        {
            try
            {
                string amount = rowToAdd.Cells[0].Value.ToString();
                string runDriver = rowToAdd.Cells[1].Value.ToString();
                string customerName = rowToAdd.Cells[2].Value.ToString();
                string invoice = rowToAdd.Cells[3].Value.ToString();
                string location = rowToAdd.Cells[4].Value.ToString();
                MySqlCommand myCommand = new MySqlCommand("UPDATE `rrs`.`driver_log` SET `amount` = '" + amount + "', `run_driver` = '" + runDriver + "', `customer_name` = '" + customerName + "', `invoice` = '" + invoice + "', `location` = '" + location + "' WHERE `driver_log`.`id` = " + id + ";");
                myCommand.Connection = connection;
                myCommand.ExecuteNonQuery();

                return true;
            }
            catch
            {
                return (false);
            }
        }*/

        public Driver getDriverHourLogger(Driver driver, DateTime date, MySqlConnection connection)
        {
            try
            {
                string query = "SELECT * FROM `rrs`.`driver_log` WHERE `date` LIKE '" + date.ToString("yyyy-MM-dd") + "' AND driver LIKE '" + driver.name + "';";
                MySqlDataReader dataReader = Select(query, connection);
                dataReader.Read();
                driver.date = DateTime.Parse(dataReader.GetString(1));
                driver.hours = Convert.ToDouble(dataReader.GetString(3));
                driver.miles = Convert.ToDouble(dataReader.GetString(4));
                driver.reason = dataReader.GetString(5);
                dataReader.Close();
                return (driver);
            }
            catch
            {
                return (driver);
            }
        }

        public bool addDriverHourLogger(Driver driver, MySqlConnection connection)
        {
            try
            {
                string datestring = "" + driver.date.ToString("yyyy-MM-dd");
                MySqlCommand myCommand = new MySqlCommand("INSERT INTO `rrs`.`driver_log` (`id`,`date`, `driver`, `hours`, `miles`, `reason`) VALUES (NULL,'" + datestring + "' , '" + driver.name.ToString() + "', " + driver.hours + ", " + driver.miles + ", '" + driver.reason + "');");
                myCommand.Connection = connection;
                myCommand.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return (false);
            }
        }
        public bool updateDriverHourLogger(Driver driver, MySqlConnection connection)
        {
            try
            {
                string datestring = "" + driver.date.ToString("yyyy-MM-dd");

                MySqlCommand myCommand = new MySqlCommand("UPDATE `rrs`.`driver_log` SET `hours`=" + driver.hours + ", `miles`=" + driver.miles + ", `reason`='" + driver.reason + "' WHERE `driver` LIKE '" + driver.name + "';");
                myCommand.Connection = connection;
                myCommand.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return (false);
            }
        }

        
    }


    public class MySQL_Invoices : MySQL
    {
        public MySQL_Invoices(string host, int port, string database, string user, string password)
            : base(host, port, database, user, password)
        {
        }

        public List<Invoice> GetInvoices(DateTime date)
        {
            StreamWriter sw = new StreamWriter("test.txt");
            sw.WriteLine("test");
            List<Invoice> invoices = new List<Invoice>();

            try
            {

                MySqlConnection theconnection = Connect();
                List<DriverInfo> drivers = new List<DriverInfo>();
                string selectquery = "SELECT * FROM INVOICE_DATA WHERE inv_due_date LIKE '%" + date.ToShortDateString() + "%' OR inv_due_date LIKE '%" + date.ToString("M/d/yy") + "%' OR inv_due_date LIKE '%" + date.ToString("M/d/yyyy") + "%' OR inv_due_date LIKE '%" + date.ToString("MM/d/yy") + "%';";
                MySqlDataReader rdr = Select(selectquery, theconnection);
                sw.WriteLine(selectquery);
                while (rdr.Read())
                {

                    Invoice toinsert = new Invoice((int)rdr[0], (int)rdr[1], (int)rdr[12], (int)rdr[13], (int)rdr[14], (int)rdr[15], DateTime.Parse((string)rdr[2]), (string)rdr[3], (string)rdr[4], (string)rdr[5], (string)rdr[6], (string)rdr[7], (string)rdr[8], (double)rdr[9], (double)rdr[10], DateTime.Parse((string)rdr[11]));
                    sw.WriteLine((string)rdr[3]);
                    invoices.Add(toinsert);
                }

            }
            catch { sw.WriteLine("test2"); }
            sw.WriteLine("test4");
            sw.Close();
            Disconnect();
            return (invoices);
        }
        public Invoice GetInvoice(int number)
        {

            try
            {

                MySqlConnection theconnection = Connect();
                List<DriverInfo> drivers = new List<DriverInfo>();
                MySqlDataReader rdr = Select("SELECT * FROM INVOICE_DATA WHERE inv_number=" + number + ";", theconnection);
                rdr.Read();
                Invoice toreturn = new Invoice((int)rdr[0], (int)rdr[1], (int)rdr[12], (int)rdr[13], (int)rdr[14], (int)rdr[15], DateTime.Parse((string)rdr[2]), (string)rdr[3], (string)rdr[4], (string)rdr[5], (string)rdr[6], (string)rdr[7], (string)rdr[8], (double)rdr[9], (double)rdr[10], DateTime.Parse((string)rdr[11]));
                Disconnect();

                return (toreturn);
            }
            catch
            {
                Disconnect();
                return (null);
            }

        }
        public void BlockInvoice(Invoice invoice)
        {
            try
            {
                MySqlConnection theconnection = Connect();
                string blockquery = "INSERT INTO invoice_block (`INV_NUMBER`,`INV_DUE_DATE`) VALUES(" + invoice.number + ",'" + invoice.due + "');";
                Insert(blockquery, theconnection);
                Disconnect();
            }
            catch { Disconnect(); }

        }
        public List<Invoice> GetBlocks(DateTime date)
        {
            try
            {
                List<Invoice> invoices = new List<Invoice>();
                MySqlConnection theconnection = Connect();
                string getblockquery = "SELECT * FROM invoice_block WHERE inv_due_date LIKE '%" + date.ToShortDateString() + "%' OR inv_due_date LIKE '%" + date.ToString("M/d/yy") + "%' OR inv_due_date LIKE '%" + date.ToString("M/d/yyyy") + "%' OR inv_due_date LIKE '%" + date.ToString("MM/d/yy") + "%';";
                MySqlDataReader rdr = Select(getblockquery, theconnection);
                while (rdr.Read())
                {
                    Invoice toinsert = new Invoice((int)rdr[0], 0, 0, 0, 0, 0, DateTime.Parse((string)rdr[1]), "", "", "", "", "", "", 0, 0, DateTime.Parse((string)rdr[1]));
                    invoices.Add(toinsert);
                }
                Disconnect();
                return (invoices);
            }
            catch
            {
                Disconnect();
                return null;
            }

        }
        public List<Invoice> AddInvoices(List<Invoice> invoices, bool safe)
        {
            try
            {
                MySqlConnection theconnection = Connect();
                //figure out if already in database
                if (safe == true)
                {
                    List<Invoice> totest = GetInvoices(invoices[0].due);

                    for (int j = 0; j < totest.Count; j++)
                    {
                        for (int i = 0; i < invoices.Count; i++)
                        {
                            if (totest[j].number == invoices[i].number)
                            {
                                invoices.RemoveAt(i);
                            }
                        }
                    }
                }
                for (int i = 0; i < invoices.Count; i++)
                {
                    //delete old one
                   //last resort fix
                    //
                    invoices[i].customername = invoices[i].customername.Replace("\'", "\\'");
                    invoices[i].customername = invoices[i].customername.Replace("\"", "\\\"");
                    string insertquery = "INSERT INTO invoice_data (`INV_NUMBER`, `INV_SALES_AMOUNT`, `INV_DUE_DATE`, `INV_CUSTOMER_NAME`, `INV_ADDR_LN1`,`INV_ADDR_LN2`, `INV_CITY`, `INV_STATE`, `INV_ZIP`, `INV_LATITUDE`, `INV_LONGITUDE`, `INV_DELIVERED_DATE`,`INV_SHIPPING`,`INV_TAX`,`INV_TOTAL`,`INV_COST`) VALUES (" + invoices[i].number + ", " + invoices[i].value + ",'" + invoices[i].due.ToShortDateString() + "','" + invoices[i].customername + "','" + invoices[i].addr1 + "','" + invoices[i].addr2 + "','" + invoices[i].city + "','" + invoices[i].state + "','" + invoices[i].zip + "'," + invoices[i].latitude + "," + invoices[i].longitude + ",'" + invoices[i].delivered.ToShortDateString() + "',"+invoices[i].shipping+","+invoices[i].tax+","+invoices[i].invtotal+","+invoices[i].invcost+");";
                    Insert(insertquery, theconnection);
                    

                }
                Disconnect();
                return invoices;

            }
            catch
            {
                Disconnect();
                return null;
            }

        }
        public bool DeleteInvoice(Invoice invoice)
        {
            try
            {
                MySqlConnection theconnection = Connect();
                string deletequery = "DELETE FROM invoice_data WHERE INV_NUMBER=" + invoice.number + ";";
                Delete(deletequery, theconnection);
                Disconnect();
                return (false);
            }
            catch
            {
                Disconnect();
                return (false);
            }
        }
        public bool UpdateInvoice(Invoice invoice, Invoice originalinvoice)
        {
            try
            {
                MySqlConnection theconnection = Connect();
                invoice.customername = invoice.customername.Replace("\'", "\\'");
                invoice.customername = invoice.customername.Replace("\"", "\\\"");
                string updatequery = "UPDATE invoice_data SET `INV_NUMBER`=" + invoice.number + ", `INV_SALES_AMOUNT`=" + invoice.value + ", `INV_DUE_DATE`='" + invoice.due.ToShortDateString() + "', `INV_CUSTOMER_NAME`='" + invoice.customername + "', `INV_ADDR_LN1`='" + invoice.addr1 + "',`INV_ADDR_LN2`='" + invoice.addr2 + "', `INV_CITY`='" + invoice.city + "', `INV_STATE`='" + invoice.state + "', `INV_ZIP`='" + invoice.zip + "', `INV_LATITUDE`=" + invoice.latitude + ", `INV_LONGITUDE`=" + invoice.longitude + ", `INV_SHIPPING`="+invoice.shipping +",`INV_TAX`="+invoice.tax+", `INV_TOTAL`="+invoice.invtotal+"`INV_COST`="+invoice.invcost+",`INV_DELIVERED_DATE`='"+ invoice.delivered.ToShortDateString() + "' WHERE `INV_NUMBER`=" + originalinvoice.number + ";";
                Update(updatequery, theconnection);
                //do line items
                updatequery = "UPDATE invoice_line_data SET `INL_INV_NUMBER`=" + invoice.number + " WHERE `INL_INV_NUMBER`=" + originalinvoice.number + ";";
                Update(updatequery, theconnection);
                Disconnect();
                return (true);
            }
            catch
            {
                Disconnect();
                return (false);
            }
        }
        public bool UpdateInvoices(List<Invoice> invoices, DateTime date, bool dodelete)
        {
            try
            {
               
                MySqlConnection theconnection = Connect();
                
                //update
                for (int j = 0; j < invoices.Count; j++)
                {
     

              
                            invoices[j].customername = invoices[j].customername.Replace("'", "");

                            string updatequery = "UPDATE invoice_data SET `INV_NUMBER`=" + invoices[j].number + ", `INV_SALES_AMOUNT`=" + invoices[j].value + ", `INV_DUE_DATE`='" + invoices[j].due.ToShortDateString() + "', `INV_CUSTOMER_NAME`='" + invoices[j].customername + "', `INV_ADDR_LN1`='" + invoices[j].addr1 + "',`INV_ADDR_LN2`='" + invoices[j].addr2 + "', `INV_CITY`='" + invoices[j].city + "', `INV_STATE`='" + invoices[j].state + "', `INV_ZIP`='" + invoices[j].zip + "', `INV_DELIVERED_DATE`='" + invoices[j].delivered.ToShortDateString() + "', `INV_LONGITUDE`=0,`INV_LATITUDE`=0 " + ", `INV_SHIPPING`=" + invoices[j].shipping + ",`INV_TAX`=" + invoices[j].tax + ", `INV_TOTAL`=" + invoices[j].invtotal + ", `INV_COST`=" + invoices[j].invcost + " WHERE `INV_NUMBER`=" + invoices[j].number + ";";
                            Update(updatequery, theconnection);
                            updatequery="UPDATE shipping_log SET `CUSTOMER_NAME`='"+invoices[j].customername+"' WHERE invoice="+invoices[j].number+";";
                            Update(updatequery, theconnection);

                }


                Disconnect();
                return (true);
            }
            catch
            {
                Disconnect();
                return (false);
            }
        }
    }
    public class MySQL_Settings : MySQL
    {
        public MySQL_Settings(string host, int port, string database, string user, string password)
            : base(host, port, database, user, password)
        { }
        public Settings GetSettings()
        {
            try
            {

                MySqlConnection theconnection = Connect();
                MySqlDataReader rdr = Select("SELECT * FROM settings;", theconnection);
                rdr.Read();
                Settings settings = new Settings((string)rdr[0], (float)rdr[1], (float)rdr[2], (string)rdr[3], (string)rdr[4], (string)rdr[5], (string)rdr[6]);

                Disconnect();
                return (settings);

            }
            catch { Disconnect(); return null; }
        }
        public bool UpdateSettings(Settings settings)
        {
            try
            {

                MySqlConnection theconnection = Connect();
                settings.RRSaddress = settings.RRSaddress.Replace("'", "");
                settings.RRSHeaderFile = settings.RRSHeaderFile.Replace("\\", "\\\\");
                settings.RRSLinesFile = settings.RRSLinesFile.Replace("\\", "\\\\");
                string updatequery = "UPDATE settings SET `RRSaddress`='" + settings.RRSaddress + "',`RRSlatitude`=" + settings.RRSlatitude + ",`RRSlongitude`=" + settings.RRSlongitude + ",`Mapquest`='" + settings.mapquestkey + "',`RRSLinesFile`='" + settings.RRSLinesFile + "',`RRSHeaderFile`='" + settings.RRSHeaderFile + "',`password`='" + settings.passwordhash + "';";
                Update(updatequery, theconnection);

                Disconnect();
                return true;

            }
            catch { Disconnect(); return false; }
        }

    }




}