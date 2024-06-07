using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using waCubos.Properties;

namespace waCubos
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string[] strArray = new bin("GENERAL").login(this.TextBox1.Text, this.TextBox2.Text);
                if (strArray != null)
                {

                    this.Session["ctrluser"] = "login";
                    this.Session["username"] = strArray;
                    this.Response.Redirect("Default.aspx");
                }
                this.Response.Redirect("login.aspx");
            }
            catch (Exception ex)
            {
                this.tag.Text = ex.Message;
            }
        }
    }
}