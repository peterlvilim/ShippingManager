namespace hourLogger
{
    partial class WeeklyLog
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
            this.costTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.loggedmilesTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.loggedhoursTextBox = new System.Windows.Forms.TextBox();
            this.hoursLabel = new System.Windows.Forms.Label();
            this.weeklyReviewTextBox = new System.Windows.Forms.TextBox();
            this.reviewCheckbox = new System.Windows.Forms.CheckBox();
            this.billedmilesTextbox = new System.Windows.Forms.TextBox();
            this.milesLabel = new System.Windows.Forms.Label();
            this.billedhoursTextbox = new System.Windows.Forms.TextBox();
            this.driverNameTHRyan = new System.Windows.Forms.GroupBox();
            this.driverNameTHRyan.SuspendLayout();
            this.SuspendLayout();
            // 
            // costTextBox
            // 
            this.costTextBox.BackColor = System.Drawing.Color.White;
            this.costTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.costTextBox.Location = new System.Drawing.Point(417, 27);
            this.costTextBox.Name = "costTextBox";
            this.costTextBox.Size = new System.Drawing.Size(60, 20);
            this.costTextBox.TabIndex = 16;
            this.costTextBox.Leave += new System.EventHandler(this.costTextBox_Leave);
            this.costTextBox.LostFocus += new System.EventHandler(this.costTextBox_LostFocus);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(368, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 18);
            this.label3.TabIndex = 25;
            this.label3.Text = "Cost:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(9, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 18);
            this.label2.TabIndex = 24;
            this.label2.Text = "Logged Miles:";
            // 
            // loggedmilesTextBox
            // 
            this.loggedmilesTextBox.BackColor = System.Drawing.Color.White;
            this.loggedmilesTextBox.Enabled = false;
            this.loggedmilesTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loggedmilesTextBox.Location = new System.Drawing.Point(119, 56);
            this.loggedmilesTextBox.Name = "loggedmilesTextBox";
            this.loggedmilesTextBox.Size = new System.Drawing.Size(60, 20);
            this.loggedmilesTextBox.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(9, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 18);
            this.label1.TabIndex = 22;
            this.label1.Text = "Logged Hours:";
            // 
            // loggedhoursTextBox
            // 
            this.loggedhoursTextBox.BackColor = System.Drawing.Color.White;
            this.loggedhoursTextBox.Enabled = false;
            this.loggedhoursTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loggedhoursTextBox.Location = new System.Drawing.Point(119, 25);
            this.loggedhoursTextBox.Name = "loggedhoursTextBox";
            this.loggedhoursTextBox.Size = new System.Drawing.Size(60, 20);
            this.loggedhoursTextBox.TabIndex = 21;
            // 
            // hoursLabel
            // 
            this.hoursLabel.AutoSize = true;
            this.hoursLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hoursLabel.ForeColor = System.Drawing.Color.Black;
            this.hoursLabel.Location = new System.Drawing.Point(191, 26);
            this.hoursLabel.Name = "hoursLabel";
            this.hoursLabel.Size = new System.Drawing.Size(92, 18);
            this.hoursLabel.TabIndex = 20;
            this.hoursLabel.Text = "Billed Hours:";
            // 
            // weeklyReviewTextBox
            // 
            this.weeklyReviewTextBox.Enabled = false;
            this.weeklyReviewTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.weeklyReviewTextBox.Location = new System.Drawing.Point(514, 57);
            this.weeklyReviewTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.weeklyReviewTextBox.Name = "weeklyReviewTextBox";
            this.weeklyReviewTextBox.Size = new System.Drawing.Size(156, 20);
            this.weeklyReviewTextBox.TabIndex = 17;
            this.weeklyReviewTextBox.Leave += new System.EventHandler(this.weeklyReviewTextBox_Leave);
            this.weeklyReviewTextBox.LostFocus += new System.EventHandler(this.weeklyReviewTextBox_LostFocus);
            // 
            // reviewCheckbox
            // 
            this.reviewCheckbox.AutoSize = true;
            this.reviewCheckbox.BackColor = System.Drawing.SystemColors.Control;
            this.reviewCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reviewCheckbox.Location = new System.Drawing.Point(371, 54);
            this.reviewCheckbox.Name = "reviewCheckbox";
            this.reviewCheckbox.Size = new System.Drawing.Size(137, 22);
            this.reviewCheckbox.TabIndex = 18;
            this.reviewCheckbox.Text = "Flag for Review: ";
            this.reviewCheckbox.UseVisualStyleBackColor = false;
            this.reviewCheckbox.CheckedChanged += new System.EventHandler(this.reviewCheckbox_CheckedChanged);
            // 
            // billedmilesTextbox
            // 
            this.billedmilesTextbox.BackColor = System.Drawing.Color.White;
            this.billedmilesTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.billedmilesTextbox.Location = new System.Drawing.Point(293, 56);
            this.billedmilesTextbox.Name = "billedmilesTextbox";
            this.billedmilesTextbox.Size = new System.Drawing.Size(60, 20);
            this.billedmilesTextbox.TabIndex = 15;
            this.billedmilesTextbox.Leave += new System.EventHandler(this.billedmilesTextbox_Leave);
            this.billedmilesTextbox.LostFocus += new System.EventHandler(this.billedmilesTextbox_LostFocus);
            // 
            // milesLabel
            // 
            this.milesLabel.AutoSize = true;
            this.milesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.milesLabel.ForeColor = System.Drawing.Color.Black;
            this.milesLabel.Location = new System.Drawing.Point(191, 56);
            this.milesLabel.Name = "milesLabel";
            this.milesLabel.Size = new System.Drawing.Size(86, 18);
            this.milesLabel.TabIndex = 16;
            this.milesLabel.Text = "Billed Miles:";
            // 
            // billedhoursTextbox
            // 
            this.billedhoursTextbox.BackColor = System.Drawing.Color.White;
            this.billedhoursTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.billedhoursTextbox.Location = new System.Drawing.Point(293, 26);
            this.billedhoursTextbox.Name = "billedhoursTextbox";
            this.billedhoursTextbox.Size = new System.Drawing.Size(60, 20);
            this.billedhoursTextbox.TabIndex = 14;
            this.billedhoursTextbox.Leave += new System.EventHandler(this.billedhoursTextbox_Leave);
            this.billedhoursTextbox.LostFocus += new System.EventHandler(this.billedhoursTextbox_LostFocus);
            // 
            // driverNameTHRyan
            // 
            this.driverNameTHRyan.Controls.Add(this.loggedhoursTextBox);
            this.driverNameTHRyan.Controls.Add(this.costTextBox);
            this.driverNameTHRyan.Controls.Add(this.billedhoursTextbox);
            this.driverNameTHRyan.Controls.Add(this.label3);
            this.driverNameTHRyan.Controls.Add(this.milesLabel);
            this.driverNameTHRyan.Controls.Add(this.label2);
            this.driverNameTHRyan.Controls.Add(this.billedmilesTextbox);
            this.driverNameTHRyan.Controls.Add(this.loggedmilesTextBox);
            this.driverNameTHRyan.Controls.Add(this.reviewCheckbox);
            this.driverNameTHRyan.Controls.Add(this.label1);
            this.driverNameTHRyan.Controls.Add(this.weeklyReviewTextBox);
            this.driverNameTHRyan.Controls.Add(this.hoursLabel);
            this.driverNameTHRyan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.driverNameTHRyan.Location = new System.Drawing.Point(3, 3);
            this.driverNameTHRyan.Name = "driverNameTHRyan";
            this.driverNameTHRyan.Size = new System.Drawing.Size(676, 88);
            this.driverNameTHRyan.TabIndex = 27;
            this.driverNameTHRyan.TabStop = false;
            this.driverNameTHRyan.Text = "Driver Name";
            this.driverNameTHRyan.Enter += new System.EventHandler(this.driverNameTHRyan_Enter);
            // 
            // WeeklyLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.driverNameTHRyan);
            this.Name = "WeeklyLog";
            this.Size = new System.Drawing.Size(686, 100);
            this.driverNameTHRyan.ResumeLayout(false);
            this.driverNameTHRyan.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox costTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox loggedmilesTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox loggedhoursTextBox;
        private System.Windows.Forms.Label hoursLabel;
        private System.Windows.Forms.TextBox weeklyReviewTextBox;
        private System.Windows.Forms.CheckBox reviewCheckbox;
        private System.Windows.Forms.TextBox billedmilesTextbox;
        private System.Windows.Forms.Label milesLabel;
        private System.Windows.Forms.TextBox billedhoursTextbox;
        private System.Windows.Forms.GroupBox driverNameTHRyan;
    }
}
