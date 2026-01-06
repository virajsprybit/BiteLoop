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

public partial class webservice_user_remove_card : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Keys.Count > 0)
        {
            UserRemoveCard();
        }
    }

    private void UserRemoveCard()
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
            if (Request["UserID"] != null)
            {
                UserID = Convert.ToInt64(Request["UserID"]);
            }
            long CardID = 0;
            if (Request["CardID"] != null)
            {
                CardID = Convert.ToInt64(Request["CardID"]);
            }

            long result = CardRemove(UserID, CardID);

            switch (result)
            {             
               
                default:
                    objResponse.success = "true";
                    objResponse.message = "Card has been removed successfully.";
                    break;
            }

            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
           
        }
        Response.End();
    }

    private int CardRemove(long UserID, long CardID)
    {
        DbParameter[] dbParam = new DbParameter[] 
        { 
                new DbParameter("@UserID", DbParameter.DbType.Int, 40, UserID),
                new DbParameter("@CardID", DbParameter.DbType.Int, 40, CardID)
        };
        DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "UserRemoveCard", dbParam);
        return 1;
    }
}