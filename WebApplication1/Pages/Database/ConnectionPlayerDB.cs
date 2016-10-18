/*
 * Author: mikael hurtig
 * 
 Login:(id = int PK
 *      username = varchar(64)
 *      user_type = varchar(20)     
 *      password = varchar(64)
 *      user_salt = varchar(10);    
 *      pass_salt   = varchar(10);
 *      tries = int           
 *      wait_time = datetime    may be null
 * )
 
 * Playlist(id = int PK
 *          name = varchar(50)     
 *          ItemSelected = int
 *          Currenttime = varchar(50)
 *          numberOfFiles = int
 * )
 
 * FilesInPlaylist( Id = int PK
 *                  IdPlayList = int FK
 *                  IdListOfFiles = int FK        
 *                  NumberOrder = int 
 * )
  
 *ListOfFiles(id = int PK
 *            filename = varchar(50)
 *            media_type = varchar(50)
 * 
 * ) 
 * 
 * 
 
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;

namespace kuroneko.Pages.Database
{
    public static class ConnectionPlayerDB
    {
        private static SqlConnection connection;
        private static SqlCommand command;

        static ConnectionPlayerDB()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PlayerDBConnection"].ToString();
            connection = new SqlConnection(connectionString);
            command = new SqlCommand("", connection);
        }

        public static string Register(kuroneko.Pages.Database.Entities.User user) 
        {
            // validation done beore this 
            // count number of user with this name , hoping it to be zero;
            string query = string.Format("SELECT COUNT(*) FROM Login WHERE username = @username ");
            command.CommandText = query;

            try 
            {
                connection.Open();
                               
                command.Parameters.Add(new SqlParameter("@username",user.username));
                
                int amountOFUsers = (int)command.ExecuteScalar();
                command.Parameters.Clear();
                
                if (amountOFUsers < 1) // user does not exist
                {
                    query = string.Format("INSERT INTO Login VALUES (@username, @user_type, @password, @pass_salt, @tries, @wait_time)");
                    command.CommandText = query;

                    Security.Hasher hasher = new Security.Hasher();                    
                    string passSalt;
                    string password = hasher.GenSaltSHA256(user.password, out passSalt);

                    command.Parameters.Add(new SqlParameter("@username", user.username)); //parameter to store the hashed username

                    command.Parameters.Add(new SqlParameter("@user_type", user.user_type)); // maybe hash too mmm 
                    
                    command.Parameters.Add(new SqlParameter("@password", password)); //parameter to store the hashed password
                    
                    command.Parameters.Add(new SqlParameter("@pass_salt", passSalt));
                    
                    int tries = 0; // no failed password logins yes
                    DateTime time = DateTime.Now;

                    command.Parameters.Add(new SqlParameter("@tries", System.Data.SqlDbType.Int));
                    command.Parameters["@tries"].Value = tries;

                    command.Parameters.Add(new SqlParameter("@wait_time", System.Data.SqlDbType.DateTime));
                    command.Parameters["@wait_time"].Value = time;

                    
                 
                    command.ExecuteNonQuery();   // store user 
                    command.Parameters.Clear();

                    return "registration successful";
                }
                else 
                {
                    return "a User with this name exists";
                
                }

            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// returns null if no user found,,
        /// returns usertype = "wait_time" means to many trie. 
        /// returns User(username, user_type, currenttime); if found,,
        /// check so user is NOT NULL,,
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static Pages.Database.Entities.User LoginUser(string username, string password)
        {
            string query = string.Format("SELECT COUNT(*) FROM Login WHERE username = @username ");
            command.CommandText = query;

            try 
            {
                connection.Open();

                command.Parameters.Add(new SqlParameter("@username",username));
                
                int amountOFUsers = (int)command.ExecuteScalar();


                if (amountOFUsers == 1) // only one 
                {
                    // now get password. password salt, Date and tries to see if locked out
                    query = string.Format("SELECT password, pass_salt,tries, wait_time FROM Login WHERE username = @username");
                    command.CommandText = query;
                    SqlDataReader reader = command.ExecuteReader();
                                         
                    string Hashed_Pass_salt = null;
                    string Hashed_password = null;
                    int tries = 0;
                    DateTime storeddate = DateTime.Now;
                    DateTime currenttime = DateTime.Now;  

                    currenttime.ToLocalTime();
                    while (reader.Read())
                    { 
                        Hashed_password = reader.GetString(0);
                        Hashed_Pass_salt = reader.GetString(1);
                        tries = reader.GetInt32(2);
                        storeddate = reader.GetDateTime(3);
                    }
                    reader.Close();

                    if (0 < DateTime.Compare(currenttime, storeddate)) // T1 if later than T2 = greater than zero IF FAIL CANT LOGIN at this time   
                    {       
                        Security.Hasher hasher = new Security.Hasher();

                        if (hasher.TestPassword(password, Hashed_Pass_salt, Hashed_password)) // pasword check.
                        {
                            // reset user tries on successfull login
                            tries = 0;
                            query = string.Format("UPDATE Login SET tries = @tries WHERE username =@username");
                            command.Parameters.Add(new SqlParameter("@tries", System.Data.SqlDbType.Int));
                            command.Parameters["@tries"].Value = tries;
                            command.CommandText = query;
                            command.ExecuteNonQuery();  // update


                            // get user information going to be more info so suing reader.
                            query = string.Format("SELECT user_type FROM Login WHERE username = @username");
                            command.CommandText = query;
                            
                            SqlDataReader reader2 = command.ExecuteReader();
                            string user_type = "user";
                            while (reader2.Read())
                            {
                                user_type = reader2.GetString(0);
                            }
                            reader2.Close();
                            command.Parameters.Clear();
                            // create return of user 
                            Pages.Database.Entities.User user = null;

                            user = new Entities.User(username, user_type, currenttime);

                            return user;
                        }
                        else
                        {
                            tries++; // Incriment the tries by one
                            query = string.Format("UPDATE Login SET tries = @tries WHERE username =@username");
                            command.Parameters.Add(new SqlParameter("@tries", System.Data.SqlDbType.Int));
                            command.Parameters["@tries"].Value = tries;

                            if (tries > 3) // three strikes your out add 1*tries min , change Query
                            {
                               currenttime = currenttime.AddMinutes(1 * tries); // the more you fail the longer this is gona take.

                                query = string.Format("UPDATE Login SET tries = @tries, wait_time=@wait_time WHERE username =@username");
                                command.Parameters.Add(new SqlParameter("@wait_time", System.Data.SqlDbType.DateTime));
                                command.Parameters["@wait_time"].Value = currenttime;
                            }

                            command.CommandText = query;
                            command.ExecuteNonQuery();
                            command.Parameters.Clear();
                            ///YOU ARE HERE add parameters 

                            return null;
                        }
                    }
                    else
                    {
                        Pages.Database.Entities.User user = null;

                        user = new Entities.User(username, "wait_time", storeddate);
                        command.Parameters.Clear();
                        return user;
                      
                    }

                 
                }
                else // no user
                { 
                    return null;
                  
                }

              
            }

            
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
        
        
        }

        public static string StoreFiles(string filename,string media_type)
        {
            string query = string.Format("INSERT INTO ListOfFiles VALUES (@filename, @media_type)");
            command.CommandText = query;
            command.Parameters.Add(new SqlParameter("@filename", filename));
            command.Parameters.Add(new SqlParameter("@media_type", media_type));

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                command.Parameters.Clear();
                return "success";
            }
            catch (Exception)
            { 
                return "failed";
            }
            finally
            {
                connection.Close();
            }

        
        }

        public static ArrayList GetFiles()
        {
            string query = string.Format("SELECT * FROM ListOfFiles");
            command.CommandText = query;
            try
            {
                connection.Open();

                ArrayList files = new ArrayList();
              
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string filename = reader.GetString(1);
                    string movie_type = reader.GetString(2);
                    Database.Entities.ListOfFiles temp = new Entities.ListOfFiles(id,filename,movie_type);
                    files.Add(temp);
                }
               
                return files;
               
            }
            finally
            {
                connection.Close();
            }

        }
    }
}