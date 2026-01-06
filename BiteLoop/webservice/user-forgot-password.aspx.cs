using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BAL;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Net;
using System.Threading;
using System.Configuration;
using System.Data.SqlClient;
using Utility;

public partial class webservice_user_mobile_Verification : System.Web.UI.Page
{
    #region Parameters    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Keys.Count > 0)
        {
            UserForgotPassword();
        }
    }
    #endregion

    #region Send OTP Instead Of Password

    private void UserForgotPassword()
    {
        Response objResponse = new Response();

        DataTable dtEmailDetails = new DataTable();
        dtEmailDetails.Columns.Add("Email");
        dtEmailDetails.Columns.Add("OTP");
        DataRow dr;

        DataTable dt = new DataTable();
        UsersBAL objUsersBAL = new UsersBAL();

        string EmailAddress = "";
        if (Request["EmailAddress"] != null)
        {
            EmailAddress = Convert.ToString(Request["EmailAddress"]).Trim();
        }

        objUsersBAL.Email = EmailAddress;
        dt = objUsersBAL.UserForgotPassword();

        if (dt.Rows.Count > 0)
        {
            string existingPassword = Convert.ToString(dt.Rows[0]["Password"]).Trim();

            // If password is empty -> user registered through social login
            if (string.IsNullOrEmpty(existingPassword))
            {
                objResponse.success = "false";
                objResponse.message = "This email is registered using Social Media. Password reset is not allowed.";
                HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
                Response.End();
                return;
            }

            // Generate 6-digit OTP
            string OTP = new Random().Next(100000, 999999).ToString();

            // Save OTP in OTP_Table
            bool saved = SaveOTP(EmailAddress, "", OTP);

            if (saved)
            {
                // Create row for email sending
                dr = dtEmailDetails.NewRow();
                dr["Email"] = EmailAddress;
                dr["OTP"] = OTP;
                dtEmailDetails.Rows.Add(dr);

                Thread emailThread = new Thread(() => SendUserEmail(dtEmailDetails));
                emailThread.IsBackground = true;
                emailThread.Start();

                objResponse.success = "true";
                objResponse.message = "OTP has been sent to your registered Email Address.";
                objResponse.OTP = OTP;
            }
            else
            {
                objResponse.success = "false";
                objResponse.message = "Unable to generate OTP right now. Please try again later.";
            }
        }
        else
        {
            objResponse.success = "false";
            objResponse.message = "This Email Address is not registered with us.";
        }


        //if (dt.Rows.Count > 0)
        //{
        //    string OTP = new Random().Next(100000, 999999).ToString();

        //    bool saved = SaveOTP(EmailAddress, "", OTP);

        //    if (saved)
        //    {
        //        dr = dtEmailDetails.NewRow();
        //        dr["Email"] = EmailAddress;
        //        dr["OTP"] = OTP;
        //        dtEmailDetails.Rows.Add(dr);

        //        Thread emailThread = new Thread(() => SendUserEmail(dtEmailDetails));
        //        emailThread.IsBackground = true;
        //        emailThread.Start();

        //        objResponse.success = "true";
        //        objResponse.message = "OTP has been sent to your registered Email Address.";
        //        objResponse.OTP = OTP;
        //    }
        //    else
        //    {
        //        objResponse.success = "false";
        //        objResponse.message = "Unable to generate OTP right now. Please try again later.";
        //    }
        //}
        //else
        //{
        //    objResponse.success = "false";
        //    objResponse.message = "This Email Address is not registered with us.";
        //}

        HttpContext.Current.Response.Write(JsonConvert.SerializeObject(
            objResponse,
            new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }
        ));
        Response.End();
    }

    #endregion

    #region Send Email With OTP
    private void SendUserEmail(DataTable dtData)
    {
        if (dtData.Rows.Count > 0)
        {
            SendEmail(
                Convert.ToString(dtData.Rows[0]["Email"]),
                Convert.ToString(dtData.Rows[0]["OTP"])
            );
        }
    }

    private void SendEmail(string EmailAddress, string OTP)
    {
        string strHeader = "<!doctype html><html><head><meta charset='utf-8'><title>Forgot Password OTP</title><link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet'><style>body { padding: 0px; margin: 0px;}</style></head><body>";
        string strFooter = "</body></html>";

        EmailTemplateBAL objEmail = new EmailTemplateBAL();
        DataTable dt = objEmail.GetByID(3, 1); 

        if (dt.Rows.Count > 0)
        {
            string strSubject = Convert.ToString(dt.Rows[0]["Subject"]);
            System.Text.StringBuilder sbEmailTemplate =
                new System.Text.StringBuilder(strHeader + Convert.ToString(dt.Rows[0]["Template"]) + strFooter);

            // Reuse ###Password### placeholder for OTP
            sbEmailTemplate.Replace("###Password###", OTP);
            sbEmailTemplate.Replace("###siteurl###", Config.WebSiteUrl);
            sbEmailTemplate.Replace("###SiteName###", Config.WebsiteName);

            if (!string.IsNullOrEmpty(EmailAddress))
            {
                ProcessEmail(
                    EmailAddress,
                    new GeneralSettings().getConfigValue("infoemail"),
                    strSubject.Replace("###WebsiteName###", Config.WebsiteName),
                    sbEmailTemplate.ToString()
                );
            }
        }
    }

    private bool ProcessEmail(string strTo, string strFrom, string strSubject, string strBody)
    {
        bool flag = false;
        try
        {
            MailMessage message = new MailMessage(strFrom, strTo);
            SmtpClient client = new SmtpClient();
            message.IsBodyHtml = true;
            message.Body = strBody;
            message.Subject = strSubject;
            message.Priority = MailPriority.Normal;

            client.Port = 587;
            client.Host = new GeneralSettings().getConfigValue("hostname");
            client.Credentials = new NetworkCredential(
                new GeneralSettings().getConfigValue("emailaddress"),
                new GeneralSettings().getConfigValue("emailpassword")
            );

            try
            {
                client.EnableSsl = Convert.ToBoolean(new GeneralSettings().getConfigValue("SSL"));
            }
            catch
            {
                client.EnableSsl = true;
            }

            client.Send(message);
            flag = true;
        }
        catch { }
        return flag;
    }
    #endregion

    #region Save OTP in OTP_Table
    private bool SaveOTP(string email, string mobile, string otp)
    {
        try
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("ForgotPasswordOTP", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Email", (object)email ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Mobile", (object)mobile ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@OTP", otp);

                    cmd.ExecuteNonQuery();
                }
            }

            return true;
        }
        catch
        {
            return false;
        }
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
//using System.Threading;


