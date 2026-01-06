using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using Utility;
using System.Data;
using Newtonsoft.Json;
using DAL;

public partial class webservice_user_registration_Apple : System.Web.UI.Page
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
            string strName = string.Empty;
            string strAppleID = string.Empty;
            string strMobile = string.Empty;
            string strDeviceKey = string.Empty;
            string strLastName = string.Empty;
            int StateID = 0;
 


            if (Request["Email"] != null)
            {
                strEmail = Convert.ToString(Request["Email"]);
            }
            if (Request["FirstName"] != null)
            {
                strName = Convert.ToString(Request["FirstName"]);
            }
            if (Request["LastName"] != null)
            {
                strLastName = Convert.ToString(Request["LastName"]);
            }
            if (Request["AppleID"] != null)
            {
                strAppleID = Convert.ToString(Request["AppleID"]);
            }
            if (Request["Mobile"] != null)
            {
                strMobile = Convert.ToString(Request["Mobile"]);
            }
            if (Request["DeviceKey"] != null)
            {
                strDeviceKey = Convert.ToString(Request["DeviceKey"]);
            }
            if (Request["StateID"] != null)
            {
                StateID = Convert.ToInt32(Request["StateID"]);
            }

            GetUserSignUp(strEmail, strName, strAppleID, strMobile, strDeviceKey, strLastName, StateID);
        }
    }
    #endregion

    #region SignUp
    private void GetUserSignUp(string strEmail, string strName, string strAppleID, string strMobile, string strDeviceKey, string strLastName, int StateID)
    {
        string strResponse = string.Empty;

        if (strEmail.Trim() != string.Empty && strAppleID != string.Empty)
        {
            Int16 intResult;

            DataTable dt = User_AppleSignUpCheck(out intResult, strEmail, strName, strAppleID, strMobile, strDeviceKey, strLastName, StateID);
            if (intResult == 0)
            {
                ResponseMessage("Signup completed successfully.", 1);

            }
            else if (intResult == 1)
            {
                ResponseMessage("Invalid Apple ID.", 0);
            }
            else if (intResult == 3)
            {
                ResponseMessage("Apple ID already exists. Please login with Apple.", 0);
            }
            else if (intResult == 4)
            {
                ResponseMessage("This user has been deactivated by the Administrator. Please contact administrator.", 0);
                // Deacivated User
            }           
        }
        HttpContext.Current.Response.End();
        //  return strResponse;
    }
    private DataTable User_AppleSignUpCheck(out Int16 intResult, string strEmail, string strName, string strAppleID, string strMobile, string strDeviceKey, string strLastName, int StateID)
    {
        DbParameter[] dbParam = new DbParameter[9];
        dbParam[0] = new DbParameter("@Name", DbParameter.DbType.VarChar, 500, strName);
        dbParam[1] = new DbParameter("@Mobile", DbParameter.DbType.VarChar, 500, strMobile);
        dbParam[2] = new DbParameter("@IsError", DbParameter.DbType.SmallInt, 2, ParameterDirection.Output);
        dbParam[3] = new DbParameter("@Email", DbParameter.DbType.VarChar, 1000, strEmail);
        dbParam[4] = new DbParameter("@DeviceKey", DbParameter.DbType.VarChar, 4000, strDeviceKey);
        dbParam[5] = new DbParameter("@DeviceType", DbParameter.DbType.VarChar, 4000, "A");
        dbParam[6] = new DbParameter("@AppleID", DbParameter.DbType.VarChar, 4000, strAppleID);
        dbParam[7] = new DbParameter("@State", DbParameter.DbType.VarChar, 4000, StateID);
        dbParam[8] = new DbParameter("@LastName", DbParameter.DbType.VarChar, 500, strLastName);
        DataTable dtblUserData = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "UserAppleSignUp", dbParam);

        intResult = Convert.ToInt16(dbParam[2].Value);
        return dtblUserData;
    }
    #endregion


    public void ResponseMessage(string strMessage, int IsError)
    {
        Response objResponse = new Response();
        if (IsError == 1)
        {
            objResponse.success = "true";
        }
        else
        {
            objResponse.success = "false";
        }
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