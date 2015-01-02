namespace hourLogger
{
    partial class editHours
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
            this.label1 = new System.Windows.Forms.Label();
            this.numberOfHoursTextBox = new System.Windows.Forms.TextBox();
            this.addDriverSearchFilter = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Number of Hours";
            // 
            // numberOfHoursTextBox
            // 
            this.numberOfHoursTextBox.Location = new System.Drawing.Point(107, 8);
            this.numberOfHoursTextBox.Name = "numberOfHoursTextBox";
            this.numberOfHoursTextBox.Size = new System.Drawing.Size(147, 20);
            this.numberOfHoursTextBox.TabIndex = 1;
            // 
            // addDriverSearchFilter
            // 
            this.addDriverSearchFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.addDriverSearchFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addDriverSearchFilter.Location = new System.Drawing.Point(260, 8);
            this.addDriverSearchFilter.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.addDriverSearchFilter.Name = "addDriverSearchFilter";
            this.addDriverSearchFilter.Size = new System.Drawing.Size(109, 23);
            this.addDriverSearchFilter.TabIndex = 16;
            this.addDriverSearchFilter.Text = "Done";
            this.addDriverSearchFilter.UseVisualStyleBackColor = false;
            this.addDriverSearchFilter.Click += new System.EventHandler(this.addDriverSearchFilter_Click);
            // 
            // editHours
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 40);
            this.Controls.Add(this.addDriverSearchFilter);
            this.Controls.Add(this.numberOfHoursTextBox);
            this.Controls.Add(this.label1);
            this.Name = "editHours";
            this.Text = "Hours";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox numberOfHoursTextBox;
        private System.Windows.Forms.Button addDriverSearchFilter;
    }
}