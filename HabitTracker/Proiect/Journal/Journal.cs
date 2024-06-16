/**************************************************************************
 *                                                                        *
 *  File:        Journal.cs                                               *
 *  Copyright:   (c) 2024, Daniel Radu                                    *
 *  Description: Clasa pentru jurnalul de activitati.                     *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/


using Proiect.Habits;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect.Journal
{
    // singleton pentru jurnal 

    /// <summary>
    /// Clasa Journal implementeaza jurnalul fiecarui obicei: salvarea unor comentarii si salvarea fisierelor
    /// Journal este implementat folosind Singleton
    /// </summary>
    public class Journal
    {
        public Habit habit;

        private static Journal _jurnalInstance;
        private string _fullPath;

        private Journal()
        {

        }

        public static Journal getInstance()
        {
            if(_jurnalInstance == null)
            {
                _jurnalInstance = new Journal();
            }

            return _jurnalInstance;

        }

        public void setHabit(Habit h)
        {
            this.habit = h;
        }

        public void createFile()
        {
            var nume_fisier = this.habit.ToString();

            var currentDirectoryPath = Directory.GetCurrentDirectory();
            var filePath = Path.Combine(currentDirectoryPath, @"..\..\TextFiles\" + nume_fisier + ".txt" );
            this._fullPath = Path.GetFullPath(Path.Combine(currentDirectoryPath, filePath));
            if(!File.Exists(_fullPath))
            {
                var my_file = File.Create(_fullPath);
                my_file.Close();
            }
        }

        // salvare fisier 
        public void saveFile(string text)
        {
           

            System.IO.File.WriteAllText(this._fullPath, String.Empty);
            System.IO.File.WriteAllText(this._fullPath, text);



        }

        public string getFile()
        {
            this.createFile();
            string data = DateTime.Now.ToString();
            System.IO.File.AppendAllText(this._fullPath, data + '\n');
            
            return this._fullPath;
        }

        // stergerea obiceiului
        /// <summary>
        /// Metoda este utilizata atunci cand utilizatorul sterge de tot un habit.
        /// </summary>
        public void closeHabit()
        {
            this.createFile();
            System.IO.File.AppendAllText(this._fullPath, "Habit closed -> " + DateTime.Now.ToString());
            System.IO.File.AppendAllText(this._fullPath, this.habit.getInfo());
        }
    }
}
