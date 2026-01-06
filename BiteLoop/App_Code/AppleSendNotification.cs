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
using System.Net.Sockets;
using System.Threading;
using Newtonsoft.Json.Linq;

/// <summary>
/// Summary description for SendNotification
/// </summary>
public class AppleSendNotification
{
    public AppleSendNotification()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    private TcpClient _apnsClient;
    private SslStream _apnsStream;
    private X509Certificate _certificate;
    private X509CertificateCollection _certificates;

    public string P12File { get; set; }
    public string P12FilePassword { get; set; }

    // Default configurations for APNS
    private const string ProductionHost = "gateway.push.apple.com";
    private const string SandboxHost = "gateway.sandbox.push.apple.com";
    private const int NotificationPort = 2195;

    // Default configurations for Feedback Service
    private const string ProductionFeedbackHost = "feedback.push.apple.com";
    private const string SandboxFeedbackHost = "feedback.sandbox.push.apple.com";
    private const int FeedbackPort = 2196;

    private bool _conected = false;
    private readonly string _host;
    private readonly string _feedbackHost;

    private List<NotificationPayload> _notifications = new List<NotificationPayload>();
    private List<string> _rejected = new List<string>();

    private Dictionary<int, string> _errorList = new Dictionary<int, string>();

    
    public AppleSendNotification(bool useSandbox, string p12File, string p12FilePassword)
    {   

        if (useSandbox)
        {
            _host = SandboxHost;
            _feedbackHost = SandboxFeedbackHost;
        }
        else
        {
            _host = ProductionHost;
            _feedbackHost = ProductionFeedbackHost;
        }

        //Load Certificates in to collection.
        _certificate = string.IsNullOrEmpty(p12FilePassword) ? new X509Certificate2(File.ReadAllBytes(p12File)) : new X509Certificate2(File.ReadAllBytes(p12File), p12FilePassword);
        _certificates = new X509CertificateCollection { _certificate };

        // Loading Apple error response list.
        _errorList.Add(0, "No errors encountered");
        _errorList.Add(1, "Processing error");
        _errorList.Add(2, "Missing device token");
        _errorList.Add(3, "Missing topic");
        _errorList.Add(4, "Missing payload");
        _errorList.Add(5, "Invalid token size");
        _errorList.Add(6, "Invalid topic size");
        _errorList.Add(7, "Invalid payload size");
        _errorList.Add(8, "Invalid token");
        _errorList.Add(255, "None (unknown)");
    }

    public List<string> SendToApple(List<NotificationPayload> queue)
    {
       // _eventLog.WriteEntry("Payload queue received.", System.Diagnostics.EventLogEntryType.Information);
        _notifications = queue;
        if (queue.Count < 8999)
        {
            SendQueueToapple(_notifications);
        }
        else
        {
            const int pageSize = 8999;
            int numberOfPages = (queue.Count / pageSize) + (queue.Count % pageSize == 0 ? 0 : 1);
            int currentPage = 0;

            while (currentPage < numberOfPages)
            {
                _notifications = (queue.Skip(currentPage * pageSize).Take(pageSize)).ToList();
                SendQueueToapple(_notifications);
                currentPage++;
            }
        }
        //Close the connection
        Disconnect();
        return _rejected;
    }

    private void SendQueueToapple(IEnumerable<NotificationPayload> queue)
    {
        int i = 1000;
        foreach (var item in queue)
        {
            if (!_conected)
            {
                Connect(_host, NotificationPort, _certificates);
                var response = new byte[6];
                _apnsStream.BeginRead(response, 0, 6, ReadResponse, new MyAsyncInfo(response, _apnsStream));
            }

            try
            {
                if (item.DeviceToken.Length == 64) //check lenght of device token, if its shorter or longer stop generating Payload.
                {
                    item.PayloadId = i;
                    byte[] payload = GeneratePayload(item);
                    _apnsStream.Write(payload);

                    //_eventLog.WriteEntry(string.Format("Notification successfully sent to APNS server for Device Toekn : {0}", item.DeviceToken), System.Diagnostics.EventLogEntryType.Information);
                    Thread.Sleep(1000); //Wait to get the response from apple.
                }
                else
                {
                  //  _eventLog.WriteEntry(string.Format("Invalid device token length, possible simulator entry: {0}", item.DeviceToken), System.Diagnostics.EventLogEntryType.Information);
                }
            }
            catch (Exception ex)
            {
              //  _eventLog.WriteEntry(string.Format("An error occurred on sending payload for device token {0} - {1}", item.DeviceToken, ex), System.Diagnostics.EventLogEntryType.Information);
                _conected = false;
            }
            i++;
        }
    }

