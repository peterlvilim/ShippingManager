namespace hourLogger
{
    partial class addTHRyanDriver
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
            this.addDriverButton = new System.Windows.Forms.Button();
            this.driverName = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // addDriverButton
            // 
            this.addDriverButton.Location = new System.Drawing.Point(154, 1);
            this.addDriverButton.Name = "addDriverButton";
            this.addDriverButton.Size = new System.Drawing.Size(75, 23);
            this.addDriverButton.TabIndex = 1;
            this.addDriverButton.Text = "Add Driver";
            this.addDriverButton.UseVisualStyleBackColor = true;
            this.addDriverButton.Click += new System.EventHandler(this.addDriverButton_Click);
            // 
            // driverName
            // 
            this.driverName.FormattingEnabled = true;
            this.driverName.Location = new System.Drawing.Point(3, 2);
            this.driverName.Name = "driverName";
            this.driverName.Size = new System.Drawing.Size(145, 21);
            this.driverName.TabIndex = 2;
            // 
            // addTHRyanDriver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.driverName);
            this.Controls.Add(this.addDriverButton);
            this.Name = "addTHRyanDriver";
            this.Size = new System.Drawing.Size(232, 26);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button addDriverButton;
        private System.Windows.Forms.ComboBox driverName;
    }
}
