using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class IOSPHP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strText = "{";
        strText = strText + "'title': 'This is test notification',";
        strText = strText + "'description': 'This is for test desscription',";
        strText = strText + "'device_token': [";
        strText = strText + "'d32815889f9750265d249aeac794ba602c2f8c0b8e5bd6a8126514299483f1b0',";
        strText = strText + "'d32815889f9750265d249aeac794ba602c2f8c0b8e5bd6a8126514299483f1b0'";
        strText = strText + "]";
        strText = strText + "}";

        SendPostRequest(strText);
    }

    private void SendPostRequest(string strText)
    {
        WebRequest request = WebRequest.Create(System.Configuration.ConfigurationManager.AppSettings["IOSPHPURL"]);     
        request.Method = "POST";

        //string postData = strText;

        string postData = "data=123";        


        byte[] byteArray = Encoding.UTF8.GetBytes(postData);

        request.ContentType = "application/json";
        request.ContentLength = byteArray.Length;        
        Stream dataStream = request.GetRequestStream();        
        dataStream.Write(byteArray, 0, byteArray.Length);        
        dataStream.Close();        
        WebResponse response = request.GetResponse();        

        
        Response.Write(((HttpWebResponse)response).StatusDescription);        
        dataStream = response.GetResponseStream();
       
        StreamReader reader = new StreamReader(dataStream);
       
        string responseFromServer = reader.ReadToEnd();

        Response.Write(responseFromServer);
       
        reader.Close();
        dataStream.Close();
        response.Close();
    }

    private string SendIOS(string NotificationsDetails)
    {
        string Responce = string.Empty;
        try
        {
            if (NotificationsDetails != string.Empty)
            {
                using (WebClientExtended webClient = new WebClientExtended())
                {
                    webClient.Timeout = 9000000;
                    webClient.Headers.Add("Content-Type", "application/json");
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    var webAPIResponse = webClient.UploadData(System.Configuration.ConfigurationManager.AppSettings["IOSPHPURL"] + "", "POST", Encoding.UTF8.GetBytes(NotificationsDetails));

                    Responce = UnicodeEncoding.UTF8.GetString(webAPIResponse);
                }
            }
        }
        catch (Exception ex)
        {
            return string.Empty;
        }
        return Responce;
    }
}
public class WebClientExtended : WebClient
{
    private int _Timeout;

    public int Timeout
    {
        get { return _Timeout; }
        set { _Timeout = value; }
    }

    protected override WebRequest GetWebRequest(Uri address)
    {
        var request = base.GetWebRequest(address);
        request.Timeout = Timeout;
        return request;
    }
}