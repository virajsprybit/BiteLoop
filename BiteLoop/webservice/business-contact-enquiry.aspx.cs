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

public partial class webservice_business_contact_enquiry : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Keys.Count > 0)
        {
            BusinessContactEnquiry();
        }
    }

    private void BusinessContactEnquiry()
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
            HttpContext.Current.Response.End();
        }
        else
        {
            ContactUsBAL objContactUsBAL = new ContactUsBAL();
            objContactUsBAL.ID = 0;
            objContactUsBAL.BusinessID = Convert.ToInt64(Request["BusinessID"]);
            objContactUsBAL.Name = Convert.ToString(Request["Name"]);
            objContactUsBAL.Phone = Convert.ToString(Request["Phone"]);
            objContactUsBAL.Subject = Convert.ToString(Request["Subject"]);
            objContactUsBAL.EmailAddress = Convert.ToString(Request["Email"]);
            objContactUsBAL.Comments = Convert.ToString(Request["Message"]);

            long result = objContactUsBAL.Save();
            switch (result)
            {
                default:
                    SendUserMail(Convert.ToString(Request["Email"]), Convert.ToString(Request["Name"]));
                    SendAdminMail(Convert.ToInt64(Request["BusinessID"]));
                    objResponse.success = "true";
                    objResponse.message = "Contact enquiry has been submitted successfully.";
                    break;
            }

            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));

        }
        HttpContext.Current.Response.End();
    }

    private void SendUserMail(string EmailAddress, string Name)
    {
        string strHeader = "<!doctype html><html><head><meta charset='utf-8'><title>Registration</title><link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet'><style>body {	padding: 0px;	margin: 0px;}</style></head><body>";
        string strFooter = "</body></html>";

        EmailTemplateBAL objEmail = new EmailTemplateBAL();
        DataTable dt = new DataTable();
        dt = objEmail.GetByID(4, 1);
        if (dt.Rows.Count > 0)
        {

            string strSubject = string.Empty;
            strSubject = Convert.ToString(dt.Rows[0]["Subject"]);
            System.Text.StringBuilder sbEmailTemplate = new System.Text.StringBuilder(strHeader + Convert.ToString(dt.Rows[0]["Template"]) + strFooter);

            sbEmailTemplate.Replace("###Name###", Name);
            sbEmailTemplate.Replace("###siteurl###", Config.WebSiteUrl);
            sbEmailTemplate.Replace("###SiteName###", Config.WebsiteName);
            if (EmailAddress != string.Empty)
            {
                GeneralSettings.SendEmail(EmailAddress, new GeneralSettings().getConfigValue("abnno"), strSubject.Replace("###WebsiteName###", Config.WebsiteName), sbEmailTemplate.ToString());
            }
        }
    }
    private void SendAdminMail(long VendorID)
    {
        string strVendorName = string.Empty;
        DataTable dtVendor = new DataTable();
        BusinessBAL objB = new BusinessBAL();
        objB.ID = VendorID;
        dtVendor = objB.BusinessDetailsByIDForContactEnquiry();
        if (dtVendor.Rows.Count > 0)
        {
            strVendorName = Convert.ToString(dtVendor.Rows[0]["Name"]);
        }

        string strHeader = "<!doctype html><html><head><meta charset='utf-8'><title>Registration</title><link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet'><style>body {	padding: 0px;	margin: 0px;}</style></head><body>";
        string strFooter = "</body></html>";

        EmailTemplateBAL objEmail = new EmailTemplateBAL();
        DataTable dt = new DataTable();
        dt = objEmail.GetByID(5, 1);
        if (dt.Rows.Count > 0)
        {

            string strSubject = string.Empty;
            strSubject = Convert.ToString(dt.Rows[0]["Subject"]);
            System.Text.StringBuilder sbEmailTemplate = new System.Text.StringBuilder(strHeader + Convert.ToString(dt.Rows[0]["Template"]) + strFooter);

            sbEmailTemplate.Replace("###BusinessName###", strVendorName);
            sbEmailTemplate.Replace("###WebsiteName###", Convert.ToString(Config.WebsiteName));
            sbEmailTemplate.Replace("###Name###", Convert.ToString(Request["Name"]));
            sbEmailTemplate.Replace("###Email###", Convert.ToString(Request["Email"]));
            sbEmailTemplate.Replace("###Phone###", Convert.ToString(Request["Phone"]));
            sbEmailTemplate.Replace("###Subject###", Convert.ToString(Request["Subject"]));
            sbEmailTemplate.Replace("###Message###", Convert.ToString(Request["Message"]));


            sbEmailTemplate.Replace("###siteurl###", Config.WebSiteUrl);
            sbEmailTemplate.Replace("###SiteName###", Config.WebsiteName);

            GeneralSettings.SendEmail(new GeneralSettings().getConfigValue("abnno"), new GeneralSettings().getConfigValue("abnno"), strSubject.Replace("###WebsiteName###", Config.WebsiteName), sbEmailTemplate.ToString());
           // GeneralSettings.SendEmail("jane@bringmehome.com.au", new GeneralSettings().getConfigValue("abnno"), strSubject.Replace("###WebsiteName###", Config.WebsiteName), sbEmailTemplate.ToString());
            
        }
    }

}
