using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FileSharingApplication.Models
{
    public class User
    {
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int level { get; set; }
        public User(string email, string password, int level)
        {
            this.email = email;
            this.password = password;
            this.level = level;
        }
        public User(DataRow row)
        {
            id = (int)row["Id"];
            email = (string)row["email"];
            password = (string)row["password"];
            level = (int)row["level"];
        }
    }
}