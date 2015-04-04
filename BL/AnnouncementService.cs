using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using MySql.Data.MySqlClient;
using logInDotNet.DAL;
using logInDotNet.Entities;


namespace logInDotNet.BL
{
    public class AnnouncementService
    {
        private AnnouncementDAL af = new AnnouncementDAL();


        ////View titles in comboBox////
        public List<String> viewComboAn()
        {
            List<String> list = af.comboDataAn();
            return list;
        }

       ////Getting data from database with comboBox////
        public List<String> selectedComboAn(String title)  
        {                                                  
            List<String> list = af.selectedCoAn(title);
            return list;
        }

        public byte[] selectedComboAnImage(String title)
        {
            byte[] image = af.selectedCoAnnImage(title);
            return image;
        }



        ////CRUD operations from DAL//// 
        public void addAnnoucement(int id, String title, String description, byte[] image,String username)
        {
            Announcement a = new Announcement(id,title,description,image,username);
            af.addA(a);


        }

        public void deleteAnnouncement(int id)
        {
            af.deleteA(id);

        }

        public void updateAnnouncement(int id, String title, String description, byte[] image, String username)
        {
            Announcement a = new Announcement(id, title, description, image,username);
            af.updateA(a);
        }

        public int viewRap(String username)
        {
           int nr= af.viewRap(username);
           return nr;
        }

    }
}
