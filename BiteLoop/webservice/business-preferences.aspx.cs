using System;
using System.Data;
using System.Web;
using System.Web.UI;
using Newtonsoft.Json;
using DAL;

public partial class webservice_business_preferences : System.Web.UI.Page
{
    protected override void Render(HtmlTextWriter writer)
    {
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.HttpMethod == "POST")
        {
            SaveBusinessPreferences();
        }
        else
        {
            WriteJsonResponse(false, 405, "Method Not Allowed");
        }
    }

    private void SaveBusinessPreferences()
    {
        try
        {
            string businessIDStr = Request.Form["BusinessID"];
            string pushNotificationStr = Request.Form["PushNotification"];
            string emailNotificationStr = Request.Form["EmailNotification"];

            if (string.IsNullOrEmpty(businessIDStr))
            {
                WriteJsonResponse(false, 400, "BusinessID is required.");
                return;
            }

            long businessID = Convert.ToInt64(businessIDStr);
            bool pushNotification = pushNotificationStr == "1";
            bool emailNotification = emailNotificationStr == "1";

            DbParameter[] dbParam = new DbParameter[]
            {
                new DbParameter("@BusinessID", DbParameter.DbType.Int, 0, businessID),
                new DbParameter("@PushNotification", DbParameter.DbType.Bit, 0, pushNotification),
                new DbParameter("@EmailNotification", DbParameter.DbType.Bit, 0, emailNotification),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 0, ParameterDirection.Output)
            };

            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "SP_InsertOrUpdateBusinessPreferences", dbParam);

            int result = Convert.ToInt32(dbParam[3].Value);

            if (result == 1)
                WriteJsonResponse(true, 200, "Preferences updated successfully.");
            else if (result == 2)
                WriteJsonResponse(true, 200, "Preferences saved successfully.");
            else
                WriteJsonResponse(true, 200, "Unknown error occurred.");
        }
        catch (Exception ex)
        {
            WriteJsonResponse(false, 500, ex.Message);
        }
    }

    private void WriteJsonResponse(bool success, int statusCode, string message)
    {
        var responseObj = new
        {
            success = success,
            StatusCode = statusCode,
            message = message
        };

        string json = JsonConvert.SerializeObject(responseObj);

        HttpResponse httpResponse = HttpContext.Current.Response;
        httpResponse.Clear();
        httpResponse.ContentType = "application/json";
        httpResponse.StatusCode = statusCode;
        httpResponse.Write(json);
        httpResponse.Flush();

        httpResponse.SuppressContent = false;
        HttpContext.Current.ApplicationInstance.CompleteRequest();
    }
}


//using System;
//using System.Data;
//using System.Web;
//using System.Web.UI;
//using Newtonsoft.Json;
//using DAL;

//public partial class webservice_business_preferences : System.Web.UI.Page
//{
//    protected void Page_Load(object sender, EventArgs e)
//    {
//        if (Request.HttpMethod == "POST")
//        {
//            SaveBusinessPreferences();
//        }
//        else
//        {            
//            Response.StatusCode = 405; 
//            Response.End();
//        }
//    }

//    private void SaveBusinessPreferences()
//    {
//        try
//        {
//            string businessIDStr = Request.Form["BusinessID"];
//            string pushNotificationStr = Request.Form["PushNotification"];
//            string emailNotificationStr = Request.Form["EmailNotification"];

//            if (string.IsNullOrEmpty(businessIDStr))
//            {
//                WriteJsonResponse(true, 200, "BusinessID is required.");
//                return;
//            }

//            long businessID = Convert.ToInt64(businessIDStr);
//            bool pushNotification = pushNotificationStr == "1";
//            bool emailNotification = emailNotificationStr == "1";

//            DbParameter[] dbParam = new DbParameter[]
//            {
//                new DbParameter("@BusinessID", DbParameter.DbType.Int, 0, businessID),
//                new DbParameter("@PushNotification", DbParameter.DbType.Bit, 0, pushNotification),
//                new DbParameter("@EmailNotification", DbParameter.DbType.Bit, 0, emailNotification),
//                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 0, ParameterDirection.Output)
//            };

//            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "SP_InsertOrUpdateBusinessPreferences", dbParam);

//            int result = Convert.ToInt32(dbParam[3].Value);

//            if (result == 1)
//                WriteJsonResponse(true, 200, "Preferences updated successfully.");
//            else if (result == 2)
//                WriteJsonResponse(true, 200, "Preferences saved successfully.");
//            else
//                WriteJsonResponse(true, 200, "Unknown error occurred.");
//        }
//        catch (Exception ex)
//        {
//            WriteJsonResponse(false, 500, ex.Message);
//        }
//    }

//    private void WriteJsonResponse(bool success, int statusCode, string message)
//    {
//        var response = new
//        {
//            success = success,
//            StatusCode = statusCode,
//            message = message
//        };

//        string json = JsonConvert.SerializeObject(response);


//        HttpResponse httpResponse = HttpContext.Current.Response;
//        Response.Clear();
//        Response.ContentType = "application/json";
//        Response.StatusCode = statusCode;
//        Response.Write(json);
//        Response.Flush();
//        //Response.Write(JsonConvert.SerializeObject(response));

//        HttpContext.Current.ApplicationInstance.CompleteRequest();
//    }
//}
