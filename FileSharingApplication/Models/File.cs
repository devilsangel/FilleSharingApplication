using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileSharingApplication.Models
{
    public class File
    {
        public int id { get; set; }
        public string filename { get; set; }
        public int userId { get; set; }
        public File(string filename, int userId, int id = 0)
        {
            this.id = id;
            this.filename = filename;
            this.userId = userId;
        }
    }
}