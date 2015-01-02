using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.IO;
namespace CondensedShippingReport
{
    public class Shipping_Log_Route
    {
        public int ID;
        public string name;
        public Shipping_Log_Route(int ID,string name)
        {
            this.ID=ID;
            this.name=name;
        }
    }

    public class Driver
    {

        public string name;
        public double hours;
        public double miles;
        public String reason;
        public DateTime date;
        public int route;
        public Driver()
        {
            this.route = -1;
            this.name = "";
            this.hours = 0;
            this.miles = 0;
            this.reason = "";
            this.date = DateTime.Now;
        }
        public Driver(String name, double hours, double miles, String reason, DateTime date,int route=-1)
        {
            this.name = name;
            this.hours = hours;
            this.miles = miles;
            this.reason = reason;
            this.date = date;
            this.route = route;
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
        public List<Driver> getDriversHourLogger(DateTime date, MySqlConnection connection)
        {
            List<Driver> drivers = new List<Driver>();
            try
            {
                string query = "SELECT * FROM `rrs`.`driver_log` WHERE `date` LIKE '" + date.ToString("yyyy-MM-dd") + "';";
                MySqlDataReader dataReader = Select(query, connection);

                while (dataReader.Read())
                {
                    Driver driver=new Driver();
                    driver.name = dataReader.GetString(2);
                    driver.date = DateTime.Parse(dataReader.GetString(1));
                    driver.hours = Convert.ToDouble(dataReader.GetString(3));
                    driver.miles = Convert.ToDouble(dataReader.GetString(4));
                    driver.reason = dataReader.GetString(5);
                    driver.route = Convert.ToInt32(dataReader.GetString(6));
                    drivers.Add(driver);
                }
                dataReader.Close();
                return (drivers);
            }
            catch
            {
                return (drivers);
            }
        }

        public InvoiceCost GetInvoiceCost(InvoiceCost invoicecost, MySqlConnection connection)
        {
            //MySQL MySQLHandle = new MySQL(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
            //MySqlConnection sqlReader = MySQLHandle.Connect();
            try
            {
                string selectquery = "SELECT `INV_NUMBER`, `INV_SALES_AMOUNT`, `INV_SHIPPING`, `INV_TAX`,`INV_TOTAL`,`INV_COST` FROM invoice_data WHERE `INV_NUMBER` ="+invoicecost.invoicenumber+";";
                MySqlDataReader dataReader = Select(selectquery, connection);
                try
                {

                    if (dataReader.HasRows == true)
                    {
                        try
                        {
                            dataReader.Read();
                            int invoicenumber = Convert.ToInt32(dataReader[0].ToString());
                            int salesamount = Convert.ToInt32(dataReader[1].ToString());
                            //grosstotal += salesamount;
                            int shipping = Convert.ToInt32(dataReader[2].ToString());
                            int tax = Convert.ToInt32(dataReader[3].ToString());
                            int total = Convert.ToInt32(dataReader[4].ToString());
                            int cost = Convert.ToInt32(dataReader[5].ToString());

                            //net = Convert.ToInt32(salesamount - cost);
                            invoicecost = new InvoiceCost(invoicenumber, salesamount, shipping, tax, total, cost);
                            dataReader.Close();
                            return (invoicecost);
                        }
                        catch
                        {
                            dataReader.Close();
                            return invoicecost;

                        }
                    }
                    else {
                        dataReader.Close();
                        return invoicecost;
                    }
                }
                catch
                {
                    dataReader.Close();
                    return (invoicecost);
                }
            }
            catch
            {
                return (invoicecost);
            }
        }

        public InvoiceCost GetShippingLogEdit(InvoiceCost invoicecost, MySqlConnection connection)
        {
            string selectquery = "SELECT `gross`,`shipping`,`net` FROM `shipping_log_edit` WHERE `invoice`=" + invoicecost.invoicenumber.ToString() + ";";
            MySqlDataReader dataReader = Select(selectquery, connection);
            try
            {
                if (dataReader.HasRows == true)
                {
                    dataReader.Read();
                    //gross sales
                    if (Convert.ToInt32(dataReader[0].ToString()) >= 0)
                    {
                        invoicecost.salesamount = Convert.ToInt32(dataReader[0].ToString());
                    }
                    //shipping
                    if (Convert.ToInt32(dataReader[1].ToString()) >= 0)
                    {
                        invoicecost.shipping = Convert.ToInt32(dataReader[1].ToString());
                    }
                    //net sales
                    if (Convert.ToInt32(dataReader.GetInt32(2))>= 0)
                    {
                        invoicecost.cost = invoicecost.salesamount - Convert.ToInt32(dataReader.GetInt32(2));
                    }

                    

                }
                dataReader.Close();
                return invoicecost;
            }
            catch { dataReader.Close(); return invoicecost; }
        }


        public List<InvoiceCost> GetInvoiceCosts(DateTime date, MySqlConnection connection)
        {
            //MySQL MySQLHandle = new MySQL(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
            //MySqlConnection sqlReader = MySQLHandle.Connect();
            List<InvoiceCost> invoicecosts = new List<InvoiceCost>();
            try
            {
                string selectquery = "SELECT `INV_NUMBER`, `INV_SALES_AMOUNT`, `INV_SHIPPING`, `INV_TAX`,`INV_TOTAL`,`INV_COST` FROM invoice_data WHERE inv_due_date LIKE '%" + date.ToShortDateString() + "%' OR inv_due_date LIKE '%" + date.ToString("M/d/yy") + "%' OR inv_due_date LIKE '%" + date.ToString("M/d/yyyy") + "%' OR inv_due_date LIKE '%" + date.ToString("MM/d/yy") + "%';";
                MySqlDataReader dataReader = Select(selectquery, connection);


                while (dataReader.Read())
                {
                    int invoicenumber = Convert.ToInt32(dataReader[0].ToString());
                    int salesamount = Convert.ToInt32(dataReader[1].ToString());
                    //grosstotal += salesamount;
                    int shipping = Convert.ToInt32(dataReader[2].ToString());
                    int tax = Convert.ToInt32(dataReader[3].ToString());
                    int total = Convert.ToInt32(dataReader[4].ToString());
                    int cost = Convert.ToInt32(dataReader[5].ToString());

                    //net = Convert.ToInt32(salesamount - cost);
                    InvoiceCost invoice = new InvoiceCost(invoicenumber,salesamount, shipping, tax, total, cost);
                    invoicecosts.Add(invoice);
                }
                dataReader.Close();
                return (invoicecosts);
            }
            catch
            {
                return (invoicecosts);
            }
        }

        public List<Shipping_Log_Route> GetShippingLogRoutes(MySqlConnection connection)
        {
            List<Shipping_Log_Route> shippinglogroutes= new List<Shipping_Log_Route>();
            MySqlDataReader dataReader=null;
            try{
                string selectquery="SELECT * FROM `shipping_log_routes`;";
                
                dataReader=Select(selectquery,connection);
                while (dataReader.Read())
                {
                    Shipping_Log_Route shippinglogroute = new Shipping_Log_Route(Convert.ToInt32(dataReader[0].ToString()), dataReader[1].ToString());
                    shippinglogroutes.Add(shippinglogroute);
                }
                dataReader.Close();
                return shippinglogroutes;
            }catch{
                if(dataReader!=null)
                {
                dataReader.Close();
                return null;
                }
            }
            return null;
        }

        public void UpdateShippingLogRoute(Shipping_Log_Route route)
        {

        }

        public List<ShippingLog> GetShippingLog(DateTime date, MySqlConnection connection)
        {
            List < ShippingLog > shippinglogs = new List<ShippingLog>();
            try
            {
                string selectquery = "SELECT `ID`,`DATE_DELIVERED`,`RUN_DRIVER`,`INVOICE`,`ORDER`,`CUSTOMER_NAME` FROM `SHIPPING_LOG` WHERE `DATE_DELIVERED` LIKE '%" +date.ToString("yyyy-MM-dd")+"%';";
                MySqlDataReader dataReader=Select(selectquery,connection);
                try
                {
                    while (dataReader.Read())
                    {
                        int id = Convert.ToInt32(dataReader[0].ToString());

                        DateTime date_delivered = DateTime.Parse(dataReader[1].ToString());

                        string run_driver = dataReader[2].ToString();
                        int invoice = Convert.ToInt32(dataReader[3].ToString());
                        int order = Convert.ToInt32(dataReader[4].ToString());
                        string customer = dataReader[5].ToString();
                        ShippingLog shippinglog = new ShippingLog(id, date_delivered, run_driver, invoice, order,customer);
                        shippinglogs.Add(shippinglog);
                    }
                    dataReader.Close();
                }
                catch
                {
                    dataReader.Close();
                    return (shippinglogs);
                }
            }
            catch
            {
                return (shippinglogs);
            }
            
            return (shippinglogs);
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
        public bool updateDriverLogRoute(Driver driver, MySqlConnection connection)
        {
            try
            {
                //string datestring = "" + driver.date.ToString("yyyy-MM-dd");

                MySqlCommand myCommand = new MySqlCommand("UPDATE `rrs`.`driver_log` SET `route`=" + driver.route + " WHERE `driver` LIKE '" + driver.name + "' AND `date` LIKE '" + driver.date.ToString("yyyy-MM-dd") + "';");
                myCommand.Connection = connection;
                myCommand.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return (false);
            }
        }
        public bool updateDriverHourLogger(Driver driver, MySqlConnection connection)//BAD WHERE?
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
                    string insertquery = "INSERT INTO invoice_data (`INV_NUMBER`, `INV_SALES_AMOUNT`, `INV_DUE_DATE`, `INV_CUSTOMER_NAME`, `INV_ADDR_LN1`,`INV_ADDR_LN2`, `INV_CITY`, `INV_STATE`, `INV_ZIP`, `INV_LATITUDE`, `INV_LONGITUDE`, `INV_DELIVERED_DATE`,`INV_SHIPPING`,`INV_TAX`,`INV_TOTAL`,`INV_COST`) VALUES (" + invoices[i].number + ", " + invoices[i].value + ",'" + invoices[i].due.ToShortDateString() + "','" + invoices[i].customername + "','" + invoices[i].addr1 + "','" + invoices[i].addr2 + "','" + invoices[i].city + "','" + invoices[i].state + "','" + invoices[i].zip + "'," + invoices[i].latitude + "," + invoices[i].longitude + ",'" + invoices[i].delivered.ToShortDateString() + "'," + invoices[i].shipping + "," + invoices[i].tax + "," + invoices[i].invtotal + "," + invoices[i].invcost + ");";
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
                string updatequery = "UPDATE invoice_data SET `INV_NUMBER`=" + invoice.number + ", `INV_SALES_AMOUNT`=" + invoice.value + ", `INV_DUE_DATE`='" + invoice.due.ToShortDateString() + "', `INV_CUSTOMER_NAME`='" + invoice.customername + "', `INV_ADDR_LN1`='" + invoice.addr1 + "',`INV_ADDR_LN2`='" + invoice.addr2 + "', `INV_CITY`='" + invoice.city + "', `INV_STATE`='" + invoice.state + "', `INV_ZIP`='" + invoice.zip + "', `INV_LATITUDE`=" + invoice.latitude + ", `INV_LONGITUDE`=" + invoice.longitude + ", `INV_SHIPPING`=" + invoice.shipping + ",`INV_TAX`=" + invoice.tax + ", `INV_TOTAL`=" + invoice.invtotal + "`INV_COST`=" + invoice.invcost + ",`INV_DELIVERED_DATE`='" + invoice.delivered.ToShortDateString() + "' WHERE `INV_NUMBER`=" + originalinvoice.number + ";";
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
                    updatequery = "UPDATE shipping_log SET `CUSTOMER_NAME`='" + invoices[j].customername + "' WHERE invoice=" + invoices[j].number + ";";
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
