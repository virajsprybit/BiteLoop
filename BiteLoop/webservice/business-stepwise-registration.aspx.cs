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
using DAL;
using BiteLoop.Common;
using System.IO;

public partial class webservice_business_stepwise_registration : System.Web.UI.Page
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
        string strResponseName = "";
        BusinessCommonBAL objBusinessCommonBAL = new BusinessCommonBAL();

        bool IsValidated = true;

        if (!IsValidated)
        {
            CommonAPI objCommonAPI = new CommonAPI();
            objCommonAPI.Unauthorized();
        }
        else
        {
            if (Request["StepNo"] == null)
            {
                objResponse.success = "false";
                objResponse.message = "Invalid API Call.";
            }
            else
            {

                if (Convert.ToString(Request["StepNo"]) == "1")
                {
                    #region Step 1 Registration

                    string strPass = string.Empty;
                    objBusinessCommonBAL.EmailAddress = Convert.ToString(Request["EmailAddress"]);
                    strPass = Convert.ToString(Request["Password"]);
                    objBusinessCommonBAL.Password = Utility.Security.EncryptDescrypt.EncryptString(Convert.ToString(Request["Password"]));
                    string DeviceKey = "";
                    if (Request["DeviceKey"] != null)
                    {
                        DeviceKey = Convert.ToString(Request["DeviceKey"]);
                    }
                    string DeviceType = "";
                    if (Request["DeviceType"] != null)
                    {
                        DeviceType = Convert.ToString(Request["DeviceType"]);
                    }


                    long result = objBusinessCommonBAL.BusinessRegistrationStep1();

                    // Send email of Password and welcome

                    switch (result)
                    {
                        case -1:
                            objResponse.success = "false";
                            objResponse.message = "This email address already exists.";
                            strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                            break;
                        case 0:
                            objResponse.success = "false";
                            objResponse.message = "Please try after some time.";
                            strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                            break;
                        default:
                            SendUserMail(objBusinessCommonBAL.EmailAddress, objBusinessCommonBAL.Name, strPass);
                            objResponse.success = "true";
                            objResponse.message = "Registration has been completed successfully.";

                            DataTable dt = new DataTable();

                            dt = objBusinessCommonBAL.BusinessDetailsForRegistration(result, DeviceKey, DeviceType);

                            BusinessLogin[] objBusinessLogin = new BusinessLogin[dt.Rows.Count];
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                objBusinessLogin[i] = new BusinessLogin();
                                objBusinessLogin[i].UserID = Convert.ToInt64(dt.Rows[i]["ID"]);
                                objBusinessLogin[i].BusinessName = Convert.ToString(dt.Rows[i]["Name"]);
                                objBusinessLogin[i].FullName = Convert.ToString(dt.Rows[i]["FullName"]);
                                objBusinessLogin[i].EmailAddress = Convert.ToString(dt.Rows[i]["EmailAddress"]);
                                objBusinessLogin[i].BusinessPhone = Convert.ToString(dt.Rows[i]["BusinessPhone"]);
                                objBusinessLogin[i].Mobile = Convert.ToString(dt.Rows[i]["Mobile"]);
                                objBusinessLogin[i].AuthToken = Convert.ToString(dt.Rows[i]["AuthTokenDetails"]);
                                objBusinessLogin[i].SecretKey = Convert.ToString(Utility.Security.Rijndael128Algorithm.EncryptString(Convert.ToString(dt.Rows[i]["ID"])));
                                objBusinessLogin[i].IsSalesAdmin = Convert.ToInt32(dt.Rows[i]["IsSalesAdmin"]);

                                objBusinessLogin[i].Step = Convert.ToInt32(dt.Rows[i]["OPenPageNO"]);

                              //  objResponse.BusinessLogin = objBusinessLogin;
                            }

                            strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                            strResponseName = strResponseName.Replace("\"BusinessLogin\"", "\"data\"");
                            break;
                    }

                    #endregion
                }
                else if (Convert.ToString(Request["StepNo"]) == "2")
                {
                    #region Step 2
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
                        objBusinessCommonBAL.ID = Convert.ToInt64(Request["BusinessID"]);
                        string FirstName = "";
                        string LastName = "";
                        string Mobile = "";
                        if (Request["FirstName"] != null)
                        {
                            FirstName = Convert.ToString(Request["FirstName"]);
                        }
                        if (Request["LastName"] != null)
                        {
                            LastName = Convert.ToString(Request["LastName"]);
                        }
                        if (Request["Mobile"] != null)
                        {
                            Mobile = Convert.ToString(Request["Mobile"]);
                        }
                        long result = objBusinessCommonBAL.BusinessRegistrationStep2(FirstName, LastName, Mobile);

                        switch (result)
                        {
                            case 0:
                                objResponse.success = "false";
                                objResponse.message = "Please try after some time.";
                                strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                                break;
                            default:
                                objResponse.success = "true";
                                objResponse.message = "Personal Details saved successfully.";

                                // Get Step 3 Values ------------------------------------------------------------------------------

                                DataTable dt = new DataTable();
                                dt = objBusinessCommonBAL.BusinessDetailsByIDForStepRegistration(objBusinessCommonBAL.ID, 3);
                                BusinessRegistrationStep3[] objBusinessRegistrationStep3 = new BusinessRegistrationStep3[dt.Rows.Count];
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    objBusinessRegistrationStep3[i] = new BusinessRegistrationStep3();
                                    objBusinessRegistrationStep3[i].BusinessName = Convert.ToString(dt.Rows[i]["BusinessName"]);
                                    objBusinessRegistrationStep3[i].Location = Convert.ToString(dt.Rows[i]["Location"]);
                                    objBusinessRegistrationStep3[i].State = Convert.ToString(dt.Rows[i]["State"]);
                                    objBusinessRegistrationStep3[i].Phone = Convert.ToString(dt.Rows[i]["Phone"]);
                                    objBusinessRegistrationStep3[i].StoreManagerName = Convert.ToString(dt.Rows[i]["StoreManagerName"]);
                                    objBusinessRegistrationStep3[i].MultipleStore = Convert.ToInt32(dt.Rows[i]["MultipleStore"]);

                                    objResponse.BusinessRegistrationStep3 = objBusinessRegistrationStep3;
                                }
                                strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                                strResponseName = strResponseName.Replace("\"BusinessRegistrationStep3\"", "\"data\"");
                                //----------------------------------------------------------------------------------------------

                                break;
                        }
                    }
                    #endregion
                }
                else if (Convert.ToString(Request["StepNo"]) == "3")
                {
                    #region Step 3

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
                        objBusinessCommonBAL.ID = Convert.ToInt64(Request["BusinessID"]);
                        string BusinessName = "";
                        string Location = "";
                        string Phone = "";
                        string StoreManagerName = "";
                        int MultipleStore = 0;
                        string State = "";
                        if (Request["BusinessName"] != null)
                        {
                            BusinessName = Convert.ToString(Request["BusinessName"]);
                        }
                        if (Request["Location"] != null)
                        {
                            Location = Convert.ToString(Request["Location"]);
                        }
                        if (Request["State"] != null)
                        {
                            State = Convert.ToString(Request["State"]);
                        }
                        if (Request["Phone"] != null)
                        {
                            Phone = Convert.ToString(Request["Phone"]);
                        }
                        if (Request["StoreManagerName"] != null)
                        {
                            StoreManagerName = Convert.ToString(Request["StoreManagerName"]);
                        }
                        if (Request["MultipleStore"] != null)
                        {
                            MultipleStore = Convert.ToInt32(Request["MultipleStore"]);
                        }
                        WriteLogFile(objBusinessCommonBAL.ID.ToString() + " -> " + State);

                        long result = objBusinessCommonBAL.BusinessRegistrationStep3(BusinessName, Location, Phone, StoreManagerName, MultipleStore, State);

                        switch (result)
                        {
                            case 0:
                                objResponse.success = "false";
                                objResponse.message = "Please try after some time.";
                                strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                                break;
                            default:
                                objResponse.success = "true";
                                objResponse.message = "Business Details saved successfully.";


                                // Get Step 4 Values ------------------------------------------------------------------------------

                                DataTable dt = new DataTable();
                                dt = objBusinessCommonBAL.BusinessDetailsByIDForStepRegistration(objBusinessCommonBAL.ID, 4);
                                BusinessRegistrationStep4[] objBusinessRegistrationStep4 = new BusinessRegistrationStep4[dt.Rows.Count];
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    objBusinessRegistrationStep4[i] = new BusinessRegistrationStep4();
                                    string strBusinessTypes = "";
                                    if (Convert.ToString(dt.Rows[i]["BusinessTypes"]) != "")
                                    {
                                        strBusinessTypes = Convert.ToString(dt.Rows[i]["BusinessTypes"]);
                                        strBusinessTypes = strBusinessTypes.Substring(0, strBusinessTypes.Length - 1);
                                    }
                                    objBusinessRegistrationStep4[i].BusinessTypes = strBusinessTypes;
                                    if (strBusinessTypes == "")
                                        objBusinessRegistrationStep4[i].BYOContainers = -1;
                                    else
                                        objBusinessRegistrationStep4[i].BYOContainers = Convert.ToInt32(dt.Rows[i]["BYoContainer"]);
                                    objResponse.BusinessRegistrationStep4 = objBusinessRegistrationStep4;
                                }
                                strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                                strResponseName = strResponseName.Replace("\"BusinessRegistrationStep4\"", "\"data\"");
                                //----------------------------------------------------------------------------------------------

                                break;
                        }
                    }
                    #endregion
                }
                else if (Convert.ToString(Request["StepNo"]) == "4")
                {
                    #region Step 4

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
                        objBusinessCommonBAL.ID = Convert.ToInt64(Request["BusinessID"]);
                        string BusinessTypes = "";
                        int BYOContainers = 0;

                        if (Request["BusinessTypes"] != null)
                        {
                            BusinessTypes = Convert.ToString(Request["BusinessTypes"]);
                        }
                        if (Request["BYOContainers"] != null)
                        {
                            BYOContainers = Convert.ToInt32(Request["BYOContainers"]);
                        }

                        long result = objBusinessCommonBAL.BusinessRegistrationStep4(BusinessTypes, BYOContainers);

                        switch (result)
                        {
                            case 0:
                                objResponse.success = "false";
                                objResponse.message = "Please try after some time.";
                                strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                                break;
                            default:
                                objResponse.success = "true";
                                objResponse.message = "Business Type Details saved successfully.";


                                // Get Step 5 Values ------------------------------------------------------------------------------

                                DataTable dt = new DataTable();
                                dt = objBusinessCommonBAL.BusinessDetailsByIDForStepRegistration(objBusinessCommonBAL.ID, 5);
                                BusinessRegistrationStep5[] objBusinessRegistrationStep5 = new BusinessRegistrationStep5[dt.Rows.Count];
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    objBusinessRegistrationStep5[i] = new BusinessRegistrationStep5();
                                    string strFoodTypes = "";
                                    if (Convert.ToString(dt.Rows[i]["FoodTypes"]) != "")
                                    {
                                        strFoodTypes = Convert.ToString(dt.Rows[i]["FoodTypes"]);
                                        strFoodTypes = strFoodTypes.Substring(0, strFoodTypes.Length - 1);
                                    }
                                    objBusinessRegistrationStep5[i].FoodTypes = strFoodTypes;
                                    objResponse.BusinessRegistrationStep5 = objBusinessRegistrationStep5;
                                }
                                strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                                strResponseName = strResponseName.Replace("\"BusinessRegistrationStep5\"", "\"data\"");
                                //----------------------------------------------------------------------------------------------


                                break;
                        }
                    }
                    #endregion
                }
                else if (Convert.ToString(Request["StepNo"]) == "5")
                {
                    #region Step 5

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
                        objBusinessCommonBAL.ID = Convert.ToInt64(Request["BusinessID"]);
                        string FoodTypes = "";

                        if (Request["FoodTypes"] != null)
                        {
                            FoodTypes = Convert.ToString(Request["FoodTypes"]);
                        }

                        long result = objBusinessCommonBAL.BusinessRegistrationStep5(FoodTypes);

                        switch (result)
                        {
                            case 0:
                                objResponse.success = "false";
                                objResponse.message = "Please try after some time.";
                                strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                                break;
                            default:
                                objResponse.success = "true";
                                objResponse.message = "Food Types saved successfully.";
                                strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                                break;
                        }
                    }
                    #endregion
                }
                else if (Convert.ToString(Request["StepNo"]) == "6")
                {
                    #region Step 6

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
                        objBusinessCommonBAL.ID = Convert.ToInt64(Request["BusinessID"]);
                        string FoodTypes = "";

                        #region Days
                        string MondayFromTime = "";
                        string MondayToTime = "";

                        string TuesdayFromTime = "";
                        string TuesdayToTime = "";

                        string WednesdayFromTime = "";
                        string WednesdayToTime = "";

                        string ThirsdayFromTime = "";
                        string ThirsdayToTime = "";

                        string FridayFromTime = "";
                        string FridayToTime = "";

                        string SaturdayFromTime = "";
                        string SaturdayToTime = "";


                        string SundayFromTime = "";
                        string SundayToTime = "";

                        string NoOfMeals = Convert.ToString(Request["NoOfMeals"]);
                        string OriginalPrice = Convert.ToString(Request["OriginalPrice"]);
                        string DiscountedPrice = Convert.ToString(Request["DiscountedPrice"]);

                        #endregion





                        if (!string.IsNullOrEmpty(Request["MondayFromTime"]) && !string.IsNullOrEmpty(Request["MondayToTime"]))
                        {
                            MondayFromTime = "01/01/1990 " + Convert.ToString(Request["MondayFromTime"]);
                            MondayToTime = "01/01/1990 " + Convert.ToString(Request["MondayToTime"]);
                        }

                        if (!string.IsNullOrEmpty(Request["TuesdayFromTime"]) && !string.IsNullOrEmpty(Request["TuesdayToTime"]))
                        {
                            TuesdayFromTime = "01/01/1990 " + Convert.ToString(Request["TuesdayFromTime"]);
                            TuesdayToTime = "01/01/1990 " + Convert.ToString(Request["TuesdayToTime"]);
                        }

                        if (!string.IsNullOrEmpty(Request["WednesdayFromTime"]) && !string.IsNullOrEmpty(Request["WednesdayToTime"]))
                        {
                            WednesdayFromTime = "01/01/1990 " + Convert.ToString(Request["WednesdayFromTime"]);
                            WednesdayToTime = "01/01/1990 " + Convert.ToString(Request["WednesdayToTime"]);
                        }


                        if (!string.IsNullOrEmpty(Request["ThirsdayFromTime"]) && !string.IsNullOrEmpty(Request["ThirsdayToTime"]))
                        {
                            ThirsdayFromTime = "01/01/1990 " + Convert.ToString(Request["ThirsdayFromTime"]);
                            ThirsdayToTime = "01/01/1990 " + Convert.ToString(Request["ThirsdayToTime"]);
                        }
                        if (!string.IsNullOrEmpty(Request["FridayFromTime"]) && !string.IsNullOrEmpty(Request["FridayToTime"]))
                        {
                            FridayFromTime = "01/01/1990 " + Convert.ToString(Request["FridayFromTime"]);
                            FridayToTime = "01/01/1990 " + Convert.ToString(Request["FridayToTime"]);
                        }
                        if (!string.IsNullOrEmpty(Request["SaturdayFromTime"]) && !string.IsNullOrEmpty(Request["SaturdayToTime"]))
                        {
                            SaturdayFromTime = "01/01/1990 " + Convert.ToString(Request["SaturdayFromTime"]);
                            SaturdayToTime = "01/01/1990 " + Convert.ToString(Request["SaturdayToTime"]);
                        }
                        if (!string.IsNullOrEmpty(Request["SundayFromTime"]) && !string.IsNullOrEmpty(Request["SundayToTime"]))
                        {
                            SundayFromTime = "01/01/1990 " + Convert.ToString(Request["SundayFromTime"]);
                            SundayToTime = "01/01/1990 " + Convert.ToString(Request["SundayToTime"]);
                        }

                        string Discount = "0";
                        if (OriginalPrice != "" && DiscountedPrice != "")
                        {
                            Discount = Convert.ToDouble(Convert.ToDouble(OriginalPrice) - Convert.ToDouble(DiscountedPrice)).ToString("f2");
                        }


                        long result = objBusinessCommonBAL.BusinessRegistrationStep6(MondayFromTime, MondayToTime, TuesdayFromTime, TuesdayToTime,
                        WednesdayFromTime, WednesdayToTime, ThirsdayFromTime, ThirsdayToTime,
                        FridayFromTime, FridayToTime, SaturdayFromTime, SaturdayToTime, SundayFromTime,
                        SundayToTime, NoOfMeals, OriginalPrice, Discount);

                        switch (result)
                        {
                            case 0:
                                objResponse.success = "false";
                                objResponse.message = "Please try after some time.";
                                strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                                break;
                            default:
                                objResponse.success = "true";
                                objResponse.message = "Pickup Times saved successfully.";
                                strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                                break;
                        }
                    }
                    #endregion
                }
            }

        }
        HttpContext.Current.Response.Write(strResponseName);
        Response.End();
    }

    private void SendUserMail(string EmailAddress, string Name, string Password)
    {
        string strHeader = "<!doctype html><html><head><meta charset='utf-8'><title>Registration</title><link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet'><style>body {	padding: 0px;	margin: 0px;}</style></head><body>";
        string strFooter = "</body></html>";

        EmailTemplateBAL objEmail = new EmailTemplateBAL();
        DataTable dt = new DataTable();
        dt = objEmail.GetByID(1, 1);
        if (dt.Rows.Count > 0)
        {

            string strSubject = string.Empty;
            strSubject = Convert.ToString(dt.Rows[0]["Subject"]);
            System.Text.StringBuilder sbEmailTemplate = new System.Text.StringBuilder(strHeader + Convert.ToString(dt.Rows[0]["Template"]) + strFooter);

            sbEmailTemplate.Replace("###Name###", Name);
            sbEmailTemplate.Replace("###Email###", EmailAddress);
            sbEmailTemplate.Replace("###Password###", Password);
            sbEmailTemplate.Replace("###siteurl###", Config.WebSiteUrl);
            sbEmailTemplate.Replace("###SiteName###", Config.WebsiteName);
            if (EmailAddress != string.Empty)
            {
                GeneralSettings.SendEmail(EmailAddress, new GeneralSettings().getConfigValue("abnno"), strSubject.Replace("###WebsiteName###", Config.WebsiteName), sbEmailTemplate.ToString());
            }
        }
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
                    sw.WriteLine(LogMessage);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(LogMessage);
                }
            }
        }
        catch (Exception ex)
        {

        }

    }
}