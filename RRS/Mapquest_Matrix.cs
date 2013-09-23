using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Map;
using System.ComponentModel;
using System.Drawing;
namespace RRS
{
    class Mapquest_Matrix
    {
        public struct PointD
        {
            
            public double RX;
            public double RY;
            public int RZ;
            public double CX;
            public double CY;
            public int CZ;
            public PointD(double rx, double ry, int rz, double cx, double cy, int cz)
            {
                RX = rx;
                RY = ry;
                RZ = rz;
                CX = cx;
                CY = cy;
                CZ = cz;
            }


        }
        int currentpoint;
        int initialcount;
        public string threadstatus;
        int finishedthreads;
        public List<List<double>> distancematrix;
        public List<BackgroundWorker> bws;
        public Queue<PointD> points;
        public double depotlatitude;
        public double depotlongitude;
        public Mapquest_Matrix(List <double> latitude,List<double> longitude, double depotlatitude,double depotlongitude)
        {
            finishedthreads=0;
            points = new Queue<PointD>();
            this.depotlatitude = depotlatitude;
            this.depotlongitude = depotlongitude;
            latitude.Insert(0, depotlatitude);
            longitude.Insert(0, depotlongitude);
            distancematrix = new List<List<Double>>();
            for (int i = 0; i < latitude.Count; i++)
            {
                
                
                distancematrix.Add(new List<Double>());
                for (int j = 0; j < latitude.Count; j++)
                {
                    if (i < j)
                    {
                        points.Enqueue(new PointD(latitude[i], longitude[i], i, latitude[j], longitude[j], j));
                    }
                        distancematrix[i].Add(100000);
                }
            }
            latitude.RemoveAt(0);
            longitude.RemoveAt(0);
        }

        public bool dooptimization()
        {
            initialcount = points.Count;
            currentpoint = 0;
            finishedthreads = 0;
            bws = new List<BackgroundWorker>();
            for(int i=0;i<100;i++)
            {
                bws.Add(new BackgroundWorker());
                            bws[i] = new BackgroundWorker
                {
                    WorkerReportsProgress = true,
                    WorkerSupportsCancellation = true
                };
                bws[i].DoWork += bw_DoWork;

                bws[i].RunWorkerCompleted += bw_RunWorkerCompleted;
                bws[i].RunWorkerAsync();//pass id???, what else?
                
            }
            while (finishedthreads!=100)
            {

            }
                return true;
        }
        void bw_DoWork(object sender, DoWorkEventArgs e)
        {

            while (points.Count > 0)
            {
                currentpoint++;
                int delta=-1*(points.Count-initialcount)+1;
                this.threadstatus=" - (1/3) Building point to point distance matrix from Mapquest ("+currentpoint+" of "+initialcount+")";
                PointD tofind=points.Dequeue();
                Map.Directions directions = new Map.Directions(GlobalVar.sqlsettings.mapquestkey);
                List <double> latitude=new List<double>();
                List <double> longitude=new List<double>();
                latitude.Add(tofind.RX);
                longitude.Add(tofind.RY);
                latitude.Add(tofind.CX);
                longitude.Add(tofind.CY);
                DirectionData result=directions.GetDirections(latitude,longitude,false);
                this.distancematrix[tofind.RZ][tofind.CZ] = result.time[0];
            }
        }

        void bw_RunWorkerCompleted(object sender,
                           RunWorkerCompletedEventArgs e)
        {
            finishedthreads++;
        }
    }
}
