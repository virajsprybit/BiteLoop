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

public partial class controlpanel_RefundDetailsPopupform : AdminAuthentication
{
    private new long ID;
    protected string ENID = "";
    protected int TotalQty = 1;
    protected string RefundStatus = "";
    protected string RefundReason = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Request["type"]) == "userrefund")
        {
            if (Request["id"] != null)
            {
                this.ID = Convert.ToInt64(Utility.Security.Rijndael128Algorithm.DecryptString(Convert.ToString((Request["id"]))));
                ENID = Convert.ToString(Request["id"]);
            }

            BindOrderDetails();
        }
    }

    private void BindOrderDetails()
    {        
        CartBAL objCartBAL = new CartBAL();
        DataSet ds = new DataSet();
        //dt = objCartBAL.OrderSummary(this.ID);
        ds = OrderSummaryAndRefundDetails(this.ID);


        if (ds.Tables[0].Rows.Count > 0)
        {
            TotalQty = Convert.ToInt32(ds.Tables[0].Rows[0]["Qty"]);
            h4head.InnerText = "Refund - Order ID: " + Convert.ToString(ds.Tables[0].Rows[0]["OrderID"]);
            RefundStatus = Convert.ToString(ds.Tables[0].Rows[0]["OrderStatus"]);
            RefundReason = Convert.ToString(ds.Tables[0].Rows[0]["RefundReason"]);
            
            rptRefundDetails.DataSource = ds.Tables[0];
            rptRefundDetails.DataBind();
            rptRefundDetails.Visible = true;
        }
        if (ds.Tables[1].Rows.Count > 0)
        {
            rptRefund.DataSource = ds.Tables[1];
            rptRefund.DataBind();
            rptRefund.Visible = true;
        }
        divContactUs.Visible = true;
    }

    public DataSet OrderSummaryAndRefundDetails(long OrderID)
    {
        DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@OrderID", DbParameter.DbType.Int, 200, OrderID),                 
            };
        return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "OrderSummaryAndRefundDetails", dbParam);
    }
}
