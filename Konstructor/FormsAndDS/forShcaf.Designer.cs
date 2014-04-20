namespace Konstructor
{
    partial class forShcaf
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
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.kursDBDataSet1 = new Konstructor.KursDBDataSet1();
            this.shcafBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.shcafTableAdapter = new Konstructor.KursDBDataSet1TableAdapters.ShcafTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.kursDBDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.shcafBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(43, 194);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 50);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(216, 194);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(111, 50);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.shcafBindingSource, "Price", true));
            this.textBox1.Location = new System.Drawing.Point(111, 44);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(164, 20);
            this.textBox1.TabIndex = 2;
            // 
            // textBox2
            // 
            this.textBox2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.shcafBindingSource, "TimeToConstruct", true));
            this.textBox2.Location = new System.Drawing.Point(111, 70);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(164, 20);
            this.textBox2.TabIndex = 3;
            // 
            // kursDBDataSet1
            // 
            this.kursDBDataSet1.DataSetName = "KursDBDataSet1";
            this.kursDBDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // shcafBindingSource
            // 
            this.shcafBindingSource.DataMember = "Shcaf";
            this.shcafBindingSource.DataSource = this.kursDBDataSet1;
            // 
            // shcafTableAdapter
            // 
            this.shcafTableAdapter.ClearBeforeFill = true;
            // 
            // forShcaf
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(423, 291);
            this.ControlBox = false;
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "forShcaf";
            this.Text = "forShcaf";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.forShcaf_FormClosing);
            this.Load += new System.EventHandler(this.forShcaf_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kursDBDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.shcafBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private KursDBDataSet1 kursDBDataSet1;
        private KursDBDataSet1TableAdapters.ShcafTableAdapter shcafTableAdapter;
        public System.Windows.Forms.BindingSource shcafBindingSource;
    }
}