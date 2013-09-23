﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RRS
{
    public partial class ChangeSettings : Form
    {
        MySQL_Settings mysql_settings = new MySQL_Settings(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
        
        public ChangeSettings()
        {
            
            this.MaximizeBox = false;
            
            InitializeComponent();
            try
            {

                textBox1.Text = GlobalVar.sqlsettings.RRSaddress;
                textBox2.Text = GlobalVar.sqlsettings.RRSlatitude.ToString();
                textBox3.Text = GlobalVar.sqlsettings.RRSlongitude.ToString();
                textBox4.Text = GlobalVar.sqlsettings.mapquestkey;
                textBox5.Text = GlobalVar.sqlsettings.RRSLinesFile.Replace("\\\\","\\");
                textBox6.Text = GlobalVar.sqlsettings.RRSHeaderFile.Replace("\\\\","\\");
                textBox7.Text = "";
                textBox7.UseSystemPasswordChar = true;
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
         
            Application.OpenForms[0].Controls[0].Enabled = true;
            
                Application.OpenForms[0].Controls[1].Enabled = true;

             Application.OpenForms[0].Controls[2].Enabled = true;
            Application.OpenForms[0].Controls[3].Enabled = true;
            
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Settings newsettings = new Settings(textBox1.Text, float.Parse(textBox2.Text), float.Parse(textBox3.Text), textBox4.Text, textBox5.Text.Replace("\\\\", "\\"), textBox6.Text.Replace("\\\\", "\\"),DataFormat.GetSha1(textBox7.Text));
                mysql_settings.UpdateSettings(newsettings);
                GlobalVar.sqlsettings=mysql_settings.GetSettings();
                textBox1.Text = GlobalVar.sqlsettings.RRSaddress;
                textBox2.Text = GlobalVar.sqlsettings.RRSlatitude.ToString();
                textBox3.Text = GlobalVar.sqlsettings.RRSlongitude.ToString();
                textBox4.Text = GlobalVar.sqlsettings.mapquestkey;
                textBox5.Text = GlobalVar.sqlsettings.RRSLinesFile.Replace("\\\\", "\\");
                textBox6.Text = GlobalVar.sqlsettings.RRSHeaderFile.Replace("\\\\", "\\");
                textBox7.Text = "";
            }
            catch {
                GlobalVar.sqlsettings = mysql_settings.GetSettings();
                textBox1.Text = GlobalVar.sqlsettings.RRSaddress;
                textBox2.Text = GlobalVar.sqlsettings.RRSlatitude.ToString();
                textBox3.Text = GlobalVar.sqlsettings.RRSlongitude.ToString();
                textBox4.Text = GlobalVar.sqlsettings.mapquestkey;
                textBox5.Text = GlobalVar.sqlsettings.RRSLinesFile.Replace("\\\\", "\\");
                textBox6.Text = GlobalVar.sqlsettings.RRSHeaderFile.Replace("\\\\", "\\");
                textBox7.Text = "";
            }
        }

        private void ChangeSettings_Load(object sender, EventArgs e)
        {

        }
    }
}
