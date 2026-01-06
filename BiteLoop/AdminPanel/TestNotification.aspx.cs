using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;


public partial class AdminPanel_TestNotification : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strDeviceKey = "dtfGMpaR_lM:APA91bE3zGCUARHsjBG0TNyFhgwYI7VVk2BM4DJLXaKXiMTS8vsIXzuMjX-mxRBx0mePd4mxJ_bQcY0VWdFRN_IWMUFtQICCWMo05z95GMOloS4N2cXV77WIXnKOaViJ9ub3CaXyW5gl";
        SendNotification objSendNotification = new SendNotification();
        CallNotification1(strDeviceKey, "Test Notification", "Test Notification", "123", "BMHNotification", "A", "B", 1);

    }

    public void CallNotification1(string deviceId, string message, string title, string strMessageID, string NotificationType, string DeviceType, string AppType, int NotificationCount)
    {
        if (DeviceType.ToUpper() == "A")
        {
            SendNotificationToDevice1(deviceId, message, title, strMessageID, NotificationType, AppType);
        }        
    }
    public void SendNotificationToDevice1(string deviceId, string message, string title, string strMessageID, string NotificationType, string AppType)
    {
        string tickerText = message;
        //string contentTitle = title;
        string contentTitle = "BringMeHome";
        string postData =
        "{\"to\":\"" + deviceId + "\", " +
          "\"data\": {\"tickerText\":\"" + tickerText + "\", " +
                     "\"contentTitle\":\"" + contentTitle + "\", " +
                      "\"messageID\":\"" + strMessageID + "\", " +
                        "\"NotificationType\":\"" + NotificationType + "\", " +
                     "\"message\": \"" + message + "\"},\"priority\":\"high\"}";

        string response = string.Empty;
        WriteLogFile(postData);
        if (AppType == "B")
        {
            WriteLogFile(strMessageID + "  ======>  " + deviceId + "  ======>  " + response);
            response = SendGCMNotification(Utility.Config.NotificationServerKey, postData);
            WriteLogFile(strMessageID + "  ======>  " + deviceId + "  ======>  " + response);
        }
        else if (AppType == "U")
        {
            response = SendGCMNotification("AIzaSyBkvPxzSCj3vEfOTOpnvrPGTzyLmi44uv8", postData);
        }
        WriteLogFile(strMessageID + "  ======>  " + deviceId + "  ======>  " + response);
    }
    public static bool ValidateServerCertificate(
                                               object sender,
                                               X509Certificate certificate,
                                               X509Chain chain,
                                               SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }
    private string SendGCMNotification(string apiKey, string postData, string postDataContentType = "application/json")
    {
        ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ValidateServerCertificate);

        //  
        //  MESSAGE CONTENT  
        byte[] byteArray = Encoding.UTF8.GetBytes(postData);

        //  
        //  CREATE REQUEST  
        HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("https://android.googleapis.com/gcm/send");
        Request.Method = "POST";
        //  Request.KeepAlive = false;  

        Request.ContentType = postDataContentType;
        Request.Headers.Add(string.Format("Authorization: key={0}", apiKey));
        Request.ContentLength = byteArray.Length;

        Stream dataStream = Request.GetRequestStream();
        dataStream.Write(byteArray, 0, byteArray.Length);
        dataStream.Close();

        //  
        //  SEND MESSAGE  
        //try
        //{
            WebResponse Response = Request.GetResponse();

            HttpStatusCode ResponseCode = ((HttpWebResponse)Response).StatusCode;
            if (ResponseCode.Equals(HttpStatusCode.Unauthorized) || ResponseCode.Equals(HttpStatusCode.Forbidden))
            {
                var text = "Unauthorized - need new token";
            }
            else if (!ResponseCode.Equals(HttpStatusCode.OK))
            {
                var text = "Response from web service isn't OK";
            }

            StreamReader Reader = new StreamReader(Response.GetResponseStream());
            string responseLine = Reader.ReadToEnd();
            Reader.Close();

            return responseLine;
        //}
        //catch (Exception e)
        //{
        //    WriteLogFile("Exception  ==> " + e.Message.ToString());
        //}
        return "error";
    }



    private void WriteLogFile(string LogMessage)
    {
        try
        {

            string path = System.Web.Hosting.HostingEnvironment.MapPath("~/source") + "\\Notifications123.txt";
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("---------------------------------------------------------------------");
                    sw.WriteLine(LogMessage);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine("---------------------------------------------------------------------");
                    sw.WriteLine(LogMessage);
                }
            }
        }
        catch (Exception ex)
        {

        }

    }

}