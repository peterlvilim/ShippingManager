using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Map;
using System.IO;
using System.Reflection;
using System.Threading;

using System.Runtime.InteropServices;

namespace RRS
{

    public partial class DeliverySchedule : Form
    {
        List<double> drivercost;
        List<double> milecost;
        List<double> maitenancecost;
        int[] totalmilesindividual;
        int[] totaltimeindividual;
        int selectionindex;
        int[] individualstopcounter;
        int []driverestimatedcost;
        bool mapdrawn;
        bool pointadded;
        DateTime viewdate;
        bool swapstate;
        RRSDataReader rrsdatareader;
        MySQL_Invoices mysql_invoices;
        MySQL_Drivers mysql_drivers;
        MySQL_LineItems mysql_lineitems;
        MySQL_Routes mysql_routes;
        MySQL_InvoiceRoute mysql_invoiceroutes;
        public List<Invoice> viewinvoices;
        List<DriverInfo> companydrivers;
        List<DriverInfo> scheduleddrivers;
        public List<LineItem> viewlineitems;
        List<RouteInfo> routes;
        static BackgroundWorker _bw1;
        static BackgroundWorker _bw2;
        static BackgroundWorker _bw3;
        static BackgroundWorker _bw4;
        int counter;
        List<bool> displayroute;
        int totalvalue;
        internal class DirectionArgs
        {
            public DirectionArgs()
            {
                latitude = new List<List<double>>();
                longitude = new List<List<double>>();
                address = new List<List<string>>();
                display = new List<bool>();
                directiondata = new List<DirectionData>();
                color = new List<int>();
            }
            internal List<int> color{get;set;} 
            internal List<DirectionData> directiondata { get; set; }
            internal List<List<string>> address { get; set; }
            internal List<bool> display { get; set; }
            internal List<List<double>> latitude { get; set; }
            internal List<List<double>> longitude { get; set; }
            internal bool optimize { get; set; }
            internal int counter { get; set; }
        }
        internal class ThreadArgs
        {
            internal string address { get; set; }
            internal int selection { get; set; }
            internal GeoData geodata { get; set; }
        }

