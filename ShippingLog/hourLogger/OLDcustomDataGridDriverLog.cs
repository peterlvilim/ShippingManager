﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using ShippingLog;

namespace hourLogger
{
    public partial class customDataGridShippingLog : UserControl
    {
        private MySQL MySQLHandle;
        private MySqlConnection sqlReader;
        private Point oldSelectedCells;
        private bool wasEditing;
        private DateTime currentLogDate;
        private MySqlConnection connection;

        public customDataGridShippingLog()
        {
            InitializeComponent();
            MySQLHandle = new MySQL(GlobalVar.sqlhost,GlobalVar.sqlport,GlobalVar.sqldatabase,GlobalVar.sqlusername,"");
            sqlReader = MySQLHandle.Connect();
            for (int rowNumber = 0; rowNumber < dataGrid.Rows.Count; rowNumber++)
            {
               dataGrid.Rows[rowNumber].HeaderCell.Value = (rowNumber + 1).ToString();
            }
            string connStr = "Server=" + GlobalVar.sqlhost + ";Uid=" + GlobalVar.sqlusername + ";Pwd=;Database=" + GlobalVar.sqldatabase + ";";
            connection = new MySqlConnection(connStr);
            connection.Open();
        }

        public void populateTable(string toPopulateWith,DateTime date)
        {
            MySQL MySQLHandle = new MySQL(GlobalVar.sqlhost,GlobalVar.sqlport,GlobalVar.sqldatabase,GlobalVar.sqlusername,"");
            MySqlConnection sqlReader = MySQLHandle.Connect();
            MySQL MySQLLocationHandle = new MySQL(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
            MySqlConnection locationConnection = MySQLLocationHandle.Connect();
            string datestring = date.ToString("yyyy-MM-dd");
            MySqlDataReader dataReader = MySQLHandle.Select("SELECT * FROM shipping_log WHERE `date_delivered` LIKE '"+datestring+"' AND run_driver='" + toPopulateWith + "' ORDER BY `order` ASC;", sqlReader);
            for (int rowCounter = 0; dataReader.Read(); rowCounter++)
            {
                string customerName = dataReader.GetString(3);
                string invoice = dataReader.GetString(4);
                MySqlDataReader locationDataReader = MySQLHandle.Select("SELECT `INV_CITY`, `INV_STATE` FROM `invoice_data` WHERE `INV_NUMBER`='" + invoice + "';", locationConnection);
                Console.WriteLine("SELECT `INV_CITY`, `INV_STATE` FROM `invoice_data` WHERE `INV_NUMBER`='" + invoice + "';");
                locationDataReader.Read();
                locationDataReader.Close();
                string location = "";
                if (locationDataReader.HasRows == true)
                {
                    location = locationDataReader.GetString(0) + ", " + locationDataReader.GetString(1);
                }
                string dateDelivered = dataReader.GetString(1);
                string id = dataReader.GetString(0);
                addRow(customerName, invoice, location, dateDelivered, id);
            }
            
            dataReader.Close();
            sqlReader.Close();
            locationConnection.Close();
        }

        public void updateRow(DataGridViewRow rowToUpdate)
        {
            string dateDelivered="";
            string customerName="";
            string invoice="";
            string id="";
            string location = "";
            if (rowToUpdate.Cells[0].Value != null)
                customerName = rowToUpdate.Cells[0].Value.ToString();
            if (rowToUpdate.Cells[1].Value != null)
                invoice = rowToUpdate.Cells[1].Value.ToString();
            if (rowToUpdate.Cells[2].Value != null)
                id = rowToUpdate.Cells[2].Value.ToString();
            if (rowToUpdate.Cells[3].Value != null)
                location = rowToUpdate.Cells[3].Value.ToString();
            if (rowToUpdate.Cells[4].Value != null)
                dateDelivered = rowToUpdate.Cells[4].Value.ToString();
            MySqlCommand myCommand = new MySqlCommand("UPDATE `rrs`.`shipping_log` SET `customer_name` = '" + customerName + "', `invoice` = '" + invoice + "' WHERE `shipping_log`.`id` = " + id + ";");
            myCommand.Connection = connection;
            myCommand.ExecuteNonQuery();
        }

        private Rectangle dragBoxFromMouseDown;
        private int rowIndexFromMouseDown;
        private int rowIndexOfItemUnderMouseToDrop;


        public void stopUserDrag()
        {
            dataGrid.AllowDrop = false;
        }
        
        private void dataGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                // If the mouse moves outside the rectangle, start the drag.
                if (dragBoxFromMouseDown != Rectangle.Empty &&
                    !dragBoxFromMouseDown.Contains(e.X, e.Y))
                {

                    // Proceed with the drag and drop, passing in the list item.                    
                    DragDropEffects dropEffect = dataGrid.DoDragDrop(
                    dataGrid.Rows[rowIndexFromMouseDown],
                    DragDropEffects.Move);
                }
            }
        }

