/**************************************************************************
 *                                                                        *
 *  File:        DBAccessManager.cs                                       *
 *  Copyright:   (c) 2024, Stefan Gherghel, Leonardo Donici               *
 *  Description: Clasa pentru logare si inregistrare.                     *
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
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Proiect.AccessManagement
{
    /// <summary>
    /// Clasă responsabilă pentru gestionarea accesului la baza de date.
    /// Oferă funcționalități pentru:
    /// - Obținerea adresei de email asociată unui utilizator.
    /// - Autentificarea utilizatorilor.
    /// - Înregistrarea de noi utilizatori.
    /// </summary>
    /// <param name="username">Numele de utilizator pentru operațiunile de obținere și actualizare email.</param>
    /// <param name="newEmail">Noua adresă de email pentru actualizarea email-ului unui utilizator.</param>
    /// <param name="user">Numele de utilizator pentru autentificare.</param>
    /// <param name="password">Parola utilizatorului pentru autentificare.</param>
    /// <param name="nume">Numele noului utilizator pentru înregistrare.</param>
    /// <param name="prenume">Prenumele noului utilizator pentru înregistrare.</param>
    /// <param name="parola">Parola noului utilizator pentru înregistrare.</param>
    /// <param name="parola2">Confirmarea parolei noului utilizator pentru înregistrare.</param>
    /// <param name="mail">Adresa de email a noului utilizator pentru înregistrare.</param>
    class DBAccessManager : IAccessManager
    {
        public string getEmail(string username)
        {
            // Obține instanța singleton a bazei de date
            DataBase db = DataBase.getInstance();

            // Definește interogarea SQL pentru a găsi email-ul asociat username-ului
            string query = "SELECT email FROM users WHERE username = @username";

            // Creează parametrii pentru interogare
            SQLiteParameter[] parameters = new SQLiteParameter[]
            {
                new SQLiteParameter("@username", DbType.String) { Value = username }
            };

            // Execută interogarea scalară și obține rezultatul
            object result = db.executeScalar(query, parameters);

            // Verifică dacă rezultatul nu este null și returnează email-ul
            if (result != null)
            {
                return result.ToString();
            }

            // În cazul în care username-ul nu a fost găsit, returnează un mesaj relevant
            return "Utilizatorul nu a fost găsit.";
        }
        public bool updateEmail(string username, string newEmail)                        //trebuie sa updateze emailul utilizatorului in baza de date
        {
            return true;
        }

        public bool LogIn(string user, string password)     //trebuie verificat ca exista in baza de date userul 
        {
            return true;
            throw new NotImplementedException();
        }

        public bool Register(string nume, string prenume, string parola, string parola2, string mail)       //inregisgtreaza un user nou in baza de date. Nu trebuie sa stocheze si parola. Aceasta este trecuta in parametrii functiei pentru a putea implementa interfata si pentru a implementa patterunul proxy
        {
            try
            {
                DataBase db = DataBase.getInstance();
                string query = "INSERT INTO users (Username, Email) " +
                               "VALUES (@Username, @Email)";

                SQLiteParameter[] parameters = new SQLiteParameter[]
                {
                                new SQLiteParameter("@Username", nume),
                                new SQLiteParameter("@Email",mail),

                };

                db.executeNonQuery(query, parameters);

                MessageBox.Show("Înregistrarea a fost realizată cu succes.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("A apărut o eroare: " + ex.Message);
            }
            return true;
            
        }
    }
}
