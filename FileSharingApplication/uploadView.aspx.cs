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
    public partial class uploadView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (GlobalVariables.u == null)
                Response.Redirect("loginView.aspx");
            if (!Page.IsPostBack)
            {

                List<string> a = Database.Instance.getFiles(GlobalVariables.u);
                DataGrid.DataSource = a;
                DataGrid.DataBind();
                foreach(RepeaterItem i in DataGrid.Items)
                {
                    DropDownList d = (DropDownList)i.Controls[3];
                    d.DataSource = Database.Instance.getAllUsers();
                    d.DataBind();
                }
                
            }
            if ((File1.PostedFile != null) && (File1.PostedFile.ContentLength > 0))
            {
                string fn = System.IO.Path.GetFileName(File1.PostedFile.FileName);
                string SaveLocation = Server.MapPath("Data") + "\\" + fn;
                File f = new File(fn, GlobalVariables.u.id);
                f.id = Database.Instance.insert(f);
                if (f.id >= 0)
                {
                    Database.Instance.insert(new Perms(GlobalVariables.u.id, f.id));
                    try
                    {
                        File1.PostedFile.SaveAs(SaveLocation);
                        Response.Write("The file has been uploaded.");
                    }
                    catch (Exception ex)
                    {
                        Response.Write("Error: " + ex.Message);
                        //Note: Exception.Message returns a detailed message that describes the current exception. 
                        //For security reasons, we do not recommend that you return Exception.Message to end users in 
                        //production environments. It would be better to put a generic error message. 
                    }
                }else
                {
                    Response.Write("Error: File with same name already exists");
                }
            }
            else
            {
                Response.Write("Please select a file to upload.");
            }
        }
        public void Repeater_btn(Object Sender, RepeaterCommandEventArgs e)
        {
            if (e.CommandArgument.Equals("dwn"))
            {
                string strURL = "/Data/" + e.CommandName;
                WebClient req = new WebClient();
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.Buffer = true;
                response.AddHeader("Content-Disposition", "attachment;filename=" + e.CommandName);
                byte[] data = req.DownloadData(Server.MapPath(strURL));
                response.BinaryWrite(data);
                response.End();
            }else
            {
                DropDownList d = (DropDownList)DataGrid.Items[e.Item.ItemIndex].Controls[3];
                User u=Database.Instance.getUser(d.SelectedItem.ToString());
                File f = Database.Instance.getFile(e.CommandName);
                Perms p = new Perms(u.id, f.id);
                Database.Instance.insert(p);
            }
        }
    }
}