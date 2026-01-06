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
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net.Http;

/// <summary>
/// Summary description for SendNotification
/// </summary>
public class SendNotification
{
    public SendNotification()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void CallNotification(string deviceId, string message, string title, string strMessageID, string NotificationType, string DeviceType, string AppType, int NotificationCount)
    {
        if (DeviceType.ToUpper() == "A")
        {
            SendNotificationToDevice(deviceId, message, title, strMessageID, NotificationType, AppType);
        }
        else if (DeviceType.ToUpper() == "I")
        {
            //SendNotificationToIOS(deviceId, message, title, strMessageID, NotificationType, AppType, NotificationCount);
            SendNotificationToDeviceIOS(deviceId, message, title, strMessageID, NotificationType, AppType, NotificationCount);
        }
    }

    public void SendNotificationToDevice(string deviceId, string message, string title, string strMessageID, string NotificationType, string AppType)
    {
        string tickerText = message;
        //string contentTitle = title;
        string contentTitle = title;
        string postData =
        "{\"to\":\"" + deviceId + "\", " +
          "\"data\": {\"tickerText\":\"" + tickerText + "\", " +
                     "\"contentTitle\":\"" + contentTitle + "\", " +
                      "\"messageID\":\"" + strMessageID + "\", " +
                        "\"NotificationType\":\"" + NotificationType + "\", " +
                     "\"message\": \"" + message + "\"},\"priority\":\"high\"}";

        string response = string.Empty;

        if (AppType == "B")
        {
            response = SendGCMNotification(Utility.Config.NotificationServerKey, postData);
        }
        else if (AppType == "U")
        {
            response = SendGCMNotification("AIzaSyBkvPxzSCj3vEfOTOpnvrPGTzyLmi44uv8", postData);
        }

        WriteLogFile(AppType + "-----" + postData + "  ======>>>>>>>>    " + strMessageID + "  ======>  " + deviceId + "  ======>  " + response);
    }

    private string SendGCMNotification(string apiKey, string postData, string postDataContentType = "application/json")
    {
        ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ValidateServerCertificate);

        //  
        //  MESSAGE CONTENT  
        byte[] byteArray = Encoding.UTF8.GetBytes(postData);

