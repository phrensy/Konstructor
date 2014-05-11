namespace Konstructor.FormsAndDS
{
    partial class forZakupka
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(forZakupka));
            this.label1 = new System.Windows.Forms.Label();
            this.labelMDF = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxMDF = new System.Windows.Forms.ComboBox();
            this.postavshikBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.kBDDataSet = new Konstructor.KBD_DataSet();
            this.postavshikTableAdapter = new Konstructor.KBD_DataSetTableAdapters.PostavshikTableAdapter();
            this.button1 = new System.Windows.Forms.Button();
            this.labelDSP = new System.Windows.Forms.Label();
            this.labelDVP = new System.Windows.Forms.Label();
            this.labelVesh = new System.Windows.Forms.Label();
            this.comboBoxDSP = new System.Windows.Forms.ComboBox();
            this.comboBoxDVP = new System.Windows.Forms.ComboBox();
            this.comboBoxVesh = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.postavshikBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kBDDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.SaddleBrown;
            this.label1.Location = new System.Drawing.Point(30, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Список отсутствующих \r\nкомплектующих:";
            // 
            // labelMDF
            // 
            this.labelMDF.AutoSize = true;
            this.labelMDF.BackColor = System.Drawing.Color.White;
            this.labelMDF.ForeColor = System.Drawing.Color.SaddleBrown;
            this.labelMDF.Location = new System.Drawing.Point(30, 72);
            this.labelMDF.Name = "labelMDF";
            this.labelMDF.Size = new System.Drawing.Size(226, 13);
            this.labelMDF.TabIndex = 1;
            this.labelMDF.Text = "тут список отсутствующих комплектующих";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.Color.SaddleBrown;
            this.label3.Image = ((System.Drawing.Image)(resources.GetObject("label3.Image")));
            this.label3.Location = new System.Drawing.Point(313, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(151, 32);
            this.label3.TabIndex = 2;
            this.label3.Text = "Выберете нужного \r\nпоставщика:";
            // 
            // comboBoxMDF
            // 
            this.comboBoxMDF.FormattingEnabled = true;
            this.comboBoxMDF.Location = new System.Drawing.Point(316, 69);
            this.comboBoxMDF.Name = "comboBoxMDF";
            this.comboBoxMDF.Size = new System.Drawing.Size(121, 21);
            this.comboBoxMDF.TabIndex = 3;
            // 
            // postavshikBindingSource
            // 
            this.postavshikBindingSource.DataMember = "Postavshik";
            this.postavshikBindingSource.DataSource = this.kBDDataSet;
            // 
            // kBDDataSet
            // 
            this.kBDDataSet.DataSetName = "KBDDataSet";
            this.kBDDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // postavshikTableAdapter
            // 
            this.postavshikTableAdapter.ClearBeforeFill = true;
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(252, 226);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelDSP
            // 
            this.labelDSP.AutoSize = true;
            this.labelDSP.BackColor = System.Drawing.Color.White;
            this.labelDSP.ForeColor = System.Drawing.Color.SaddleBrown;
            this.labelDSP.Location = new System.Drawing.Point(30, 107);
            this.labelDSP.Name = "labelDSP";
            this.labelDSP.Size = new System.Drawing.Size(226, 13);
            this.labelDSP.TabIndex = 5;
            this.labelDSP.Text = "тут список отсутствующих комплектующих";
            // 
            // labelDVP
            // 
            this.labelDVP.AutoSize = true;
            this.labelDVP.BackColor = System.Drawing.Color.White;
            this.labelDVP.ForeColor = System.Drawing.Color.SaddleBrown;
            this.labelDVP.Location = new System.Drawing.Point(30, 147);
            this.labelDVP.Name = "labelDVP";
            this.labelDVP.Size = new System.Drawing.Size(226, 13);
            this.labelDVP.TabIndex = 6;
            this.labelDVP.Text = "тут список отсутствующих комплектующих";
            // 
            // labelVesh
            // 
            this.labelVesh.AutoSize = true;
            this.labelVesh.BackColor = System.Drawing.Color.White;
            this.labelVesh.ForeColor = System.Drawing.Color.SaddleBrown;
            this.labelVesh.Location = new System.Drawing.Point(30, 186);
            this.labelVesh.Name = "labelVesh";
            this.labelVesh.Size = new System.Drawing.Size(226, 13);
            this.labelVesh.TabIndex = 7;
            this.labelVesh.Text = "тут список отсутствующих комплектующих";
            // 
            // comboBoxDSP
            // 
            this.comboBoxDSP.FormattingEnabled = true;
            this.comboBoxDSP.Location = new System.Drawing.Point(316, 107);
            this.comboBoxDSP.Name = "comboBoxDSP";
            this.comboBoxDSP.Size = new System.Drawing.Size(121, 21);
            this.comboBoxDSP.TabIndex = 8;
            // 
            // comboBoxDVP
            // 
            this.comboBoxDVP.FormattingEnabled = true;
            this.comboBoxDVP.Location = new System.Drawing.Point(316, 144);
            this.comboBoxDVP.Name = "comboBoxDVP";
            this.comboBoxDVP.Size = new System.Drawing.Size(121, 21);
            this.comboBoxDVP.TabIndex = 9;
            // 
            // comboBoxVesh
            // 
            this.comboBoxVesh.FormattingEnabled = true;
            this.comboBoxVesh.Location = new System.Drawing.Point(316, 183);
            this.comboBoxVesh.Name = "comboBoxVesh";
            this.comboBoxVesh.Size = new System.Drawing.Size(121, 21);
            this.comboBoxVesh.TabIndex = 10;
            // 
            // forZakupka
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(556, 261);
            this.Controls.Add(this.comboBoxVesh);
            this.Controls.Add(this.comboBoxDVP);
            this.Controls.Add(this.comboBoxDSP);
            this.Controls.Add(this.labelVesh);
            this.Controls.Add(this.labelDVP);
            this.Controls.Add(this.labelDSP);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBoxMDF);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelMDF);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "forZakupka";
            this.Text = "Закупка комплектующих";
            this.Load += new System.EventHandler(this.forZakupka_Load);
            ((System.ComponentModel.ISupportInitialize)(this.postavshikBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kBDDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelMDF;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxMDF;
        private KBD_DataSet kBDDataSet;
        private System.Windows.Forms.BindingSource postavshikBindingSource;
        private KBD_DataSetTableAdapters.PostavshikTableAdapter postavshikTableAdapter;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labelDSP;
        private System.Windows.Forms.Label labelDVP;
        private System.Windows.Forms.Label labelVesh;
        private System.Windows.Forms.ComboBox comboBoxDSP;
        private System.Windows.Forms.ComboBox comboBoxDVP;
        private System.Windows.Forms.ComboBox comboBoxVesh;
    }
}