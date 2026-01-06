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

public partial class webservice_user_rating_add : System.Web.UI.Page
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
            BusinessBAL objBusinessBAL = new BusinessBAL();
            objBusinessBAL.ID = Convert.ToInt64(Request["BusinessID"]);
            long result = UserRatingSave();


            switch (result)
            {
                default:
                    objResponse.success = "true";
                    objResponse.message = "Rating saved successfully.";
                    break;
            }

            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));

        }
        Response.End();
    }

    public long UserRatingSave()
    {
        DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@UserID", DbParameter.DbType.Int, 400, Convert.ToInt64(Request["UserID"])),
                new DbParameter("@BusinessID", DbParameter.DbType.Int, 400, Convert.ToInt64(Request["BusinessID"])),
                new DbParameter("@Point1", DbParameter.DbType.Decimal, 40, Convert.ToDecimal(Request["Point1Rating"])),
                new DbParameter("@Point2", DbParameter.DbType.Decimal, 40, Convert.ToDecimal(Request["Point2Rating"])),
                new DbParameter("@Point3", DbParameter.DbType.Decimal, 40, Convert.ToDecimal(Request["Point3Rating"])),
                new DbParameter("@Point4", DbParameter.DbType.Decimal, 40, Convert.ToDecimal(Request["Point4Rating"])),
                new DbParameter("@AppRating", DbParameter.DbType.Decimal, 40, Convert.ToDecimal(Request["AppRating"]))
            };
        DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "RatingSave", dbParam);
        return 1;
    }
}