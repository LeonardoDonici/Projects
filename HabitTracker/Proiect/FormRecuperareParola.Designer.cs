namespace Proiect
{
    partial class FormRecuperareParola
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textboxUsername = new System.Windows.Forms.TextBox();
            this.textBoxCodText = new System.Windows.Forms.TextBox();
            this.textBoxCod = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonValidare = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(137, 60);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "Nume utilizator:";
            // 
            // textboxUsername
            // 
            this.textboxUsername.Location = new System.Drawing.Point(273, 60);
            this.textboxUsername.Name = "textboxUsername";
            this.textboxUsername.Size = new System.Drawing.Size(100, 20);
            this.textboxUsername.TabIndex = 1;
            // 
            // textBoxCodText
            // 
            this.textBoxCodText.Location = new System.Drawing.Point(137, 158);
            this.textBoxCodText.Name = "textBoxCodText";
            this.textBoxCodText.Size = new System.Drawing.Size(100, 20);
            this.textBoxCodText.TabIndex = 2;
            this.textBoxCodText.Text = "Codul primit pe mail:";
            this.textBoxCodText.Visible = false;
            // 
            // textBoxCod
            // 
            this.textBoxCod.Location = new System.Drawing.Point(273, 158);
            this.textBoxCod.Name = "textBoxCod";
            this.textBoxCod.Size = new System.Drawing.Size(100, 20);
            this.textBoxCod.TabIndex = 3;
            this.textBoxCod.Visible = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button1.Cursor = System.Windows.Forms.Cursors.No;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button1.Location = new System.Drawing.Point(194, 101);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(123, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Trimite cod";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.buttonTrimiteMail_Click);
            // 
            // buttonValidare
            // 
            this.buttonValidare.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonValidare.Cursor = System.Windows.Forms.Cursors.No;
            this.buttonValidare.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonValidare.Location = new System.Drawing.Point(194, 189);
            this.buttonValidare.Name = "buttonValidare";
            this.buttonValidare.Size = new System.Drawing.Size(123, 23);
            this.buttonValidare.TabIndex = 5;
            this.buttonValidare.Text = "Valideaza cod";
            this.buttonValidare.UseVisualStyleBackColor = false;
            this.buttonValidare.Visible = false;
            this.buttonValidare.Click += new System.EventHandler(this.buttonTrimiteCod_Click);
            // 
            // FormRecuperareParola
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 301);
            this.Controls.Add(this.buttonValidare);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoxCod);
            this.Controls.Add(this.textBoxCodText);
            this.Controls.Add(this.textboxUsername);
            this.Controls.Add(this.textBox1);
            this.Name = "FormRecuperareParola";
            this.Text = "FormRecuperareParola";
            this.Load += new System.EventHandler(this.FormRecuperareParola_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textboxUsername;
        private System.Windows.Forms.TextBox textBoxCodText;
        private System.Windows.Forms.TextBox textBoxCod;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonValidare;
    }
}