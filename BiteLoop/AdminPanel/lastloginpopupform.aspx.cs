using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BAL;
using Utility;
using DAL;

public partial class controlpanel_LastLoginpopupform : AdminAuthentication
{
    private long ID = 0;
    private void BindLastLogin()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        ds = UserLastLoginHistory(ID);
        divContactUs.Visible = true;
        if (ds.Tables[0].Rows.Count > 0)
        {
            rptContactUs.DataSource = ds.Tables[0];
            rptContactUs.DataBind();
        }
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 1)
        {
            rptLocation.DataSource = ds.Tables[0];
            rptLocation.DataBind();
            trNoRecords.Visible = false;
            rptLocation.Visible = true;
        }
        else
        {
            rptLocation.Visible = false;
            trNoRecords.Visible = true;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Request["type"]) == "userlastlogin")
        {
            if (Convert.ToInt64(Request["id"]) != null)
            {
                ID = Convert.ToInt64(Request["id"]);
            }
            rptContactUs.Visible = true;
            h4head.InnerText = "Last Login";
            BindLastLogin();
        }
    }


    public DataSet UserLastLoginHistory(long UserID)
    {
        DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@UserID", DbParameter.DbType.Int, 10, UserID)
            };
        return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "UserLastLoginHistory", dbParam);
    }
    protected string GetLocationDetails(string Location)
    {
        string strLocation = "";

        if (Location != string.Empty)
        {
            if (Location.IndexOf("###") > 0)
            {
                Location = Location.Replace("###", "^");
                string[] strLocationSplit = Location.Split('^');
                string strGoogleMapURL = "http://www.google.com/maps/place/" + Convert.ToString(strLocationSplit[0]) + "," + Convert.ToString(strLocationSplit[1]);
                strLocation = "<a href='" + strGoogleMapURL + "' target='_blank'>" + Convert.ToString(strLocationSplit[2]) + "</a>";
            }
        }

        return strLocation;
    }
}
