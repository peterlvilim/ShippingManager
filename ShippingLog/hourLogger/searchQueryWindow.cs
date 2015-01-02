using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ShippingLog
{
    public partial class searchQueryWindow : Form
    {
        public searchQueryWindow()
        {
            InitializeComponent();
            searchTypeDropdown.SelectedIndex = 0;
            searchCategoryDropdown.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
