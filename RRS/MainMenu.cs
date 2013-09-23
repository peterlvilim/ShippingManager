﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RRS
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            
            this.MaximizeBox = false;
           
            InitializeComponent();
        
        }
        
        private void ConfigureDrivers_Click(object sender, EventArgs e)
        {
            try
            {
                ConfigureDrivers.Enabled = false;
                Exit.Enabled = false;
                button1.Enabled = false;
                DeliverySchedule.Enabled = false;
                new ConfigureAvaiableDrivers().Show();
                
            }
            catch { }
        }
        private void MainMenu_GotFocus(object sender, EventArgs e)
        {
            FormCollection collection = Application.OpenForms;
                for(int i=0;i<collection.Count;i++)
                {
                    if(i==collection.Count-1){
                    collection[i].Activate();
                        
                    }
                    }
            
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void WeeklyRoute_Click(object sender, EventArgs e)
        {
            new ConfigureWeeklyRoute().Show();
        }

        private void DeliverySchedule_Click(object sender, EventArgs e)
        {
            try
            {
                ConfigureDrivers.Enabled = false;
                Exit.Enabled = false;
                button1.Enabled = false;
                DeliverySchedule.Enabled = false;
                new DeliverySchedule().Show();

            }
            catch { }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (button1.Text == "Change Settings")
                {
                    new ChangeSettings().Show();
                }
                else
                {
                    ConfigureDrivers.Enabled = false;
                    Exit.Enabled = false;
                    button1.Enabled = false;
                    DeliverySchedule.Enabled = false;
                    new Authenticate().Show();
                }

            }
            catch { }
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            if(GlobalVar.authenticated==false)
            {
                ConfigureDrivers.Enabled = false;
                DeliverySchedule.Text = "View Daily Delivery Schedule";
                ConfigureDrivers.Enabled = false;
            }
           
        }
    }
}
