/**************************************************************************
 *                                                                        *
 *  File:        HealthStrategy.cs                                        *
 *  Copyright:   (c) 2024, Daniel Radu                                    *
 *  Description: Clasa pentru mesaje motivationale.                       *
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
    public class HealthStrategy : MotivatorStrategy
    {
        public void buildMotivation(string text)
        {
            var quote = Proiect.QuotesAPIManager.GetQuote(QuotesAPIManager.Topics.health);
            var message = quote.Item1 + "\n" + quote.Item2;
            System.Windows.Forms.MessageBox.Show(text+message);
        }
    }
}
