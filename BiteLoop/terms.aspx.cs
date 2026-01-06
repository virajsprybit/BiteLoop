using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using System.Data;
using BAL;

public partial class terms : System.Web.UI.Page
{
    protected string strContent = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        CMSBAL objCMSBAL = new CMSBAL();
        DataTable dt=new DataTable();
        objCMSBAL.ID = 3;
        dt = objCMSBAL.GetCMSByID();
        strContent = Convert.ToString(dt.Rows[0]["PageDescription"]).Replace("{%WebSiteUrl%}",Config.WebSiteUrl);
    }
}