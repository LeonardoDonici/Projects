/**************************************************************************
 *                                                                        *
 *  File:        Inregistrare.cs                                          *
 *  Copyright:   (c) 2024, Leonardo Donici                                *
 *  E-mail:                                                               *
 *  Website:                                                              *
 *  Description: Clasa utilizata pentru a stoca username si email.        *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proiect
{
    public partial class Inregistrare : Form
    {
        private ProxyAccessManager _proxyAccessManager;
        public Inregistrare()
        {
            InitializeComponent();
            _proxyAccessManager = ProxyAccessManager.Instance;

            textBoxParola.PasswordChar = '*';
            textBoxParola2.PasswordChar = '*';
        }
        private void buttonRegisterUserClick(object sender, EventArgs e)
        {


            bool registerResult = _proxyAccessManager.Register(textBoxUserName.Text, textBoxPrenume.Text, textBoxParola.Text, textBoxParola2.Text, textBoxEmail.Text);
            if (!registerResult)
                MessageBox.Show("Inregistrarea unui nou utilizator a esuat. Incercati din nou...");
            else
                this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
