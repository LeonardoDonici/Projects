/**************************************************************************
 *                                                                        *
 *  File:        MotivatorStrategy.cs                                     *
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
    // interfata strategy
    /// <summary>
    /// Interfata MotivatorStrategy, care are la baza conceptul design patternului Strategy.
    /// Clasele ce mostentesc aceasta interfata sunt obligate sa implementeze buildMotivation care in functie de parametru, genereaza un anumit citat.
    /// </summary>
    public interface MotivatorStrategy
    {
        void buildMotivation(string text);
    }
}
