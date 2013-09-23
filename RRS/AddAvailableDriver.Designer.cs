namespace RRS
{
    partial class AddAvailableDriver
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label7 = new System.Windows.Forms.Label();
            this.driverNotes = new System.Windows.Forms.TextBox();
            this.truckType = new System.Windows.Forms.ComboBox();
            this.AddDriver = new System.Windows.Forms.Button();
            this.maitenanceSurcharge = new System.Windows.Forms.TextBox();
            this.fuelSurcharge = new System.Windows.Forms.TextBox();
            this.hourlyRate = new System.Windows.Forms.TextBox();
            this.name = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Cancel = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.overtimeRate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 107);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 13);
            this.label7.TabIndex = 27;
            this.label7.Text = "Driver Notes:";
            // 
            // driverNotes
            // 
            this.driverNotes.Location = new System.Drawing.Point(12, 123);
            this.driverNotes.Multiline = true;
            this.driverNotes.Name = "driverNotes";
            this.driverNotes.Size = new System.Drawing.Size(445, 83);
            this.driverNotes.TabIndex = 26;
            // 
            // truckType
            // 
            this.truckType.FormattingEnabled = true;
            this.truckType.Location = new System.Drawing.Point(78, 49);
            this.truckType.Name = "truckType";
            this.truckType.Size = new System.Drawing.Size(121, 21);
            this.truckType.TabIndex = 25;
            this.truckType.Text = "Straight";
            // 
            // AddDriver
            // 
            this.AddDriver.Location = new System.Drawing.Point(12, 212);
            this.AddDriver.Name = "AddDriver";
            this.AddDriver.Size = new System.Drawing.Size(445, 23);
            this.AddDriver.TabIndex = 24;
            this.AddDriver.Text = "Add Driver";
            this.AddDriver.UseVisualStyleBackColor = true;
            this.AddDriver.Click += new System.EventHandler(this.AddDriver_Click);
            // 
            // maitenanceSurcharge
            // 
            this.maitenanceSurcharge.Location = new System.Drawing.Point(384, 93);
            this.maitenanceSurcharge.Name = "maitenanceSurcharge";
            this.maitenanceSurcharge.Size = new System.Drawing.Size(74, 20);
            this.maitenanceSurcharge.TabIndex = 23;
            this.maitenanceSurcharge.Text = ".35";
            this.maitenanceSurcharge.Leave += new System.EventHandler(this.maitenanceSurcharge_Leave);
            // 
            // fuelSurcharge
            // 
            this.fuelSurcharge.Location = new System.Drawing.Point(383, 70);
            this.fuelSurcharge.Name = "fuelSurcharge";
            this.fuelSurcharge.Size = new System.Drawing.Size(74, 20);
            this.fuelSurcharge.TabIndex = 22;
            this.fuelSurcharge.Text = ".49";
            this.fuelSurcharge.Leave += new System.EventHandler(this.fuelSurcharge_Leave);
            // 
            // hourlyRate
            // 
            this.hourlyRate.Location = new System.Drawing.Point(383, 18);
            this.hourlyRate.Name = "hourlyRate";
            this.hourlyRate.Size = new System.Drawing.Size(74, 20);
            this.hourlyRate.TabIndex = 21;
            this.hourlyRate.Text = "54.08";
            this.hourlyRate.Leave += new System.EventHandler(this.hourlyRate_Leave);
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(80, 15);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(152, 20);
            this.name.TabIndex = 20;
            this.name.Text = "Wally";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(248, 96);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(121, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Maintenance Surcharge";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(247, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Fuel Surcharge";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(248, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Hourly Rate";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Truck Type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Name";
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(13, 241);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(445, 23);
            this.Cancel.TabIndex = 28;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 269);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(470, 22);
            this.statusStrip1.TabIndex = 29;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // overtimeRate
            // 
            this.overtimeRate.Location = new System.Drawing.Point(383, 44);
            this.overtimeRate.Name = "overtimeRate";
            this.overtimeRate.Size = new System.Drawing.Size(74, 20);
            this.overtimeRate.TabIndex = 30;
            this.overtimeRate.Text = "54.08";
            this.overtimeRate.TextChanged += new System.EventHandler(this.overtimeRate_TextChanged);
            this.overtimeRate.Leave += new System.EventHandler(this.overtimeRate_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(248, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "Overtime Hourly Rate";
            // 
            // AddAvailableDriver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 291);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.driverNotes);
            this.Controls.Add(this.truckType);
            this.Controls.Add(this.AddDriver);
            this.Controls.Add(this.maitenanceSurcharge);
            this.Controls.Add(this.overtimeRate);
            this.Controls.Add(this.fuelSurcharge);
            this.Controls.Add(this.hourlyRate);
            this.Controls.Add(this.name);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Name = "AddAvailableDriver";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Available Driver";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox driverNotes;
        private System.Windows.Forms.ComboBox truckType;
        private System.Windows.Forms.Button AddDriver;
        private System.Windows.Forms.TextBox maitenanceSurcharge;
        private System.Windows.Forms.TextBox fuelSurcharge;
        private System.Windows.Forms.TextBox overtimeRate;
        private System.Windows.Forms.TextBox hourlyRate;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        
        private System.Windows.Forms.Label label1;

    }
}