    private void ReadResponse(IAsyncResult ar)
    {
        if (!_conected)
            return;
        string payLoadId = "";
        int payLoadIndex = 0;

        try
        {
            var info = ar.AsyncState as MyAsyncInfo;
            info.MyStream.ReadTimeout = 100;
            if (_apnsStream.CanRead)
            {
                var command = Convert.ToInt16(info.ByteArray[0]);
                var status = Convert.ToInt16(info.ByteArray[1]);
                var ID = new byte[4];
                Array.Copy(info.ByteArray, 2, ID, 0, 4);

                payLoadId = Encoding.Default.GetString(ID);
                payLoadIndex = ((int.Parse(payLoadId)) - 1000);

              //  _eventLog.WriteEntry(string.Format("Apple rejected palyload for device token: {0}, Apple Error code: {1}",_notifications[payLoadIndex].DeviceToken, _errorList[status]), System.Diagnostics.EventLogEntryType.Error);

                _rejected.Add(_notifications[payLoadIndex].DeviceToken);
                _conected = false;
            }
        }
        catch (Exception ex)
        {
          //  _eventLog.WriteEntry(string.Format("An error occurred while reading Apple response for token {0} - {1}",_notifications[payLoadIndex].DeviceToken, ex), System.Diagnostics.EventLogEntryType.Error);
        }
    }

    private void Connect(string host, int port, X509CertificateCollection certificates)
    {

        try
        {
            _apnsClient = new TcpClient();
            _apnsClient.Connect(host, port);
        }
        catch (SocketException ex)
        {
            //_eventLog.WriteEntry(string.Format("Error while connecting to apple server: {0}", ex), System.Diagnostics.EventLogEntryType.Error);
        }

        var sslOpened = OpenSslStream(host, certificates);

        if (sslOpened)
        {
            _conected = true;
           // _eventLog.WriteEntry("Connected to apple server.", System.Diagnostics.EventLogEntryType.Information);
        }
    }

    private void Disconnect()
    {
        try
        {
            Thread.Sleep(500);
            _apnsClient.Close();
            _apnsStream.Close();
            _apnsStream.Dispose();
            _apnsStream = null;
            _conected = false;
           // _eventLog.WriteEntry("Disconnected from apple server.", System.Diagnostics.EventLogEntryType.Information);
        }
        catch (Exception ex)
        {
           // _eventLog.WriteEntry(string.Format("Error while disconnecting from apple server:{0}", ex), System.Diagnostics.EventLogEntryType.Error);
        }
    }

    private bool OpenSslStream(string host, X509CertificateCollection certificates)
    {

        _apnsStream = new SslStream(_apnsClient.GetStream(), false, validateServerCertificate, SelectLocalCertificate);

        try
        {
            _apnsStream.AuthenticateAsClient(host, certificates, System.Security.Authentication.SslProtocols.Tls, false);
        }
        catch (System.Security.Authentication.AuthenticationException ex)
        {
          //  _eventLog.WriteEntry(string.Format("Error while Authenticate SSL Stream :{0}", ex), System.Diagnostics.EventLogEntryType.Error);
            return false;
        }

        if (!_apnsStream.IsMutuallyAuthenticated)
        {
          //  _eventLog.WriteEntry("SSL Stream Failed to Authenticate", System.Diagnostics.EventLogEntryType.Error);
            return false;
        }

        if (!_apnsStream.CanWrite)
        {
           // _eventLog.WriteEntry("SSL Stream is not Writable", System.Diagnostics.EventLogEntryType.Error);
            return false;
        }
        return true;
    }

