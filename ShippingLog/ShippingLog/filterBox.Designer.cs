namespace ShippingLog
{
    partial class filterBox
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
            this.filterDummy = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // filterDummy
            // 
            this.filterDummy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.filterDummy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filterDummy.Location = new System.Drawing.Point(0, 0);
            this.filterDummy.Margin = new System.Windows.Forms.Padding(0);
            this.filterDummy.Name = "filterDummy";
            this.filterDummy.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.filterDummy.Size = new System.Drawing.Size(121, 24);
            this.filterDummy.TabIndex = 26;
            this.filterDummy.Click += new System.EventHandler(this.filterDummy_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(106, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "X";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // filterBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.filterDummy);
            this.Name = "filterBox";
            this.Size = new System.Drawing.Size(121, 24);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label filterDummy;
        private System.Windows.Forms.Label label1;
    }
}
