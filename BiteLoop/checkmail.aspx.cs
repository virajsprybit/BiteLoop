using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using Utility;
using Newtonsoft.Json;
using System.Net;
using System.Data;
using System.Net.Mail;
public partial class CheckMail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SendEmail("johnsmsmithsm@gmail.com", new GeneralSettings().getConfigValue("infoemail"), "Test Subject", "Test Body");
    }
    private bool SendEmail(string strTo, string strFrom, string strSubject, string strBody)
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
            //client.Host = Config.SMTPServer;
            client.Port = 587;
            client.Host = new GeneralSettings().getConfigValue("hostname");
            client.Credentials = new NetworkCredential(new GeneralSettings().getConfigValue("emailaddress"), new GeneralSettings().getConfigValue("emailpassword"));
            try
            {
                client.EnableSsl = Convert.ToBoolean(new GeneralSettings().getConfigValue("SSL"));
            }
            catch (Exception ex)
            {
                client.EnableSsl = true;
            }

            client.Send(message);
            flag = true;
            Response.Write(flag);
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString() + "================> " + ex.StackTrace.ToString());
        }
        return flag;
    }
   
}