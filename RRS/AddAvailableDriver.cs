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
    public partial class AddAvailableDriver : Form
    {
        MySQL_Drivers mysql_drivers;
        public AddAvailableDriver()
        {
            this.TopLevel = true;
            
            this.MaximizeBox = false;
            
            InitializeComponent();
            truckType.DropDownStyle = ComboBoxStyle.DropDownList;
            toolStripStatusLabel1.Text = "";
            truckType.Items.Clear();
            truckType.Items.Add("Flat");
            truckType.Items.Add("Trailer");
            truckType.SelectedIndex = 0;
            name.Text = "";
            hourlyRate.Text = "";
            overtimeRate.Text = "";
            maitenanceSurcharge.Text = "";
            fuelSurcharge.Text = "";
            mysql_drivers = new MySQL_Drivers(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
            name.TabIndex = 0;
            hourlyRate.TabIndex = 1;
                overtimeRate.TabIndex=2;
            fuelSurcharge.TabIndex=3;
            maitenanceSurcharge.TabIndex=4;
            driverNotes.TabIndex = 5;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            new ConfigureAvaiableDrivers().Show();
            this.Close();
        }

        private void AddDriver_Click(object sender, EventArgs e)
        {
            if (name.Text.Length == 0)
            {
                toolStripStatusLabel1.Text = "Please specify a name.";
                return;
            }
            string pattern = @"^\$?\-?([1-9]{1}[0-9]{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))$|^\-?\$?([1-9]{1}\d{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))$|^\(\$?([1-9]{1}\d{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))\)$";


            if (!Regex.IsMatch(hourlyRate.Text, pattern))
            {

                toolStripStatusLabel1.Text = "Hourly rate must be a U.S. dollar amount (dollar symbol is optional).";
                return;
            }
            if (!Regex.IsMatch(overtimeRate.Text, pattern))
            {

                toolStripStatusLabel1.Text = "Overtime rate must be a U.S. dollar amount (dollar symbol is optional).";
                return;
            }
            if (!Regex.IsMatch(fuelSurcharge.Text, pattern))
            {

                toolStripStatusLabel1.Text = "Fuel surcharge must be a U.S. dollar amount (dollar symbol is optional).";
                return;
            }
            if (!Regex.IsMatch(maitenanceSurcharge.Text, pattern))
            {

                toolStripStatusLabel1.Text = "Maitenance surcharge must be a U.S. dollar amount (dollar symbol is optional).";
                return;
            }
            bool flatflag=false;
            bool trailerflag=false;
            if (truckType.SelectedIndex == 0)
            {
                flatflag = true;
            }
            if (truckType.SelectedIndex == 1)
            {
                trailerflag = true;
            }
            int hourrate = DataFormat.FormatMoneyToInt(hourlyRate.Text);
            int overrate = DataFormat.FormatMoneyToInt(overtimeRate.Text);
            int fuelsurch = DataFormat.FormatMoneyToInt(fuelSurcharge.Text);
            int maintsurch = DataFormat.FormatMoneyToInt(maitenanceSurcharge.Text);
            DriverInfo drivertoadd = new DriverInfo(0, name.Text, flatflag, trailerflag, hourrate, overrate, fuelsurch, maintsurch, driverNotes.Text);
            mysql_drivers.AddDriverInfo(drivertoadd);
            new ConfigureAvaiableDrivers().Show();
            this.Close();
        }

        private void hourlyRate_Leave(object sender, EventArgs e)
        {
            
            string pattern = @"^\$?\-?([1-9]{1}[0-9]{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))$|^\-?\$?([1-9]{1}\d{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))$|^\(\$?([1-9]{1}\d{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))\)$";

            if (!Regex.IsMatch(hourlyRate.Text, pattern))
            {
                hourlyRate.Focus();


                toolStripStatusLabel1.Text = "Hourly rate must be a U.S. dollar amount (dollar symbol is optional).";
            }
        }
        private void overtimeRate_Leave(object sender, EventArgs e)
        {

            string pattern = @"^\$?\-?([1-9]{1}[0-9]{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))$|^\-?\$?([1-9]{1}\d{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))$|^\(\$?([1-9]{1}\d{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))\)$";

            if (!Regex.IsMatch(overtimeRate.Text, pattern))
            {
                overtimeRate.Focus();


                toolStripStatusLabel1.Text = "Hourly rate must be a U.S. dollar amount (dollar symbol is optional).";
            }
        }
        private void maitenanceSurcharge_Leave(object sender, EventArgs e)
        {

            string pattern = @"^\$?\-?([1-9]{1}[0-9]{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))$|^\-?\$?([1-9]{1}\d{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))$|^\(\$?([1-9]{1}\d{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))\)$";

            if (!Regex.IsMatch(maitenanceSurcharge.Text, pattern))
            {
                maitenanceSurcharge.Focus();


                toolStripStatusLabel1.Text = "Maitenance surcharge must be a U.S. dollar amount (dollar symbol is optional).";
            }
        }
        private void fuelSurcharge_Leave(object sender, EventArgs e)
        {

            string pattern = @"^\$?\-?([1-9]{1}[0-9]{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))$|^\-?\$?([1-9]{1}\d{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))$|^\(\$?([1-9]{1}\d{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))\)$";

            if (!Regex.IsMatch(fuelSurcharge.Text, pattern))
            {
                fuelSurcharge.Focus();


                toolStripStatusLabel1.Text = "Fuel surcharge must be a U.S. dollar amount (dollar symbol is optional).";
            }
        }

        private void overtimeRate_TextChanged(object sender, EventArgs e)
        {

        }










    }
}
