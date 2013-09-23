using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace RRS
{
    public partial class ConfigureAvaiableDrivers : Form
    {
        MySQL_Drivers mysql_drivers;
        MySQL_Routes mysql_routes;
        List<DriverInfo> drivers;
        bool changeddriverflag;
        
        public ConfigureAvaiableDrivers()
        {
            this.TopLevel = true;
            
            this.MaximizeBox = false;
           
                InitializeComponent();
                truckType.DropDownStyle = ComboBoxStyle.DropDownList;
                toolStripStatusLabel1.Text = "";
                changeddriverflag = false;
                mysql_drivers = new MySQL_Drivers(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
                mysql_routes = new MySQL_Routes(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");

                    drivers = mysql_drivers.GetDrivers();
                    DriverInfo test = mysql_drivers.GetDriverInfo(1);
                    if (drivers == null)
                    {
                        this.Close();
                    }
                    else
                    {
                        driversList.Items.Clear();
                        for (int i = 0; i < drivers.Count; i++)
                        {
                            driversList.Items.Add(drivers[i].name);
                            
                        }
                        driversList.SelectedIndex = 0;

                    }
                    ApplyEdits.Enabled = false;
                    name.TabIndex = 0;
                    hourlyRate.TabIndex = 1;
                    overtimeRate.TabIndex = 2;
                    fuelSurcharge.TabIndex = 3;
                    maitenanceSurcharge.TabIndex = 4;
                    driverNotes.TabIndex = 5;
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.OpenForms[0].Controls[0].Enabled = true;
            Application.OpenForms[0].Controls[1].Enabled = true;
            Application.OpenForms[0].Controls[2].Enabled = true;
            Application.OpenForms[0].Controls[3].Enabled = true;
            this.Close();
        }

        private void AddDriver_Click(object sender, EventArgs e)
        {

            new AddAvailableDriver().Show();
            this.Close();
        }

        private void driversList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyEdits.Enabled = false;
            List <DriverInfo> newdrivers = mysql_drivers.GetDrivers();
            if (newdrivers.Count != drivers.Count)
            {
                driversList.Items.Clear();
                drivers = newdrivers;
                for (int i = 0; i < drivers.Count; i++)
                {
                    driversList.Items.Add(drivers[i].name);

                }
                driversList.SelectedIndex = 0;

                return;
            }

            DriverInfo updateinfo = mysql_drivers.GetDriverInfo(drivers[driversList.SelectedIndex].number);
            drivers[driversList.SelectedIndex] = updateinfo;

            changeddriverflag = true;
            truckType.Items.Clear();
            truckType.Items.Add("Flat");
            truckType.Items.Add("Trailer");
            if (updateinfo.trailer == true)
            {
                truckType.SelectedIndex = 1;
            }
            if (updateinfo.flat == true)
            {
                truckType.SelectedIndex = 0;
            }
            
            driverNotes.Text = updateinfo.notes;
            maitenanceSurcharge.Text = DataFormat.FormatMoneyToString(updateinfo.maint_surch);
            hourlyRate.Text = DataFormat.FormatMoneyToString(updateinfo.hour_rate);
            overtimeRate.Text = DataFormat.FormatMoneyToString(updateinfo.overtime_rate);
            fuelSurcharge.Text = DataFormat.FormatMoneyToString(updateinfo.fuel_surch);
            name.Text = updateinfo.name;
            
        }

        private void ApplyEdits_Click(object sender, EventArgs e)
        {
                ApplyEdits.Enabled = false;
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
                bool flatflag = false;
                bool trailerflag = false;
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
                DriverInfo drivertoupdate = new DriverInfo(drivers[driversList.SelectedIndex].number, name.Text, flatflag, trailerflag, hourrate, overrate, fuelsurch, maintsurch, driverNotes.Text);
                mysql_drivers.UpdateDriverInfo(drivertoupdate);
                new ConfigureAvaiableDrivers().Show();
                this.Close();
        }

        private void truckType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (changeddriverflag == false)
                ApplyEdits.Enabled = true;

        }

        private void hourlyRate_TextChanged(object sender, EventArgs e)
        {
            if (changeddriverflag == false)
                ApplyEdits.Enabled = true;
            
        }

        private void fuelSurcharge_TextChanged(object sender, EventArgs e)
        {
            if (changeddriverflag == false)
                ApplyEdits.Enabled = true;

        }

        private void maitenanceSurcharge_TextChanged(object sender, EventArgs e)
        {
            if (changeddriverflag == false)
                ApplyEdits.Enabled = true;
        }

        private void driverNotes_TextChanged(object sender, EventArgs e)
        {
            if (changeddriverflag == false)
                ApplyEdits.Enabled = true;
        }

        private void overtimeRate_TextChanged(object sender, EventArgs e)
        {
            if (changeddriverflag == false)
                ApplyEdits.Enabled = true;
            
        }

        private void name_TextChanged(object sender, EventArgs e)
        {
                        if(changeddriverflag==true){ changeddriverflag = false; }
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

        private void fuelSurcharge_Leave(object sender, EventArgs e)
        {

            string pattern = @"^\$?\-?([1-9]{1}[0-9]{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))$|^\-?\$?([1-9]{1}\d{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))$|^\(\$?([1-9]{1}\d{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))\)$";

            if (!Regex.IsMatch(fuelSurcharge.Text, pattern))
            {
                fuelSurcharge.Focus();


                toolStripStatusLabel1.Text = "Fuel surcharge must be a U.S. dollar amount (dollar symbol is optional).";
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
        private void overtimeRate_Leave(object sender, EventArgs e)
        {
            string pattern = @"^\$?\-?([1-9]{1}[0-9]{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))$|^\-?\$?([1-9]{1}\d{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))$|^\(\$?([1-9]{1}\d{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))\)$";

            if (!Regex.IsMatch(overtimeRate.Text, pattern))
            {
                overtimeRate.Focus();


                toolStripStatusLabel1.Text = "Overtime rate must be a U.S. dollar amount (dollar symbol is optional).";
            }

        }

        private void removeDriver_Click(object sender, EventArgs e)
        {
            
            DateTime currentdate=DateTime.Now;
            List<RouteInfo> routes=mysql_routes.GetRouteInfo(drivers[driversList.SelectedIndex].number);
            for(int i=0;i<routes.Count;i++)
            {
                mysql_routes.DeleteRouteInfo(routes[i].number);
            }
            mysql_drivers.DeleteDriver(drivers[driversList.SelectedIndex].number);
            driversList.SelectedIndex = 0;
            
        }

        private void ConfigureAvaiableDrivers_Load(object sender, EventArgs e)
        {

        }

    }
}
