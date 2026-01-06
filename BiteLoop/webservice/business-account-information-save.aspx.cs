using System.Net.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using Utility;
using Newtonsoft.Json;
using DAL;
using System.Data;

public partial class webservice_business_account_save : System.Web.UI.Page
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
            objBusinessBAL.Name = Convert.ToString(Request["Name"]);
            objBusinessBAL.ABN = Convert.ToString(Request["ABN"]);
            objBusinessBAL.StreetAddress = Convert.ToString(Request["StreetAddress"]);
            objBusinessBAL.Location = Convert.ToString(Request["Location"]);
            objBusinessBAL.FullName = Convert.ToString(Request["FullName"]);
            objBusinessBAL.EmailAddress = Convert.ToString(Request["EmailAddress"]);
            objBusinessBAL.BusinessPhone = Convert.ToString(Request["BusinessPhone"]);
            objBusinessBAL.Mobile = Convert.ToString(Request["Mobile"]);

            string LastName = "";
            if (Request["LastName"] != null)
            {
                LastName = Convert.ToString(Request["LastName"]);
            }


            if (Request["Latitude"] != null)
            {
                objBusinessBAL.Latitude = Convert.ToString(Request["Latitude"]);
            }
            else
            {
                objBusinessBAL.Latitude = string.Empty;
            }
            if (Request["Longitude"] != null)
            {
                objBusinessBAL.Longitude = Convert.ToString(Request["Longitude"]);
            }
            else
            {
                objBusinessBAL.Longitude = string.Empty;
            }

            string State = "";
            if (Request["State"] != null)
            {
                State = Convert.ToString(Request["State"]);
            }


            //long result = objBusinessBAL.BusinessAccountInformationUpdate();
            long result = BusinessAccountInformationUpdate(objBusinessBAL, LastName, State);

            switch (result)
            {
                case -1:
                    objResponse.success = "false";
                    objResponse.message = "Duplicate Email Address found.";
                    break;
                default:
                    objResponse.success = "true";
                    objResponse.message = "Details has been updated successfully.";
                    SendAdminMail(objBusinessBAL.ID);
                    break;
            }

            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));

        }
        Response.End();
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
            dt = objEmail.GetByID(8, 1);
            if (dt.Rows.Count > 0)
            {

                string strSubject = string.Empty;
                strSubject = Convert.ToString(dt.Rows[0]["Subject"]);
                System.Text.StringBuilder sbEmailTemplate = new System.Text.StringBuilder(strHeader + Convert.ToString(dt.Rows[0]["Template"]) + strFooter);

                
                sbEmailTemplate.Replace("###BusinessName###", Convert.ToString(dtBusiness.Rows[0]["Name"]) + "(" + Convert.ToString(dtBusiness.Rows[0]["VendorUniqueID"]) + ")");
                sbEmailTemplate.Replace("###ABN###", Convert.ToString(dtBusiness.Rows[0]["ABN"]));
                sbEmailTemplate.Replace("###Location###", Convert.ToString(dtBusiness.Rows[0]["Location"]));
                sbEmailTemplate.Replace("###FirstName###", Convert.ToString(dtBusiness.Rows[0]["FullName"]));
                sbEmailTemplate.Replace("###LastName###", Convert.ToString(dtBusiness.Rows[0]["LastName"]));
                sbEmailTemplate.Replace("###Phone###", Convert.ToString(dtBusiness.Rows[0]["BusinessPhone"]));
                sbEmailTemplate.Replace("###Mobile###", Convert.ToString(dtBusiness.Rows[0]["Mobile"]));
                sbEmailTemplate.Replace("###Email###", Convert.ToString(dtBusiness.Rows[0]["EmailAddress"]));

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
    private long BusinessAccountInformationUpdate(BusinessBAL objBusinessBAL, string LastName, string State)
    {
        DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.Int, 40, objBusinessBAL.ID), 
                new DbParameter("@Name", DbParameter.DbType.VarChar, 200, objBusinessBAL.Name),
                new DbParameter("@ABN", DbParameter.DbType.VarChar, 200, objBusinessBAL.ABN),
                new DbParameter("@StreetAddress", DbParameter.DbType.VarChar, 500, objBusinessBAL.StreetAddress),
                new DbParameter("@Location", DbParameter.DbType.VarChar, 500,objBusinessBAL.Location),
                new DbParameter("@FullName", DbParameter.DbType.VarChar, 200, objBusinessBAL.FullName),
                new DbParameter("@EmailAddress", DbParameter.DbType.VarChar, 200, objBusinessBAL.EmailAddress),
                new DbParameter("@BusinessPhone", DbParameter.DbType.VarChar, 20, objBusinessBAL.BusinessPhone),
                new DbParameter("@Mobile", DbParameter.DbType.VarChar, 500, objBusinessBAL.Mobile),
                new DbParameter("@Latitude", DbParameter.DbType.VarChar, 1000, objBusinessBAL.Latitude),
                new DbParameter("@Longitude", DbParameter.DbType.VarChar, 1000, objBusinessBAL.Longitude),
                new DbParameter("@LastName", DbParameter.DbType.VarChar, 1000, LastName),                
                new DbParameter("@State", DbParameter.DbType.VarChar, 100, State),                
                
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output)
            };
        DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "BusinessAccountInformationUpdate", dbParam);
        return Convert.ToInt64(dbParam[dbParam.Length - 1].Value);
    }
}