using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using Utility;
using Newtonsoft.Json;
using System.Data;
using System.Net.Mail;

public partial class webservice_business_bankinformation_save : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Keys.Count > 0)
        {
            BusinessRegistration();
        }
    }

    private void BusinessRegistration()
    {
        Response objResponse = new Response();
        bool IsValidated = false;
        if (Request["UserID"] != null && Request["SecretKey"] != null && Request["AuthToken"] != null)
        {
            if (ValidateRequestBAL.BusinessValidateClientRequest(Convert.ToInt64(Request["UserID"]), Convert.ToString(Request["SecretKey"]), Convert.ToString(Request["AuthToken"])))
            {
                IsValidated = true;
            }
        }
        if (!IsValidated)
        {
            CommonAPI objCommonAPI = new CommonAPI();
            objCommonAPI.Unauthorized();
        }
        else
        {
            BusinessBAL objBusinessBAL = new BusinessBAL();
            objBusinessBAL.ID = Convert.ToInt64(Request["BusinessID"]);
            objBusinessBAL.BSBNo = Convert.ToString(Request["BSBNo"]);
            objBusinessBAL.AccountNumber = Convert.ToString(Request["AccountNumber"]);
            objBusinessBAL.BankName = Convert.ToString(Request["BankName"]);
            objBusinessBAL.AccountName = Convert.ToString(Request["AccountName"]);

            long result = objBusinessBAL.UpdateBusinessBankDetails();

            switch (result)
            {  
                default:
                    objResponse.success = "true";
                    objResponse.message = "Details has been updated successfully.";
                    SendAdminMail(objBusinessBAL.ID);
                    break;
            }

            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));

        } Response.End();
    }

    private void SendAdminMail(long BusinessID)
    {
        //===== Business Details ===================
        DataTable dtBusiness = new DataTable();
        BusinessBAL objBusinessBAL = new BusinessBAL();
        objBusinessBAL.ID = BusinessID;
        dtBusiness = objBusinessBAL.BusinessDetailsByIDForContactEnquiry();

        if (dtBusiness.Rows.Count > 0)
        {

            string strHeader = "<!doctype html><html><head><meta charset='utf-8'><title>Registration</title><link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet'><style>body {	padding: 0px;	margin: 0px;}</style></head><body>";
            string strFooter = "</body></html>";

            EmailTemplateBAL objEmail = new EmailTemplateBAL();
            DataTable dt = new DataTable();
            dt = objEmail.GetByID(9, 1);
            if (dt.Rows.Count > 0)
            {

                string strSubject = string.Empty;
                strSubject = Convert.ToString(dt.Rows[0]["Subject"]);
                System.Text.StringBuilder sbEmailTemplate = new System.Text.StringBuilder(strHeader + Convert.ToString(dt.Rows[0]["Template"]) + strFooter);



                sbEmailTemplate.Replace("###BusinessName###", Convert.ToString(dtBusiness.Rows[0]["Name"]) + "(" + Convert.ToString(dtBusiness.Rows[0]["VendorUniqueID"]) + ")");
                sbEmailTemplate.Replace("###BSBNo###", Convert.ToString(dtBusiness.Rows[0]["BSBNo"]));
                sbEmailTemplate.Replace("###AccountNo###", Convert.ToString(dtBusiness.Rows[0]["AccountNumber"]));
                sbEmailTemplate.Replace("###Bank###", Convert.ToString(dtBusiness.Rows[0]["BankName"]));
                sbEmailTemplate.Replace("###AccountName###", Convert.ToString(dtBusiness.Rows[0]["AccountName"]));
                
                sbEmailTemplate.Replace("###siteurl###", Config.WebSiteUrl);
                sbEmailTemplate.Replace("###SiteName###", Config.WebsiteName);
                SendEmailToAdmin(new GeneralSettings().getConfigValue("abnno"), new GeneralSettings().getConfigValue("abnno"), strSubject.Replace("###WebsiteName###", Config.WebsiteName), sbEmailTemplate.ToString());

            }
        }
    }


    public bool SendEmailToAdmin(string strTo, string strFrom, string strSubject, string strBody)
    {
        bool flag = false;
        try
        {
            MailMessage message = new MailMessage();
            SmtpClient client = new SmtpClient();
            message.To.Add("jane@bringmehome.com.au");
            message.From = new MailAddress(strFrom);
            message.Bcc.Add("senthil@flexboxdigital.com.au");
            message.Subject = strSubject;
            message.Body = strBody;
            message.IsBodyHtml = true;
            message.Priority = MailPriority.Normal;
            client.Host = new GeneralSettings().getConfigValue("hostname"); //GetSmtpServer();
            client.Port = 587;
            client.EnableSsl = Convert.ToBoolean(new GeneralSettings().getConfigValue("SSL"));
            client.Credentials = new System.Net.NetworkCredential(new GeneralSettings().getConfigValue("emailaddress"), new GeneralSettings().getConfigValue("emailpassword"));
            client.Send(message);
            flag = true;
        }
        catch (Exception)
        {
            flag = false;
        }
        return flag;
    }
          
}