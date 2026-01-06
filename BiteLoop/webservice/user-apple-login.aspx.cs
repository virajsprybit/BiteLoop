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

public partial class webservice_user_login_Apple : System.Web.UI.Page
{
    #region Parameters
    UsersBAL objUsersBAL = new UsersBAL();
    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Keys.Count > 0)
        {
            string strAppleID = string.Empty;            
            string strDeviceKey = string.Empty;
            
            if (Request["AppleID"] != null)
            {
                strAppleID = Convert.ToString(Request["AppleID"]);
            }
            if (Request["DeviceKey"] != null)
            {
                strDeviceKey = Convert.ToString(Request["DeviceKey"]);
            }
            
            GetUserLogin( strAppleID, strDeviceKey);
        }
    }
    #endregion

    #region Login
    private void GetUserLogin(string strAppleID,  string strDeviceKey)
    {
        string strResponse = string.Empty;

        if (strAppleID != string.Empty)
        {
            Int16 intResult;

            DataTable dt = User_AppleLoginCheck(out intResult,strAppleID,  strDeviceKey);
            if (intResult == 0)
            {
                if (dt.Rows.Count > 0)
                {
                    GetUserDetailsAfterLogin(dt);
                }

            }
            else if (intResult == 1)
            {
                ResponseMessage("Invalid Apple ID.", 0);
            }
            else if (intResult == 3)
            {
                ResponseMessage("User not found. Please signup with Apple.", 0);
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
    private DataTable User_AppleLoginCheck(out Int16 intResult, string strAppleID, string strDeviceKey)
    {
        DbParameter[] dbParam = new DbParameter[4];
        dbParam[0] = new DbParameter("@DeviceKey", DbParameter.DbType.VarChar, 4000, strDeviceKey);
        dbParam[1] = new DbParameter("@DeviceType", DbParameter.DbType.VarChar, 4000, "A");
        dbParam[2] = new DbParameter("@AppleID", DbParameter.DbType.VarChar, 4000, strAppleID);
        dbParam[3] = new DbParameter("@IsError", DbParameter.DbType.SmallInt, 2, ParameterDirection.Output);
        DataTable dtblUserData = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "UserAppleLogin", dbParam);

        intResult = Convert.ToInt16(dbParam[3].Value);
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



    public void GetUserDetailsAfterLogin(DataTable dt)
    {
        Response objResponse = new Response();
        if (dt.Rows.Count > 0)
        {
            objResponse.success = "true";
            objResponse.message = "Login Successfull.";

            UserLogin[] objUserLogin = new UserLogin[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                objUserLogin[i] = new UserLogin();
                objUserLogin[i].UserID = Convert.ToInt64(dt.Rows[i]["ID"]);
                objUserLogin[i].FullName = Convert.ToString(dt.Rows[i]["Name"]);
                objUserLogin[i].EmailAddress = Convert.ToString(dt.Rows[i]["Email"]);
                objUserLogin[i].Mobile = Convert.ToString(dt.Rows[i]["Mobile"]);
                objUserLogin[i].AuthToken = Convert.ToString(dt.Rows[i]["AuthToken"]);
                objUserLogin[i].SecretKey = Convert.ToString(Utility.Security.Rijndael128Algorithm.EncryptString(Convert.ToString(dt.Rows[i]["ID"])));

                objUserLogin[i].LastName = Convert.ToString(dt.Rows[i]["LastName"]);
                objUserLogin[i].StateName = Convert.ToString(dt.Rows[i]["StateName"]);
                if (Convert.ToString(dt.Rows[i]["StateID"]) != string.Empty)
                    objUserLogin[i].StateID = Convert.ToInt32(dt.Rows[i]["StateID"]);
                else
                    objUserLogin[i].StateID = 0;

                if (Convert.ToString(dt.Rows[i]["FacebookID"]) != string.Empty)
                    objUserLogin[i].IsSignupByFacebook = 1;
                else
                    objUserLogin[i].IsSignupByFacebook = 0;

                objResponse.UserLogin = objUserLogin;
            }
            string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            strResponseName = strResponseName.Replace("\"UserLogin\"", "\"data\"");
            HttpContext.Current.Response.Write(strResponseName);
            //HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
        }
        else
        {
            CommonAPI objCommonAPI = new CommonAPI();
            objCommonAPI.InvalidLogin();
        }
    }

    public void NoRecordExists()
    {
        Response objResponse = new Response();
        objResponse.success = "false";
        objResponse.message = "No records exists.";
        HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
    }
}