﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using Map;

namespace RRS
{
    public partial class OptimizeWindow : Form
    {
        int pointtopointcount;
        int counter;
        List<RouteInfo> routes;
        public DateTime viewdate;
        List<Invoice> viewinvoices;
        List<DriverInfo> companydrivers;
        bool optimizationrunning;
        List<DriverInfo> scheduleddrivers;
        List<int> driverassignments;
        MySQL_Invoices mysql_invoices;
        MySQL_Drivers mysql_drivers;
        MySQL_Routes mysql_routes;
        MySQL_InvoiceRoute mysql_invoiceroutes;
        static BackgroundWorker _bw1;
        static BackgroundWorker _bw2;
        static BackgroundWorker _bw3;
        static BackgroundWorker _bw4;
        List<List<InvoiceRouteInfo>> optimizedroutes;
        bool optimized;
        string currentstatus;
        List<int> distances;
        Mapquest_Matrix matrixbuilder;
        internal class OptimizeArgs
        {
            public OptimizeArgs()
            {
                output = "";
                longitudein = new List<double>();
                latitudein = new List<double>();

            }
            internal string output { get; set; }
            internal List<double> longitudein { get; set; }
            internal List<double> latitudein { get; set; }
            
        }
        internal class DirectionArgs
        {
            public DirectionArgs()
            {
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
        public void SetData(DateTime date, List <DriverInfo> scheduleddrivers)
        {
            viewdate = date;
            this.scheduleddrivers = scheduleddrivers;
        }

        public OptimizeWindow()
        {
            InitializeComponent();
        }

        private void OptimizeWindow_Load(object sender, EventArgs e)
        {
            
            mysql_invoices = new MySQL_Invoices(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
            mysql_drivers = new MySQL_Drivers(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
            mysql_routes = new MySQL_Routes(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
            mysql_invoiceroutes = new MySQL_InvoiceRoute(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
            buttonSave.Enabled = false;
            textDistance.Text = "600";
            textCapacity.Text = "15";
            textRadius.Text = "50";
            textRate.Text = "";
            textOvertime.Text = "";
            textFuel.Text = "";
            textMaitenance.Text = "";
            listDrivers.Items.Clear();
            listRoutes.Items.Clear();
            optimized = false;
            counter = 0;
            radioButton1.Checked = true;
            driverassignments = new List<int>();
            textStatus.Text = "";
            checkBox1.Checked = true;
            DoRefresh();
            toolStripStatusLabel1.Text = "";
            
        }
        private void DoRefresh()
        {
            try
            {
                this.Text = "Full Day Optimization - " + viewdate.Month + "/" + viewdate.Day + "/" + viewdate.Year + " - " + viewdate.DayOfWeek;
                companydrivers = new List<DriverInfo>();
                companydrivers = mysql_drivers.GetDrivers();
                viewinvoices = mysql_invoices.GetInvoices(viewdate);
                routes = mysql_routes.GetRouteInfo(viewdate);
                for (int i = 0; i < routes.Count; i++)
                {
                    routes[i].invoicerouteinfo = mysql_invoiceroutes.GetRouteInfo(routes[i]);
                }
                    for (int j = 0; j < scheduleddrivers.Count; j++)
                    {
                        for (int i = 0; i < companydrivers.Count; i++)
                        {

                            if (scheduleddrivers[j].number == companydrivers[i].number)
                            {
                                companydrivers.RemoveAt(i);
                                i = 0;
                            }
                        }
                    }
                int assignedinvoicecount=0;
                for (int i = 0; i<routes.Count; i++)
                {
                    for (int j = 0; j < routes[i].invoicerouteinfo.Count; j++)
                    {
                        for(int k=0;k<viewinvoices.Count;k++)
                        {
                            if (routes[i].invoicerouteinfo[j].invoicenumber == viewinvoices[k].number)
                            {
                                viewinvoices.RemoveAt(k);
                                k = 0;
                                assignedinvoicecount++;
                            }
                        }
                    }
                }
                textStatus.Text = "Optimization will run on " + viewinvoices.Count + " invoices.  " + assignedinvoicecount + " invoices have already been assigned.";
                    for (int i = 0; i < companydrivers.Count; i++)
                    {
                        listDrivers.Items.Add(companydrivers[i].name + " - Unassigned");
                    }
                if (listDrivers.Items.Count > 0)
                {
                    listDrivers.SelectedIndex = 0;
                }
                toolStripStatusLabel1.Text = "";
                if (viewinvoices.Count == 0)
                {
                    buttonOptimization.Enabled = false;
                }
            }
            catch { }
        }

        private void listDrivers_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listDrivers.SelectedIndex >= 0)
                {
                    textRate.Text = DataFormat.FormatMoneyToString(companydrivers[listDrivers.SelectedIndex].hour_rate);
                    textFuel.Text = DataFormat.FormatMoneyToString(companydrivers[listDrivers.SelectedIndex].fuel_surch);
                    textMaitenance.Text = DataFormat.FormatMoneyToString(companydrivers[listDrivers.SelectedIndex].maint_surch);
                    textOvertime.Text = DataFormat.FormatMoneyToString(companydrivers[listDrivers.SelectedIndex].overtime_rate);
                }
                if (listRoutes.SelectedIndex >= 0 && listDrivers.SelectedIndex >= 0)
                {
                    buttonAdd.Enabled = false;

                    if (driverassignments.Count > 0)
                    {
                        if (driverassignments[listRoutes.SelectedIndex] == -1)
                        {
                            buttonAdd.Enabled = true;

                        }
                    }
                    for (int i = 0; i < driverassignments.Count; i++)
                    {
                        if (driverassignments[i] == listDrivers.SelectedIndex)
                        {
                            buttonAdd.Enabled = false;

                        }
                    }

                }
                else
                {
                    buttonAdd.Enabled = false;
                    buttonRemove.Enabled = false;
                }
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            //optimizationrunning = false;
            if (matrixbuilder != null)
            {
                for (int i = 0; i < matrixbuilder.bws.Count; i++)
                {
                    matrixbuilder.points.Clear();
                    if (matrixbuilder.bws[i] != null)
                    {
                        //matrixbuilder.bws[i].CancelAsync();
                    }
                }
            }
            
            if (_bw1 != null)
            {
                _bw1.CancelAsync();
            }
            if (_bw2 != null)
            {
                _bw2.CancelAsync();
            }
            if (_bw3 != null)
            {
                _bw3.CancelAsync();
            }
            if (_bw4 != null)
            {
                _bw4.CancelAsync();
            }
            
            DeliverySchedule deliveryschedule = new DeliverySchedule();
            deliveryschedule.SetDate(viewdate,0);
            deliveryschedule.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //DialogResult result = MessageBox.Show("Saving changes will remove all configured routes for the current day and replace them with optimized routes.  Are you sure?", "Warning", MessageBoxButtons.YesNo);
                //if (result == DialogResult.Yes)
                //{

                   // for (int i = 0; i < driverassignments.Count; i++)
                    //{
                     //   mysql_routes.DeleteRouteInfo(companydrivers[driverassignments[i]].number, viewdate);
                    //}
                    for (int i = 0; i < driverassignments.Count; i++)
                    {
                        RouteInfo toadd = new RouteInfo(0, viewdate, companydrivers[driverassignments[i]].number, distances[i]);
                        mysql_routes.AddRouteInfo(toadd);
                    }
                    List<RouteInfo> currentroutes = mysql_routes.GetRouteInfo(viewdate);

                    for (int i = 0; i < driverassignments.Count; i++)
                    {
                        int routenumber = 0;
                        for (int j = 0; j < currentroutes.Count; j++)
                        {
                            if (currentroutes[j].drivernumber == companydrivers[driverassignments[i]].number)
                            {
                                routenumber = currentroutes[j].number;
                            }
                        }
                        for (int j = 0; j < optimizedroutes[i].Count; j++)
                        {
                            optimizedroutes[i][j].routenumber = routenumber;
                            InvoiceRouteInfo toadd = new InvoiceRouteInfo(optimizedroutes[i][j].invoicenumber, optimizedroutes[i][j].routenumber, optimizedroutes[i][j].stop);
                            mysql_invoiceroutes.AddRouteInfo(toadd);
                        }

                    }
                    DeliverySchedule deliveryschedule = new DeliverySchedule();
                    deliveryschedule.SetDate(viewdate,0);
                    deliveryschedule.Show();
                    this.Close();
                }
            //}
            catch { }
        }

        private void buttonOptimization_Click(object sender, EventArgs e)
        {
            try
            {
                button1.Enabled = false;
                listRoutes.Enabled = false;
                listDrivers.Enabled = false;
                checkBox1.Enabled = false;
                buttonOptimization.Enabled = false;
                buttonOptimization.Text = "Running optimization...";
                toolStripStatusLabel1.Text = "Running optimization...";
                _bw1 = new BackgroundWorker
                {
                    WorkerReportsProgress = true,
                    WorkerSupportsCancellation = true
                };
                _bw1.DoWork += bw1_DoWork;

                _bw1.RunWorkerCompleted += bw1_RunWorkerCompleted;
                _bw1.RunWorkerAsync();
                
                _bw4 = new BackgroundWorker
                {
                    WorkerReportsProgress = true,
                    WorkerSupportsCancellation = true
                };
                _bw4.DoWork += bw4_DoWork;

                _bw4.RunWorkerCompleted += bw4_RunWorkerCompleted;
                
                _bw4.RunWorkerAsync();
            }
            catch
            {
                listRoutes.Enabled = true;
                listDrivers.Enabled = true;
                checkBox1.Enabled = true;
                buttonOptimization.Enabled = true;
                buttonOptimization.Text = "Run Optimization";
                
                
                optimizationrunning = false;
                currentstatus = "";
            }
        }
        void bw4_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                optimizationrunning = true;
                DateTime starttime = DateTime.Now;
                while (optimizationrunning == true)
                {
                    DateTime currenttime = DateTime.Now;
                    TimeSpan elapsedseconds = starttime.Subtract(currenttime);
                    TimeSpan elapsedminutes = starttime.Subtract(currenttime);
                    if (matrixbuilder != null)
                    {
                        if (matrixbuilder.points.Count > 0)
                        {
                            currentstatus = matrixbuilder.threadstatus;
                        }
                    }
                    toolStripStatusLabel1.Text = "[" + DataFormat.FormatTimeToString2((-1 * elapsedseconds.Seconds) + (-60 * elapsedminutes.Minutes)) + "] " + currentstatus;
                }
            }
            catch { }
        }
        void bw4_RunWorkerCompleted(object sender,
                   RunWorkerCompletedEventArgs e)
        {
            toolStripStatusLabel1.Text = currentstatus;
        }
        void bw1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                
                List<double> longitudein = new List<double>();
                List<double> latitudein = new List<double>();
                for (int i = 0; i < viewinvoices.Count; i++)
                {
                    latitudein.Add(viewinvoices[i].latitude);
                    longitudein.Add(viewinvoices[i].longitude);
                }

