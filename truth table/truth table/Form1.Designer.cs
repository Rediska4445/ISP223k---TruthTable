namespace truth_table
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.Input1 = new System.Windows.Forms.TextBox();
            this.GetBtn = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TruthTableView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.TruthTableView)).BeginInit();
            this.SuspendLayout();
            // 
            // Input1
            // 
            this.Input1.Location = new System.Drawing.Point(13, 13);
            this.Input1.Name = "Input1";
            this.Input1.Size = new System.Drawing.Size(181, 22);
            this.Input1.TabIndex = 0;
            this.Input1.TextChanged += new System.EventHandler(this.Input1_TextChanged);
            // 
            // GetBtn
            // 
            this.GetBtn.Location = new System.Drawing.Point(670, 12);
            this.GetBtn.Name = "GetBtn";
            this.GetBtn.Size = new System.Drawing.Size(199, 23);
            this.GetBtn.TabIndex = 1;
            this.GetBtn.Text = "button1";
            this.GetBtn.UseVisualStyleBackColor = true;
            this.GetBtn.Click += new System.EventHandler(this.GenBtn_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "a&b",
            "a|c",
            "a&b|c",
            "a&b=c",
            "a|c\'x",
            "a=c\'z",
            "a&(a|b)",
            "b|(a=\'c)"});
            this.comboBox1.Location = new System.Drawing.Point(200, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(189, 24);
            this.comboBox1.TabIndex = 10;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(396, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 16);
            this.label1.TabIndex = 11;
            this.label1.Text = "label1";
            // 
            // TruthTableView
            // 
            this.TruthTableView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TruthTableView.Location = new System.Drawing.Point(13, 44);
            this.TruthTableView.Name = "TruthTableView";
            this.TruthTableView.RowHeadersWidth = 49;
            this.TruthTableView.RowTemplate.Height = 24;
            this.TruthTableView.Size = new System.Drawing.Size(856, 482);
            this.TruthTableView.TabIndex = 12;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 538);
            this.Controls.Add(this.TruthTableView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.GetBtn);
            this.Controls.Add(this.Input1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(527, 420);
            this.Name = "Form1";
            this.Text = "Truth Table Generator";
            ((System.ComponentModel.ISupportInitialize)(this.TruthTableView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Input1;
        private System.Windows.Forms.Button GetBtn;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView TruthTableView;
    }
}