        private void dataGrid_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hti = dataGrid.HitTest(e.X, e.Y);
                dataGrid.ClearSelection();
                dataGrid.Rows[hti.RowIndex].Selected = true;
            }

            // Get the index of the item the mouse is below.
            rowIndexFromMouseDown = dataGrid.HitTest(e.X, e.Y).RowIndex;
            if (rowIndexFromMouseDown != -1)
            {
                // Remember the point where the mouse down occurred. 
                // The DragSize indicates the size that the mouse can move 
                // before a drag event should be started.                
                Size dragSize = SystemInformation.DragSize;

                // Create a rectangle using the DragSize, with the mouse position being
                // at the center of the rectangle.
                dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2),
                                                               e.Y - (dragSize.Height / 2)),
                                    dragSize);
            }
            else
                // Reset the rectangle if the mouse is not over an item in the ListBox.
                dragBoxFromMouseDown = Rectangle.Empty;
        }

        private void dataGrid_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void dataGrid_DragDrop(object sender, DragEventArgs e)
        {
            // The mouse locations are relative to the screen, so they must be 
            // converted to client coordinates.
            Point clientPoint = dataGrid.PointToClient(new Point(e.X, e.Y));

            // Get the row index of the item the mouse is below. 
            rowIndexOfItemUnderMouseToDrop =
                dataGrid.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

            // If the drag operation was a move then remove and insert the row.
            if (e.Effect == DragDropEffects.Move)
            {
                DataGridViewRow rowToMove = e.Data.GetData(
                typeof(DataGridViewRow)) as DataGridViewRow;
                if(dataGrid.Rows[rowIndexOfItemUnderMouseToDrop].IsNewRow==false)
                {
                    dataGrid.Rows.RemoveAt(rowIndexFromMouseDown);
                    dataGrid.Rows.Insert(rowIndexOfItemUnderMouseToDrop, rowToMove);
                    for (int rowCounter = 0; rowCounter < dataGrid.RowCount - 1; rowCounter++)
                    {
                        MySQLHandle.updateShippingLogInvoice(dataGrid.Rows[rowCounter],false,false);
                    }
                }
            }
        }

        public DataGridViewRowCollection returnRows()
        {
            return dataGrid.Rows;
        }

        private void dataGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            dataGrid.Rows[e.RowIndex].Cells[0].Value = currentLogDate.ToString("yyyy-MM-dd");
            for (int rowNumber = 0; rowNumber < dataGrid.Rows.Count; rowNumber++)
            {
                dataGrid.Rows[rowNumber].HeaderCell.Value = (rowNumber + 1).ToString();
            }
        }

        public void sendDataGridNewDate(DateTime newDate)
        {
            currentLogDate = newDate;
        }

        public void addBlankRow(DateTime dateToSet)
        {
            dataGrid.Rows.Add(1);
            dataGrid.Rows[0].Cells[0].Value = dateToSet.ToString("yyyy-MM-dd");
        }

        public void addRow(string customerName, string invoice, string location, string dateDelivered, string id)
        {
            dataGrid.Rows.Insert(dataGrid.Rows.Count, 1);
            dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[0].Value = customerName;
            dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[1].Value = invoice;
            dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[2].Value = id;
            dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[3].Value = location;
            dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[4].Value = dateDelivered;
        }

        public void setRow(int rowToSet, string dateDelivered, string runDriver, string customerName, string invoice, string id)
        {
            dataGrid.Rows[rowToSet].Cells[0].Value = dateDelivered;
            dataGrid.Rows[rowToSet].Cells[1].Value = runDriver;
            dataGrid.Rows[rowToSet].Cells[2].Value = customerName;
            dataGrid.Rows[rowToSet].Cells[3].Value = "#" + invoice;
            dataGrid.Rows[rowToSet].Cells[4].Value = id;
       }

        private void dataGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dataGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
            DataGridViewRow rowToUpdate = dataGrid.Rows[e.RowIndex];
            updateRow(rowToUpdate);
        }

        private void dataGrid_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
        }

        public void clearRows()
        {
            dataGrid.Rows.Clear();
        }

        public void setOldRows()
        {
            if (dataGrid.CurrentCellAddress.Y >= 0 && dataGrid.CurrentCellAddress.X>=0)
            {
                oldSelectedCells = dataGrid.CurrentCellAddress;
                wasEditing = dataGrid.Rows[dataGrid.CurrentCellAddress.Y].Cells[dataGrid.CurrentCellAddress.X].IsInEditMode;
            }
        }

        public void selectOldCells()
        {
            if (dataGrid.CurrentCellAddress.Y >= 0 && dataGrid.CurrentCellAddress.X >= 0)
            {
                DataGridViewCell cell = dataGrid.Rows[oldSelectedCells.Y].Cells[oldSelectedCells.X];
                dataGrid.CurrentCell = cell;
             //   if(wasEditing)
               // dataGrid.BeginEdit(false);
            }
        }

        private void deleteRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Int32 rowToDelete = dataGrid.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            string idOfRow = dataGrid.Rows[rowToDelete].Cells[4].Value.ToString();
            MySqlCommand myCommand = new MySqlCommand("DELETE FROM `rrs`.`shipping_log` WHERE `shipping_log`.`id` = " +  idOfRow + ";");
            myCommand.Connection = sqlReader;
            myCommand.ExecuteNonQuery();
            dataGrid.Rows.RemoveAt(rowToDelete);
            dataGrid.ClearSelection();
            for (int rowNumber = 0; rowNumber < dataGrid.Rows.Count; rowNumber++)
            {
                dataGrid.Rows[rowNumber].HeaderCell.Value = (rowNumber + 1).ToString();
            }
        }

        private void dataGrid_CellValidated(object sender, DataGridViewCellValidatingEventArgs e)
        {
            
            

        }
    }
}