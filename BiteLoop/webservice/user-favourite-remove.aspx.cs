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

public partial class webservice_user_favourite_remove : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Keys.Count > 0)
        {
            AddToFavourite();
        }
    }

    private void AddToFavourite()
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
            
            long result = UserFavouriteBusinessRemove(Convert.ToInt64(Request["UserID"]), Convert.ToInt64(Request["BusinessID"]));

            switch (result)
            {                
                default:                    
                    objResponse.success = "true";
                    objResponse.message = "Selected business has been removed successfully from your Saved List.";                  
                    break;
            }

            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
          
        }
        Response.End();
    }

    public long UserFavouriteBusinessRemove(long UserID, long BusinessID)
    {
        DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@UserID", DbParameter.DbType.Int, 40, UserID), 
                new DbParameter("@BusinessID", DbParameter.DbType.Int, 40, BusinessID),                 
            };
        DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "UserFavouriteBusinessRemove", dbParam);
        return 1;
    }
}