    private bool validateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        return true; // Dont care about server's cert
    }

    private X509Certificate SelectLocalCertificate(object sender, string targetHost, X509CertificateCollection localCertificates, X509Certificate remoteCertificate, string[] acceptableIssuers)
    {
        return _certificate;
    }

    private byte[] GeneratePayload(NotificationPayload payload)
    {
        try
        {
            //convert Devide token to HEX value.
            byte[] deviceToken = new byte[payload.DeviceToken.Length / 2];
            for (int i = 0; i < deviceToken.Length; i++)
                deviceToken[i] = byte.Parse(payload.DeviceToken.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);

            var memoryStream = new MemoryStream();

            // Command
            memoryStream.WriteByte(1); // Changed command Type 

            //Adding ID to Payload
            memoryStream.Write(Encoding.ASCII.GetBytes(payload.PayloadId.ToString()), 0, payload.PayloadId.ToString().Length);

            //Adding ExpiryDate to Payload
            int epoch = (int)(DateTime.UtcNow.AddMinutes(300) - new DateTime(1970, 1, 1)).TotalSeconds;
            byte[] timeStamp = BitConverter.GetBytes(epoch);
            memoryStream.Write(timeStamp, 0, timeStamp.Length);

            byte[] tokenLength = BitConverter.GetBytes((Int16)32);
            Array.Reverse(tokenLength);
            // device token length
            memoryStream.Write(tokenLength, 0, 2);

            // Token
            memoryStream.Write(deviceToken, 0, 32);

            // String length
            string apnMessage = payload.ToJson();
            //Logger.Info("Payload generated for " + payload.DeviceToken + " : " + apnMessage);

            byte[] apnMessageLength = BitConverter.GetBytes((Int16)apnMessage.Length);
            Array.Reverse(apnMessageLength);

            // message length
            memoryStream.Write(apnMessageLength, 0, 2);

            // Write the message
            memoryStream.Write(Encoding.ASCII.GetBytes(apnMessage), 0, apnMessage.Length);
            return memoryStream.ToArray();
        }
        catch (Exception ex)
        {
           // _eventLog.WriteEntry(string.Format("Unable to generate payload - {0}", ex), System.Diagnostics.EventLogEntryType.Error);
            return null;
        }
    }

}

public class MyAsyncInfo
{
    public Byte[] ByteArray { get; set; }
    public SslStream MyStream { get; set; }

    public MyAsyncInfo(Byte[] array, SslStream stream)
    {
        ByteArray = array;
        MyStream = stream;
    }
}
public class NotificationPayload
{
    public NotificationAlert Alert { get; set; }

    public string DeviceToken { get; set; }

    public int? Badge { get; set; }

    public string Sound { get; set; }

    internal int PayloadId { get; set; }

    public Dictionary<string, object[]> CustomItems
    {
        get;
        private set;
    }

    public NotificationPayload(string deviceToken)
    {
        DeviceToken = deviceToken;
        Alert = new NotificationAlert();
        CustomItems = new Dictionary<string, object[]>();
    }

    public NotificationPayload(string deviceToken, string alert)
    {
        DeviceToken = deviceToken;
        Alert = new NotificationAlert() { Body = alert };
        CustomItems = new Dictionary<string, object[]>();
    }

    public NotificationPayload(string deviceToken, string alert, int badge)
    {
        DeviceToken = deviceToken;
        Alert = new NotificationAlert() { Body = alert };
        Badge = badge;
        CustomItems = new Dictionary<string, object[]>();
    }

    public NotificationPayload(string deviceToken, string alert, int badge, string sound)
    {
        DeviceToken = deviceToken;
        Alert = new NotificationAlert() { Body = alert };
        Badge = badge;
        Sound = sound;
        CustomItems = new Dictionary<string, object[]>();
    }

    public void AddCustom(string key, params object[] values)
    {
        if (values != null)
            this.CustomItems.Add(key, values);
    }

    public string ToJson()
    {
        JObject json = new JObject();

        JObject aps = new JObject();

        if (!this.Alert.IsEmpty)
        {
            if (!string.IsNullOrEmpty(this.Alert.Body)
                && string.IsNullOrEmpty(this.Alert.LocalizedKey)
                && string.IsNullOrEmpty(this.Alert.ActionLocalizedKey)
                && (this.Alert.LocalizedArgs == null || this.Alert.LocalizedArgs.Count <= 0))
            {
                aps["alert"] = new JValue(this.Alert.Body);
            }
            else
            {
                JObject jsonAlert = new JObject();

                if (!string.IsNullOrEmpty(this.Alert.LocalizedKey))
                    jsonAlert["loc-key"] = new JValue(this.Alert.LocalizedKey);

                if (this.Alert.LocalizedArgs != null && this.Alert.LocalizedArgs.Count > 0)
                    jsonAlert["loc-args"] = new JArray(this.Alert.LocalizedArgs.ToArray());

                if (!string.IsNullOrEmpty(this.Alert.Body))
                    jsonAlert["body"] = new JValue(this.Alert.Body);

                if (!string.IsNullOrEmpty(this.Alert.ActionLocalizedKey))
                    jsonAlert["action-loc-key"] = new JValue(this.Alert.ActionLocalizedKey);

                aps["alert"] = jsonAlert;
            }
        }

        if (this.Badge.HasValue)
            aps["badge"] = new JValue(this.Badge.Value);

        if (!string.IsNullOrEmpty(this.Sound))
            aps["sound"] = new JValue(this.Sound);

        json["aps"] = aps;

        foreach (string key in this.CustomItems.Keys)
        {
            if (this.CustomItems[key].Length == 1)
                json[key] = new JValue(this.CustomItems[key][0]);
            else if (this.CustomItems[key].Length > 1)
                json[key] = new JArray(this.CustomItems[key]);
        }

        string rawString = json.ToString();
        StringBuilder encodedString = new StringBuilder();
        foreach (char c in rawString)
        {
            if ((int)c < 32 || (int)c > 127)
                encodedString.Append("\\u" + String.Format("{0:x4}", Convert.ToUInt32(c)));
            else
                encodedString.Append(c);
        }
        return rawString;// encodedString.ToString();
    }

