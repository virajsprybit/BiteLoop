using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using BAL;
using DAL;
using Newtonsoft.Json;
using Utility;

public partial class webservice_delete_business_Image : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Keys.Count > 0)
        {
            DeleteStoreImage();
        }
    }

    private void DeleteStoreImage()
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

        long userID = Convert.ToInt64(Request["UserID"]);
        string imagePath = Convert.ToString(Request["ImagePath"]);  

        if (string.IsNullOrEmpty(imagePath))
        {
            objResponse.success = "false";
            objResponse.message = "Missing ImagePath parameter.";
            WriteResponse(objResponse);
            return;
        }

        try
        {
            string imagePathClean = imagePath;

            if (imagePathClean.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    Uri uri = new Uri(imagePathClean);
                    imagePathClean = uri.AbsolutePath.TrimStart('/');
                }
                catch
                {
                    imagePathClean = imagePath.Replace("http://", "").Replace("https://", "");
                    int index = imagePathClean.IndexOf("/");
                    if (index >= 0)
                        imagePathClean = imagePathClean.Substring(index + 1);
                }
            }

            string physicalPath = HttpContext.Current.Server.MapPath("~/" + imagePathClean);

            if (File.Exists(physicalPath))
            {
                File.Delete(physicalPath);
            }
        }
        catch (Exception ex)
        {
            objResponse.success = "false";
            objResponse.message = "Error deleting file: " + ex.Message;
            WriteResponse(objResponse);
            return;
        }

        long result = DeleteStoreImageFromDB(userID, imagePath);

        if (result > 0)
        {
            objResponse.success = "true";
            objResponse.message = "Store image deleted successfully.";
        }
        else
        {
            objResponse.success = "false";
            objResponse.message = "Failed to update database.";
        }

        WriteResponse(objResponse);
    }

    private void WriteResponse(Response objResponse)
    {
        string json = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        HttpContext.Current.Response.Write(json);
        Response.End();
    }

    #region Database Call
    private long DeleteStoreImageFromDB(long userID, string imagePath)
    {
        DbParameter[] dbParam = new DbParameter[]
        {
            new DbParameter("@UserID", DbParameter.DbType.Int, 4, userID),
            new DbParameter("@ImagePath", DbParameter.DbType.VarChar, 8000, imagePath)
        };

        DataTable dt = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "Business_DeleteStoreImage", dbParam);

        if (dt != null && dt.Rows.Count > 0)
            return Convert.ToInt64(dt.Rows[0]["ReturnVal"]);
        else
            return -1;
    }
    #endregion
}