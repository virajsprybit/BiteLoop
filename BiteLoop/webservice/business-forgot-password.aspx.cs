using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using Utility;
using Newtonsoft.Json;
using System.Net;
using System.Data;
using System.Net.Mail;

public partial class webservice_business_forgotpassword : System.Web.UI.Page
{
    #region Parameters
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Keys.Count > 0)
        {
            BusinessForgotPassword();
        }
    }
    #endregion

    #region Forgot Password with OTP
    private void BusinessForgotPassword()
    {
        Response objResponse = new Response();
        DataTable dt = new DataTable();
        BusinessBAL objBusinessBAL = new BusinessBAL();

        string EmailAddress = "";
        if (Request["EmailAddress"] != null)
        {
            EmailAddress = Convert.ToString(Request["EmailAddress"]).Trim();
        }

        if (string.IsNullOrEmpty(EmailAddress))
        {
            objResponse.success = "false";
            objResponse.message = "Email Address is required.";
            WriteResponse(objResponse);
            return;
        }

        // Generate 6-digit OTP
        string OTP = new Random().Next(100000, 999999).ToString();

        objBusinessBAL.EmailAddress = EmailAddress;

        // Save OTP in Database
        dt = objBusinessBAL.BusinessForgotPasswordOTP(OTP);

        if (dt.Rows.Count > 0 && !string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["EmailAddress"])))
        {
            // Send OTP to Email (reuse existing SendEmail function)
            SendEmail(EmailAddress, OTP);

            objResponse.success = "true";
            objResponse.message = "OTP has been sent to your registered email address.";
            objResponse.OTP = OTP; // include OTP in API response
        }
        else
        {
            objResponse.success = "false";
            objResponse.message = "This Email Address is not registered with us.";
        }

        WriteResponse(objResponse);
    }
    #endregion

    #region Send Email (Existing Function)
    private void SendEmail(string EmailAddress, string OTP)
    {
        string strHeader = "<!doctype html><html><head><meta charset='utf-8'><title>Forgot Password OTP</title><link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet'><style>body {	padding: 0px;	margin: 0px;}</style></head><body>";
        string strFooter = "</body></html>";

        EmailTemplateBAL objEmail = new EmailTemplateBAL();
        DataTable dt = new DataTable();
        dt = objEmail.GetByID(3, 1); // existing template

        if (dt.Rows.Count > 0)
        {
            string strSubject = Convert.ToString(dt.Rows[0]["Subject"]);
            System.Text.StringBuilder sbEmailTemplate = new System.Text.StringBuilder(strHeader + Convert.ToString(dt.Rows[0]["Template"]) + strFooter);

            // replace ###Password### with OTP (reuse placeholder)
            sbEmailTemplate.Replace("###Password###", OTP);
            sbEmailTemplate.Replace("###siteurl###", Config.WebSiteUrl);
            sbEmailTemplate.Replace("###SiteName###", Config.WebsiteName);

            if (!string.IsNullOrEmpty(EmailAddress))
            {
                GeneralSettings.SendEmail(
                    EmailAddress,
                    new GeneralSettings().getConfigValue("abnno"),
                    strSubject.Replace("###WebsiteName###", Config.WebsiteName),
                    sbEmailTemplate.ToString()
                );
            }
        }
    }
    #endregion

    #region Helper
    private void WriteResponse(Response objResponse)
    {
        Response.StatusCode = (int)HttpStatusCode.OK;
        HttpContext.Current.Response.ContentType = "application/json";
        HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore
        }));
        Response.End();
    }
    #endregion
}




//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using BAL;
//using Utility;
//using Newtonsoft.Json;
//using System.Net;
//using System.Data;
//using System.Net.Mail;

//public partial class webservice_business_forgotpassword : System.Web.UI.Page
//{
//    #region Parameters
//    protected void Page_Load(object sender, EventArgs e)
//    {
//        if (Request.Form.Keys.Count > 0)
//        {
//            BusinessForgotPassword();
//        }
//    }
//    #endregion

//    #region Send Mobile Code

//    private void BusinessForgotPassword()
//    {
//        Response objResponse = new Response();
//        DataTable dt = new DataTable();
//        BusinessBAL objBusinessBAL = new BusinessBAL();

//        string EmailAddress = "";
//        if (Request["EmailAddress"] != null)
//        {
//            EmailAddress = Convert.ToString(Request["EmailAddress"]).Trim();
//        }

//        objBusinessBAL.EmailAddress = EmailAddress;
//        string strPass = Convert.ToString(Utility.Common.RandomString(7));
//        dt = objBusinessBAL.BusinessForgotPassword(Utility.Security.EncryptDescrypt.EncryptString(strPass));

