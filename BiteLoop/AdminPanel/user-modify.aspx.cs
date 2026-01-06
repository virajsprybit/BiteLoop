using BAL;
using BiteLoop.Common;
using DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

public partial class AdminPanel_vendor_Modify : AdminAuthentication
{
    protected string txtUserNameValue = "";
    protected string txtPasswordValue = "";
    protected string txtEmailValue = "";
    protected string txtPostCodeValue = "";
    protected string txtMobileValue = "";
    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.HttpMethod == "POST" &&
        Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        {
            int id = int.Parse(Request.Form["hdnUserID"]);
            string username = Request.Form["txtUserName"];
            string password = Request.Form["txtPassword"];
            string email = Request.Form["txtEmail"];
            string postcode = Request.Form["txtPostCode"];
            string mobile = Request.Form["txtMobile"];

            SaveUser(id, username, password, email, postcode, mobile);

            Response.Clear();
            Response.ContentType = "text/plain";
            Response.Write("success");
            Response.End();
            return;
        }

        if (!IsPostBack)
        {
            string idStr = Request.QueryString["id"];
            if (!string.IsNullOrEmpty(idStr))
            {
                int id;
                if (int.TryParse(idStr, out id))
                {
                    UserDetailsByID(id);
                }
                else
                {
                    Response.Redirect("user-list.aspx");
                }
            }
        }
    }
    #endregion

    #region Bind Controls

    //private void BindDropdownsAndControls()
    //{
    //    DataSet ds = new DataSet();
    //    BusinessBAL objBusinessBAl = new BusinessBAL();
    //    ds = objBusinessBAl.BusinessDropdownRegistration();
    //    if (ds.Tables.Count > 0)
    //    {

    //    }
    //}
    //private void BindState()
    //{
    //    DataTable dt = new DataTable();
    //    CommonBAL objCommonBAL = new CommonBAL();
    //    dt = objCommonBAL.StateList();
    //    if (dt != null && dt.Rows.Count > 0)
    //    {
    //    }
    //}

    //private void BindControls()
    //{
    //    SalesAdminBAL objSalesAdminBAL = new SalesAdminBAL();
    //    objSalesAdminBAL.ID = ID;
    //    DataSet ds = objSalesAdminBAL.BusinessDetailsByID();

    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        StringBuilder sbTemplate = new StringBuilder(Convert.ToString(ds.Tables[0].Rows[0]["Description"]));
    //        sbTemplate.Replace("{%WebSiteUrl%}", Config.WebSiteUrl);


    //        // IMPORTANT: All schedule-binding code REMOVED (Option A)
    //        // Tables 3, 5, 6 are ignored
    //    }
    //}
    #endregion

    #region Save Information

    //private bool EmailExists(string email, string id)
    //{
    //    bool exists = false;
    //    string connStr = ConfigurationManager.ConnectionStrings["YourConnectionString"].ConnectionString;

    //    using (SqlConnection con = new SqlConnection(connStr))
    //    {
    //        using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Email=@Email AND ID<>@ID", con))
    //        {
    //            cmd.Parameters.AddWithValue("@Email", email);
    //            cmd.Parameters.AddWithValue("@ID", string.IsNullOrEmpty(id) ? 0 : Convert.ToInt32(id));
    //            con.Open();
    //            int count = (int)cmd.ExecuteScalar();
    //            if (count > 0) exists = true;
    //        }
    //    }
    //    return exists;
    //}

    private void SaveUser(int id, string username, string password, string email, string postcode, string mobile)
    {
        DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@ID", DbParameter.DbType.Int, 10, id),
                new DbParameter("@UserName", DbParameter.DbType.VarChar, 100, username),
                new DbParameter("@Password", DbParameter.DbType.VarChar, 100, password),
                new DbParameter("@Email", DbParameter.DbType.VarChar, 100, email),
                new DbParameter("@Postcode", DbParameter.DbType.VarChar, 10, postcode),
                new DbParameter("@Mobile", DbParameter.DbType.VarChar, 100, mobile),
            };
        DataSet ds = DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "UserModify", dbParam);
    }

    public void UserDetailsByID(int ID)
    {
        DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@ID", DbParameter.DbType.Int, 10, ID)
            };
        DataSet ds = DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "UserDetailsByID", dbParam);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].Rows[0];
            txtUserNameValue = dr["Name"] != DBNull.Value ? dr["Name"].ToString() : "";
            txtPasswordValue = dr["Password"] != DBNull.Value ? dr["Password"].ToString() : "";
            txtEmailValue = dr["Email"] != DBNull.Value ? dr["Email"].ToString() : "";
            txtPostCodeValue = dr["PostCode"] != DBNull.Value ? dr["PostCode"].ToString() : "";
            txtMobileValue = dr["Mobile"] != DBNull.Value ? dr["Mobile"].ToString() : "";
        }
    }
    #endregion
}