        public DeliverySchedule()
        {
               InitializeComponent();
               
               
               
               this.MaximizeBox = false;
               
               mysql_invoices = new MySQL_Invoices(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
               mysql_drivers = new MySQL_Drivers(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
               mysql_lineitems = new MySQL_LineItems(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
               mysql_routes = new MySQL_Routes(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
               mysql_invoiceroutes = new MySQL_InvoiceRoute(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
               viewdate = DateTime.Now;
               scheduleddrivers = new List<DriverInfo>();
               counter = 0;

        
        }
        public void SetDate(DateTime date,int selection)
        {
            this.selectionindex = selection;
            viewdate=date;
        }
        private void Exit_Click(object sender, EventArgs e)
        {
            Application.OpenForms[0].Controls[0].Enabled = true;
            Application.OpenForms[0].Controls[1].Enabled = true;
            Application.OpenForms[0].Controls[2].Enabled = true;
            
            if (GlobalVar.authenticated == true)
            {
                Application.OpenForms[0].Controls[3].Enabled = true;
            }
            this.Close();
        }

        private void ConfigureRoute_Click(object sender, EventArgs e)
        {

            ConfigureTruckRoute configureroute =new ConfigureTruckRoute();
            configureroute.SetData(viewdate, scheduleddrivers[listScheduledDrivers.SelectedIndex]);
            configureroute.Show();
            this.Close();
        }

        private void AddInvoice_Click(object sender, EventArgs e)
        {
            AddInvoice addinvoice = new AddInvoice();
            addinvoice.SetDay(viewdate,true,null);
            addinvoice.Show();
            this.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            EditInvoice editinvoice = new EditInvoice();
            
            editinvoice.SetInvoice(viewinvoices[listInvoices.SelectedIndex],true,viewdate,null,listInvoices.SelectedIndex);
            editinvoice.Show();
            this.Close();
        }

        private void update_Click(object sender, EventArgs e)
        {
            try
            {

                DialogResult result=MessageBox.Show("New invoices will be added.  Update invoice data and delete invoices?","Update Invoices",MessageBoxButtons.YesNoCancel);
                if (result != DialogResult.Cancel)
                {
                    this.Enabled = false;
                    update.Text = "Updating invoices...";
                    _bw3 = new BackgroundWorker
                    {
                        WorkerReportsProgress = true,
                        WorkerSupportsCancellation = true
                    };
                    _bw3.DoWork += bw3_DoWork;
                    _bw3.RunWorkerCompleted += bw3_RunWorkerCompleted;
                    if (result == DialogResult.Yes)//do removes and updates
                    {
                        _bw3.RunWorkerAsync(true);
                    }
                    else
                    {
                        _bw3.RunWorkerAsync(false);
                    }

                }

            }
            catch { }
        }

        void bw3_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                rrsdatareader = new RRSDataReader(GlobalVar.sqlsettings.RRSHeaderFile, GlobalVar.sqlsettings.RRSLinesFile);
                rrsdatareader.ReadInvoices(viewdate);

                List<Invoice> filteredinvoices = rrsdatareader.FilterInvoices(viewdate, null);
                rrsdatareader.ReadLineItems();
                List<LineItem> filteredlineitems = rrsdatareader.FilterLineItems(filteredinvoices);
                List<Invoice> newfilteredinvoices = new List<Invoice>(filteredinvoices);
                newfilteredinvoices = mysql_invoices.AddInvoices(newfilteredinvoices, true);
                List<LineItem> templineitems = new List<LineItem>();
                for (int i = 0; i < newfilteredinvoices.Count; i++)
                {
                    for (int j = 0; j < filteredlineitems.Count; j++)
                    {
                        if (filteredlineitems[j].invoicenumber == newfilteredinvoices[i].number)
                        {
                            templineitems.Add(filteredlineitems[j]);
                        }

                    }
                }
                if((bool)e.Argument==true){
                mysql_invoices.UpdateInvoices(filteredinvoices, viewdate, true);
                }
                
                filteredlineitems = templineitems;
                mysql_lineitems.AddLineItems(filteredlineitems);
                if ((bool)e.Argument == true||listInvoices.Items.Count==0)
                {
                    //do lookup
                    for (int i = 0; i < filteredinvoices.Count; i++)
                    {
                        string address = filteredinvoices[i].addr1;
                        address = address + ", " + filteredinvoices[i].city;
                        address = address + ", " + filteredinvoices[i].state;
                        address = address + ",  " + filteredinvoices[i].zip;
                        Mapquest mapquest = new Mapquest(GlobalVar.sqlsettings.mapquestkey);

                        GeoData geodata = mapquest.GetLocation(address);
                        if (geodata != null)
                        {
                            filteredinvoices[i].latitude = geodata.latitude[0];
                            filteredinvoices[i].longitude = geodata.longitude[0];

                            mysql_invoices.UpdateInvoice(filteredinvoices[i], filteredinvoices[i]);
                        }
                        InvoiceRouteInfo invoicerouteinfo = mysql_invoiceroutes.GetRouteInfo(filteredinvoices[i].number);
                        if (invoicerouteinfo != null)
                        {
                            RouteInfo routeinfo = mysql_routes.GetRouteInfo(invoicerouteinfo);
                            if (routeinfo != null)
                            {
                                if (routeinfo.date.Day == viewdate.Day && routeinfo.date.Month == viewdate.Month && routeinfo.date.Year == viewdate.Year)
                                {
                                    
                                }
                                else
                                {
                                    mysql_invoiceroutes.DeleteRouteInfo(invoicerouteinfo, true);
                                }
                            }
                        }
                    }
                }

            }
            catch { }
        }

        void bw3_RunWorkerCompleted(object sender,
                           RunWorkerCompletedEventArgs e)
        {
            this.Enabled = true;
            update.Text = "Update Invoices";
            NextDayRefresh(0);
            
        }

        private void RefreshMap()
        {
            buttonPrint.Enabled = false;
            mapdrawn = false;
            pointadded = false;
            
            try{
                if (listScheduledDrivers.Items.Count <1)
                {
                    //return;
                    
                }

            string currentdirectory = Directory.GetCurrentDirectory();
            currentdirectory = currentdirectory.Replace("\\", "/");
            currentdirectory = Uri.EscapeUriString(currentdirectory);
            string tovisit = "file:///" + currentdirectory + "/" + "loading.html";
            if (this.IsDisposed == false)
            {
                webkitBrowser2.Navigate(tovisit);
            }
                webkitBrowser2.DocumentCompleted +=
            new WebBrowserDocumentCompletedEventHandler(webkitBrowser2_DocumentCompleted);

            _bw2 = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            _bw2.DoWork += bw2_DoWork;
            _bw2.RunWorkerCompleted += bw2_RunWorkerCompleted;
            DirectionArgs directionargs = new DirectionArgs();
            counter++;
            directionargs.counter = counter;
                                directionargs.address = new List<List<String>>();
                    directionargs.latitude=new List<List<double>>();
                    directionargs.longitude=new List<List<double>>();
                    directionargs.display = new List<bool>();
                    for (int i = 0; i < scheduleddrivers.Count; i++)
                    {
                        for (int j = 0; j < routes.Count; j++)
                        {
                            if (scheduleddrivers[i].number == routes[j].drivernumber)
                            {
                                if (routes[j].invoicerouteinfo.Count > 0)
                                {
                                    //if (displayroute[i] == true)
                                    //{
                                        directionargs.address.Add(new List<String>());
                                        directionargs.latitude.Add(new List<double>());
                                        directionargs.longitude.Add(new List<double>());
                                        directionargs.display.Add(false);
                                        directionargs.color.Add(0);
                                    //}
                                 }
                            }
                        }
                    }

            
            for (int i = 0; i < routes.Count; i++)
            {
                if (routes[i].invoicerouteinfo.Count > 0)
                {
                    List<string> addresses = new List<string>();
                    List<double> latitudes = new List<double>();
                    List<double> longitudes = new List<double>();
                    latitudes.Add(GlobalVar.sqlsettings.RRSlatitude);
                    longitudes.Add(GlobalVar.sqlsettings.RRSlongitude);
                    addresses.Add(GlobalVar.sqlsettings.RRSaddress);
                    int stop = 0;


                    for (int j = 0; j < routes[i].invoicerouteinfo.Count; j++)
                    {

                        for (int k = 0; k < viewinvoices.Count; k++)
                        {

                            if (routes[i].invoicerouteinfo[j].invoicenumber == viewinvoices[k].number && routes[i].invoicerouteinfo[j].stop == stop)
                            {
                                stop++;
                                string address = "";
                                if (viewinvoices[k].addr1 != "")
                                {
                                    address = address + viewinvoices[k].addr1;
                                }
                                else
                                {
                                    address = address + viewinvoices[k].addr2;
                                }
                                address = address + ", " + viewinvoices[k].city;
                                address = address + ", " + viewinvoices[k].state;
                                address = address + ",  " + viewinvoices[k].zip;
                                addresses.Add(address);
                                latitudes.Add(viewinvoices[k].latitude);
                                longitudes.Add(viewinvoices[k].longitude);
                                j = 0;
                                k = -1;
                            }

                        }

                    }
                    stop = 0;
                    latitudes.Add(GlobalVar.sqlsettings.RRSlatitude);
                    longitudes.Add(GlobalVar.sqlsettings.RRSlongitude);
                    addresses.Add(GlobalVar.sqlsettings.RRSaddress);
                    for (int j = 0; j < scheduleddrivers.Count; j++)
                    {

                            int index = -1;
                            int color = -1;
                            if (scheduleddrivers[j].number == routes[i].drivernumber)
                            {
                                for (int k = 0; k <= j; k++)
                                {
                                    int index2 = 0;

                                    for (int l = 0; l < routes.Count; l++)
                                    {
                                        if (routes[l].drivernumber == scheduleddrivers[k].number)
                                        {
                                            index2 = l;
                                        }
                                    }
                                    if (routes[index2].invoicerouteinfo.Count > 0)//change
                                    {
                                    index++;
                                    }
                                    if (displayroute[k] == true && routes[index2].invoicerouteinfo.Count > 0)//change
                                    {
                                        color++;
                                    }

                                }
                                if (index > -1)//&&displayroute[j]==true
                                {
                                    directionargs.address[index] = addresses;
                                    directionargs.latitude[index] = latitudes;
                                    directionargs.longitude[index] = longitudes;
                                    directionargs.color[index] = color;
                                    if (displayroute[j] == true)
                                    {
                                        directionargs.display[index] = true;
                                    }
                                    else
                                    {
                                        directionargs.display[index] = false;
                                    }
                                }
                            }
                        

                    }
                }
            }
            _bw2.RunWorkerAsync(directionargs);
            }catch{
                int test = 123123;
            }
        }
        void bw2_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Mapquest mapquest = new Mapquest(GlobalVar.sqlsettings.mapquestkey);
                Map.Directions directions = new Map.Directions(GlobalVar.sqlsettings.mapquestkey);

                DirectionArgs args = (DirectionArgs)e.Argument;
                for (int i = 0; i < args.latitude.Count; i++)
                {
                    for (int j = 0; j < args.latitude[i].Count; j++)
                    {
                        if (args.latitude[i][j] == 0 && args.longitude[i][j] == 0)
                        {
                            GeoData geodata = mapquest.GetLocation(args.address[i][j]);
                            if (geodata == null)
                            {
                                args.counter = -1;
                                break;
                            }
                            args.latitude[i][j] = geodata.latitude[0];
                            args.longitude[i][j] = geodata.longitude[0];

                                viewinvoices[i].latitude = (double)geodata.latitude[0];
                                viewinvoices[i].longitude = (double)geodata.longitude[0];
                                mysql_invoices.UpdateInvoice(viewinvoices[i], viewinvoices[i]);
                            
                        }
                    }
                }
                if (args.counter != -1)
                {
                    for (int i = 0; i < args.latitude.Count;i++ )
                    {
                        DirectionData directiondata = directions.GetDirections(args.latitude[i], args.longitude[i], args.optimize);
                        args.directiondata.Add(directiondata);
                    }
                }
                e.Result = args;
            }
            catch { }
        }
        void bw2_RunWorkerCompleted(object sender,
                           RunWorkerCompletedEventArgs e)
        {
            bool dontoutputflag=false;
            try
            {
                DirectionArgs args = (DirectionArgs)e.Result;
                if (args.counter == counter && args.counter != -1)
                {
                    MapWriter mapwriter = new MapWriter();
                    int numberdisplayed = 0;
                    for (int i = 0; i < args.display.Count;i++ )
                    {
                        if (args.display[i] == true)
                        {
                            numberdisplayed++;
                        }
                    }
                    
                    List<float>[] points_shape = new List<float>[numberdisplayed];
                    List<int> color = new List<int>();
                    List<double>[] points_latitude = new List<double>[numberdisplayed];
                    List<int>[] sequence = new List<int>[numberdisplayed];
                    List<double>[] points_longitude = new List<double>[numberdisplayed];
                    int index = 0;
                    
                        for (int i = 0; i < args.latitude.Count; i++)
                        {

                            if (args.display[i] == true)
                            {
                                color.Add(-1);
                                points_shape[index] = args.directiondata[i].routeshape;
                                points_latitude[index] = args.directiondata[i].latitude;
                                points_longitude[index] = args.directiondata[i].longitude;
                                sequence[index] = args.directiondata[i].locationSequence;
                                color[index]=args.color[i];
                                index++;
                            }

                        }
                    mapwriter.WriteMap(numberdisplayed, color, null, points_shape, points_latitude, points_longitude, sequence, 420, 668);
                    string currentdirectory = Directory.GetCurrentDirectory();
                    currentdirectory = currentdirectory.Replace("\\", "/");
                    currentdirectory = Uri.EscapeUriString(currentdirectory);
                    string tovisit = "file:///" + currentdirectory + "/" + "map.html";
                    if (this.IsDisposed == false)
                    {
                        webkitBrowser2.Navigate(tovisit);
                    }
                    int totalmiles = 0;
                    long totaltime = 0;
                    totaltimeindividual = new int[args.directiondata.Count];
                    totalmilesindividual = new int[args.directiondata.Count];
                    for (int l = 0; l < args.directiondata.Count; l++)
                    {
                        for (int i = 0; i < args.directiondata[l].distance.Count; i++)
                        {
                            totalmilesindividual[l] += (int)args.directiondata[l].distance[i];
                            totalmiles += (int)args.directiondata[l].distance[i];

                            totaltimeindividual[l] += (int)args.directiondata[l].time[i];
                            totaltime += (int)args.directiondata[l].time[i];
                        }
                    }
                    textTotalMiles.Text = totalmiles + " Mi";
                    //weekly miles
                    drivercost = new List<double>();
                    milecost = new List<double>();
                    maitenancecost = new List<double>();
                    double estimatedcost = 0 ;
                    driverestimatedcost=new int[routes.Count];
                    individualstopcounter = new int[routes.Count];
                    try
                    {
                        for (int l = 0; l < routes.Count; l++)
                        {
                            if (routes[l].invoicerouteinfo.Count > 0)
                            {
                                int index2 = 0;
                                for (int k = 0; k < scheduleddrivers.Count; k++)
                                {
                                    if (scheduleddrivers[k].number == routes[l].drivernumber)
                                    {
                                        index2 = k;
                                    }
                                }
                                DayOfWeek startOfWeek = DayOfWeek.Sunday;

                                int diff = viewdate.DayOfWeek - startOfWeek;
                                if (diff < 0)
                                {
                                    diff += 7;
                                }
                                DateTime aday = new DateTime();
                                aday = viewdate.AddDays(-1 * diff).Date;
                                int weeklymiles = 0;
                                for (int i = 0; i <= diff; i++)
                                {
                                    List<RouteInfo> routesforday = mysql_routes.GetRouteInfo(aday);
                                    for (int j = 0; j < routesforday.Count; j++)
                                    {
                                        if (routes[index2].drivernumber == routesforday[j].drivernumber)
                                        {
                                            weeklymiles += routesforday[j].miles;
                                        }
                                    }
                                    aday = aday.AddDays(1);
                                }
                                int stopcounter = 0;
                                for (int k = 0; k < args.directiondata[l].time.Count; k++)
                                {
                                    if (args.directiondata[l].time[k] != 0)
                                    {
                                        stopcounter++;
                                    }
                                }
                                stopcounter--;
                                totaltimeindividual[l] += stopcounter * 1800;
                                individualstopcounter[l] = stopcounter;
                                drivercost.Add((totaltimeindividual[l]/3600 )* scheduleddrivers[l].hour_rate);
                                milecost.Add(totalmilesindividual[l] * scheduleddrivers[l].fuel_surch);
                                maitenancecost.Add(0);
                                if (totalmilesindividual[l] > 100)
                                {
                                    maitenancecost[l] = (totalmilesindividual[l] - 100) * scheduleddrivers[l].maint_surch;
                                }
                                estimatedcost += drivercost[l] + milecost[l] + maitenancecost[l];
                                driverestimatedcost[l] = (int)drivercost[l] + (int)milecost[l] + (int)maitenancecost[l];
                            }
                        }
                    }
                    catch { dontoutputflag = true; }
                    try
                    {
                        if (dontoutputflag == false)
                        {
                            textEstimatedCost.Text = DataFormat.FormatMoneyToString((int)estimatedcost);
                            double percent = estimatedcost / (double)totalvalue;
                            textCostPercentage.Text = percent.ToString("0.00%");
                        }
                    }
                    catch { }
                }
                
            }

            catch
            {
                string currentdirectory = Directory.GetCurrentDirectory();
                currentdirectory = currentdirectory.Replace("\\", "/");
                currentdirectory = Uri.EscapeUriString(currentdirectory);
                string tovisit = "file:///" + currentdirectory + "/" + "error.html";
                try
                {
                    if (this.IsDisposed == false)
                    {
                        webkitBrowser2.Navigate(tovisit);
                    }
                    
                    
                }
                catch { }
            }
        }
        void webkitBrowser2_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            
            string url = webkitBrowser2.Url.ToString();
            string []totest=url.Split('/');
            if(totest[totest.Length-1]=="map.html")
            {
                mapdrawn = true;
            }
            if (swapstate == false&&mapdrawn==true)
            {
                buttonPrint.Enabled = true;
            }

        }

