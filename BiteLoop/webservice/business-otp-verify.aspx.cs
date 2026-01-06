using System;
using System.Data;
using System.Web;
using System.Web.UI;
using BAL;
using Newtonsoft.Json;
using Utility.Security; // your AES256Encrypt

public partial class webservice_business_otp_verify : System.Web.UI.Page
{
    protected override void Render(HtmlTextWriter writer)
    {
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Count > 0)
        {
            VerifyAndChangePassword();
        }
        else
        {
            Response.Write(JsonConvert.SerializeObject(new { success = false, message = "Invalid request" }));
        }
    }

    private void VerifyAndChangePassword()
    {
        try
        {
            BusinessBAL objBusinessBAL = new BusinessBAL();
            objBusinessBAL.EmailAddress = Request.Form["EmailAddress"];
            objBusinessBAL.OTP = Request.Form["Otp"];
            objBusinessBAL.NewPassword = EncryptDescrypt.EncryptString(Request.Form["NewPassword"]);

            // Step 1: Verify OTP
            DataTable dt = objBusinessBAL.VerifyBusinessOTP();
            if (dt.Rows.Count > 0 && Convert.ToBoolean(dt.Rows[0]["IsValidOTP"]))
            {
                // Step 2: Change Password
                objBusinessBAL.ChangeBusinessPassword();

                Response.Write(JsonConvert.SerializeObject(new
                {
                    success = true,
                    message = "Password changed successfully."
                }));
            }
            else
            {
                Response.Write(JsonConvert.SerializeObject(new
                {
                    success = false,
                    message = "Invalid OTP. Please try again."
                }));
            }
        }
        catch (Exception ex)
        {
            Response.Write(JsonConvert.SerializeObject(new
            {
                success = false,
                message = "Error: " + ex.Message
            }));
        }
    }
}
