namespace ShippingLog
{
    partial class customDataGridShippingLog
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.dateDeliveredColumn = new ShippingLog.CalendarColumn();
            this.runDriverColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customerNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.invoiceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rightClickDataGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calendarColumn1 = new ShippingLog.CalendarColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.rightClickDataGrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGrid
            // 
            this.dataGrid.AllowDrop = true;
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dateDeliveredColumn,
            this.runDriverColumn,
            this.customerNameColumn,
            this.invoiceColumn,
            this.id});
            this.dataGrid.ContextMenuStrip = this.rightClickDataGrid;
            this.dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid.Location = new System.Drawing.Point(0, 0);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.RowHeadersWidth = 50;
            this.dataGrid.Size = new System.Drawing.Size(758, 158);
            this.dataGrid.TabIndex = 0;
            this.dataGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dataGrid_KeyPress);
            this.dataGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_CellEndEdit);
            this.dataGrid.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGrid_EditingControlShowing);
            this.dataGrid.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGrid_RowsAdded);
            this.dataGrid.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGrid_UserAddedRow);
            this.dataGrid.DragDrop += new System.Windows.Forms.DragEventHandler(this.dataGrid_DragDrop);
            this.dataGrid.DragOver += new System.Windows.Forms.DragEventHandler(this.dataGrid_DragOver);
            this.dataGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGrid_MouseDown);
            this.dataGrid.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dataGrid_MouseMove);
            // 
            // dateDeliveredColumn
            // 
            this.dateDeliveredColumn.HeaderText = "DATE DELIVERED";
            this.dateDeliveredColumn.Name = "dateDeliveredColumn";
            this.dateDeliveredColumn.Visible = false;
            this.dateDeliveredColumn.Width = 200;
            // 
            // runDriverColumn
            // 
            this.runDriverColumn.HeaderText = "RUN DRIVER";
            this.runDriverColumn.Name = "runDriverColumn";
            this.runDriverColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.runDriverColumn.Width = 200;
            // 
            // customerNameColumn
            // 
            this.customerNameColumn.HeaderText = "CUSTOMER NAME";
            this.customerNameColumn.Name = "customerNameColumn";
            this.customerNameColumn.Width = 200;
            // 
            // invoiceColumn
            // 
            this.invoiceColumn.HeaderText = "INVOICE";
            this.invoiceColumn.Name = "invoiceColumn";
            // 
            // id
            // 
            this.id.HeaderText = "ID";
            this.id.MinimumWidth = 300;
            this.id.Name = "id";
            this.id.Visible = false;
            this.id.Width = 300;
            // 
            // rightClickDataGrid
            // 
            this.rightClickDataGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteRowToolStripMenuItem});
            this.rightClickDataGrid.Name = "rightClickDataGrid";
            this.rightClickDataGrid.Size = new System.Drawing.Size(134, 26);
            // 
            // deleteRowToolStripMenuItem
            // 
            this.deleteRowToolStripMenuItem.Name = "deleteRowToolStripMenuItem";
            this.deleteRowToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.deleteRowToolStripMenuItem.Text = "Delete Row";
            this.deleteRowToolStripMenuItem.Click += new System.EventHandler(this.deleteRowToolStripMenuItem_Click);
            // 
            // calendarColumn1
            // 
            this.calendarColumn1.HeaderText = "DATE DELIVERED";
            this.calendarColumn1.Name = "calendarColumn1";
            this.calendarColumn1.Width = 200;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "RUN DRIVER";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 200;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "CUSTOMER NAME";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 200;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "INVOICE";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // customDataGridShippingLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGrid);
            this.Name = "customDataGridShippingLog";
            this.Size = new System.Drawing.Size(758, 158);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.rightClickDataGrid.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGrid;
        private CalendarColumn calendarColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.ContextMenuStrip rightClickDataGrid;
        private System.Windows.Forms.ToolStripMenuItem deleteRowToolStripMenuItem;
        private CalendarColumn dateDeliveredColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn runDriverColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn customerNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn invoiceColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
    }
}
