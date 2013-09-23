﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Web;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Map
{
    class MapWriter
    {
        public List<string> colors;
        public MapWriter()
        {
            colors=new List<string>();
            colors.Add("blue");
            colors.Add("green");
            colors.Add("orange");
            colors.Add("purple");
            colors.Add("white");
            colors.Add("yellow");
            colors.Add("pink");
            colors.Add("#000000");
            colors.Add("#A0522D");
            colors.Add("#8B0000");
            colors.Add("#00FF00");
            colors.Add("#191970");
        }

        public float[] CalcBoundingBox(float [][]boundingboxes)
        {
            float []boundingbox=new float[4];
            boundingbox[0] = 9999;
            boundingbox[1] = 9999;
            boundingbox[2] = -9999;
            boundingbox[3] = -9999;

            for (int i = 0; i<boundingboxes.Length;i++ )
            {
                if (boundingboxes[i][0] < boundingbox[0])
                {
                    boundingbox[0] = boundingboxes[i][0];
                }
                if (boundingboxes[i][1] < boundingbox[1])
                {
                    boundingbox[1] = boundingboxes[i][1];
                }
                if (boundingboxes[i][2] > boundingbox[2])
                {
                    boundingbox[2] = boundingboxes[i][2];
                }
                if (boundingboxes[i][3] > boundingbox[3])
                {
                    boundingbox[3] = boundingboxes[i][3];
                }
            }
                return (boundingbox);
        }

        public void WriteMap(int numberofroutes, List<int> colorsin,float []boundingbox,List<float> []routeshape,List<double> []points_latitude,List<double> []points_longitude,List <int>[]locationsequence,int width,int height)
        {
            List<string> lines = new List<string>();
            lines.Add("<html>");
            lines.Add("<head>");
            lines.Add("<script src=\"http://open.mapquestapi.com/sdk/js/v7.0.s/mqa.toolkit.js\"></script>");
            lines.Add("<script type=\"text/javascript\">");
            lines.Add("MQA.EventUtil.observe(window, 'load', function() {");

            //draw map
            lines.Add("window.map = new MQA.TileMap(document.getElementById('map'),1,{lat:41.878114, lng:-87.629798},'map');");

            //add controls
            lines.Add("MQA.withModule('largezoom', function() {");
            lines.Add(" map.addControl(new MQA.LargeZoom(), new MQA.MapCornerPlacement(MQA.MapCorner.TOP_LEFT, new MQA.Size(5,5)));");
            lines.Add("});");

            //draw points
            if (points_latitude != null && points_longitude != null)
            {
                int penalty = 0;
                int penalty2 = 0;
                for (int i = 0; (i < numberofroutes) && (i < 12); i++)//limit to seven routes
                {
                    penalty = 0;
                    for(int j=0;j<(points_latitude[i].Count);j++)
                    {
                    int total = i + j;
                    int k = j;
                    if (k == points_latitude[i].Count - 1 && locationsequence != null)
                    {
                        k = 0;
                    }
                    else
                    {
                        if (locationsequence != null)
                        {

                            if (locationsequence[i][j] > 0)
                            {
                                if (points_latitude[i][locationsequence[i][j] - 1] == points_latitude[i][locationsequence[i][j]])
                                {
                                    penalty++;
                                }
                                else
                                {
                                    if (penalty > 0)
                                    {
                                      //  penalty2++;
                                    }
                                   // penalty = 0;

                                }
                            }
                            k = locationsequence[i][j] - penalty - penalty2;
                        }
                    }
                    if (locationsequence == null)
                    {
                        k+= 1;
                    }
                    lines.Add("var info" + total.ToString() + "=new MQA.Poi({lat:" + points_latitude[i][j].ToString() + ", lng:" + points_longitude[i][j].ToString() + "});");
                    if (colorsin == null)
                    {
                        if (i < 7)
                        {
                            lines.Add("var customIcon" + total.ToString() + "=new MQA.Icon('http://www.mapquestapi.com/staticmap/geticon?uri=poi-" + colors[i] + "_1-" + (k).ToString() + ".png',20,29);");
                        }
                        else
                        {
                            lines.Add("var customIcon" + total.ToString() + "=new MQA.Icon('" + colors[i].Replace("#", "%23") + "/" + (k).ToString() + ".png',20,29);");
                        }
                    }
                    else
                    {
                        if (i < 7)
                        {
                            lines.Add("var customIcon" + total.ToString() + "=new MQA.Icon('http://www.mapquestapi.com/staticmap/geticon?uri=poi-" + colors[colorsin[i]] + "_1-" + (k).ToString() + ".png',20,29);");
                        }
                        else
                        {
                            lines.Add("var customIcon" + total.ToString() + "=new MQA.Icon('" + colors[colorsin[i]].Replace("#", "%23") + "/" + (k).ToString() + ".png',20,29);");
                        }
                    }
                    lines.Add("info"+total.ToString()+".setIcon(customIcon"+total.ToString()+");");
                    lines.Add("map.addShape(info" + total.ToString() + ")");
                    }
                }
            }
            //draw route
            if (routeshape != null)
            {
                
                for (int i = 0; i < numberofroutes; i++)
                {
                    lines.Add(" MQA.withModule('shapes', function() {");
                    lines.Add("var line = new MQA.LineOverlay();");
                    if (colorsin == null)
                    {
                        lines.Add("line.color='" + colors[i] + "'");
                    }
                    else
                    {
                        lines.Add(("line.color='" + colors[colorsin[i]] + "'"));
                    }
                    lines.Add("line.setShapePoints([");
                    string routeshapestring = "";
                    for (int j = 0; j < routeshape[i].Count; j++)
                    {
                        if (j != 0)
                        {
                            routeshapestring = routeshapestring + ",";
                        }
                        routeshapestring = routeshapestring + routeshape[i][j].ToString();
                    }
                    lines.Add(routeshapestring);
                    lines.Add("]);");
                    lines.Add("map.addShape(line);");
                    lines.Add("});");
                }
            }
            //do zoom size based on zoomrect
            if (boundingbox != null)
            {
                lines.Add("ul=new MQA.LatLng(" + boundingbox[0].ToString() + "," + boundingbox[1].ToString() + ");");
                lines.Add("lr=new MQA.LatLng(" + boundingbox[2].ToString() + "," + boundingbox[3].ToString() + ");");
                lines.Add("var zoomrect=new MQA.RectLL(ul,lr);");
                lines.Add("map.zoomToRect(zoomrect);");
                
            }
            else
            {
                lines.Add("map.bestFit()");
                //lines.Add("map.setZoomLevel(map.getZoomLevel() + 1)");
            }

            lines.Add("});");
            lines.Add("</script>");
            lines.Add("</head>");
            lines.Add("<body bgcolor=\"F0F0F0\"><div id='map' style='width:"+width+"px; height:"+height+"px;'></div></body>");

            lines.Add("</html>");
            StreamWriter file = new StreamWriter("map.html");
            
            for (int i = 0; i<lines.Count; i++)
            {
                file.WriteLine(lines[i]);
            }
            file.Close();
        }

    }

    class DirectionData
    {
        public List<double> distance;
        public List <long> time;
        public string sessionId;
        public List<int> locationSequence;
        public float []rectangle;
        public List<float> routeshape;
        public List<double> latitude;
        public List<double> longitude;
        public DirectionData()
        {
            distance=new List<double>();
            time=new List<long>();
            locationSequence = new List<int>();
            routeshape = new List<float>();
            latitude = new List<double>();
            longitude = new List<double>();
        }
    }

    class Directions
    {
        string apikey;
        public Directions(string key)
        {
            apikey = key;
        }
        /*
         *  0(double)distance
            1(long)time
            2(string)sessionId
            3(int[])locationSequence
         *  4rectangle (ul_lat,ul_long,lr_lat,lr_long)
         * 5List<float>routeshape
         * 6List<double> latitude
         * 7List<double> longitude
         */



        public DirectionData GetDirections(List<double> latitude, List<double> longitude,bool optimize)
        {

            // used to build entire input
            StringBuilder sb = new StringBuilder();
            
            // used on each read operation
            byte[] buf = new byte[8192];
            string webrequest="http://open.mapquestapi.com/directions/v1/";
            if (optimize == false)
            {
                webrequest = webrequest + "route";
            }
            else
            {
                webrequest = webrequest + "optimizedroute";
            }
            webrequest = webrequest + "?json={mapState: { center: {lng: 87.629,lat: 41.878},scale:1504475,height: 3000,width: 4000},locations:[";
            for(int i=0;i<latitude.Count;i++)
            {
                webrequest = webrequest + "{latLng:{lat:";
                webrequest = webrequest + latitude[i].ToString();
                webrequest = webrequest + ",lng:";
                webrequest=webrequest+longitude[i].ToString();
                webrequest=webrequest+"}},";
            }
            webrequest=webrequest+"]}";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(webrequest);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream resStream = response.GetResponseStream();
            string tempString = null;
            int count = 0;

            do
            {
                // fill the buffer with data
                count = resStream.Read(buf, 0, buf.Length);

                // make sure we read some data
                if (count != 0)
                {
                    // translate from bytes to ASCII text
                    tempString = Encoding.ASCII.GetString(buf, 0, count);

                    // continue building the string
                    sb.Append(tempString);
                }
            }
            while (count > 0); // any more data to read?
            string sbcomplete = sb.ToString();
            JObject o = JObject.Parse(sbcomplete);
            DirectionData results= new DirectionData();
            JObject legs;
            if(optimize==false)
            {
                try
                {
                    legs = (JObject)o.First.First.First.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.First.First;
                    //oldlegs = (JObject)o.First.First.First.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.First.First;
                }
                catch
                {
                    sb = new StringBuilder();

                    // used on each read operation
                    buf = new byte[8192];
                    webrequest = "http://www.mapquestapi.com/directions/v1/";

                    webrequest = webrequest + "route";

                    webrequest = webrequest + "?key=" + apikey + "&json={mapState: { center: {lng: 87.629,lat: 41.878},scale:1504475,height: 3000,width: 4000},locations:[";
                    for (int i = 0; i < latitude.Count; i++)
                    {
                        webrequest = webrequest + "{latLng:{lat:";
                        webrequest = webrequest + latitude[i].ToString();
                        webrequest = webrequest + ",lng:";
                        webrequest = webrequest + longitude[i].ToString();
                        webrequest = webrequest + "}},";
                    }
                    webrequest = webrequest + "]}";
                    request = (HttpWebRequest)WebRequest.Create(webrequest);
                    response = (HttpWebResponse)request.GetResponse();
                    resStream = response.GetResponseStream();
                    tempString = null;
                    count = 0;

                    do
                    {
                        // fill the buffer with data
                        count = resStream.Read(buf, 0, buf.Length);

                        // make sure we read some data
                        if (count != 0)
                        {
                            // translate from bytes to ASCII text
                            tempString = Encoding.ASCII.GetString(buf, 0, count);

                            // continue building the string
                            sb.Append(tempString);
                        }
                    }
                    while (count > 0); // any more data to read?
                    sbcomplete = sb.ToString();
                    o = JObject.Parse(sbcomplete);
                    legs = (JObject)o.First.First.First.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.First.First;
                }
            }else{
                try
                {
                                    
                    legs = (JObject)o.First.First.First.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.First.First;
                }
                catch
                {
                    sb = new StringBuilder();

                    // used on each read operation
                    buf = new byte[8192];
                    webrequest = "http://www.mapquestapi.com/directions/v1/";

                    webrequest = webrequest + "optimizedroute";

                    webrequest = webrequest + "?key=" + apikey + "&json={mapState: { center: {lng: 87.629,lat: 41.878},scale:1504475,height: 3000,width: 4000},locations:[";
                    for (int i = 0; i < latitude.Count; i++)
                    {
                        webrequest = webrequest + "{latLng:{lat:";
                        webrequest = webrequest + latitude[i].ToString();
                        webrequest = webrequest + ",lng:";
                        webrequest = webrequest + longitude[i].ToString();
                        webrequest = webrequest + "}},";
                    }
                    webrequest = webrequest + "]}";
                    request = (HttpWebRequest)WebRequest.Create(webrequest);
                    response = (HttpWebResponse)request.GetResponse();
                    resStream = response.GetResponseStream();
                    tempString = null;
                    count = 0;

                    do
                    {
                        // fill the buffer with data
                        count = resStream.Read(buf, 0, buf.Length);

                        // make sure we read some data
                        if (count != 0)
                        {
                            // translate from bytes to ASCII text
                            tempString = Encoding.ASCII.GetString(buf, 0, count);

                            // continue building the string
                            sb.Append(tempString);
                        }
                    }
                    while (count > 0); // any more data to read?
                    sbcomplete = sb.ToString();
                    o = JObject.Parse(sbcomplete);
                    legs = (JObject)o.First.First.First.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.First.First;
                }
            }
            while (legs != null)
            {
                double distancetemp = (double)legs.First.Next.Next.Next.Next.Next;
                long timetemp = (long)legs.First.Next.Next.Next.Next.Next.Next;
                legs = (JObject)legs.Next;
                results.distance.Add(distancetemp);
                results.time.Add(timetemp);
            }
            results.latitude = new List<double>(latitude);
            results.longitude = new List<double>(longitude);
            JObject shapepoints = (JObject)o.First.First;
            JToken shapepoints2 = (JToken)shapepoints.First;
            JToken shapepointspointer;
            if (optimize == false)
            {
                shapepointspointer = (JToken)shapepoints2.Next.Next.Next.Next.First.First.Next.First.First;
            }
            else
            {
                shapepointspointer = (JToken)shapepoints2.Next.Next.Next.First.First.Next.First.First;
            }
                List<float> shape = new List<float>();
            while (shapepointspointer.Next != null)
            {
                shape.Add((float)shapepointspointer);
                shapepointspointer = shapepointspointer.Next;
            }

            results.sessionId=(string)o.First.First["sessionId"];
            List<int> locationsequence = new List<int>();
            JToken locationpointer = (JToken)o.First.First["locationSequence"].First;
            while (locationpointer!=null)
            {
                locationsequence.Add((int)locationpointer);
                locationpointer = locationpointer.Next;
            }
            results.locationSequence=locationsequence;
            float[] rectangle = new float[4];
            rectangle[0]=(float)o.First.First["boundingBox"].First.First.First.Next.First;//upper left latitude
            rectangle[1]=(float)o.First.First["boundingBox"].First.First.First.First;//upper left longitude
            rectangle[2]=(float)o.First.First["boundingBox"].Last.First.Last.First;//lower right latitude
            rectangle[3] = (float)o.First.First["boundingBox"].Last.First.First.First;//lower right longitude
            results.rectangle=rectangle;
            results.routeshape=shape;

            return (results);
        }
    }
    class GeoData
    {
        public List<float> latitude;
        public List<float> longitude;
        public List<string> city;
        public List<string> street;
        public List<string> state;
        public List<string> zip;
        public GeoData()
        {
            latitude = new List<float>();
            longitude = new List<float>();
            city = new List<string>();
            street = new List<string>();
            state = new List<string>();
            zip = new List<string>();
        }
    }
    class Mapquest
    {
        

        string apikey;
        
        Hashtable cache;

        public Mapquest(string key)
        {
            cache= new Hashtable();
            
            apikey = key;
        }
        /*
         *return GeoData
         * 
         */
        public GeoData GetLocation(string address)
        {
            address.Replace('#',' ');
            GeoData location = new GeoData();
            List<float> longitude;
            if(cache.ContainsKey(address))
            {
                return(GeoData)cache[address];
            }
            string[] possiblelocations = new string[1];
            // used to build entire input
            StringBuilder sb = new StringBuilder();
            
            // used on each read operation
            byte[] buf = new byte[8192];

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.mapquestapi.com/geocoding/v1/address?key="+apikey+"&location="+address+"&maxResults=5");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream resStream = response.GetResponseStream();
            string tempString = null;
            int count = 0;
             
            do
            {
                // fill the buffer with data
                count = resStream.Read(buf, 0, buf.Length);

                // make sure we read some data
                if (count != 0)
                {
                    // translate from bytes to ASCII text
                    tempString = Encoding.ASCII.GetString(buf, 0, count);

                    // continue building the string
                    sb.Append(tempString);
                }
            }
            while (count > 0); // any more data to read?
            string sbcomplete=sb.ToString();
            try
            {
                JObject o = JObject.Parse(sbcomplete);
                JObject nextlocation = (JObject)o.First.First.First.First.First.First;
                List<string> street = new List<string>();
                List<string> city = new List<string>();
                List<string> state = new List<string>();
                List<string> zip = new List<string>();
                longitude = new List<float>();
                List<float> latitude = new List<float>();
                while (nextlocation != null)
                {
                    longitude.Add((float)nextlocation.First.First.First.First);
                    latitude.Add((float)nextlocation.First.First.Last.First);
                    city.Add((string)nextlocation.First.Next.Next.Next.Next.First);
                    street.Add((string)nextlocation.First.Next.Next.Next.Next.Next.First);
                    state.Add((string)nextlocation.First.Next.Next.Next.Next.Next.Next.Next.First);
                    zip.Add((string)nextlocation.First.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.Next.First);
                    nextlocation = (JObject)nextlocation.Next;
                }
                if (latitude[0] >= 39.527 && latitude[0] <= 39.528 && longitude[0] <= -99.141 && longitude[0] >= -99.142)
                {
                    return (null);
                }
                location.latitude = latitude;
                location.longitude = longitude;
                location.city = city;
                location.street = street;
                location.state = state;
                location.zip = zip;
                cache.Add(address, location);
            }
            catch
            {
                return (null);
            }
            return(location);

            
        }

    }
}
