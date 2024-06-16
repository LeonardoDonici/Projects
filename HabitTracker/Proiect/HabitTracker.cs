/**************************************************************************
 *                                                                        *
 *  File:        HabitTracker.cs                                          *
 *  Copyright:   (c) 2024, Daniel Radu, Stefan Radu                       *
 *  Description: Clasa pentru interfata aplicatiei.                       *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/


//using Proiect.Habits;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Proiect.Habits;
using Proiect.Journal;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Proiect.Calendar;
using Google.Apis.Calendar.v3;
using System.IO;

namespace Proiect
{
    public partial class HabitTracker : Form
    {

        private string _currentUser;

        private HabitsManager _habitsManager;
        // partea de notite pentru fiecare habit
        private Journal.Journal journal;
        // strategia pentru generarea de quotes (buton separat in interfata)
        private MotivatorStrategy motivatorStrategy;
        // calendarul fizic din interfata
        private static Calendary calendar = new Calendary(DateTime.Now.Month, DateTime.Now.Year);

        public HabitTracker(string curentUser)
        {
            _currentUser = curentUser;


            
            this._habitsManager = new HabitsManager(_currentUser);
            
            this._habitsManager.initializeHabitsList();
            this._habitsManager.createHabitsFile();
            

            this.journal  = Journal.Journal.getInstance();

            InitializeComponent();

            //Setam label-ul sa reprezinte luna + anul curent
            currentDate.Text = calendar.setLabel();
            //Adaugam calendarul in grid
            calendar.addGridToControl(this);

            // ascund anumit butoane la intializare
            this.hideNewHabit();
            this.hideRich();
            this.comboBox1.FormattingEnabled = true;
            // lista de habits-uri din baza de date (fisier)
            this.comboBox1.DataSource = this._habitsManager.getHabitsList();
            // lista cu topic-uri pentru quotes
            this.quotesBox.DataSource = new List<String>() { "change", "failure", "faith", "courage", "health", "perseverence" };

            //Colorare a calendarului
            if (comboBox1.SelectedValue != null)
                calendar.colorGrid((Habit)comboBox1.SelectedValue);
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        // create new habbit 
        private void button1_Click(object sender, EventArgs e)
        {
            // ascund interfata pentru prezentarea statisticilor si pornesc interfata de creare obicei
            this.hideStats();
            this.showNewHabit();
        }

        // update statistici in functie de ce obicei vizualizeaza userul
        private void updateStreakBoxes()
        {
            Habit current = (Habit)comboBox1.SelectedValue;
            current.verifyStreak();
            this.currentStreak.Text =  "Streak-ul curent: " + current.getCurrentStreak().ToString();
            this.maximStreak.Text   =  "Streak-ul maxim:  " + current.getBestStreak().ToString();
        }

        // update calendar in functie de ce obicei vizualizeaza userul

        private void updateCalendar()
        {
            Habit current = (Habit)comboBox1.SelectedValue;
           //TBA LA CALENDAR
           // this.monthCalendarTest.BoldedDates = null;
           // this.monthCalendarTest.BoldedDates = current.getCheckedDays().ToArray();
        }

        // metoda prin care tratez schimbare obiceiului de catre user
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue != null)
            {
                Habit current = (Habit)comboBox1.SelectedValue;

                updateStreakBoxes();

                this.checkHabit.Checked = false;
                this.checkHabit.CheckState = CheckState.Unchecked;

                updateCalendar();

                calendar.colorGrid(current);


                if (current.getTimetoCheck())
                {
                    if (current.getCheckedToday())
                    {
                        this.checkHabit.Text = "Already checked today!";
                        this.checkHabit.Enabled = false;
                    }
                    else
                    {
                        this.checkHabit.Text = "Check today!";
                        this.checkHabit.Enabled = true;
                    }
                }
                else
                {
                    this.checkHabit.Text = "You are in graphic with this one!";
                    this.checkHabit.Enabled = false;
                }
                

            }
        }


        // metoda prin care tratez crearea unui nou obicei
        private void addNewHabit_Click(object sender, EventArgs e)
        {
            //tratare exceptie pentru inserare date invalide

            var name = this.habitName.Text;
            try
            {
                var frequency = int.Parse(this.habitFrequency.Text);
                if(frequency <= 0)
                {
                    MessageBox.Show("Frquency can't be lower or equal to 0!");
                    frequency = 1;
                }
                // build the object
                Habit newHabit = new HabitBuilder().setUsername(_currentUser).setName(name).setStartDate(DateTime.Now.Date).setStartDateNewStreak(DateTime.Now.Date).
                setFrequency(frequency).setMissedDays(null).
                setCurrentStreak(0).setBestStreak(0).setLastTimeChecked(DateTime.Now.Date.AddDays(-frequency)).
                setCheckedDays(null).build();

                this._habitsManager.addHabit(newHabit);
                this.comboBox1.DataSource = null;
                this.comboBox1.DataSource = this._habitsManager.getHabitsList();

                this.hideNewHabit();
                this.showStats();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Frquency has to be a number!");
            }
            

           


        }

        private void showStats()
        {
            this.currentStreak.Show();
            this.maximStreak.Show();
            this.button1.Show();
        }

        private void hideStats()
        {
            this.currentStreak.Hide();
            this.maximStreak.Hide();
            this.button1.Hide();
        }

        private void showNewHabit()
        {
            this.habitFrequency.Show();
            this.habitName.Show();
            this.habitNameLabel.Show();
            this.habitFrequencyLabel.Show();
            this.habitInfoLabel.Show();
            this.addNewHabit.Show();
        }

        private void hideNewHabit()
        {
            this.habitFrequency.Hide();
            this.habitName.Hide();
            this.habitNameLabel.Hide();
            this.habitFrequencyLabel.Hide();
            this.habitInfoLabel.Hide();
            this.addNewHabit.Hide();
        }

        private void showRich()
        {
            this.richJurnal.Show();
            this.buttonSaveJurnal.Show();
        }

        private void hideRich()
        {
            this.richJurnal.Hide();
            this.buttonSaveJurnal.Hide();
        }

        // metoda de tratare a evenimentului de bifare a unui obicei
        private void checkBox1_Click(object sender, System.EventArgs e)
        {
           
            if (this.checkHabit.Checked)
            {
                Habit current = (Habit)comboBox1.SelectedValue;
                current.checkToday();
                this.checkHabit.Enabled = false;
                this.updateStreakBoxes();
                this.updateCalendar();
                calendar.colorGrid(current);
            }
            else
            {
                
            }
        }

        // !!! atunci cand inchid formularul, fac update la fisier !!!
        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            this._habitsManager.updateFile();
        }

        // partea de jurnal
        private void journalHabit_Click(object sender, EventArgs e)
        {
            this.hideNewHabit();
            this.hideStats();


            this.showRich();

            Habit current = (Habit)comboBox1.SelectedValue;
            this.journal.setHabit(current);

            
            string filePath = this.journal.getFile();

            // incarc continutul fisierului in rich file
            this.richJurnal.LoadFile(filePath, RichTextBoxStreamType.PlainText);
            
           
        }

        private void buttonSaveJurnal_Click(object sender, EventArgs e)
        {
            // salvare text in fisierul corespunzator
            string text = this.richJurnal.Text;
            Console.Write("Textul " + text);
            this.journal.saveFile(text);
            this.hideRich();
            this.showStats();

        }

        // functia de delete a unui obicei
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Habit current = (Habit)comboBox1.SelectedValue;
            this._habitsManager.removeHabit(current);
            // salvez in fisierul corespunzator inchiderea acestui obicei
            this.journal.setHabit(current);
            this.journal.closeHabit();
            // actualizarea listei din interfata
            this.comboBox1.DataSource = null;
            this.comboBox1.DataSource = this._habitsManager.getHabitsList();

        }

        // strategy 
        private void setMotivator(MotivatorStrategy m)
        {
            this.motivatorStrategy = m;
        }

        // generare de citate
        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            var current = quotesBox.SelectedValue;
            

            if (current.ToString() == "change")
            {
                this.setMotivator(new TransformationStrategy());
            }
            else if(current.ToString() =="failure")
            {
                this.setMotivator(new FailureStrategy());
            }
            else if(current.ToString() =="courage")
            {
                this.setMotivator(new CourageStrategy());

            }
            else if(current.ToString()=="health")
            {
                this.setMotivator(new HealthStrategy());

            }
            else if(current.ToString()=="perseverence")
            {
                this.setMotivator(new PerseverenceStrategy());

            }

            this.motivatorStrategy.buildMotivation(string.Empty);


        }
        //Tratare eveniment de apasare a butonului de mers inainte cu o luna
        private void monthForward_Click(object sender, EventArgs e)
        {
            int newMonth, newYear;
            if (calendar.Month + 1 > 12)
            {
                newMonth = 1;
                newYear = calendar.Year + 1;
            }
            else
            {
                newMonth = calendar.Month + 1;
                newYear = calendar.Year;
            }

            calendar.removeGridFromControl(this);

            calendar = new Calendary(newMonth, newYear);

            calendar.addGridToControl(this);

            currentDate.Text = calendar.setLabel();

            if (comboBox1.SelectedValue != null)
                calendar.colorGrid((Habit)comboBox1.SelectedValue);
        }
        //Tratare eveniment de apasare a butonului de mers inapoi cu o luna
        private void monthBack_Click(object sender, EventArgs e)
        {
            int newMonth, newYear;
            if (calendar.Month - 1 < 1)
            {
                newMonth = 12;
                newYear = calendar.Year - 1;
            }
            else
            {
                newMonth = calendar.Month - 1;
                newYear = calendar.Year;
            }

            calendar.removeGridFromControl(this);

            calendar = new Calendary(newMonth, newYear);

            calendar.addGridToControl(this);

            currentDate.Text = calendar.setLabel();

            if (comboBox1.SelectedValue != null)
                calendar.colorGrid((Habit)comboBox1.SelectedValue);
        }

        private void checkHabit_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            var currentDirectoryPath = Directory.GetCurrentDirectory();
            var filePath = Path.Combine(currentDirectoryPath, @"..\..\bin\debug\x64\UserHelp.chm");
            string fullPath = Path.GetFullPath(Path.Combine(currentDirectoryPath, filePath));
            Help.ShowHelp(this, fullPath);
        }
    }
}
