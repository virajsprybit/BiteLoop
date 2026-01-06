using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using Utility;
using System.Data;
using Newtonsoft.Json;

public partial class webservice_admin_business_login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Keys.Count > 0)
        {
            BusinessLogin();
        }
    }

    private void BusinessLogin()
    {
        if (Request["Email"] != null && Request["Password"] != null)
        {
            Response objResponse = new Response();

            SalesAdminBAL objSalesAdminBAL = new SalesAdminBAL();
            objSalesAdminBAL.EmailID = Convert.ToString(Request["Email"]);
            objSalesAdminBAL.Password = Convert.ToString(Utility.Security.EncryptDescrypt.EncryptString(Convert.ToString(Request["Password"])));

            DataTable dt = new DataTable();
            dt = objSalesAdminBAL.SalesAdminLoginCheck();
            if (dt.Rows.Count > 0)
            {
                objResponse.success = "true";
                objResponse.message = "Login Successfull.";
                SalesAdmin[] objSalesAdmin = new SalesAdmin[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    objSalesAdmin[i] = new SalesAdmin();
                    objSalesAdmin[i].UserID = Convert.ToInt64(dt.Rows[i]["ID"]);
                    objSalesAdmin[i].FirtName = Convert.ToString(dt.Rows[i]["FirstName"]);
                    objSalesAdmin[i].LastName = Convert.ToString(dt.Rows[i]["LastName"]);
                    objSalesAdmin[i].Email = Convert.ToString(dt.Rows[i]["EmailID"]);
                    objSalesAdmin[i].Phone = Convert.ToString(dt.Rows[i]["Phone"]);
                    objSalesAdmin[i].AuthToken = Convert.ToString(dt.Rows[i]["AuthToken"]);
                    objSalesAdmin[i].SecretKey = Convert.ToString(Utility.Security.Rijndael128Algorithm.EncryptString(Convert.ToString(dt.Rows[i]["ID"])));
                    objResponse.SalesAdmin = objSalesAdmin;
                }
                string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                
                strResponseName = strResponseName.Replace("\"SalesAdmin\"", "\"data\"");
                HttpContext.Current.Response.Write(strResponseName);
                //HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));

            }
            else
            {
                CommonAPI objCommonAPI = new CommonAPI();
                objCommonAPI.InvalidLogin();
            }
        }
        else
        {
            CommonAPI objCommonAPI = new CommonAPI();
            objCommonAPI.Unauthorized();
        }
        HttpContext.Current.Response.End();
    }

}