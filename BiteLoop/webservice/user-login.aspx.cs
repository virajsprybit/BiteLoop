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
using System.Security.AccessControl;
using Microsoft.VisualBasic.ApplicationServices;


public partial class webservice_user_login : System.Web.UI.Page
{
    UsersBAL objUsersBAL = new UsersBAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        
        // ------------ Read Inputs -------------
        string email = Convert.ToString(Request["Email"] ?? "").Trim();
        string password = Convert.ToString(Request["Password"] ?? "").Trim();
        string socialKey = Convert.ToString(Request["SocialMediaKey"] ?? "").Trim();
        int IsFacebookGoogleApple = Convert.ToInt32(Request["IsFacebookGoogleApple"]);
        string deviceKey = Convert.ToString(Request["DeviceKey"] ?? "");
        string deviceType = Convert.ToString(Request["DeviceType"] ?? "A");
        string versionNumber = Convert.ToString(Request["VersionNumber"] ?? "");
        string firebaseKey = Convert.ToString(Request["FirebaseKey"] ?? "");
        string appType = Convert.ToString(Request["AppType"] ?? "");
                
        // Assign Parameters to BAL
        objUsersBAL.Email = email;
        objUsersBAL.Password = password;
        objUsersBAL.SocialMediaKey = socialKey;
        objUsersBAL.IsFacebookGoogleApple = IsFacebookGoogleApple;

        objUsersBAL.VersionNumber = versionNumber;
        objUsersBAL.FirebaseKey = firebaseKey;
        objUsersBAL.AppType = appType;

        // Call SQL
        int resultCode;
        DataTable dt = objUsersBAL.User_LoginCheck(out resultCode, deviceKey, deviceType);

        if (resultCode == 0 && dt.Rows.Count > 0)
        {
            SendLoginResponse(dt);
        }
        else if (resultCode == 1)
        {
            WriteError("Wrong Username or Password.");
        }
        else if (resultCode == 2)
        {
            WriteError("This user has been deactivated by the administrator.");
        }
        else if (resultCode == 4)
        {
            WriteError("Invalid login details.");
        }

