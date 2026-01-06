using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class StripeTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        PushIOS("1f085bc2f9dcd9805dd9cdb555945cf419b8f54c80ed9402517b96000d1dd0d6");


        int port = 2195;
        String hostname = "gateway.sandbox.push.apple.com";

        //load certificate
        string certificatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pushcert.pem");
        string certificatePassword = "12345jsk";
        X509Certificate2 clientCertificate = new X509Certificate2(certificatePath, certificatePassword);
        X509Certificate2Collection certificatesCollection = new X509Certificate2Collection(clientCertificate);

        TcpClient client = new TcpClient(hostname, port);
        SslStream sslStream = new SslStream(
                client.GetStream(),
                false,
                new RemoteCertificateValidationCallback(ValidateServerCertificate),
                null
        );

        try
        {
            sslStream.AuthenticateAsClient(hostname, certificatesCollection, SslProtocols.Tls, true);
        }
        catch (AuthenticationException ex)
        {
            client.Close();
            return;
        }

        // Encode a test message into a byte array.
        MemoryStream memoryStream = new MemoryStream();
        BinaryWriter writer = new BinaryWriter(memoryStream);

        writer.Write((byte)0);  //The command
        writer.Write((byte)0);  //The first byte of the deviceId length (big-endian first byte)
        writer.Write((byte)32); //The deviceId length (big-endian second byte)

        String deviceId = "1f085bc2f9dcd9805dd9cdb555945cf419b8f54c80ed9402517b96000d1dd0d6";
        writer.Write(HexStringToByteArray(deviceId.ToUpper()));

        String payload = "{\"aps\":{\"alert\":\"I like spoons also\",\"badge\":14}}";

        writer.Write((byte)0); //First byte of payload length; (big-endian first byte)
        writer.Write((byte)payload.Length); //payload length (big-endian second byte)

        byte[] b1 = System.Text.Encoding.UTF8.GetBytes(payload);
        writer.Write(b1);
        writer.Flush();

        byte[] array = memoryStream.ToArray();
        sslStream.Write(array);
        sslStream.Flush();

        // Close the client connection.
        client.Close();




        //string  P12CertificateName = Config.P12CertificateName.ToString();
        //string P12FilePassword = Config.P12FilePassword.ToString();
        //string strNotificationReturnMessage = string.Empty;

        //var payload = new NotificationPayload("75d83c5e2b312ef3466a8ba8f0e6a7a432ea319e00ab127852675536cc274492", "Test Apple Notification", 1, "default");
        //var payloads = new List<NotificationPayload> { payload };
        //string p12File = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, P12CertificateName);

        //var push = new AppleSendNotification(true, p12File, P12FilePassword);
        //var rejected = push.SendToApple(payloads);

        //if (rejected != null && rejected.Count > 0)
        //{            
        //    strNotificationReturnMessage = "Apple Notification failure";
        //}



        // CreateStripeTransaction();
    }

    private bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        throw new NotImplementedException();
    }
    private bool validateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        return true; // Dont care about server's cert
    }
    public static byte[] HexStringToByteArray(string hex)
    {
        return Enumerable.Range(0, hex.Length)
                         .Where(x => x % 2 == 0)
                         .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                         .ToArray();
    }
    private void PushIOS(string deviceID)
    {
        int port = 2195;
        String hostname = "gateway.sandbox.push.apple.com";
        string p12File = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"pushcert.pem");
        //string p12File = @"E:\Projects\Bring Me Home\Development\Development\bringmehome\bringmehome16june.p12";
        //E:\Projects\Bring Me Home\Development\Development\bringmehome
        String certificatePath = p12File;
        X509Certificate2 clientCertificate = new X509Certificate2(System.IO.File.ReadAllBytes(certificatePath), "12345jsk", X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.Exportable);
        X509Certificate2Collection certificatesCollection = new X509Certificate2Collection(clientCertificate);

        
        TcpClient client = new TcpClient(hostname, port);
        SslStream sslStream = new SslStream(client.GetStream(), false, new RemoteCertificateValidationCallback(validateServerCertificate), null);

        try
        {
            sslStream.AuthenticateAsClient(hostname, certificatesCollection, SslProtocols.Tls, false);
            MemoryStream memoryStream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(memoryStream);
            writer.Write((byte)0);
            writer.Write((byte)0);
            writer.Write((byte)32);

            writer.Write(HexStringToByteArray(deviceID.ToUpper()));
            String payload = "{\"aps\":{\"alert\":\"" + "Hi,, This Is a Sample Push Notification For IPhone.." + "\",\"badge\":1,\"sound\":\"default\"}}";
            writer.Write((byte)0);
            writer.Write((byte)payload.Length);
            byte[] b1 = System.Text.Encoding.UTF8.GetBytes(payload);
            writer.Write(b1);
            writer.Flush();
            byte[] array = memoryStream.ToArray();
            sslStream.Write(array);
            sslStream.Flush();
            client.Close();
            Response.Write("Message Success");
        }
        catch (System.Security.Authentication.AuthenticationException ex)
        {
            Response.Write("Error: 1 : ===> " + ex.Message.ToString() + "===> " + Convert.ToString(ex.InnerException));
            client.Close();
        }
        catch (Exception e)
        {
			//Response.Write("Error: 2 : ===> " + e.Message.ToString());
            throw e;
            //client.Close();
        }
        
    }

    public void SetBasicAuthHeader(WebRequest request, string username, string password)
    {
        string auth = username + ":" + password;
        auth = Convert.ToBase64String(Encoding.Default.GetBytes(auth));
        request.Headers["Authorization"] = "Basic " + auth;
    }
    private void CreateStripeTransaction()
    {
        var stripetoken = Request["stripetoken"];       // comes from stripe.js

        string data = string.Format("amount=10&currency=aud&source={0}&description=testcharge", "");
        byte[] dataStream = Encoding.UTF8.GetBytes(data);
        string charge_id = string.Empty;
        var endpoint = "https://api.stripe.com/v1/charges";
        string username = "sk_test_RYrujANj0BBneiLue0uVPfBe";
        WebRequest request = HttpWebRequest.Create(endpoint);

        SetBasicAuthHeader(request, username, "");  // BASIC AUTH
        request.Method = "POST";
        request.ContentLength = dataStream.Length;
        Stream newstream = request.GetRequestStream();

        newstream.Write(dataStream, 0, dataStream.Length);
        newstream.Close();
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

        var response = request.GetResponse();

        // get the result
        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
        {
            JsonSerializer json = new JsonSerializer();
            JObject content = JObject.Parse(reader.ReadToEnd());

            charge_id = content["id"].ToString();
        }

        response.Close();
    }

}