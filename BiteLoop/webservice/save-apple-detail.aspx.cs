using System;
using System.Data;
using DAL;
using Newtonsoft.Json;

public partial class webservice_save_apple_detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string socialKey = Request["SocialMediaKey"];
        if (string.IsNullOrWhiteSpace(socialKey))
        {
            WriteResponse(false, "Please provide SocialMediaKey.", null);
            return;
        }

        string email = Request["Email"] ?? null;
        string name = Request["Name"] ?? null;
        string mobile = Request["Mobile"] ?? null;

        DbConnectionDAL.ExecuteNonQuery(
            CommandType.StoredProcedure,
            "SaveAppleDetails",
            new DbParameter[]
            {
                new DbParameter("@SocialMediaKey", DbParameter.DbType.VarChar, 500, socialKey),
                new DbParameter("@Email", DbParameter.DbType.VarChar, 500, email),
                new DbParameter("@Name", DbParameter.DbType.VarChar, 500, name),
                new DbParameter("@Mobile", DbParameter.DbType.VarChar, 20, mobile)
            }
        );

        WriteResponse(true, "Apple details saved successfully.", null);
    }

    private void WriteResponse(bool success, string message, object data)
    {
        var response = new
        {
            success = success ? "true" : "false",
            message = message,
            StatusCode = success ? "200" : "400",
            data = data == null ? new object[] { } : new[] { data }
        };

        string json = JsonConvert.SerializeObject(response,
            new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

        Response.ContentType = "application/json";
        Response.Write(json);
        Response.End();
    }
}
