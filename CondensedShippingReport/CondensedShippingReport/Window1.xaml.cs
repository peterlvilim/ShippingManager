using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CondensedShippingReport
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        List<int> routeids;
        public Window1()
        {
            InitializeComponent();
            routeids = new List<int>();
            MySQL MySQLHandle = new MySQL(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
            MySqlConnection sqlReader = MySQLHandle.Connect();
            List<Shipping_Log_Route> shippinglogroutes = MySQLHandle.GetShippingLogRoutes(sqlReader);
            for (int i = 0; i < shippinglogroutes.Count; i++)
            {
                listRoutes.Items.Add(shippinglogroutes[i].name);
                routeids.Add(shippinglogroutes[i].ID);
            }
        }

        private void buttonRemove_Click(object sender, RoutedEventArgs e)
        {
            int position=listRoutes.SelectedIndex;
            listRoutes.Items.RemoveAt(position);

            MySQL MySQLHandle = new MySQL(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
            MySqlConnection sqlWriter = MySQLHandle.Connect();
            MySQLHandle.Delete("DELETE FROM `shipping_log_routes` WHERE ID=" + routeids[position].ToString() + ";", sqlWriter);
            routeids.RemoveAt(position);
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            string currenttext=textName.Text;
            listRoutes.Items.Add(currenttext);
            textName.Text = "";
            
                        MySQL MySQLHandle = new MySQL(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
            MySqlConnection sqlWriter = MySQLHandle.Connect();
            string insertquery="INSERT INTO `shipping_log_routes` (ID,Route) VALUES(0,'"+currenttext+"');";
            long insertid = MySQLHandle.Insert(insertquery,sqlWriter);
            routeids.Add((int)insertid);
        }
    }
}