                //figure out demand
                List<int> demand = new List<int>();
                int index = 0;
                for (int i = 0; i < latitudein.Count; i++)
                {
                    demand.Add(1);
                    double lat1 = latitudein[i];
                    double long1 = longitudein[i];
                    for (int j = i + 1; j < latitudein.Count; j++)
                    {
                        if (latitudein[j] == lat1 && longitudein[j] == long1)
                        {
                            demand[index]++;
                            latitudein.RemoveAt(j);
                            longitudein.RemoveAt(j);
                            j--;

                        }
                    }
                    index++;
                }
                int distance = Int32.Parse(textDistance.Text);
                Optimize optimize = new Optimize();
                optimize.buildPoints(GlobalVar.sqlsettings.RRSlatitude, GlobalVar.sqlsettings.RRSlongitude, latitudein, longitudein);
                if (radioButton1.Checked == true)
                {
                    distance = 1000;
                    for (int i = 0; i < optimize.xin.Count;i++ )
                    {
                        double hypotenuse = (double)Math.Sqrt(Math.Pow(optimize.xin[i],2)+ Math.Pow(optimize.yin[i],2));
                        if (hypotenuse > Int32.Parse(textRadius.Text))
                        {
                            optimize.xin.RemoveAt(i);
                            optimize.yin.RemoveAt(i);
                            demand.RemoveAt(i);
                            latitudein.RemoveAt(i);
                            longitudein.RemoveAt(i);
                            i = -1;
                        }
                    }
                }
                if (latitudein.Count == 0)
                {
                    optimizationrunning = false;
                    return;
                }
                if (checkBox1.Checked == false)
                {


            this.Invoke((MethodInvoker)delegate
            {
                button1.Enabled = true;
            });
            if (matrixbuilder != null)
            {
                if ((latitudein.Count+1) != matrixbuilder.distancematrix.Count)
                {
                    matrixbuilder = new Mapquest_Matrix(latitudein, longitudein, GlobalVar.sqlsettings.RRSlatitude, GlobalVar.sqlsettings.RRSlongitude);
                    matrixbuilder.dooptimization();
                }
            }
            else
            {
                matrixbuilder = new Mapquest_Matrix(latitudein, longitudein, GlobalVar.sqlsettings.RRSlatitude, GlobalVar.sqlsettings.RRSlongitude);
                matrixbuilder.dooptimization();
            }
            

                   int maxtime = (int)((double)Int32.Parse(textDistance.Text) * (double)3600);
                    optimize.writeinputfile_matrix(Int32.Parse(textCapacity.Text), maxtime,matrixbuilder.distancematrix,demand);
                    pointtopointcount = matrixbuilder.points.Count;
                    
                }
                else
                {
                    
                    optimize.writeinputfile(Int32.Parse(textCapacity.Text), distance, demand);
                }
                this.Invoke((MethodInvoker)delegate
                {
                    button1.Enabled = false;
                });
                currentstatus = " - (2/3) Solving VRP Problem";
                Process p = new Process();

                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.FileName = "vrp_rtr.exe";
                p.StartInfo.Arguments = "-f input.vrp -D 50 -L 5 -N 30 -h ONE_POINT_MOVE -h TWO_OPT -h THREE_OPT -v -out Test_inst2.sol";
                p.Start();

