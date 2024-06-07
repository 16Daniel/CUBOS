using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace waCubos
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["ctrluser"] == null)
                return;
            this.Label1.Text = "Bienvenido " + ((string[])this.Session["username"])[1] + " (Salir)";
        }
    }
}
