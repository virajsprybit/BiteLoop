using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using Utility;
using Newtonsoft.Json;

public partial class webservice_admin_business_description : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Keys.Count > 0)
        {
            UpdateBusinessDescription();
        }
    }

    private void UpdateBusinessDescription()
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
            BusinessBAL objBusinessBAL = new BusinessBAL();
            objBusinessBAL.ID = Convert.ToInt64(Request["BusinessID"]);
            objBusinessBAL.Description = Convert.ToString(Request["Description"]);
            objBusinessBAL.AboutUs = Convert.ToString(Request["AboutUs"]);

            long result = objBusinessBAL.UpdateBusinessDescription();            

            switch (result)
            {               
                default:
                    // SendUserMail();                    
                      objResponse.success = "true";
                      objResponse.message = "Description updated successfully.";
                    break;
            }

            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
          
        }
        Response.End();
    }
}