    public override string ToString()
    {
        return ToJson();
    }
}
public class NotificationAlert
{
    /// <summary>
    /// Constructor
    /// </summary>
    public NotificationAlert()
    {
        Body = null;
        ActionLocalizedKey = null;
        LocalizedKey = null;
        LocalizedArgs = new List<object>();
    }

    /// <summary>
    /// Body Text of the Notification's Alert
    /// </summary>
    public string Body
    {
        get;
        set;
    }

    /// <summary>
    /// Action Button's Localized Key
    /// </summary>
    public string ActionLocalizedKey
    {
        get;
        set;
    }

    /// <summary>
    /// Localized Key
    /// </summary>
    public string LocalizedKey
    {
        get;
        set;
    }

    /// <summary>
    /// Localized Argument List
    /// </summary>
    public List<object> LocalizedArgs
    {
        get;
        set;
    }

    public void AddLocalizedArgs(params object[] values)
    {
        this.LocalizedArgs.AddRange(values);
    }

    /// <summary>
    /// Determines if the Alert is empty and should be excluded from the Notification Payload
    /// </summary>
    public bool IsEmpty
    {
        get
        {
            if (!string.IsNullOrEmpty(Body)
                || !string.IsNullOrEmpty(ActionLocalizedKey)
                || !string.IsNullOrEmpty(LocalizedKey)
                || (LocalizedArgs != null && LocalizedArgs.Count > 0))
                return false;
            else
                return true;
        }
    }
}

// ========
// public static bool PushMessage(string message)
// {
// string deviceID = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
// int port = 2195;
// //String hostname = "gateway.sandbox.push.apple.com"; // TEST
// String hostname = "gateway.push.apple.com"; // REAL
// string certificatePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CertDevelopment.p12");


// //X509Certificate2 clientCertificate = new X509Certificate2(System.IO.File.ReadAllBytes(certificatePath), "9876543", X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.Exportable); //same error

// X509Certificate2 clientCertificate = new X509Certificate2(certificatePath, "9876543");  //same error


// X509Certificate2Collection certificatesCollection = new X509Certificate2Collection(clientCertificate);

// using (TcpClient client = new TcpClient())
// {
// client.Connect(hostname, port);
// SslStream sslStream = new SslStream(client.GetStream(), false, new RemoteCertificateValidationCallback(ValidateServerCertificate), null); //, validateServerCertificate, SelectLocalCertificate);

// try
// {
// sslStream.AuthenticateAsClient(hostname, certificatesCollection, SslProtocols.Default, false);
// }
// catch (Exception e)
// {
// client.Close();
// throw (e);
// }

// MemoryStream memoryStream = new MemoryStream();
// BinaryWriter writer = new BinaryWriter(memoryStream);
// writer.Write((byte)0);  //The command
// writer.Write((byte)0);  //The first byte of the deviceId length (big-endian first byte)
// writer.Write((byte)32); //The deviceId length (big-endian second byte)

// writer.Write(HexToData(deviceID.ToUpper()));
// String payload = "{\"aps\":{\"alert\":\"hello\",\"badge\":0,\"sound\":\"default\"}}";
// writer.Write((byte)0);
// writer.Write((byte)payload.Length);
// byte[] b1 = System.Text.Encoding.UTF8.GetBytes(payload);
// writer.Write(b1);
// writer.Flush();
// byte[] array = memoryStream.ToArray();
// sslStream.Write(array);
// sslStream.Flush();
// client.Close();
// }
// return false;
// }