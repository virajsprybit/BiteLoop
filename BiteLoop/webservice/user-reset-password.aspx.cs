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
using System.Runtime.InteropServices.WindowsRuntime;

public partial class webservice_user_reset_password : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Keys.Count > 0)
        {
            ResetPassword();
        }
    }

    private void ResetPassword()
    {
        Response objResponse = new Response();

        string Email = Convert.ToString(Request["Email"] ?? "").Trim();
        string OTP = Convert.ToString(Request["OTP"] ?? "").Trim();
        string NewPassword = Convert.ToString(Request["NewPassword"] ?? "").Trim();

        if (Email == "" || OTP == "" || NewPassword == "")
        {
            objResponse.success = "false";
            objResponse.message = "Email, OTP, and NewPassword are required.";
            WriteResponse(objResponse);
            return;
        }

        int returnVal = 0;

        string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connStr))
        {
            con.Open();

            using (SqlCommand cmd = new SqlCommand("UserResetPassword", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@OTP", OTP);
                cmd.Parameters.AddWithValue("@NewPassword", NewPassword);

                SqlParameter output = new SqlParameter("@ReturnVal", SqlDbType.Int);
                output.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(output);
                cmd.ExecuteNonQuery();

                returnVal = Convert.ToInt32(output.Value);
            }
        }

        if (returnVal == 1)
        {
            objResponse.success = "true";
            objResponse.message = "Your password has been reset successfully.";
        }
        else if (returnVal == -1)
        {
            objResponse.success = "false";
            objResponse.message = "Email is not registered with us.";
        }
        else if (returnVal == -2)
        {
            objResponse.success = "false";
            objResponse.message = "Invalid OTP.";
        }
        else
        {
            objResponse.success = "false";
            objResponse.message = "Something went wrong. Please try again.";
        }

        WriteResponse(objResponse);
    }

    private void WriteResponse(Response obj)
    {
        HttpContext.Current.Response.Write(
            JsonConvert.SerializeObject(obj, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore })
        );
        Response.End();
    }
}