//public partial class webservice_user_mobile_Verification : System.Web.UI.Page
//{
//    #region Parameters    
//    protected void Page_Load(object sender, EventArgs e)
//    {
//        if (Request.Form.Keys.Count > 0)
//        {
//            UserForgotPassword();
//        }
//    }
//    #endregion

//    #region Send Mobile Code

//    private void UserForgotPassword()
//    {
//        Response objResponse = new Response();

//        DataTable dtEmailDetails = new DataTable();
//        dtEmailDetails.Columns.Add("Email");
//        dtEmailDetails.Columns.Add("Password");
//        DataRow dr;


//        DataTable dt = new DataTable();
//        UsersBAL objUsersBAL = new UsersBAL();
//        string EmailAddress = "";
//        if (Request["EmailAddress"] != null)
//        {
//            EmailAddress = Convert.ToString(Request["EmailAddress"]).Trim();
//        }
//        objUsersBAL.Email = EmailAddress;
//        dt = objUsersBAL.UserForgotPassword();


//        string EmailAndPassword = "";
//        if (dt.Rows.Count > 0)
//        {
//            if (Convert.ToString(dt.Rows[0]["Password"]) != string.Empty)
//            {                
//                EmailAndPassword = EmailAddress + "###" + Convert.ToString(dt.Rows[0]["Password"]);

