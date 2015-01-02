using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySql;
using ShippingLog;
namespace hourLogger
{
    public partial class driverInstanceDriverHourLog : UserControl
    {
        public driverInstanceDriverHourLog(string driverName,DateTime date)
        {
            InitializeComponent();
            //populateViewLogTable(driverName);
            driverLabel.Text = driverName;
            
            driverLog.populateTable(driverName,date);
            refreshDriverInfo(date);
        }

        public DataGridViewRowCollection getDataGrid()
        {
            return driverLog.returnRows();
        }

        public string getDriverName()
        {
            return driverLabel.Text;
        }

        public string getEstimatedTime()
        {
            return estimatedTimeLabel.Text;
        }

        private void reviewCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (reviewCheckbox.Checked)
                weeklyReviewTextBox.Enabled = true;
            else
                weeklyReviewTextBox.Enabled = false;
        }

        private void collapseLog_MouseUp(object sender, MouseEventArgs e)
        {
            if (collapseLog.IsPlus)
            {
                driverLog.Visible = false;
                this.Height = 80;
            }
            else
            {
                driverLog.Visible = true;
                this.Height = 189;
            }
        }


        public void setHours(int newHours)
        {
            billedhoursTextbox.Text = newHours.ToString();
        }
        public double getHours()
        {
            return(Convert.ToDouble(billedhoursTextbox.Text));
        }

        public void setMiles(int newMiles)
        {
            billedmilesTextbox.Text = newMiles.ToString();
        }
        public double getMiles()
        {
            return (Convert.ToDouble(billedmilesTextbox.Text));
        }
        public void setEstimatedTime(int newTime)
        {
            estimatedTimeLabel.Text = "Estimated Time: " + newTime.ToString() + " Hours";
        }

        private void driverLog_Load(object sender, EventArgs e)
        {

        }

        private void hoursTextbox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
                if (!char.IsControl(e.KeyChar) 
        && !char.IsDigit(e.KeyChar) 
        && e.KeyChar != '.')
    {
        e.Handled = true;
    }

    // only allow one decimal point
    if (e.KeyChar == '.' 
        && (sender as TextBox).Text.IndexOf('.') > -1)
    {
        e.Handled = true;
    }
        }

        private void milesTextbox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
&& !char.IsDigit(e.KeyChar)
&& e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void hoursTextbox_LostFocus(object sender, EventArgs e)
        {
            //updateDriverInfo();
        }

        private void updateDriverInfo()
        {
            try
            {
                string reviewtext = "";
                if (reviewCheckbox.Checked == true)
                {
                    reviewtext = weeklyReviewTextBox.Text;
                }
                DateTimePicker temp = (DateTimePicker)this.Parent.Parent.Parent.Parent.Controls["logDate"];
                DateTime viewdate = temp.Value;
                Driver driver = new Driver(driverLabel.Text.ToString(), Convert.ToDouble(billedhoursTextbox.Text), Convert.ToDouble(billedmilesTextbox.Text), reviewtext, temp.Value);
                MySQL MySQLHandle = new MySQL(GlobalVar.sqlhost,GlobalVar.sqlport,GlobalVar.sqldatabase,GlobalVar.sqlusername,"");
                MySqlConnection sqlWriter = MySQLHandle.Connect();


                MySQLHandle.Update("UPDATE `rrs`.`driver_log` SET `miles` = " + driver.miles.ToString("N" + 2) + ",`hours` = " + driver.hours.ToString("N" + 2) + ", reason='" + driver.reason.ToString() + "' WHERE (`driver_log`.`driver` LIKE '" + driverLabel.Text + "') && (`driver_log`.`date` LIKE '" + viewdate.Date.ToString("yyyy-MM-dd") + "');", sqlWriter);
                //MySQLHandle.Update("UPDATE `rrs`.`driver_log` SET `miles` = " + billedhoursTextbox.Text + " WHERE (`driver_log`.`driver` = '" + driverLabel.Text + "') && (`driver_log`.`date` = '" + viewdate.Date.ToString("yyyy-MM-dd") + "');", sqlWriter);
                //MySQLHandle.updateDriverHourLogger(driver, sqlWriter);
                MySQLHandle.Disconnect();
            }
            catch { }
        }

        public void refreshDriverInfo(DateTime date)
        {
            Driver driver = new Driver();
            driver.name = driverLabel.Text.ToString();
            driver.date = date;
            MySQL MySQLHandle = new MySQL(GlobalVar.sqlhost,GlobalVar.sqlport,GlobalVar.sqldatabase,GlobalVar.sqlusername,"");
            MySqlConnection sqlReader = MySQLHandle.Connect();

            MySQLHandle.addDriverHourLogger(driver, sqlReader);
            driver=MySQLHandle.getDriverHourLogger(driver,date, sqlReader);
            
            billedhoursTextbox.Text=driver.hours.ToString("N"+2);
            billedmilesTextbox.Text = driver.miles.ToString("N" + 2);
            if (driver.reason != "")
            {
                reviewCheckbox.Checked = true;
                weeklyReviewTextBox.Text = driver.reason;
            }
        }

        private void driverContainerDummy_Enter(object sender, EventArgs e)
        {

        }

        private void milesTextbox_LostFocus(object sender, EventArgs e)
        {
           // updateDriverInfo();
        }

        private void reviewTextbox_Leave(object sender, EventArgs e)
        {
            updateDriverInfo();
        }

        private void billedhoursTextbox_Leave(object sender, EventArgs e)
        {
            updateDriverInfo();
        }

        private void billedmilesTextbox_Leave(object sender, EventArgs e)
        {
            updateDriverInfo();
        }

        private void billedhoursTextbox_TextChanged(object sender, EventArgs e)
        {
            //updateDriverInfo();
        }

        private void billedmilesTextbox_TextChanged(object sender, EventArgs e)
        {
            //updateDriverInfo();
        }

        private void weeklyReviewTextBox_TextChanged(object sender, EventArgs e)
        {
            //updateDriverInfo();
        }



        /*private void populateViewLogTable(string driverID)
        {
            MySQL MySQLHandle = new MySQL("192.168.100.114", 3306, "rrs", "root", "");
            MySqlConnection sqlReader = MySQLHandle.Connect();
            MySqlDataReader dataReader = MySQLHandle.Select("SELECT * FROM driver_log WHERE driverID='" + driverID + "';", sqlReader);
            for (int rowCounter = 0; dataReader.Read(); rowCounter++)
            {
                string amount = dataReader.GetString(1);
                string runDriver = dataReader.GetString(2);
                string customerName = dataReader.GetString(3);
                string invoice = dataReader.GetString(4);
                string location = dataReader.GetString(5);
                string id = dataReader.GetString(0);
                driverLog.addRow(amount, runDriver, customerName, invoice, location, id);
            }
        }*/
    }
}