using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;
using logInDotNet.BL;


namespace logInDotNet
{
    public partial class Form3 : Form
    {
        AnnouncementService announce = new AnnouncementService();
        String usernameR="";
        String picPath = "";
        public Form3(String s,String username)
        {
            InitializeComponent();
            label1.Text = s;
            usernameR = username;
        }

        public void clearTextBox()
        {
            textBoxId.Text = String.Empty;
            textBoxTitle.Text = String.Empty;
            textBoxDescription.Text = String.Empty;
         

        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "JPG Files(*.jpg)|*.jpg|PNG Files(*.png)|*.png|All Files(*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                 picPath = dlg.FileName.ToString();
                textBoxPath.Text = picPath;
                pictureBox1.ImageLocation = picPath;
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            
            int id = int.Parse(textBoxId.Text);
            String title = textBoxTitle.Text;
            String description = textBoxDescription.Text;

            byte[] imageBt = null;
            FileStream fstream = new FileStream(this.textBoxPath.Text,FileMode.Open,FileAccess.Read);
            BinaryReader br = new BinaryReader(fstream);
            imageBt = br.ReadBytes((int)fstream.Length);

            try
            {
                announce.addAnnoucement(id, title, description, imageBt, usernameR);
                MessageBox.Show("Added");

            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            clearTextBox();
            comboBoxAnn.Items.Clear();
            loadDataCombo();
           

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBoxId.Text);
            try
            {
                announce.deleteAnnouncement(id);
                MessageBox.Show("Deleted");
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
           
            clearTextBox();
            comboBoxAnn.Items.Clear();
            loadDataCombo();


        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBoxId.Text);
            String title = textBoxTitle.Text;
            String description = textBoxDescription.Text;

            byte[] imageBt = null;
            FileStream fstream = new FileStream(this.textBoxPath.Text, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fstream);
            imageBt = br.ReadBytes((int)fstream.Length);
            try
            {
                announce.updateAnnouncement(id, title, description, imageBt, usernameR);
                MessageBox.Show("Updated");
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            clearTextBox();
            comboBoxAnn.Items.Clear();
            loadDataCombo();

        }

        public void loadDataCombo()
        {
            List<String> listCombi = announce.viewComboAn();
            foreach (String I in listCombi)
            {
                comboBoxAnn.Items.Add(I);

            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

            loadDataCombo();
            MessageBox.Show("Welcome " + usernameR);
        }

        private void comboBoxAnn_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<String> list = announce.selectedComboAn(comboBoxAnn.Text);
            textBoxId.Text = list[0];
            textBoxTitle.Text = list[1];
            textBoxDescription.Text = list[2];
           

            byte[] image = announce.selectedComboAnImage(comboBoxAnn.Text);
            if (image == null)
            {
                    pictureBox1.Image = null;
            }
            else
            {
                    MemoryStream mstream = new MemoryStream(image);
                    pictureBox1.Image = System.Drawing.Image.FromStream(mstream);
            }
            
        }
    }
}
