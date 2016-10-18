using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kuroneko.Pages.Database.Entities
{
     public class FileInPlaylist
    {
      public int Id { get; set; }
      public int IdPlayList { get; set; }
      public int IdListOfFiles { get; set; }
      public int NumberOrder { get; set; }

      public FileInPlaylist(int id,int IdPlayList,int IdListOfFiles,int NumberOrder)
      {
          this.Id = id;
          this.IdPlayList = IdPlayList;
          this.IdListOfFiles = IdListOfFiles;     
      }
      public FileInPlaylist(int IdPlayList, int IdListOfFiles, int NumberOrder)
      {         
          this.IdPlayList = IdPlayList;
          this.IdListOfFiles = IdListOfFiles;
      }
    }
}