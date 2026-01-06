using System;
using System.Data;
using System.Web;
using System.Web.UI;
using Newtonsoft.Json;
using DAL;

public partial class webservice_user_preferences_get : System.Web.UI.Page
{
    protected override void Render(HtmlTextWriter writer)
    {
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.HttpMethod == "GET")
        {
            GetUserPreferences();
        }
        else
        {
            WriteJsonResponse(false, 405, "Method Not Allowed");
        }
    }

    private void GetUserPreferences()
    {
        try
        {
            string userIDStr = Request.QueryString["UserID"];

            if (string.IsNullOrEmpty(userIDStr))
            {
                WriteJsonResponse(false, 400, "UserID is required.");
                return;
            }

            long userID = Convert.ToInt64(userIDStr);

            DbParameter[] dbParam = new DbParameter[]
            {
                new DbParameter("@UserID", DbParameter.DbType.Int, 0, userID)
            };

            DataSet ds = DbConnectionDAL.GetDataSet(
                CommandType.StoredProcedure,
                "SP_GetUserPreferences",
                dbParam
            );

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                var row = ds.Tables[0].Rows[0];

                var responseObj = new
                {
                    success = true,
                    Message = "Record Found Successfully",
                    StatusCode = 200,
                    Data = new
                    {
                        UserID = Convert.ToInt64(row["UserID"]),
                        PushNotification = Convert.ToInt32(row["PushNotification"]),
                        EmailNotification = Convert.ToInt32(row["EmailNotification"])
                    }
                };

                WriteJson(responseObj);
            }
            else
            {
                WriteJsonResponse(false, 404, "No preferences found for this user.");
            }
        }
        catch (Exception ex)
        {
            WriteJsonResponse(false, 500, ex.Message);
        }
    }

    private void WriteJson(object obj)
    {
        string json = JsonConvert.SerializeObject(obj);

        HttpResponse response = HttpContext.Current.Response;
        response.Clear();
        response.ContentType = "application/json";
        response.Write(json);
        response.Flush();
        HttpContext.Current.ApplicationInstance.CompleteRequest();
    }

    private void WriteJsonResponse(bool success, int statusCode, string message)
    {
        var responseObj = new
        {
            success = success,
            StatusCode = statusCode,
            message = message
        };
        WriteJson(responseObj);
    }
}