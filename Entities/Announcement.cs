using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;


namespace logInDotNet.Entities
{
   public  class Announcement
    {
        private String title;
        private String usernameRap;
        private String description;
        private byte[] image;
        private int id;


        public Announcement(int id ,String title, String description, byte[] image,String usernameRap)
        {
            this.title = title;
            this.description = description;
            this.image = image;
            this.id=id;
            this.usernameRap = usernameRap;

        }

        public int getId()
        {
            return this.id;
        }

        public String getTitle()
        {
            return this.title;
        }

        public String getDescription()
        {
            return this.description;
        }

        public String getUsernameRap()
        {
            return this.usernameRap;
        }

        public byte[] getImage()
        {
            return this.image;
        }

        



    }
}
