using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
//using Twilio;
//using Twilio.Rest.Api.V2010.Account;

public partial class TwillioTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Find your Account Sid and Token at twilio.com/console
        // and set the environment variables. See http://twil.io/secure

        

        string accountSid = "AC934ac048716d104d44ba10ce6382e312";
        string authToken = "269e29fd63ea6abe925f1c8f091f1be1";
       
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

        TwilioClient.Init(accountSid, authToken);

        var message = MessageResource.Create(
            body: "Hi there!",
            from: new Twilio.Types.PhoneNumber("+14703945890"),
            to: new Twilio.Types.PhoneNumber("+61452441382")
        );

        Console.WriteLine(message.Sid);
    }
}