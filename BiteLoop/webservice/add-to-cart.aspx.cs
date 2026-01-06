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

public partial class webservice_add_to_cart : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Keys.Count > 0)
        {
            AddToCart();
        }
    }

    private void AddToCart()
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
            CartBAL objCartBAL = new CartBAL();
            objCartBAL.UserID = Convert.ToInt64(Request["UserID"]);
            objCartBAL.BusinessID = Convert.ToInt64(Request["BusinessID"]);
            objCartBAL.Qty = Convert.ToInt32(Request["Qty"]);
            objCartBAL.Donation = Convert.ToDecimal(Request["Donation"]);
            long PickupTimeID = -1;
            if (Request["PickUpTimeID"] != null)
            {
                PickupTimeID = Convert.ToInt64(Request["PickUpTimeID"]);
            }
            string StateCode = "VIC";
            if (Request["StateCode"] != null)
            {
                StateCode = Convert.ToString(Request["StateCode"]);
            }

            int RemainingQty = 0;

            //long result = objCartBAL.CartAdd(out RemainingQty, PickupTimeID);
            long result = CartAdd(out RemainingQty, PickupTimeID, objCartBAL, StateCode);

            switch (result)
            {
                case -1:
                    objResponse.success = "false";
                    objResponse.message = "Someone has just purchased and remaining quantity is " + RemainingQty + ".";
                    break;
                default:
                    // SendUserMail();                    
                    objResponse.success = "true";
                    objResponse.message = "Product added successfully.";

                    // Cart Summary Start
                    objCartBAL = new CartBAL();
                    objCartBAL.UserID = Convert.ToInt64(Request["UserID"]);
                    objCartBAL.BusinessID = Convert.ToInt64(Request["BusinessID"]);
                    DataTable dt = new DataTable();


                    dt = objCartBAL.CartSummary();

                    CartSummary[] objCartSummary = new CartSummary[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        objCartSummary[i] = new CartSummary();
                        objCartSummary[i].UserID = Convert.ToInt64(dt.Rows[i]["UserID"]);
                        objCartSummary[i].BusinessID = Convert.ToInt64(dt.Rows[i]["BusinessID"]);
                        objCartSummary[i].Email = Convert.ToString(dt.Rows[i]["Email"]);
                        objCartSummary[i].Mobile = Convert.ToString(dt.Rows[i]["Mobile"]);
                        objCartSummary[i].Name = Convert.ToString(dt.Rows[i]["Name"]);
                        objCartSummary[i].Qty = Convert.ToString(dt.Rows[i]["Qty"]);
                        objCartSummary[i].OriginalPrice = Convert.ToDouble(dt.Rows[i]["OriginalPrice"]).ToString("f2");
                        objCartSummary[i].Donation = Convert.ToDouble(dt.Rows[i]["Donation"]).ToString("f2");
                        objCartSummary[i].TransactionFee = Convert.ToDouble(dt.Rows[i]["TransactionFee"]).ToString("f2");
                        objCartSummary[i].BiteloopFee = Convert.ToDouble(dt.Rows[i]["BringMeHomeFee"]).ToString("f2");
                        objCartSummary[i].GrandTotal = Convert.ToDouble(dt.Rows[i]["GrandTotal"]).ToString("f2");
                        objCartSummary[i].ItemPrice = Convert.ToDouble(dt.Rows[i]["ItemPrice"]).ToString("f2");
                        objCartSummary[i].VendorGST = Convert.ToDouble(dt.Rows[i]["VendorGST"]).ToString("f2");

                        objResponse.CartSummary = objCartSummary;
                    }
                    // Cart Summary End

                    break;
            }
            string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            strResponseName = strResponseName.Replace("\"CartSummary\"", "\"data\"");
            HttpContext.Current.Response.Write(strResponseName);
            //HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));

        }
        Response.End();
    }

    public long CartAdd(out int RemainingQty, long PickUpTimeID, CartBAL objCartBAL, string StateCode)
    {
        DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@StateCode", DbParameter.DbType.VarChar, 20, StateCode),
                new DbParameter("@UserID", DbParameter.DbType.Int, 40, objCartBAL.UserID),
                new DbParameter("@BusinessID", DbParameter.DbType.Int, 40, objCartBAL.BusinessID),
                new DbParameter("@Qty", DbParameter.DbType.Int, 500, objCartBAL.Qty),
                new DbParameter("@Donation", DbParameter.DbType.Decimal, 200, objCartBAL.Donation),
                new DbParameter("@RemainingQuantity", DbParameter.DbType.Int, 200, ParameterDirection.Output),
                new DbParameter("@PickUpTimeID", DbParameter.DbType.Int, 20, PickUpTimeID),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output)
            };
        DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "CartAdd", dbParam);
        RemainingQty = Convert.ToInt32(dbParam[dbParam.Length - 3].Value);
        return Convert.ToInt64(dbParam[dbParam.Length - 1].Value);
    }
}