                string output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                //log output
                StreamWriter sw = new StreamWriter("out.log");
                sw.WriteLine(output);
                sw.Close();
                OptimizeArgs args = new OptimizeArgs();
                args.output = output;
                args.latitudein = latitudein;
                args.longitudein = longitudein;
                e.Result = (object)args;
            }
            catch
            {

            }
        }
        void bw1_RunWorkerCompleted(object sender,
                           RunWorkerCompletedEventArgs e)
        {
            try
            {
                OptimizeArgs args = (OptimizeArgs)e.Result;
                if (args.output.IndexOf("Default routes length violation:") > 0)
                {
                    Regex regex = new Regex(@"Default routes length violation: (\d*)");
                    MatchCollection result = regex.Matches(args.output);
                    int minimumdistance = 0;
                    for (int i = 0; i < result.Count; i++)
                    {
                        int value = Int32.Parse(result[i].Groups[1].Value) + 1;
                        if (value > minimumdistance)
                        {
                            minimumdistance = value;
                        }
                    }
                    listRoutes.Enabled = true;
                    listDrivers.Enabled = true;
                    checkBox1.Enabled = true;
                    buttonOptimization.Enabled = true;
                    buttonOptimization.Text = "Run Optimization";
                    button1.Enabled = true;
                    currentstatus = "";
                    if (checkBox1.Checked == true)
                    {
                        currentstatus = "Route length violation: At least one delivery point requires a minimimum distance contraint of " + minimumdistance + " miles.";
                    }
                    else
                    {


                        currentstatus = "Route time violation: At least one delivery point requires a minimimum driving time of " + DataFormat.FormatTimeToString((long)minimumdistance);
                    }
                    optimizationrunning = false;
                    return;
                }
                else if (args.output.IndexOf("Solution for problem input") > 0)
                {

                    Regex regex = new Regex(@"Route \d\d\d\d\(routenum=(\d)\).*\n(.*)\n");
                    MatchCollection result = regex.Matches(args.output);

                    List<List<int>> order = new List<List<int>>();
                    for (int i = 0; i < result.Count; i++)
                    {
                        order.Add(new List<int>());
                        string orderstring = result[i].Groups[2].Value;
                        string[] array = orderstring.Split('-');
                        for (int j = 0; j < array.Length; j++)
                        {
                            order[i].Add(Int32.Parse(array[j]));
                        }


                    }
                    optimizedroutes = new List<List<InvoiceRouteInfo>>();
                    listRoutes.Items.Clear();
                    listDrivers.Items.Clear();

                    for (int i = 0; i < order.Count; i++)
                    {
                        optimizedroutes.Add(new List<InvoiceRouteInfo>());
                        listRoutes.Items.Add("Route " + (i + 1));
                        int index = 0;
                        for (int j = 1; j < (order[i].Count - 1); j++)
                        {

                            for (int k = 0; k < viewinvoices.Count; k++)
                            {
                                if (viewinvoices[k].latitude == args.latitudein[order[i][j] - 2] && viewinvoices[k].longitude == args.longitudein[order[i][j] - 2])
                                {
                                    InvoiceRouteInfo toinsert = new InvoiceRouteInfo(viewinvoices[k].number, i, index);
                                    index++;
                                    optimizedroutes[i].Add(toinsert);
                                }
                            }
                        }
                    }
                    button1.Enabled = true;
                    _bw3 = new BackgroundWorker
                    {
                        WorkerReportsProgress = true,
                        WorkerSupportsCancellation = true
                    };
                    _bw3.DoWork += bw3_DoWork;

                    _bw3.RunWorkerCompleted += bw3_RunWorkerCompleted;
                    _bw3.RunWorkerAsync(order);
                    return;
                }
            }
            catch {
                listRoutes.Enabled = true;
                listDrivers.Enabled = true;
                checkBox1.Enabled = true;
                buttonOptimization.Enabled = true;
                buttonOptimization.Text = "Run Optimization";
                currentstatus = "";
                optimizationrunning = false;
            }

        }

