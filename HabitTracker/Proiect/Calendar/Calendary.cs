/**************************************************************************
 *                                                                        *
 *  File:        Calendary.cs                                             *
 *  Copyright:   (c) 2024, Stefan Radu                                    *
 *  Description: Clasa pentru calendar.                                   *
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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Proiect.Habits;

namespace Proiect.Calendar
{
    /// <summary>
    /// Clasa asociata calendarului din aplicatie
    /// </summary>
    public class Calendary
    {
        private int _month;
        private int _year;
        private Days[] _dayz;


        public Calendary(int month, int year)
        {

            _month = month;
            _year = year;

            DateTime theFirst = new DateTime(_year, _month, 1);

            buildGrid(theFirst.DayOfWeek, DateTime.DaysInMonth(_year, _month));

        }
        public int Month { get { return _month; } }
        public int Year { get { return _year; } }
        /// <summary>
        /// Construieste calendarul si textbox-urile ce reprezinta zilele
        /// </summary>
        /// <param name="startingPoint">Day of the week in which the month begins</param>
        /// <param name="daysInMonth">The amount of days in the current month</param>
        private void buildGrid(DayOfWeek startingPoint, int daysInMonth)
        {
            int actualStartingPoint = (int)startingPoint;
            int currentRow = 0;

            DateTime dateWeAreAtInCalendar = new DateTime(_year, _month, 1);

            _dayz = new Days[DateTime.DaysInMonth(dateWeAreAtInCalendar.Year, dateWeAreAtInCalendar.Month)];

            int i = 0;

            while (i < daysInMonth)
            {
                _dayz[i] = new Days(dateWeAreAtInCalendar, actualStartingPoint, currentRow, i + 1);
                dateWeAreAtInCalendar.AddDays(1);
                i++;
                actualStartingPoint++;
                if (actualStartingPoint == 7)
                {
                    actualStartingPoint = 0;
                    currentRow++;
                }
            }
        }

        /// <summary>
        /// Stergem calendarul din form-ul dat
        /// </summary>
        /// <param name="form"></param>
        public void removeGridFromControl(Form form)
        {
            for (int i = 0; i < _dayz.Length; i++)
                form.Controls.Remove(_dayz[i].Entity);
        }
        /// <summary>
        /// Adaugam calendarul in form-ul dat
        /// </summary>
        /// <param name="form"></param>
        public void addGridToControl(Form form)
        {
            for (int i = 0; i < _dayz.Length; i++)
                form.Controls.Add(_dayz[i].Entity);
        }
        /// <summary>
        /// Obtinem un string ce contine anul si luna reprezentata de catre calendar
        /// </summary>
        /// <returns></returns>
        public string setLabel()
        {
            Label label = new Label();

            DateTime date = new DateTime(_year, _month, 1);

            return date.ToString("MMMM yyyy", CultureInfo.InvariantCulture);

           
        }
        /// <summary>
        /// Colorarea calendarului in functie de zilele bifate / nebifate
        /// </summary>
        /// <param name="currentHabit"></param>
        public void colorGrid(Habit currentHabit)
        {
            completeListDays(currentHabit);

            List<DateTime> checkd = currentHabit.getCheckedDaysInMonth(_month);

            for(int i = 0; i < _dayz.Length; i++)
            {
                _dayz[i].setDayStatus("outside");
            }

            foreach(DateTime day in checkd)
            {
                if (day.Year == _year)
                    _dayz[day.Day - 1].setDayStatus("checked");
            }

            List<DateTime> uncheckd = currentHabit.getUncheckedDaysInMonth(_month);

            foreach(DateTime day in uncheckd)
            {
                if (day.Year == _year)
                    _dayz[day.Day - 1].setDayStatus("failed");
            }

        }

        private void completeListDays(Habit curentHabit)
        {
            DateTime startDay = curentHabit.getStartDate();
            DateTime endDay = DateTime.Today.AddDays(-1);

            List<DateTime> checkedDays = curentHabit.getCheckedDays();
            List<DateTime> uncheckedDays = curentHabit.getUncheckedDays();


            for (DateTime date = startDay; date <= endDay; date = date.AddDays(1))
            {
                if(!checkedDays.Contains(date) && !uncheckedDays.Contains(date))
                {
                    curentHabit.AddUncheckedDay(date);
                }
            }
        }
    }
}

