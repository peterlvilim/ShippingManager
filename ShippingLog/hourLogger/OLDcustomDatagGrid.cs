using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace hourLogger
{
    public partial class customDataGridHourLogger : UserControl
    {
        public customDataGridHourLogger()
        {
            InitializeComponent();
            for (int rowNumber = 0; rowNumber < dataGrid.Rows.Count; rowNumber++)
            {
                dataGrid.Rows[rowNumber].HeaderCell.Value = (rowNumber + 1).ToString();
            }
        }


        private Rectangle dragBoxFromMouseDown;
        private int rowIndexFromMouseDown;
        private int rowIndexOfItemUnderMouseToDrop;
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
                dataGrid.Rows.RemoveAt(rowIndexFromMouseDown);
                dataGrid.Rows.Insert(rowIndexOfItemUnderMouseToDrop, rowToMove);
            }
        }

        private void dataGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int rowNumber = 0; rowNumber < dataGrid.Rows.Count; rowNumber++)
            {
                dataGrid.Rows[rowNumber].HeaderCell.Value = (rowNumber+1).ToString();
            }
        }

        public void addRow(string amount, string runDriver, string customerName, string invoice, string location, string id)
        {
            dataGrid.Rows.Insert(dataGrid.Rows.Count, 1);
            dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[0].Value = amount;
            dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[1].Value = runDriver;
            dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[2].Value = customerName;
            dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[3].Value = "#" + invoice;
            dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[4].Value = location;
            dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[5].Value = id;
        }

        public void setRow(int rowToSet, string amount, string runDriver, string customerName, string invoice, string location, string id)
        {
            dataGrid.Rows[rowToSet].Cells[0].Value = amount;
            dataGrid.Rows[rowToSet].Cells[1].Value = runDriver;
            dataGrid.Rows[rowToSet].Cells[2].Value = customerName;
            dataGrid.Rows[rowToSet].Cells[3].Value = "#" + invoice;
            dataGrid.Rows[rowToSet].Cells[4].Value = location;
            dataGrid.Rows[rowToSet].Cells[5].Value = id;
        }

        private void dataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        
    }
}