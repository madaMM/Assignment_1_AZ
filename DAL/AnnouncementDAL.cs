using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using logInDotNet.Entities;
using System.Drawing;

namespace logInDotNet.DAL
{
    public class AnnouncementDAL
    {

        private String connectionString;
        MySqlCommand cmd;
        private MySqlConnection conn;
        public AnnouncementDAL()
        {
            connectionString = String.Format("server={0};user id={1}; password={2}; database={3}; pooling=false", "localhost", "root", "root", "login");
            conn = new MySqlConnection(connectionString);            

        }


        ////Get the titles to display in comboBox////
        public List<String> comboDataAn()
        {
            List<String> aux = new List<String>();

            try
            {
                conn.Open();
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM announcement";
                MySqlDataReader mdr = cmd.ExecuteReader();
                while (mdr.Read())
                {
                    aux.Add(mdr.GetString("title"));
                }
                

            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return aux;

        }


        ////Getting data from database with item selected in comboBox////
        public List<String> selectedCoAn(String title)
        {
            List<String> aux = new List<String>();
          
            try
            {
                conn.Open();
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM announcement WHERE title=@t";
                cmd.Parameters.AddWithValue("@t", title);
                MySqlDataReader mdr = cmd.ExecuteReader();
                while (mdr.Read())
                {
                    String id = mdr.GetInt32("idannouncement").ToString();
                    String titlee = mdr.GetString("title");
                    String description = mdr.GetString("description");

                    //byte[] img = (byte[])mdr["image"];
                   // String result = AnnouncementDAL.GetString(img);
                  
                    aux.Add(id);
                    aux.Add(titlee);
                    aux.Add(description);
                 

                }
              

            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return aux;
        }


        ////Getting image from database with item selected in comboBox////
        public byte[] selectedCoAnnImage(String title)
        {
            byte[] img = null;

            try
            {
                conn.Open();
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM announcement WHERE title=@t";
                cmd.Parameters.AddWithValue("@t", title);
                MySqlDataReader mdr = cmd.ExecuteReader();
                while (mdr.Read())
                {
                    img = (byte[])mdr["image"];
                  
                }

            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return img;
        }



        //CRUD OPERATIONS//

        public void addA(Announcement a)
        {


            try
            {
                conn.Open();
                cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO announcement(idannouncement,title,description,image,username) VALUES (@i,@t,@d,@im,@u)";
                cmd.Parameters.AddWithValue("@i", a.getId());
                cmd.Parameters.AddWithValue("@t", a.getTitle());
                cmd.Parameters.AddWithValue("@d", a.getDescription());
                cmd.Parameters.AddWithValue("@im",a.getImage() );
                cmd.Parameters.AddWithValue("@u", a.getUsernameRap());
                cmd.ExecuteNonQuery();

            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void deleteA(int id)
        {
            try
            {
                conn.Open();
                cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM announcement WHERE idannouncement = @i";
                cmd.Parameters.AddWithValue("@i", id);
                cmd.ExecuteNonQuery();

            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void updateA(Announcement a)
        {


            try
            {
                conn.Open();
                cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE announcement SET title=@t,description=@d,image=@im,username=@u WHERE idannouncement=@i";
                cmd.Parameters.AddWithValue("@i", a.getId());
                cmd.Parameters.AddWithValue("@t", a.getTitle());
                cmd.Parameters.AddWithValue("@d", a.getDescription());
                cmd.Parameters.AddWithValue("@im", a.getImage());
                cmd.Parameters.AddWithValue("@u", a.getUsernameRap());
                cmd.ExecuteNonQuery();

            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public int viewRap(String username)
        {

            int count = 0;
            try
            {
                conn.Open();
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM announcement WHERE username=@u";
                cmd.Parameters.AddWithValue("@u", username);
                
                count = Convert.ToInt32(cmd.ExecuteScalar());

            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return count;
        }

       
    }
}
