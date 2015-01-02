using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using ShippingLog;

namespace hourLogger
{
    public class RRSDataReader
    {
        
        MySQL_Invoices mysql_invoices;
        string invoicefile;
        string linefile;
        //public RRSDataInvoices rawinvoices;
        public List<Invoice> rawinvoices;
        public List<LineItem> rawlineitems;
        public RRSDataReader(string invoiceinputfile,string lineinputfile)
        {
            this.invoicefile = invoiceinputfile;
            this.linefile = lineinputfile;
            rawinvoices = new List<Invoice>();// RRSDataInvoices();
            rawlineitems=new List<LineItem>();
        }
        public void ReadInvoices(DateTime date)
        {
            // TODO: Set date value.
            
            OleDbConnection dbConn = new OleDbConnection(@"Provider=vfpoledb.1;Data Source="+invoicefile+";Collating Sequence=general");
            try
            {

            // Initialize database connection.

            dbConn.Open();

                // Open connection.
                OleDbCommand command= new OleDbCommand("select INVNO,NAME,ADD1,ADD2,CITY,STATE,ZIP,SNAME,SADD1,SADD2,SCITY,SSTATE,SZIP,WHEN_REQ,SUBTOTAL,SHIPVIA,ORDDATE,SHIPPING,STATETAX,LOCALTAX,INVTOTAL,INVCOST  from oehead.dbf",dbConn);
                Invoice invoicetoadd;
                OleDbDataReader reader= command.ExecuteReader();
                

                while (reader.Read())
                {
                    
                    string raw = (string)reader[13];
                    string[] dates = raw.Split('/');
                    if (raw.Length > 10)
                    {
                        raw = raw.Remove(10);
                    }
                    try
                    {
                        
                        
                        int[] dates_int = new int[3];
                        if (dates.Length >= 3)
                        {
                            dates[0] = dates[0].Trim();
                            dates[1] = dates[1].Trim();
                            dates[2] = dates[2].Trim();
                            dates[2] = dates[2].Substring(0, 4);
                            if (dates[0][0] == '0')
                            {
                                dates[0] = dates[0].Remove(0, 1);
                            }
                            if (dates[1][0] == '0')
                            {
                                dates[1] = dates[1].Remove(0, 1);
                            }

                        }
                        else { dates = new string[3]; dates[0] = "01"; dates[1] = "01"; dates[2] = "1980"; }
                    }
                    catch { }
                 //   if (dates[0] == date.Month.ToString() && dates[1] == date.Day.ToString() && dates[2] == date.Year.ToString())
                 //   {
                   
                  
                   // if ((string)reader[2] != "                                   " || (string)reader[8] != "                                   ")
                   // {

                        DateTime due = new DateTime();
                        try
                        {
                        String duestring=dates[0]+"/"+dates[1]+"/"+dates[2];

                        
                        String[] duearray = duestring.Split(' ');
                        
                        
                            due = DateTime.Parse(duearray[0]);
                        }
                        catch { continue; }
                        try{
                        DateTime order = (DateTime)reader[16];
                        int dueint = due.Year * 12 + due.Month;
                        int orderint = order.Year * 12 + order.Month;
                       // if (!((dueint - orderint) > 2) && !((dueint - orderint) < -2))
                      //  {
                        //if (!reader[15].ToString().Contains("WILL CALL"))
                        //{
                            if ((string)reader[11].ToString().Trim() == "" || (string)reader[7].ToString().Substring(0, 4) == "SAME")
                            {
                                invoicetoadd = new Invoice(Int32.Parse((string)reader[0]), System.Decimal.Multiply((System.Decimal)reader[14], 100), System.Decimal.Multiply((System.Decimal)reader[17], 100), System.Decimal.Multiply(  (Decimal)reader[18]+(System.Decimal)reader[19], 100), System.Decimal.Multiply((System.Decimal)reader[20], 100), System.Decimal.Multiply((System.Decimal)reader[21], 100), due, (string)reader[1], (string)reader[2], (string)reader[3], (string)reader[4], (string)reader[5], (string)reader[6], 0, 0, DateTime.Now.AddYears(1));

                            }
                            else
                            {
                                invoicetoadd = new Invoice(Int32.Parse((string)reader[0]), System.Decimal.Multiply((System.Decimal)reader[14], 100), System.Decimal.Multiply((System.Decimal)reader[17], 100), System.Decimal.Multiply((Decimal)reader[18]+(System.Decimal)reader[19], 100), System.Decimal.Multiply((System.Decimal)reader[20], 100), System.Decimal.Multiply((System.Decimal)reader[21], 100), due, (string)reader[1], (string)reader[8], (string)reader[9], (string)reader[10], (string)reader[11], (string)reader[12], 0, 0, DateTime.Now.AddYears(1));

                            }

                            invoicetoadd.customername = invoicetoadd.customername.Trim();
                            invoicetoadd.addr1 = invoicetoadd.addr1.Trim();
                            invoicetoadd.addr2 = invoicetoadd.addr2.Trim();
                            invoicetoadd.addr1=invoicetoadd.addr1.Replace('#', ' ');
                            invoicetoadd.addr2=invoicetoadd.addr2.Replace('#', ' ');
                            invoicetoadd.city = invoicetoadd.city.Trim();
                            invoicetoadd.state = invoicetoadd.state.Trim();
                            invoicetoadd.zip = invoicetoadd.zip.Trim();
                            int i = Math.Abs(invoicetoadd.number);
                            while (i >= 10)
                                i /= 10;
                            if(i!=8)
                            {
                            rawinvoices.Add(invoicetoadd);
                            }
                        //}
                    //}else{
                    //}
                    }
                        catch{}
                    //}
                    //else
                    //{

                   // }
                //}
                }
            }
            catch { }

                // Close connection.
                dbConn.Close();
 
        }
        public void ReadLineItems()
        {
            OleDbConnection dbConn = new OleDbConnection(@"Provider=vfpoledb.1;Data Source="+linefile+";Collating Sequence=general");
            try
            {
                dbConn.Open();

                // Open connection.
                OleDbCommand command = new OleDbCommand("select INVNO,DESCRIPT,COST from oelines.dbf", dbConn);
                LineItem lineitemtoadd;
                OleDbDataReader reader= command.ExecuteReader();
                while (reader.Read())
                {
                    lineitemtoadd = new LineItem(Int32.Parse((string)reader[0]), 0, (string)reader[1].ToString().Trim(), System.Decimal.Multiply((System.Decimal)reader[2],100));
                    rawlineitems.Add(lineitemtoadd);
                }
                dbConn.Close();
            }
            catch
            {
            }
        }
        public List<LineItem> FilterLineItems(List<Invoice> invoices)
        {
            try
            {
                List<LineItem> filteredlineitems = new List<LineItem>();
                for (int i = 0; i < invoices.Count; i++)
                {
                    int k = 1;
                    for (int j = 0; j < rawlineitems.Count; j++)
                    {
                        if (invoices[i].number == rawlineitems[j].invoicenumber)
                        {
                            filteredlineitems.Add(new LineItem(rawlineitems[j].invoicenumber,k,rawlineitems[j].description,rawlineitems[j].value));
                            k++;
                        }
                    }
                }
                return (filteredlineitems);
            }
            catch
            {
                return (null);
            }
        }
       public List<Invoice> FilterInvoices(DateTime date,List<int> invoice)//do one or the other (default invoice)
        {
            try
            {
                
                List<Invoice> filteredinvoices = new List<Invoice>();

                    mysql_invoices = new MySQL_Invoices(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
                    List<Invoice> blockedinvoices= mysql_invoices.GetBlocks(date);
                    for (int i = 0; i < rawinvoices.Count; i++)
                    {
                        if ((rawinvoices[i].due.Day == date.Day && rawinvoices[i].due.Month == date.Month && rawinvoices[i].due.Year == date.Year))
                        {
                            filteredinvoices.Add(rawinvoices[i]);
                            if (blockedinvoices != null)
                            {
                                for (int j = 0; j < blockedinvoices.Count; j++)
                                {
                                    if (rawinvoices[i].number == blockedinvoices[j].number)
                                    {
                                        filteredinvoices.RemoveAt(filteredinvoices.Count - 1);
                                    }
                                }
                            }
                        }
                        else 
                        {
                            for (int j = 0; j < invoice.Count; j++)
                            {
                                if (rawinvoices[i].number == invoice[j])
                                {
                                    filteredinvoices.Add(rawinvoices[i]);
                                }
                            }
                        }
                    }

                        return (filteredinvoices);
                
            }
            catch
            {
                return null;
            }
        }
        
    }
}
