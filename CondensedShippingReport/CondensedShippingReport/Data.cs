﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CondensedShippingReport
{
    //`INV_SALES_AMOUNT`, `INV_SHIPPING`, `INV_TAX`,`INV_TOTAL`,`INV_COST`
    public class InvoiceCost
    {
        public int salesamount;
        public int shipping;
        public int tax;
        public int total;
        public int cost;
        public int invoicenumber;
        public InvoiceCost(int invoicenumber,int salesamount,int shipping, int tax,int total, int cost)
        {
            this.invoicenumber = invoicenumber;
            this.salesamount=salesamount;
            this.shipping=shipping;
            this.tax=tax;
            this.total=total;
            this.cost=cost;
        }
    }

    public class ShippingLog
    {
        public int id;
        public DateTime date;
        public string driver;
        public int invoicenumber;
        public int order;
        public string customer;
        public ShippingLog(int id, DateTime date, string driver, int invoicenumber, int order, string customer)
        {
            this.id = id;
            this.date = date;
            this.driver = driver;
            this.invoicenumber = invoicenumber;
            this.order = order;
            this.customer = customer;
        }
    }

    public class Invoice
    {
        public int number;
        public int value;
        public int shipping;
        public int tax;
        public int invtotal;
        public int invcost;
        public DateTime due;
        public string customername;
        public string addr1;
        public string addr2;
        public string city;
        public string state;
        public string zip;
        public double longitude;
        public double latitude;
        public DateTime delivered;
        public Invoice(int number, System.Decimal value1, System.Decimal shipping, System.Decimal tax, System.Decimal invtotal, System.Decimal invcost, DateTime due, string customername, string addr1, string addr2, string city, string state, string zip, double longitude, double latitude, DateTime delivered)
        {
            this.number = number;

            this.value = (int)value1;
            this.due = due;
            this.customername = customername;
            this.addr1 = addr1;
            this.addr2 = addr2;
            this.city = city;
            this.state = state;
            this.zip = zip;
            this.longitude = longitude;
            this.latitude = latitude;
            this.delivered = delivered;
            this.shipping = (int)shipping;
            this.tax = (int)tax;
            this.invtotal = (int)invtotal;
            this.invcost = (int)invcost;
        }
    }
    public class Settings
    {
        public string RRSaddress;
        public double RRSlatitude;
        public double RRSlongitude;
        public string mapquestkey;
        public string RRSLinesFile;
        public string RRSHeaderFile;
        public string passwordhash;
        public Settings(string address, double latitude, double longitude, string key, string linesfile, string headerfile, string hash)
        {
            this.RRSaddress = address;
            this.RRSlatitude = latitude;
            this.RRSlongitude = longitude;
            this.mapquestkey = key;
            this.RRSLinesFile = linesfile;
            this.RRSHeaderFile = headerfile;
            this.passwordhash = hash;
        }

    }
    public class LineItem
    {
        public int invoicenumber;
        public int lineitemnumber;
        public string description;
        public int value;
        public LineItem(int invoicenumber, int lineitemnumber, string description, System.Decimal value)
        {
            this.invoicenumber = invoicenumber;
            this.lineitemnumber = lineitemnumber;
            this.description = description;
            this.value = (int)value;
        }
    }
    class RRSDataInvoices
    {
        public List<string> invoicenumber;
        public List<string> companyname;
        public List<string> address1;
        public List<string> address2;
        public List<string> city;
        public List<string> zip;
        public List<string> state;
        public List<Decimal> value;
        public List<DateTime> date;
        public RRSDataInvoices()
        {
            companyname = new List<string>(); ;
            address1 = new List<string>();
            address2 = new List<string>();
            city = new List<string>();
            zip = new List<string>();
            state = new List<string>();
            value = new List<Decimal>();
            date = new List<DateTime>();
            invoicenumber = new List<string>();
        }
        //invoice, name,address info,delivery date,value
    }
    public class RouteInfo
    {
        public int miles;
        public int number;
        public DateTime date;
        public int drivernumber;
        public List<InvoiceRouteInfo> invoicerouteinfo;
        public RouteInfo(int number, DateTime date, int drivernumber, int miles)
        {
            invoicerouteinfo = new List<InvoiceRouteInfo>();
            this.number = number;
            this.date = date;
            this.drivernumber = drivernumber;
            this.miles = miles;
        }
    }
    public class InvoiceRouteInfo
    {
        public int invoicenumber;
        public int routenumber;
        public int stop;
        public InvoiceRouteInfo(int invoicenumber, int routenumber, int stop)
        {
            this.invoicenumber = invoicenumber;
            this.routenumber = routenumber;
            this.stop = stop;
        }
    }
    public class DriverInfo
    {
        public int number;
        public string name;
        public bool flat;
        public bool trailer;
        public int hour_rate;
        public int overtime_rate;
        public int fuel_surch;
        public int maint_surch;
        public string notes;
        public DriverInfo(int number, string name, bool flat, bool trailer, int hour_rate, int overtime_rate, int fuel_surch, int maint_surch, string notes)
        {
            this.number = number;
            this.name = name;
            this.flat = flat;
            this.trailer = trailer;
            this.hour_rate = hour_rate;
            this.overtime_rate = overtime_rate;
            this.fuel_surch = fuel_surch;
            this.maint_surch = maint_surch;
            this.notes = notes;
        }

    }
    public class DataFormat
    {
        public static string FormatMoneyToString(int input)
        {
            bool flag = false;
            if (input < 0)
            {
                input = input * -1;
                flag = true;
            }

            string output = "$";
            int dollars = 0;
            int cents = 0;
            if (input / 100 > 1)
            {
                dollars = input / 100;
            }
            cents = input - dollars * 100;
            if (dollars > 0)
            {

                output = output + dollars.ToString("#,##0");
            }
            output = output + ".";
            if (cents < 10)
            {
                output = output + "0" + cents.ToString();
            }
            else
            {
                output = output + cents.ToString();
            }
            if (flag == true)
            {
                output = "-" + output;
            }
            return (output);

        }
        public static int FormatMoneyToInt(string input)
        {
            int output = 0;
            int cents = 0;
            input = input.Replace("$", "");
            input = input.Replace(",", "");
            string[] temp = input.Split('.');
            temp[0] = "0" + temp[0];
            int dollars = Int32.Parse(temp[0]);
            if (temp.Length > 1)
            {
                cents = Int32.Parse(temp[1]);
            }
            output = dollars * 100 + cents;
            return (output);
        }
        public static string FormatTimeToString(long time)//seconds
        {

            long hours = time / 3600;
            long minutes = (time - hours * 3600) / 60;
            return (hours + " H " + minutes + " M");
        }
        public static string FormatTimeToString2(long time)//seconds
        {

            long hours = time / 3600;
            long minutes = (time - hours * 3600) / 60;
            long seconds = (time - hours * 3600 - minutes * 60);
            if (hours > 0)
            {
                return (hours + " H " + minutes + " M");
            }
            else
            {
                string extrazero = "0";
                if (minutes > 9)
                {
                    extrazero = "";
                }

                if (seconds > 9)
                {
                    return (extrazero + minutes + ":" + seconds);
                }
                else
                {
                    return (extrazero + minutes + ":0" + seconds);
                }
            }
        }
        public static string GetSha1(string value)
        {
            var data = Encoding.ASCII.GetBytes(value);
            var hashData = new SHA1Managed().ComputeHash(data);

            var hash = string.Empty;

            foreach (var b in hashData)
                hash += b.ToString("X2");

            return hash;
        }

    }
}
