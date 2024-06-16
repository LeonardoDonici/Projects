using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect
{
    interface IAccessManager
    {
        bool LogIn(string user, string password);
        bool Register(string nume, string prenume, string parola, string parola2, string mail);
    }
}
