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

public partial class webservice_Apple_user_detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Validate SocialMediaKey parameter
        if (Request["SocialMediaKey"] == null || string.IsNullOrWhiteSpace(Request["SocialMediaKey"]))
        {
            WriteResponse(false, "Please provide SocialMediaKey.", null);
            return;
        }

        string socialKey = Request["SocialMediaKey"].Trim();

        DataTable dt = DbConnectionDAL.GetDataTable(
            CommandType.StoredProcedure,
            "GetAppleUserDetails",
            new DbParameter[]
            {
                new DbParameter("@SocialMediaKey", DbParameter.DbType.VarChar, 500, socialKey)
            }
        );

        if (dt.Rows.Count == 0)
        {
            WriteResponse(false, "No record found for this SocialMediaKey.", null);
            return;
        }

        // Build response data
        var result = new
        {
            SocialMediaKey = Convert.ToString(dt.Rows[0]["SocialMediaKey"]),
            Name = Convert.ToString(dt.Rows[0]["Name"]),
            Email = Convert.ToString(dt.Rows[0]["Email"]),
            Mobile = Convert.ToString(dt.Rows[0]["Mobile"]),
        };

        WriteResponse(true, "Data fetched successfully.", result);
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

        HttpContext.Current.Response.ContentType = "application/json";
        HttpContext.Current.Response.Write(json);
        HttpContext.Current.Response.End();
    }
}