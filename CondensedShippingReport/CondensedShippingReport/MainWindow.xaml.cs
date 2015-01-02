using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using MySql.Data.MySqlClient;
using System.IO;
using System.Globalization;
using Microsoft.Win32;



namespace CondensedShippingReport
{

    public static class GlobalVar
    {
        public static string sqlhost;
        public static int sqlport;
        public static string sqldatabase;
        public static string sqlusername;
        public static bool authenticated;
        public static Settings sqlsettings;
    }
            public class DataGridTable : ObservableCollection<DataGridRow> {
                public DataGridTable()
                    : base()
                {
                    //Add(new DataGridRow("asdf", "ba"));
                    //Add(new DataGridRow("asdf", "ba"));
                }
    }
        public class DataGridRow
        {
            private int id;
            private DateTime date;
            private int grossSales;
            private int netSales;
            private int costOfTruck;
            private int numberOfStops;
            private int costPerStop;
            private double shippingPercent;
            private List<string> route;
            private string selectedroute;
            private string driver;
            public DataGridRow(int id,DateTime date,List<string> route,string selectedroute,string driver,int grossSales, int netSales, int costOfTruck, int numberOfStops, int costPerStop, double shippingPercent)
              {
                  this.id = id;
                  this.date = date;
                  this.route = route;
                  this.driver= driver;
                  this.grossSales = grossSales;
                  this.netSales = netSales;
                  this.costOfTruck = costOfTruck;
                  this.numberOfStops = numberOfStops;
                  this.costPerStop = costPerStop;
                  this.shippingPercent = shippingPercent;
                  this.selectedroute = selectedroute;
              }

            public string SelectedRoute
            {
                get { return selectedroute; }
                set { selectedroute = value; }
            }

            public int ID
            {
                get { return id; }
                set { id = value; }
            }
            public List<string> Route
            {
                get { return route; }
                set { route = value; }
            }
            public string Driver
            {
                get { return driver; }
                set { driver = value; }
            }
            public DateTime Date
            {
                get { return date; }
                set { date = value; }
            }

              public int GrossSales
              {
                  get { return grossSales; }
                  set { grossSales= value; }
              }

              public int NetSales
              {
                  get { return netSales; }
                  set { netSales = value; }
              }

              public int CostOfTruck
              {
                  get { return costOfTruck; }
                  set { costOfTruck = value; }
              }

              public int NumberOfStops
              {
                  get { return numberOfStops; }
                  set { numberOfStops = value; }
              }

              public int CostPerStop
              {
                  get { return costPerStop; }
                  set { costPerStop = value; }
              }

              public double ShippingPercent
              {
                  get { return shippingPercent; }
                  set { shippingPercent = value; }
              }


        }

