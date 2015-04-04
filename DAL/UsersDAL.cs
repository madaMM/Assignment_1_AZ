using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Data.Common;
using System.Data;
using logInDotNet.Entities;

namespace logInDotNet.DAL
{
    public class UsersDAL
    {

        private String connectionString;
        MySqlCommand cmd;
        private MySqlConnection conn;
        public UsersDAL()
        {
            connectionString = String.Format("server={0};user id={1}; password={2}; database={3}; pooling=false", "localhost", "root", "root", "login");
            conn = new MySqlConnection(connectionString);  
         
        }

        


        public User getUser(String username, String password)
        {
            User u = null;
            String sql = "SELECT * FROM users WHERE username='" + username + "' AND password='" + password + "'";
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                u = new User(reader["username"].ToString(), reader["password"].ToString(), reader["name"].ToString(), reader["role"].ToString());
                conn.Close();

            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {
                conn.Close();
            }
            return u;
        }


        public void updatePass( String username,String password)
        {
            User u = null;
            String sql = "UPDATE Users set password='"+ password + "'  WHERE username='" + username + "'";
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                conn.Close();

            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
               
            }
           
        }


        ////Load all user information to display in dataGridView////
        public DataTable loadData()
        {
            MySqlDataAdapter dataAdap;
            DataTable data = new DataTable();
            try
            {
                conn.Open();
                cmd = conn.CreateCommand();
                dataAdap=new MySqlDataAdapter("SELECT * FROM users",conn);
                dataAdap.Fill(data);
         
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return data;
        }


        ////Get the names from database to display in comboBox////
        public List<String> comboData()
        {
            List<String> aux=new List<String>();
          
            try
            {
                conn.Open();
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM users";
                MySqlDataReader mdr = cmd.ExecuteReader();
                while (mdr.Read())
                {
                    aux.Add( mdr.GetString("name"));
                }
                //cmd.ExecuteNonQuery();

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

        //CRUD on Employees
        public void addE(User a)
        {


            try
            {
                conn.Open();
                cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO users(username,password,name,role) VALUES (@u,@p,@n,@r)";
                cmd.Parameters.AddWithValue("@u", a.getUsername());
                cmd.Parameters.AddWithValue("@p", a.getPassword());
                cmd.Parameters.AddWithValue("@n", a.getName());
                cmd.Parameters.AddWithValue("@r", a.getRole());
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

        public void deleteE(String username)
        {
            try
            {
                conn.Open();
                cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM users WHERE username = @u";
                cmd.Parameters.AddWithValue("@u", username);
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

        public void updateE(User u)
        {


            try
            {
                conn.Open();
                cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE users SET username=@u,password=@p,name=@n,role=@r WHERE username=@u";
                cmd.Parameters.AddWithValue("@u", u.getUsername());
                cmd.Parameters.AddWithValue("@p", u.getPassword());
                cmd.Parameters.AddWithValue("@n", u.getName());
                cmd.Parameters.AddWithValue("@r", u.getRole());
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

        ////Getting info from database with item selected in comboBox////
        public List<String> selectedC(String name)
        {
            List<String> aux = new List<String>();
            try
            {
                conn.Open();
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM users WHERE name=@n";
                cmd.Parameters.AddWithValue("@n", name);
                MySqlDataReader mdr = cmd.ExecuteReader();
                while (mdr.Read())
                {
                    String username = mdr.GetString("username");
                    String password = mdr.GetString("password");
                    String namee = mdr.GetString("name");
                    String role = mdr.GetString("role");

                    aux.Add(username);
                    aux.Add(password);
                    aux.Add(namee);
                    aux.Add(role);

                }
                //cmd.ExecuteNonQuery();

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


    }
}
