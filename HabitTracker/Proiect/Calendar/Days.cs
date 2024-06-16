/**************************************************************************
 *                                                                        *
 *  File:        Days.cs                                                  *
 *  Copyright:   (c) 2024, Stefan Radu                                    *
 *  Description: Clasa pentru colorarea zilelor din calendar.             *
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
using System.Windows.Forms;

namespace Proiect.Calendar
{
    /// <summary>
    /// Clasa ce reprezinta un singur patrat / zi din grupare din calendar
    /// </summary>
    public class Days
    {
        private string _status;
        private DateTime _date;
        private RichTextBox _entity;

        public Days(DateTime date, int x, int y, int day)
        {
            _entity = new RichTextBox();
            _entity.Text = day.ToString();
            _entity.BackColor = System.Drawing.Color.White;
            _entity.ReadOnly = true;
            _entity.Width = 49;
            _entity.Height = 51;
            _entity.Location = new System.Drawing.Point(x * 53 + 20, y * 51 + 50);

            _date = date;
        }
        /// <summary>
        /// Getter pentru richTextBox-ul ce reprezinta ziua in interfata
        /// </summary>
        public RichTextBox Entity
        {
            get
            {
                return _entity;
            }
        }

        /// <summary>
        /// Setare statusul zilei 
        /// </summary>
        /// <param name="status">checked - > Verde; failed -> Rosu; outside -> White</param>
        public void setDayStatus(string status)
        {
            _status = status;
            if(_status == "checked")
            {
                _entity.BackColor = System.Drawing.Color.Green;
            }

            if (_status == "failed")
            {
                _entity.BackColor = System.Drawing.Color.Red;
            }

            if( _status == "outside")
            {
                _entity.BackColor = System.Drawing.Color.White;
            }
        }
    }
}
