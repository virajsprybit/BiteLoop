using System;
using System.Data;
using System.Web;
using System.Web.UI;
using Newtonsoft.Json;
using DAL;

public partial class webservice_business_preferences_get : System.Web.UI.Page
{
    protected override void Render(HtmlTextWriter writer)
    {
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.HttpMethod == "GET")
        {
            GetBusinessPreferences();
        }
        else
        {
            WriteJsonResponse(false, 405, "Method Not Allowed");
        }
    }

    private void GetBusinessPreferences()
    {
        try
        {
            string businessIDStr = Request.QueryString["BusinessID"];

            if (string.IsNullOrEmpty(businessIDStr))
            {
                WriteJsonResponse(false, 400, "BusinessID is required.");
                return;
            }

            long businessID = Convert.ToInt64(businessIDStr);

            DbParameter[] dbParam = new DbParameter[]
            {
                new DbParameter("@BusinessID", DbParameter.DbType.Int, 0, businessID)
            };

            DataSet ds = DbConnectionDAL.GetDataSet(
                CommandType.StoredProcedure,"SP_GetBusinessPreferences",dbParam
            );

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                var row = ds.Tables[0].Rows[0];

                var responseObj = new
                {
                    success = true,
                    StatusCode = 200,
                    Message = "Record Found Successfully",
                    Data = new
                    {
                        BusinessID = Convert.ToInt64(row["BusinessID"]),
                        PushNotification = Convert.ToInt32(row["PushNotification"]),
                        EmailNotification = Convert.ToInt32(row["EmailNotification"])
                    }
                };

                WriteJson(responseObj);
            }
            else
            {
                WriteJsonResponse(false, 404, "Business not found.");
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