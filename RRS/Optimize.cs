﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RRS
{
    class Optimize
    {
        public List<double> xin;
        public List<double> yin;
        List<int> demand;

        //build the set of points from latitude and longitude
        public void buildPoints(double depotlatitude,double depotlongitude,List <double> latitude,List<double> longitude)
        {
            


            //figure out x and ys
            double rvalue=6371.0;
            double kiltomile=.621371192;
            xin = new List<double>();
            yin = new List<double>();
            for (int i = 0; i < latitude.Count; i++)
            {
                double latituderadians1=depotlatitude*(Math.PI/180);
                double longituderadians1=depotlongitude*(Math.PI/180);
                double latituderadians2 = latitude[i] * (Math.PI / 180);
                double longituderadians2 = longitude[i] * (Math.PI / 180);
                double distance=Math.Acos(Math.Sin(latituderadians1)*Math.Sin(latituderadians2)+Math.Cos(latituderadians1)*Math.Cos(latituderadians2)*Math.Cos(longituderadians2-longituderadians1))*rvalue*kiltomile;
                double yvalue=Math.Sin(longituderadians2-longituderadians1)*Math.Cos(latituderadians2);
                double xvalue=Math.Cos(latituderadians1)*Math.Sin(latituderadians2)-Math.Sin(latituderadians1)*Math.Cos(latituderadians2)*Math.Cos(longituderadians2-longituderadians1);
                double bearingrad=Math.Atan2(yvalue,xvalue);
                double bearingdeg=bearingrad*Convert.ToDouble(180/Math.PI);
                bearingdeg=Convert.ToDouble(bearingdeg%360);
                double xcoord=0;
                double ycoord=0;
                if(bearingdeg<0)
                {
                    bearingdeg=bearingdeg+360;
                }
                if(bearingdeg<90)
                {
                    xcoord=Math.Sin(bearingdeg*(Math.PI/180))*distance;
                    ycoord=Math.Cos(bearingdeg*(Math.PI/180))*distance;
                }
                else if(bearingdeg<=180)
                {
                    bearingdeg=bearingdeg-90;
                    xcoord=Math.Cos(bearingdeg*(Math.PI/180))*distance;
                    ycoord=Math.Sin(bearingdeg*(Math.PI/180))*-distance;
                }
                else if(bearingdeg<=270)
                {
                    bearingdeg=bearingdeg-180;
                    xcoord=Math.Sin(bearingdeg*(Math.PI/180))*-distance;
                    ycoord=Math.Cos(bearingdeg*(Math.PI/180))*-distance;
                }
                else if(bearingdeg<=360)
                {
                    bearingdeg=bearingdeg-270;
                    xcoord=Math.Cos(bearingdeg*(Math.PI/180))*-distance;
                    ycoord=Math.Sin(bearingdeg*(Math.PI/180))*distance;

                }
                xin.Add(xcoord);
                yin.Add(ycoord);
            }
            

        }
        public void writeinputfile(int capacity,int distance,List<int> demand)
        {
            this.demand = demand;
            StreamWriter sw = new StreamWriter("input.vrp");
            sw.WriteLine("NAME: input");
            sw.WriteLine("BEST_KNOWN: 0");
            sw.WriteLine("DIMENSION: "+(demand.Count+1).ToString());
            sw.WriteLine("CAPACITY: "+capacity.ToString());
            sw.WriteLine("DISTANCE: " + distance.ToString());
            sw.WriteLine("EDGE_WEIGHT_FORMAT: FUNCTION");
            
            sw.WriteLine("EDGE_WEIGHT_TYPE: EXACT_2D");
            sw.WriteLine("NODE_COORD_TYPE: TWOD_COORDS");
            sw.WriteLine("NODE_COORD_SECTION");
            sw.WriteLine("1 0 0");
            for (int i = 0; i < xin.Count; i++)
            {
                sw.WriteLine((i + 2).ToString() + " " + xin[i].ToString() + " " + yin[i].ToString());
            }
            sw.WriteLine("DEMAND_SECTION");
            sw.WriteLine("1 0");
            for (int i = 0; i < demand.Count; i++)
            {
                sw.WriteLine((i+2).ToString()+" 1");
            }
            sw.WriteLine("DEPOT_SECTION");
            sw.WriteLine("1");
            sw.WriteLine("-1");
            sw.WriteLine("EOF");
            sw.Close();
        }
        public void writeinputfile_matrix(int capacity, int time,List<List<double>> times, List<int> demand)
        {
            this.demand = demand;
            StreamWriter sw = new StreamWriter("input.vrp");
            sw.WriteLine("NAME: input");
            sw.WriteLine("BEST_KNOWN: 0");
            sw.WriteLine("DIMENSION: " + (demand.Count + 1).ToString());
            sw.WriteLine("CAPACITY: " + capacity.ToString());
            sw.WriteLine("DISTANCE: " + time.ToString());


            sw.WriteLine("EDGE_WEIGHT_TYPE: EXPLICIT");
            sw.WriteLine("EDGE_WEIGHT_FORMAT: FULL_MATRIX");
            sw.WriteLine("NODE_COORD_TYPE: TWOD_COORDS");
            sw.WriteLine("NODE_COORD_SECTION");
            sw.WriteLine("1 0 0");
            for (int i = 0; i < xin.Count; i++)
            {
                sw.WriteLine((i + 2).ToString() + " " + xin[i].ToString() + " " + yin[i].ToString());
            }

            sw.WriteLine("EDGE_WEIGHT_SECTION");
            for (int i = 0; i < times.Count; i++)
            {
                for (int j = 0; j < times[i].Count; j++)
                {
                    if (i < j)
                    {
                        sw.Write(times[i][j] + " ");
                    }
                    if (i == j)
                    {
                        sw.Write("0 ");
                    }
                    if (i > j)
                    {
                        sw.Write(times[j][i] + " ");
                    }
                    
                }
                sw.WriteLine("");
            }
            sw.WriteLine("DEMAND_SECTION");

            sw.WriteLine("1 0");
            for (int i = 0; i < demand.Count; i++)
            {
                sw.WriteLine((i + 2).ToString() + " 1");
            }
            sw.WriteLine("DEPOT_SECTION");
            sw.WriteLine("1");
            sw.WriteLine("-1");
            sw.WriteLine("EOF");
            sw.Close();
        }
    }
}
