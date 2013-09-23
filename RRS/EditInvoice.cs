﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
namespace RRS
{
    public partial class EditInvoice : Form
    {
        Invoice inputinvoice;
        Invoice newinvoice;
        List<LineItem> lineitems;
        MySQL_Invoices mysql_invoices;
        MySQL_LineItems mysql_lineitems;
        bool caller;
        DateTime viewdate;
        DriverInfo driverinfo;
        int selection;
        public void SetInvoice(Invoice invoice,bool caller,DateTime date,DriverInfo driver,int selection)
        {
            inputinvoice = invoice;
            this.caller = caller;
            this.viewdate = date;
            this.driverinfo = driver;
            this.selection = selection;
        }
        public EditInvoice()
        {
            this.TopLevel = true;
            
            this.MaximizeBox = false;
            
            InitializeComponent();
 
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Cancel_Click_1(object sender, EventArgs e)
        {

            if (caller == true)
            {
                DeliverySchedule deliveryschedule = new DeliverySchedule();
                deliveryschedule.SetDate(viewdate,this.selection);
                deliveryschedule.Show();
                this.Close();
            }
            else
            {
                ConfigureTruckRoute configureroute = new ConfigureTruckRoute();
                configureroute.SetData(viewdate, driverinfo);
                configureroute.Show();
            }
            this.Close();
        }

        private void button9_Click_1(object sender, EventArgs e)
        {

        }

        private void EditInvoice_Load(object sender, EventArgs e)
        {
            try
            {
                textInvoice.Text = "";
                textValue.Text = "";
                textCustomer.Text = "";
                textStreet.Text = "";
                textCity.Text = "";
                textZip.Text = "";
                textState.Text = "";
                listAddresses.Items.Clear();
                button1.Enabled = false;
                toolStripStatusLabel1.Text = "";
                mysql_invoices = new MySQL_Invoices(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
                mysql_lineitems = new MySQL_LineItems(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
                newinvoice = mysql_invoices.GetInvoice(inputinvoice.number);
                if (newinvoice != null)
                {
                    lineitems = mysql_lineitems.GetLineItems(newinvoice);
                    textInvoice.Text = newinvoice.number.ToString();
                    textValue.Text = DataFormat.FormatMoneyToString(newinvoice.value);
                    if (newinvoice.addr1 != "")
                    {
                        textStreet.Text = newinvoice.addr1;
                    }
                    else
                    {
                        textStreet.Text = newinvoice.addr2;
                    }
                    textCustomer.Text = newinvoice.customername;
                    textCity.Text = newinvoice.city;
                    textZip.Text = newinvoice.zip;
                    textState.Text = newinvoice.state;
                    for (int i = 0; i < lineitems.Count; i++)
                    {
                        string toinsert = lineitems[i].description + " - " + DataFormat.FormatMoneyToString(lineitems[i].value);
                        listAddresses.Items.Add(toinsert);
                    }
                    DatePicker.Value = newinvoice.due;
                    button1.Enabled = true;
                }
                else { Cancel_Click(null, null); }
            }
            catch
            {
                Cancel_Click(null, null);
            }
        }

        private void textValue_Leave(object sender, EventArgs e)
        {
            string pattern = @"^\$?\-?([1-9]{1}[0-9]{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))$|^\-?\$?([1-9]{1}\d{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))$|^\(\$?([1-9]{1}\d{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))\)$";

            if (!Regex.IsMatch(textValue.Text, pattern))
            {
                textValue.Focus();


                toolStripStatusLabel1.Text = "Value must be a U.S. dollar amount (dollar symbol is optional).";
            }
            else
            {
                toolStripStatusLabel1.Text = "";
            }
        }

        private void textInvoice_Leave(object sender, EventArgs e)
        {
            string pattern = @"^[0-9]+$";
            if (!Regex.IsMatch(textInvoice.Text, pattern))
            {
                textInvoice.Focus();
                toolStripStatusLabel1.Text = "Invoice number must be a number.";
            }
            else
            {
                toolStripStatusLabel1.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //check blank fields
                if (textInvoice.Text == "" || textValue.Text == "" || textCustomer.Text == "" || textStreet.Text == "" || textCity.Text == "" || textZip.Text == "" || textState.Text == "")
                {
                    toolStripStatusLabel1.Text = "Some required fields are blank.";
                }

                string pattern = @"^\$?\-?([1-9]{1}[0-9]{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))$|^\-?\$?([1-9]{1}\d{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))$|^\(\$?([1-9]{1}\d{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))\)$";
                if (!Regex.IsMatch(textValue.Text, pattern))
                {
                    textValue.Focus();
                    toolStripStatusLabel1.Text = "Value must be a U.S. dollar amount (dollar symbol is optional).";
                    return;
                }
                pattern = @"^[0-9]+$";
                if (!Regex.IsMatch(textInvoice.Text, pattern))
                {
                    textInvoice.Focus();
                    toolStripStatusLabel1.Text = "Invoice number must be a number.";
                    return;
                }
                newinvoice.number = Int32.Parse(textInvoice.Text);
                newinvoice.value = DataFormat.FormatMoneyToInt(textValue.Text);
                newinvoice.customername = textCustomer.Text;
                newinvoice.due = DatePicker.Value;
                newinvoice.addr1 = textStreet.Text;
                newinvoice.addr2="";
                newinvoice.city = textCity.Text;
                newinvoice.zip = textZip.Text;
                newinvoice.state = textState.Text;

                mysql_invoices.UpdateInvoice(newinvoice, inputinvoice);
            }
            catch
            {
                toolStripStatusLabel1.Text = "Failed to update invoice.";
                return;
            }

            if (caller == true)
            {
                DeliverySchedule deliveryschedule = new DeliverySchedule();
                deliveryschedule.SetDate(viewdate,this.selection);
                deliveryschedule.Show();
                this.Close();
            }
            else
            {
                ConfigureTruckRoute configureroute = new ConfigureTruckRoute();
                configureroute.SetData(viewdate, driverinfo);
                configureroute.Show();


            }
            this.Close();
        }

        private void FindLocation_Click(object sender, EventArgs e)
        {
            FindAddress findaddress = new FindAddress();
            findaddress.SetInvoice(inputinvoice, 2, viewdate, null,caller);
            findaddress.Show();
            this.Close();
        }
    }
}
