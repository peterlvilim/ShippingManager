﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Map;
using System.IO;
namespace RRS
{
    public partial class ConfigureTruckRoute : Form
    {
        int totalvalue;
        bool mapdrawn;
        bool pointadded;
        DateTime viewdate;
        MySQL_Invoices mysql_invoices;
        MySQL_Drivers mysql_drivers;
        MySQL_LineItems mysql_lineitems;
        MySQL_Routes mysql_routes;
        MySQL_InvoiceRoute mysql_invoiceroutes;
        public List<Invoice> viewinvoices;
        public List<Invoice> scheduledinvoices;
        public List<int> scheduledinvoicesorder;
        public DriverInfo driver;
        public List<LineItem> viewlineitems;
        RouteInfo viewroute;
        List<RouteInfo> routes;
        int currentnumberofstops;
        static BackgroundWorker _bw1;
        static BackgroundWorker _bw2;
        static BackgroundWorker _bw3;
        int counter;
        int stopcounter;
        internal class DirectionArgs
        {
            public DirectionArgs(){
                latitude = new List<double>();
                longitude = new List<double>();
                address = new List<string>();
        }
            internal DirectionData directiondata { get; set; }
            internal List<string> address { get; set; }
            internal List<double> latitude { get; set; }
            internal List<double> longitude { get; set; }
            internal bool optimize { get; set; }
            internal int counter { get; set; }
        }
        internal class ThreadArgs
        {
            internal string address { get; set; }
            internal int selection { get; set; }
            internal int list { get; set; }
            internal GeoData geodata { get; set; }
        }
        public ConfigureTruckRoute()
        {
            this.TopLevel = true;
            
            this.MaximizeBox = false;
            
            mysql_invoices = new MySQL_Invoices(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
            mysql_drivers = new MySQL_Drivers(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
            mysql_lineitems = new MySQL_LineItems(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
            mysql_routes = new MySQL_Routes(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
            mysql_invoiceroutes = new MySQL_InvoiceRoute(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
            
            InitializeComponent();

        }
        public void SetData(DateTime date,DriverInfo driverinfo)
        {
            viewdate = date;
            driver = driverinfo;
            
        }
        private void RefreshData(bool listflag,int select)
        {

            routes = mysql_routes.GetRouteInfo(viewdate);
            for (int i = 0; i < routes.Count; i++)
            {
                routes[i].invoicerouteinfo = mysql_invoiceroutes.GetRouteInfo(routes[i]);
                if (driver.number == routes[i].drivernumber)
                { viewroute = routes[i]; }
            }
            viewinvoices = new List<Invoice>();
            viewlineitems = new List<LineItem>();
            
            //set date

            this.Text = "Configure Driver Route - "+driver.name +" - "+ viewdate.Month + "/" + viewdate.Day + "/" + viewdate.Year + " - " + viewdate.DayOfWeek;

            //zero out areas
            //add in the route time???
            currentnumberofstops = 0;
            textEstimatedCost.Text = "";
            textValueMain.Text = "";
            textTotalMiles.Text = "";
            textTotalTime.Text = "";
            textCostPercentage.Text = "";
            listRouteItems.Items.Clear();
            listAllItems.Items.Clear();
            textInvoice.Text = "";
            textCustomer.Text = "";
            textStreet.Text = "";
            textCity.Text = "";
            textZip.Text = "";
            textState.Text = "";
            textValue.Text = "";
            listLineItems.Items.Clear();
            counter = 0;
            buttonPrint.Enabled = false;
            RefreshInvoices(listflag,select);
            RefreshMap();

        }

        private void RefreshMap()
        {
            mapdrawn = false;
            pointadded = false;
            if (listRouteItems.Items.Count > 0)
            {
                string currentdirectory = Directory.GetCurrentDirectory();
                currentdirectory = currentdirectory.Replace("\\", "/");
                currentdirectory = Uri.EscapeUriString(currentdirectory);
                string tovisit = "file:///" + currentdirectory + "/" + "loading.html";
                if (this.IsDisposed == false)
                {
                    webkitBrowser.Navigate(tovisit);
                
                    webkitBrowser.DocumentCompleted +=
new WebBrowserDocumentCompletedEventHandler(webkitBrowser2_DocumentCompleted);
                }
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
                directionargs.optimize = false;
                //add RRS info
                directionargs.latitude.Add(GlobalVar.sqlsettings.RRSlatitude);
                directionargs.longitude.Add(GlobalVar.sqlsettings.RRSlongitude);
                directionargs.address.Add(GlobalVar.sqlsettings.RRSaddress);
                for (int i = 0; i < scheduledinvoices.Count;i++ )
                {
                    for (int j = 0; j < scheduledinvoicesorder.Count;j++ )
                    {
                        if (scheduledinvoicesorder[j] == i)
                        {
                            directionargs.latitude.Add(scheduledinvoices[j].latitude);
                            directionargs.longitude.Add(scheduledinvoices[j].longitude);
                                                string address = "";
                                                if (scheduledinvoices[j].addr1 != "")
                                                {
                                                    address = address + scheduledinvoices[j].addr1;
                                                }
                                                else
                                                {
                                                    address = address + scheduledinvoices[j].addr2;
                                                }
                    address = address + ", " + scheduledinvoices[j].city;
                    address = address + ", " + scheduledinvoices[j].state;
                    address = address + ",  " + scheduledinvoices[j].zip;
                    directionargs.address.Add(address);
                        }
                    }
                }
                directionargs.latitude.Add(GlobalVar.sqlsettings.RRSlatitude);
                directionargs.longitude.Add(GlobalVar.sqlsettings.RRSlongitude);
                directionargs.address.Add(GlobalVar.sqlsettings.RRSaddress);
                _bw2.RunWorkerAsync(directionargs);//temp
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
                    if (args.latitude[i] == 0 && args.longitude[i] == 0)
                    {
                        GeoData geodata = mapquest.GetLocation(args.address[i]);
                        if (geodata == null)
                        {
                            args.counter = -1;
                            break;
                        }
                        args.latitude[i] = geodata.latitude[0];
                        args.longitude[i] = geodata.longitude[0];
                    }
                }
                if (args.counter != -1)
                {
                    DirectionData directiondata = directions.GetDirections(args.latitude, args.longitude, args.optimize);
                    args.directiondata = directiondata;
                }
                e.Result = args;
            }
            catch { }
        }
        void bw2_RunWorkerCompleted(object sender,
                                   RunWorkerCompletedEventArgs e)
        {
            try
            {
                DirectionArgs args = (DirectionArgs)e.Result;
                if (args.counter == counter&&args.counter!=-1)
                {
                    MapWriter mapwriter = new MapWriter();
                    List<float>[] points_shape = new List<float>[1];
                    points_shape[0] = args.directiondata.routeshape;
                    List<double>[] points_latitude = new List<double>[1];
                    points_latitude[0] = args.directiondata.latitude;
                    List<double>[] points_longitude = new List<double>[1];
                    points_longitude[0] = args.directiondata.longitude;
                    List<int>[] sequence = new List<int>[1];
                    sequence[0] = args.directiondata.locationSequence;
                    mapwriter.WriteMap(1, null, null, points_shape, points_latitude, points_longitude, sequence, 420, 668);
                    string currentdirectory = Directory.GetCurrentDirectory();
                    currentdirectory = currentdirectory.Replace("\\", "/");
                    currentdirectory = Uri.EscapeUriString(currentdirectory);
                    string tovisit = "file:///" + currentdirectory + "/" + "map.html";
                    if (this.IsDisposed == false)
                    {
                        webkitBrowser.Navigate(tovisit);
                    }
                    //update miles
                    
                    int totalmiles=0;
                    for(int i=0;i<args.directiondata.distance.Count;i++)
                    {
                        totalmiles+=(int)args.directiondata.distance[i];
                    }
                    viewroute.miles=totalmiles;
                    mysql_routes.updateMileage(viewroute);
                    //weekly miles
                    DayOfWeek startOfWeek = DayOfWeek.Sunday;
                    
                    int diff = viewdate.DayOfWeek - startOfWeek;
                    if (diff < 0)
                    {
                        diff += 7;
                    }
                    DateTime aday = new DateTime();
                    aday=viewdate.AddDays(-1 * diff).Date;
                    int weeklymiles = 0;
                    for (int i = 0; i <= diff; i++)
                    {
                        List <RouteInfo> routesforday=mysql_routes.GetRouteInfo(aday);
                        for (int j = 0; j < routesforday.Count; j++)
                        {
                            if (routesforday[j].drivernumber == viewroute.drivernumber)
                            {
                                weeklymiles += routesforday[j].miles;
                            }
                        }
                            aday = aday.AddDays(1);
                    }

                    long totaltime = 0;
                    for(int i=0;i<args.directiondata.time.Count;i++)
                    {
                        totaltime+=(int)args.directiondata.time[i];
                    }
                    totaltime += stopcounter * 1800;
                    textTotalMiles.Text = totalmiles + " Mi";
                    textTotalTime.Text=DataFormat.FormatTimeToString(totaltime);
                    double drivercost=(totaltime/3600)*driver.hour_rate;
                    double milecost = totalmiles * driver.fuel_surch;
                    double maitenancecost=0;
                    if (totalmiles > 100)
                    {
                        maitenancecost = (totalmiles - 100) * driver.maint_surch;
                    }
                    double estimatedcost = drivercost + milecost + maitenancecost;
                    textEstimatedCost.Text = DataFormat.FormatMoneyToString((int)estimatedcost);
                    double percent=estimatedcost/(double)totalvalue;
                    textCostPercentage.Text = percent.ToString("0.00%");
                }
                else
                {
                    string currentdirectory = Directory.GetCurrentDirectory();
                    currentdirectory = currentdirectory.Replace("\\", "/");
                    currentdirectory = Uri.EscapeUriString(currentdirectory);
                    string tovisit = "file:///" + currentdirectory + "/" + "error.html";
                    if (this.IsDisposed == false)
                    {
                        webkitBrowser.Navigate(tovisit);
                    }
                        textTotalMiles.Text = "";
                    textTotalTime.Text = "";
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
                        webkitBrowser.Navigate(tovisit);
                    }
                }
                catch { }
            }
        }
        void webkitBrowser2_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

            string url = webkitBrowser.Url.ToString();
            string[] totest = url.Split('/');
            if (totest[totest.Length - 1] == "map.html")
            {
                mapdrawn = true;
                buttonPrint.Enabled = true;
            }
        }
        
        private void RefreshInvoices(bool listflag,int select)//true if left or false if right, select is the index to select
        {
            try
            {
                moveUp.Enabled = false;
                moveDown.Enabled = false;
                viewinvoices = mysql_invoices.GetInvoices(viewdate);
                List<InvoiceRouteInfo> invoicerouteinfo = mysql_invoiceroutes.GetRouteInfo(viewroute);
                currentnumberofstops = invoicerouteinfo.Count;
                scheduledinvoices = new List<Invoice>();
                scheduledinvoicesorder = new List<int>();
                listAllItems.Items.Clear();
                listRouteItems.Items.Clear();

                if (viewinvoices.Count > 0)
                {
                    for (int i = 0; i < viewinvoices.Count; i++)
                    {
                        bool flag = false;
                        string toinsert = "";
                        for (int j = 0; j < invoicerouteinfo.Count; j++)
                        {


                            if (invoicerouteinfo[j].invoicenumber == viewinvoices[i].number)
                            {

                                scheduledinvoicesorder.Add(invoicerouteinfo[j].stop);
                                scheduledinvoices.Add(viewinvoices[i]);
                                viewinvoices.RemoveAt(i);
                                i = i - 1;
                                flag = true;
                                j = invoicerouteinfo.Count;
                            }
                        }
                        for (int l = 0; l < routes.Count; l++)
                        {
                            for (int k = 0; k < routes[l].invoicerouteinfo.Count; k++)
                            {
                                if (flag == false)
                                {
                                    if (routes[l].invoicerouteinfo[k].invoicenumber == viewinvoices[i].number)
                                    {
                                        viewinvoices.RemoveAt(i);
                                        i = i - 1;
                                        flag = true;

                                        k = routes[l].invoicerouteinfo.Count;
                                        //l = routes.Count;

                                    }
                                }
                            }
                        }
                            if (flag == false)
                            {
                                toinsert = viewinvoices[i].number + " - " + viewinvoices[i].city + ", " + viewinvoices[i].state;
                                listAllItems.Items.Add(toinsert);
                            }
                    }
                    if (listflag == false)
                    {
                        try
                        {
                            listAllItems.SelectedIndex = select;
                        }
                        catch { }
                    }
                    if (listflag == true)
                    {
                        try
                        {
                            listRouteItems.SelectedIndex = select;
                        }
                        catch { }
                    }
                }
                RefreshValue();
                DoReorder(listflag,select);
            }
            catch { }
        }

        private void DoReorder(bool listflag, int select)
        {
            listRouteItems.Items.Clear();
            
            stopcounter = 1;
            for (int i = 0; i < scheduledinvoices.Count; i++)
            {
                for (int j = 0; j < scheduledinvoices.Count; j++)
                {
                    if (i == (scheduledinvoicesorder[j]))
                    {
                        int index=-1;
                        for(int k=0;k<scheduledinvoices.Count;k++)
                        {
                            if ((scheduledinvoicesorder[j] - 1) == scheduledinvoicesorder[k])
                            {
                                index=k;
                            }
                        }
                        if (index != -1)
                        {
                            if (scheduledinvoices[j].latitude == 0)
                            {
                                string address = scheduledinvoices[j].addr1;
                                address= address + ", " + scheduledinvoices[j].city;
                                address = address + ", " + scheduledinvoices[j].state;
                                address = address + ",  " + scheduledinvoices[j].zip;
                                Mapquest mapquest = new Mapquest(GlobalVar.sqlsettings.mapquestkey);
                                GeoData geodata = mapquest.GetLocation(address);
                                scheduledinvoices[j].latitude = geodata.latitude[0];
                                scheduledinvoices[j].longitude = geodata.longitude[0];
                            }
                            if (scheduledinvoices[index].latitude == scheduledinvoices[j].latitude && scheduledinvoices[index].latitude == scheduledinvoices[j].latitude)
                            {


                            }
                            else
                            {
                                stopcounter++;
                            }
                        }
                        else { counter++; }
                        
                        string toinsert = "#"+(stopcounter)+" - "+scheduledinvoices[j].number +" - "+ scheduledinvoices[j].city + ", " + scheduledinvoices[j].state +" - " + driver.name.ToString();
                        listRouteItems.Items.Insert(i,toinsert);
                    }
                }
            }
            if (listflag == true)
            {
                listRouteItems.SelectedIndex = select;
            }
        }

        private void RefreshValue()
        {
            textValueMain.Text = "";
            totalvalue = 0;
            for (int i = 0; i < scheduledinvoices.Count; i++)
            {
                totalvalue = totalvalue + scheduledinvoices[i].value;
                
                textValueMain.Text = DataFormat.FormatMoneyToString(totalvalue);
                
            }
            
        }
        private void Exit_Click(object sender, EventArgs e)
        {
            if (_bw2 != null)
            {
                _bw2.Dispose();
            }
            if (_bw1 != null)
            {
                _bw1.Dispose();
            }
            
            DeliverySchedule deliveryschedule=new DeliverySchedule();
            deliveryschedule.SetDate(viewdate,0);
            deliveryschedule.Show();
            this.Close();
            
        }

        private void EditInvoice_Click(object sender, EventArgs e)
        {
            EditInvoice editinvoice = new EditInvoice();
            if (listRouteItems.SelectedIndex != -1)
            {
                int index = 0;
                for (int i = 0; i < scheduledinvoicesorder.Count; i++)
                {
                    if (scheduledinvoicesorder[i] == listRouteItems.SelectedIndex)
                    {
                        index = i;
                    }
                }
                editinvoice.SetInvoice(scheduledinvoices[index],false,viewdate,driver,listRouteItems.SelectedIndex);
            }
            if (listAllItems.SelectedIndex != -1)
            {
                editinvoice.SetInvoice(viewinvoices[listAllItems.SelectedIndex],false,viewdate,driver,listAllItems.SelectedIndex);

            }
            editinvoice.Show();
            this.Close();
        }


        private void button10_Click(object sender, EventArgs e)
        {

        }
        private void ConfigureTruckRoute_Load(object sender, EventArgs e)
        {
            if (GlobalVar.authenticated == false)
            {
                removeInvoice.Enabled = false;
                addInvoice.Enabled = false;
                editInvoice.Enabled = false;
                optimizeRoute.Enabled = false;
                addItem.Enabled = false;
                removeItem.Enabled = false;
            }
            RefreshData(false,0);
        }

        private void addItem_Click(object sender, EventArgs e)
        {

            
            InvoiceRouteInfo routeinfo = new InvoiceRouteInfo(viewinvoices[listAllItems.SelectedIndex].number, viewroute.number, currentnumberofstops);
            mysql_invoiceroutes.AddRouteInfo(routeinfo);
            for (int i = 0; i < viewinvoices.Count; i++)
            {
                if (viewinvoices[i].addr1 == viewinvoices[listAllItems.SelectedIndex].addr1 && viewinvoices[i].city == viewinvoices[listAllItems.SelectedIndex].city&&routeinfo.invoicenumber!=viewinvoices[i].number)
                {
                    currentnumberofstops++;
                    InvoiceRouteInfo routeinfo2 = new InvoiceRouteInfo(viewinvoices[i].number, viewroute.number, currentnumberofstops);
                    mysql_invoiceroutes.AddRouteInfo(routeinfo2);
                }

            }
                RefreshData(true, 0);
        }

        private void listRouteItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listRouteItems.SelectedIndex != -1)
                {
                    addItem.Enabled = false;
                    removeItem.Enabled = true;
                    int index = 0;
                    for (int i = 0; i < scheduledinvoicesorder.Count; i++)
                    {
                        if (scheduledinvoicesorder[i] == listRouteItems.SelectedIndex)
                        {
                            index = i;
                        }
                    }
                    listAllItems.SelectedIndex = -1;
                    textInvoice.Text = scheduledinvoices[index].number.ToString();
                    textCustomer.Text = scheduledinvoices[index].customername.ToString();
                    textStreet.Text = scheduledinvoices[index].addr1.ToString();
                    if (scheduledinvoices[index].addr1 == "")
                    {
                        textStreet.Text = scheduledinvoices[index].addr2;
                    }
                    textCity.Text = scheduledinvoices[index].city.ToString();
                    textState.Text = scheduledinvoices[index].state.ToString();
                    textZip.Text = scheduledinvoices[index].zip.ToString();
                    textValue.Text = DataFormat.FormatMoneyToString(scheduledinvoices[index].value);
                    RefreshfindAddress();
                    if (listRouteItems.SelectedIndex == 0&&listRouteItems.Items.Count>1)
                    {
                        moveDown.Enabled = true;
                        moveUp.Enabled= false;
                    }
                    else if (listRouteItems.SelectedIndex == scheduledinvoices.Count - 1 && listRouteItems.Items.Count > 1)
                    {
                        moveUp.Enabled = true;
                        moveDown.Enabled = false;
                    }
                    else if(listRouteItems.Items.Count>1)
                    {
                        moveUp.Enabled = true;
                        moveDown.Enabled = true;
                    }
                }else{
                moveUp.Enabled=false;
                moveDown.Enabled=false;
                }
                if (mapdrawn == true)
                {
                    int index=0;
                    for (int i = 0; i < scheduledinvoices.Count; i++)
                    {
                        if (scheduledinvoicesorder[i] == listRouteItems.SelectedIndex)
                        {
                            index = i;
                        }
                    }
                        if (viewinvoices[index].latitude != 0 && viewinvoices[index].longitude != 0)
                        {
                            if (pointadded == true)
                            {
                                webkitBrowser.StringByEvaluatingJavaScriptFromString("map.removeShape(current);");
                            }
                            webkitBrowser.StringByEvaluatingJavaScriptFromString("var current=new MQA.Poi( {lat:" + scheduledinvoices[index].latitude + ", lng:" + scheduledinvoices[index].longitude + "} );");
                            webkitBrowser.StringByEvaluatingJavaScriptFromString("map.addShape(current);");
                            pointadded = true;

                        }
                }
            }
            catch { }
            if (GlobalVar.authenticated == false)
            {
                moveUp.Enabled = false;
                moveDown.Enabled = false;
                addItem.Enabled = false;
                removeItem.Enabled = false;

            }
        }

