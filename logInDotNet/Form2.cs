using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using logInDotNet.BL;

namespace logInDotNet
{
    public partial class Form2 : Form
    {
        UserService userService = new UserService();
        AnnouncementService annService = new AnnouncementService();
      
        public Form2(String s)
        {
            InitializeComponent();
            label1.Text = s;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            List<String> listCombi=userService.viewCombo();
            loadDataCombo();
        }

        public void clearTextBox()
        {
            textBoxUserName.Text = String.Empty;
            textBoxPassword.Text = String.Empty;
            textBoxName.Text = String.Empty;
            textBoxRole.Text = String.Empty;



        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            String username = textBoxUserName.Text;
            String password = textBoxPassword.Text;
            String name = textBoxName.Text;
            String role = textBoxRole.Text;

            try
            {
                userService.addEmployee(username, password, name, role);
                MessageBox.Show("Created");
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            clearTextBox();
            comboBoxEmployee.Items.Clear();
            loadDataCombo();

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            String username = textBoxUserName.Text;
            try
            {
                userService.deleteEmployee(username);
                MessageBox.Show("Deleted");
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            clearTextBox();
            comboBoxEmployee.Items.Clear();
            loadDataCombo();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            String username = textBoxUserName.Text;
            String password = textBoxPassword.Text;
            String name = textBoxName.Text;
            String role = textBoxRole.Text;

            try
            {
                userService.updateEmployee(username, password, name, role);
                MessageBox.Show("Updated");
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            clearTextBox();
            comboBoxEmployee.Items.Clear();
            loadDataCombo();

        }

        private void buttonView_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource=userService.viewData();
            dataGridView1.Update();
            dataGridView1.Refresh(); 
        }

        private void comboBoxEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {

            List<String> list = userService.selectedCombo(comboBoxEmployee.Text);
            textBoxUserName.Text = list[0];
            textBoxPassword.Text = list[1];
            textBoxName.Text = comboBoxEmployee.Text;
            textBoxRole.Text = list[3];
        }

        public void loadDataCombo()
        {
            List<String> listCombi = userService.viewCombo();
            foreach (String I in listCombi)
            {
                comboBoxEmployee.Items.Add(I);

            }  
        }

        private void buttonRap_Click(object sender, EventArgs e)
        {
            
            int number = annService.viewRap(textBoxUserName.Text);
            MessageBox.Show("The Employee with username " + textBoxUserName.Text +"\n"+
                             "introduced  "+number+" articles");
        }
    }
}
