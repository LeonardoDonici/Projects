/**************************************************************************
 *                                                                        *
 *  File:        Cryptography.cs                                          *
 *  Copyright:   (c) 2024, Stefan Gherghel                                *
 *  Description: Clasa pentru criptarea parolei si a username-ului.       *
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
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Proiect
{
    public class Cryptography
    {
        /// <summary>
        /// Criptează textul specificat utilizând algoritmul simetric Rijndael
        /// și returnează un rezultat codificat în base64.
        /// </summary>
        /// <param name="plainText">
        /// Textul în clar care trebuie criptat.
        /// </param>
        /// <param name="passPhrase">
        /// Parola din care va fi derivată o parolă pseudo-aleatorie.
        /// Parola derivată va fi folosită pentru a genera cheia de criptare.
        /// Parola poate fi orice șir de caractere. În acest exemplu, presupunem că această
        /// parolă este un șir ASCII.
        /// </param>
        /// <param name="saltValue">
        /// Valoarea de sare folosită împreună cu parola pentru a genera parola. Sarea poate
        /// fi orice șir de caractere. În acest exemplu, presupunem că sarea este un șir ASCII.
        /// </param>
        /// <param name="hashAlgorithm">
        /// Algoritmul de hash folosit pentru a genera parola. Valorile permise sunt: "MD5" și
        /// "SHA1". Hash-urile SHA1 sunt puțin mai lente, dar mai sigure decât hash-urile MD5.
        /// </param>
        /// <param name="passwordIterations">
        /// Numărul de iterații folosite pentru a genera parola. Una sau două iterații
        /// ar trebui să fie suficiente.
        /// </param>
        /// <param name="initVector">
        /// Vectorul de inițializare (sau IV). Această valoare este necesară pentru a cripta
        /// primul bloc de date în clar. Pentru clasa RijndaelManaged, IV trebuie să fie
        /// exact 16 caractere ASCII.
        /// </param>
        /// <param name="keySize">
        /// Dimensiunea cheii de criptare în biți. Valorile permise sunt: 128, 192 și 256.
        /// Cheile mai lungi sunt mai sigure decât cheile mai scurte.
        /// </param>
        /// <returns>
        /// Valoarea criptată formatată ca un șir codificat în base64.
        /// </returns>
        public static string Encrypt(string text, string pass)
        {
            string plainText = text;
            string passPhrase = pass;
            string saltValue = "s@1tValue"; // poate fi orice șir
            string hashAlgorithm = "SHA1";// poate fi "MD5"
            int passwordIterations = 2; // poate fi orice număr
            string initVector = "@1B2c3D4e5F6g7H8"; // trebuie să fie 16 bytes
            int keySize = 256; // poate fi 192 sau 128

            // Convertim șirurile în tablouri de octeți.
            // Să presupunem că șirurile conțin doar coduri ASCII.
            // Dacă șirurile includ caractere Unicode, folosim Unicode, UTF7 sau UTF8.
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            // Convertim textul în clar într-un tablou de octeți.
            // Să presupunem că textul în clar conține caractere codificate UTF8.
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            // Mai întâi, trebuie să creăm o parolă din care va fi derivată cheia.
            // Această parolă va fi generată din parola specificată și valoarea de sare.
            // Parola va fi creată folosind algoritmul de hash specificat.
            // Crearea parolei se poate face în mai multe iterații.
            PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                            passPhrase,
                                                            saltValueBytes,
                                                            hashAlgorithm,
                                                            passwordIterations);

            // Folosim parola pentru a genera octeți pseudo-aleatori pentru cheia de criptare.
            // Specificăm dimensiunea cheii în octeți (în loc de biți).
            byte[] keyBytes = password.GetBytes(keySize / 8);

            // Creăm obiectul de criptare Rijndael neinițializat.
            RijndaelManaged symmetricKey = new RijndaelManaged();

            // Este rezonabil să setăm modul de criptare la Cipher Block Chaining (CBC).
            // Folosim opțiunile implicite pentru ceilalți parametri ai cheii simetrice.
            symmetricKey.Mode = CipherMode.CBC;

            // Generăm criptorul din octeții cheii existenți și vectorul de inițializare.
            // Dimensiunea cheii va fi definită pe baza numărului de octeți ai cheii.
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(
                                                             keyBytes,
                                                             initVectorBytes);

            // Definim un flux de memorie care va fi folosit pentru a ține datele criptate.
            MemoryStream memoryStream = new MemoryStream();

            // Definim un flux criptografic (întotdeauna folosim modul Write pentru criptare).
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                         encryptor,
                                                         CryptoStreamMode.Write);
            // Începem criptarea.
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

            // Finalizăm criptarea.
            cryptoStream.FlushFinalBlock();

            // Convertim datele criptate din fluxul de memorie într-un tablou de octeți.
            byte[] cipherTextBytes = memoryStream.ToArray();

            // Închidem ambele fluxuri.
            memoryStream.Close();
            cryptoStream.Close();

            // Convertim datele criptate într-un șir codificat în base64.
            string cipherText = Convert.ToBase64String(cipherTextBytes);

            // Returnăm șirul criptat.
            return cipherText;
        }

        /// <summary>
        /// Decriptează textul cifrat specificat utilizând algoritmul simetric Rijndael.
        /// </summary>
        /// <param name="cipherText">
        /// Valoarea textului cifrat formatată în base64.
        /// </param>
        /// <param name="passPhrase">
        /// Parola din care va fi derivată o parolă pseudo-aleatorie.
        /// Parola derivată va fi folosită pentru a genera cheia de criptare.
        /// Parola poate fi orice șir de caractere. În acest exemplu, presupunem că această
        /// parolă este un șir ASCII.
        /// </param>
        /// <param name="saltValue">
        /// Valoarea de sare folosită împreună cu parola pentru a genera parola. Sarea poate
        /// fi orice șir de caractere. În acest exemplu, presupunem că sarea este un șir ASCII.
        /// </param>
        /// <param name="hashAlgorithm">
        /// Algoritmul de hash folosit pentru a genera parola. Valorile permise sunt: "MD5" și
        /// "SHA1". Hash-urile SHA1 sunt puțin mai lente, dar mai sigure decât hash-urile MD5.
        /// </param>
        /// <param name="passwordIterations">
        /// Numărul de iterații folosite pentru a genera parola. Una sau două iterații
        /// ar trebui să fie suficiente.
        /// </param>
        /// <param name="initVector">
        /// Vectorul de inițializare (sau IV). Această valoare este necesară pentru a cripta
        /// primul bloc de date în clar. Pentru clasa RijndaelManaged, IV trebuie să fie
        /// exact 16 caractere ASCII.
        /// </param>
        /// <param name="keySize">
        /// Dimensiunea cheii de criptare în biți. Valorile permise sunt: 128, 192 și 256.
        /// Cheile mai lungi sunt mai sigure decât cheile mai scurte.
        /// </param>
        /// <returns>
        /// Valoarea decriptată.
        /// </returns>
        /// <remarks>
        /// Majoritatea logicii din această funcție este similară cu logica de criptare.
        /// Pentru ca decriptarea să funcționeze, toți parametrii acestei funcții
        /// - cu excepția valorii textului cifrat - trebuie să se potrivească cu parametrii
        /// corespunzători ai funcției de criptare care a fost apelată pentru a genera
        /// textul cifrat.
        /// </remarks>
        public static string Decrypt(string text, string pass)
        {
            string cipherText = text;
            string passPhrase = pass;
            string saltValue = "s@1tValue"; // poate fi orice șir
            string hashAlgorithm = "SHA1";// poate fi "MD5"
            int passwordIterations = 2; // poate fi orice număr
            string initVector = "@1B2c3D4e5F6g7H8"; // trebuie să fie 16 bytes
            int keySize = 256; // poate fi 192 sau 128
            // Convertim șirurile care definesc caracteristicile cheii de criptare în
            // tablouri de octeți. Să presupunem că șirurile conțin doar coduri ASCII.
            // Dacă șirurile includ caractere Unicode, folosim Unicode, UTF7 sau UTF8.
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            // Convertim textul cifrat într-un tablou de octeți.
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

            // Mai întâi, trebuie să creăm o parolă din care va fi derivată cheia.
            // Această parolă va fi generată din parola specificată și valoarea de sare.
            // Parola va fi creată folosind algoritmul de hash specificat.
            // Crearea parolei se poate face în mai multe iterații.
            PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                            passPhrase,
                                                            saltValueBytes,
                                                            hashAlgorithm,
                                                            passwordIterations);

            // Folosim parola pentru a genera octeți pseudo-aleatori pentru cheia de criptare.
            // Specificăm dimensiunea cheii în octeți (în loc de biți).
            byte[] keyBytes = password.GetBytes(keySize / 8);

            // Creăm obiectul de criptare Rijndael neinițializat.
            RijndaelManaged symmetricKey = new RijndaelManaged();

            // Este rezonabil să setăm modul de criptare la Cipher Block Chaining (CBC).
            // Folosim opțiunile implicite pentru ceilalți parametri ai cheii simetrice.
            symmetricKey.Mode = CipherMode.CBC;

            // Generăm decriptorul din octeții cheii existenți și vectorul de inițializare.
            // Dimensiunea cheii va fi definită pe baza numărului de octeți ai cheii.
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(
                                                             keyBytes,
                                                             initVectorBytes);

            // Definim un flux de memorie care va fi folosit pentru a ține datele criptate.
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);

            // Definim un flux criptografic (întotdeauna folosim modul Read pentru criptare).
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                          decryptor,
                                                          CryptoStreamMode.Read);

            // Deoarece în acest punct nu știm ce dimensiune vor avea datele decriptate,
            // alocăm un buffer suficient de mare pentru a ține textul cifrat;
            // textul în clar nu este niciodată mai lung decât textul cifrat.
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            // Începem decriptarea.
            int decryptedByteCount = cryptoStream.Read(plainTextBytes,
                                                       0,
                                                       plainTextBytes.Length);

            // Închidem ambele fluxuri.
            memoryStream.Close();
            cryptoStream.Close();

            // Convertim datele decriptate într-un șir de caractere.
            // Să presupunem că șirul original în clar era codificat UTF8.
            string plainText = Encoding.UTF8.GetString(plainTextBytes,
                                                       0,
                                                       decryptedByteCount);

            // Returnăm șirul decriptat.
            return plainText;
        }

        /// <summary>
        /// Returnează hash-ul unui șir de caractere
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string HashString(string str)
        {
            HashAlgorithm sha = new SHA1CryptoServiceProvider();
            byte[] buf = new byte[str.Length];
            for (int i = 0; i < str.Length; i++)
                buf[i] = (byte)str[i];
            byte[] result = sha.ComputeHash(buf);
            return Convert.ToBase64String(result);
        }
    }
}
