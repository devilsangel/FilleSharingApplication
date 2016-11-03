using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileSharingApplication.Models
{
    public class Perms
    {
        public int userId { get; set; }
        public int fileId { get; set; }
        public Perms(int userId, int fileId)
        {
            this.userId = userId;
            this.fileId = fileId;
        }
    }
}