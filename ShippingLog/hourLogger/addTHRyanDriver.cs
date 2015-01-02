using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql;
using MySql.Data.MySqlClient;
using ShippingLog;
using System.Globalization;
namespace hourLogger
{
    public partial class addTHRyanDriver : UserControl
    {
        private class Item
        {
            public string Name;
            public int Value;
            public Item(string name, int value)
            {
                Name = name; Value = value;
            }
            public override string ToString()
            {
                // Generates the text shown in the combo box
                return Name;
            }
        }

        public addTHRyanDriver()
        {
            InitializeComponent();
            MySQL MySQLHandle = new MySQL(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
            MySqlConnection sqlReader = MySQLHandle.Connect();
            MySqlDataReader dataReader = MySQLHandle.Select("SELECT `DRV_NAME` FROM drivers;", sqlReader);
            for(int rowCounter=0; dataReader.Read(); rowCounter++)
            {
                driverName.Items.Add(new Item(dataReader.GetString(0), rowCounter));
            }
        }

        private void addDriverButton_Click(object sender, EventArgs e)
        {
            MySQL MySQLHandle = new MySQL(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
            MySqlConnection sqlReader = MySQLHandle.Connect();
            string stringdrivername = driverName.Text;
            DateTimePicker picker = (DateTimePicker)this.Parent.Parent.Parent.Parent.Controls["logDate"];
            GregorianCalendar gc = new GregorianCalendar();
            int weekno = gc.GetWeekOfYear(picker.Value, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);
            string stringdate = "" + picker.Value.Year + weekno;
            string insertstring="INSERT INTO `th_ryan_invoices` (`id`, `billed_hours`, `billed_miles`, `cost`, `review`, `driver`,`date`) VALUES (NULL, '', '', '', '', '"+stringdrivername+"','"+stringdate+"');";
            FlowLayoutPanel tabcontrol = (FlowLayoutPanel)this.Parent.Parent.Controls[0];
            WeeklyLog newDriver = new WeeklyLog(stringdrivername,picker.Value);
            tabcontrol.Controls.Add(newDriver);
            
            FlowLayoutPanel newlayout = new FlowLayoutPanel();
            tabcontrol.Controls.SetChildIndex(newDriver, tabcontrol.Controls.Count - 2);
       
            tabcontrol = newlayout;
            MySQLHandle.Insert(insertstring, sqlReader);
            MySQLHandle.Disconnect();
            driverName.Text = "";
            
        }
    }
}
