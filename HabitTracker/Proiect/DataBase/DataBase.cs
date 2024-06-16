/**************************************************************************
 *                                                                        *
 *  File:        DataBase.cs                                              *
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
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;
//GHOST OF CHRISTMAS PAST
namespace Proiect
{

    /// <summary>
    /// Clasă pentru gestionarea conexiunilor și operațiunilor cu baza de date SQLite.
    /// Implementarea sablonului Singleton pentru a asigura o singură instanță a clasei.
    /// </summary>
    /// <remarks>
    /// Această clasă oferă metode pentru deschiderea și închiderea conexiunilor, precum și pentru
    /// executarea interogărilor non-query, interogărilor SELECT și interogărilor scalare.
    /// </remarks>
    /// <param name="instance">Instanța singleton a clasei DataBase.</param>
    /// <param name="connection">Obiectul de conexiune la baza de date SQLite.</param>

    public class DataBase
    {
     
       
        private static DataBase _instance;
      
        private SQLiteConnection connection;

       
        private DataBase()
        {
            try
            {

                var currentDirectoryPath = Directory.GetCurrentDirectory();
                var filePath = Path.Combine(currentDirectoryPath, @"..\..\DataBase\users.db");
                string connectionString = $"Data Source={Path.GetFullPath(filePath)};";
                connection = new SQLiteConnection(connectionString);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Nu s-a găsit baza de date. Detalii: " + ex.Message);
            }
        }

        // Metoda pentru singleton
        public static DataBase getInstance()
        {
            if (_instance == null)
            {
                _instance = new DataBase();
            }
            return _instance;
        }

        // Metoda pentru a deschide o conexiune cu baza de date
        public bool openConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Nu s-a putut face conexiunea! Detalii: " + ex.Message);
                return false;
            }
        }

        // Metoda pentru a inchide conexiunea cu baza de date
        public bool closeConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Nu s-a putut închide conexiunea! Detalii: " + ex.Message);
                return false;
            }
        }

        // Metoda pentru a executa comenzi NON-QUERY(INSERT, UPDATE, DELETE)
        public void executeNonQuery(string query, SQLiteParameter[] parameters)
        {
            if (this.openConnection() == true)
            {
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    command.ExecuteNonQuery();
                }
                this.closeConnection();
            }
        }

        // Metoda pentru a executa SELECT si returneaza un DataTable
        public DataTable executeQuery(string query, SQLiteParameter[] parameters = null)
        {
            DataTable dataTable = new DataTable();
            if (this.openConnection() == true)
            {
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    using (SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(command))
                    {
                        dataAdapter.Fill(dataTable);
                    }
                }
                this.closeConnection();
            }
            return dataTable;
        }

        // Metoda pentru a executa scalar query (e.g., COUNT, SUM)
        public object executeScalar(string query, SQLiteParameter[] parameters = null)
        {
            object result = null;
            if (this.openConnection() == true)
            {
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    result = command.ExecuteScalar();
                }
                this.closeConnection();
            }
            return result;
        }
    }
}