        void bw3_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = e.Argument;
            distances = new List<int>();
            for (int i = 0; i < optimizedroutes.Count; i++)
            {
                currentstatus = " - (3/3) Querying Mapquest for optimal delivery route order ("+(i+1)+" of "+(optimizedroutes.Count)+")";
                List<double> latitude = new List<Double>();
                List<double> longitude = new List<Double>();
                latitude.Add(GlobalVar.sqlsettings.RRSlatitude);
                longitude.Add(GlobalVar.sqlsettings.RRSlongitude);
                for (int j = 0; j < optimizedroutes[i].Count; j++)
                {
                    for (int k = 0; k < viewinvoices.Count; k++)
                    {
                        if (optimizedroutes[i][j].invoicenumber == viewinvoices[k].number)
                        {
                            latitude.Add(viewinvoices[k].latitude);
                            longitude.Add(viewinvoices[k].longitude);
                        }
                    }

                }
                latitude.Add(GlobalVar.sqlsettings.RRSlatitude);
                longitude.Add(GlobalVar.sqlsettings.RRSlongitude);
                Map.Directions directions = new Map.Directions(GlobalVar.sqlsettings.mapquestkey);

                DirectionData directiondata = directions.GetDirections(latitude, longitude, true);
                distances.Add((int)directiondata.distance.Sum());

                for (int j = 0; j < optimizedroutes[i].Count; j++)
                {
                    for (int k = 1; k < directiondata.locationSequence.Count - 1; k++)
                    {
                        if (directiondata.locationSequence[k] == j + 1)
                        {
                            optimizedroutes[i][j].stop = k - 1;
                        }
                    }
                }

            }
            
        }

        void bw3_RunWorkerCompleted(object sender,
                   RunWorkerCompletedEventArgs e)
        {
            List<List<int>> order = new List<List<int>>();
            order = (List<List<int>>)e.Result;
            for (int i = 0; i < companydrivers.Count; i++)
            {
                listDrivers.Items.Add(companydrivers[i].name + " - Unassigned");
            }
            optimized = true;
            listRoutes.SelectedIndex = 0;
            listDrivers.SelectedIndex = 0;
            driverassignments = new List<int>();
            for (int i = 0; i < order.Count; i++)
            {
                driverassignments.Add(-1);
            }
            if (listDrivers.Items.Count > 0)
            {
                buttonAdd.Enabled = true;
            }
            listRoutes.Enabled = true;
            listDrivers.Enabled = true;
            checkBox1.Enabled = true;
            buttonOptimization.Enabled = true;
            buttonOptimization.Text = "Run Optimization";
            currentstatus= "Optimization Complete.";
            optimizationrunning = false;
        }
        private void listRoutes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listRoutes.SelectedIndex >= 0 && listDrivers.SelectedIndex >= 0)
                {
                    buttonRemove.Enabled = false;

                    if (driverassignments.Count > 0)
                    {
                        if (driverassignments[listRoutes.SelectedIndex] != -1)
                        {
                            buttonRemove.Enabled = true;
                            buttonAdd.Enabled = false;
                        }
                        else
                        {
                            listDrivers_SelectedIndexChanged(this, null);
                        }
                    }

                    /*for (int i = 0; i < driverassignments.Count; i++)
                    {
                        if (driverassignments[i] == listDrivers.SelectedIndex)
                        {
                            buttonRemove.Enabled = false;

                        }
                    }*/
                }
                else
                {
                    buttonAdd.Enabled = false;
                    buttonRemove.Enabled = false;
                }

                if (optimized == true && listRoutes.SelectedIndex >= 0)
                {
                    RefreshMap();
                }
            }
            catch { }
        }
        private void RefreshMap()
        {
            try
            {
                

                string currentdirectory = Directory.GetCurrentDirectory();
                currentdirectory = currentdirectory.Replace("\\", "/");
                currentdirectory = Uri.EscapeUriString(currentdirectory);
                string tovisit = "file:///" + currentdirectory + "/" + "loading.html";
                if (this.IsDisposed == false)
                {
                    webKitBrowser1.Navigate(tovisit);
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
                for (int i = 0; i < optimizedroutes[listRoutes.SelectedIndex].Count; i++)
                {
                    for (int k = 0; k < optimizedroutes[listRoutes.SelectedIndex].Count; k++)
                    {
                        if (optimizedroutes[listRoutes.SelectedIndex][k].stop == i)
                        {
                            for (int j = 0; j < viewinvoices.Count; j++)
                            {
                                if (optimizedroutes[listRoutes.SelectedIndex][k].invoicenumber == viewinvoices[j].number)
                                {

                                    directionargs.latitude.Add(viewinvoices[j].latitude);
                                    directionargs.longitude.Add(viewinvoices[j].longitude);
                                    string address = "";
                                    if (viewinvoices[j].addr1 != "")
                                    {
                                        address = address + viewinvoices[j].addr1;
                                    }
                                    else
                                    {
                                        address = address + viewinvoices[j].addr2;
                                    }
                                    address = address + ", " + viewinvoices[j].city;
                                    address = address + ", " + viewinvoices[j].state;
                                    address = address + ",  " + viewinvoices[j].zip;
                                    directionargs.address.Add(address);
                                }
                            }
                        }
                    }

                }

                directionargs.latitude.Add(GlobalVar.sqlsettings.RRSlatitude);
                directionargs.longitude.Add(GlobalVar.sqlsettings.RRSlongitude);
                directionargs.address.Add(GlobalVar.sqlsettings.RRSaddress);
                _bw2.RunWorkerAsync(directionargs);//temp
            }
            catch { }   
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
                if (args.counter == counter && args.counter != -1)
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
                    mapwriter.WriteMap(1, null, null, points_shape, points_latitude, points_longitude, sequence, 403, 560);
                    string currentdirectory = Directory.GetCurrentDirectory();
                    currentdirectory = currentdirectory.Replace("\\", "/");
                    currentdirectory = Uri.EscapeUriString(currentdirectory);
                    string tovisit = "file:///" + currentdirectory + "/" + "map.html";
                    if (this.IsDisposed == false)
                    {
                        webKitBrowser1.Navigate(tovisit);
                    }
                    //update miles
                    
                    int totalmiles = 0;
                    for (int i = 0; i < args.directiondata.distance.Count; i++)
                    {
                        totalmiles += (int)args.directiondata.distance[i];
                    }


                    long totaltime = 0;
                    for (int i = 0; i < args.directiondata.time.Count; i++)
                    {
                        totaltime += (int)args.directiondata.time[i];
                    }
                    
                    textDistance1.Text = totalmiles + " Mi";
                    textTime1.Text = DataFormat.FormatTimeToString(totaltime);
                    textInvoices.Text = optimizedroutes[listRoutes.SelectedIndex].Count.ToString();

                }
                else
                {
                    string currentdirectory = Directory.GetCurrentDirectory();
                    currentdirectory = currentdirectory.Replace("\\", "/");
                    currentdirectory = Uri.EscapeUriString(currentdirectory);
                    string tovisit = "file:///" + currentdirectory + "/" + "blank.html";
                    if (this.IsDisposed == false)
                    {
                        webKitBrowser1.Navigate(tovisit);
                    }
                    textDistance1.Text = "";
                    textTime1.Text = "";
                    textInvoices.Text = "";
                }
            }
            catch
            {
                string currentdirectory = Directory.GetCurrentDirectory();
                currentdirectory = currentdirectory.Replace("\\", "/");
                currentdirectory = Uri.EscapeUriString(currentdirectory);
                string tovisit = "file:///" + currentdirectory + "/" + "blank.html";
                try
                {
                    if (this.IsDisposed == false)
                    {
                        webKitBrowser1.Navigate(tovisit);
                    }
                }
                catch { }
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (listDrivers.SelectedIndex >= 0 && listRoutes.SelectedIndex >= 0)
                {
                    driverassignments[listRoutes.SelectedIndex] = listDrivers.SelectedIndex;
                    listRoutes.Items[listRoutes.SelectedIndex] = "Route " + (listRoutes.SelectedIndex + 1) + " - " + companydrivers[listDrivers.SelectedIndex].name;
                    listDrivers.Items[listDrivers.SelectedIndex] = companydrivers[listDrivers.SelectedIndex].name + " - Route " + (listRoutes.SelectedIndex + 1);
                    listDrivers.SelectedIndex = 0;
                    listRoutes.SelectedIndex = 0;
                    buttonSave.Enabled = true;
                    for (int i = 0; i < driverassignments.Count; i++)
                    {
                        if (driverassignments[i] == -1)
                        {
                            buttonSave.Enabled = false;
                        }
                    }
                }
            }
            catch { }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (listDrivers.SelectedIndex >= 0 && listRoutes.SelectedIndex >= 0)
                {

                    listRoutes.Items[listRoutes.SelectedIndex] = "Route " + (listRoutes.SelectedIndex + 1);
                    listDrivers.Items[driverassignments[listRoutes.SelectedIndex]] = companydrivers[driverassignments[listRoutes.SelectedIndex]].name + " - Unassigned";
                    driverassignments[listRoutes.SelectedIndex] = -1;
                    listDrivers.SelectedIndex = 0;
                    listRoutes.SelectedIndex = 0;
                    buttonSave.Enabled = false;
                }
            }
            catch { }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                radioButton2.Checked = false;
                textRadius.Enabled = true;
                textDistance.Enabled = false;
            }

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                radioButton1.Checked = false;
                textRadius.Enabled = false;
                textDistance.Enabled = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                labelAllType.Text = "Miles";
                textDistance.Text = "600";
                radioButton2.Text = "Optimize all available invoices (max route distance)";
            }
            else
            {
                labelAllType.Text = "Hours";
                textDistance.Text = "10";
                radioButton2.Text = "Optimize all available invoices (max driving time)";
            }
        }
        

    }
}
