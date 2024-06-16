/**************************************************************************
 *                                                                        *
 *  File:        HabitsManager.cs                                         *
 *  Copyright:   (c) 2024, Daniel Radu                                    *
 *  Description: Clasa pentru habituri.                                   *
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
using System.Data.SQLite;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Globalization;

namespace Proiect.Habits
{
    /// <summary>
    /// Clasa HabitsManager se ocupa de instantiere obiectelor Habit, suprascrierea fisierelor, citirea din fisiere.
    /// </summary>
    public class HabitsManager
    {
        private List<Habit> habits_list;
        private string _username;
        private string _fullPath;

        // constructor in care retin si variabila ce reprezinta path-ul catre fisierul de habits
        public HabitsManager(string user)
        {
            this.habits_list = new List<Habit>();
            _username = user;
            var currentDirectoryPath = Directory.GetCurrentDirectory();
            var filePath = Path.Combine(currentDirectoryPath, @"..\..\TextFiles\user_habits.txt");
            _fullPath = Path.GetFullPath(Path.Combine(currentDirectoryPath, filePath));
        }
       
        public void createHabitsFile()
        {
           
            //daca fisierul cu habits-urile utilizatorului curent nu exista, il creez
            if(!File.Exists(_fullPath))
            {
                File.Create(_fullPath);
            }    
        }

        //se initializeaza lista de habits cu cele gaasite in fisier (daca exista)
        public void initializeHabitsList()
        {
            
            string[] lines = File.ReadAllLines(this._fullPath);
            if(lines.Length == 0)
            {
                return;
            }

            foreach(string line in lines)
            {
                var columns = line.Split(';');
                string user_name = columns[0].Split(':')[1];
                string habit_name = columns[1].Split(':')[1];
                string habits_start_date = columns[2].Split(':')[1];
                string frequency = columns[3].Split(':')[1];
                string [] missed_days = columns[4].Split(':')[1].Split('&');
                string new_start_streak = columns[5].Split(':')[1];
                string current_streak = columns[6].Split(':')[1];
                string best_streak = columns[7].Split(':')[1];
                string last_time_checked = columns[8].Split(':')[1];
                string [] checked_days = columns[9].Split(':')[1].Split('&');

                List<DateTime> missed_days_list = new List<DateTime>();
                List<DateTime> checked_days_list = new  List<DateTime> ();
                            
                foreach (string missedDay in missed_days)
                {
                    //tratarea exceptiei : &
                    try
                    {
                        missed_days_list.Add(DateTime.Parse(missedDay));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exceptie la initializare habit: &");
                    }
                    finally
                    {

                    }
                    
                }
                //tratarea exceptiei : &

                foreach (string checkedDay in checked_days)
                {
                    try
                    {
                        checked_days_list.Add(DateTime.Parse(checkedDay));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exceptie la initializare habit: &");

                    }
                    finally
                    {

                    }

                }

          

                // adaug in lista de habits
                if(user_name == _username)
                this.habits_list.Add(new HabitBuilder().setName(habit_name).setUsername(user_name).setStartDate(DateTime.Parse(habits_start_date)).
                    setStartDateNewStreak(DateTime.Parse(new_start_streak)).setFrequency(int.Parse(frequency)).
                    setMissedDays(missed_days_list).setBestStreak(int.Parse(best_streak)). 
                    setCurrentStreak(int.Parse(current_streak)).setLastTimeChecked(DateTime.Parse(last_time_checked)).
                    setCheckedDays(checked_days_list).build());

                foreach (Habit habit in this.habits_list)
                {
                    habit.printInfo();
                }
            }

        }
        public List<Habit> getHabitsList()
        {
            return this.habits_list;
        }

        public void addHabit(Habit new_habit)
        {
            this.habits_list.Add(new_habit);
        }
        
        // suprascrierea fisierului cu noile valori ale habitsurilor
        public void updateFile()
        {
            var text = "";

            string[] lines = File.ReadAllLines(this._fullPath);
            
            foreach(string line in lines)
            {
                var columns = line.Split(';');
                if (columns[0].Split(':')[1] != _username)
                    text += line + "\n";
            }

            foreach (Habit habit in this.habits_list)
            {
                text += habit.fileText() + "\n";
            }




            System.IO.File.WriteAllText(this._fullPath, string.Empty);
            System.IO.File.WriteAllText(this._fullPath, text);


        }

        public void removeHabit(Habit h)
        {
            this.habits_list.Remove(h);
        }
    }
}
