using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using ShippingLog;
using System.Globalization;

namespace hourLogger
{
    public partial class WeeklyLog : UserControl
    {
        private string globalDriverName;

        public WeeklyLog(string driverName,DateTime date)
        {
            InitializeComponent();


            MySQL MySQLHandle = new MySQL(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
            MySqlConnection sqlReader = MySQLHandle.Connect();

            GregorianCalendar gc = new GregorianCalendar();
            int weekno = gc.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);
            string stringdate = "" + date.Year + weekno;
            
            //MySQLHandle.Insert("INSERT INTO `rrs`.`th_ryan_invoies` (`id`, `billed_hours`, `billed_miles`, `cost`, `review`, `driver`) VALUES (NULL, 0, 0, 0, '', '');", sqlReader);
            globalDriverName=driverName;
            driverNameTHRyan.Text = driverName;
            string command="SELECT * FROM `th_ryan_invoices` WHERE `driver` LIKE '" + driverName + "' AND `date` LIKE '" + stringdate + "';";
            MySqlDataReader dataReader = MySQLHandle.Select(command, sqlReader);
            dataReader.Read();
            if (dataReader.HasRows == true)
            {
                string id = dataReader.GetString(0);
                int billedHours = dataReader.GetInt32(1);
                int billedMiles = dataReader.GetInt32(2);
                int cost = dataReader.GetInt32(3);
                string review = dataReader.GetString(4);
                string driver = dataReader.GetString(5);
                billedhoursTextbox.Text = billedHours.ToString();
                billedmilesTextbox.Text = billedMiles.ToString();
                costTextBox.Text = cost.ToString();
                if (review != "")
                {
                    weeklyReviewTextBox.Text = review;
                    reviewCheckbox.Checked = true;
                }
                else
                {
                    reviewCheckbox.Checked = false;
                }
                int diff = date.DayOfWeek - DayOfWeek.Sunday;
                if (diff < 0)
                {
                    diff += 7;
                }
                date = date.AddDays(-1 * diff);
                string date1=date.ToString("yyyy-MM-dd");
                date=date.AddDays(1);
                string date2 = date.ToString("yyyy-MM-dd");
                date = date.AddDays(1);
                string date3 = date.ToString("yyyy-MM-dd");
                date = date.AddDays(1);
                string date4 = date.ToString("yyyy-MM-dd");
                date = date.AddDays(1);
                string date5 = date.ToString("yyyy-MM-dd");
                date = date.AddDays(1);
                string date6 = date.ToString("yyyy-MM-dd");
                date = date.AddDays(1);
                string date7 = date.ToString("yyyy-MM-dd");
                date = date.AddDays(1);
                command = "SELECT * FROM `driver_log` WHERE `driver` LIKE '" + driverName + "' AND (`date` LIKE '" + date1 + "' OR `date` LIKE '" + date2 + "' OR `date` LIKE '" + date3 + "' OR `date` LIKE '" + date4+ "' OR `date` LIKE '" + date5+ "' OR `date` LIKE '" + date6+ "' OR `date` LIKE '" + date7 + "');";
                dataReader.Close();
                dataReader = MySQLHandle.Select(command, sqlReader);
                int hours=0;
                int miles=0;
                for (int i = 0; dataReader.Read(); i++)
                {
                    hours+=dataReader.GetInt32(3);
                    miles+=dataReader.GetInt32(4);
                }
                dataReader.Close();
                loggedhoursTextBox.Text = hours.ToString();
                loggedmilesTextBox.Text = miles.ToString();
            }
            dataReader.Close();
        }

        private void updateDriver()
        {
            string driverName = "" + this.Controls[0].Text;

            DateTimePicker temp = (DateTimePicker)this.Parent.Parent.Parent.Parent.Controls["logDate"];
            DateTime logDate = temp.Value;
            GregorianCalendar gc = new GregorianCalendar();
            int weekno = gc.GetWeekOfYear(logDate, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);
            string stringdate = DateTime.Now.Year.ToString() + weekno.ToString();
            
            MySQL MySQLHandle = new MySQL(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
            MySqlConnection sqlWriter = MySQLHandle.Connect();
            string command = "UPDATE `th_ryan_invoices` SET `cost`="+costTextBox.Text+" ,`billed_hours`="+billedhoursTextbox.Text+",`billed_miles`="+billedmilesTextbox.Text+", review='"+weeklyReviewTextBox.Text+"' WHERE `driver` LIKE '" + driverName + "' AND `date` LIKE '" + stringdate + "';";
            MySQLHandle.Update(command, sqlWriter);
            MySQLHandle.Disconnect();
        }

        private void driverNameTHRyan_Enter(object sender, EventArgs e)
        {
        }

        private void billedhoursTextbox_Leave(object sender, EventArgs e)
        {
            updateDriver();
        }

        private void billedmilesTextbox_Leave(object sender, EventArgs e)
        {
            updateDriver();
        }

        private void costTextBox_Leave(object sender, EventArgs e)
        {
            updateDriver();
        }

        private void weeklyReviewTextBox_Leave(object sender, EventArgs e)
        {
            updateDriver();
        }

        private void reviewCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if(reviewCheckbox.Checked==true)
            {
                weeklyReviewTextBox.Enabled=true;
            }else{
                weeklyReviewTextBox.Enabled = true;
            }
        }

        private void billedhoursTextbox_LostFocus(object sender, EventArgs e)
        {
            
        }

        private void billedmilesTextbox_LostFocus(object sender, EventArgs e)
        {
            
        }

        private void costTextBox_LostFocus(object sender, EventArgs e)
        {
            
        }

        private void weeklyReviewTextBox_LostFocus(object sender, EventArgs e)
        {
            
        }
    }
}
