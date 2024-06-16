using Microsoft.VisualStudio.TestTools.UnitTesting;
using Proiect.Habits;
using System;
using System.Collections.Generic;


namespace ProjectTesting
{
    [TestClass]
    public class UnitTestHabits
    {
        [TestMethod]
        /// Metoda testeaza setarea corecta a numelui Habitului.
        public void TestHabitName()
        {
            Habit habit1 = new HabitBuilder().setName("name1").setUsername("username1").setStartDate(DateTime.Now).
                    setStartDateNewStreak(DateTime.Now).setFrequency(1).
                    setBestStreak(0).
                    setCurrentStreak(0).
                    build();
            Assert.AreEqual("name1", habit1.ToString());
        }

        /// Metoda testeaza setarea corecta a start day-ului Habitului.

        [TestMethod]
        public void TestHabitStartDay()
        {
            Habit habit1 = new HabitBuilder().setName("name1").setUsername("username1").setStartDate(DateTime.Now).
                    setStartDateNewStreak(DateTime.Now).setFrequency(1).
                    setBestStreak(0).
                    setCurrentStreak(0).
                    build();
            Assert.AreEqual(DateTime.Now, habit1.getStartDate());
        }

        [TestMethod]
        /// Metoda testeaza pierderea corecta a streak-ului.

        public void TestHabitLostStreak()
        {
            Habit habit1 = new HabitBuilder().setName("ShouldBeLost").setCurrentStreak(3).
                setUsername("username1").setFrequency(1).setLastTimeChecked(DateTime.Now.AddDays(-2)).build();
            habit1.verifyStreak();
            Assert.AreEqual(3, habit1.getCurrentStreak());
        }

        [TestMethod]
        /// Metoda testeaza pierderea streak-ului.

        public void TestHabitNotLostStreak()
        {
            Habit habit1 = new HabitBuilder().setName("ShouldNotBeLost").setCurrentStreak(1).
                setUsername("username1").setFrequency(1).setLastTimeChecked(DateTime.Now.AddDays(-2)).build();
            habit1.verifyStreak();
            Assert.AreEqual(1, habit1.getCurrentStreak());
        }


        /// Metoda testeaza daca habit-ul a fost verificat astazi.
        [TestMethod]
        public void TestHabitCheckedToday()
        {
            Habit habit1 = new HabitBuilder().setName("name1").setCurrentStreak(1).
                setUsername("username1").setFrequency(1).setLastTimeChecked(DateTime.Now.Date).build();
          
            Assert.AreEqual(true, habit1.getCheckedToday());
        }



        /// Metoda testeaza returnarea corecta a zilelor bifate.
        [TestMethod]
        public void TestHabitCheckedDays1()
        {
            Habit habit1 = new HabitBuilder().setName("name1").setCurrentStreak(1).
                setUsername("username1").setFrequency(1).setCheckedDays(null).build();

            Assert.AreEqual(0, habit1.getCheckedDays().Count);
        }

        /// Metoda testeaza returnarea corecta a zilelor bifate.
        [TestMethod]
        public void TestHabitCheckedDays2()
        {
            List<DateTime> list = new List<DateTime>();
            list.Add(DateTime.Parse("24.05.2024"));
            list.Add(DateTime.Parse("25.05.2024"));
            Habit habit1 = new HabitBuilder().setName("name1").setCurrentStreak(1).
                setUsername("username1").setFrequency(1).setCheckedDays(list).build();

            Assert.AreEqual(2, habit1.getCheckedDays().Count);
        }

        public void TestHabitCheckedDays3()
        {
            List<DateTime> list = new List<DateTime>();
            list.Add(DateTime.Parse("24.05.2024"));
            list.Add(DateTime.Parse("25.05.2024"));
            list.Add(DateTime.Parse("26.05.2024"));
            Habit habit1 = new HabitBuilder().setName("name1").setCurrentStreak(1).
                setUsername("username1").setFrequency(1).setCheckedDays(list).build();

            Assert.AreEqual(3, habit1.getCheckedDays().Count);
        }
    }
}
