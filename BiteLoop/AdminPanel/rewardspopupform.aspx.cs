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

public partial class controlpanel_Rewardspopupform : AdminAuthentication
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
        if (Request["updaterewards"] != null)
        {
            Updaterewards();
        }
        if (Convert.ToString(Request["type"]) == "userreward")
        {
            int.TryParse(Request["id"], out this.ID);
            rptContactUs.Visible = true;
            BindRewardsPoints();
            h4head.InnerText = "Modify Rewards Points";
        }      
    }

    private void Updaterewards()
    {
        if (Request["rewards"] != null)
        {
            UpdateUserRewards(Convert.ToDecimal(Request["rewards"]), Convert.ToInt64(Request["id"]));
        }
        Response.End();
    }
    public void UpdateUserRewards(decimal Rewards, long UserID)
    {
        DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@UserID", DbParameter.DbType.Int, 10, UserID), 
                new DbParameter("@Rewards", DbParameter.DbType.Decimal, 50, Rewards)
            };
        DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "UpdateUserRewards", dbParam);
    }
}
