using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using hourLogger;
using MySql.Data.MySqlClient;

namespace ShippingLog
{
    public partial class customDataGridShippingLog : UserControl
    {
        public bool editing;
        private MySQL MySQLHandle;
        private MySqlConnection sqlReader;
        private Point oldSelectedCells;
        private bool wasEditing;
        private DateTime currentLogDate;
        private AutoCompleteStringCollection autoCompleteCollection;
        private bool rowadded;
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

        public customDataGridShippingLog()
        {

            InitializeComponent();
            editing = false;
            MySQLHandle = new MySQL(GlobalVar.sqlhost,GlobalVar.sqlport,GlobalVar.sqldatabase,GlobalVar.sqlusername,"");
            sqlReader = MySQLHandle.Connect();
            autoCompleteCollection = MySQLHandle.returnAutoComplete();
            for (int rowNumber = 0; rowNumber < dataGrid.Rows.Count; rowNumber++)
            {
               dataGrid.Rows[rowNumber].HeaderCell.Value = (rowNumber + 1).ToString();
            }
        }

        private Rectangle dragBoxFromMouseDown;
        private int rowIndexFromMouseDown;
        private int rowIndexOfItemUnderMouseToDrop;

        public void allowUserToEdit()
        {
            editing = false;
            dataGrid.AllowUserToAddRows = true;
        }

        public void stopUserEdit()
        {
            editing = false;
            dataGrid.AllowUserToAddRows = false;
        }

        public void stopUserDrag()
        {
            editing = false;
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
            editing = false;
            e.Effect = DragDropEffects.Move;
        }

        private void dataGrid_DragDrop(object sender, DragEventArgs e)
        {

            editing = false;
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
            editing = false;
            return dataGrid.Rows;
        }

        public void sortByInvoices()
        {
            editing = false;
            dataGrid.Sort(dataGrid.Columns[3], ListSortDirection.Ascending);
        }

        private void dataGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            editing = false;
            dataGrid.Rows[e.RowIndex].Cells[0].Value = currentLogDate.ToString("yyyy-MM-dd");
            for (int rowNumber = 0; rowNumber < dataGrid.Rows.Count; rowNumber++)
            {
                dataGrid.Rows[rowNumber].HeaderCell.Value = (rowNumber + 1).ToString();
            }
        }

        public void sendDataGridNewDate(DateTime newDate)
        {
            editing = false;
            currentLogDate = newDate;
        }

        public void addBlankRow(DateTime dateToSet)
        {
            dataGrid.Rows.Add(1);
            dataGrid.Rows[0].Cells[0].Value = dateToSet.ToString("yyyy-MM-dd");
        }

        public void addRow(string dateDelivered, string runDriver, string customerName, string invoice, string id)
        {
            editing = false;
            stopUserEdit();
            dataGrid.Rows.Insert(dataGrid.Rows.Count, 1);
            dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[0].Value = dateDelivered;
            dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[1].Value = runDriver;
            dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[2].Value = customerName;
            dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[3].Value = invoice;
            dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[4].Value = id;
            allowUserToEdit();
        }

        public void setRow(int rowToSet, string dateDelivered, string runDriver, string customerName, string invoice, string id)
        {
            editing = false;
            dataGrid.Rows[rowToSet].Cells[0].Value = dateDelivered;
            dataGrid.Rows[rowToSet].Cells[1].Value = runDriver;
            dataGrid.Rows[rowToSet].Cells[2].Value = customerName;
            dataGrid.Rows[rowToSet].Cells[3].Value = "#" + invoice;
            dataGrid.Rows[rowToSet].Cells[4].Value = id;
       }