        //  
        //  CREATE REQUEST  
        //HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("https://android.googleapis.com/gcm/send");
        HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("https://fcm.googleapis.com/fcm/send");
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
        try
        {
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
        }
        catch (Exception e)
        {
            WriteLogFile("Exception  ==> " + e.Message.ToString());
        }
        return "error";
    }

    private void SendNotificationToIOS(string deviceId, string message, string title, string strMessageID, string NotificationType, string AppType, int NotificationCount)
    {
        try
        {
            string strText = "[{";
            strText = strText + "\"title\": \"" + title + "\",";
            strText = strText + "\"AppType\": \"" + AppType + "\",";
            strText = strText + "\"description\": \"" + message + "\",";
            strText = strText + "\"badge\": \"" + NotificationCount + "\",";
            strText = strText + "\"id\": \"" + strMessageID + "\",";
            strText = strText + "\"NotificationType\": \"" + NotificationType + "\",";
            strText = strText + "\"device_token\": [";
            strText = strText + "\"" + deviceId + "\"";
            strText = strText + "]";
            strText = strText + "}]";

            string strWebrequestURL = string.Empty;
            if (AppType.ToUpper() == "U")
            {
                strWebrequestURL = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["IOSPHPURLUsers"]);
            }
            else
            {
                strWebrequestURL = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["IOSPHPURL"]);
            }



            //WebRequest request = WebRequest.Create(System.Configuration.ConfigurationManager.AppSettings["IOSPHPURL"]);
            WebRequest request = WebRequest.Create(strWebrequestURL);
            request.Method = "POST";
            string postData = "data=" + strText;


            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            dataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);

            string responseFromServer = reader.ReadToEnd();

            reader.Close();
            dataStream.Close();
            response.Close();
        }
        catch (Exception ex)
        {
            WriteLogFile(ex.Message.ToString());
        }
    }

    public void SendNotificationToIOSMultiple(string strText)
    {
        try
        {

            WebRequest request = WebRequest.Create(System.Configuration.ConfigurationManager.AppSettings["IOSPHPURL"]);
            request.Method = "POST";
            string postData = "data=" + strText;

            //WriteLogFile(strText);

            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            dataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);

            string responseFromServer = reader.ReadToEnd();

            reader.Close();
            dataStream.Close();
            response.Close();
        }
        catch (Exception ex)
        {
            WriteLogFile(ex.Message.ToString());
        }
    }

    public void SendNotificationToIOSMultipleUsers(string strText)
    {
        try
        {

            WebRequest request = WebRequest.Create(System.Configuration.ConfigurationManager.AppSettings["IOSPHPURLUsers"]);
            request.Method = "POST";
            string postData = "data=" + strText;


            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            dataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);

            string responseFromServer = reader.ReadToEnd();

            reader.Close();
            dataStream.Close();
            response.Close();
        }
        catch (Exception ex)
        {
            WriteLogFile(ex.Message.ToString());
        }
    }

    public static bool ValidateServerCertificate(
                                                 object sender,
                                                 X509Certificate certificate,
                                                 X509Chain chain,
                                                 SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }
    private void WriteLogFile(string LogMessage)
    {
        try
        {

            string path = System.Web.Hosting.HostingEnvironment.MapPath("~/source") + "\\LogFile.txt";
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

    #region IOS

    public void SendNotificationToDeviceIOS(string deviceId, string message, string title, string strMessageID, string NotificationType, string AppType, int NotificationCount)
    {
        string tickerText = message;
        string contentTitle = title;
        string Sound = "default";
        string postData =
        "{\"to\":\"" + deviceId + "\", " +
          "\"notification\": {\"tickerText\":\"" + tickerText + "\", " +
                     "\"title\":\"" + contentTitle + "\", " +
                     "\"badge\":\"" + NotificationCount + "\", " +
                       "\"sound\":\"" + Sound + "\", " +
                     "\"body\": \"" + message + "\"}," +
                     "\"data\": {\"tickerText\":\"" + tickerText + "\", " +
                     "\"contentTitle\":\"" + contentTitle + "\", " +
                      "\"messageID\":\"" + strMessageID + "\", " +
                        "\"NotificationType\":\"" + NotificationType + "\", " +
                     "\"message\": \"" + message + "\"},\"priority\":\"high\"}";

        string response = string.Empty;

        if (AppType == "B")
        {
            response = SendGCMNotificationIOS(Convert.ToString(ConfigurationManager.AppSettings["IOSBusinessNotificationServerKey"]), postData);
        }
        else if (AppType == "U")
        {
            response = SendGCMNotificationIOS(Convert.ToString(ConfigurationManager.AppSettings["IOSUserNotificationServerKey"]), postData);
        }
        WriteLogFile(AppType + "-----" + postData + "  ======>>>>>>>>    " + strMessageID + "  ======>  " + deviceId + "  ======>  " + response);
    }
    private string SendGCMNotificationIOS(string apiKey, string postData, string postDataContentType = "application/json")
    {
        ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ValidateServerCertificate);

        //  
        //  MESSAGE CONTENT  
        byte[] byteArray = Encoding.UTF8.GetBytes(postData);

        //  
        //  CREATE REQUEST  

        HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("https://fcm.googleapis.com/fcm/send");
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
        try
        {
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
        }
        catch (Exception e)
        {
            WriteLogFile("Exception  ==> " + e.Message.ToString());
        }
        return "error";
    }



    #endregion


    #region Notification
    public async Task<string> GetAccessTokenAsync(string jsonFilePath)
    {
        //GoogleCredential credential = GoogleCredential
        //    .FromFile(Server.MapPath("~/biteloop-b7f33-firebase-adminsdk-fbsvc-dd6ff02569.json"))
        //    .CreateScoped("https://www.googleapis.com/auth/firebase.messaging");

        GoogleCredential credential = GoogleCredential
           .FromFile(jsonFilePath)
           .CreateScoped("https://www.googleapis.com/auth/firebase.messaging");

        string token = await credential.UnderlyingCredential
            .GetAccessTokenForRequestAsync()
            .ConfigureAwait(false);

        return token;
    }

    //public async Task SendPushNotification(string deviceToken, string jsonFilePath, string title, string notification_message, string notification_id, string order_id)
    //{
    //  //  ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;//;| SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;


    //    string accessToken = await GetAccessTokenAsync(jsonFilePath).ConfigureAwait(false);

    //    string projectId = "biteloop-b7f33";

    //    string url = "https://fcm.googleapis.com/v1/projects/" + projectId + "/messages:send";

    //    var message = new FcmMessage()
    //    {
    //        message = new Message()
    //        {
    //            token = deviceToken,
    //            data = new Dictionary<string, string>()
    //            {
    //                { "title", title},
    //                { "message", notification_message},
    //                { "notification_id", notification_id },
    //                { "order_id", order_id },
    //            }
    //        }
    //    };

    //    string jsonMessage = JsonConvert.SerializeObject(message);

    //    using (var client = new HttpClient())
    //    {
    //        client.DefaultRequestHeaders.Authorization =
    //            new AuthenticationHeaderValue("Bearer", accessToken);

    //        var content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");

    //        HttpResponseMessage response = await client.PostAsync(url, content)
    //                                                  .ConfigureAwait(false);

    //        string responseBody = await response.Content.ReadAsStringAsync();          
    //    }
    //}

    public async Task SendPushNotification(
                                string deviceToken,
                                string jsonFilePath,
                                string title,
                                string notification_message,
                                string notification_id,
                                string order_id
     )
    {
        try
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string accessToken = await GetAccessTokenAsync(jsonFilePath)
                .ConfigureAwait(false);

            string projectId = "biteloop-b7f33";
            string url = "https://fcm.googleapis.com/v1/projects/" + projectId + "/messages:send";

            var message = new FcmMessage()
            {
                message = new Message()
                {
                    token = deviceToken,
                    data = new Dictionary<string, string>()
                {
                    { "title", title},
                    { "message", notification_message},
                    { "notification_id", notification_id },
                    { "order_id", order_id },
                }
                }
            };

            string jsonMessage = JsonConvert.SerializeObject(message);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", accessToken);

                var content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, content)
                    .ConfigureAwait(false);

                string responseBody = await response.Content.ReadAsStringAsync();

                //// Log BOTH STATUS CODE & RESPONSE BODY
                //WriteLogFile("STATUS: " + response.StatusCode);
                //WriteLogFile("RESPONSE: " + responseBody);
            }
        }
        catch (Exception ex)
        {
            WriteLogFile("EXCEPTION: " + ex.ToString());
        }
    }

    #endregion


}


public class FcmMessage
{
    public Message message { get; set; }
}

public class Message
{
    public string token { get; set; }
    public Notification notification { get; set; }
    public Dictionary<string, string> data { get; set; }
}

public class Notification
{
    public string title { get; set; }
    public string body { get; set; }
}