        private void RefreshInvoices(int selection)
        {
            viewinvoices=mysql_invoices.GetInvoices(viewdate);
            listInvoices.Items.Clear();
            
            if (viewinvoices.Count > 0)
            {
                
                for (int i = 0; i < viewinvoices.Count; i++)
                {
                    
                    string toinsert = "";
                    string shiptext = " - unassigned";
                    for (int l = 0; l < routes.Count; l++)
                    {
                        for (int j = 0; j < routes[l].invoicerouteinfo.Count; j++)
                        {
                            if (viewinvoices[i].number == routes[l].invoicerouteinfo[j].invoicenumber)
                            {
                                DriverInfo driver = mysql_drivers.GetDriverInfo(routes[l].drivernumber);
                                shiptext = " - "+driver.name;
                            }
                        }
                    }
                    
                    toinsert = viewinvoices[i].number+shiptext+" - "+ viewinvoices[i].city+", "+viewinvoices[i].state;
                    listInvoices.Items.Add(toinsert);
                }
                if (selection < listInvoices.Items.Count)
                {
                    listInvoices.SelectedIndex = selection;
                }
                else
                {
                    listInvoices.SelectedIndex = 0;
                }
            }
            RefreshValue();
            
        }
        private void RefreshLineItems()
        {
            viewlineitems = mysql_lineitems.GetLineItems(viewinvoices[listInvoices.SelectedIndex]);
            listItems.Items.Clear();
            try
            {
                for (int i = 0; i < viewlineitems.Count; i++)
                {
                    string toinsert = viewlineitems[i].description + " - " + DataFormat.FormatMoneyToString(viewlineitems[i].value);
                    listItems.Items.Add(toinsert);
                }
            }
            catch
            {
            }
        }

