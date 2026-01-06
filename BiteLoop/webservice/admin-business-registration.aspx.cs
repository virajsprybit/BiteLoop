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
using System.IO;

public partial class webservice_admin_business_registration : System.Web.UI.Page
{
    protected override void Render(HtmlTextWriter writer)
    {
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Keys.Count > 0)
        {
            BusinessRegistration();
        }
    }

    private void BusinessRegistration()
    {
        Response objResponse = new Response();
        bool IsValidated = false;

        if (Request["UserID"] != null && Request["SecretKey"] != null && Request["AuthToken"] != null)
        {
            if (ValidateRequestBAL.BusinessValidateClientRequest(
                    Convert.ToInt64(Request["UserID"]),
                    Convert.ToString(Request["SecretKey"]),
                    Convert.ToString(Request["AuthToken"])))
            {
                IsValidated = true;
            }
        }

        if (!IsValidated)
        {
            CommonAPI objCommonAPI = new CommonAPI();
            objCommonAPI.Unauthorized();
            Response.End();
            return;
        }

        BusinessBAL objBusinessBAL = new BusinessBAL();

        objBusinessBAL.ID = Convert.ToInt64(Request["UserID"]);
        objBusinessBAL.AuthToken = Convert.ToString(Request["AuthToken"]);
        //objBusinessBAL.ProfilePhoto = Request["ProfilePhoto"] != null ? Convert.ToString(Request["ProfilePhoto"]) : string.Empty;
        string photoPath = string.Empty;
        if (Request.Files.Count > 0)
        {
            HttpPostedFile file = Request.Files["ProfilePhoto"];
            if (file != null && file.ContentLength > 0)
            {
                string folderPath = Server.MapPath("~/source/CMSFiles/");
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                string extension = Path.GetExtension(file.FileName);
                string uniqueName = fileName + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + extension;
                string fullPath = Path.Combine(folderPath, uniqueName);

                file.SaveAs(fullPath);

                photoPath = uniqueName;
            }
        }
        objBusinessBAL.ProfilePhoto = photoPath;

        List<string> storeImagePaths = new List<string>();

        foreach (string key in Request.Files.AllKeys)
        {
            if (key.StartsWith("StoreImages"))
            {
                HttpPostedFile storeImage = Request.Files[key];
                if (storeImage != null && storeImage.ContentLength > 0)
                {
                    string folderPath = Server.MapPath("~/source/CMSFiles/");
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    string fileName = Path.GetFileNameWithoutExtension(storeImage.FileName);
                    string extension = Path.GetExtension(storeImage.FileName);
                    string uniqueName = fileName + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + extension;
                    string fullPath = Path.Combine(folderPath, uniqueName);
                    storeImage.SaveAs(fullPath);

                    storeImagePaths.Add(uniqueName);
                }
            }
        }

        objBusinessBAL.StoreImages = string.Join(",", storeImagePaths);

        objBusinessBAL.ProfilePhotoID = 0;
        objBusinessBAL.ProfilePhotoID = Convert.ToInt32(Request["ProfilePhotoID"]);
        objBusinessBAL.Description = Convert.ToString(Request["Description"]);
        objBusinessBAL.BSBNo = Convert.ToString(Request["BSBNo"]);
        objBusinessBAL.AccountNumber = Convert.ToString(Request["AccountNumber"]);
        objBusinessBAL.BankName = Convert.ToString(Request["BankName"]);
        objBusinessBAL.AccountName = Convert.ToString(Request["AccountName"]);
        objBusinessBAL.BusinessType = Convert.ToString(Request["BusinessType"]);
        objBusinessBAL.FoodItems = Convert.ToString(Request["FoodItems"]);
        objBusinessBAL.AboutUs = Convert.ToString(Request["AboutUs"]);

        byte registerGst = 0; 
        if (!string.IsNullOrEmpty(Request["RegisterGst"]))
        {
            byte.TryParse(Request["RegisterGst"], out registerGst);
        }
        objBusinessBAL.RegisterGst = registerGst;

        
        byte categoryTaxItemOrNot = 1; 
        if (!string.IsNullOrEmpty(Request["CategoryTaxItemOrNot"]))
        {
            byte.TryParse(Request["CategoryTaxItemOrNot"], out categoryTaxItemOrNot);
        }
        objBusinessBAL.CategoryTaxItemOrNot = categoryTaxItemOrNot;

        // Read multiple diet IDs (comma-separated)
        string dietIdsRaw = Request["DietryID"];
        if (!string.IsNullOrEmpty(dietIdsRaw))
        {
            // Clean and remove empty items
            var cleanIds = dietIdsRaw
                .Split(',')
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim())
                .Distinct();

            objBusinessBAL.DietryIDs = string.Join(",", cleanIds);
        }
        else
        {
            objBusinessBAL.DietryIDs = string.Empty;
        }



        long result = BusinessBankUpdate(objBusinessBAL);

        switch (result)
        {
            case -1:
                objResponse.success = "false";
                objResponse.message = "Business not found or update failed.";
                break;

            default:
                objResponse.success = "true";
                objResponse.message = "Registration Successful.";

                BusinessID[] objBusinessID = new BusinessID[1];
                objBusinessID[0] = new BusinessID();
                objBusinessID[0].ID = Convert.ToInt64(result);
                objResponse.BusinessID = objBusinessID;
                break;
        }

        string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        strResponseName = strResponseName.Replace("\"BusinessID\"", "\"data\"");
        HttpContext.Current.Response.Write(strResponseName);
        Response.End();
    }

    #region Database Call

    private long BusinessBankUpdate(BusinessBAL objBusinessBAL)
    {
        DbParameter[] dbParam = new DbParameter[]
        {
        new DbParameter("@ID", DbParameter.DbType.Int, 4, objBusinessBAL.ID),
        new DbParameter("@ProfilePhotoID", DbParameter.DbType.Int, 4, objBusinessBAL.ProfilePhotoID),
        new DbParameter("@ProfilePhoto", DbParameter.DbType.VarChar, 500, objBusinessBAL.ProfilePhoto),
        new DbParameter("@StoreImages", DbParameter.DbType.VarChar, 8000, objBusinessBAL.StoreImages),
        new DbParameter("@DietryIDs", DbParameter.DbType.VarChar, 1000, objBusinessBAL.DietryIDs),
        new DbParameter("@Description", DbParameter.DbType.VarChar, 8000, objBusinessBAL.Description),
        new DbParameter("@AboutUs", DbParameter.DbType.VarChar, 8000, objBusinessBAL.AboutUs),
        new DbParameter("@BSBNo", DbParameter.DbType.VarChar, 200, objBusinessBAL.BSBNo),
        new DbParameter("@AccountNumber", DbParameter.DbType.VarChar, 200, objBusinessBAL.AccountNumber),
        new DbParameter("@BankName", DbParameter.DbType.VarChar, 200, objBusinessBAL.BankName),
        new DbParameter("@AccountName", DbParameter.DbType.VarChar, 200, objBusinessBAL.AccountName),
        new DbParameter("@BusinessType", DbParameter.DbType.VarChar, 500, objBusinessBAL.BusinessType),
        new DbParameter("@FoodItems", DbParameter.DbType.VarChar, 500, objBusinessBAL.FoodItems),
        new DbParameter("@RegisterGst", DbParameter.DbType.Int, 4, objBusinessBAL.RegisterGst),
        new DbParameter("@CategoryTaxItemOrNot", DbParameter.DbType.Int, 4, objBusinessBAL.CategoryTaxItemOrNot),
        new DbParameter("@AuthToken", DbParameter.DbType.VarChar, 500, objBusinessBAL.AuthToken)

        };

        DataTable dt = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "BusinessBankUpdate", dbParam);

        if (dt != null && dt.Rows.Count > 0)
            return Convert.ToInt64(dt.Rows[0]["BusinessID"]);
        else
            return -1;
    }

    #endregion
}