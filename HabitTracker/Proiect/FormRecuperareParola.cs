/**************************************************************************
 *                                                                        *
 *  File:        FormRecuperareParola.cs                                  *
 *  Copyright:   (c) 2024, Leonardo Donici                                *
 *  E-mail:                                                               *
 *  Website:                                                              *
 *  Description: Clasa utilizata pentru conectare cu mail daca parola     *
 *  a fost uitata                                                         *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

using Proiect.AccessManagement;
using Proiect.Email;
using System;
using System.Windows.Forms;

namespace Proiect
{
    public partial class FormRecuperareParola : Form
    {
        int _secretNumber;
        DBAccessManager dBAccessManager = new DBAccessManager();
        public FormRecuperareParola()
        {
            InitializeComponent();
        }

        private void buttonTrimiteMail_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            _secretNumber = random.Next(1000, 9999);
            SMTPEmail.sendEmail(dBAccessManager.getEmail(textboxUsername.Text), "Cod recuperare parola", "Codul dvs este: " + _secretNumber.ToString());        //cauta mailul pentru acest utilizator si ii trimite codul secret
            
            textBoxCodText.Visible = true;                           //face celelalte campuri vizibile
            textBoxCod.Visible = true;
            buttonValidare.Visible = true;
        }
        private void buttonTrimiteCod_Click(object sender, EventArgs e)
        {
            if (_secretNumber.ToString() == textBoxCod.Text)                //daca codul introdus este identic cu codul secret se deschide aplicatia. 
            {
                HabitTracker aplicatieCalendar = new HabitTracker("");
                aplicatieCalendar.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Codul introdus este gresit!");
            }
        }

        private void FormRecuperareParola_Load(object sender, EventArgs e)
        {

        }
    }
}
