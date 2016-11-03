using FileSharingApplication.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FileSharingApplication
{
    public sealed class Database
    {
        private SqlConnection con;
        private static readonly Lazy<Database> lazy =
        new Lazy<Database>(() => new Database());

        public static Database Instance { get { return lazy.Value; } }

        private Database()
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
            con.Open();
        }

        public User login(string email, string pass)
        {
            SqlCommand cmd = new SqlCommand("select * from users where email=@email and password=@password", con);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", GlobalVariables.md5(pass));
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return new User(dt.Rows[0]);
            }
            else
            {
                return null;
            }
        }
        public List<string> getAllUsers()
        {
            List<string> res = new List<string>();
            SqlCommand cmd = new SqlCommand("Select email from users", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                res.Add((string)row["email"]);
            }
            return res;
        }
        public int insert(File f)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO files (filename, ownerId) output inserted.id VALUES (@filename, @userId)", con);
            cmd.Parameters.AddWithValue("@filename", f.filename);
            cmd.Parameters.AddWithValue("@userId", f.userId);
            int i = -1;
            try
            {
                i = (int)cmd.ExecuteScalar();
            }
            catch(SqlException e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            return i;
        }
        public User getUser(string email)
        {
            SqlCommand cmd = new SqlCommand("Select * from users where email=@email", con);
            cmd.Parameters.AddWithValue("@email", email);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            User u=null;
            if (dt.Rows.Count > 0)
                u = new User(dt.Rows[0]);
            return u;
        }
        public File getFile(string filename)
        {
            SqlCommand cmd = new SqlCommand("Select * from files where filename=@filename", con);
            cmd.Parameters.AddWithValue("@filename", filename);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            File f = null;
            if (dt.Rows.Count > 0)
                f = new File((string)dt.Rows[0]["filename"],(int)dt.Rows[0]["ownerId"],(int)dt.Rows[0]["Id"]);
            return f;
        }
        public void insert(Perms p)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO perms (userId, fileId) VALUES (@userId, @fileId)", con);
            cmd.Parameters.AddWithValue("@userId", p.userId);
            cmd.Parameters.AddWithValue("@fileId", p.fileId);
            try
            {
                cmd.ExecuteNonQuery();
            }catch(SqlException e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
        }
        public int insert(User u)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO users (email, password, level) output inserted.id VALUES (@email, @password, @level)",con);
            cmd.Parameters.AddWithValue("@email", u.email);
            cmd.Parameters.AddWithValue("@password", GlobalVariables.md5(u.password));
            cmd.Parameters.AddWithValue("@level", u.level);
            return (int)cmd.ExecuteScalar();
        }
        public List<string> getFiles(User u)
        {
            List<string> res=new List<string>();
            SqlCommand cmd = new SqlCommand("Select filename from perms,files where userId=@userId and fileId=Id",con);
            cmd.Parameters.AddWithValue("@userId", u.id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach(DataRow row in dt.Rows){
                res.Add((string)row["filename"]);
            }
            return res;
        }
    }
}