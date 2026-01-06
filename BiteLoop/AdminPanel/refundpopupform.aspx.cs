using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BAL;
using Utility;
using DAL;
using Stripe;

public partial class controlpanel_Refundpopupform : AdminAuthentication
{
    private new long ID;
    protected string ENID = "";
    protected int TotalQty = 1;
    protected string OrderID = "";
    //private void BindRewardsPoints()
    //{
    //    UsersBAL objUserBAL = new UsersBAL();
    //    objUserBAL.ID = this.ID;
    //    DataTable dt = new DataTable();

    //    dt = objUserBAL.UserDetailsByID();
    //    if (dt.Rows.Count > 0)
    //    {
    //        rptRefundDetails.DataSource = dt;
    //        rptRefundDetails.DataBind();
    //        divContactUs.Visible = true;
    //    }
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["processrefund"] != null)
        {
            ProcessRefund();
        }
        if (Convert.ToString(Request["type"]) == "userrefund")
        {
            if (Request["id"] != null)
            {
                this.ID = Convert.ToInt64(Utility.Security.Rijndael128Algorithm.DecryptString(Convert.ToString((Request["id"]))));
                ENID = Convert.ToString(Request["id"]);
                OrderID = Convert.ToString(this.ID);
            }

            BindOrderDetails();
            // rptRefundDetails.Visible = true;
            // BindRewardsPoints();

        }
    }
    private void ProcessRefund()
    {
        int Qty = 0;
        string RefundReason = string.Empty;
        long OrderID = 0;
        string RefundType = string.Empty;
        string StripeChargeID = string.Empty;
        string StripeResponse = string.Empty;
        string StripeRefundID = string.Empty;
        decimal StripeRefundAmount = 0;
        decimal SingeQtyAmount = 0;
        decimal GrandTotal = 0;
        
        string BalanceTransactionId = string.Empty;
        string Currency = string.Empty;

        //if (Request["RefundType"] != null)
        //{
        //    RefundType = Convert.ToString(Request["RefundType"]).ToUpper();
        //    OrderID = Convert.ToInt64(Utility.Security.Rijndael128Algorithm.DecryptString(Request["ENID"]));
        //    DataTable dtOrder = new DataTable();
        //    CartBAL objCartBAL = new CartBAL();
        //    dtOrder = objCartBAL.OrderSummary(OrderID);

        //    if (dtOrder.Rows.Count > 0)
        //    {
        //        SingeQtyAmount = (Convert.ToDecimal(dtOrder.Rows[0]["OriginalPrice"]) - Convert.ToDecimal(dtOrder.Rows[0]["DiscountValue"]) - -Convert.ToDecimal(dtOrder.Rows[0]["APPPromocodeDiscountAmount"]));
        //        StripeChargeID = Convert.ToString(dtOrder.Rows[0]["StripeChargeID"]);
        //        GrandTotal = Convert.ToDecimal(dtOrder.Rows[0]["GrandTotal"]);
        //        if (RefundType == "F")
        //        {
        //            StripeRefundAmount = GrandTotal;
        //        }
        //    }
        //    RefundReason = Convert.ToString(Request["RefundReason"]);



        //    if (RefundType == "P")
        //    {
        //        Qty = Convert.ToInt32(Request["Qty"]);
        //        StripeRefundAmount = Qty * SingeQtyAmount;

        //        if (StripeRefundAmount > GrandTotal)
        //        {
        //            StripeRefundAmount = GrandTotal;
        //        }
        //    }

        //    if (StripeChargeID != string.Empty && StripeRefundAmount > 0)
        //    {
        //        StripeConfiguration.SetApiKey(ConfigurationManager.AppSettings["StripeSecretKey"].ToString());
        //        var myToken = new StripeTokenCreateOptions();
        //        var refundService = new StripeRefundService();

        //        var refundOptions = new StripeRefundCreateOptions();
        //        refundOptions.Amount = (int)(StripeRefundAmount * 100);
        //        try
        //        {
        //            var refund = refundService.Create(StripeChargeID, refundOptions);
        //            StripeResponse = refund.Status;
        //            StripeRefundID = refund.Id;
        //            BalanceTransactionId = refund.BalanceTransactionId;
        //            Currency = refund.Currency;
        //        }
        //        catch (Exception ex)
        //        {
        //            StripeResponse = ex.Message.ToString();
        //        }
        //        UpdateUserRewards(OrderID, RefundType, Qty, RefundReason, StripeChargeID, StripeResponse, StripeRefundID, StripeRefundAmount, BalanceTransactionId, Currency);
        //    }

        //    Response.Write(StripeResponse);
        //    Response.End();
        //    // Stripe Refund



        //    //string StripeRefundID = string.Empty;
        //}

    }

    private void BindOrderDetails()
    {
        DataTable dt = new DataTable();
        CartBAL objCartBAL = new CartBAL();

        dt = objCartBAL.OrderSummary(this.ID);

        if (dt.Rows.Count > 0)
        {
            TotalQty = Convert.ToInt32(dt.Rows[0]["Qty"]);
            h4head.InnerText = "Refund - Order ID: " + Convert.ToString(dt.Rows[0]["OrderID"]);
            rptRefundDetails.DataSource = dt;
            rptRefundDetails.DataBind();
            rptRefundDetails.Visible = true;
        }
        divContactUs.Visible = true;
    }

    protected DataTable GetQty(int Qty)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Qty");
        DataRow dr;
        for (int i = 0; i < Qty; i++)
        {
            dr = dt.NewRow();
            dr["Qty"] = Convert.ToString(i + 1);
            dt.Rows.Add(dr);
        }

        return dt;
    }


    public void UpdateUserRewards(long OrderID, string RefundType, int Qty, string RefundReason, string StripeChargeID, string StripeResponse, string StripeRefundID, decimal Amount, string BalanceTransactionId, string Currency)
    {
        DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@OrderID", DbParameter.DbType.Int, 20, OrderID), 
                new DbParameter("@RefundType", DbParameter.DbType.VarChar, 50, RefundType),
                new DbParameter("@Qty", DbParameter.DbType.Int, 500, Qty),
                new DbParameter("@RefundReason", DbParameter.DbType.VarChar, 500, RefundReason),
                new DbParameter("@StripeChargeID", DbParameter.DbType.VarChar, 500, StripeChargeID),
                new DbParameter("@StripeResponse", DbParameter.DbType.VarChar, 500, StripeResponse),
                new DbParameter("@StripeRefundID", DbParameter.DbType.VarChar, 500, StripeRefundID),
                new DbParameter("@Amount", DbParameter.DbType.Decimal, 500, Amount),
                new DbParameter("@BalanceTransactionId", DbParameter.DbType.VarChar, 500, BalanceTransactionId),
                new DbParameter("@Currency", DbParameter.DbType.VarChar, 50, Currency)

            };
        DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "OrderRefund", dbParam);
    }
}
