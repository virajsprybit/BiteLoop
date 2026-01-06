using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using Utility;
using System.Data;
using Newtonsoft.Json;

public partial class webservice_business_login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Keys.Count > 0)
        {
            BusinessLogin();
        }
    }
    private void WriteLogFile(string LogMessage)
    {
        try
        {

            string path = System.Web.Hosting.HostingEnvironment.MapPath("~/source") + "\\BusinessLogFileLogin.txt";
            if (!System.IO.File.Exists(path))
            {
                using (System.IO.StreamWriter sw = System.IO.File.CreateText(path))
                {
                    sw.WriteLine(LogMessage);
                }
            }
            else
            {
                using (System.IO.StreamWriter sw = System.IO.File.AppendText(path))
                {
                    sw.WriteLine(LogMessage);
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void BusinessLogin()
    {
        if (Request["Email"] != null && Request["Password"] != null)
        {
            Response objResponse = new Response();

            BusinessBAL objBusinessBAL = new BusinessBAL();
            objBusinessBAL.EmailAddress = Convert.ToString(Request["Email"]);
            objBusinessBAL.DeviceToken = Convert.ToString(Request["DeviceToken"]);
            objBusinessBAL.Password = Convert.ToString(Utility.Security.EncryptDescrypt.EncryptString(Convert.ToString(Request["Password"])));

            DataTable dt = new DataTable();
            string strDeviceKey = "";
            if (Request["DeviceKey"] != null)
            {
                strDeviceKey = Convert.ToString(Request["DeviceKey"]);
            }
            string strDeviceType = "A";
            if (Request["DeviceType"] != null)
            {
                strDeviceType = Convert.ToString(Request["DeviceType"]);
            }


            string strKeys = "";
            foreach (string key in Request.Form.Keys)
            {
                strKeys += key + ": " + Convert.ToString(Request.Form[key]) + "\n";
            }


            WriteLogFile(strKeys + "\n\n");

            dt = objBusinessBAL.BusinessLoginCheck(strDeviceKey, strDeviceType);
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToString(dt.Rows[0]["UserActive"]) == "0")
                {
                    objResponse.success = "false";
                    objResponse.message = "Your business is not activated. Please contact admin for further information.";
                    HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));

                }
                else
                {

                    objResponse.success = "true";
                    objResponse.message = "Login Successfull.";
                    BusinessLogin[] objBusinessLogin = new BusinessLogin[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        objBusinessLogin[i] = new BusinessLogin();
                        objBusinessLogin[i].UserID = Convert.ToInt64(dt.Rows[i]["ID"]);
                        objBusinessLogin[i].BusinessName = Convert.ToString(dt.Rows[i]["Name"]);
                        string FullName = Convert.ToString(dt.Rows[i]["FullName"]);
                        if (Convert.ToString(dt.Rows[i]["LastName"]) != "")
                        {
                            FullName = FullName + " " + Convert.ToString(dt.Rows[i]["LastName"]);
                        }
                        objBusinessLogin[i].FullName = FullName;


                        objBusinessLogin[i].EmailAddress = Convert.ToString(dt.Rows[i]["EmailAddress"]);
                        objBusinessLogin[i].ABN = Convert.ToString(dt.Rows[i]["ABN"]);
                        objBusinessLogin[i].BusinessPhone = Convert.ToString(dt.Rows[i]["BusinessPhone"]);
                        objBusinessLogin[i].Mobile = Convert.ToString(dt.Rows[i]["Mobile"]);
                        objBusinessLogin[i].AuthToken = Convert.ToString(dt.Rows[i]["AuthTokenDetails"]);
                        objBusinessLogin[i].SecretKey = Convert.ToString(Utility.Security.Rijndael128Algorithm.EncryptString(Convert.ToString(dt.Rows[i]["ID"])));
                        objBusinessLogin[i].IsSalesAdmin = Convert.ToInt32(dt.Rows[i]["IsSalesAdmin"]);
                        objBusinessLogin[i].Step = Convert.ToInt32(dt.Rows[i]["OPenPageNO"]);

                        objBusinessLogin[i].StateID = Convert.ToInt32(dt.Rows[i]["StateID"]);
                        objBusinessLogin[i].StateCode = Convert.ToString(dt.Rows[i]["StateCode"]);
                        objBusinessLogin[i].StateFullName = Convert.ToString(dt.Rows[i]["StateName"]);
                        objBusinessLogin[i].PostCode = Convert.ToString(dt.Rows[i]["PostCode"]);

                        // Check if all profile details are filled
                        bool isProfileComplete =
                            !string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["RestaurantTypesValues"])) &&
                            //!string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["ProfilePhotoID"])) &&
                            //!string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["ProfilePhoto"])) &&
                            !string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["Description"])) &&
                            !string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["BSBNo"])) &&
                            !string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["AccountNumber"])) &&
                            !string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["BankName"])) &&
                            !string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["AccountName"]));

                        objBusinessLogin[i].IsProfileDetails = isProfileComplete;

                        // Map GST and Marketing Email preferences from DB
                        objBusinessLogin[i].GstVerified = dt.Columns.Contains("GSTregistered")
                            ? Convert.ToInt32(dt.Rows[i]["GSTregistered"])
                            : 0;

                        objBusinessLogin[i].ReceiveMarketingEmail = dt.Columns.Contains("ReceiveMarketingMails")
                            ? Convert.ToInt32(dt.Rows[i]["ReceiveMarketingMails"])
                            : 0;
                        objBusinessLogin[i].Note = dt.Columns.Contains("Note")
                            ? Convert.ToString(dt.Rows[i]["Note"])
                            : "";
                        objBusinessLogin[i].StoreName = dt.Columns.Contains("StoreName")
                            ? Convert.ToString(dt.Rows[i]["StoreName"])
                            : "";
                        objBusinessLogin[i].Latitude = dt.Columns.Contains("Latitude")
                            ? Convert.ToString(dt.Rows[i]["Latitude"])
                            : "";
                        objBusinessLogin[i].Longitude = dt.Columns.Contains("Longitude")
                            ? Convert.ToString(dt.Rows[i]["Longitude"])
                            : "";
                        objBusinessLogin[i].FirstName = dt.Columns.Contains("FirstName")
                            ? Convert.ToString(dt.Rows[i]["FirstName"])
                            : "";
                        objBusinessLogin[i].LastName = dt.Columns.Contains("LastName")
                            ? Convert.ToString(dt.Rows[i]["LastName"])
                            : "";
                        objBusinessLogin[i].Suburb = dt.Columns.Contains("Suburb")
                            ? Convert.ToString(dt.Rows[i]["Suburb"])
                            : "";
                        objBusinessLogin[i].State = dt.Columns.Contains("State")
                            ? Convert.ToString(dt.Rows[i]["State"])
                            : "";
                        //objResponse.BusinessLogin = objBusinessLogin;
                        objResponse.BusinessLogin = objBusinessLogin[0];

                        //objBusinessLogin[i].StateID = Convert.ToInt32(dt.Rows[i]["StateID"]);
                        //objBusinessLogin[i].StateCode = Convert.ToString(dt.Rows[i]["StateCode"]);
                        //objBusinessLogin[i].StateFullName = Convert.ToString(dt.Rows[i]["StateName"]);


                        //objResponse.BusinessLogin = objBusinessLogin;
                    }

                    //string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                    //strResponseName = strResponseName.Replace("\"BusinessLogin\"", "\"data\"");
                    //HttpContext.Current.Response.Write(strResponseName);
                    string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings()
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });
                    strResponseName = strResponseName.Replace("\"BusinessLogin\"", "\"data\"");

                    HttpContext.Current.Response.Write(strResponseName);
                }
            }
            else
            {
                CommonAPI objCommonAPI = new CommonAPI();
                objCommonAPI.InvalidLogin();
            }
        }
        else
        {
            CommonAPI objCommonAPI = new CommonAPI();
            objCommonAPI.Unauthorized();
        }
        HttpContext.Current.Response.End();
    }
}