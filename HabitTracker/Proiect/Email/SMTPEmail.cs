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
using System.Net;
using System.Net.Mail;

namespace Proiect.Email
{
    /// <summary>
    /// Clasă care gestionează trimiterea email-urilor folosind protocolul SMTP.
    /// </summary>
    /// <remarks>
    /// Această clasă folosește un cont Gmail cu autentificare în doi pași pentru a trimite email-uri de recuperare a parolei.
    /// </remarks>
    /// <param name="recipient">Adresa de email a destinatarului.</param>
    /// <param name="subject">Subiectul email-ului.</param>
    /// <param name="text">Textul conținutului email-ului.</param>
    public class SMTPEmail                  //clasa care se ocupa cu trimis mailuri
    {
        public static void sendEmail(string recipient, string subject, string text)
        {
            string myEmail = "arduinonotifications4@gmail.com";             //o adresa de mail cu 2fa activat
            string password = "btsc ywwm lmqo kmtb";                       

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(myEmail, password),
                EnableSsl = true,
            };

            smtpClient.Send(recipient, recipient, subject, text);
        }
    }
}
