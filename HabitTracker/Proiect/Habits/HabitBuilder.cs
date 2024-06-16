/**************************************************************************
 *                                                                        *
 *  File:        HabitsBuilder.cs                                         *
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
    // builder-ul obiectelor de tip Habit.
    // fiecare metoda (exceptand build) returneaza propria referinta (this)
    // build returneaza un obiect de tip Habit.
    /// <summary>
    /// Clasa HabitBuilder - folosind aceasta clasa, instantierea obiectelor devine mai simpla. 
    /// S-a implementat design pattern-ul Builder.
    /// metoda build() returneaza un obiect de tip Habit, in timp ce restul obiectelor returneaza o referinta la obiectul
    /// curent (this)
    /// </summary>
    public class HabitBuilder
    {
        public string name;
        public string username;

        public DateTime startDateHabit;
      
        public List<DateTime> missedDays;
        
      
      
        public List<DateTime> checkedDays;
       
        public DateTime startDateNewStreak;
        public DateTime lastTimeChecked;
      
        public int frequency;
        public int bestStreak;
        public int currentStreak;
     
        

        
        public HabitBuilder setName(string name)
        {
            this.name = name;
            return this;
        }
        public HabitBuilder setUsername(string usernam)
        {
            username = usernam;
            return this;
        }
        public HabitBuilder setStartDate(DateTime startDate)
        {
            this.startDateHabit = startDate;
            return this;
        }

        public HabitBuilder setMissedDays(List<DateTime> missedDays)
        {
            this.missedDays = (missedDays == null) ? (new List<DateTime>()) : missedDays;
            return this;
        }

        public HabitBuilder setCheckedDays(List <DateTime> checkedDays)
        {
            this.checkedDays = (checkedDays == null) ? (new List<DateTime>()) : checkedDays;
            return this;
        }

        public HabitBuilder setStartDateNewStreak(DateTime startDateNewStreak)
        {
            this.startDateNewStreak = startDateNewStreak;
            return this;
        }

        public HabitBuilder setLastTimeChecked(DateTime lastTimetDate)
        {
            this.lastTimeChecked = lastTimetDate;
            return this;
        }

        public HabitBuilder setFrequency(int frequency)
        {
            this.frequency = frequency;
            return this;
        }

        public HabitBuilder setCurrentStreak(int currentStreak)
        {
            this.currentStreak = currentStreak;
            return this;
        }

        public HabitBuilder setBestStreak(int bestsStreak)
        {
            this.bestStreak = bestsStreak;
            return this;
        }

        public Habit build()
        {
            return new Habit(this);
        }


    }
}