        private void RefreshValue()
        {
            textValueMain.Text = "";
            totalvalue = 0;
            for(int i=0;i<viewinvoices.Count;i++)
            {
                totalvalue=totalvalue+viewinvoices[i].value;
                textValueMain.Text=DataFormat.FormatMoneyToString(totalvalue);

                
            }
        }
        private void RefreshCompanyDrivers()
        {
            try
            {
                companydrivers = new List<DriverInfo>();
                scheduleddrivers.Clear();
                companydrivers = mysql_drivers.GetDrivers();//change removed routes local var
                //List<RouteInfo> routes = mysql_routes.GetRouteInfo(viewdate);
                listCompanyDrivers.Items.Clear();
                listScheduledDrivers.Items.Clear();
                int colorindex = 0;
                int index = 0;
                for (int i = 0; i < companydrivers.Count; i++)
                {
                    string toinsert = companydrivers[i].name;
                    if (companydrivers[i].flat == true)
                    {
                        toinsert = toinsert + " - Flat";
                    }
                    if (companydrivers[i].trailer == true)
                    {
                        toinsert = toinsert + " - Trailer";
                    }
                    bool flag = false;
                    for (int j = 0; j < routes.Count; j++)
                    {

                        if (routes[j].drivernumber == companydrivers[i].number)
                        {
                            MapWriter mapwriter = new MapWriter();
                            if (routes[j].invoicerouteinfo.Count == 0)
                            {
                                displayroute[index] = false;
                            }
                            if (displayroute[index] == true)
                            {
                                if (mapwriter.colors[colorindex][0] != '#')
                                {
                                    toinsert = toinsert + " - " + mapwriter.colors[colorindex];
                                }
                                else
                                {
                                    if (mapwriter.colors[colorindex] == "#000000")
                                    {
                                        toinsert = toinsert + " - " + "black";
                                    }
                                    if (mapwriter.colors[colorindex] == "#A0522D")
                                    {
                                        toinsert = toinsert + " - " + "brown";
                                    }
                                    if (mapwriter.colors[colorindex] == "#8B0000")
                                    {
                                        toinsert = toinsert + " - " + "red";
                                    }
                                    if (mapwriter.colors[colorindex] == "#00FF00")
                                    {
                                        toinsert = toinsert + " - " + "lime";
                                    }
                                    if (mapwriter.colors[colorindex] == "#191970")
                                    {
                                        toinsert = toinsert + " - " + "dark blue";
                                    }

                                }
                                colorindex++;
                            }
                            index++;
                            listScheduledDrivers.Items.Add(toinsert);
                            j = routes.Count;
                            flag = true;
                            scheduleddrivers.Add(companydrivers[i]);
                            companydrivers.RemoveAt(i);
                            i = i - 1;
                        }

                    }
                    if (flag == false)
                    {
                        listCompanyDrivers.Items.Add(toinsert);
                    }

                }

                listCompanyDrivers.SelectedIndex = 0;
            }
            catch { }
        }
        private void NextDayRefresh(int selection)
        {
            try
            {
                viewinvoices = new List<Invoice>();

                viewlineitems = new List<LineItem>();
                displayroute = new List<bool>();

                //set date
                routes = mysql_routes.GetRouteInfo(viewdate);
                for (int i = 0; i < routes.Count; i++)
                {
                    routes[i].invoicerouteinfo = mysql_invoiceroutes.GetRouteInfo(routes[i]);
                }

                this.Text = "Delivery Schedule - " + viewdate.Month + "/" + viewdate.Day + "/" + viewdate.Year + " - " + viewdate.DayOfWeek;
                Calendar.Value = viewdate;
                for (int i = 0; i < routes.Count; i++)
                {
                    if (i < 12)
                    {
                        displayroute.Add(true);
                    }
                    else
                    {
                        displayroute.Add(false);
                    }
                }
                //zero out areas
                textTotalMiles.Text = "";
                listInvoices.Items.Clear();
                textFuel.Text = "";
                textHourly.Text = "";
                textOvertime.Text = "";
                textMaitenance.Text = "";
                textInvoice.Text = "";
                textCusomter.Text = "";
                textStreet.Text = "";
                textCity.Text = "";
                textZip.Text = "";
                textState.Text = "";
                textValue.Text = "";
                textEstimatedCost.Text = "";
                textCostPercentage.Text = "";
                listItems.Items.Clear();
                listScheduledDrivers.Items.Clear();
                buttonEdit.Enabled = false;
                configureRoute.Enabled = false;
                addRoute.Enabled = false;
                deleteRoute.Enabled = false;
                checkDisplayRoute.Enabled = false;
                //buttonRemove.Enabled = false;
                //refresh data
                RefreshCompanyDrivers();
                RefreshInvoices(selection);
                RefreshValue();


                RefreshMap();
            }
            catch { }
        }
        private void RefreshfindAddress()
        {
            if (viewinvoices[listInvoices.SelectedIndex].longitude == 0 && viewinvoices[listInvoices.SelectedIndex].latitude == 0)
            {
                findAddress.Image = RRS.Properties.Resources.loading;
                
                
                _bw1 = new BackgroundWorker
                {
                    WorkerReportsProgress = true,
                    WorkerSupportsCancellation = true
                };
                _bw1.DoWork += bw1_DoWork;
                
                _bw1.RunWorkerCompleted += bw1_RunWorkerCompleted;
                string address = "";
                address = address + textStreet.Text;
                address = address + ", " + textCity.Text;
                address = address + ", " + textState.Text;
                address = address + ",  " + textZip.Text;
                
                List<object> compiled = new List<object>();
                compiled.Add(address);
                compiled.Add(listInvoices.SelectedIndex);
                ThreadArgs arguments = new ThreadArgs();
                arguments.address = address;
                arguments.selection = listInvoices.SelectedIndex;
                _bw1.RunWorkerAsync(arguments);
            }
            else
            {
                

                findAddress.Image = RRS.Properties.Resources.greencheck;
            }
        }
        void bw1_DoWork(object sender, DoWorkEventArgs e)
        {
            ThreadArgs temp = (ThreadArgs)e.Argument;

            Mapquest mapquest = new Mapquest(GlobalVar.sqlsettings.mapquestkey);
            GeoData currentgeodata = mapquest.GetLocation(temp.address);
            temp.geodata = currentgeodata;
            e.Result = temp;

        }
        void bw1_RunWorkerCompleted(object sender,
                                   RunWorkerCompletedEventArgs e)
        {
            try
            {
                
                    ThreadArgs results= (ThreadArgs)e.Result;
                    if (listInvoices.SelectedIndex == results.selection)
                    {
                        GeoData currentgeodata = results.geodata;
                        Invoice newinvoice = new Invoice(viewinvoices[listInvoices.SelectedIndex].number, viewinvoices[listInvoices.SelectedIndex].value, viewinvoices[listInvoices.SelectedIndex].due, viewinvoices[listInvoices.SelectedIndex].customername, currentgeodata.street[0], "", currentgeodata.city[0], currentgeodata.state[0], currentgeodata.zip[0], currentgeodata.longitude[0], currentgeodata.latitude[0], viewinvoices[listInvoices.SelectedIndex].delivered);

                        mysql_invoices.UpdateInvoice(newinvoice, viewinvoices[listInvoices.SelectedIndex]);
                        viewinvoices[listInvoices.SelectedIndex] = newinvoice;
                        findAddress.Image = RRS.Properties.Resources.greencheck;
                        if (pointadded == true)
                        {
                            webkitBrowser2.StringByEvaluatingJavaScriptFromString("map.removeShape(current);");
                        }
                        webkitBrowser2.StringByEvaluatingJavaScriptFromString("var current=new MQA.Poi( {lat:" + viewinvoices[listInvoices.SelectedIndex].latitude + ", lng:" + viewinvoices[listInvoices.SelectedIndex].longitude + "} );");
                        webkitBrowser2.StringByEvaluatingJavaScriptFromString("map.addShape(current);");
                        pointadded = true;
                        
                    }
                    else
                    {
                        
                    }

                
            }
            catch
            {
                findAddress.Image = RRS.Properties.Resources.redx;
            }
        }
        private void listInvoices_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillInvoiceInfo();
                RefreshLineItems();
                RefreshfindAddress();
                if (mapdrawn == true)
                {
                    if (viewinvoices[listInvoices.SelectedIndex].latitude != 0 && viewinvoices[listInvoices.SelectedIndex].longitude != 0)
                    {
                        if (pointadded == true)
                        {
                            webkitBrowser2.StringByEvaluatingJavaScriptFromString("map.removeShape(current);");
                        }
                        webkitBrowser2.StringByEvaluatingJavaScriptFromString("var current=new MQA.Poi( {lat:" + viewinvoices[listInvoices.SelectedIndex].latitude + ", lng:" + viewinvoices[listInvoices.SelectedIndex].longitude + "} );");
                        webkitBrowser2.StringByEvaluatingJavaScriptFromString("map.addShape(current);");
                        pointadded = true;

                    }
                }
            }
            catch { }
        }
        private void FillInvoiceInfo()
        {
            if (GlobalVar.authenticated == true)
            {
                buttonEdit.Enabled = true;
            }
            textInvoice.Text = viewinvoices[listInvoices.SelectedIndex].number.ToString();

            textCusomter.Text = viewinvoices[listInvoices.SelectedIndex].customername;
            textStreet.Text = viewinvoices[listInvoices.SelectedIndex].addr1;
            if (viewinvoices[listInvoices.SelectedIndex].addr1 == "")
            {
                textStreet.Text = viewinvoices[listInvoices.SelectedIndex].addr2;
            }
            textCity.Text = viewinvoices[listInvoices.SelectedIndex].city;
            textZip.Text = viewinvoices[listInvoices.SelectedIndex].zip;
            textState.Text = viewinvoices[listInvoices.SelectedIndex].state;
            textValue.Text = DataFormat.FormatMoneyToString(viewinvoices[listInvoices.SelectedIndex].value);
        }
        private void listCompanyDrivers_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                
                if (listCompanyDrivers.SelectedIndex!=-1)
                {
                    buttonSwap.Enabled = true;
                    deleteRoute.Enabled = false;
                    addRoute.Enabled = true;
                    checkDisplayRoute.Enabled = false;
                    listScheduledDrivers.SelectedIndex = -1;
                    textHourly.Text = DataFormat.FormatMoneyToString(companydrivers[listCompanyDrivers.SelectedIndex].hour_rate);
                    textOvertime.Text = DataFormat.FormatMoneyToString(companydrivers[listCompanyDrivers.SelectedIndex].overtime_rate);
                    textFuel.Text = DataFormat.FormatMoneyToString(companydrivers[listCompanyDrivers.SelectedIndex].fuel_surch);
                    textMaitenance.Text = DataFormat.FormatMoneyToString(companydrivers[listCompanyDrivers.SelectedIndex].maint_surch);
                }

            }
            catch { }
            if (GlobalVar.authenticated == false)
            {
                buttonSwap.Enabled = false;
                deleteRoute.Enabled = false;
                addRoute.Enabled = false;
            }
        }
        private void listScheduledDrivers_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
               
                configureRoute.Enabled = false;
                if (listScheduledDrivers.SelectedIndex != -1)
                {
                    if (swapstate == true)
                    {
                        swapstate = false;
                        buttonSwap.Text = "Swap Driver";
                        for (int i = 0; i < routes.Count; i++)
                        {
                            if (routes[i].drivernumber == scheduleddrivers[listScheduledDrivers.SelectedIndex].number)
                            {
                                routes[i].drivernumber = companydrivers[listCompanyDrivers.SelectedIndex].number;
                                mysql_routes.updateDriver(routes[i]);
                            }
                        }
                        Calendar.Enabled = true;
                        nextDay.Enabled = true;
                        addRoute.Enabled = true;
                        deleteRoute.Enabled = true;
                        listCompanyDrivers.Enabled = true;
                        button13.Enabled = true;
                        checkDisplayRoute.Enabled = true;
                        listInvoices.Enabled = true;
                        findAddress.Enabled = true;
                        buttonEdit.Enabled = true;
                        AddInvoice.Enabled = true;
                        update.Enabled = true;
                        buttonRemove.Enabled = true;
                        buttonPrint.Enabled = true;
                        NextDayRefresh(0);
                        return;
                    }

                    buttonSwap.Enabled = false;
                    addRoute.Enabled = false;
                    deleteRoute.Enabled = true;
                    configureRoute.Enabled = true;
                    textHourly.Text = DataFormat.FormatMoneyToString(scheduleddrivers[listScheduledDrivers.SelectedIndex].hour_rate);
                    textOvertime.Text = DataFormat.FormatMoneyToString(scheduleddrivers[listScheduledDrivers.SelectedIndex].overtime_rate);
                    textFuel.Text = DataFormat.FormatMoneyToString(scheduleddrivers[listScheduledDrivers.SelectedIndex].fuel_surch);
                    textMaitenance.Text = DataFormat.FormatMoneyToString(scheduleddrivers[listScheduledDrivers.SelectedIndex].maint_surch);

                    listCompanyDrivers.SelectedIndex = -1;
                    int count = 0;
                    for (int i = 0; i < displayroute.Count; i++)
                    {
                        if (displayroute[i] == true)
                        {
                            count++;
                        }
                    }
                    if (count >= 12 && displayroute[listScheduledDrivers.SelectedIndex] == false)
                    {
                        checkDisplayRoute.Enabled = false;
                    }
                    else
                    {
                        checkDisplayRoute.Enabled = true;
                    }
                    if (displayroute[listScheduledDrivers.SelectedIndex] == true)
                    {
                        checkDisplayRoute.Checked = true;
                    }
                    else
                    {
                        checkDisplayRoute.Checked = false;
                    }
                }
                else
                {
                    
                }

            }
            catch { }
            if (GlobalVar.authenticated == false)
            {
                buttonSwap.Enabled = false;
                deleteRoute.Enabled = false;
                addRoute.Enabled = false;
            }
        }
        private void nextDay_Click(object sender, EventArgs e)
        {
            if (_bw1 != null)
            {
                _bw1.Dispose();
            }
            if (_bw2 != null)
            {
                _bw2.Dispose();
            }
            viewinvoices.Clear();
            viewlineitems.Clear();
            
            viewdate = viewdate.AddDays(1);
            Calendar.Value = viewdate;
        }




        private void Calendar_ValueChanged(object sender, EventArgs e)
        {
            viewdate = Calendar.Value;
            if (this.selectionindex > 0)
            {
                NextDayRefresh(this.selectionindex);
            }
            else
            {
                this.selectionindex = 0;
                NextDayRefresh(this.selectionindex);
            }

        }
        private void Calendar_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void deleteRoute_Click(object sender, EventArgs e)
        {
            mysql_routes.DeleteRouteInfo(scheduleddrivers[listScheduledDrivers.SelectedIndex].number,viewdate);
            NextDayRefresh(0);
        }

        private void addRoute_Click(object sender, EventArgs e)
        {
            RouteInfo routeinfo = new RouteInfo(0, viewdate, companydrivers[listCompanyDrivers.SelectedIndex].number,0);
            mysql_routes.AddRouteInfo(routeinfo);
            NextDayRefresh(0);
        }

        private void findAddress_Click(object sender, EventArgs e)
        {
            FindAddress findaddress = new FindAddress();
            findaddress.SetInvoice(viewinvoices[listInvoices.SelectedIndex], 0, viewdate, null,true);
            findaddress.Show();
            this.Close();
        }

        private void DeliverySchedule_Load(object sender, EventArgs e)
        {
            Calendar.Value = viewdate;
            if (GlobalVar.authenticated == false)
            {
                buttonSwap.Enabled = false;
                addRoute.Enabled = false;
                deleteRoute.Enabled = false;
                button13.Enabled = false;
                AddInvoice.Enabled = false;
                update.Enabled = false;
                buttonEdit.Enabled = false;
                buttonRemove.Enabled = false;
                findAddress.Enabled = false;
            }
            //viewdate = DateTime.Parse("4/11/2012");
            //button13_Click(null, null);
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedinvoice=listInvoices.SelectedIndex;
                if (selectedinvoice == listInvoices.Items.Count - 1)
                {
                    selectedinvoice--;
                }
                if (selectedinvoice < 0)
                {
                    selectedinvoice = 0;
                }
                DialogResult result=MessageBox.Show("Prevent this invoice from being imported again for the current day?","", MessageBoxButtons.YesNo);
                
                mysql_invoices.DeleteInvoice(viewinvoices[listInvoices.SelectedIndex]);
                InvoiceRouteInfo todelete = null;
                for (int i = 0; i < routes.Count; i++)
                {
                    for (int j = 0; j < routes[i].invoicerouteinfo.Count; j++)
                    {
                        if (viewinvoices[listInvoices.SelectedIndex].number == routes[i].invoicerouteinfo[j].invoicenumber)
                        {
                            todelete = routes[i].invoicerouteinfo[j];
                        }
                    }
                }
                if (todelete != null)
                {
                    mysql_invoiceroutes.DeleteRouteInfo(todelete, true);
                }
                if (result == DialogResult.Yes)
                {
                    mysql_invoices.BlockInvoice(viewinvoices[listInvoices.SelectedIndex]);
                }
                NextDayRefresh(selectedinvoice);
            }
            catch { }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            button13.Text = "Optimize Daily Route...";
            this.Enabled = false;
            _bw4 = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            _bw4.RunWorkerCompleted += bw4_RunWorkerCompleted;
            _bw4.DoWork += bw4_DoWork;
            _bw4.RunWorkerAsync();

        }
        void bw4_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result=true;
            for (int i = 0; i < viewinvoices.Count; i++)
            {
                if (viewinvoices[i].latitude == 0 && viewinvoices[i].longitude == 0)
                {
                    Mapquest mapquest = new Mapquest(GlobalVar.sqlsettings.mapquestkey);
                    string address = "";
                    if (viewinvoices[i].addr1 != "")
                    {
                        address = address + viewinvoices[i].addr1;
                    }
                    else
                    {
                        address = address + viewinvoices[i].addr2;
                    }
                    address = address + ", " + viewinvoices[i].city;
                    address = address + ", " + viewinvoices[i].state;
                    address = address + ",  " + viewinvoices[i].zip;
                    GeoData currentgeodata = mapquest.GetLocation(address);
                    if (currentgeodata == null) 
                    {
                        e.Result = false;
                    }
                    else
                    {
                        viewinvoices[i].latitude = (double)currentgeodata.latitude[0];
                        viewinvoices[i].longitude = (double)currentgeodata.longitude[0];
                        mysql_invoices.UpdateInvoice(viewinvoices[i], viewinvoices[i]);
                    }
                }
            }
        }
        void bw4_RunWorkerCompleted(object sender,
                                   RunWorkerCompletedEventArgs e)
        {
            this.Enabled = true;
            button13.Text = "Optimize Daily Route";
            if ((bool)e.Result == true)
            {
                OptimizeWindow optimizewindow = new OptimizeWindow();
                optimizewindow.SetData(viewdate,scheduleddrivers);
                optimizewindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Mapquest was not able to locate all addresses.  Please make sure that all invoice delivery addresses are valid before running daily optimization.");
            }

        }
        private void checkDisplayRoute_Click(object sender, EventArgs e)
        {
            
            if (listScheduledDrivers.SelectedIndex != -1)
            {
                if (checkDisplayRoute.Checked == true)
                {
                    displayroute[listScheduledDrivers.SelectedIndex] = true;
                }
                if (checkDisplayRoute.Checked == false)
                {
                    displayroute[listScheduledDrivers.SelectedIndex] = false;
                }
                RefreshCompanyDrivers();
                RefreshMap();
                checkDisplayRoute.Enabled = false;
                checkDisplayRoute.Checked = false;
            }
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            if (mapdrawn == true)
            {
                int displaycount = 0;
                int[] totals = new int[8];
            StreamWriter sw = new StreamWriter("toprint.html", false);
            sw.WriteLine("<body style=\"font-family:calibri\">");
            sw.WriteLine("<div style=\"width:21.59cm\">");
            sw.WriteLine("<strong>Scheduled Deliveries for "+viewdate.Month+"/"+viewdate.Day+"/"+viewdate.Year+":<br></strong>");
            sw.WriteLine("<TABLE border=1>");
            sw.WriteLine("<TR>");
            sw.WriteLine("<TD>");
            sw.WriteLine("Driver Name");
            sw.WriteLine("</TD>");
            sw.WriteLine("<TD>");
            sw.WriteLine("Route Length");
            sw.WriteLine("</TD>");
            sw.WriteLine("<TD>");
            sw.WriteLine("Route Time");
            sw.WriteLine("</TD>");
            sw.WriteLine("<TD>");
            sw.WriteLine("Number of Stops");
            sw.WriteLine("</TD>");
            sw.WriteLine("<TD>");
            sw.WriteLine("Driver Cost");
            sw.WriteLine("</TD>");
            sw.WriteLine("<TD>");
            sw.WriteLine("Fuel Surcharge");
            sw.WriteLine("</TD>");
            sw.WriteLine("<TD>");
            sw.WriteLine("Maitenance Surcharge");
            sw.WriteLine("</TD>");
            sw.WriteLine("<TD>");
            sw.WriteLine("Total Cost");
            sw.WriteLine("</TD>");
            sw.WriteLine("<TD>");
            sw.WriteLine("Value of Invoices");
            sw.WriteLine("</TD>");
            sw.WriteLine("<TD>");
            sw.WriteLine("Cost Percentage");
            sw.WriteLine("</TD>");
            sw.WriteLine("</TR>");
            for (int i = 0; i < scheduleddrivers.Count; i++)
            {
                sw.WriteLine("<TR>");
                int index=0;
                for(int j=0;j<routes.Count;j++)
                {
                    if(scheduleddrivers[i].number==routes[j].drivernumber)
                    {
                        index=j;
                    }
                
                }
                int value=0;
                for(int j=0;j<routes[index].invoicerouteinfo.Count;j++)
                {
                    int index2=0;
                    for(int k=0;k<viewinvoices.Count;k++)
                    {
                        if(viewinvoices[k].number==routes[index].invoicerouteinfo[j].invoicenumber)
                        {index2 = k;}
                    }
                    value+=viewinvoices[index2].value;
                }

                
                sw.WriteLine("<TD>");
                sw.Write(scheduleddrivers[i].name);
                if (displayroute[i] == true)
                {
                    if (displaycount == 0)
                    {
                        sw.Write(" - Blue");
                    }
                    if (displaycount == 1)
                    {
                        sw.Write(" - Green");
                    }
                    if (displaycount == 2)
                    {
                        sw.Write(" - Orange");
                    }
                    if (displaycount == 3)
                    {
                        sw.Write(" - Purple");
                    }
                    if (displaycount == 4)
                    {
                        sw.Write(" - White");
                    }
                    if (displaycount == 5)
                    {
                        sw.Write(" - Yellow");
                    }
                    if (displaycount == 6)
                    {
                        sw.Write(" - Pink");
                    }
                    if (displaycount == 7)
                    {
                        sw.Write(" - Black");
                    }
                    if (displaycount == 8)
                    {
                        sw.Write(" - Brown");
                    }
                    if (displaycount == 9)
                    {
                        sw.Write(" - Red");
                    }
                    if (displaycount == 10)
                    {
                        sw.Write(" - Lime");
                    }
                    if (displaycount == 11)
                    {
                        sw.Write(" - Dark Blue");
                    }

                    displaycount++;
                }
                sw.WriteLine("</TD>");
                sw.WriteLine("<TD>");
                sw.WriteLine(totalmilesindividual[i]+" Mi");
                totals[0]+=totalmilesindividual[i];
                sw.WriteLine("<TD>");
                sw.WriteLine(((double)totaltimeindividual[i]/(double)3600).ToString("0.00"));
                totals[1] += totaltimeindividual[i];
                sw.WriteLine(" Hours");

                sw.WriteLine("</TD>");
                sw.WriteLine("</TD>");
                sw.WriteLine("<TD>");
                sw.WriteLine(individualstopcounter[i]);
                totals[2]+=individualstopcounter[i];
                sw.WriteLine("</TD>");
                sw.WriteLine("<TD>");
                sw.WriteLine(DataFormat.FormatMoneyToString((int)drivercost[i]));
                totals[3] += (int)drivercost[i];
                sw.WriteLine("</TD>");
                sw.WriteLine("<TD>");
                sw.WriteLine(DataFormat.FormatMoneyToString((int)milecost[i]));
                totals[4] += (int)milecost[i];
                sw.WriteLine("</TD>");
                sw.WriteLine("<TD>");
                sw.WriteLine(DataFormat.FormatMoneyToString((int)maitenancecost[i]));
                totals[5] += (int)maitenancecost[i];
                sw.WriteLine("</TD>");
                sw.WriteLine("<TD>");
                sw.WriteLine(DataFormat.FormatMoneyToString(driverestimatedcost[i]));
                totals[6]+=driverestimatedcost[i];
                sw.WriteLine("</TD>");
                sw.WriteLine("<TD>");
                sw.WriteLine(DataFormat.FormatMoneyToString(value));
                totals[7] += value;
                sw.WriteLine("</TD>");
                sw.WriteLine("<TD>");
                double percent = driverestimatedcost[i] / (double)value;
                sw.WriteLine(percent.ToString("0.00%"));
                sw.WriteLine("</TD>");
                sw.WriteLine("</TR>");
            }
                sw.Write("<b><TR>");
                sw.WriteLine("<TD><b>");
                sw.WriteLine("Totals");
                sw.WriteLine("</b></TD>");
                sw.WriteLine("<TD><b>");
                sw.WriteLine(totals[0] + " Mi");
                sw.WriteLine("</b></TD>");
                sw.WriteLine("<TD><b>");
                sw.WriteLine(((double)totals[1]/(double)3600).ToString("0.00"));
                sw.WriteLine(" Hours");
                sw.WriteLine("</b></TD>");
                sw.WriteLine("<TD><b>");
                sw.WriteLine(totals[2]);
                sw.WriteLine("</b></TD>");
                sw.WriteLine("<TD><b>");
                sw.WriteLine(DataFormat.FormatMoneyToString(totals[3]));
                sw.WriteLine("</b></TD>");
                sw.WriteLine("<TD><b>");
                sw.WriteLine(DataFormat.FormatMoneyToString(totals[4]));
                sw.WriteLine("</b></TD>");
                sw.WriteLine("<TD><b>");
                sw.WriteLine(DataFormat.FormatMoneyToString(totals[5]));
                sw.WriteLine("</b></TD>");
                sw.WriteLine("<TD><b>");
                sw.WriteLine(DataFormat.FormatMoneyToString(totals[6]));
                sw.WriteLine("</b></TD>");
                sw.WriteLine("<TD><b>");
                sw.Write(DataFormat.FormatMoneyToString(totals[7]));
                sw.WriteLine("</b></TD>");
                sw.WriteLine("<TD><b>");
                double percent2 = (double)totals[6] / (double)totals[7];
                sw.WriteLine(percent2.ToString("0.00%"));
                sw.WriteLine("</b></TD>");
                
                sw.WriteLine("</TR>");
                sw.WriteLine("</TABLE></b>");
                sw.WriteLine("Display Delivery Map: <input type=\"checkbox\" checked name=\"mapcheck\" onClick=\"toggle(this)\" /><br>");
                StreamReader sr = new StreamReader("map.html");

                String mapstring = sr.ReadToEnd();
                mapstring = mapstring.Replace("<div id='map' style='width:420px; height:668px;'></div>", "<table><tr><td><div id='map' style='float:left; width:420px; height:500px;'></div></td><td><div id='map2' style='float:left; width:420px; height:500px;'></div></tr></td></table>");
                sw.Write(mapstring);
                mapstring=mapstring.Replace("map.", "map2.");
                mapstring=mapstring.Replace("map2.bestFit()", "center=new MQA.LatLng(41.859334,-87.610474);map2.setCenter(center);map2.setZoomLevel(7);");
                mapstring = mapstring.Replace("window.map = new MQA.TileMap(document.getElementById('map')", "window.map2 = new MQA.TileMap(document.getElementById('map2')");
                //mapstring = mapstring.Replace("<div id='map' style='float:left; width:420px; height:600px;'></div>", "<div id='map2' style='float:left; width:420px; height:600px;'></div>");
                sw.WriteLine(mapstring);
                sw.WriteLine("<script>");
                
                
                sw.WriteLine("function toggle(switchElement) {");
                sw.WriteLine("if (switchElement.checked == true)");
                sw.WriteLine("{document.getElementById('map').style.visibility = 'visible';document.getElementById('map2').style.visibility = 'visible';}");
                sw.WriteLine("else");
                sw.WriteLine("{document.getElementById('map').style.visibility = 'hidden';document.getElementById('map2').style.visibility = 'hidden';}");
                sw.WriteLine("}");
                
                sw.WriteLine("</script>");
                sr.Close();
                sw.Close();
                Print printwindow = new Print();
                printwindow.SetDate(viewdate);
                printwindow.Show();
                this.Close();
        }
        }

        private void buttonSwap_Click(object sender, EventArgs e)
        {

            if (listCompanyDrivers.SelectedIndex != -1 && listCompanyDrivers.Items.Count > 0)
            {
                if (swapstate == true)
                {
                    swapstate = false;
                    buttonSwap.Text = "Swap Driver";
                    Calendar.Enabled = true;
                    nextDay.Enabled = true;
                    addRoute.Enabled = true;
                    
                    listCompanyDrivers.Enabled = true;
                    button13.Enabled = true;
                    checkDisplayRoute.Enabled = true;
                    listInvoices.Enabled = true;
                    findAddress.Enabled = true;
                    buttonEdit.Enabled = true;
                    AddInvoice.Enabled = true;
                    update.Enabled = true;
                    buttonRemove.Enabled = true;
                    buttonPrint.Enabled = true;
                }else{
                
                listScheduledDrivers.Enabled = true;
                swapstate = true;
                buttonSwap.Text = "Select scheduled driver...";
                Calendar.Enabled = false;
                nextDay.Enabled = false;
                addRoute.Enabled = false;
                
                listCompanyDrivers.Enabled = false;
                button13.Enabled = false;
                checkDisplayRoute.Enabled = false;
                listInvoices.Enabled = false;
                findAddress.Enabled = false;
                buttonEdit.Enabled = false;
                AddInvoice.Enabled = false;
                update.Enabled = false;
                buttonRemove.Enabled = false;
                buttonPrint.Enabled = false;
                }
            }
        }

        


    }
}
