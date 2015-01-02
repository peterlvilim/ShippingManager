namespace ShippingLog
{
    partial class searchQueryWindow
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
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.searchCategoryDropdown = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.searchTypeDropdown = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.addCriterion = new System.Windows.Forms.Button();
            this.searchButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(83, 22);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 0;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(454, 22);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Search From:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(387, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Search To:";
            // 
            // searchCategoryDropdown
            // 
            this.searchCategoryDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.searchCategoryDropdown.FormattingEnabled = true;
            this.searchCategoryDropdown.Items.AddRange(new object[] {
            "ID",
            "Amount",
            "Date Delivered",
            "Run Driver",
            "Customer Name",
            "Invoice"});
            this.searchCategoryDropdown.Location = new System.Drawing.Point(80, 80);
            this.searchCategoryDropdown.Name = "searchCategoryDropdown";
            this.searchCategoryDropdown.Size = new System.Drawing.Size(121, 21);
            this.searchCategoryDropdown.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Criterion 1:";
            // 
            // searchTypeDropdown
            // 
            this.searchTypeDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.searchTypeDropdown.FormattingEnabled = true;
            this.searchTypeDropdown.Items.AddRange(new object[] {
            "Is Exactly",
            "Contains",
            "Starts With",
            "Ends With"});
            this.searchTypeDropdown.Location = new System.Drawing.Point(234, 80);
            this.searchTypeDropdown.Name = "searchTypeDropdown";
            this.searchTypeDropdown.Size = new System.Drawing.Size(121, 21);
            this.searchTypeDropdown.TabIndex = 6;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(389, 81);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(167, 20);
            this.textBox1.TabIndex = 7;
            // 
            // addCriterion
            // 
            this.addCriterion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.addCriterion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addCriterion.Location = new System.Drawing.Point(575, 79);
            this.addCriterion.Name = "addCriterion";
            this.addCriterion.Size = new System.Drawing.Size(122, 23);
            this.addCriterion.TabIndex = 8;
            this.addCriterion.Text = "Add Criterion";
            this.addCriterion.UseVisualStyleBackColor = false;
            this.addCriterion.Click += new System.EventHandler(this.button1_Click);
            // 
            // searchButton
            // 
            this.searchButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.searchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchButton.Location = new System.Drawing.Point(622, 252);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(82, 23);
            this.searchButton.TabIndex = 9;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = false;
            this.searchButton.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // searchQueryWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 287);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.addCriterion);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.searchTypeDropdown);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.searchCategoryDropdown);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Name = "searchQueryWindow";
            this.Text = "searchQueryWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox searchCategoryDropdown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox searchTypeDropdown;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button addCriterion;
        private System.Windows.Forms.Button searchButton;
    }
}