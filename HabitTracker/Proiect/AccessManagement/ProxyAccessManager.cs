/**************************************************************************
 *                                                                        *
 *  File:        ProxyAccesManager.cs                                     *
 *  Copyright:   (c) 2024, Stefan Gherghel                                *
 *  Description: Clasa pentru criptarea parolei si a usernameului.        *
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
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Proiect
{
    /// <summary>
    /// Clasă care gestionează accesul la aplicație și la baza de date.
    /// Oferă funcționalități pentru:
    /// - Salvarea utilizatorilor noi în baza de date si fisier users.txt cu parola criptata.
    /// Implementarea sablonului Singleton pentru a evita crearea de instanțe multiple.
    /// </summary>
    /// <param name="DBAM">Instanța clasei DBAccessManager folosită pentru operațiuni de bază de date.</param>
    /// <param name="_users">Lista utilizatorilor.</param>
    /// <param name="pm">Instanța singleton a clasei ProxyAccessManager.</param>
    public class ProxyAccessManager : IAccessManager            //clasa care manageriaza accesul la aplicatie si la baza de date
    {
        private DBAccessManager DBAM;                   //instanta a clasei de acces management la baza de date. Este folosita pentru a salva in baza de date useri noi si pentru a obtine adresa de mail din baza de date in cazul recuperarii parolei
        private List<User> _users;                      //lista utilizatorilor

        private static ProxyAccessManager pm;           //clasa implementeaza sablonul singletone pentru a nu crea instante diferite la deschiderea form-ului de logare sau inregstrare

        public struct User                              //structura de date pentru utilizator ce contine doar datele de acces
        {
            public readonly string Name;
            public readonly string PassHash;

            public User(string name, string passHash)
            {
                Name = name;
                PassHash = passHash;
            }
        }

        private ProxyAccessManager()                                //in constructor citim din fiser toti utilizatorii si parolele criptate si le stocam intr-o lista pentru acces rapid
        {
            DBAM = new DBAccessManager();
            try
            {
                _users = new List<User>();
                var currentDirectoryPath = Directory.GetCurrentDirectory();
                var filePath = Path.Combine(currentDirectoryPath, @"..\..\TextFiles\users.txt");
                string fullPath = Path.GetFullPath(Path.Combine(currentDirectoryPath, filePath));
                StreamReader sr = new StreamReader(fullPath);
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] toks = line.Split('\t');
                    User user = new User(Cryptography.Decrypt(toks[0], "ProiectulNostruLaIP"), toks[1]);
                    _users.Add(user);
                }
                sr.Close();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }
        public static ProxyAccessManager Instance                   //Instance reprezinta constructorul static singleton
        {
            get
            {
                if (pm == null)
                    pm = new ProxyAccessManager();
                return pm;
            }
        }



        void ForgotPassword()
        {
            //  TBD
        }

        public bool LogIn(string user, string password)         //verifica daca exista un utilizator cu aceste creditentiale
        {
            if (user == "")
                throw new Exception("Introduceti un nume!");

            if (password == "" && user != "admin")
                throw new Exception("Introduceti o parola!");


            foreach (User u in _users)
            {
                if (u.Name == user && u.PassHash == Cryptography.HashString(password))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Obtinerea listei de nume de utilizator
        /// </summary>
        /// <returns></returns>
        public List<String> getUserList()
        {
            List<String> result = new List<String>();

            foreach(User u in _users)
                result.Add(u.Name);

            return result;
       
        }
        public bool Register(string username, string nume, string parola, string parola2, string mail)   //inregistreaza un utilizator nou
        {
            if (parola != parola2)              //verific ca cele doua parole sa fie identice
                return false;

            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            if (!Regex.IsMatch(mail, pattern, RegexOptions.IgnoreCase))
                return false;

            if (!DBAM.Register(username, nume, parola, parola2, mail))       //apelez DBAM pentru a inregistra in baza de date noul utilisatori
                return false;


            

            try
            {

                var currentDirectoryPath = Directory.GetCurrentDirectory();                                         //C:\Users\stefg\Desktop\Proiect IP\Proiect\Proiect\TextFiles\users.txt
                var filePath = Path.Combine(currentDirectoryPath, @"..\..\TextFiles\users.txt");
                string fullPath = Path.GetFullPath(Path.Combine(currentDirectoryPath, filePath));
                StreamWriter sw = File.AppendText(fullPath);   //<- nu reusesc sa fac sa mearga cu calea relativa !!!!
                string numeCriptat = Cryptography.Encrypt(username, "ProiectulNostruLaIP");
                string parolaHashuita = Cryptography.HashString(parola);
                string line = numeCriptat + "\t" + parolaHashuita;                      //in fisier se trece numele de utilizator criptat si un hash al parolei
                sw.WriteLine(line);
                sw.Close();
                _users.Add(new User(username, parolaHashuita));
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.ToString());
            }

            return true;
        }

        public void removeUser(string userName)
        {
            List<User> newUserList = new List<User>(); 
            foreach (var user in _users)
            {
                if(user.Name != userName)
                {
                    newUserList.Add(user);
                }
            }

            var currentDirectoryPath = Directory.GetCurrentDirectory();                                         //C:\Users\stefg\Desktop\Proiect IP\Proiect\Proiect\TextFiles\users.txt
            var filePath = Path.Combine(currentDirectoryPath, @"..\..\TextFiles\users.txt");
            string fullPath = Path.GetFullPath(Path.Combine(currentDirectoryPath, filePath));

            System.IO.File.WriteAllText(fullPath, "");

            StreamWriter sw = File.AppendText(fullPath);

            foreach (var user in newUserList)
            {
                string numeCriptat = Cryptography.Encrypt(user.Name, "ProiectulNostruLaIP");
                string line = numeCriptat + "\t" + user.PassHash;                      //in fisier se trece numele de utilizator criptat si un hash al parolei
                sw.WriteLine(line);
            }

            sw.Close();

            _users = newUserList;
        }
    }
}