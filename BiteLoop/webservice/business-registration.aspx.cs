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
using System.Text.RegularExpressions;
using Stripe;
using System.Configuration;
using System.Runtime.InteropServices;

public partial class webservice_business_registration : System.Web.UI.Page
{
    string Last4Digits = "";
    string CardType = "";
    string StripeCardID = "";
    string ExpiryMonth = "";
    string ExpiryYear = "";
    string CustomerID = "";
    string GSTPer = "";
    string GSTAmount = "";

    string StripeChargeID = "";
    string StripeAmount = "";
    string StripeTransactionId = "";
    string StripeCustomerID = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Keys.Count > 0)
        {
            BusinessRegistration();
        }
    }

    //private void BusinessRegistration()
    //{
    //    Response objResponse = new Response();
    //    bool IsValidated = true;     
    //    if (!IsValidated)
    //    {
    //        CommonAPI objCommonAPI = new CommonAPI();
    //        objCommonAPI.Unauthorized();
    //    }

    private Tuple<string, List<string>> ValidateBusinessRegistration()
    {
        List<string> missing = new List<string>();

        //if (string.IsNullOrWhiteSpace(Request["Name"])) missing.Add("Name");
        if (string.IsNullOrWhiteSpace(Request["BusinessName"])) missing.Add("BusinessName");
        //if (string.IsNullOrWhiteSpace(Request["FullName"])) missing.Add("FullName");
        //if (string.IsNullOrWhiteSpace(Request["ContactPersonName"])) missing.Add("ContactPersonName");
        if (string.IsNullOrWhiteSpace(Request["Firstname"])) missing.Add("FirstName");
        if (string.IsNullOrWhiteSpace(Request["LastName"])) missing.Add("LastName");
        if (string.IsNullOrWhiteSpace(Request["EmailAddress"])) missing.Add("EmailAddress");
        if (string.IsNullOrWhiteSpace(Request["Password"])) missing.Add("Password");
        if (string.IsNullOrWhiteSpace(Request["Mobile"])) missing.Add("Mobile");
        if (string.IsNullOrWhiteSpace(Request["PostCode"])) missing.Add("PostCode");

        if (missing.Count > 0)
            return new Tuple<string, List<string>>("Missing or empty required fields.", missing);

        return new Tuple<string, List<string>>(string.Empty, new List<string>());
    }

    //private string ValidateBusinessRegistration()
    //{
    //    List<string> missing = new List<string>();

    //    if (string.IsNullOrWhiteSpace(Request["Name"])) missing.Add("Name");
    //    if (string.IsNullOrWhiteSpace(Request["FullName"])) missing.Add("FullName");
    //    if (string.IsNullOrWhiteSpace(Request["EmailAddress"])) missing.Add("EmailAddress");
    //    if (string.IsNullOrWhiteSpace(Request["Password"])) missing.Add("Password");
    //    if (string.IsNullOrWhiteSpace(Request["Mobile"])) missing.Add("Mobile");
    //    if (string.IsNullOrWhiteSpace(Request["PostCode"])) missing.Add("PostCode");

    //    if (missing.Count > 0)
    //        return "Missing or empty required fields: " + string.Join(", ", missing);

    //    return string.Empty;
    //}

    private void BusinessRegistration()
    {
        Response objResponse = new Response();

        var validationResult = ValidateBusinessRegistration();
        if (!string.IsNullOrEmpty(validationResult.Item1))
        {
            var responseBody = new
            {
                success = "false",
                message = validationResult.Item1,
                missing_fields = validationResult.Item2
            };

            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(responseBody, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            }));
            Response.End();
            return;
        }

        //private void BusinessRegistration()
        //{
        //    Response objResponse = new Response();

        //    string validationError = ValidateBusinessRegistration();
        //    if (!string.IsNullOrEmpty(validationError))
        //    {
        //        objResponse.success = "false";
        //        objResponse.message = validationError;
        //        HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse));
        //        Response.End();
        //        return;
        //    }

        bool IsValidated = true;
        if (!IsValidated)
        {
            CommonAPI objCommonAPI = new CommonAPI();
            objCommonAPI.Unauthorized();
        }
        else
        {
            BusinessBAL objBusinessBAL = new BusinessBAL();
            objBusinessBAL.ID = 0;
            //objBusinessBAL.Name = Convert.ToString(Request["Name"]);
            objBusinessBAL.Name = Convert.ToString(Request["BusinessName"]);
            objBusinessBAL.ABN = Convert.ToString(Request["ABN"]);
            objBusinessBAL.FirstName = Convert.ToString(Request["FirstName"]);
            objBusinessBAL.LastName = Convert.ToString(Request["LastName"]);
            objBusinessBAL.Suburb = Convert.ToString(Request["Suburb"]);
            objBusinessBAL.State = Convert.ToString(Request["State"]);
            objBusinessBAL.StreetAddress = Convert.ToString(Request["StreetAddress"]);
            objBusinessBAL.Location = Convert.ToString(Request["Location"]);
            //objBusinessBAL.FullName = Convert.ToString(Request["ContactPersonName"]);
            //objBusinessBAL.FullName = Convert.ToString(Request["FullName"]);
            objBusinessBAL.EmailAddress = Convert.ToString(Request["EmailAddress"]);
            objBusinessBAL.BusinessPhone = Convert.ToString(Request["BusinessPhone"]);
            objBusinessBAL.Mobile = Convert.ToString(Request["Mobile"]);
            objBusinessBAL.Note = Convert.ToString(Request["Note"]);
            objBusinessBAL.StoreName = Convert.ToString(Request["StoreName"]);



            int IsGoogleSignUp = 0;
            string strGoogleID = string.Empty;
            string strPass = string.Empty;
            if (Request["IsGoogleSignUP"] != null)
            {
                if (Convert.ToInt32(Request["IsGoogleSignUP"]) == 1)
                {
                    IsGoogleSignUp = 1;
                }

            }
            if (Request["GoogleID"] != null)
            {
                strGoogleID = Convert.ToString(Request["GoogleID"]);
            }

            if (IsGoogleSignUp == 1)
            {
                strPass = Convert.ToString(Utility.Common.RandomString(7));
                objBusinessBAL.Password = Utility.Security.EncryptDescrypt.EncryptString(strPass);

            }
            else
            {
                if (Request["Password"] != null)
                {
                    strPass = Convert.ToString(Request["Password"]);
                    objBusinessBAL.Password = Utility.Security.EncryptDescrypt.EncryptString(Convert.ToString(Request["Password"]));
                }
                else
                {
                    objBusinessBAL.Password = "";
                }
            }
            objBusinessBAL.Latitude = Convert.ToString(Request["Latitude"]);
            objBusinessBAL.Longitude = Convert.ToString(Request["Longitude"]);
            objBusinessBAL.PostCode = Convert.ToString(Request["PostCode"]);
            //new parameter
            //objBusinessBAL.GSTregistered = Request["GSTregistered"] != null && Request["GSTregistered"] == "1" ? 1 : 0;
            objBusinessBAL.ReceiveMarketingMails = Request["ReceiveMarketingMails"] != null && Request["ReceiveMarketingMails"] == "1" ? 1 : 0;
            objBusinessBAL.DeviceToken = Convert.ToString(Request["DeviceToken"]);

            if (BusinessValidationCheck(objBusinessBAL) == -1)
            {
                objResponse.success = "false";
                objResponse.message = "Email Is Already Exist.";
            }
            else
            {
                string stripeResponse = string.Empty;

                if (Request["StripeCustomerCode"] != null)
                {
                    stripeResponse = StripIntegration();
                }



                if (stripeResponse == "Success" || stripeResponse == string.Empty)
                {
                    long result = BusinessSaveWithGoogle(objBusinessBAL, strGoogleID, IsGoogleSignUp);

                    switch (result)
                    {
                        case -1:
                            objResponse.success = "false";
                            objResponse.message = "This User already exists.";
                            break;
                        case 0:
                            objResponse.success = "false";
                            objResponse.message = "Please try after some time.";
                            break;
                        default:

                            SendUserMail(objBusinessBAL.EmailAddress, objBusinessBAL.Name, strPass);

                            BusinessBAL objLoginBAL = new BusinessBAL();
                            objLoginBAL.EmailAddress = objBusinessBAL.EmailAddress;
                            objLoginBAL.Password = objBusinessBAL.Password;

                            DataTable dt = objLoginBAL.BusinessLoginCheck("", "A");

                            if (dt.Rows.Count > 0)
                            {
                                objResponse.success = "true";
                                objResponse.message = "Registration has been completed successfully.";

                                BusinessLogin[] objBusinessLogin = new BusinessLogin[dt.Rows.Count];
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    objBusinessLogin[i] = new BusinessLogin();
                                    objBusinessLogin[i].UserID = Convert.ToInt64(dt.Rows[i]["ID"]);
                                    objBusinessLogin[i].BusinessName = Convert.ToString(dt.Rows[i]["Name"]);

                                    string FullName = Convert.ToString(dt.Rows[i]["FullName"]);
                                    if (Convert.ToString(dt.Rows[i]["LastName"]) != "")
                                        FullName += " " + Convert.ToString(dt.Rows[i]["LastName"]);

                                    objBusinessLogin[i].FullName = FullName;
                                    objBusinessLogin[i].ABN = Convert.ToString(dt.Rows[i]["ABN"]);
                                    objBusinessLogin[i].EmailAddress = Convert.ToString(dt.Rows[i]["EmailAddress"]);
                                    objBusinessLogin[i].FirstName = Convert.ToString(dt.Rows[i]["FirstName"]);
                                    objBusinessLogin[i].LastName = Convert.ToString(dt.Rows[i]["LastName"]);
                                    objBusinessLogin[i].Suburb = Convert.ToString(dt.Rows[i]["Suburb"]);
                                    objBusinessLogin[i].State = Convert.ToString(dt.Rows[i]["State"]);
                                    objBusinessLogin[i].PostCode = Convert.ToString(dt.Rows[i]["PostCode"]);
                                    objBusinessLogin[i].BusinessPhone = Convert.ToString(dt.Rows[i]["BusinessPhone"]);
                                    objBusinessLogin[i].Mobile = Convert.ToString(dt.Rows[i]["Mobile"]);
                                    objBusinessLogin[i].AuthToken = Convert.ToString(dt.Rows[i]["AuthTokenDetails"]);
                                    objBusinessLogin[i].SecretKey = Convert.ToString(Utility.Security.Rijndael128Algorithm.EncryptString(Convert.ToString(dt.Rows[i]["ID"])));
                                    objBusinessLogin[i].IsSalesAdmin = Convert.ToInt32(dt.Rows[i]["IsSalesAdmin"]);
                                    objBusinessLogin[i].Step = Convert.ToInt32(dt.Rows[i]["OPenPageNO"]);
                                    objBusinessLogin[i].StateID = Convert.ToInt32(dt.Rows[i]["StateID"]);
                                    objBusinessLogin[i].StateCode = Convert.ToString(dt.Rows[i]["StateCode"]);
                                    objBusinessLogin[i].StateFullName = Convert.ToString(dt.Rows[i]["StateName"]);
                                    //objBusinessLogin[i].GstVerified = Convert.ToInt32(dt.Rows[i]["GSTregistered"]);
                                    objBusinessLogin[i].ReceiveMarketingEmail = Convert.ToInt32(dt.Rows[i]["ReceiveMarketingMails"]);
                                    objBusinessLogin[i].Note = Convert.ToString(dt.Rows[i]["Note"]);
                                    objBusinessLogin[i].StoreName = Convert.ToString(dt.Rows[i]["StoreName"]);
                                    objBusinessLogin[i].Latitude =
                                        CleanLatLong(Convert.ToString(dt.Rows[i]["Latitude"]));

                                    objBusinessLogin[i].Longitude =
                                        CleanLatLong(Convert.ToString(dt.Rows[i]["Longitude"]));

                                    bool isUserRegistered = (
                                     !string.IsNullOrWhiteSpace(Convert.ToString(dt.Rows[i]["RestaurantTypesValues"])) &&
                                     //!string.IsNullOrWhiteSpace(Convert.ToString(dt.Rows[i]["ProfilePhotoID"])) &&
                                     !string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["ProfilePhoto"])) &&
                                     !string.IsNullOrWhiteSpace(Convert.ToString(dt.Rows[i]["Description"])) &&
                                     !string.IsNullOrWhiteSpace(Convert.ToString(dt.Rows[i]["BSBNo"])) &&
                                     !string.IsNullOrWhiteSpace(Convert.ToString(dt.Rows[i]["AccountNumber"])) &&
                                     !string.IsNullOrWhiteSpace(Convert.ToString(dt.Rows[i]["BankName"])) &&
                                     !string.IsNullOrWhiteSpace(Convert.ToString(dt.Rows[i]["AccountName"]))
                                    );

                                    objBusinessLogin[i].IsProfileDetails = isUserRegistered;
                                }

                                objResponse.BusinessLogin = objBusinessLogin[0];

                                string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings()
                                {
                                    NullValueHandling = NullValueHandling.Ignore
                                });
                                strResponseName = strResponseName.Replace("\"BusinessLogin\"", "\"data\"");

                                UserCreditCardAdd(Convert.ToInt64(dt.Rows[0]["ID"]), CustomerID, CardType, Last4Digits, "1", ExpiryMonth, ExpiryYear, StripeCardID, Convert.ToString(dt.Rows[0]["Name"]));

                                HttpContext.Current.Response.Write(strResponseName);
                                HttpContext.Current.Response.End();
                                return;
                            }
                            else
                            {

                                objResponse.success = "true";
                                objResponse.message = "Registration completed but data not found.";
                            }

                        break;
                    }
                }
                else
                {
                    objResponse.success = "false";
                    objResponse.message = stripeResponse;
                }
            }
            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));

        }
        Response.End();
    }

    private string CleanLatLong(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return "";
        if (value.Trim() == ",") return "";
        return value.Trim();
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

    private string StripIntegration()
    {
        try
        {
            StripeConfiguration.ApiKey = Convert.ToString(ConfigurationManager.AppSettings["StripeSecretKey"]);

            // Create a Customer:
            var customerOptions = new CustomerCreateOptions
            {
                Source = Convert.ToString(Request["StripeCustomerCode"]),
                Email = Convert.ToString(Request["EmailAddress"]),
                Name = Convert.ToString(Request["BusinessName"]),
                Phone = Convert.ToString(Request["Mobile"])
            };

            var customerService = new CustomerService();
            Customer customer = customerService.Create(customerOptions);
            CustomerID = customer.Id;
            StripeCardID = customer.DefaultSourceId;

            var cardService = new CardService();

            var cardResponse = cardService.Get(
                CustomerID,
                StripeCardID
             );

            Last4Digits = cardResponse.Last4;
            CardType = cardResponse.Brand;
            ExpiryMonth = Convert.ToString(cardResponse.ExpMonth);
            ExpiryYear = Convert.ToString(cardResponse.ExpYear);

            if (cardResponse.StripeResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                // Payment Process

                Decimal Amount = 50;
                Decimal GST = 0;
                try
                {
                    Amount = Convert.ToDecimal(new BAL.GeneralSettings().getConfigValue("contactus")); // Amount
                    GST = Convert.ToDecimal(new BAL.GeneralSettings().getConfigValue("title")); // GST
                    GSTAmount = Convert.ToString(((Amount * GST) / 100));
                }
                catch (Exception ex)
                {
                    Amount = 50;
                }

                if (Request["GSTregistered"] != null)
                {
                    if (Convert.ToString(Request["GSTregistered"]) == "0")
                    {
                        GSTPer = "0";
                        Amount = Amount - Convert.ToDecimal(GSTAmount);
                    }
                    else
                    {
                        GSTPer = Convert.ToString(GST);
                    }
                }
                else
                {
                    GSTPer = Convert.ToString(GST);
                }


                try
                {

                    var options = new ChargeCreateOptions()
                    {
                        Amount = Convert.ToInt64((Amount * 100)),
                        Currency = "AUD",
                        Customer = cardResponse.CustomerId, // Customer ID
                        Source = StripeCardID,

                        Metadata = new Dictionary<string, string>
                        {
                            { "BusinessName", Convert.ToString(Request["BusinessName"]) },
                            { "BusinessPhone", Convert.ToString(Request["BusinessPhone"]) },

                        },
                        Description = "Biteloop Registration"
                    };

                    var service = new ChargeService();
                    Charge charge = service.Create(options);

                    //Payment Process
                    if (charge.StripeResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        StripeChargeID = Convert.ToString(charge.Id);
                        StripeAmount = Convert.ToString(Amount);
                        StripeTransactionId = Convert.ToString(charge.BalanceTransactionId);
                        StripeCustomerID = Convert.ToString(charge.CustomerId);

                        return "Success";
                    }
                    else
                    {
                        return "Error: Response from Stripe: " + charge.StripeResponse.StatusCode;
                    }


                }
                catch (Exception ex)
                {
                    return "Error: " + ex.Message.ToString();
                }

            }
            else
            {
                return "Error. Please try after some time.";
            }

        }
        catch (Exception ex)
        {
            return "Error from Stripe: " + ex.Message.ToString();
        }

    }

    private long BusinessSaveWithGoogle(BusinessBAL objBusinessBAL, string strGoogleID, int IsGoogleSignUp)
    {
        DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@ID", DbParameter.DbType.Int, 40, objBusinessBAL.ID),
                new DbParameter("@Name", DbParameter.DbType.VarChar, 200, objBusinessBAL.Name),
                new DbParameter("@ABN", DbParameter.DbType.VarChar, 200, objBusinessBAL.ABN),
                new DbParameter("@StreetAddress", DbParameter.DbType.VarChar, 500, objBusinessBAL.StreetAddress),
                new DbParameter("@Location", DbParameter.DbType.VarChar, 500, objBusinessBAL.Location),
                new DbParameter("@FullName", DbParameter.DbType.VarChar, 200, objBusinessBAL.FullName),
                new DbParameter("@EmailAddress", DbParameter.DbType.VarChar, 200, objBusinessBAL.EmailAddress),
                new DbParameter("@FirstName", DbParameter.DbType.VarChar, 100, objBusinessBAL.FirstName),
                new DbParameter("@LastName", DbParameter.DbType.VarChar, 100, objBusinessBAL.LastName),
                new DbParameter("@Suburb", DbParameter.DbType.VarChar, 100, objBusinessBAL.Suburb),
                new DbParameter("@State", DbParameter.DbType.VarChar, 100, objBusinessBAL.State),
                new DbParameter("@BusinessPhone", DbParameter.DbType.VarChar, 20, objBusinessBAL.BusinessPhone),
                new DbParameter("@Mobile", DbParameter.DbType.VarChar, 500, objBusinessBAL.Mobile),
                new DbParameter("@Password", DbParameter.DbType.VarChar, 500, objBusinessBAL.Password),
                new DbParameter("@Latitude", DbParameter.DbType.VarChar, 500, objBusinessBAL.Latitude),
                new DbParameter("@Longitude", DbParameter.DbType.VarChar, 500, objBusinessBAL.Longitude),
                new DbParameter("@PostCode", DbParameter.DbType.VarChar, 20, objBusinessBAL.PostCode),
                //new Parameter
                //new DbParameter("@GSTregistered", DbParameter.DbType.Int, 4, objBusinessBAL.GSTregistered),
                new DbParameter("@ReceiveMarketingMails", DbParameter.DbType.Int, 4, objBusinessBAL.ReceiveMarketingMails),
                new DbParameter("@GoogleID", DbParameter.DbType.VarChar, 2000, strGoogleID),
                new DbParameter("@IsGoogleSignUp", DbParameter.DbType.Int, 20, IsGoogleSignUp),
                new DbParameter("@Note", DbParameter.DbType.VarChar, 2000, objBusinessBAL.Note),
                new DbParameter("@StoreName", DbParameter.DbType.VarChar, 200, objBusinessBAL.StoreName),
                new DbParameter("@DeviceToken", DbParameter.DbType.VarChar, 500, objBusinessBAL.DeviceToken),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output),

            };
        DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "BusinessSaveWithGoogle", dbParam);
        return Convert.ToInt64(dbParam[dbParam.Length - 1].Value);
    }

    private int BusinessValidationCheck(BusinessBAL objBusinessBAL)
    {
        DbParameter[] dbParam = new DbParameter[] {
            new DbParameter("@EmailAddress", DbParameter.DbType.VarChar, 200, objBusinessBAL.EmailAddress),
            //new DbParameter("@BusinessPhone", DbParameter.DbType.VarChar, 20, objBusinessBAL.BusinessPhone),
            new DbParameter("@Mobile", DbParameter.DbType.VarChar, 500, objBusinessBAL.Mobile),
            new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output)
        };
        DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "BusinessValidationCheck", dbParam);
        return Convert.ToInt32(dbParam[dbParam.Length - 1].Value);
    }

    public int UserCreditCardAdd(long CustomerID, string CustomerCode, string CardType, string LastDigits, string IsDefault, string ExpiryMonth, string ExpiryYear, string CardID, string Name)
    {
        DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@CustomerID", DbParameter.DbType.Int, 400, CustomerID) ,
                new DbParameter("@CustomerCode", DbParameter.DbType.VarChar, 4000, CustomerCode),
                new DbParameter("@LastDigits", DbParameter.DbType.VarChar, 40, LastDigits),
                new DbParameter("@CardType", DbParameter.DbType.VarChar, 40, CardType),
                new DbParameter("@IsDefault", DbParameter.DbType.Int, 40, IsDefault),
                new DbParameter("@ExpiryMonth", DbParameter.DbType.VarChar, 40, ExpiryMonth),
                new DbParameter("@ExpiryYear", DbParameter.DbType.VarChar, 40, ExpiryYear),
                new DbParameter("@StripeCardID", DbParameter.DbType.VarChar, 100, CardID),
                new DbParameter("@NickName", DbParameter.DbType.VarChar, 50, Name),
                new DbParameter("@Name", DbParameter.DbType.VarChar, 50, Name),

                new DbParameter("@StripeChargeID", DbParameter.DbType.VarChar, 200, StripeChargeID),
                new DbParameter("@StripeAmount", DbParameter.DbType.VarChar, 200, StripeAmount),
                new DbParameter("@StripeTransactionId", DbParameter.DbType.VarChar, 200, StripeTransactionId),
                new DbParameter("@StripeCustomerID", DbParameter.DbType.VarChar, 200, StripeCustomerID),
                new DbParameter("@GST", DbParameter.DbType.VarChar, 200, GSTPer),
                new DbParameter("@GSTAmount", DbParameter.DbType.VarChar, 200, GSTAmount),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output)
            };
        DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "BusinessCreditCardAddWithDefault", dbParam);
        return Convert.ToInt32(dbParam[dbParam.Length - 1].Value);
    }
}