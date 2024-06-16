namespace Proiect
{
    partial class HabitTracker
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.currentStreak = new System.Windows.Forms.TextBox();
            this.maximStreak = new System.Windows.Forms.TextBox();
            this.habitName = new System.Windows.Forms.TextBox();
            this.habitFrequency = new System.Windows.Forms.TextBox();
            this.habitNameLabel = new System.Windows.Forms.Label();
            this.habitFrequencyLabel = new System.Windows.Forms.Label();
            this.addNewHabit = new System.Windows.Forms.Button();
            this.habitInfoLabel = new System.Windows.Forms.Label();
            this.checkHabit = new System.Windows.Forms.CheckBox();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.journalHabit = new System.Windows.Forms.Button();
            this.richJurnal = new System.Windows.Forms.RichTextBox();
            this.buttonSaveJurnal = new System.Windows.Forms.Button();
            this.quotesBox = new System.Windows.Forms.ComboBox();
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.qouteLabel = new System.Windows.Forms.Label();
            this.currentDate = new System.Windows.Forms.Label();
            this.monthForward = new System.Windows.Forms.Button();
            this.monthBack = new System.Windows.Forms.Button();
            this.buttonHelp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(119, 473);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 24);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 476);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "My Habits";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(580, 286);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(155, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "CreateNewHabit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // currentStreak
            // 
            this.currentStreak.Location = new System.Drawing.Point(580, 326);
            this.currentStreak.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.currentStreak.Name = "currentStreak";
            this.currentStreak.Size = new System.Drawing.Size(187, 22);
            this.currentStreak.TabIndex = 3;
            this.currentStreak.Text = "Streak-ul curent:";
            // 
            // maximStreak
            // 
            this.maximStreak.Location = new System.Drawing.Point(580, 364);
            this.maximStreak.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.maximStreak.Name = "maximStreak";
            this.maximStreak.Size = new System.Drawing.Size(187, 22);
            this.maximStreak.TabIndex = 4;
            this.maximStreak.Text = "Streak-ul maxim:";
            // 
            // habitName
            // 
            this.habitName.Location = new System.Drawing.Point(688, 326);
            this.habitName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.habitName.Name = "habitName";
            this.habitName.Size = new System.Drawing.Size(100, 22);
            this.habitName.TabIndex = 5;
            // 
            // habitFrequency
            // 
            this.habitFrequency.Location = new System.Drawing.Point(688, 364);
            this.habitFrequency.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.habitFrequency.Name = "habitFrequency";
            this.habitFrequency.Size = new System.Drawing.Size(100, 22);
            this.habitFrequency.TabIndex = 6;
            // 
            // habitNameLabel
            // 
            this.habitNameLabel.AutoSize = true;
            this.habitNameLabel.Location = new System.Drawing.Point(577, 329);
            this.habitNameLabel.Name = "habitNameLabel";
            this.habitNameLabel.Size = new System.Drawing.Size(79, 16);
            this.habitNameLabel.TabIndex = 7;
            this.habitNameLabel.Text = "Habit name:";
            // 
            // habitFrequencyLabel
            // 
            this.habitFrequencyLabel.AutoSize = true;
            this.habitFrequencyLabel.Location = new System.Drawing.Point(577, 370);
            this.habitFrequencyLabel.Name = "habitFrequencyLabel";
            this.habitFrequencyLabel.Size = new System.Drawing.Size(104, 16);
            this.habitFrequencyLabel.TabIndex = 8;
            this.habitFrequencyLabel.Text = "Habit frequency:";
            // 
            // addNewHabit
            // 
            this.addNewHabit.Location = new System.Drawing.Point(535, 407);
            this.addNewHabit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.addNewHabit.Name = "addNewHabit";
            this.addNewHabit.Size = new System.Drawing.Size(75, 23);
            this.addNewHabit.TabIndex = 9;
            this.addNewHabit.Text = "Add habit";
            this.addNewHabit.UseVisualStyleBackColor = true;
            this.addNewHabit.Click += new System.EventHandler(this.addNewHabit_Click);
            // 
            // habitInfoLabel
            // 
            this.habitInfoLabel.AutoSize = true;
            this.habitInfoLabel.Location = new System.Drawing.Point(580, 286);
            this.habitInfoLabel.Name = "habitInfoLabel";
            this.habitInfoLabel.Size = new System.Drawing.Size(110, 16);
            this.habitInfoLabel.TabIndex = 10;
            this.habitInfoLabel.Text = "Habit information:";
            // 
            // checkHabit
            // 
            this.checkHabit.AutoSize = true;
            this.checkHabit.Location = new System.Drawing.Point(279, 476);
            this.checkHabit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkHabit.Name = "checkHabit";
            this.checkHabit.Size = new System.Drawing.Size(125, 20);
            this.checkHabit.TabIndex = 11;
            this.checkHabit.Text = "Check for today!";
            this.checkHabit.UseVisualStyleBackColor = true;
            this.checkHabit.CheckedChanged += new System.EventHandler(this.checkHabit_CheckedChanged);
            this.checkHabit.Click += new System.EventHandler(this.checkBox1_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(279, 508);
            this.buttonDelete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(125, 23);
            this.buttonDelete.TabIndex = 14;
            this.buttonDelete.Text = "Delete habit";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // journalHabit
            // 
            this.journalHabit.Location = new System.Drawing.Point(279, 537);
            this.journalHabit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.journalHabit.Name = "journalHabit";
            this.journalHabit.Size = new System.Drawing.Size(125, 23);
            this.journalHabit.TabIndex = 15;
            this.journalHabit.Text = "Journal habit";
            this.journalHabit.UseVisualStyleBackColor = true;
            this.journalHabit.Click += new System.EventHandler(this.journalHabit_Click);
            // 
            // richJurnal
            // 
            this.richJurnal.Location = new System.Drawing.Point(511, 139);
            this.richJurnal.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.richJurnal.Name = "richJurnal";
            this.richJurnal.Size = new System.Drawing.Size(256, 249);
            this.richJurnal.TabIndex = 16;
            this.richJurnal.Text = "";
            // 
            // buttonSaveJurnal
            // 
            this.buttonSaveJurnal.Location = new System.Drawing.Point(660, 407);
            this.buttonSaveJurnal.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSaveJurnal.Name = "buttonSaveJurnal";
            this.buttonSaveJurnal.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveJurnal.TabIndex = 17;
            this.buttonSaveJurnal.Text = "Save";
            this.buttonSaveJurnal.UseVisualStyleBackColor = true;
            this.buttonSaveJurnal.Click += new System.EventHandler(this.buttonSaveJurnal_Click);
            // 
            // quotesBox
            // 
            this.quotesBox.FormattingEnabled = true;
            this.quotesBox.Items.AddRange(new object[] {
            "change",
            "failure",
            "faith",
            "courage",
            "health",
            "perseverence"});
            this.quotesBox.Location = new System.Drawing.Point(580, 50);
            this.quotesBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.quotesBox.Name = "quotesBox";
            this.quotesBox.Size = new System.Drawing.Size(121, 24);
            this.quotesBox.TabIndex = 18;
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Location = new System.Drawing.Point(580, 82);
            this.buttonGenerate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(121, 23);
            this.buttonGenerate.TabIndex = 19;
            this.buttonGenerate.Text = "Generate";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // qouteLabel
            // 
            this.qouteLabel.AutoSize = true;
            this.qouteLabel.Location = new System.Drawing.Point(545, 18);
            this.qouteLabel.Name = "qouteLabel";
            this.qouteLabel.Size = new System.Drawing.Size(176, 16);
            this.qouteLabel.TabIndex = 20;
            this.qouteLabel.Text = "Do you need a quote today?";
            // 
            // currentDate
            // 
            this.currentDate.AutoSize = true;
            this.currentDate.Location = new System.Drawing.Point(225, 18);
            this.currentDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.currentDate.Name = "currentDate";
            this.currentDate.Size = new System.Drawing.Size(44, 16);
            this.currentDate.TabIndex = 21;
            this.currentDate.Text = "label2";
            // 
            // monthForward
            // 
            this.monthForward.Location = new System.Drawing.Point(367, 12);
            this.monthForward.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.monthForward.Name = "monthForward";
            this.monthForward.Size = new System.Drawing.Size(65, 28);
            this.monthForward.TabIndex = 22;
            this.monthForward.Text = "=>";
            this.monthForward.UseVisualStyleBackColor = true;
            this.monthForward.Click += new System.EventHandler(this.monthForward_Click);
            // 
            // monthBack
            // 
            this.monthBack.Location = new System.Drawing.Point(75, 12);
            this.monthBack.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.monthBack.Name = "monthBack";
            this.monthBack.Size = new System.Drawing.Size(65, 28);
            this.monthBack.TabIndex = 23;
            this.monthBack.Text = "<=";
            this.monthBack.UseVisualStyleBackColor = true;
            this.monthBack.Click += new System.EventHandler(this.monthBack_Click);
            // 
            // buttonHelp
            // 
            this.buttonHelp.Location = new System.Drawing.Point(622, 472);
            this.buttonHelp.Name = "buttonHelp";
            this.buttonHelp.Size = new System.Drawing.Size(66, 24);
            this.buttonHelp.TabIndex = 24;
            this.buttonHelp.Text = "Help";
            this.buttonHelp.UseVisualStyleBackColor = true;
            this.buttonHelp.Click += new System.EventHandler(this.buttonHelp_Click);
            // 
            // HabitTracker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 580);
            this.Controls.Add(this.buttonHelp);
            this.Controls.Add(this.monthBack);
            this.Controls.Add(this.monthForward);
            this.Controls.Add(this.currentDate);
            this.Controls.Add(this.qouteLabel);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.quotesBox);
            this.Controls.Add(this.buttonSaveJurnal);
            this.Controls.Add(this.richJurnal);
            this.Controls.Add(this.journalHabit);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.checkHabit);
            this.Controls.Add(this.habitInfoLabel);
            this.Controls.Add(this.addNewHabit);
            this.Controls.Add(this.habitFrequencyLabel);
            this.Controls.Add(this.habitNameLabel);
            this.Controls.Add(this.habitFrequency);
            this.Controls.Add(this.habitName);
            this.Controls.Add(this.maximStreak);
            this.Controls.Add(this.currentStreak);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "HabitTracker";
            this.Text = "Form4";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form4_FormClosing);
            this.Load += new System.EventHandler(this.Form4_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox currentStreak;
        private System.Windows.Forms.TextBox maximStreak;
        private System.Windows.Forms.TextBox habitName;
        private System.Windows.Forms.TextBox habitFrequency;
        private System.Windows.Forms.Label habitNameLabel;
        private System.Windows.Forms.Label habitFrequencyLabel;
        private System.Windows.Forms.Button addNewHabit;
        private System.Windows.Forms.Label habitInfoLabel;
        private System.Windows.Forms.CheckBox checkHabit;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button journalHabit;
        private System.Windows.Forms.RichTextBox richJurnal;
        private System.Windows.Forms.Button buttonSaveJurnal;
        private System.Windows.Forms.ComboBox quotesBox;
        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.Label qouteLabel;
        private System.Windows.Forms.Label currentDate;
        private System.Windows.Forms.Button monthForward;
        private System.Windows.Forms.Button monthBack;
        private System.Windows.Forms.Button buttonHelp;
    }
}