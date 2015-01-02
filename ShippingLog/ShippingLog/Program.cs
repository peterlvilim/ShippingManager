using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using hourLogger;
using MySql.Data.MySqlClient;
namespace ShippingLog
{
    
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                //GlobalVar.authenticated = true;
                StreamReader sr = new StreamReader("RRS.conf");
                GlobalVar.sqlhost = sr.ReadLine();
                GlobalVar.sqlport = Int32.Parse(sr.ReadLine());
                GlobalVar.sqldatabase = sr.ReadLine();
                GlobalVar.sqlusername = sr.ReadLine();
                sr.Close();
            }
            catch { MessageBox.Show("Unable to load configuration file RRS.conf"); return; }
            try
            {
                MySQL MySQLHandle = new MySQL(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
                MySqlConnection sqlReader = MySQLHandle.Connect();
                if (sqlReader == null)
                {
                    MessageBox.Show("Unable to connect to database"); return;
                }
                MySQLHandle.Disconnect();
            }
            catch { MessageBox.Show("Unable to connect to database"); return; }
            try
            {

                MySQL_Settings mysql_settings = new MySQL_Settings(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
                GlobalVar.sqlsettings = mysql_settings.GetSettings();
            }
            catch
            {

            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new mainWindow());
        }
    }
}