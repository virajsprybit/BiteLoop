using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class GooglePlace : System.Web.UI.Page
{
    protected string GoogleMapApiKey = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        GoogleMapApiKey = Convert.ToString(ConfigurationManager.AppSettings["GoogleMapApiKey"]).Trim();
    }
}