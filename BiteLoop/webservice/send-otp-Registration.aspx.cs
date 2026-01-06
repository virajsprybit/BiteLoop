using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Net;
using BAL;
using Utility;
using Newtonsoft.Json;

public partial class webservice_Send_OTP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.HttpMethod == "POST")
        {
            string email = Request.Form["Email"];
            string mobile = Request.Form["Mobile"];

            Response.ContentType = "application/json";

            if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(mobile))
            {
                WriteResponse(new
                {
                    success = "false",
                    message = "Email or Mobile is required.",
                    StatusCode = "400",
                    data = new object[] { }
                }, 400);
                return;
            }

            bool isDuplicate = CheckDuplicate(email, mobile);

            if (isDuplicate)
            {
                WriteResponse(new
                {
                    success = "false",
                    message = "Email or Mobile is Duplicate.",
                    StatusCode = "409",
                    data = new object[] { }
                }, 200);

                //WriteResponse(new
                //{
                //    success = "false",
                //    message = "Email or Mobile is Duplicate.",
                //    StatusCode = "409",
                //    data = new object[] { }
                //}, 409);
                return;
            }

            string OTP = new Random().Next(100000, 999999).ToString();

            SaveOTP(email, mobile, OTP);

            if (!string.IsNullOrEmpty(email))
            {
                SendEmail(email, OTP);
            }
           
            WriteResponse(new
            {
                success = "true",
                message = "OTP Sent.",
                StatusCode = "200",
                data = new[]
                {
                new { OTP = OTP }
            }
            }, 200);
        }
        else
        {
            Response.StatusCode = 405;
            Response.End();
        }
    }

    private bool CheckDuplicate(string email, string mobile)
    {
        bool exists = false;

        string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(connStr))
        {
            con.Open();

            string query = @"SELECT COUNT(*) FROM Users WHERE Email = @Email OR Mobile = @Mobile";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Email", email ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Mobile", mobile ?? (object)DBNull.Value);

                int count = (int)cmd.ExecuteScalar();
                exists = count > 0;
            }
        }
        return exists;
    }

    private void SaveOTP(string email, string mobile, string otp)
    {
        string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connStr))
        {
            con.Open();

            string query = @"
                INSERT INTO OTP_Table (Email, Mobile, OTP, CreatedDate)
                VALUES (@Email, @Mobile, @OTP, GETDATE())";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Email", (object)email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Mobile", (object)mobile ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@OTP", otp);

                cmd.ExecuteNonQuery();
            }
        }
    }

    private void SendEmail(string EmailAddress, string OTP)
    {
        string strHeader = "<!doctype html><html><head><meta charset='utf-8'><title>Registration OTP</title></head><body>";
        string strFooter = "</body></html>";

        EmailTemplateBAL objEmail = new EmailTemplateBAL();
        DataTable dt = objEmail.GetByID(3, 1); 

        if (dt.Rows.Count > 0)
        {
            string strSubject = Convert.ToString(dt.Rows[0]["Subject"]);
            System.Text.StringBuilder sbEmailTemplate =
                new System.Text.StringBuilder(strHeader + Convert.ToString(dt.Rows[0]["Template"]) + strFooter);

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

    private void WriteResponse(object obj, int statusCode = 200)
    {
        Response.StatusCode = statusCode;
        Response.Write(
            JsonConvert.SerializeObject(obj,
            new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })
        );
        Response.End();
    }
}

//using System;
//using System.Data;
//using System.Data.SqlClient;
//using System.Configuration;
//using System.Web.Script.Serialization;

//public partial class webservice_Send_OTP : System.Web.UI.Page
//{
//    protected void Page_Load(object sender, EventArgs e)
//    {
//        if (Request.HttpMethod == "POST")
//        {
//            string email = Request.Form["Email"];
//            string mobile = Request.Form["Mobile"];

//            Response.ContentType = "application/json";

//            if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(mobile))
//            {
//                WriteResponse(new { success = false, message = "Email or Mobile is required." });
//                return;
//            }

//            bool isDuplicate = CheckDuplicate(email, mobile);

//            if (isDuplicate)
//            {
//                WriteResponse(new { success = false, message = "Email or Mobile is Duplicate." });
//            }
//            else
//            {
//                WriteResponse(new { success = true, message = "OTP Sent." });
//            }
//        }
//        else
//        {
//            Response.StatusCode = 405; 
//            Response.End();
//        }
//    }

//    private bool CheckDuplicate(string email, string mobile)
//    {
//        bool exists = false;

//        string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
//        using (SqlConnection con = new SqlConnection(connStr))
//        {
//            con.Open();

//            string query = @"SELECT COUNT(*) FROM Users WHERE Email = @Email OR Mobile = @Mobile";

//            using (SqlCommand cmd = new SqlCommand(query, con))
//            {
//                cmd.Parameters.AddWithValue("@Email", email ?? (object)DBNull.Value);
//                cmd.Parameters.AddWithValue("@Mobile", mobile ?? (object)DBNull.Value);

//                int count = (int)cmd.ExecuteScalar();
//                exists = count > 0;
//            }
//        }
//        return exists;
//    }

//    private void WriteResponse(object obj)
//    {
//        var js = new JavaScriptSerializer();
//        Response.Write(js.Serialize(obj));
//        Response.End();
//    }
//}
