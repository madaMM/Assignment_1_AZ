using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Data.Common;
using System.Data;
using logInDotNet.DAL;
using logInDotNet.Entities;

namespace logInDotNet.BL
{
    public class UserService
    {

        private UsersDAL userDAL=new UsersDAL() ;
        User user;
        public User logIn(String username, String password)
        {
            String pass = getMd5Hash(password);
            user = userDAL.getUser(username,pass);
            return user;
        }
        public String upd(String username, String password)
        {
            String pass = getMd5Hash(password);
            userDAL.updatePass( username,pass);
            return pass;
        }


        public String updateP()
        {
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
            return pass;
        }


        public void addEmployee(String username, String password,String name, String role)
        {
            String pass = getMd5Hash(password);
            user = new User(username,pass,name,role);
            userDAL.addE(user);

        }

        public void deleteEmployee(String username)
        {
            userDAL.deleteE(username);
        }

        public void updateEmployee(String username, String password, String name, String role)
        {
            String pass = getMd5Hash(password);
            user = new User(username, pass, name, role);
            userDAL.updateE(user);
        }
       
        public DataTable viewData()
        {
            DataTable d=userDAL.loadData();
            return d;
        }
        public List<String> viewCombo()
        {
            List<String> list = userDAL.comboData();
            return list;
        }

        public List<String> selectedCombo(String name)
        {
            List<String> list = userDAL.selectedC(name);
            return list;
        }

        private string getMd5Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }


    }
}