//        if (dt.Rows.Count > 0)
//        {
//            SendEmail(EmailAddress, Convert.ToString(dt.Rows[0]["Password"]));
//            objResponse.success = "true";
//            objResponse.message = "Password has been sent to your registered Email Address.";
//        }
//        else
//        {
//            objResponse.success = "false";
//            objResponse.message = "This Email Address is not registered with us.";
//        }

//        Response.StatusCode = (int)HttpStatusCode.OK;

//        HttpContext.Current.Response.ContentType = "application/json";
//        HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings()
//        {
//            NullValueHandling = NullValueHandling.Ignore
//        }));
//        Response.End();
//    }


//    //    private void BusinessForgotPassword()
//    //{
//    //    Response objResponse = new Response();
//    //    DataTable dt = new DataTable();
//    //    BusinessBAL objBusinessBAL = new BusinessBAL();

//    //    string EmailAddress = "";
//    //    if (Request["EmailAddress"] != null)
//    //    {
//    //        EmailAddress = Convert.ToString(Request["EmailAddress"]).Trim();
//    //    }

//    //    objBusinessBAL.EmailAddress = EmailAddress;
//    //    string strPass = Convert.ToString(Utility.Common.RandomString(7));        
//    //    dt = objBusinessBAL.BusinessForgotPassword(Utility.Security.EncryptDescrypt.EncryptString(strPass));

//    //    if (dt.Rows.Count > 0)
//    //    {
//    //        SendEmail(EmailAddress, Convert.ToString(dt.Rows[0]["Password"]));
//    //        objResponse.success = "true";
//    //        objResponse.message = "Password has been sent to your registered Email Address.";

//    //        Response.StatusCode = (int)HttpStatusCode.OK;
//    //    }
//    //    else
//    //    {
//    //        objResponse.success = "false";
//    //        objResponse.message = "This Email Address is not registered with us.";

//    //        Response.StatusCode = (int)HttpStatusCode.BadRequest;
//    //    }

//    //    HttpContext.Current.Response.ContentType = "application/json";
//    //    HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
//    //    Response.End();
//    //}

//    //----------------------------------------------------------------------------------

//    //private void BusinessForgotPassword()
//    //{
//    //    Response objResponse = new Response();

//    //    DataTable dt = new DataTable();
//    //    BusinessBAL objBusinessBAL = new BusinessBAL();
//    //    string EmailAddress = "";
//    //    if (Request["EmailAddress"] != null)
//    //    {
//    //        EmailAddress = Convert.ToString(Request["EmailAddress"]).Trim();
//    //    }
//    //    objBusinessBAL.EmailAddress = EmailAddress;
//    //    string strPass = Convert.ToString(Utility.Common.RandomString(7));        
//    //    dt = objBusinessBAL.BusinessForgotPassword(Utility.Security.EncryptDescrypt.EncryptString(strPass));
//    //    if (dt.Rows.Count > 0)
//    //    {
//    //        SendEmail(EmailAddress, Convert.ToString(dt.Rows[0]["Password"]));
//    //        objResponse.success = "true";
//    //        objResponse.message = "Password has been sent to your registered Email Address.";

//    //    }
//    //    else
//    //    {
//    //        objResponse.success = "false";
//    //        objResponse.message = "This Email Address is not registered with us.";
//    //    }

//    //    HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
//    //    Response.End();


//    //}

//    #endregion

//    private void SendEmail(string EmailAddress, string Password)
//    {
//        string strHeader = "<!doctype html><html><head><meta charset='utf-8'><title>Forgot Password</title><link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet'><style>body {	padding: 0px;	margin: 0px;}</style></head><body>";
//        string strFooter = "</body></html>";

//        EmailTemplateBAL objEmail = new EmailTemplateBAL();
//        DataTable dt = new DataTable();
//        dt = objEmail.GetByID(3, 1);
//        if (dt.Rows.Count > 0)
//        {

//            string strSubject = string.Empty;
//            strSubject = Convert.ToString(dt.Rows[0]["Subject"]);
//            System.Text.StringBuilder sbEmailTemplate = new System.Text.StringBuilder(strHeader + Convert.ToString(dt.Rows[0]["Template"]) + strFooter);

//            sbEmailTemplate.Replace("###Password###", Utility.Security.EncryptDescrypt.DecryptString(Password));
//            sbEmailTemplate.Replace("###siteurl###", Config.WebSiteUrl);            
//            sbEmailTemplate.Replace("###SiteName###", Config.WebsiteName);
//            if (EmailAddress != string.Empty)
//            {
//                GeneralSettings.SendEmail(EmailAddress, new GeneralSettings().getConfigValue("abnno"), strSubject.Replace("###WebsiteName###", Config.WebsiteName), sbEmailTemplate.ToString());
//            }            
//        }
//    }


//}