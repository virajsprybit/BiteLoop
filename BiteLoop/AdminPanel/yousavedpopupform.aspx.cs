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

public partial class controlpanel_YouSavedPopupform : AdminAuthentication
{
    private new int ID;
    private void BindRewardsPoints()
    {
        UsersBAL objUserBAL = new UsersBAL();
        objUserBAL.ID = this.ID;
        DataTable dt = new DataTable();

        dt = objUserBAL.UserDetailsByID();
        if (dt.Rows.Count > 0)
        {
            rptContactUs.DataSource = dt;
            rptContactUs.DataBind();
            divContactUs.Visible = true;
        }
    }
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["updateyousaved"] != null)
        {
            Updaterewards();
        }
        if (Convert.ToString(Request["type"]) == "yousaved")
        {
            int.TryParse(Request["id"], out this.ID);
            rptContactUs.Visible = true;
            BindRewardsPoints();
            h4head.InnerText = "Modify $ Saved";
        }      
    }

    private void Updaterewards()
    {
        if (Request["meals"] != null)
        {
            decimal Meals = 0;
            if (Convert.ToString(Request["meals"]) != string.Empty)
            {
                Meals = Convert.ToDecimal(Request["meals"]);
            }
            UpdateUserYouSaved(Meals, Convert.ToInt64(Request["id"]));
        }
        Response.End();
    }
    public void UpdateUserYouSaved(decimal YouSaved, long UserID)
    {
        DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@UserID", DbParameter.DbType.Int, 10, UserID), 
                new DbParameter("@YouSaved", DbParameter.DbType.Decimal, 50, YouSaved)
            };
        DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "UpdateUserYouSaved", dbParam);
    }
}
