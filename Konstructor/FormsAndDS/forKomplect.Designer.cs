namespace Konstructor
{
    partial class forKomplect
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
            this.komplectBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.kursDBDataSetKomplect = new Konstructor.FormsAndDS.KursDBDataSetKomplect();
            this.komplectTableAdapter = new Konstructor.FormsAndDS.KursDBDataSetKomplectTableAdapters.KomplectTableAdapter();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.komplectBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kursDBDataSetKomplect)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(73, 178);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(78, 50);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(296, 178);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(78, 50);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.komplectBindingSource, "Name", true));
            this.textBox1.Location = new System.Drawing.Point(119, 38);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(140, 20);
            this.textBox1.TabIndex = 2;
            // 
            // komplectBindingSource
            // 
            this.komplectBindingSource.DataMember = "Komplect";
            this.komplectBindingSource.DataSource = this.kursDBDataSetKomplect;
            // 
            // kursDBDataSetKomplect
            // 
            this.kursDBDataSetKomplect.DataSetName = "KursDBDataSetKomplect";
            this.kursDBDataSetKomplect.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // komplectTableAdapter
            // 
            this.komplectTableAdapter.ClearBeforeFill = true;
            // 
            // textBox2
            // 
            this.textBox2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.komplectBindingSource, "Kolvo", true));
            this.textBox2.Location = new System.Drawing.Point(119, 64);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(140, 20);
            this.textBox2.TabIndex = 3;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // textBox3
            // 
            this.textBox3.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.komplectBindingSource, "Price", true));
            this.textBox3.Location = new System.Drawing.Point(119, 90);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(140, 20);
            this.textBox3.TabIndex = 4;
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // textBox4
            // 
            this.textBox4.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.komplectBindingSource, "Material", true));
            this.textBox4.Location = new System.Drawing.Point(119, 116);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(140, 20);
            this.textBox4.TabIndex = 5;
            // 
            // forKomplect
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(471, 284);
            this.ControlBox = false;
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "forKomplect";
            this.Text = "Комплектующие";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.forPost_FormClosing);
            this.Load += new System.EventHandler(this.forPost_Load);
            ((System.ComponentModel.ISupportInitialize)(this.komplectBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kursDBDataSetKomplect)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private FormsAndDS.KursDBDataSetKomplect kursDBDataSetKomplect;
        private FormsAndDS.KursDBDataSetKomplectTableAdapters.KomplectTableAdapter komplectTableAdapter;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        public System.Windows.Forms.BindingSource komplectBindingSource;
    }
}