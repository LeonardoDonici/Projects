namespace Proiect
{
    partial class LogIn
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
            this.textBoxUser = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonForgotPassword = new System.Windows.Forms.Button();
            this.buttonLoggIn = new System.Windows.Forms.Button();
            this.buttonRegister = new System.Windows.Forms.Button();
            this.butonDespre = new System.Windows.Forms.Button();
            this.Info = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxUser
            // 
            this.textBoxUser.Location = new System.Drawing.Point(217, 106);
            this.textBoxUser.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxUser.Name = "textBoxUser";
            this.textBoxUser.Size = new System.Drawing.Size(291, 22);
            this.textBoxUser.TabIndex = 0;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(217, 156);
            this.textBoxPassword.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(291, 22);
            this.textBoxPassword.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(129, 110);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Utilizator";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(129, 160);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Parola";
            // 
            // buttonForgotPassword
            // 
            this.buttonForgotPassword.Location = new System.Drawing.Point(540, 133);
            this.buttonForgotPassword.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonForgotPassword.Name = "buttonForgotPassword";
            this.buttonForgotPassword.Size = new System.Drawing.Size(164, 26);
            this.buttonForgotPassword.TabIndex = 4;
            this.buttonForgotPassword.Text = "Autentificare prin email";
            this.buttonForgotPassword.UseVisualStyleBackColor = true;
            this.buttonForgotPassword.Click += new System.EventHandler(this.buttonForgotPassword_Click);
            // 
            // buttonLoggIn
            // 
            this.buttonLoggIn.Location = new System.Drawing.Point(289, 204);
            this.buttonLoggIn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonLoggIn.Name = "buttonLoggIn";
            this.buttonLoggIn.Size = new System.Drawing.Size(117, 41);
            this.buttonLoggIn.TabIndex = 5;
            this.buttonLoggIn.Text = "Logare";
            this.buttonLoggIn.UseVisualStyleBackColor = true;
            this.buttonLoggIn.Click += new System.EventHandler(this.buttonLoginClick);
            // 
            // buttonRegister
            // 
            this.buttonRegister.Location = new System.Drawing.Point(289, 270);
            this.buttonRegister.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonRegister.Name = "buttonRegister";
            this.buttonRegister.Size = new System.Drawing.Size(117, 41);
            this.buttonRegister.TabIndex = 6;
            this.buttonRegister.Text = "Inregistrare";
            this.buttonRegister.UseVisualStyleBackColor = true;
            this.buttonRegister.Click += new System.EventHandler(this.buttonRegister_Click);
            // 
            // butonDespre
            // 
            this.butonDespre.Location = new System.Drawing.Point(575, 270);
            this.butonDespre.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.butonDespre.Name = "butonDespre";
            this.butonDespre.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.butonDespre.Size = new System.Drawing.Size(116, 41);
            this.butonDespre.TabIndex = 7;
            this.butonDespre.Text = "Despre";
            this.butonDespre.UseVisualStyleBackColor = true;
            this.butonDespre.Click += new System.EventHandler(this.butonDespre_Click);
            // 
            // Info
            // 
            this.Info.Location = new System.Drawing.Point(575, 334);
            this.Info.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Info.Name = "Info";
            this.Info.Size = new System.Drawing.Size(116, 28);
            this.Info.TabIndex = 8;
            this.Info.Text = "Info";
            this.Info.UseVisualStyleBackColor = true;
            this.Info.Click += new System.EventHandler(this.Info_Click);
            // 
            // LogIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 395);
            this.Controls.Add(this.Info);
            this.Controls.Add(this.butonDespre);
            this.Controls.Add(this.buttonRegister);
            this.Controls.Add(this.buttonLoggIn);
            this.Controls.Add(this.buttonForgotPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxUser);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "LogIn";
            this.Text = "LogIn";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxUser;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonForgotPassword;
        private System.Windows.Forms.Button buttonLoggIn;
        private System.Windows.Forms.Button buttonRegister;
        private System.Windows.Forms.Button butonDespre;
        private System.Windows.Forms.Button Info;
    }
}

