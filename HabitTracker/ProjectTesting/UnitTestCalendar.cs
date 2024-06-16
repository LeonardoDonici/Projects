using Microsoft.VisualStudio.TestTools.UnitTesting;
using Proiect.Calendar;
using System;

namespace ProjectTesting
{
    [TestClass]
    public class UnitTestCalendar
    {
        [TestMethod]
        public void testLabelString()
        {
            Calendary calendar = new Calendary(5, 2002);
            Assert.AreEqual("May 2002", calendar.setLabel());
        }

        [TestMethod]
        public void testLabelString2()
        {
            Calendary calendar = new Calendary(1, 2011);
            Assert.AreEqual("January 2011", calendar.setLabel());
        }

        [TestMethod]
        public void testLabelString3()
        {
            Calendary calendar = new Calendary(2, 2009);
            Assert.AreEqual("February 2009", calendar.setLabel());
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void invalidConstructor()
        {
            Calendary calendar = new Calendary(13, 2009);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void invalidConstructor2()
        {
            Calendary calendar = new Calendary(13, -2009);
        }
    }
}
