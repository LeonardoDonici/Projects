/**************************************************************************
 *                                                                        *
 *  File:        QuotesAPI.cs                                             *
 *  Copyright:   (c) 2024, Stefan Gherghel                                *
 *  Description: Clasa pentru API citate motivationale.                   *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

using Newtonsoft.Json; // Importarea bibliotecii Newtonsoft.Json pentru lucrul cu JSON
using System;
using System.Net; // Importarea spațiului de nume pentru lucrul cu rețeaua
using System.Net.Http;

namespace Proiect
{ 
        /// <summary>
      /// Metoda pentru a obține un citat pe baza unei teme date.
      /// </summary>
      /// <param name="topic">Tema citatului.</param>
      /// <returns>
      /// O tuplă formată din conținutul citatului și numele autorului.
      /// </returns>
    public class QuotesAPIManager
    {
        // Enumerație pentru diferitele teme de citate
        public enum Topics { change, failure, faith, courage, health, perseverence };

        // Metoda pentru a obține un citat pe baza unei teme date. Returnează o tuplă formată din conținutul citatului și numele autorului
        public static Tuple<string, string> GetQuote(Topics topic)
        {
            // Afișează un mesaj pentru a indica începerea apelului către API-ul de citate
            Console.WriteLine("Making Quotes API Call...");

            // Utilizarea unui client HTTP pentru a face cererea către API-ul de citate
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                // Stabilește adresa de bază a clientului HTTP la adresa API-ului de citate
                client.BaseAddress = new Uri("https://api.quotable.io/");

                // Realizarea unei cereri GET către API-ul de citate pentru a obține un citat aleatoriu bazat pe tema specificată
                HttpResponseMessage response = client.GetAsync("random?tags=" + topic.ToString()).Result;
                response.EnsureSuccessStatusCode(); // Asigură-te că răspunsul HTTP indică succes

                // Citirea conținutului răspunsului HTTP ca un șir de caractere
                string result = response.Content.ReadAsStringAsync().Result;

                // Parsarea răspunsului JSON pentru a extrage conținutul citatului și numele autorului
                dynamic quote = JsonConvert.DeserializeObject(result);
                string content = quote.content;
                string author = quote.author;

                // Crearea unei tuple cu conținutul citatului și numele autorului și returnarea acesteia
                Tuple<string, string> quoteTuple = new Tuple<string, string>(content, author);
                return quoteTuple;
            }
        }
    }
}