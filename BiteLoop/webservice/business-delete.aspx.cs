using BAL;
using Newtonsoft.Json;
using System;
using System.Data;

public partial class webservice_business_delete : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.ContentType = "application/json";

        string businessId = Request["BusinessID"];
        string secretKey = Request["SecretKey"];
        string authToken = Request["AuthToken"];

        if (string.IsNullOrEmpty(businessId) ||
            string.IsNullOrEmpty(secretKey) ||
            string.IsNullOrEmpty(authToken))
        {
            WriteResponse("false", "Missing required parameters.", "400");
            return;
        }

        // Create BAL object
        BusinessBAL bal = new BusinessBAL();

        // 1️⃣ Get Business Details
        DataTable dt = bal.GetBusinessByID(Convert.ToInt64(businessId));

        if (dt == null || dt.Rows.Count == 0)
        {
            WriteResponse("false", "BusinessID not found.", "404");
            return;
        }

        DataRow row = dt.Rows[0];

        bool IsValidated = ValidateRequestBAL.BusinessValidateClientRequest(
                                Convert.ToInt64(businessId),
                                secretKey,
                                authToken);

        if (!IsValidated)
        {
            WriteResponse("false", "Invalid SecretKey or AuthToken.", "401");
            return;
        }


        int result = BusinessBAL.DeleteBusiness(
            Convert.ToInt64(businessId),
            secretKey,
            authToken
        );

        if (result == 1)
        {
            WriteResponse("true", "Business deleted successfully.", "200");
        }
        else
        {
            WriteResponse("false", "Invalid BusinessID, SecretKey or AuthToken.", "401");
        }
    }

    void WriteResponse(string success, string message, string status)
    {
        var json = new
        {
            success = success,
            message = message,
            StatusCode = status
        };

        Response.Write(JsonConvert.SerializeObject(json));
        Response.End();
    }
}
