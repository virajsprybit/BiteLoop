using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using Utility;
using Newtonsoft.Json;
using BiteLoop.Common;
using DAL;
using System.Data;
using Stripe;
using System.Configuration;

public partial class webservice_order_status_change : System.Web.UI.Page
{
    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Keys.Count > 0)
        {
            OrderStatusChange();
        }
    }
    #endregion

    #region Order Status Change
    private void OrderStatusChange()
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

            //OrderStatusID
            //IsCancelled: 0/1
            //OrderID



            if (Convert.ToInt32(Request["IsCancelled"]) == 1)
            {
                DataTable dt = new DataTable();
                dt = OrderDetailsByID(Convert.ToInt64(Request["OrderID"]));
                if (dt.Rows.Count > 0)
                {
                    string refund = StripDisputeTran(dt);
                    if (refund.ToLower() == "success")
                    {
                        objResponse.success = "true";
                        objResponse.message = "Order refund successfully.";
                    }
                    else
                    {
                        objResponse.success = "false";
                        objResponse.message = refund;
                    }
                }
            }
            else
            {
                OrderStatusChangeSave(Convert.ToInt64(Request["OrderID"]), Convert.ToInt32(Request["OrderStatusID"]));
                objResponse.success = "true";
                objResponse.message = "Order status changed successfully.";
            }

            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
        }

        Response.End();
    }
    private string StripDisputeTran(DataTable dt)
    {
        string Result = "";
        StripeConfiguration.ApiKey = Convert.ToString(ConfigurationManager.AppSettings["StripeSecretKey"]);
        var options = new RefundCreateOptions
        {
            Charge = Convert.ToString(dt.Rows[0]["StripeChargeID"]),
            Amount = Convert.ToInt64(Convert.ToDecimal(dt.Rows[0]["GrandTotal"]) * 100)
        };

        var service = new RefundService();

        try
        {
            Refund response = service.Create(options);
            long OrderID = Convert.ToInt64(Request["OrderID"]);
            string RefundChargeID = response.ChargeId;
            string RefundReason = "";
            if (Convert.ToString(Request["Reason"]) != null)
            {
                RefundReason = Convert.ToString(Request["Reason"]);
            }
            decimal RefundAmount = Convert.ToDecimal(dt.Rows[0]["GrandTotal"]);
            SaveRefundTransaction(OrderID, RefundChargeID, RefundReason, RefundAmount);
            Result = "Success";
        }
        catch (Exception ex)
        {
            Result = ex.Message;

        }
        return Result;
    }

    public void OrderStatusChangeSave(long OrderID, int OrderStatusID)
    {
        DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@OrderID", DbParameter.DbType.Int, 400, OrderID),
                new DbParameter("@OrderStatusID", DbParameter.DbType.Int, 40, OrderStatusID)
            };
        DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "OrderStatusChange", dbParam);
    }

    public DataTable OrderDetailsByID(long OrderID)
    {
        DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@OrderID", DbParameter.DbType.Int, 200, OrderID)
            };
        return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "OrderDetailsByID", dbParam);
    }


    public void SaveRefundTransaction(long OrderID, string RefundChargeID, string RefundReason, decimal RefundAmount)
    {
        DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@OrderID", DbParameter.DbType.Int, 400, OrderID),
                new DbParameter("@RefundChargeID", DbParameter.DbType.VarChar, 4000, RefundChargeID),
                new DbParameter("@RefundReason", DbParameter.DbType.VarChar, 4000, RefundReason),
                new DbParameter("@RefundAmount", DbParameter.DbType.Decimal, 4000, RefundAmount)
            };
        DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "OrderRefundStatusChange", dbParam);
    }

    #endregion

    #region Json Response
    public string SetResponse(string strResponse, int Status)
    {
        Response objResponse = new Response();
        if (Status == 0)
        {
            objResponse.success = "false";
        }
        else
        {
            objResponse.success = "true";
        }

        objResponse.message = strResponse;

        return JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
    }
    #endregion
}