        private void dataGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            editing = false;
            if (e.ColumnIndex == 1 && !autoCompleteCollection.Contains(dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
            {
                dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
            }

            dataGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
            DataGridViewRow rowToUpdate = dataGrid.Rows[e.RowIndex];
            bool dosearch = false;
            if (e.ColumnIndex == 3)
            {
                dosearch = true;
            }
            int currentid=MySQLHandle.updateShippingLogInvoice(rowToUpdate,rowadded,dosearch);
            dataGrid.Rows[e.RowIndex].Cells[4].Value = currentid;

            rowadded = false;
        }

        private void dataGrid_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            editing = false;
            rowadded = true;
        }

        public void clearRows()
        {
            editing = false;
            dataGrid.Rows.Clear();
        }

        public void setOldRows()
        {
            editing = false;
            if (dataGrid.CurrentCellAddress.Y >= 0 && dataGrid.CurrentCellAddress.X>=0)
            {
                oldSelectedCells = dataGrid.CurrentCellAddress;
                wasEditing = dataGrid.Rows[dataGrid.CurrentCellAddress.Y].Cells[dataGrid.CurrentCellAddress.X].IsInEditMode;
            }
        }

        public void selectOldCells()
        {
            editing = false;
            try
            {
                if (dataGrid.CurrentCellAddress.Y >= 0 && dataGrid.CurrentCellAddress.X >= 0)
                {
                    DataGridViewCell cell = dataGrid.Rows[oldSelectedCells.Y].Cells[oldSelectedCells.X];
                    dataGrid.CurrentCell = cell;
                    if (wasEditing)
                        dataGrid.BeginEdit(false);
                }
            }
            catch { }
        }

        private void deleteRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MySQL MySQLHandle2 = new MySQL(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
                MySqlConnection sqlReader2 = MySQLHandle2.Connect();
                Int32 rowToDelete = dataGrid.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (dataGrid.Rows[rowToDelete].Cells[4].Value != null)
                {
                    string idOfRow = dataGrid.Rows[rowToDelete].Cells[4].Value.ToString();
                    MySqlCommand myCommand = new MySqlCommand("DELETE FROM `rrs`.`shipping_log` WHERE `shipping_log`.`id` = " + idOfRow + ";");
                    myCommand.Connection = sqlReader2;
                    myCommand.ExecuteNonQuery();
                    dataGrid.Rows.RemoveAt(rowToDelete);
                    dataGrid.ClearSelection();
                    for (int rowNumber = 0; rowNumber < dataGrid.Rows.Count; rowNumber++)
                    {
                        dataGrid.Rows[rowNumber].HeaderCell.Value = (rowNumber + 1).ToString();
                    }
                }
                sqlReader2.Close();
                MySQLHandle2.Disconnect();
            }
            catch { }
        }

        private void dataGrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            editing = true;
           int column = dataGrid.CurrentCell.ColumnIndex;

            string headerText = dataGrid.Columns[column].HeaderText;


                if (e.Control is TextBox)
                {
                    TextBox tb = e.Control as TextBox;
                    tb.KeyPress -= new KeyPressEventHandler(tb_KeyPress);
                    if (headerText.Equals("INVOICE"))
                    {
                    
                    tb.KeyPress += new KeyPressEventHandler(tb_KeyPress);
                    
                }
            }


            if (headerText.Equals("RUN DRIVER"))
            {


                TextBox tb = e.Control as TextBox;



                if (tb != null)
                {
                    MySQL MySQLHandle = new MySQL(GlobalVar.sqlhost, GlobalVar.sqlport, GlobalVar.sqldatabase, GlobalVar.sqlusername, "");
                    MySqlConnection sqlReader = MySQLHandle.Connect();
                    tb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

                    tb.AutoCompleteCustomSource = autoCompleteCollection;


                    tb.AutoCompleteSource = AutoCompleteSource.CustomSource;

                }

            } 
        }

        void tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != '\b') //allow the backspace key
                {
                    e.Handled = true;
                }
            }
            
        }

        private void dataGrid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
&& !char.IsDigit(e.KeyChar)
&& e.KeyChar != '.')
            {
                e.Handled = true;
            }

        }
    }
}