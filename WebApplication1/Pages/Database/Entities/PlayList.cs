using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kuroneko.Pages.Database.Entities
{
    public class PlayList
    {
        public int id { get; set; }
        public string name { get; set; }
        public int ItemSelected { get; set; }
        public string Currenttime { get; set; }
        public int numberOfFiles { get; set; }

        public PlayList(int id,
                        string names,
                        int ItemSelected,
                        string Currenttime,
                        int numberOfFiles)
        {
            this.id = id;
            this.name = name;
            this.ItemSelected = ItemSelected;
            this.Currenttime = Currenttime;
            this.numberOfFiles = numberOfFiles;
        }
        public PlayList(
                    string names,
                    int ItemSelected,
                    string Currenttime,
                    int numberOfFiles)
        {
          
            this.name = name;
            this.ItemSelected = ItemSelected;
            this.Currenttime = Currenttime;
            this.numberOfFiles = numberOfFiles;
        }
    }
}