namespace Konstructor
{
    partial class MainForm
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
            this.bKonstructor = new System.Windows.Forms.Button();
            this.bDB = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bKonstructor
            // 
            this.bKonstructor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bKonstructor.Location = new System.Drawing.Point(104, 12);
            this.bKonstructor.Name = "bKonstructor";
            this.bKonstructor.Size = new System.Drawing.Size(117, 53);
            this.bKonstructor.TabIndex = 0;
            this.bKonstructor.Text = "Конструктор";
            this.bKonstructor.UseVisualStyleBackColor = true;
            this.bKonstructor.Click += new System.EventHandler(this.bKonstructor_Click);
            // 
            // bDB
            // 
            this.bDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bDB.Location = new System.Drawing.Point(104, 86);
            this.bDB.Name = "bDB";
            this.bDB.Size = new System.Drawing.Size(117, 53);
            this.bDB.TabIndex = 1;
            this.bDB.Text = "База Данных";
            this.bDB.UseVisualStyleBackColor = true;
            this.bDB.Click += new System.EventHandler(this.bDB_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 353);
            this.Controls.Add(this.bDB);
            this.Controls.Add(this.bKonstructor);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bKonstructor;
        private System.Windows.Forms.Button bDB;
    }
}