//                dr = dtEmailDetails.NewRow();
//                dr["Email"] = EmailAddress;
//                dr["Password"] = Convert.ToString(dt.Rows[0]["Password"]);
//                dtEmailDetails.Rows.Add(dr);


//                Thread emailThread = new Thread(() => SendUserEmail(dtEmailDetails));
//                 emailThread.IsBackground = true;
//                emailThread.Start();


//                //System.Threading.Thread emailThread = new System.Threading.Thread(new ThreadStart(SendUserEmail(dtEmailDetails));

//                ////System.Threading.Thread emailThread = new System.Threading.Thread(SendUserEmail(dtEmailDetails));
//                //emailThread.IsBackground = true;
//                //emailThread.Start();                
//                //SendEmail(EmailAddress, Convert.ToString(dt.Rows[0]["Password"]));
//                objResponse.success = "true";
//                objResponse.message = "Password has been sent to your registered Email Address.";
//            }
//            else
//            {
//                objResponse.success = "false";
//                objResponse.message = "If you are a valid registered user with Bring Me Home, you should get an email in your inbox soon.";
//            }

//        }
//        else
//        {
//            objResponse.success = "false";
//            objResponse.message = "This Email Address is not registered with us.";
//        }

//        HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
//        Response.End();

//    }

//    #endregion
//    private void SendUserEmail(DataTable EmailAndPassword)
//    {
//        if (EmailAndPassword.Rows.Count > 0)
//        {
//            SendEmail(Convert.ToString(EmailAndPassword.Rows[0]["Email"]), Convert.ToString(EmailAndPassword.Rows[0]["Password"]));
//        }

//       // SendEmail(GlobalEmailAddress, GlobalPassword);
//    }
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

//            sbEmailTemplate.Replace("###Password###", Convert.ToString(Password));
//            sbEmailTemplate.Replace("###siteurl###", Config.WebSiteUrl);
//            sbEmailTemplate.Replace("###SiteName###", Config.WebsiteName);
//            if (EmailAddress != string.Empty)
//            {
//               // GeneralSettings.SendEmail(EmailAddress, new GeneralSettings().getConfigValue("infoemail"), strSubject.Replace("###WebsiteName###", Config.WebsiteName), sbEmailTemplate.ToString());
//                ProcessEmail(EmailAddress, new GeneralSettings().getConfigValue("infoemail"), strSubject.Replace("###WebsiteName###", Config.WebsiteName), sbEmailTemplate.ToString());
//            }
//        }
//    }

//    private bool ProcessEmail(string strTo, string strFrom, string strSubject, string strBody)
//    {
//        bool flag = false;
//        try
//        {
//            MailMessage message = new MailMessage(strFrom, strTo);
//            SmtpClient client = new SmtpClient();
//            message.IsBodyHtml = true;
//            message.Body = strBody;
//            message.Subject = strSubject;
//            message.Priority = MailPriority.Normal;
//            //client.Host = Config.SMTPServer;
//            client.Port = 587;
//            client.Host = new GeneralSettings().getConfigValue("hostname");
//            client.Credentials = new NetworkCredential(new GeneralSettings().getConfigValue("emailaddress"), new GeneralSettings().getConfigValue("emailpassword"));
//            try
//            {
//                client.EnableSsl = Convert.ToBoolean(new GeneralSettings().getConfigValue("SSL"));
//            }
//            catch (Exception ex)
//            {
//                client.EnableSsl = true;
//            }

//            client.Send(message);
//            flag = true;            
//        }
//        catch (Exception ex)
//        {
//            //Response.Write(ex.Message.ToString() + "================> " + ex.StackTrace.ToString());
//        }
//        return flag;
//    }

//}