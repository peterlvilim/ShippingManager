using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RRS
{
    public partial class ConfigureWeeklyRoute : Form
    {
        public ConfigureWeeklyRoute()
        {
            this.TopLevel = true;
            
            this.MaximizeBox = false;
           
            InitializeComponent();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
