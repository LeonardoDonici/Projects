using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Proiect;
using System.Collections.Generic;

namespace ProjectTesting
{
    [TestClass]
    public class UnitTestAccess
    {
        [TestMethod]
        /// Metoda testare LogIn
        public void TestLogIn()
        {
            var objectAccess = ProxyAccessManager.Instance;
            Assert.AreEqual(true, objectAccess.LogIn("leo1", "leo1"));
        }

        public void TestLogIn2()
        {
            var objectAccess = ProxyAccessManager.Instance;
            Assert.AreEqual(true, objectAccess.LogIn("robert", "robert"));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void IncorrectLogIn()
        {
            var objectAccess = ProxyAccessManager.Instance;
            objectAccess.LogIn("", "robert");
            
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void IncorrectLogIn2()
        {
            var objectAccess = ProxyAccessManager.Instance;
            objectAccess.LogIn("robert", "");

        }
        [TestMethod]
        /// Metoda testare LogIn
        public void TestLogInIncorrect()
        {
            var objectAccess = ProxyAccessManager.Instance;
            Assert.AreEqual(false, objectAccess.LogIn("asdadsadda", "leoasgasgasgasgasg1"));
        }

        [TestMethod]
        /// Metoda testare Resgister cu parole diferite
        public void TestRegister1()
        {
            var objectAccess = ProxyAccessManager.Instance;
            Assert.AreEqual(false, objectAccess.Register("daniel", "daniel", "da", "nu", "da@gmail.com"));
        }

        [TestMethod]
        /// Metoda testare Resgister cu mail gresit
        public void TestRegister2()
        {
            var objectAccess = ProxyAccessManager.Instance;
            Assert.AreEqual(false, objectAccess.Register("daniel", "daniel", "da", "da", "mailrau"));
        }

        [TestMethod]
        public void TestRegister3()
        {
            var objectAccess = ProxyAccessManager.Instance;
            Assert.AreEqual(true, objectAccess.Register("daniel", "daniel", "da", "da", "da@gmail.com"));
        }
    }
}
