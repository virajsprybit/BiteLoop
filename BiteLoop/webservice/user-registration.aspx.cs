using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using Utility;
using System.IO;
using System.Threading;
using System.Globalization;
using System.Net;
using System.Data;
using System.Net.Mail;
using Newtonsoft.Json;
using BiteLoop.Common;
using System.Data.SqlClient;

public partial class webservice_user_registration : System.Web.UI.Page
{
    UsersBAL objUsersBAL = new UsersBAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Keys.Count > 0)
        {
            string validation = ValidateRegistration();
            if (validation == string.Empty)
            {
                SaveInfo();
            }
            else
            {
                WriteResponse(false, validation, null);
            }
        }
    }

    private string ValidateRegistration()
    {
        string err = string.Empty;

        if (Request["VersionNumber"] == null) err += "VersionNumber, ";
        if (Request["DeviceKey"] == null) err += "DeviceKey, ";
        if (Request["Name"] == null) err += "Name, ";
        if (Request["Email"] == null) err += "Email, ";
        if (Request["Mobile"] == null) err += "Mobile, ";
        if (Request["PostCode"] == null) err += "PostCode, ";
        if (Request["FirebaseKey"] == null) err += "FirebaseKey, ";

        int loginType;
        if (Request["IsFacebookGoogleApple"] == null ||
            !int.TryParse(Request["IsFacebookGoogleApple"], out loginType) ||
            loginType < 0 || loginType > 3)
        {
            return "Invalid IsFacebookGoogleApple value.";
        }

        if (loginType == 0)
        {
            if (Request["Password"] == null) err += "Password, ";
            if (Request["OTP"] == null) err += "OTP, ";
        }

        if (loginType == 1 || loginType == 2 || loginType == 3)
        {
            if (Request["SocialMediaKey"] == null) err += "SocialMediaKey, ";
        }

        if (!string.IsNullOrEmpty(err))
            return "Please provide " + err.Trim().TrimEnd(',') + ".";

        return string.Empty;
    }

    private void SaveInfo()
    {
        UserAPIBAL objUserAPIBAL = new UserAPIBAL();

        string email = Convert.ToString(Request["Email"]).Trim();
        string mobile = Convert.ToString(Request["Mobile"]).Trim().Replace(" ", "");
        string otp = Convert.ToString(Request["OTP"] ?? "").Trim();

        bool emailExists, mobileExists;
        objUserAPIBAL.CheckEmailMobileExist(email, mobile, out emailExists, out mobileExists);

        if (emailExists || mobileExists)
        {
            WriteResponse(false, "Email or Mobile number already exists.", null);
            return;
        }

        objUserAPIBAL.ID = 0;
        objUserAPIBAL.Name = Convert.ToString(Request["Name"]).Trim();
        objUserAPIBAL.LastName = Convert.ToString(Request["LastName"]).Trim();
        //objUserAPIBAL.State = Convert.ToString(Request["State"]).Trim();
        objUserAPIBAL.Email = email;
        objUserAPIBAL.Mobile = mobile;
        objUserAPIBAL.Password = Convert.ToString(Request["Password"] ?? "");
        objUserAPIBAL.VersionNumber = Convert.ToString(Request["VersionNumber"]).Trim();
        objUserAPIBAL.DeviceKey = Convert.ToString(Request["DeviceKey"]).Trim();
        objUserAPIBAL.DeviceType = Convert.ToString(Request["DeviceType"] ?? "").Trim();
        objUserAPIBAL.PostCode = Convert.ToString(Request["PostCode"]).Trim();
        objUserAPIBAL.FirebaseKey = Convert.ToString(Request["FirebaseKey"]).Trim();
        objUserAPIBAL.OTP = otp;

        int isSocial = Convert.ToInt32(Request["IsFacebookGoogleApple"]);
        objUserAPIBAL.IsFacebookGoogleApple = isSocial;
        objUserAPIBAL.SocialMediaKey = Convert.ToString(Request["SocialMediaKey"] ?? "");

        objUserAPIBAL.AuthToken = Guid.NewGuid().ToString().ToLower();

        long result = objUserAPIBAL.Save();

        if (result > 0)
        {
            objUserAPIBAL.ID = Convert.ToInt32(result);

            objUserAPIBAL.SecretKey =
                Utility.Security.Rijndael128Algorithm.EncryptString(objUserAPIBAL.ID.ToString());

            if (isSocial == 0)
                SendUserMail(objUserAPIBAL.Email, objUserAPIBAL.Name, objUserAPIBAL.Password);

            WriteResponse(true, "Your registration has been completed successfully.", objUserAPIBAL);
        }
        else if (result == -1)
        {
            WriteResponse(false, "Duplicate Email or Social Media Key found.", null);
        }
        else if (result == -2)
        {
            WriteResponse(false, "Invalid OTP.", null);
        }
        else
        {
            WriteResponse(false, "Something went wrong while saving user.", null);
        }
    }

    private void SendUserMail(string EmailAddress, string Name, string Password)
    {
        EmailTemplateBAL objEmail = new EmailTemplateBAL();
        DataTable dt = objEmail.GetByID(2, 1);
        if (dt.Rows.Count == 0) return;

        string subject = Convert.ToString(dt.Rows[0]["Subject"]);
        string body = Convert.ToString(dt.Rows[0]["Template"]);

        body = body.Replace("###Name###", Name)
                   .Replace("###Email###", EmailAddress)
                   .Replace("###Password###", Password)
                   .Replace("###siteurl###", Config.WebSiteUrl)
                   .Replace("###SiteName###", Config.WebsiteName);

        GeneralSettings.SendEmail(
            EmailAddress,
            new GeneralSettings().getConfigValue("infoemail"),
            subject.Replace("###WebsiteName###", Config.WebsiteName),
            body);
    }

    private void WriteResponse(bool success, string message, UserAPIBAL user)
    {
        int statusCode = success ? 200 : 400;

        object dataPayload = new object[] { };

        if (success && user != null)
        {
            dataPayload = new[]
            {
                new
                {
                    UserID = user.ID,
                    FullName = user.Name,
                    LastName = user.LastName ?? "",
                    EmailAddress = user.Email,
                    Mobile = user.Mobile,
                    AuthToken = user.AuthToken ?? "",
                    SecretKey = user.SecretKey ?? "",
                    FirebaseKey = user.FirebaseKey ?? "",
                    SocialMediaKey = user.SocialMediaKey ?? "",
                    IsFacebookGoogleApple = user.IsFacebookGoogleApple,
                    ProfilePhoto = user.ProfilePhoto ?? "",
                    PostCode = user.PostCode ?? ""
                }
            };
        }

        var response = new
        {
            success = success ? "true" : "false",
            message = message,
            StatusCode = statusCode.ToString(),
            data = dataPayload
        };

        string json = JsonConvert.SerializeObject(response,
            new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

        HttpContext.Current.Response.ContentType = "application/json";
        HttpContext.Current.Response.Write(json);
        HttpContext.Current.Response.End();
    }
}