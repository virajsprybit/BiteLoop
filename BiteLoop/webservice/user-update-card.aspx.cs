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

public partial class webservice_user_update_card : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Keys.Count > 0)
        {
            UserUpdateCard();
        }
    }

    private void UserUpdateCard()
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
            long UserID = 0;
            string ExpiryYear = "";
            string ExpiryMonth = "";
            int IsDefault = 0;
            if (Request["UserID"] != null)
            {
                UserID = Convert.ToInt64(Request["UserID"]);
            }
            long CardID = 0;
            if (Request["CardID"] != null)
            {
                CardID = Convert.ToInt64(Request["CardID"]);
            }

            if (Request["ExpiryYear"] != null)
            {
                ExpiryYear = Convert.ToString(Request["ExpiryYear"]);
            }
            if (Request["ExpiryMonth"] != null)
            {
                ExpiryMonth = Convert.ToString(Request["ExpiryMonth"]);
            }
            if (Request["IsDefault"] != null)
            {
                IsDefault = Convert.ToInt32(Request["IsDefault"]);
            }

            long result = 0;

            DataTable dt = new DataTable();
            UsersBAL objUsersBAL = new UsersBAL();
            objUsersBAL.ID = Convert.ToInt64(UserID);
            dt = objUsersBAL.UserCardList();

            //if (dt.Rows.Count > 0 && ExpiryMonth != "" && ExpiryYear!= "")
            //{
            //    try
            //    {
            //        StripeConfiguration.SetApiKey(ConfigurationManager.AppSettings["StripeSecretKey"].ToString());
            //        var myToken = new StripeTokenCreateOptions();

            //        var options = new StripeCardUpdateOptions
            //        {
            //            ExpirationMonth = Convert.ToInt32(ExpiryMonth),
            //            ExpirationYear = Convert.ToInt32(ExpiryYear)
            //        };

            //        var service = new StripeCardService();
            //        StripeCard csService = service.Update(Convert.ToString(dt.Rows[0]["CustomerID"]), Convert.ToString(dt.Rows[0]["CardID"]), options, false, null);

            //        result = CardUpdate(UserID, CardID, ExpiryMonth, ExpiryYear, IsDefault);
            //    }
            //    catch (Exception ex)
            //    {

            //    }               
               
            //}
            switch (result)
            {

                default:
                    objResponse.success = "true";
                    objResponse.message = "Card has been updated successfully.";
                    break;
            }

            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));

        }
        Response.End();
    }

    private int CardUpdate(long UserID, long CardID, string ExpiryMonth, string ExpiryYear,int IsDefault)
    {
        DbParameter[] dbParam = new DbParameter[] 
        { 
                new DbParameter("@UserID", DbParameter.DbType.Int, 40, UserID),
                new DbParameter("@CardID", DbParameter.DbType.Int, 40, CardID),
                new DbParameter("@ExpiryMonth", DbParameter.DbType.VarChar, 40, ExpiryMonth),
                new DbParameter("@ExpiryYear", DbParameter.DbType.VarChar, 40, ExpiryYear),
                new DbParameter("@IsDefault", DbParameter.DbType.VarChar, 40, IsDefault)
        };
        DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "UserUpdateCard", dbParam);
        return 1;
    }
}