        Response.End();
    }

    private void WriteError(string msg)
    {
        Response obj = new Response();
        obj.success = "false";
        obj.message = msg;

        HttpContext.Current.Response.Write(JsonConvert.SerializeObject(obj,
            new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
    }

    private void SendLoginResponse(DataTable dt)
    {
        Response objResponse = new Response();
        objResponse.success = "true";
        objResponse.message = "Login Successful.";

        UserLogin[] usr = new UserLogin[dt.Rows.Count];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            var r = dt.Rows[i];
            usr[i] = new UserLogin
            {
                UserID = Convert.ToInt64(r["ID"]),
                FullName = Convert.ToString(r["Name"]),
                LastName = Convert.ToString(r["LastName"]),
                EmailAddress = Convert.ToString(r["Email"]),
                Mobile = Convert.ToString(r["Mobile"]),
                AuthToken = Convert.ToString(r["AuthToken"]),
                SecretKey = Utility.Security.Rijndael128Algorithm.EncryptString(Convert.ToString(r["ID"])),
                //LastName = Convert.ToString(r["LastName"]),
                //StateName = Convert.ToString(r["StateName"]),
                //StateID = string.IsNullOrEmpty(Convert.ToString(r["StateID"])) ? 0 : Convert.ToInt32(r["StateID"]),
                IsSignupByFacebook = Convert.ToInt32(r["IsFacebookGoogleApple"]),
                ProfilePhoto = string.IsNullOrEmpty(Convert.ToString(r["ProfilePhoto"]))
                  ? ""
                  : Config.WebSiteUrl + "source/CMSFiles/" + Convert.ToString(r["ProfilePhoto"]),
                PostCode = Convert.ToString(r["PostCode"])
            };
        }

        objResponse.UserLogin = usr;

        string json = JsonConvert.SerializeObject(objResponse,
            new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

        json = json.Replace("\"UserLogin\"", "\"data\"");

        HttpContext.Current.Response.Write(json);
    }
}

//public partial class webservice_user_login : System.Web.UI.Page
//{
//    #region Parameters
//    UsersBAL objUsersBAL = new UsersBAL();
//    #endregion

//    #region Page Events
//    protected void Page_Load(object sender, EventArgs e)
//    {
//        if (Request.Form.Keys.Count > 0)
//        {
//            string strLogin = string.Empty;
//            string strPassword = string.Empty;
//            string FacebookID = string.Empty;

//            string VersionNumber = string.Empty;
//            string AppType = string.Empty;
//            string FirebaseKey = string.Empty;
//            int? IsFacebookGoogleApple = null; 
//            string SocialMediaKey = string.Empty;

//            if (Request["Email"] != null)
//            {
//                strLogin = Convert.ToString(Request["Email"]);
//            }

//            if (Request["Password"] != null)
//            {
//                strPassword = Convert.ToString(Request["Password"]);
//            }

//            if (Request["FacebookID"] != null)
//            {
//                FacebookID = Convert.ToString(Request["FacebookID"]);
//            }

//            if (Request["VersionNumber"] != null)
//                VersionNumber = Convert.ToString(Request["VersionNumber"]);

//            if (Request["AppType"] != null)
//                AppType = Convert.ToString(Request["AppType"]);

//            if (Request["FirebaseKey"] != null)
//                FirebaseKey = Convert.ToString(Request["FirebaseKey"]);

//            if (!string.IsNullOrEmpty(Request["IsFacebookGoogleApple"]))
//            {
//                int temp;
//                if (int.TryParse(Request["IsFacebookGoogleApple"], out temp))
//                {
//                    IsFacebookGoogleApple = temp;
//                }
//            }

//            if (Request["SocialMediaKey"] != null)
//                SocialMediaKey = Convert.ToString(Request["SocialMediaKey"]);

//            GetUserLogin(strLogin, strPassword, FacebookID, VersionNumber, AppType, FirebaseKey, IsFacebookGoogleApple, SocialMediaKey);
//        }
//    }
//    #endregion

//    #region Login

//    private void WriteLogFile(string LogMessage)
//    {
//        try
//        {
//            string path = System.Web.Hosting.HostingEnvironment.MapPath("~/source") + "\\LogFileLogin.txt";
//            if (!System.IO.File.Exists(path))
//            {
//                using (System.IO.StreamWriter sw = System.IO.File.CreateText(path))
//                {
//                    sw.WriteLine(LogMessage);
//                }
//            }
//            else
//            {
//                using (System.IO.StreamWriter sw = System.IO.File.AppendText(path))
//                {
//                    sw.WriteLine(LogMessage);
//                }
//            }
//        }
//        catch (Exception ex)
//        {
//            // Log error if needed
//        }
//    }

//    private void GetUserLogin(
//        string strLogin,
//        string strPassword,
//        string FacebookID,
//        string VersionNumber,
//        string AppType,
//        string FirebaseKey,
//        int? IsFacebookGoogleApple,  
//        string SocialMediaKey)
//    {
//        if ((strLogin.Trim() != string.Empty && strPassword.Trim() != string.Empty) || FacebookID != string.Empty)
//        {
//            objUsersBAL.Email = strLogin;
//            objUsersBAL.Password = strPassword;
//            //objUsersBAL.FacebookID = FacebookID;
//            objUsersBAL.VersionNumber = VersionNumber;
//            //objUsersBAL.AppType = AppType;
//            objUsersBAL.FirebaseKey = FirebaseKey;
//            objUsersBAL.IsFacebookGoogleApple = IsFacebookGoogleApple; 
//            objUsersBAL.SocialMediaKey = SocialMediaKey;

//            string strDeviceKey = Request["DeviceKey"] != null ? Request["DeviceKey"].ToString() : "";
//            string strDeviceType = Request["DeviceType"] != null ? Request["DeviceType"].ToString() : "A";

//            Int16 intResult;
//            DataTable dt = objUsersBAL.User_LoginCheck(out intResult, strDeviceKey, strDeviceType);

//            if (intResult == 0)
//            {
//                if (dt.Rows.Count > 0)
//                {
//                    GetUserDetailsAfterLogin(dt);
//                }
//            }
//            else if (intResult == 1)
//            {
//                ResponseMessage("Wrong Username or Password.", 0);
//            }
//            else if (intResult == 2)
//            {
//                ResponseMessage("This user has been deactivated by the Administrator. Please contact administrator.", 0);
//            }
//            else if (intResult == 4)
//            {
//                ResponseMessage("Invalid login details.", 0);
//            }
//        }
//        HttpContext.Current.Response.End();
//    }
//    #endregion

//    public void ResponseMessage(string strMessage, int IsError)
//    {
//        Response objResponse = new Response();
//        objResponse.success = (IsError == 1) ? "true" : "false";
//        objResponse.message = strMessage;
//        HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
//    }

//    public void GetUserDetailsAfterLogin(DataTable dt)
//    {
//        if (dt.Rows.Count > 0)
//        {
//            Response objResponse = new Response();
//            objResponse.success = "true";
//            objResponse.message = "Login Successfull.";

//            UserLogin[] objUserLogin = new UserLogin[dt.Rows.Count];
//            for (int i = 0; i < dt.Rows.Count; i++)
//            {
//                var row = dt.Rows[i];
//                objUserLogin[i] = new UserLogin
//                {
//                    UserID = Convert.ToInt64(row["ID"]),
//                    FullName = Convert.ToString(row["Name"]),
//                    EmailAddress = Convert.ToString(row["Email"]),
//                    Mobile = Convert.ToString(row["Mobile"]),
//                    AuthToken = Convert.ToString(row["AuthToken"]),
//                    SecretKey = Utility.Security.Rijndael128Algorithm.EncryptString(Convert.ToString(row["ID"])),
//                    LastName = Convert.ToString(row["LastName"]),
//                    StateName = Convert.ToString(row["StateName"]),
//                    StateID = !string.IsNullOrEmpty(Convert.ToString(row["StateID"])) ? Convert.ToInt32(row["StateID"]) : 0,
//                    IsSignupByFacebook = row["IsFacebookGoogleApple"] != DBNull.Value ? Convert.ToInt32(row["IsFacebookGoogleApple"]) : 0
//                };
//            }

//            objResponse.UserLogin = objUserLogin;
//            string json = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
//            json = json.Replace("\"UserLogin\"", "\"data\"");
//            HttpContext.Current.Response.Write(json);
//        }
//        else
//        {
//            new CommonAPI().InvalidLogin();
//        }
//    }

//    public void NoRecordExists()
//    {
//        Response objResponse = new Response
//        {
//            success = "false",
//            message = "No records exists."
//        };
//        HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
//    }
//}