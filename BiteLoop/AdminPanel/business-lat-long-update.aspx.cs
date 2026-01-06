using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using Utility;
using DAL;

public partial class AdminPanel_business_lat_long_update : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["updatelatlong"] != null)
        {
            Updatelatlong();
        }
        BindPartners(); 
    }
    private void BindPartners()
    {
        UsersBAL objUsersBAL = new UsersBAL();
        DataSet ds = new DataSet();
        ds = objUsersBAL.BusinessUsersDropdown();
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlBusiness.DataSource = ds.Tables[0];
            ddlBusiness.DataTextField = "Name";
            ddlBusiness.DataValueField = "ID";
            ddlBusiness.DataBind();
        }

        ddlBusiness.Items.Insert(0, new ListItem("-- Select Vendor --", "0"));


        BusinessBAL objBusinessBAL = new BusinessBAL();
        int CurrentPage = 1;
        int intTotalRecord = 0;
        DataTable dtTable = new DataTable();
        dtTable = objBusinessBAL.GetList(ref CurrentPage, 100000, out intTotalRecord, "Business.ID", "DESC");
        if (dtTable.Rows.Count > 0)
        {
            rptRecord.DataSource = dtTable;
            rptRecord.DataBind();
        }

    }

    private void Updatelatlong()
    {
        Save(Convert.ToInt64(Request["id"]), Convert.ToString(Request["lat"]), Convert.ToString(Request["long"]));
    }

    public long Save(long BID, string Lat, string Long)
    {
        DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.Int, 20, BID),                 
                new DbParameter("@Lat", DbParameter.DbType.VarChar, 4000, Lat), 
                new DbParameter("@Long", DbParameter.DbType.VarChar, 4000,Long)              
            };
        DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "Updatelatlong", dbParam);
        return 1;
    }
}