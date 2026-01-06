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

public partial class webservice_devicekey_update : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Keys.Count > 0)
        {
            UpdateKey();
        }
    }

    private void UpdateKey()
    {
        Response objResponse = new Response();
        bool IsValidated = false;
        

        if (Request["UserID"] != null && Request["SecretKey"] != null && Request["AuthToken"] != null)
        {
            if (ValidateRequestBAL.UserValidateClientRequest(Convert.ToInt64(Request["UserID"]), Convert.ToString(Request["SecretKey"]), Convert.ToString(Request["AuthToken"]), ""))
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

            long result = UpdateUserKey(Convert.ToString(Request["DeviceKey"]), Convert.ToInt64(Request["UserID"]));

            switch (result)
            {
                case -1:
                    objResponse.success = "false";
                    objResponse.message = "Please try after sometime.";
                    break;
                default:
                    // SendUserMail();                    
                    objResponse.success = "true";
                    objResponse.message = "Key updated successfully.";

                    break;
            }
            string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });                        
            HttpContext.Current.Response.Write(strResponseName);
        }
        Response.End();
    }

    public long UpdateUserKey(string DeviceKey, long UserID)
    {
        DbParameter[] dbParam = new DbParameter[] {                 
                new DbParameter("@UserID", DbParameter.DbType.Int, 400, UserID), 
                new DbParameter("@DeviceKey", DbParameter.DbType.VarChar, 80000, DeviceKey)
            };
        DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "UpdateUserKey", dbParam);
        return 1;
    }
}