/**************************************************************************
 *                                                                        *
 *  File:        Login.cs                                                 *
 *  Copyright:   (c) 2024, Stefan Gherghel                                *
 *  Description: Clasa pentru interfata de logare                         *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

using Proiect.Email;
using System;
using System.IO;
using System.Windows.Forms;



namespace Proiect
{
    public partial class LogIn : Form
    {
        ProxyAccessManager _proxyAccessManager;
        public LogIn()
        {
            InitializeComponent();
            _proxyAccessManager = ProxyAccessManager.Instance;              //ProxyAccessManager implementeaza Singleton si are constructor privat
            textBoxPassword.PasswordChar = '*';
        }

        private void buttonLoginClick(object sender, EventArgs e)                                                           //eveniment de logare
        {

            if (textBoxUser.Text == "admin")
            {
                AdminPanel panouAdmin = new AdminPanel();
                panouAdmin.Show();  

                this.Hide();
            }

            else if (_proxyAccessManager.LogIn(textBoxUser.Text, textBoxPassword.Text))
            {
                //MessageBox.Show("Logare reusita!!");                                                                        // <- AICI VINE ACCESUL SPRE URMATORUL FORM, USERUL ARE ACUM acces la contul lui
                //....new form3 ..... 

                HabitTracker aplicatieCalendar = new HabitTracker(textBoxUser.Text);
                aplicatieCalendar.Show();
                this.Hide();
            }
            else
                MessageBox.Show("LOGARE ESUATA!");
        }

        private void buttonRegister_Click(object sender, EventArgs e)                                                               //eveniment de inregistrare user nou
        {
            Inregistrare inregistrare = new Inregistrare();
            inregistrare.Show();
        }
        private void buttonForgotPassword_Click(object sender, EventArgs e)                                                               //eveniment de inregistrare user nou
        {

            FormRecuperareParola formRecuperareParola = new FormRecuperareParola();
            formRecuperareParola.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void butonDespre_Click(object sender, EventArgs e)
        {
            var currentDirectoryPath = Directory.GetCurrentDirectory();                                       
            var filePath = Path.Combine(currentDirectoryPath, @"..\..\bin\debug\x64\UserHelp.chm");
            string fullPath = Path.GetFullPath(Path.Combine(currentDirectoryPath, filePath));
            Help.ShowHelp(this, fullPath);
        }

        private void Info_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Această aplicație are ca scopul de a ajuta cu managerierea și crearea de obiceiuri bune\n" +
                "Proiect realziat de: Donici Leonardo Mario, Gherghe Ștefan, Radu Daniel și Radu Ștefan-Vlăduț" +
                " în cadrul disciplinei Ingineria Programării, anul 2024, cu profesorul îndrumător Tiberius Dumitriu");
        }

        private void buttonInfo_Click(object sender, EventArgs e)
        {

        }
    }
}