        private void listAllItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listAllItems.SelectedIndex != -1)
                {
                    addItem.Enabled = true;
                    removeItem.Enabled = false;

                    listRouteItems.SelectedIndex = -1;
                    textInvoice.Text = viewinvoices[listAllItems.SelectedIndex].number.ToString();
                    textCustomer.Text = viewinvoices[listAllItems.SelectedIndex].customername.ToString();
                    textStreet.Text = viewinvoices[listAllItems.SelectedIndex].addr1.ToString();
                    if (viewinvoices[listAllItems.SelectedIndex].addr1 == "")
                    {
                        textStreet.Text = viewinvoices[listAllItems.SelectedIndex].addr2;
                    }
                    textCity.Text = viewinvoices[listAllItems.SelectedIndex].city.ToString();
                    textState.Text = viewinvoices[listAllItems.SelectedIndex].state.ToString();
                    textZip.Text = viewinvoices[listAllItems.SelectedIndex].zip.ToString();
                    textValue.Text = DataFormat.FormatMoneyToString(viewinvoices[listAllItems.SelectedIndex].value);
                    RefreshfindAddress();
                    if (mapdrawn == true)
                    {
                        if (viewinvoices[listAllItems.SelectedIndex].latitude != 0 && viewinvoices[listAllItems.SelectedIndex].longitude != 0)
                        {
                            if (pointadded == true)
                            {
                                webkitBrowser.StringByEvaluatingJavaScriptFromString("map.removeShape(current);");
                            }
                            webkitBrowser.StringByEvaluatingJavaScriptFromString("var current=new MQA.Poi( {lat:" + viewinvoices[listAllItems.SelectedIndex].latitude + ", lng:" + viewinvoices[listAllItems.SelectedIndex].longitude + "} );");
                            webkitBrowser.StringByEvaluatingJavaScriptFromString("map.addShape(current);");
                            pointadded = true;

                        }
                    }
                }

            }
            catch { }
            if (GlobalVar.authenticated == false)
            {
                moveUp.Enabled = false;
                moveDown.Enabled = false;
                addItem.Enabled = false;
                removeItem.Enabled = false;

            }
        }

        private void removeItem_Click(object sender, EventArgs e)
        {
            

            int index=0;
            for (int i = 0; i < scheduledinvoicesorder.Count; i++)
            {
                if (scheduledinvoicesorder[i] == listRouteItems.SelectedIndex)
                {
                    index = i;

                }
            }
            int index2=0;
            List<InvoiceRouteInfo> routeinfo=mysql_invoiceroutes.GetRouteInfo(viewroute);
            for(int j=0;j<routeinfo.Count;j++)
            {
                if(routeinfo[j].invoicenumber==scheduledinvoices[index].number)
                {
                    index2=j;

                }
            }
            mysql_invoiceroutes.DeleteRouteInfo(routeinfo[index2],true);

            RefreshData(true, 0);
               
        }
        private void RefreshfindAddress()
        {
            if (listAllItems.SelectedIndex != -1)
            {
                if (viewinvoices[listAllItems.SelectedIndex].longitude == 0 && viewinvoices[listAllItems.SelectedIndex].latitude == 0)
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
                    ThreadArgs threadargs = new ThreadArgs();
                    threadargs.address = address;
                    threadargs.list = 2;
                    threadargs.selection = listAllItems.SelectedIndex;
                    _bw1.RunWorkerAsync(threadargs);
                }
                else
                {
                    findAddress.Image = RRS.Properties.Resources.greencheck;
                }
            }
            if (listRouteItems.SelectedIndex != -1)
            {
                int index = 0;
                for (int i = 0; i < scheduledinvoicesorder.Count; i++)
                {
                    if (listRouteItems.SelectedIndex == scheduledinvoicesorder[i])
                    {
                        index= i;
                    }
                }
                if (scheduledinvoices[index].longitude == 0 && scheduledinvoices[index].latitude == 0)
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
                    ThreadArgs threadargs = new ThreadArgs();
                    threadargs.address = address;
                    threadargs.list = 1;
                    threadargs.selection = index;
                    
                    _bw1.RunWorkerAsync(threadargs);
                }
                else
                {
                    findAddress.Image = RRS.Properties.Resources.greencheck;
                }
            }
        }
        void bw1_DoWork(object sender, DoWorkEventArgs e)
        {
            ThreadArgs threadargs = (ThreadArgs)e.Argument;
            string address = threadargs.address;
            Mapquest mapquest = new Mapquest(GlobalVar.sqlsettings.mapquestkey);
            GeoData currentgeodata = mapquest.GetLocation(address);

            threadargs.geodata = currentgeodata;
            e.Result = threadargs;

        }
        void bw1_RunWorkerCompleted(object sender,
                                   RunWorkerCompletedEventArgs e)
        {
            try
            {
                ThreadArgs threadargs = (ThreadArgs)e.Result;
                GeoData currentgeodata = threadargs.geodata;
                if (threadargs.list == 2)
                {
                    int index = threadargs.selection;
                    Invoice newinvoice = new Invoice(viewinvoices[index].number, viewinvoices[index].value, viewinvoices[index].due, viewinvoices[index].customername, currentgeodata.street[0], "", currentgeodata.city[0], currentgeodata.state[0], currentgeodata.zip[0], currentgeodata.longitude[0], currentgeodata.latitude[0], viewinvoices[index].delivered);
                    mysql_invoices.UpdateInvoice(newinvoice, viewinvoices[index]);
                    viewinvoices[index] = newinvoice;
                    findAddress.Image = RRS.Properties.Resources.greencheck;
                }
                if (threadargs.list == 1)
                {
                    int index=threadargs.selection;
                    Invoice newinvoice = new Invoice(scheduledinvoices[index].number, scheduledinvoices[index].value, scheduledinvoices[index].due, scheduledinvoices[index].customername, currentgeodata.street[0], "", currentgeodata.city[0], currentgeodata.state[0], currentgeodata.zip[0], currentgeodata.longitude[0], currentgeodata.latitude[0], scheduledinvoices[index].delivered);
                    mysql_invoices.UpdateInvoice(newinvoice, scheduledinvoices[index]);
                    scheduledinvoices[index] = newinvoice;
                    findAddress.Image = RRS.Properties.Resources.greencheck;
                }
            }
            catch
            {
                findAddress.Image = RRS.Properties.Resources.redx;
            }
        }
        private void moveUp_Click(object sender, EventArgs e)
        {
            int index = 0;
            for (int i = 0; i < scheduledinvoicesorder.Count; i++)
            {
                if (scheduledinvoicesorder[i] == listRouteItems.SelectedIndex)
                {
                    index = i;
                }
            }
            int index2 = 0;
            List<InvoiceRouteInfo> routeinfo = mysql_invoiceroutes.GetRouteInfo(viewroute);
            for (int j = 0; j < routeinfo.Count; j++)
            {
                if (routeinfo[j].invoicenumber == scheduledinvoices[index].number)
                {
                    index2 = j;
                }
            }
            mysql_invoiceroutes.MoveUp(routeinfo[index2]);

            
                RefreshData(true, listRouteItems.SelectedIndex - 1);
        }

        private void moveDown_Click(object sender, EventArgs e)
        {
            int index = 0;
            for (int i = 0; i < scheduledinvoicesorder.Count; i++)
            {
                if (scheduledinvoicesorder[i] == listRouteItems.SelectedIndex)
                {
                    index = i;
                }
            }
            int index2 = 0;
            List<InvoiceRouteInfo> routeinfo = mysql_invoiceroutes.GetRouteInfo(viewroute);
            for (int j = 0; j < routeinfo.Count; j++)
            {
                if (routeinfo[j].invoicenumber == scheduledinvoices[index].number)
                {
                    index2 = j;
                }
            }
            mysql_invoiceroutes.MoveDown(routeinfo[index2]);
            RefreshData(true,listRouteItems.SelectedIndex+1);
        }

        private void removeInvoice_Click(object sender, EventArgs e)
        {
            AddInvoice addinvoice = new AddInvoice();
            addinvoice.SetDay(viewdate,false,driver);
            addinvoice.Show();
            this.Close();
        }

        private void findAddress_Click(object sender, EventArgs e)
        {
            FindAddress findaddress = new FindAddress();
            if (listRouteItems.SelectedIndex != -1)
            {
                int index=0;
                for (int i = 0; i < scheduledinvoicesorder.Count; i++)
                {
                    if (listRouteItems.SelectedIndex == scheduledinvoicesorder[i])
                    {
                        index = i;
                    }
                }
                    findaddress.SetInvoice(scheduledinvoices[index], 1, viewdate, driver, true);
            }
            if (listAllItems.SelectedIndex != -1)
            {
                findaddress.SetInvoice(viewinvoices[listAllItems.SelectedIndex], 1, viewdate, driver, true);
            }
            findaddress.Show();
            this.Close();
        }

        private void optimizeRoute_Click(object sender, EventArgs e)
        {

            _bw3 = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            _bw3.DoWork += bw3_DoWork;
            _bw3.RunWorkerCompleted += bw3_RunWorkerCompleted;
            DirectionArgs directionargs = new DirectionArgs();
            counter++;
            directionargs.counter = counter;
            directionargs.optimize = true;
            //add RRS info
            directionargs.latitude.Add(GlobalVar.sqlsettings.RRSlatitude);
            directionargs.longitude.Add(GlobalVar.sqlsettings.RRSlongitude);
            directionargs.address.Add(GlobalVar.sqlsettings.RRSaddress);
            for (int i = 0; i < scheduledinvoices.Count; i++)
            {

                
                    directionargs.latitude.Add(scheduledinvoices[i].latitude);
                    directionargs.longitude.Add(scheduledinvoices[i].longitude);
                    string address = "";
                    if (scheduledinvoices[i].addr1 != "")
                    {
                        address = address + scheduledinvoices[i].addr1;
                    }
                    else
                    {
                        address = address + scheduledinvoices[i].addr2;
                    }
                    address = address + ", " + scheduledinvoices[i].city;
                    address = address + ", " + scheduledinvoices[i].state;
                    address = address + ",  " + scheduledinvoices[i].zip;

                    directionargs.address.Add(address);
                
            }

            directionargs.latitude.Add(GlobalVar.sqlsettings.RRSlatitude);
            directionargs.longitude.Add(GlobalVar.sqlsettings.RRSlongitude);
            directionargs.address.Add(GlobalVar.sqlsettings.RRSaddress);
            optimizeRoute.Enabled = false;
            optimizeRoute.Text = "Calculating Optimal Route...";
            _bw3.RunWorkerAsync(directionargs);
        }
        
        void bw3_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Mapquest mapquest = new Mapquest(GlobalVar.sqlsettings.mapquestkey);
                Map.Directions directions = new Map.Directions(GlobalVar.sqlsettings.mapquestkey);

                DirectionArgs args = (DirectionArgs)e.Argument;
                for (int i = 0; i < args.latitude.Count; i++)
                {
                    if (args.latitude[i] == 0 && args.longitude[i] == 0)
                    {
                        GeoData geodata = mapquest.GetLocation(args.address[i]);
                        if (geodata == null)
                        {
                            args.counter = -1;
                            break;
                        }
                        args.latitude[i] = geodata.latitude[0];
                        args.longitude[i] = geodata.longitude[0];
                    }
                }
                /*for (int i = 1; i<args.latitude.Count-1; i++)
                {
                    for(int j=1;j<args.latitude.Count-1;j++)
                    {
                        if (args.longitude[j] == args.longitude[i] && args.latitude[j] == args.latitude[i]&&i!=j)
                        {
                            args.longitude.RemoveAt(j);
                            args.latitude.RemoveAt(j);
                            args.address.RemoveAt(j);
                            j = 1;
                        }
                    }
                }*/
                    if (args.counter != -1)
                    {
                        DirectionData directiondata = directions.GetDirections(args.latitude, args.longitude, args.optimize);
                        args.directiondata = directiondata;
                    }
                e.Result = args;
            }
            catch { }
        }
        void bw3_RunWorkerCompleted(object sender,
                                   RunWorkerCompletedEventArgs e)
        {
            DirectionArgs args = (DirectionArgs)e.Result;
            List<int> newsequence = new List<int>();
            //int index = 1;
            /*if ((args.address.Count - 2) != scheduledinvoices.Count)
            {
                newsequence.Add(0);
                for (int i = 1; i < args.directiondata.locationSequence.Count-1; i++)
                {
                    for (int k = 1; k < args.directiondata.locationSequence.Count - 1; k++)
                    {
                        if (args.directiondata.locationSequence[k] == i)
                        {
                            //newsequence.RemoveAt(i);
                            List<int> addsequence = new List<int>();
                            for (int j = 0; j < scheduledinvoices.Count; j++)
                            {
                                if (args.latitude[k] == scheduledinvoices[j].latitude && args.longitude[k] == scheduledinvoices[j].longitude)
                                {
                                    addsequence.Add(index);
                                    index++;
                                    //newsequence.Insert(i, j + 1);

                                }
                            }
                            newsequence.AddRange(addsequence);
                        }
                    }
                }
                newsequence.Add(newsequence.Count);
                args.directiondata.locationSequence = newsequence;
            }*/
            
            for (int i = 0; i < scheduledinvoices.Count; i++)
            {
                
                for (int k = 0; k < routes.Count; k++)
                {
                    if (routes[k].number == viewroute.number)
                    {
                        for (int j = 0; j < routes[k].invoicerouteinfo.Count; j++)
                        {
                            if (routes[k].invoicerouteinfo[j].invoicenumber == scheduledinvoices[i].number)
                            {
                                mysql_invoiceroutes.DeleteRouteInfo(routes[k].invoicerouteinfo[j],false);
                                break;
                                

                            }
                        }
                        break;
                    }
                    
                }

            }
            for (int i = 0; i < scheduledinvoices.Count; i++)
            {
                int index2=0;
                for (int j = 1; j < args.directiondata.locationSequence.Count-1; j++)
                {
                    if (args.directiondata.locationSequence[j ]-1  == i)
                    {
                        index2 = j-1;
                    }
                }
                InvoiceRouteInfo routeinfo = new InvoiceRouteInfo(scheduledinvoices[i].number, viewroute.number, index2);//args.directiondata.locationSequence[i + 1] - 1
                mysql_invoiceroutes.AddRouteInfo(routeinfo); 
            }
            if (GlobalVar.authenticated == true)
            {
                optimizeRoute.Enabled = true;
            }
                optimizeRoute.Text = "Find Optimal Route";
            RefreshData(false,0);
        }

        private void removeInvoice_Click_1(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Prevent this invoice from being imported again for the current day?", "", MessageBoxButtons.YesNo);
                
                int selection = 0;
                bool flag = false;
                InvoiceRouteInfo todelete = null;
                if (listAllItems.SelectedIndex != -1)
                {
                    flag = false;
                    selection = listAllItems.SelectedIndex;
                    if (selection == (listAllItems.Items.Count - 1))
                    {
                        selection--;
                    }
                    mysql_invoices.DeleteInvoice(viewinvoices[listAllItems.SelectedIndex]);
                    if (result == DialogResult.Yes)
                    {
                            mysql_invoices.BlockInvoice(viewinvoices[listAllItems.SelectedIndex]);
                    }
                    for (int i = 0; i < routes.Count; i++)
                    {
                        for (int j = 0; j < routes[i].invoicerouteinfo.Count; j++)
                        {
                            if (viewinvoices[listAllItems.SelectedIndex].number == routes[i].invoicerouteinfo[j].invoicenumber)
                            {

                                todelete = routes[i].invoicerouteinfo[j];
                            }
                        }
                    }
                }
                if (listRouteItems.SelectedIndex != -1)
                {
                    int index=0;
                    for (int i = 0; i < scheduledinvoices.Count; i++)
                    {
                        if (scheduledinvoicesorder[i] == listRouteItems.SelectedIndex)
                        {
                            index = i;
                        }
                    }
                    selection = listRouteItems.SelectedIndex;
                    flag = true;
                    if (selection == (listRouteItems.Items.Count - 1))
                    {
                        selection--;
                    }
                    mysql_invoices.DeleteInvoice(scheduledinvoices[index]);
                    if (result == DialogResult.Yes)
                    {
                        mysql_invoices.BlockInvoice(scheduledinvoices[index]);
                    }
                    for (int i = 0; i < routes.Count; i++)
                    {
                        for (int j = 0; j < routes[i].invoicerouteinfo.Count; j++)
                        {
                            if (scheduledinvoices[index].number == routes[i].invoicerouteinfo[j].invoicenumber)
                            {

                                todelete = routes[i].invoicerouteinfo[j];
                            }
                        }
                    }
                }
                if (todelete != null)
                {
                    mysql_invoiceroutes.DeleteRouteInfo(todelete, true);
                }

                RefreshData(flag,selection);
            }
            catch { }
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            StreamWriter sw= new StreamWriter("toprint.html", false);
            StreamReader sr=new StreamReader("map.html");
            sw.WriteLine("<style>");
            sw.WriteLine(".break { page-break-before: always; }");
            sw.WriteLine("</style>");
            sw.WriteLine("<body style=\"font-family:calibri\">");
            sw.WriteLine("<div style=\"width:21.59cm\">");
            sw.WriteLine("Driver: "+driver.name+"<br>");
            sw.WriteLine("Number of Stops: "+stopcounter+"<br>");
            sw.WriteLine("Distance: "+textTotalMiles.Text+"<br>");

            sw.WriteLine("Route Time: " + textTotalTime.Text + "<br>");
            sw.WriteLine("<head>");
            sw.WriteLine("<br><strong>Scheduled Deliveries:<br></strong>");
            sw.WriteLine("<TABLE border=1>");
            stopcounter = 1;
            
            for (int i = 0; i < scheduledinvoices.Count; i++)
            {
                sw.WriteLine("<TR>");
                

                

                    for (int j = 0; j < scheduledinvoices.Count; j++)
                    {
                        if (i == (scheduledinvoicesorder[j]))
                        {
                            int index = -1;
                            for (int k = 0; k < scheduledinvoices.Count; k++)
                            {
                                if ((scheduledinvoicesorder[j] - 1) == scheduledinvoicesorder[k])
                                {
                                    index = k;
                                }
                            }
                            if (index != -1)
                            {
                                if (scheduledinvoices[index].latitude == scheduledinvoices[j].latitude && scheduledinvoices[index].longitude == scheduledinvoices[j].longitude)
                                {


                                }
                                else
                                {
                                    stopcounter++;
                                }
                            }
                            else { counter++; }
                            sw.WriteLine("<TD>");
                            sw.WriteLine(stopcounter);
                            sw.WriteLine("</TD>");
                            sw.WriteLine("<TD>");
                            sw.WriteLine(scheduledinvoices[j].number);
                            sw.WriteLine("</TD>");
                            sw.WriteLine("<TD>");
                            sw.WriteLine(DataFormat.FormatMoneyToString(scheduledinvoices[j].value));
                            sw.WriteLine("</TD>");
                            sw.WriteLine("<TD>");
                            sw.WriteLine(scheduledinvoices[j].addr1+", "+scheduledinvoices[j].city+", "+scheduledinvoices[j].state+"  "+scheduledinvoices[j].zip);
                            sw.WriteLine("</TD>");
                        }
                    }
                


                sw.WriteLine("</TR>");
            }
            sw.WriteLine("</TABLE>");
            sw.WriteLine("<br><br>");
            String mapstring = sr.ReadToEnd();
            sw.Write(mapstring);
                sw.Write("</body>");
            sr.Close();
            sw.Close();
            Print printwindow = new Print();
            printwindow.SetDate(viewdate);
            printwindow.Show();
            this.Close();
        }

        private void webkitBrowser_Load(object sender, EventArgs e)
        {

        }

        private void textTotalMiles_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
