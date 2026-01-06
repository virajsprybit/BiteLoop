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
using Stripe;
using System.Configuration;

public partial class webservice_user_add_card : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Keys.Count > 0)
        {
            UserAddCard();
        }
    }

    private void UserAddCard()
    {
        Response objResponse = new Response();
        bool IsValidated = false;

        // ================User Last Login Location Track=============================
        string strSuburb = string.Empty;
        if (HttpContext.Current.Request["Suburb"] != null)
        {
            strSuburb = Convert.ToString(HttpContext.Current.Request["Suburb"]);
        }
        // ================User Last Login Location Track=============================

        if (Request["UserID"] != null && Request["SecretKey"] != null && Request["AuthToken"] != null)
        {
            if (ValidateRequestBAL.UserValidateClientRequest(Convert.ToInt64(Request["UserID"]), Convert.ToString(Request["SecretKey"]), Convert.ToString(Request["AuthToken"]), strSuburb))
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
            try
            {
                long UserID = 0;
                if (Request["UserID"] != null)
                {
                    UserID = Convert.ToInt64(Request["UserID"]);
                }
                StripeAddCustomer(UserID, Convert.ToString(Request["CardNumber"]), Convert.ToInt32(Request["ExpiryYear"]), Convert.ToInt32(Request["ExpiryMonth"]), Convert.ToString(Request["CVV"]), Convert.ToString(Request["IsDefault"]));


            }
            catch (Exception ex)
            {
                objResponse.success = "false";
                objResponse.message = ex.Message.ToString();
                HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
              
            }
            HttpContext.Current.Response.End();
        }
        
    }
    private void StripeAddCustomer(long UserID, string strCardNumber, int ExpiryYear, int ExpiryMonth, string CVV, string IsDefault)
    {
        //int isError = 0;
        //Response objResponse = new Response();       
        //string strUserName = "";
        //string strUserEmail = "";

        //// Get User Name & Email -----------------------------------------------
        //DataTable dt = new DataTable();
        //UsersBAL objUserBAL = new UsersBAL();
        //objUserBAL.ID = UserID;
        //dt = objUserBAL.UserDetailsByID();
        //if(dt.Rows.Count>0)
        //{
        //    strUserName = Convert.ToString(dt.Rows[0]["Name"]);
        //    strUserEmail = Convert.ToString(dt.Rows[0]["Email"]);
        //}

        ////----------------------------------------------------------------------

        //var options = new StripeChargeCreateOptions();
        //StripeConfiguration.SetApiKey(ConfigurationManager.AppSettings["StripeSecretKey"].ToString());
        //var myToken = new StripeTokenCreateOptions();
        //myToken.Card = new StripeCreditCardOptions()
        //{
        //    Number = strCardNumber,
        //    ExpirationYear = ExpiryYear,
        //    ExpirationMonth = ExpiryMonth,
        //    AddressCountry = "AU",
        //    AddressLine1 = "",    
        //    AddressLine2 = "",    
        //    AddressCity = "",     
        //    AddressState = "",    
        //    AddressZip = "",
        //    Name = strUserName,       
        //    Cvc = CVV
        //};

        //var tokenService = new StripeTokenService();
        //var token = "";
        //try
        //{
        //    StripeToken stripeToken = tokenService.Create(myToken);
        //    token = stripeToken.Id;
        //}
        //catch (Exception ex)
        //{
        //    isError = 1;
        //    objResponse.success = "false";
        //    objResponse.message = ex.Message.ToString();
        //    HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));            
        //}

        //if (isError == 0)
        //{
        //    var customerOptions = new StripeCustomerCreateOptions
        //    {
        //        SourceToken = token,
        //        Email = strUserEmail,
        //    };
        //    var customerService = new StripeCustomerService();
        //    StripeCustomer customer = null;
        //    try
        //    {
        //        customer = customerService.Create(customerOptions);
        //        string CustomerID = customer.Id;
        //        string Last4Digits = customer.Sources.Data[0].Card.Last4;
        //        string CardType = customer.Sources.Data[0].Card.Brand;
        //        string CardID = customer.DefaultSourceId;

        //        AddCard(UserID, CustomerID, CardType, Last4Digits, IsDefault, Convert.ToString(ExpiryMonth), Convert.ToString(ExpiryYear), CardID);
        //        objResponse.success = "true";
        //        objResponse.message = "Card added successfully.";
        //        HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));                
        //    }
        //    catch (Exception ex)
        //    {
        //        objResponse.success = "false";
        //        objResponse.message = ex.Message.ToString();
        //        HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));               

        //    }
        //}
        
    }

    private void AddCard(long intUserID, string strCustomerID, string strCardType, string strLastDigits, string IsDefault, string ExpiryMonth, string ExpiryYear, string CardID)
    {
        CartBAL objCartBAL = new CartBAL();
        //objCartBAL.UserCreditCardAdd(intUserID, strCustomerID, strCardType, strLastDigits);
        UserCreditCardAddDAL(intUserID, strCustomerID, strCardType, strLastDigits, IsDefault, ExpiryMonth, ExpiryYear, CardID);

    }
    private long UserCreditCardAddDAL(long intUserID, string CustomerID, string strCardType, string strLastDigits, string IsDefault, string ExpiryMonth, string ExpiryYear, string CardID)
    {
        DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@UserID", DbParameter.DbType.Int, 40, intUserID) ,               
                new DbParameter("@CustomerID", DbParameter.DbType.VarChar, 4000, CustomerID),
                new DbParameter("@LastDigits", DbParameter.DbType.VarChar, 40, strLastDigits),
                new DbParameter("@CardType", DbParameter.DbType.VarChar, 40, strCardType), 
                new DbParameter("@IsDefault", DbParameter.DbType.Int, 40, IsDefault), 
                new DbParameter("@ExpiryMonth", DbParameter.DbType.VarChar, 40, ExpiryMonth), 
                new DbParameter("@ExpiryYear", DbParameter.DbType.VarChar, 40, ExpiryYear), 
                 new DbParameter("@StripeCardID", DbParameter.DbType.VarChar, 40, CardID), 
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output)
            };
        DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "UserCreditCardAddWithDefault", dbParam);
        return Convert.ToInt64(dbParam[dbParam.Length - 1].Value);
    }

}