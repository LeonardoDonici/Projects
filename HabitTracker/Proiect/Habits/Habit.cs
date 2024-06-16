/**************************************************************************
 *                                                                        *
 *  File:        Habits.cs                                                *
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Proiect.Habits
{
    /// <summary>
    /// Clasa Habit implementeaza un obicei. Aceasta este instantiata folosind un obiect de tip HabitBuilder.
    /// Habit a fost implementata utilizand Builder ca design pattern.
    /// Gestionarea si instantierea obiectelor depinde de fisierul text, acolo unde este salvat progresul.
    /// Clasa foloseste si MotivatorStrategy, implementat cu ajutorul design patternului Strategy.
    /// </summary>
    public class Habit
    {
       
        private string _name;
        private string _username;
        
        //inceputul habitului
        private DateTime _startDateHabit;

        private List<DateTime> _missedDays;

        // zilele bifate

        private List<DateTime> _checkedDays;

        //inceputul unui nou strike
        private DateTime _startDateNewStreak;
        private DateTime _lastTimeChecked;

        private int _frequency;
        private int _bestStreak;
        private int _currentStreak;

        private bool motivated = false;
        private bool time_to_check = false;
        private bool _checkedToday = false;

        //variabila prin care implementam Strategy pentru generare citate
        private MotivatorStrategy motivationStrategy;
       

        // constructor in care avem ca parametru un builder
        public Habit(HabitBuilder habitBuilder)
        {
            this._name = habitBuilder.name;
            this._username = habitBuilder.username;
            this._startDateHabit = habitBuilder.startDateHabit;
            this._startDateNewStreak = habitBuilder.startDateNewStreak;
            this._frequency = habitBuilder.frequency;
            this._bestStreak = habitBuilder.bestStreak;
            this._missedDays = habitBuilder.missedDays;
            this._checkedDays = habitBuilder.checkedDays;
            this._currentStreak = habitBuilder.currentStreak;
            this._lastTimeChecked = habitBuilder.lastTimeChecked;

            // verific daca astazi trebuie bifat
            this.time_to_check = (this._lastTimeChecked.AddDays(this._frequency).Date <= DateTime.Now.Date);
            // verific daca a fost deja bifat astazi
            this._checkedToday = (this._lastTimeChecked == DateTime.Now.Date);

           
        }
        public DateTime getStartDate()
        {
            return this._startDateHabit;
        }
        public void setDateHabit(DateTime startDateHabit)
        {
            _startDateHabit = startDateHabit;
        }

        public DateTime getDateHabit()
        {
            return this._startDateHabit;
        }

        public void setDateStreak(DateTime startDateStreak)
        {
            this._startDateNewStreak = startDateStreak;
        }

        public DateTime getDateStreak()
        {
            return _startDateNewStreak;
        }

        public int getCurrentStreak()
        {
            return this._currentStreak;
        }

        public int getBestStreak()
        {
            return this._bestStreak;
        }
        //verific doar pe baza ultimei zile introduse
        //ar trebui sa adaug frequency la ultima zi introdusa si sa vad daca este peste ziua de azi.
        //daca este peste ziua de azi => introduc ziua respectiva in categoria de zile uitate
        /// <summary>
        /// VerifyStreak verifica daca obiceiul este la zi sau a fost intrerupt. 
        /// In functie de acest lucru, seteaza strategia potrivita pentru generarea citatelor.
        /// </summary>

        public void verifyStreak()
        {
            //fac acest lucru o sigura data, la deshiderea aplicatiei
            if(!motivated)
            {
                // verific daca nu cumva s-a trecut peste ziua in care trebuia bifat habitul
                if ((DateTime.Now.Date - this._lastTimeChecked).Days > this._frequency)
                {
                    //setez strategia de motivare
                    this.setMotivationStrategy(new FailureStrategy());
                    //motivez
                    this.buildMotivation("You lost your work on - " + this._name + " - ! But keep going trying!\n");
                    
                    // setez noile valori
                    this._startDateNewStreak = DateTime.Now;
                    if (this._currentStreak > this._bestStreak)
                    {
                        this._bestStreak = this._currentStreak;
                    }
                    this._currentStreak = 0;


                    // trec prin toate zilele care ar fi trebuit sa fie bifate si le marchez ca fiind pierdute
                    var firstMissed = this._lastTimeChecked.AddDays(this._frequency);
                    while (firstMissed < DateTime.Now.Date)
                    {
                        this._missedDays.Add(firstMissed);
                        firstMissed = firstMissed.AddDays(this._frequency);
                    }

                }
                // verific daca nu cumva chiar astazi trebuie bifat
                else if ((DateTime.Now.Date - this._lastTimeChecked).Days == this._frequency)
                {
                    

                    this.setMotivationStrategy(new CourageStrategy());
                    this.buildMotivation("Remember! You have to do this habit - " + this._name + " - today!\n");


                }
                // altfel, inseamna ca habitul este in grafic, nu trebuie bifat.
                else
                {
                   

                    this.setMotivationStrategy(new PerseverenceStrategy());
                    this.buildMotivation("Keep going with - " + this._name +" - !!\n");


                }
                this.motivated = true;
            }
          
        }

        public void AddUncheckedDay(DateTime day)
        {
            _missedDays.Add(day);
        }

        /// <summary>
        /// checkToday - Verifica daca obiceiul a fost bifat astazi
        /// </summary>
        public void checkToday()
        {
            // daca utilizatorul bifeaza habit-ul ca fiind facut astazi, salvez acest lucru
            this._lastTimeChecked = DateTime.Now;
            
            this._currentStreak++;
            if(this._currentStreak > this._bestStreak)
            {
                this._bestStreak = this._currentStreak;
            }
            this._checkedToday = true;
            this._checkedDays.Add(DateTime.Today);
        }

       
        // strategy 
        public void buildMotivation(string text)
        {
            this.motivationStrategy.buildMotivation(text);
        }

        // strategy
        public void setMotivationStrategy(MotivatorStrategy m)
        {
            this.motivationStrategy = m;
        }

        //functie ajutatoare pentru generarea zilelor bifate de catre utilizator
        public List<DateTime> getCheckedDays()
        {
            // varianta initiala, nu atat de buna:
/*            List<DateTime> checkedDays = new List<DateTime>();
            var startDay = this._startDateHabit;
            while(startDay < this._lastTimeChecked)
            {
                if(!this._missedDays.Contains(startDay))
                {
                    checkedDays.Add(startDay);
                }
                startDay = startDay.AddDays(this._frequency);
            }   
            return checkedDays;*/

            return this._checkedDays;
        }

        public List<DateTime> getUncheckedDays()
        {
            return this._missedDays;
        }

        // functie ajutatoare pentru returnarea zilelor bifate doar dintr-o anumita luna
        /// <summary>
        /// 
        /// Functie care returneaza zilele bifate intr-o anumita luna
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public List<DateTime> getCheckedDaysInMonth(int month)
        {
            var checkedDays = this.getCheckedDays();
            List<DateTime> checkedDaysInMonth = new List<DateTime>();

            foreach(var day in checkedDays)
            {
                if(day.Month == month)
                {
                    checkedDaysInMonth.Add(day);
                }
            }

            return checkedDaysInMonth;
        }
        public List<DateTime> getUncheckedDaysInMonth(int month)
        {
            var uncheckedDays = this.getUncheckedDays();
            List<DateTime> uncheckedDaysInMonth = new List<DateTime>();

            foreach (var day in uncheckedDays)
            {
                if (day.Month == month)
                {
                    uncheckedDaysInMonth.Add(day);
                }
            }

            return uncheckedDaysInMonth;
        }

        public string getInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Username: " + this._username);
            sb.AppendLine("Nume: " + this._name);
            sb.AppendLine("Frecventa: " + this._frequency);
            sb.AppendLine("StartDate: " + this._startDateHabit);
            sb.AppendLine("StartStreak: " + this._startDateNewStreak);
            sb.AppendLine("Best streak: " + this._bestStreak);
            sb.AppendLine("Current streak: " + this._currentStreak);
            sb.AppendLine("Missed days: ");
            foreach (DateTime date in this._missedDays)
            {
                sb.AppendLine(date.ToString());
            }

            sb.AppendLine("Checked days");
            foreach (DateTime date in this._checkedDays)
            {
                sb.AppendLine(date.ToString());
            }

            return sb.ToString();

        }
        public void printInfo()
        {

            Console.Write(this.getInfo());
        }
            
        // generarea textului prin care suprascriu row-ul corespondent in fisier.
        /// <summary>
        /// Functia file text suprascriere habitul in fisier. 
        /// </summary>
        /// <returns></returns>
        public string fileText()
        {
            var text = "username:"+this._username + "; name:" +this._name+ "; start_date:" + this._startDateHabit.ToString().Split(' ')[0] 
                +"; frequency:" + this._frequency+";missed_days: ";
            foreach(DateTime missed in this._missedDays)
            {
                text += missed.ToString().Split(' ')[0]+"&";
            }
            
            text += ";new_start_streak:" + this._startDateNewStreak.ToString().Split(' ')[0] + ";" +
                 " current_streak:" + this._currentStreak + ";max_streak:" + this._bestStreak +
                 "; last_time_checked:" + this._lastTimeChecked.ToString().Split(' ')[0] + ";checked_days:";

            foreach (DateTime checked_day in this._checkedDays)
            {
                //nu adaug si ora 00:00:00, doar data
                text += checked_day.ToString().Split(' ')[0]+"&";
            }

            return text;
        }

        public bool getCheckedToday()
        {
            return this._checkedToday;
        }

        public bool getTimetoCheck()
        {
            return this.time_to_check;
        }

        public override string ToString()
        {
            return this._name;
        }

        
    }
}
