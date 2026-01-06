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

public partial class controlpanel_popupform_Promocode : AdminAuthentication
{    
    protected string Category = "";
    protected string FoodItems = "";
    private void BindEnquiry()
    {
        DataTable dt = new DataTable();
        dt = PromocodeUsedUserList(Convert.ToString(Request["code"]));
        if (dt.Rows.Count > 0)
        {
            rptContactUs.DataSource = dt;
            rptContactUs.DataBind();
            tyrNoRecords.Visible = false;

        }
        else
        {
            tyrNoRecords.Visible = true;
        }
        divContactUs.Visible = true;
    }
  
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Request["type"]) == "user")
        {           
            rptContactUs.Visible = true;
            BindEnquiry();
            h4head.InnerText = Convert.ToString(Request["code"]) ;
        }        

    }

    public DataTable PromocodeUsedUserList(string Code)
    {
        DbParameter[] dbParam = new DbParameter[] { 
            new DbParameter("@Code", DbParameter.DbType.VarChar, 200, Code) };
        DataTable table = new DataTable();
        return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "PromocodeUsedUserList", dbParam);
    }
}
