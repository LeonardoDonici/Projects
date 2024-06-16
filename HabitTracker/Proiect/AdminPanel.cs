using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proiect
{
    public partial class AdminPanel : Form
    {
        public AdminPanel()
        {
            InitializeComponent();

            populateComboBox();

        }

        private void populateComboBox()
        {
            List<string> userList = ProxyAccessManager.Instance.getUserList();
            comboBox1.Items.Clear();
            
            foreach (string user in userList)
            {
                comboBox1.Items.Add(user);
            }

            if(comboBox1.Items.Count != 0)
                comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try { 
                if (comboBox1.SelectedItem != null)
                    ProxyAccessManager.Instance.removeUser(comboBox1.SelectedItem.ToString());
                else throw new Exception("Invalid user");


                List<string> userList = ProxyAccessManager.Instance.getUserList();
                foreach (string user in userList)
                {
                    comboBox1.Items.Add(user);
                }

                populateComboBox();

            }catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
