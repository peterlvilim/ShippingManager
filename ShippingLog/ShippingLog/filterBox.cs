using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ShippingLog
{
    public partial class filterBox : UserControl
    {
        public filterBox(string label)
        {
            InitializeComponent();
            filterDummy.Text = label;
        }

        private void filterDummy_Click(object sender, EventArgs e)
        {
            this.Parent.Controls.Remove(this);
        }

        public string getFilter()
        {
            return filterDummy.Text;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Parent.Controls.Remove(this);
        }
    }
}
