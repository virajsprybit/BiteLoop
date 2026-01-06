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
using Stripe;
using System.Configuration;
using System.Web.Script.Serialization;
using DAL;

public partial class webservice_place_order : System.Web.UI.Page
{
    int CurrentCartQty = 0;
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
    string TransactionResponse = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Keys.Count > 0)
        {
            PlaceOrder();
        }
    }

    private void PlaceOrder()
    {
        Response objResponse = new Response();
        string StateCode = "VIC";
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
            // Stripe Payment Gateway Call
            string strName = string.Empty;
            string strEmail = string.Empty;
            string strCardNumber = string.Empty;
            string strCustomerCardID = string.Empty;
            int ExpiryYear = DateTime.Now.Year;
            int ExpiryMonth = 1;
            string CVV = "";
            decimal PayAmount = 0;
            int RememberMe = 0;
            //var options = new StripeChargeCreateOptions();
            string strCustomerID = "";
            string strCardID = "";
            int Version = 1;
            decimal RewardsPoints = 0;
            long UserPromocodeID = 0;
            decimal PromocodeDiscountAmount = 0;
            string StripeCustomerCode = "";


            #region Check Payment Amount is ZERO?
            if (Request["PayAmount"] != null)
            {
                if (Convert.ToString(Request["PayAmount"]) != string.Empty)
                    PayAmount = Convert.ToDecimal(Request["PayAmount"]);
            }

            if (PayAmount < 0)
            {
                objResponse.success = "false";
                objResponse.message = "Total pay amount can not be 0.";
                HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
                HttpContext.Current.Response.End();
            }
            #endregion

            #region Set Request Parameters
            if (Request["Name"] != null)
            {
                if (Convert.ToString(Request["Name"]) != string.Empty)
                    strName = Convert.ToString(Request["Name"]);
            }
            if (Request["Email"] != null)
            {
                if (Convert.ToString(Request["Email"]) != string.Empty)
                    strEmail = Convert.ToString(Request["Email"]);
            }
            //if (Request["CardNumber"] != null)
            //{
            //    if (Convert.ToString(Request["CardNumber"]) != string.Empty)
            //        strCardNumber = Convert.ToString(Request["CardNumber"]);
            //}
            //if (Request["ExpiryYear"] != null)
            //{
            //    if (Convert.ToString(Request["ExpiryYear"]) != string.Empty)
            //        ExpiryYear = Convert.ToInt32(Request["ExpiryYear"]);
            //}
            //if (Request["ExpiryMonth"] != null)
            //{
            //    if (Convert.ToString(Request["ExpiryMonth"]) != string.Empty)
            //        ExpiryMonth = Convert.ToInt32(Request["ExpiryMonth"]);
            //}
            //if (Request["CVV"] != null)
            //{
            //    if (Convert.ToString(Request["CVV"]) != string.Empty)
            //        CVV = Convert.ToString(Request["CVV"]);
            //}


            if (Request["RememberMe"] != null)
            {
                if (Convert.ToString(Request["RememberMe"]) != string.Empty)
                    RememberMe = Convert.ToInt32(Request["RememberMe"]);
            }
            //if (Request["CustomerCardID"] != null)
            //{
            //    if (Convert.ToString(Request["CustomerCardID"]) != string.Empty)
            //        strCustomerCardID = Convert.ToString(Request["CustomerCardID"]);
            //}

            if (Request["Version"] != null)
            {
                if (Convert.ToString(Request["Version"]) != string.Empty)
                    Version = Convert.ToInt32(Request["Version"]);
            }
            if (Request["RewardsPoints"] != null)
            {
                if (Convert.ToString(Request["RewardsPoints"]) != string.Empty)
                    RewardsPoints = Convert.ToDecimal(Request["RewardsPoints"]);
            }

            if (Request["StateCode"] != null)
            {
                if (Convert.ToString(Request["StateCode"]) != string.Empty)
                    StateCode = Convert.ToString(Request["StateCode"]);
            }
            if (Request["UserPromocodeID"] != null)
            {
                if (Convert.ToString(Request["UserPromocodeID"]) != string.Empty)
                    UserPromocodeID = Convert.ToInt64(Request["UserPromocodeID"]);
            }
            if (Request["PromocodeDiscountAmount"] != null)
            {
                if (Convert.ToString(Request["PromocodeDiscountAmount"]) != string.Empty)
                    PromocodeDiscountAmount = Convert.ToDecimal(Request["PromocodeDiscountAmount"]);
            }

            if (Request["StripeCustomerCode"] != null)
            {
                if (Convert.ToString(Request["StripeCustomerCode"]) != string.Empty)
                    StripeCustomerCode = Convert.ToString(Request["StripeCustomerCode"]);
            }

            #endregion

            #region Check Current Day Quantity

            long VerifyQtyResult = 0;
            int RemainingQty = 0;
            VerifyQtyResult = VerifyCurrentQtyDuringOrder(out RemainingQty, Convert.ToInt64(Request["BusinessID"]), Convert.ToInt64(Request["UserID"]), out CurrentCartQty, StateCode);

            if (VerifyQtyResult == -1)
            {
                UpdateCurrentQtyDuringOrder(Convert.ToInt64(Request["BusinessID"]), CurrentCartQty, StateCode);
                objResponse.success = "false";
                objResponse.message = "Someone has just purchased and remaining quantity is " + RemainingQty + ".";
                HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
                HttpContext.Current.Response.End();
            }

            #endregion

            #region Stript Payment & Place Order

            #region Commented Code
            //if (StripeCustomerCode != string.Empty)
            //{
            //   // StripeConfiguration.SetApiKey(ConfigurationManager.AppSettings["StripeSecretKey"].ToString());


            //    if (strCustomerCardID != string.Empty)
            //    {
            //        #region Commented Code
            //        //RememberMe = 0;

            //        //options = new StripeChargeCreateOptions
            //        //{
            //        //    Amount = (int)(PayAmount * 100),
            //        //    Currency = "aud",
            //        //    Description = "Bring Me Home Order",
            //        //    CustomerId = strCustomerCardID,
            //        //};
            //        #endregion
            //    }
            //    else
            //    {

            //        #region Commented Code
            //        //var myToken = new StripeTokenCreateOptions();
            //        //myToken.Card = new StripeCreditCardOptions()
            //        //{
            //        //    Number = strCardNumber,
            //        //    ExpirationYear = ExpiryYear,
            //        //    ExpirationMonth = ExpiryMonth,
            //        //    AddressCountry = "AU",// optional
            //        //    AddressLine1 = "",    // optional
            //        //    AddressLine2 = "",    // optional
            //        //    AddressCity = "",     // optional
            //        //    AddressState = "",    // optional
            //        //    AddressZip = "",      // optional
            //        //    Name = strName,       // optional
            //        //    Cvc = CVV // optional
            //        //};

            //        //var tokenService = new StripeTokenService();
            //        //var token = "";
            //        //try
            //        //{
            //        //    StripeToken stripeToken = tokenService.Create(myToken);
            //        //    token = stripeToken.Id;
            //        //}
            //        //catch (Exception ex)
            //        //{
            //        //    UpdateCurrentQtyDuringOrder(Convert.ToInt64(Request["BusinessID"]), CurrentCartQty, StateCode);
            //        //    objResponse.success = "false";
            //        //    objResponse.message = Convert.ToString(ex.Message);
            //        //    HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));

            //        //    HttpContext.Current.Response.End();
            //        //}

            //        // Add New Customer
            //        #endregion


            //        if (RememberMe == 1)
            //        {
            //            #region Commented Code
            //            //var customerOptions = new StripeCustomerCreateOptions
            //            //{
            //            //    SourceToken = token,
            //            //    Email = strEmail,
            //            //};
            //            //var customerService = new StripeCustomerService();
            //            //StripeCustomer customer = null;
            //            //try
            //            //{
            //            //    customer = customerService.Create(customerOptions);
            //            //}
            //            //catch (Exception ex)
            //            //{
            //            //    UpdateCurrentQtyDuringOrder(Convert.ToInt64(Request["BusinessID"]), CurrentCartQty, StateCode);
            //            //    objResponse.success = "false";
            //            //    objResponse.message = Convert.ToString(ex.Message);
            //            //    HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));

            //            //    HttpContext.Current.Response.End();

            //            //}

            //            // Add New Customer
            //            //strCustomerID = customer.Id;
            //            //strCardID = customer.DefaultSourceId;
            //            //options = new StripeChargeCreateOptions
            //            //{
            //            //    Amount = (int)(PayAmount * 100),
            //            //    Currency = "aud",
            //            //    Description = "Bring Me Home Order",
            //            //    CustomerId = customer.Id,
            //            //};

            //            #endregion

            //        }
            //        else
            //        {
            //            #region Commented Code
            //            //options = new StripeChargeCreateOptions
            //            //{
            //            //    Amount = (int)(PayAmount * 100),
            //            //    Currency = "aud",
            //            //    Description = "Bring Me Home Order",
            //            //    SourceTokenOrExistingSourceId = token,

            //            //};
            //            #endregion
            //        }

            //    }
            //}
            #endregion

            if (PayAmount >= 0)
            {
                try
                {

                    string stripeResponse = StripIntegration();

                    if (stripeResponse == "Success")
                    {
                        string OrderStatus = "";
                        string FailureMessage = "";
                        CartBAL objCartBAL = new CartBAL();
                        objCartBAL.UserID = Convert.ToInt64(Request["UserID"]);
                        objCartBAL.BusinessID = Convert.ToInt64(Request["BusinessID"]);



                        OrderStatus = "succeeded";
                        objCartBAL.TransactionId = Convert.ToString(StripeTransactionId);
                        objCartBAL.Currency = "AUD";
                        objCartBAL.CustomerId = Convert.ToString(StripeCustomerID);
                        objCartBAL.CardType = Convert.ToString(CardType);
                        objCartBAL.CardLastDigits = Convert.ToString(Last4Digits);
                        objCartBAL.TransactionResponse = Convert.ToString(TransactionResponse);
                        StripeChargeID = Convert.ToString(StripeChargeID);



                        if (OrderStatus == "succeeded")
                        {

                            // Stripe Payment Gateway Call

                            //long result = objCartBAL.PlaceOrder(Version, RewardsPoints);
                            long result = PlaceOrderDAL(Version, RewardsPoints, objCartBAL, StripeChargeID, StateCode, UserPromocodeID, PromocodeDiscountAmount);

                            if (result > 0 && RememberMe == 1)
                            {
                                AddCard(Convert.ToInt64(Request["UserID"]), strCustomerID, objCartBAL.CardType, objCartBAL.CardLastDigits, Convert.ToString(ExpiryMonth), Convert.ToString(ExpiryYear), strCardID);
                            }


                            #region Switch Case

                            switch (result)
                            {
                                case -1:
                                    objResponse.success = "false";
                                    objResponse.message = "Please try after sometime.";
                                    break;
                                case -2:
                                    objResponse.success = "false";
                                    objResponse.message = "This order is already completed.";
                                    break;
                                default:
                                    objResponse.success = "true";
                                    objResponse.message = "Order has been placed successfully.";

                                    SendOrderPickupNotifications(result);

                                    // Order Summary Start
                                    objCartBAL = new CartBAL();
                                    objCartBAL.UserID = Convert.ToInt64(Request["UserID"]);
                                    objCartBAL.BusinessID = Convert.ToInt64(Request["BusinessID"]);
                                    DataTable dt = new DataTable();


                                    dt = objCartBAL.OrderSummary(result);

                                    OrderSummary[] objOrderSummary = new OrderSummary[dt.Rows.Count];
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        objOrderSummary[i] = new OrderSummary();
                                        objOrderSummary[i].OrderNo = Convert.ToString(dt.Rows[i]["OrderID"]);
                                        objOrderSummary[i].OrderID = Convert.ToInt64(dt.Rows[i]["OrderNo"]);
                                        objOrderSummary[i].BusinessID = Convert.ToInt64(dt.Rows[i]["BusinessID"]);
                                        objOrderSummary[i].BusinessName = Convert.ToString(dt.Rows[i]["BusinessName"]);
                                        objOrderSummary[i].BusinessPhone = Convert.ToString(dt.Rows[i]["BusinessPhone"]);
                                        objOrderSummary[i].Mobile = Convert.ToString(dt.Rows[i]["Mobile"]);
                                        objOrderSummary[i].StreetAddress = Convert.ToString(dt.Rows[i]["StreetAddress"]);
                                        objOrderSummary[i].Location = Convert.ToString(dt.Rows[i]["Location"]);
                                        objOrderSummary[i].UserID = Convert.ToInt64(dt.Rows[i]["UserID"]);
                                        objOrderSummary[i].PickupDate = Convert.ToDateTime(dt.Rows[i]["PickupDate"]).ToString("dd/MM/yyyy");
                                        if (Convert.ToString(dt.Rows[i]["PickupFromTime"]) != string.Empty)
                                            objOrderSummary[i].PickupFromTime = Convert.ToDateTime(dt.Rows[i]["PickupFromTime"]).ToString("hh:mm tt");
                                        else
                                            objOrderSummary[i].PickupFromTime = string.Empty;


                                        if (Convert.ToString(dt.Rows[i]["PickupToTime"]) != string.Empty)
                                            objOrderSummary[i].PickupToTime = Convert.ToDateTime(dt.Rows[i]["PickupToTime"]).ToString("hh:mm tt");
                                        else
                                            objOrderSummary[i].PickupToTime = string.Empty;

                                        objOrderSummary[i].Qty = Convert.ToString(dt.Rows[i]["Qty"]);
                                        objOrderSummary[i].OriginalPrice = Convert.ToDouble(dt.Rows[i]["OriginalPrice"]).ToString("f2");
                                        objOrderSummary[i].Donation = Convert.ToDouble(dt.Rows[i]["Donation"]).ToString("f2");
                                        objOrderSummary[i].TransactionFee = Convert.ToDouble(dt.Rows[i]["TransactionFee"]).ToString("f2");
                                        objOrderSummary[i].BiteloopFee = Convert.ToDouble(dt.Rows[i]["BringMeHomeFee"]).ToString("f2");
                                        objOrderSummary[i].GrandTotal = Convert.ToDouble(dt.Rows[i]["GrandTotal"]).ToString("f2");
                                        objOrderSummary[i].ItemPrice = Convert.ToDouble(dt.Rows[i]["ItemPrice"]).ToString("f2");
                                        objOrderSummary[i].RedeemPoints = Convert.ToDouble(dt.Rows[i]["RewardsAmount"]).ToString("f2");
                                        objOrderSummary[i].Latitude = Convert.ToString(dt.Rows[i]["Latitude"]);
                                        objOrderSummary[i].Longitude = Convert.ToString(dt.Rows[i]["Longitude"]);
                                        objOrderSummary[i].OrderStatusID = Convert.ToInt32(dt.Rows[i]["OrderStatusID"]);
                                        objOrderSummary[i].OrderStatusText = Convert.ToString(dt.Rows[i]["OrderStatusText"]);
                                        objOrderSummary[i].Note = Convert.ToString(dt.Rows[i]["Note"]);
                                        if (PromocodeDiscountAmount > 0)
                                        {
                                            objOrderSummary[i].PromocodeDiscountAmount = Convert.ToString(PromocodeDiscountAmount);
                                        }
                                        else
                                        {
                                            objOrderSummary[i].PromocodeDiscountAmount = "0.00";
                                        }

                                        objResponse.OrderSummary = objOrderSummary;
                                    }
                                    // Order Summary End

                                    break;
                            }
                            #endregion

                            UpdateCurrentQtyDuringOrder(Convert.ToInt64(Request["BusinessID"]), CurrentCartQty, StateCode);
                            string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

                            strResponseName = strResponseName.Replace("\"OrderSummary\"", "\"data\"");
                            HttpContext.Current.Response.Write(strResponseName);
                            // HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
                        }
                        else
                        {
                            UpdateCurrentQtyDuringOrder(Convert.ToInt64(Request["BusinessID"]), CurrentCartQty, StateCode);
                            objResponse.success = "false";
                            objResponse.message = Convert.ToString(FailureMessage);
                            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
                        }
                    }
                    else
                    {
                        objResponse.success = "false";
                        objResponse.message = stripeResponse;
                        HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));

                    }
                }
                catch (Exception ex)
                {
                    UpdateCurrentQtyDuringOrder(Convert.ToInt64(Request["BusinessID"]), CurrentCartQty, StateCode);
                    objResponse.success = "false";
                    objResponse.message = Convert.ToString(ex.Message.ToString());
                    HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
                }
                // finally
                // {
                // UpdateCurrentQtyDuringOrder(Convert.ToInt64(Request["BusinessID"]), CurrentCartQty);
                // }


            }
            #endregion
        }
        HttpContext.Current.Response.End();
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
                Email = Convert.ToString(Request["Email"]),
                Name = Convert.ToString(Request["Name"])
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

                Decimal Amount = Convert.ToDecimal(Request["PayAmount"]);

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
                            { "Name", Convert.ToString(Request["Name"]) },
                            { "Email", Convert.ToString(Request["Email"]) },

                        },
                        Description = "Biteloop Customer Order"
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
                        TransactionResponse = charge.StripeResponse.Content;


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


    private void AddCard(long intUserID, string strCustomerID, string strCardType, string strLastDigits, string ExpiryMonth, string ExpiryYear, string strCardID)
    {
        CartBAL objCartBAL = new CartBAL();
        // objCartBAL.UserCreditCardAdd(intUserID, strCustomerID, strCardType, strLastDigits);
        UserCreditCardAddWirhExpiry(intUserID, strCustomerID, strCardType, strLastDigits, ExpiryMonth, ExpiryYear, strCardID);

    }
    private long UserCreditCardAddWirhExpiry(long intUserID, string CustomerID, string strCardType, string strLastDigits, string ExpiryMonth, string ExpiryYear, string strCardID)
    {
        DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@UserID", DbParameter.DbType.Int, 40, intUserID) ,
                new DbParameter("@CustomerID", DbParameter.DbType.VarChar, 4000, CustomerID),
                new DbParameter("@LastDigits", DbParameter.DbType.VarChar, 40, strLastDigits),
                new DbParameter("@CardType", DbParameter.DbType.VarChar, 40, strCardType),
                new DbParameter("@ExpiryMonth", DbParameter.DbType.VarChar, 40, ExpiryMonth),
                new DbParameter("@ExpiryYear", DbParameter.DbType.VarChar, 40, ExpiryYear),
                new DbParameter("@StripeCardID", DbParameter.DbType.VarChar, 40, strCardID),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output)
            };
        DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "UserCreditCardAddWithExpiry", dbParam);
        return Convert.ToInt64(dbParam[dbParam.Length - 1].Value);
    }
    private long VerifyCurrentQtyDuringOrder(out int RemainingQty, long BusinessID, long UserID, out int CurrentCartQty, string StateCode)
    {
        DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@StateCode", DbParameter.DbType.VarChar, 500, StateCode),
                new DbParameter("@BusinessID", DbParameter.DbType.Int, 500, BusinessID),
                new DbParameter("@UserID", DbParameter.DbType.Int, 500, UserID),
                new DbParameter("@CurrentCartQuantity", DbParameter.DbType.Int, 200, ParameterDirection.Output),
                new DbParameter("@RemainingQuantity", DbParameter.DbType.Int, 200, ParameterDirection.Output),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output)
            };
        DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "VerifyCurrentQtyDuringOrder", dbParam);
        RemainingQty = Convert.ToInt32(dbParam[dbParam.Length - 2].Value);
        CurrentCartQty = Convert.ToInt32(dbParam[dbParam.Length - 3].Value);
        return Convert.ToInt64(dbParam[dbParam.Length - 1].Value);
    }
    private void UpdateCurrentQtyDuringOrder(long BusinessID, int Qty, string StateCode)
    {
        DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@BusinessID", DbParameter.DbType.Int, 500, BusinessID),
                new DbParameter("@StateCode", DbParameter.DbType.VarChar, 500, StateCode),
                new DbParameter("@Qty", DbParameter.DbType.Int, 500, Qty)
            };
        DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "UpdateCurrentQtyDuringOrder", dbParam);
    }
    private void SendOrderPickupNotifications(long OrderID)
    {
        DataTable dtUsers = new DataTable();
        CartBAL objCartBAL = new CartBAL();
        string strUsers = string.Empty;
        string strBusiness = string.Empty;
        dtUsers = objCartBAL.OrderUsersDeviceKey(OrderID);
        if (dtUsers.Rows.Count > 0)
        {
            SendNotification objSendNotification = new SendNotification();
            string strMessage = string.Empty;
            for (int i = 0; i < dtUsers.Rows.Count; i++)
            {
                if (Convert.ToString(dtUsers.Rows[i]["UserType"]) == "U")
                {
                    strMessage = "Thank you for ordering with us!";
                }
                else
                {
                    strMessage = "You received new order from Bring Me Home.";
                }

                if (Convert.ToString(dtUsers.Rows[i]["DeviceKey"]) != string.Empty)
                {
                    int NotificationCount = 0;
                    if (Convert.ToString(dtUsers.Rows[i]["NotificationCount"]) == string.Empty)
                    {
                        NotificationCount = Convert.ToInt32(dtUsers.Rows[i]["NotificationCount"]);
                        NotificationCount = NotificationCount + 1;
                    }
                    if (Convert.ToString(dtUsers.Rows[i]["UserType"]) == "U")
                    {
                        strMessage = "Thank you for your order at Bring Me Home.";
                        strUsers = strUsers + Convert.ToString(dtUsers.Rows[i]["UserID"]) + ",";
                    }
                    else
                    {
                        strBusiness = strBusiness + Convert.ToString(dtUsers.Rows[i]["UserID"]) + ",";
                        strMessage = "You received new order from Bring Me Home.";
                    }

                    objSendNotification.CallNotification(Convert.ToString(dtUsers.Rows[i]["DeviceKey"]), strMessage, "BMH Notification", Convert.ToString(dtUsers.Rows[i]["OrderID"]), "OrderNotification", Convert.ToString(dtUsers.Rows[i]["DeviceType"]), Convert.ToString(dtUsers.Rows[i]["UserType"]), NotificationCount);
                }
            }
        }
        if (strUsers.Length > 0)
        {
            strUsers = strUsers.Substring(0, strUsers.Length - 1);
        }
        if (strBusiness.Length > 0)
        {
            strBusiness = strBusiness.Substring(0, strBusiness.Length - 1);
        }

        if (strBusiness != string.Empty || strUsers != string.Empty)
        {
            GeneralBAL objGeneralBAL = new GeneralBAL();
            objGeneralBAL.UpdateNoticationsCountForUsers(strUsers, strBusiness);
        }
    }

    private long PlaceOrderDAL(int intVersion, decimal RewardsPoints, CartBAL objCartBAL, string StripeChargeID, string StateCode, long UserPromocodeID, decimal PromocodeDiscountAmount)
    {
        DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@UserID", DbParameter.DbType.Int, 40, objCartBAL.UserID),
                new DbParameter("@BusinessID", DbParameter.DbType.Int, 40, objCartBAL.BusinessID),

                new DbParameter("@TransactionId", DbParameter.DbType.VarChar, 8000, objCartBAL.TransactionId),
                new DbParameter("@Currency", DbParameter.DbType.VarChar, 8000, objCartBAL.Currency),
                new DbParameter("@CustomerId", DbParameter.DbType.VarChar, 8000, objCartBAL.CustomerId),
                new DbParameter("@CardType", DbParameter.DbType.VarChar, 8000, objCartBAL.CardType),
                new DbParameter("@CardLastDigits", DbParameter.DbType.VarChar, 8000, objCartBAL.CardLastDigits),
                new DbParameter("@TransactionResponse", DbParameter.DbType.VarChar, 8000, objCartBAL.TransactionResponse),
                new DbParameter("@Version", DbParameter.DbType.Int, 2, intVersion),
                new DbParameter("@RewardsPoints", DbParameter.DbType.Decimal, 10, RewardsPoints),
                new DbParameter("@StripeChargeID", DbParameter.DbType.VarChar, 1000, StripeChargeID),
                new DbParameter("@StateCode", DbParameter.DbType.VarChar, 1000, StateCode),
                //new DbParameter("@UserPromocodeID", DbParameter.DbType.Int, 1000, UserPromocodeID),
                //new DbParameter("@APPPromocodeDiscountAmount", DbParameter.DbType.Decimal, 1000, PromocodeDiscountAmount),

                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output)
            };
        DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "PlaceOrder", dbParam);
        return Convert.ToInt64(dbParam[dbParam.Length - 1].Value);
    }

    private void WriteLogFile(string LogMessage)
    {
        try
        {

            string path = System.Web.Hosting.HostingEnvironment.MapPath("~/source") + "\\LogFile.txt";
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
}