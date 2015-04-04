using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using logInDotNet.BL;
using logInDotNet.Entities;


namespace logInDotNet
{
    public partial class Form1 : Form
    {
        public UserService user;
        public Form1()
        {
            InitializeComponent();
            user = new UserService();
            passTxtBox.PasswordChar='*';
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        public void clearTxt()
        {
            userTxtBox.Text = String.Empty;
            passTxtBox.Text = String.Empty; 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String username = userTxtBox.Text;
            String password = passTxtBox.Text;

            User u = user.logIn(username, password);
            if (u != null)
            {
                String auxRole = u.getRole();
                if (auxRole.Equals("u"))
                {
                    Form3 p = new Form3("Employee",username);
                    p.Show();
                   
                }
                else if (auxRole.Equals("a"))
                {
                    Form2 f = new Form2("Administrator");
                    f.Show();
                }

                
            }
            else
            {
                MessageBox.Show("Username or password incorrect");
                clearTxt();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            String username = userTxtBox.Text;
           
            int d;
            String pass = "";
            String aux = "abcdefghijklmnopqrstuvwxyz";
            Random rnd = new Random();
            int nr = rnd.Next(8, 10);
            for (int f = 0; f < nr; f++)
             {
                 d = rnd.Next(1, 25);
                 pass = pass + aux[d];

             }
            String s=user.upd(username,pass);
            MessageBox.Show("Noua parola "+pass);
        }
    }
}
