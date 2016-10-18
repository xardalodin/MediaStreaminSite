using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kuroneko.Pages.Database.Entities
{
    public class User
    {
        public int id { get; set;}
        public string username { get; set; }
        public string user_type { get; set; }
        public string password { get; set; }
        public string pass_salt { get; set; }

        public int tries { get; set; }
        public DateTime wait_time { get; set; }


        public User(int id,
                    string username,
                    string user_type,
                    string password,
                    string pass_salt,
                    int tries,
                    DateTime wait_time) 
        {
            this.id = id;
            this.username = username;
            this.password = password;
            this.user_type = user_type;
            this.pass_salt = pass_salt;
            this.tries = tries;
            this.wait_time = wait_time;           
        }

        public User(
                 string username,
                 string user_type,
                 string password)
        {
          
            this.username = username;
            this.password = password;
            this.user_type = user_type;
        }

        public User(
                string username,
                string user_type,
                DateTime wait_time)
        {
            this.username = username;
            this.user_type = user_type;
            this.wait_time = wait_time;
        }
    }
}