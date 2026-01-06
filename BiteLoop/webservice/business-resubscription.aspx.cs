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
using Stripe;
using System.Net;

public partial class webservice_business_schedule : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Keys.Count > 0)
        {
            BusinessResubscription();
        }
    }

    private void BusinessResubscription()
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
            DataTable dt = new DataTable();
            dt = BusinessCardDetails(Convert.ToInt64(Request["CardID"]), Convert.ToInt64(Request["UserID"]));

            if (dt.Rows.Count > 0)
            {
                int GSTregistered = Convert.ToInt32(dt.Rows[0]["GSTregistered"]);
                string StripeCustomerID = Convert.ToString(dt.Rows[0]["CustomerCode"]);
                string StripeCustomerCardID = Convert.ToString(dt.Rows[0]["CardID"]);


                Decimal Amount = 50;
                Decimal GST = 0;

                string StripeChargeID = "";
                string StripeAmount = "";
                string StripeTransactionId = "";
                string GSTAmount = "";



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

                if (GSTregistered == 0)
                {
                    GST = 0;
                    Amount = Amount - Convert.ToDecimal(GSTAmount);
                }

                try
                {
                    var options = new ChargeCreateOptions()
                    {
                        Amount = Convert.ToInt64((Amount * 100)),
                        Currency = "AUD",
                        Customer = StripeCustomerID, // Customer ID
                        Source = StripeCustomerCardID,

                        Metadata = new Dictionary<string, string>
                        {
                            { "BusinessName", Convert.ToString(dt.Rows[0]["BusinessName"])},
                            { "BusinessPhone", Convert.ToString(dt.Rows[0]["BusinessPhone"])},
                        },
                        Description = "Biteloop Resubscription"
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


                        BusinessResubscriptionUpdate(Convert.ToInt64(Request["UserID"]), StripeCustomerCardID, StripeChargeID, StripeAmount, StripeTransactionId, StripeCustomerID, Convert.ToString(GST), GSTAmount);

                        objResponse.success = "true";
                        objResponse.message = "Subscription activated successfully.";
                        WriteResponse(objResponse);

                    }
                    else
                    {
                        objResponse.success = "false";
                        objResponse.message = "Stripe Response: " + charge.StripeResponse.StatusCode;
                        WriteResponse(objResponse);
                    }


                }
                catch (Exception ex)
                {
                    objResponse.success = "false";
                    objResponse.message = "Error: " + ex.Message.ToString();
                    WriteResponse(objResponse);
                }
            }
            else
            {
                objResponse.success = "false";
                objResponse.message = "Card not found.";
                WriteResponse(objResponse);
            }

            // Payment Process

        }
        Response.End();
    }
    public int BusinessResubscriptionUpdate(long CustomerID, string CardID, string StripeChargeID, string StripeAmount, string StripeTransactionId, string StripeCustomerID, string GST, string GSTAmount)
    {
        DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@CustomerID", DbParameter.DbType.Int, 400, CustomerID),
                new DbParameter("@StripeCardID", DbParameter.DbType.VarChar, 100, CardID),
                new DbParameter("@StripeChargeID", DbParameter.DbType.VarChar, 200, StripeChargeID),
                new DbParameter("@StripeAmount", DbParameter.DbType.VarChar, 200, StripeAmount),
                new DbParameter("@StripeTransactionId", DbParameter.DbType.VarChar, 200, StripeTransactionId),
                new DbParameter("@StripeCustomerID", DbParameter.DbType.VarChar, 200, StripeCustomerID),
                new DbParameter("@GST", DbParameter.DbType.VarChar, 200, GST),
                new DbParameter("@GSTAmount", DbParameter.DbType.VarChar, 200, GSTAmount),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output)
            };
        DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "BusinessResubscription", dbParam);
        return Convert.ToInt32(dbParam[dbParam.Length - 1].Value);
    }

    public DataTable BusinessCardDetails(long CardID, long BusinessID)
    {
        DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@BusinessID", DbParameter.DbType.Int, 200, BusinessID),
                new DbParameter("@CardID", DbParameter.DbType.Int, 200, CardID)
                };
        return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "BusinessCardDetails", dbParam);

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
        // Response.End();
    }
}