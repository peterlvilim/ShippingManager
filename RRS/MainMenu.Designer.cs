namespace RRS
{
    partial class MainMenu
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
            this.ConfigureDrivers = new System.Windows.Forms.Button();
            this.DeliverySchedule = new System.Windows.Forms.Button();
            this.Exit = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ConfigureDrivers
            // 
            this.ConfigureDrivers.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfigureDrivers.Location = new System.Drawing.Point(12, 60);
            this.ConfigureDrivers.Name = "ConfigureDrivers";
            this.ConfigureDrivers.Size = new System.Drawing.Size(374, 42);
            this.ConfigureDrivers.TabIndex = 0;
            this.ConfigureDrivers.Text = "Configure Available Drivers ";
            this.ConfigureDrivers.UseVisualStyleBackColor = true;
            this.ConfigureDrivers.Click += new System.EventHandler(this.ConfigureDrivers_Click);
            // 
            // DeliverySchedule
            // 
            this.DeliverySchedule.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeliverySchedule.Location = new System.Drawing.Point(12, 108);
            this.DeliverySchedule.Name = "DeliverySchedule";
            this.DeliverySchedule.Size = new System.Drawing.Size(374, 42);
            this.DeliverySchedule.TabIndex = 2;
            this.DeliverySchedule.Text = "Configure Daily Delivery Schedule";
            this.DeliverySchedule.UseVisualStyleBackColor = true;
            this.DeliverySchedule.Click += new System.EventHandler(this.DeliverySchedule_Click);
            // 
            // Exit
            // 
            this.Exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Exit.Location = new System.Drawing.Point(12, 156);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(374, 42);
            this.Exit.TabIndex = 3;
            this.Exit.Text = "Exit";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(374, 42);
            this.button1.TabIndex = 4;
            this.button1.Text = "Enter Password";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 208);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Exit);
            this.Controls.Add(this.DeliverySchedule);
            this.Controls.Add(this.ConfigureDrivers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "MainMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RRS Shipping Manager";
            this.Activated += new System.EventHandler(this.MainMenu_GotFocus);
            this.Load += new System.EventHandler(this.MainMenu_Load);
            this.ResumeLayout(false);

        }

        #endregion
        
        //.Click += new System.EventHandler(this.Exit_Click);
        private System.Windows.Forms.Button ConfigureDrivers;
        private System.Windows.Forms.Button DeliverySchedule;
        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.Button button1;
    }
}

