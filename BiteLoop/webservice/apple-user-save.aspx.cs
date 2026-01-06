using BAL;
using DAL;
using Newtonsoft.Json;
using System.Data;
using System.Web;
using System;

public partial class webservice_user_Apple_User : System.Web.UI.Page
{
    #region Parameters
    UsersBAL objUsersBAL = new UsersBAL();
    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Keys.Count > 0)
        {
            string strEmail = string.Empty;
            string strFullName = string.Empty;

            if (Request["Email"] != null)
            {
                strEmail = Convert.ToString(Request["Email"]);
            }
            if (Request["FullName"] != null)
            {
                strFullName = Convert.ToString(Request["FullName"]);
            }

            AppleUserSave(strEmail, strFullName);
        }
    }
    #endregion

    #region Login
    private void AppleUserSave(string strEmail, string strFullName)
    {
        User_AppleSave(strEmail, strFullName);
        ResponseMessage("Apple Details saved successfully.", 1);
        HttpContext.Current.Response.End();
    }

    private void User_AppleSave(string strEmail, string strFullName)
    {
        DbParameter[] dbParam = new DbParameter[2];
        dbParam[0] = new DbParameter("@FullName", DbParameter.DbType.VarChar, 200, strFullName);
        dbParam[1] = new DbParameter("@Email", DbParameter.DbType.VarChar, 1000, strEmail);
        DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "AppleUserDetailsSave", dbParam);
    }
    #endregion

    public void ResponseMessage(string strMessage, int IsError)
    {
        Response objResponse = new Response();
        objResponse.success = IsError == 1 ? "true" : "false";
        objResponse.message = strMessage;
        HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
    }

    public void NoRecordExists()
    {
        Response objResponse = new Response();
        objResponse.success = "false";
        objResponse.message = "No records exists.";
        HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
    }
}