        public class MoneyConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                int money = (int)value;
                return DataFormat.FormatMoneyToString(money);
            }
            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                /*string strValue = value as string;
                DateTime resultDateTime;
                if (DateTime.TryParse(strValue, out resultDateTime))
                {
                    return resultDateTime;
                }*/
                return DependencyProperty.UnsetValue;
            }
        }

        public class DateConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                DateTime date = (DateTime)value;
                return date.ToShortDateString();
            }
            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                /*string strValue = value as string;
                DateTime resultDateTime;
                if (DateTime.TryParse(strValue, out resultDateTime))
                {
                    return resultDateTime;
                }*/
                return DependencyProperty.UnsetValue;
            }
        }

        public class PercentConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                double percent= (double)value;
                return percent.ToString("#0.##%");
            }
            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                /*string strValue = value as string;
                DateTime resultDateTime;
                if (DateTime.TryParse(strValue, out resultDateTime))
                {
                    return resultDateTime;
                }*/
                return DependencyProperty.UnsetValue;
            }
        }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Shipping_Log_Route> shippinglogroutes;
        DateTime oldFromDate;
        DateTime oldToDate;
        public void UpdateTable(DateTime start,DateTime end, bool additems,bool prepend)
        {
            try
            {
                MySQL MySQLHandle = new MySQL(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
                MySQL MySQLHandle2 = new MySQL(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
                MySqlConnection sqlReader = MySQLHandle.Connect();
                MySqlConnection sqlReader2 = MySQLHandle.Connect();
                shippinglogroutes = MySQLHandle.GetShippingLogRoutes(sqlReader);

                int insertindex = 0;
                DateTime indexdate = start;
                if (additems == true)
                {
                    while (indexdate.CompareTo(end) <= 0)
                    {
                        List<Driver> drivers = MySQLHandle.getDriversHourLogger(indexdate, sqlReader);

                        //List<InvoiceCost> invoicecosts = MySQLHandle.GetInvoiceCosts(indexdate,sqlReader);
                        List<ShippingLog> shippinglogs = MySQLHandle.GetShippingLog(indexdate, sqlReader);
                        List<string> routestrings = new List<string>();
                        for (int i = 0; i < shippinglogroutes.Count; i++)
                        {
                            routestrings.Add(shippinglogroutes[i].name);
                        }

                        for (int i = 0; i < drivers.Count; i++)
                        {
                            HashSet<string> customers = new HashSet<string>();
                            int grosssales = 0;
                            int netsales = 0;
                            int shipping = 0;
                            for (int j = 0; j < shippinglogs.Count; j++)
                            {
                                if (shippinglogs[j].driver == drivers[i].name)
                                {
                                    //for (int k = 0; k < invoicecosts.Count; k++)
                                    //{
                                    customers.Add(shippinglogs[j].customer);
                                    InvoiceCost invoicecost = new InvoiceCost(shippinglogs[j].invoicenumber, 0, 0, 0, 0, 0);
                                    invoicecost = MySQLHandle.GetInvoiceCost(invoicecost, sqlReader);
                                    //if (shippinglogs[j].id == invoicecosts[k].invoicenumber)
                                    //{
                                    if (invoicecost != null)
                                    {
                                        invoicecost = MySQLHandle2.GetShippingLogEdit(invoicecost, sqlReader2);
                                        grosssales += invoicecost.salesamount;
                                        netsales = netsales + invoicecost.salesamount + invoicecost.shipping - invoicecost.cost;
                                        shipping += invoicecost.shipping;


                                        
                                    }
                                    //}
                                    //}
                                }
                            }
                            int numberofstops = 0;
                            foreach (string customer in customers)
                            {
                                numberofstops++;
                            }
                            //int numberofstops = 4;
                            double shippingpercentage = 0;
                            if (grosssales == 0)
                            {
                                shippingpercentage = 0;
                            }
                            else
                            {
                                shippingpercentage = (double)shipping / (double)grosssales;
                            }
                            int costperstop = 0;
                            if (numberofstops == 0)
                            {
                                costperstop = 0;
                            }
                            else
                            {
                                costperstop = (int)shipping / (int)numberofstops;
                            }
                            string selectedroute = "N/A";
                            for (int j = 0; j < shippinglogroutes.Count; j++)
                            {
                                if (shippinglogroutes[j].ID == drivers[i].route)
                                {
                                    selectedroute = routestrings[j];
                                }
                            }
                            //int selectedroute = drivers[i].route;
                            if (prepend == false)
                            {
                                dataGrid.Add(new DataGridRow(drivers[i].route, indexdate, routestrings, selectedroute, drivers[i].name, grosssales, netsales, shipping, numberofstops, costperstop, shippingpercentage));
                            }
                            else
                            {
                                dataGrid.Insert(insertindex, new DataGridRow(drivers[i].route, indexdate, routestrings, selectedroute, drivers[i].name, grosssales, netsales, shipping, numberofstops, costperstop, shippingpercentage));
                                insertindex++;
                            }
                        }
                        indexdate = indexdate.AddDays(1);
                    }
                }
                else
                {
                    while (indexdate.CompareTo(end) <= 0)
                    {
                        int datagridlength = dataGrid.Count;
                        for (int i = 0; i < datagridlength;i++ )
                        {
                            if (dataGrid[i].Date == indexdate)
                            {
                                dataGrid.RemoveAt(i);
                                datagridlength--;
                                i--;
                            }
                        }
                            indexdate = indexdate.AddDays(1);
                    }
                }
                int endtest = 1;
            }
            catch { }
        }
        public bool Connect()
        {
            try
            {
                //GlobalVar.authenticated = true;
                StreamReader sr = new StreamReader("RRS.conf");
                GlobalVar.sqlhost = sr.ReadLine();
                GlobalVar.sqlport = Int32.Parse(sr.ReadLine());
                GlobalVar.sqldatabase = sr.ReadLine();
                GlobalVar.sqlusername = sr.ReadLine();
                sr.Close();
            }
            catch { MessageBox.Show("Unable to load configuration file RRS.conf"); return false; }
            try
            {
                MySQL MySQLHandle = new MySQL(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
                MySqlConnection sqlReader = MySQLHandle.Connect();
                if (sqlReader == null)
                {
                    MessageBox.Show("Unable to connect to database"); return false;
                }
                MySQLHandle.Disconnect();
            }
            catch { MessageBox.Show("Unable to connect to database"); return false; }
            try
            {

                MySQL_Settings mysql_settings = new MySQL_Settings(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
                GlobalVar.sqlsettings = mysql_settings.GetSettings();
                return true;
            }
            catch
            {
                return false;
            }
        }

        DataGridTable dataGrid;    
        public MainWindow()
        {

            
            InitializeComponent();
            fromDate.SelectedDate = System.DateTime.Now;
            toDate.SelectedDate = System.DateTime.Now;
            oldFromDate = System.DateTime.Now;
            oldToDate = System.DateTime.Now;
            dataGrid = new DataGridTable();

            mainGrid.ItemsSource = dataGrid;

            if (Connect() == false)
            {
                Application.Current.Shutdown();
            }
            //UpdateTable();    

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int test = 1;
            
        }

        private void DataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            int test = 1;
            
        }

        private void exportCSV_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog savefiledialog = new SaveFileDialog();
            savefiledialog.ShowDialog();
            CsvExport cvsexport = new CsvExport();
            for (int i = 0; i < dataGrid.Count; i++)
            {
                cvsexport.AddRow();
                cvsexport["Date"] = dataGrid[i].Date;
                cvsexport["Route"] = dataGrid[i].SelectedRoute;
                cvsexport["Driver"] = dataGrid[i].Driver;
                cvsexport["GrossSales"] = DataFormat.FormatMoneyToString(dataGrid[i].GrossSales);
                cvsexport["NetSales"] = DataFormat.FormatMoneyToString( dataGrid[i].NetSales);
                cvsexport["CostofTruck"] = DataFormat.FormatMoneyToString( dataGrid[i].CostOfTruck);
                cvsexport["NumberofStops"] = dataGrid[i].NumberOfStops;
                cvsexport["CostperStop"] = DataFormat.FormatMoneyToString(dataGrid[i].CostPerStop);
                cvsexport["ShippingPercentofSales"] = dataGrid[i].ShippingPercent.ToString("#0.##");

                cvsexport.ExportToFile(savefiledialog.FileName);
            }
        }

        private void toDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DateTime newdate = (DateTime)toDate.SelectedDate;
                DateTime olddate = oldToDate;
                if (newdate.CompareTo(fromDate.SelectedDate) < 0)
                {
                    fromDate.SelectedDate = toDate.SelectedDate;
                    dataGrid.Clear();
                    UpdateTable((DateTime)fromDate.SelectedDate, (DateTime)toDate.SelectedDate, true,false);
                }
                else
                {
                    if (olddate.CompareTo(newdate) > 0)
                    {
                        UpdateTable((DateTime)newdate.AddDays(1), (DateTime)olddate, false,false);
                    }
                    else if (olddate.CompareTo(newdate) < 0)
                    {
                        UpdateTable((DateTime)olddate, (DateTime)newdate, true,false);
                    }
                }
                oldToDate = (DateTime)toDate.SelectedDate;
            }
            catch { }
        }

        private void fromDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DateTime newdate= (DateTime)fromDate.SelectedDate;
                DateTime olddate = oldFromDate;
                if (newdate.CompareTo(toDate.SelectedDate) > 0)
                {
                    toDate.SelectedDate = fromDate.SelectedDate;
                    dataGrid.Clear();
                    UpdateTable((DateTime)fromDate.SelectedDate, (DateTime)toDate.SelectedDate, true,false);
                }
                else
                {
                    if (olddate.CompareTo(newdate) > 0)
                    {
                        UpdateTable((DateTime)newdate, (DateTime)olddate.AddDays(-1),true,true);
                    }
                    else if(olddate.CompareTo(newdate) <0)
                    {
                        UpdateTable((DateTime)olddate, (DateTime)newdate.AddDays(-1), false,true);
                    }
                }
                oldFromDate = (DateTime)fromDate.SelectedDate;
            }
            catch { }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string routename = (string)e.AddedItems[0];//figure out if this should be an id or text //i'm guessing text but might lead to text probs

                int routeid = dataGrid[mainGrid.SelectedIndex].ID;
                string drivername = dataGrid[mainGrid.SelectedIndex].Driver;
                DateTime date = dataGrid[mainGrid.SelectedIndex].Date;

                MySQL MySQLHandle = new MySQL(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
                MySqlConnection sqlReader = MySQLHandle.Connect();
                shippinglogroutes = MySQLHandle.GetShippingLogRoutes(sqlReader);
                for (int i = 0; i < shippinglogroutes.Count; i++)
                {
                    if (shippinglogroutes[i].name == routename)
                    {
                        routeid = shippinglogroutes[i].ID;
                    }
                }
                Driver driver = new Driver();
                driver.name = drivername;
                driver.route = routeid;
                driver.date = date;
                MySQLHandle.updateDriverLogRoute(driver, sqlReader);
            }
            catch { }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 routeswindow = new Window1();
            routeswindow.Show();
        }
    }
}
