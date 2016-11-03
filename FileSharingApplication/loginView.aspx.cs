using FileSharingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FileSharingApplication
{
    public partial class loginView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            GlobalVariables.u = Database.Instance.login(email.Text, pwd.Text);
            if (GlobalVariables.u != null)
            {
                Response.Redirect("uploadView.aspx");
            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Invalid Username and Password')</script>");
            }
        }
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            User u = new User(email.Text, pwd.Text, 0);
            u.id = Database.Instance.insert(u);
            GlobalVariables.u = u;
            Response.Redirect("uploadView.aspx");
        }
    }
}