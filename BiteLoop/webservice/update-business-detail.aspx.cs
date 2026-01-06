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
using Twilio.Http;

public partial class webservice_update_business_detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Keys.Count > 0)
        {
            updateBusinessDetails();
        }
    }

    private void updateBusinessDetails()
    {
        Response objResponse = new Response();
        BusinessBAL objBusinessBAL = new BusinessBAL();
        DataTable dtResult = new DataTable();

        string strID = Convert.ToString(Request.Form["ID"]);
        if (string.IsNullOrEmpty(strID))
        {
            objResponse.success = "false";
            objResponse.message = "Business ID is required.";
            WriteResponse(objResponse);
            return;
        }

        long BusinessID = Convert.ToInt64(strID);
        objBusinessBAL.ID = BusinessID;

        DataTable existingData = objBusinessBAL.GetBusinessByID(BusinessID);
        if (existingData.Rows.Count == 0)
        {
            objResponse.success = "false";
            objResponse.message = "Business not found.";
            WriteResponse(objResponse);
            return;
        }

        DataRow dr = existingData.Rows[0];


        objBusinessBAL.Name = GetValueOrDefault("BusinessName", dr["Name"]);
        objBusinessBAL.ABN = GetValueOrDefault("ABN", dr["ABN"]);
        objBusinessBAL.StreetAddress = GetValueOrDefault("StreetAddress", dr["StreetAddress"]);
        objBusinessBAL.Location = GetValueOrDefault("Location", dr["Location"]);
        objBusinessBAL.FullName = GetValueOrDefault("ContactPersonName", dr["FullName"]);
        objBusinessBAL.EmailAddress = GetValueOrDefault("EmailAddress", dr["EmailAddress"]);
        string newPassword = Convert.ToString(Request["Password"]);

        if (!string.IsNullOrEmpty(newPassword))
        {

            objBusinessBAL.Password = Utility.Security.EncryptDescrypt.EncryptString(newPassword);
        }
        else
        {

            objBusinessBAL.Password = Convert.ToString(dr["Password"]);
        }

        objBusinessBAL.BusinessPhone = GetValueOrDefault("BusinessPhone", dr["BusinessPhone"]);
        objBusinessBAL.Mobile = GetValueOrDefault("Mobile", dr["Mobile"]);
        objBusinessBAL.Latitude = GetValueOrDefault("Latitude", dr["Latitude"]);
        objBusinessBAL.Longitude = GetValueOrDefault("Longitude", dr["Longitude"]);
        objBusinessBAL.PostCode = GetValueOrDefault("PostCode", dr["PostCode"]);
        //objBusinessBAL.Note = GetValueOrDefault("Note", dr["Note"]);
        string newNote = Convert.ToString(Request["Note"]);
        
        if (newNote != null)
        {
            objBusinessBAL.Note = newNote; 
        }
        else
        {
            objBusinessBAL.Note = Convert.ToString(dr["Note"]); 
        }

        objBusinessBAL.StoreName = GetValueOrDefault("StoreName", dr["StoreName"]);
        objBusinessBAL.GSTregistered = GetIntValue(Request["GSTregistered"], dr["GSTregistered"]);
        objBusinessBAL.ReceiveMarketingMails = GetIntValue(Request["ReceiveMarketingMails"], dr["ReceiveMarketingMails"]);
        objBusinessBAL.FirstName = GetValueOrDefault("FirstName", dr["First_Name"]);
        objBusinessBAL.LastName = GetValueOrDefault("LastName", dr["Last_Name"]);
        objBusinessBAL.Suburb = GetValueOrDefault("Suburb", dr["Suburb"]);
        objBusinessBAL.State = GetValueOrDefault("State", dr["State"]);


        long result = objBusinessBAL.UpdateBusinessDetails();

        if (result > 0)
        {

            DataTable dtUpdated = objBusinessBAL.GetBusinessByID(BusinessID);
            if (dtUpdated.Rows.Count > 0)
            {
                DataRow updatedRow = dtUpdated.Rows[0];


                BusinessLogin objBusinessLogin = new BusinessLogin();
                objBusinessLogin.UserID = Convert.ToInt64(updatedRow["ID"]);
                objBusinessBAL.RestaurantTypes = updatedRow.Table.Columns.Contains("RestaurantTypes")
                ? Convert.ToString(updatedRow["RestaurantTypes"])
                : string.Empty;


                objBusinessLogin.BusinessName = Convert.ToString(updatedRow["Name"]);
                objBusinessLogin.FullName = Convert.ToString(updatedRow["FullName"]);
                objBusinessLogin.EmailAddress = Convert.ToString(updatedRow["EmailAddress"]);
                objBusinessLogin.Password = Convert.ToString(updatedRow["Password"]);

                // objBusinessLogin.Password = Convert.ToString(Utility.Security.EncryptDescrypt.DecryptString(Convert.ToString(updatedRow["Password"])));
                objBusinessLogin.BusinessPhone = Convert.ToString(updatedRow["BusinessPhone"]);
                objBusinessLogin.Mobile = Convert.ToString(updatedRow["Mobile"]);
                objBusinessLogin.SecretKey = Convert.ToString(Request["SecretKey"]);


                string existingAuth = objBusinessBAL.GetAuthToken(BusinessID);
                objBusinessLogin.AuthToken = existingAuth;


                //objBusinessLogin.IsSalesAdmin = Convert.ToInt32(updatedRow["IsSalesAdmin"]);
                //objBusinessLogin.Step = Convert.ToInt32(updatedRow["OPenPageNO"]);
                //objBusinessLogin.StateID = Convert.ToInt32(updatedRow["StateID"]);
                //objBusinessLogin.StateCode = Convert.ToString(updatedRow["StateCode"]);
                //objBusinessLogin.StateFullName = Convert.ToString(updatedRow["StateName"]);
                objBusinessLogin.ABN = updatedRow.Table.Columns.Contains("ABN") ? Convert.ToString(updatedRow["ABN"]) : string.Empty;
                objBusinessLogin.StreetAddress = updatedRow.Table.Columns.Contains("StreetAddress") ? Convert.ToString(updatedRow["StreetAddress"]) : string.Empty;
                objBusinessLogin.Location = updatedRow.Table.Columns.Contains("Location") ? Convert.ToString(updatedRow["Location"]) : string.Empty;
                //objBusinessLogin.ContactPersonName = updatedRow.Table.Columns.Contains("PersonInCharge") ? Convert.ToString(updatedRow["PersonInCharge"]) : string.Empty;
                objBusinessLogin.GstVerified = updatedRow.Table.Columns.Contains("GSTregistered")
                    ? (Convert.ToBoolean(updatedRow["GSTregistered"]) ? 1 : 0)
                    : 0; objBusinessLogin.ReceiveMarketingEmail = updatedRow.Table.Columns.Contains("ReceiveMarketingMails") ? (Convert.ToBoolean(updatedRow["ReceiveMarketingMails"]) ? 1 : 0) : 0;
                objBusinessLogin.Latitude = updatedRow.Table.Columns.Contains("Latitude") ? Convert.ToString(updatedRow["Latitude"]) : string.Empty;
                objBusinessLogin.Longitude = updatedRow.Table.Columns.Contains("Longitude") ? Convert.ToString(updatedRow["Longitude"]) : string.Empty;
                objBusinessLogin.Note = updatedRow.Table.Columns.Contains("Note")
                ? Convert.ToString(updatedRow["Note"])
                : string.Empty;
                objBusinessLogin.StoreName = updatedRow.Table.Columns.Contains("StoreName")
                ? Convert.ToString(updatedRow["StoreName"])
                : string.Empty;
                objBusinessLogin.PostCode = updatedRow.Table.Columns.Contains("PostCode") ? Convert.ToString(updatedRow["PostCode"]) : string.Empty;
                objBusinessLogin.FirstName = updatedRow.Table.Columns.Contains("First_Name") ? Convert.ToString(updatedRow["First_Name"]) : string.Empty;
                objBusinessLogin.LastName = updatedRow.Table.Columns.Contains("Last_Name") ? Convert.ToString(updatedRow["Last_Name"]) : string.Empty;
                objBusinessLogin.Suburb = updatedRow.Table.Columns.Contains("Suburb") ? Convert.ToString(updatedRow["Suburb"]) : string.Empty;
                objBusinessLogin.State = updatedRow.Table.Columns.Contains("State") ? Convert.ToString(updatedRow["State"]) : string.Empty;


                //DataTable dtLogin = objBusinessBAL.BusinessLoginCheck("", "A");
                DataTable dtLogin = objBusinessBAL.GetBusinessByID(BusinessID);

                bool isProfile = false;

                if (dtLogin.Rows.Count > 0)
                {
                    bool isProfileComplete =
                        !string.IsNullOrEmpty(Convert.ToString(dtLogin.Rows[0]["RestaurantTypesValues"])) &&
                        !string.IsNullOrEmpty(Convert.ToString(dtLogin.Rows[0]["Description"])) &&
                        !string.IsNullOrEmpty(Convert.ToString(dtLogin.Rows[0]["BSBNo"])) &&
                        !string.IsNullOrEmpty(Convert.ToString(dtLogin.Rows[0]["AccountNumber"])) &&
                        !string.IsNullOrEmpty(Convert.ToString(dtLogin.Rows[0]["BankName"])) &&
                        !string.IsNullOrEmpty(Convert.ToString(dtLogin.Rows[0]["AccountName"]));

                    isProfile = isProfileComplete;
                }

                objBusinessLogin.IsProfileDetails = isProfile;


                objResponse.success = "true";
                objResponse.message = "Business details updated successfully.";
                objResponse.BusinessLogin = objBusinessLogin;

                string strResponse = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

                strResponse = strResponse.Replace("\"BusinessLogin\"", "\"data\"");
                Response.Clear();
                Response.StatusCode = (int)HttpStatusCode.OK;
                Response.ContentType = "application/json";
                Response.Write(strResponse);
                Response.End();
                return;
            }
        }
        else
        {
            objResponse.success = "false";
            objResponse.message = "Failed to update business details.";
            WriteResponse(objResponse);
        }
    }

    //private void updateBusinessDetails()
    //{ 
    //    Response objResponse = new Response();
    //    BusinessBAL objBusinessBAL = new BusinessBAL();
    //    DataTable dtResult = new DataTable();

    //    string strID = Convert.ToString(Request.Form["ID"]);
    //    if (string.IsNullOrEmpty(strID))
    //    { 
    //        objResponse.success = "false";
    //        objResponse.message = "Business ID is required.";
    //        WriteResponse(objResponse);
    //        return;
    //    }

    //    long BusinessID = Convert.ToInt64(strID);
    //    objBusinessBAL.ID = BusinessID;

    //    DataTable existingData = objBusinessBAL.GetBusinessByID(BusinessID);
    //    if (existingData.Rows.Count == 0)
    //    {
    //        objResponse.success = "false";
    //        objResponse.message = "Business not found.";
    //        WriteResponse(objResponse);
    //        return;
    //    }

    //    DataRow dr = existingData.Rows[0];

    //    objBusinessBAL.Name = GetValueOrDefault("BusinessName", dr["Name"]);
    //    objBusinessBAL.ABN = GetValueOrDefault("ABN", dr["ABN"]);
    //    objBusinessBAL.StreetAddress = GetValueOrDefault("StreetAddress", dr["StreetAddress"]);
    //    objBusinessBAL.Location = GetValueOrDefault("Location", dr["Location"]);
    //    objBusinessBAL.FullName = GetValueOrDefault("ContactPersonName", dr["FullName"]);
    //    objBusinessBAL.EmailAddress = GetValueOrDefault("EmailAddress", dr["EmailAddress"]);
    //    objBusinessBAL.BusinessPhone = GetValueOrDefault("BusinessPhone", dr["BusinessPhone"]);
    //    objBusinessBAL.Mobile = GetValueOrDefault("Mobile", dr["Mobile"]);
    //    objBusinessBAL.Latitude = GetValueOrDefault("Latitude", dr["Latitude"]);
    //    objBusinessBAL.Longitude = GetValueOrDefault("Longitude", dr["Longitude"]);
    //    objBusinessBAL.PostCode = GetValueOrDefault("PostCode", dr["PostCode"]);

    //    objBusinessBAL.GSTregistered = GetIntValue(Request["GSTregistered"], dr["GSTregistered"]);
    //    objBusinessBAL.ReceiveMarketingMails = GetIntValue(Request["ReceiveMarketingMails"], dr["ReceiveMarketingMails"]);


    //    long result = objBusinessBAL.UpdateBusinessDetails();

    //    if (result > 0)
    //    {
    //        objResponse.success = "true";
    //        objResponse.message = "Business details updated successfully.";
    //    }
    //    else
    //    {
    //        objResponse.success = "false";
    //        objResponse.message = "Failed to update business details.";
    //    }

    //    WriteResponse(objResponse);
    //}

    private string GetValueOrDefault(string requestKey, object existingValue)
    {
        return !string.IsNullOrEmpty(Convert.ToString(Request[requestKey]))
            ? Convert.ToString(Request[requestKey])
            : Convert.ToString(existingValue);
    }

    private void WriteResponse(object responseObj)
    {
        Response.Clear();
        Response.StatusCode = (int)HttpStatusCode.OK;
        Response.ContentType = "application/json";
        Response.Write(JsonConvert.SerializeObject(responseObj, new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore
        }));
        Response.End();
    }

    //private void WriteResponse(Response objResponse)
    //{
    //    Response.Clear();
    //    Response.StatusCode = (int)HttpStatusCode.OK;
    //    Response.ContentType = "application/json";
    //    Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings()
    //    {
    //        NullValueHandling = NullValueHandling.Ignore
    //    }));

    //    Response.End(); // Forces response to stop — no HTML appended
    //}


    //private void WriteResponse(Response objResponse)
    //{
    //    Response.StatusCode = (int)HttpStatusCode.OK;
    //    HttpContext.Current.Response.ContentType = "application/json";
    //    HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings()
    //    {
    //        NullValueHandling = NullValueHandling.Ignore
    //    }));
    //}

    private int GetIntValue(object requestValue, object dbValue)
    {
        int result;

        if (requestValue != null && int.TryParse(requestValue.ToString(), out result))
            return result;

        if (dbValue != null && int.TryParse(dbValue.ToString(), out result))
            return result;

        return 0;
    }
}