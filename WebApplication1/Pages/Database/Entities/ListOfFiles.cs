using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kuroneko.Pages.Database.Entities
{
    public class ListOfFiles
    {
        public int id { get; set; }
        public string filename { get; set; }
        public string media_type { get; set; }

        public ListOfFiles(int id,string filename,string media_type)
        {
            this.id = id;
            this.filename = filename;
            this.media_type = media_type;       
        }
        public ListOfFiles(string filename, string media_type)
        {
            this.filename = filename;
            this.media_type = media_type;
        }

    }
}