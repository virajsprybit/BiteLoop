using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using BAL;
using PAL;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using Utility;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using BiteLoop.Common;
using DAL;

/// <summary>
/// Summary description for CommonAPI
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class CommonAPI : System.Web.Services.WebService
{

    public CommonAPI()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    public void Error(string message)
    {
        HttpContext.Current.Response.ContentType = "application/json";
        HttpContext.Current.Response.Write(
            "{\"success\":\"false\",\"message\":\"" + message + "\",\"StatusCode\":\"500\"}"
        );
        HttpContext.Current.Response.End();
    }

    #region Common API

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void AppleUserDetails(string AppleID)
    {
        string strResponse = string.Empty;
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        try
        {
            Response objResponse = new Response();
            DataTable dt = new DataTable();

            dt = AppleUserListDetails(AppleID);

            if (dt.Rows.Count > 0)
            {
                objResponse.success = "true";
                objResponse.message = dt.Rows.Count + " records found.";

                AppleUserDetails[] objAppleUserDetails = new AppleUserDetails[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    objAppleUserDetails[i] = new AppleUserDetails();

                    objAppleUserDetails[i].AppleID = Convert.ToString(dt.Rows[i]["AppleID"]);
                    objAppleUserDetails[i].FirstName = Convert.ToString(dt.Rows[i]["FirstName"]);
                    objAppleUserDetails[i].LastName = Convert.ToString(dt.Rows[i]["LastName"]);
                    objAppleUserDetails[i].Email = Convert.ToString(dt.Rows[i]["Email"]);
                    objResponse.AppleUserDetails = objAppleUserDetails;
                }
                string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                strResponseName = strResponseName.Replace("\"AppleUserDetails\"", "\"data\"");
                HttpContext.Current.Response.Write(strResponseName);
            }
            else
            {
                NoRecordExists();
            }
        }
        catch (Exception Ex)
        {
            ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
        }
        HttpContext.Current.Response.End();
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void RewardsImpactDetails()
    {
        string strResponse = string.Empty;
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        try
        {
            Response objResponse = new Response();
            objResponse.success = "true";
            objResponse.message = "1 records found.";

            Settings[] objSettings = new Settings[1];
            for (int i = 0; i < 1; i++)
            {
                objSettings[i] = new Settings();
                string strValue = "";
                strValue += "Track your individual impact, savings and rewards that you’ve earned through rescuing food with Biteloop.<br/><br/>";
                strValue += "Every time you rescue a meal, you earn rewards points that will help you save even more money.<br/><br/>";
                strValue += "You earn 100 points for every dollar spent on the app. For every 5000 points you earn, you can redeem $1 of credit to save on your next meal. Please note that credits can only be redeemed when you’ve reached a full dollar amount.<br/><br/>";
                strValue += "Happy saving and enjoy your meal!<br/><br/>";
                strValue += "For more information visit our website. T&Cs apply.";

                objSettings[i].Title = "Rewards & Impact";
                objSettings[i].Description = strValue;
                objResponse.Settings = objSettings;
            }
            string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            strResponseName = strResponseName.Replace("\"Settings\"", "\"data\"");
            HttpContext.Current.Response.Write(strResponseName);


        }
        catch (Exception Ex)
        {
            ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
        }
        HttpContext.Current.Response.End();
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void GetSettingsKey(string Key)
    {
        //PrivacyURL
        //FAQURL
        //AndroidVersion
        //IOSVersion
        string strResponse = string.Empty;
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        try
        {
            Response objResponse = new Response();
            objResponse.success = "true";
            objResponse.message = "1 records found.";

            Settings[] objSettings = new Settings[1];
            for (int i = 0; i < 1; i++)
            {
                objSettings[i] = new Settings();
                string strValue = "";
                if (Key.ToLower() == "privacyurl")
                {
                    strValue = Convert.ToString(new GeneralSettings().getConfigValue("TelephoneNo"));
                }
                if (Key.ToLower() == "faqurl")
                {
                    strValue = Convert.ToString(new GeneralSettings().getConfigValue("tweeterfeedurl"));
                }
                if (Key.ToLower() == "androidversion")
                {
                    strValue = Convert.ToString(new GeneralSettings().getConfigValue("externalvedio"));
                }
                if (Key.ToLower() == "iosversion")
                {
                    strValue = Convert.ToString(new GeneralSettings().getConfigValue("copyright"));

                }
                if (Key.ToLower() == "aboutusurl")
                {
                    strValue = Config.WebSiteUrl + "about-us.aspx";
                }
                if (Key.ToLower() == "redeempopuptext")
                {
                    strValue = Convert.ToString(new GeneralSettings().getConfigValue("tradinghrs"));
                }
                if (Key.ToLower() == "proirity")
                {
                    strValue = Convert.ToString(new GeneralSettings().getConfigValue("contactaddress1"));
                }
                if (Key.ToLower() == "referfriendtext")
                {
                    strValue = Convert.ToString(new GeneralSettings().getConfigValue("ReferralCodeAmount"));
                    strValue = "Refer Friends, Get $" + strValue;

                }
                if (Key.ToLower() == "referscreentext")
                {
                    strValue = Convert.ToString(new GeneralSettings().getConfigValue("ReferralCodeAmount"));
                    strValue = "Share your code with friends! Once they've signed up and applied the code, they'll get $" + strValue + " to spend on rescuing food and you'll get $" + strValue + " right after they've made their first purchase.";

                }
                objSettings[i].Title = Key;
                objSettings[i].Description = strValue;
                objResponse.Settings = objSettings;
            }
            string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            strResponseName = strResponseName.Replace("\"Settings\"", "\"data\"");
            HttpContext.Current.Response.Write(strResponseName);


        }
        catch (Exception Ex)
        {
            ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
        }
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void SubscriptionDetails()
    {
        string strResponse = string.Empty;
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        try
        {
            Response objResponse = new Response();
            objResponse.success = "true";
            objResponse.message = "1 records found.";

            SubscriptionDetails objSubscriptionDetails = new SubscriptionDetails();

            objSubscriptionDetails.GST = Convert.ToString(new GeneralSettings().getConfigValue("title"));
            objSubscriptionDetails.SubscriptionCharge = Convert.ToString(new GeneralSettings().getConfigValue("contactus"));
            objResponse.SubscriptionDetails = objSubscriptionDetails;

            string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            strResponseName = strResponseName.Replace("\"SubscriptionDetails\"", "\"data\"");
            HttpContext.Current.Response.Write(strResponseName);
        }
        catch (Exception Ex)
        {
            ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
        }
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void GetPriority(string DeviceType)
    {
        string strResponse = string.Empty;
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        try
        {
            string currentVersion = "";
            if (HttpContext.Current.Request["currentVersion"] != null)
            {
                currentVersion = Convert.ToString(HttpContext.Current.Request["currentVersion"]);
            }


            Response objResponse = new Response();
            objResponse.success = "true";
            objResponse.message = "1 records found.";

            Settings[] objSettings = new Settings[1];
            for (int i = 0; i < 1; i++)
            {
                objSettings[i] = new Settings();
                string strValue = "";
                string strCurrentVersion = "";
                if (DeviceType.ToLower() == "android")
                {
                    //-- android currentVersion externalvedio
                    strValue = Convert.ToString(new GeneralSettings().getConfigValue("contactaddress1"));
                    strCurrentVersion = Convert.ToString(new GeneralSettings().getConfigValue("externalvedio"));
                }
                else
                {
                    // IOS currentVersion 
                    strValue = Convert.ToString(new GeneralSettings().getConfigValue("description"));
                    strCurrentVersion = Convert.ToString(new GeneralSettings().getConfigValue("copyright"));
                }

                // Remove This Block
                // if ((currentVersion == "3.0" || currentVersion == "3.1" || currentVersion == "3.2") && DeviceType.ToLower() == "android")
                // {
                //     strValue = "Low";
                // }
                //  else if (currentVersion == strCurrentVersion)
                //  {
                //      strValue = "Low";
                //  }
                // Remove This Block

                //OPen THis Block
                if (currentVersion == strCurrentVersion)
                {
                    strValue = "Low";
                }
                //OPen THis Block
                objSettings[i].Title = "Priority";
                objSettings[i].Description = strValue;
                objResponse.Settings = objSettings;

                // WriteLogFile(objSettings[i].Title + " --> " + objSettings[i].Description);
            }


            string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            strResponseName = strResponseName.Replace("\"Settings\"", "\"data\"");
            HttpContext.Current.Response.Write(strResponseName);

        }
        catch (Exception Ex)
        {
            ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
        }
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void GetBusinessPriority(string DeviceType)
    {
        string strResponse = string.Empty;
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        try
        {
            string currentVersion = "";
            if (HttpContext.Current.Request["currentVersion"] != null)
            {
                currentVersion = Convert.ToString(HttpContext.Current.Request["currentVersion"]);
            }


            Response objResponse = new Response();
            objResponse.success = "true";
            objResponse.message = "1 records found.";

            Settings[] objSettings = new Settings[1];
            for (int i = 0; i < 1; i++)
            {
                objSettings[i] = new Settings();
                string strValue = "";
                string strCurrentVersion = "";
                if (DeviceType.ToLower() == "android")
                {
                    //-- android currentVersion externalvedio
                    strValue = Convert.ToString(new GeneralSettings().getConfigValue("contactaddress2"));
                    strCurrentVersion = Convert.ToString(new GeneralSettings().getConfigValue("twitter"));
                }
                else
                {
                    // IOS currentVersion 
                    strValue = Convert.ToString(new GeneralSettings().getConfigValue("googlemapaddress2"));
                    strCurrentVersion = Convert.ToString(new GeneralSettings().getConfigValue("googlemapaddress1"));
                }

                if (currentVersion == strCurrentVersion)
                {
                    strValue = "Low";
                }
                objSettings[i].Title = "Priority";
                objSettings[i].Description = strValue;
                objResponse.Settings = objSettings;
            }


            string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            //WriteLogFile(strResponseName);
            strResponseName = strResponseName.Replace("\"Settings\"", "\"data\"");
            HttpContext.Current.Response.Write(strResponseName);


        }
        catch (Exception Ex)
        {
            ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
        }
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void CategoryList()
    {
        string strResponse = string.Empty;
        try
        {
            Response objResponse = new Response();

            DataTable dt = new DataTable();
            int CurrentPage = 1, TotalRecord = 0;
            CategoryBAL objCategoryBAL = new CategoryBAL();
            //dt = objCategoryBAL.GetList(ref CurrentPage, 1000, out TotalRecord, "Name", "ASC");
            // First: get your table
            dt = objCategoryBAL.GetList(ref CurrentPage, 1000, out TotalRecord, "Name", "ASC");

            // Create a new DataTable with same structure
            DataTable newDt = dt.Clone();

            // Add all rows EXCEPT "Other" first
            foreach (DataRow row in dt.Rows)
            {
                if (row["Name"].ToString().Trim().ToLower() != "other")
                    newDt.ImportRow(row);
            }

            // Add "Other" rows at the end
            foreach (DataRow row in dt.Rows)
            {
                if (row["Name"].ToString().Trim().ToLower() == "other")
                    newDt.ImportRow(row);
            }

            // Replace dt with reordered table
            dt = newDt;


            if (dt.Rows.Count > 0)
            {
                objResponse.success = "true";
                objResponse.message = dt.Rows.Count + " records found.";

                Category[] objCategory = new Category[dt.Rows.Count];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    objCategory[i] = new Category();

                    objCategory[i].IndexID = i;
                    objCategory[i].ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    objCategory[i].Name = Convert.ToString(dt.Rows[i]["Name"]);
                    objCategory[i].CategoryURL = Convert.ToString(dt.Rows[i]["CategoryURL"]);

                    string imageName = Convert.ToString(dt.Rows[i]["ImageName"]);

                    if (!string.IsNullOrEmpty(imageName) && Char.IsDigit(imageName[0]))
                    {
                        objCategory[i].Image = Config.WebSiteUrl + Config.CMSFiles + imageName;
                    }
                    else
                    {
                        objCategory[i].Image = "";
                    }
                }

                objResponse.Category = objCategory;

                string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Include
                });

                strResponseName = strResponseName.Replace("\"Category\"", "\"data\"");
                HttpContext.Current.Response.Write(strResponseName);
            }
            else
            {
                NoRecordExists();
            }
        }
        catch (Exception Ex)
        {
            ExceptionTRack(Ex.Message.ToString());
            HttpContext.Current.Response.End();
        }

        HttpContext.Current.Response.End();
    }



    //[WebMethod]
    //[ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    //public void CategoryList()
    //{
    //    string strResponse = string.Empty;
    //    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
    //    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
    //    try
    //    {
    //        Response objResponse = new Response();

    //        DataTable dt = new DataTable();
    //        int CurrentPage = 1, TotalRecord = 0;
    //        CategoryBAL objCategoryBAL = new CategoryBAL();
    //        dt = objCategoryBAL.GetList(ref CurrentPage, 1000, out TotalRecord, "Name", "ASC");

    //        if (dt.Rows.Count > 0)
    //        {
    //            objResponse.success = "true";
    //            objResponse.message = dt.Rows.Count + " records found.";

    //            Category[] objCategory = new Category[dt.Rows.Count];
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                objCategory[i] = new Category();

    //                objCategory[i].IndexID = Convert.ToInt32(i);
    //                objCategory[i].ID = Convert.ToInt32(dt.Rows[i]["ID"]);
    //                objCategory[i].Name = Convert.ToString(dt.Rows[i]["Name"]);
    //                objCategory[i].Image = Config.WebSiteUrl + Config.CMSFiles + Convert.ToString(dt.Rows[i]["ImageName"]);
    //                objCategory[i].CategoryURL = Convert.ToString(dt.Rows[i]["CategoryURL"]);

    //                objResponse.Category = objCategory;
    //            }
    //            string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
    //            strResponseName = strResponseName.Replace("\"Category\"", "\"data\"");
    //            HttpContext.Current.Response.Write(strResponseName);

    //        }
    //        else
    //        {
    //            NoRecordExists();
    //        }
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
    //    }
    //    HttpContext.Current.Response.End();
    //}

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void DiscountList()
    {
        string strResponse = string.Empty;
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        try
        {
            Response objResponse = new Response();


            DataTable dt = new DataTable();
            int CurrentPage = 1, TotalRecord = 0;
            DiscountBAL objDiscountBAL = new DiscountBAL();
            dt = objDiscountBAL.GetList(ref CurrentPage, 1000, out TotalRecord, "Discount", "ASC");

            if (dt.Rows.Count > 0)
            {
                objResponse.success = "true";
                objResponse.message = dt.Rows.Count + " records found.";

                Discount[] objDiscount = new Discount[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    objDiscount[i] = new Discount();
                    objDiscount[i].ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    objDiscount[i].DiscountValue = Convert.ToString(dt.Rows[i]["Discount"]).Replace(".00", string.Empty);
                    objResponse.Discount = objDiscount;
                }
                string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                strResponseName = strResponseName.Replace("\"Discount\"", "\"data\"");
                HttpContext.Current.Response.Write(strResponseName);
                //HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));

            }
            else
            {
                NoRecordExists();
            }
        }
        catch (Exception Ex)
        {
            ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
        }
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void ProfilePhotoList(long UserID)
    {
        string strResponse = string.Empty;
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        try
        {
            Response objResponse = new Response();
            DataTable dt = new DataTable();

            ProfilePhotoBAL objProfilePhotoBAL = new ProfilePhotoBAL();
            dt = objProfilePhotoBAL.ProfilePhotoListByUserID(UserID);

            if (dt.Rows.Count > 0)
            {
                objResponse.success = "true";
                objResponse.message = dt.Rows.Count + " records found.";

                ProfilePhoto[] objProfilePhoto = new ProfilePhoto[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    objProfilePhoto[i] = new ProfilePhoto();
                    objProfilePhoto[i].ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    objProfilePhoto[i].Name = Convert.ToString(dt.Rows[i]["Name"]);
                    objProfilePhoto[i].Image = Config.WebSiteUrl + Config.CMSFiles + Convert.ToString(dt.Rows[i]["ImageName"]);
                    objProfilePhoto[i].IsSelected = Convert.ToInt32(dt.Rows[i]["IsSelected"]);
                    objResponse.ProfilePhoto = objProfilePhoto;
                }

                string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                strResponseName = strResponseName.Replace("\"ProfilePhoto\"", "\"data\"");
                HttpContext.Current.Response.Write(strResponseName);
                //HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));

            }
            else
            {
                NoRecordExists();
            }
        }
        catch (Exception Ex)
        {
            ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
        }
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void CmsContent(int ID)
    {
        string strResponse = string.Empty;
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        try
        {
            Response objResponse = new Response();


            DataTable dt = new DataTable();
            CMSBAL objCMSBAL = new CMSBAL();
            objCMSBAL.ID = ID;
            dt = objCMSBAL.GetCMSByID_Webservice();

            if (dt.Rows.Count > 0)
            {
                objResponse.success = "true";
                objResponse.message = dt.Rows.Count + " records found.";

                CMS[] objCMS = new CMS[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    objCMS[i] = new CMS();
                    objCMS[i].ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    objCMS[i].Title = Convert.ToString(dt.Rows[i]["PageTitle"]);

                    objCMS[i].Description = Convert.ToString(dt.Rows[i]["PageDescription"]).Replace("{%WebSiteUrl%}", Config.WebSiteUrl);

                    if (Convert.ToString(dt.Rows[i]["ImageName"]) != string.Empty)
                        objCMS[i].Image = Config.WebSiteUrl + Config.CMSFiles + Convert.ToString(dt.Rows[i]["ImageName"]);
                    else
                        objCMS[i].Image = "";

                    objResponse.CMS = objCMS;
                }

                string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                strResponseName = strResponseName.Replace("\"CMS\"", "\"data\"");
                HttpContext.Current.Response.Write(strResponseName);

            }
            else
            {
                NoRecordExists();
            }
        }
        catch (Exception Ex)
        {
            ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
        }
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void FoodItemsList()
    {
        string strResponse = string.Empty;
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        try
        {
            Response objResponse = new Response();


            DataTable dt = new DataTable();
            int CurrentPage = 1, TotalRecord = 0;
            FoodItemsBAL objFoodItemsBAL = new FoodItemsBAL();
            dt = objFoodItemsBAL.GetList(ref CurrentPage, 1000, out TotalRecord, "Name", "ASC");

            if (dt.Rows.Count > 0)
            {
                objResponse.success = "true";
                objResponse.message = dt.Rows.Count + " records found.";

                FoodItems[] objFoodItems = new FoodItems[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    objFoodItems[i] = new FoodItems();
                    objFoodItems[i].ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    objFoodItems[i].Name = Convert.ToString(dt.Rows[i]["Name"]);
                    objResponse.FoodItems = objFoodItems;
                }
                string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                strResponseName = strResponseName.Replace("\"FoodItems\"", "\"data\"");
                HttpContext.Current.Response.Write(strResponseName);
                //HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));

            }
            else
            {
                NoRecordExists();
            }
        }
        catch (Exception Ex)
        {
            ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
        }
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void DonationsList()
    {
        string strResponse = string.Empty;
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        try
        {
            Response objResponse = new Response();


            DataTable dt = new DataTable();
            int CurrentPage = 1, TotalRecord = 0;
            DonationsBAL objDonationsBAL = new DonationsBAL();

            dt = objDonationsBAL.GetList(ref CurrentPage, 1000, out TotalRecord, "SequenceNo", "ASC");

            if (dt.Rows.Count > 0)
            {
                objResponse.success = "true";
                objResponse.message = dt.Rows.Count + " records found.";

                Donations[] objDonations = new Donations[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    objDonations[i] = new Donations();
                    objDonations[i].ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    objDonations[i].Amount = Convert.ToString(dt.Rows[i]["Donation"]);
                    objResponse.Donations = objDonations;
                }
                string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                strResponseName = strResponseName.Replace("\"Donations\"", "\"data\"");
                HttpContext.Current.Response.Write(strResponseName);

            }
            else
            {
                NoRecordExists();
            }
        }
        catch (Exception Ex)
        {
            ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
        }
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void VerifyBusinessEmail(string EmailAddress)
    {
        string strResponse = string.Empty;
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        try
        {
            Response objResponse = new Response();
            if (ValidateRequestBAL.VerifyBusinessEmail(EmailAddress))
            {
                objResponse.success = "false";
                objResponse.message = "Email Address Already Exists.";
            }
            else
            {
                objResponse.success = "true";
                objResponse.message = "Email Address Verified.";
            }
            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
        }
        catch (Exception Ex)
        {
            ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
        }
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void BusinessLocationAutocomplete(string keyword)
    {
        string strResponse = string.Empty;
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        try
        {
            Response objResponse = new Response();
            DataTable dt = new DataTable();

            string StateCode = "VIC";
            if (HttpContext.Current.Request["StateCode"] != null)
            {
                StateCode = Convert.ToString(HttpContext.Current.Request["StateCode"]).ToUpper();
            }


            dt = GetDonationsByID(keyword, StateCode);

            if (dt.Rows.Count > 0)
            {
                objResponse.success = "true";
                objResponse.message = dt.Rows.Count + " records found.";

                BusinessLocation[] objBusinessLocation = new BusinessLocation[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    objBusinessLocation[i] = new BusinessLocation();
                    objBusinessLocation[i].Title = Convert.ToString(dt.Rows[i]["Title"]);
                    objBusinessLocation[i].ID = Convert.ToInt64(dt.Rows[i]["ID"]);
                    if (Convert.ToString(dt.Rows[i]["order_indicator"]) == "1")
                    {
                        objBusinessLocation[i].IndicatorType = "L";
                    }
                    else
                    {
                        objBusinessLocation[i].IndicatorType = "B";
                    }

                    objResponse.BusinessLocation = objBusinessLocation;
                }
                string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                strResponseName = strResponseName.Replace("\"BusinessLocation\"", "\"data\"");
                HttpContext.Current.Response.Write(strResponseName);

            }
            else
            {
                NoRecordExists();
            }
        }
        catch (Exception Ex)
        {
            ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
        }
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void RestaurantTypesList()
    {
        try
        {
            Response objResponse = new Response();
            RestaurantTypesBAL objRestaurantTypesBAL = new RestaurantTypesBAL();

            int CurrentPage = 1, TotalRecord = 0;
            DataTable dt = objRestaurantTypesBAL.GetList(ref CurrentPage, 1000, out TotalRecord, "Name", "ASC");
            DataTable newDt = dt.Clone();

            foreach (DataRow row in dt.Rows)
            {
                if (row["Name"].ToString().Trim().ToLower() != "other")
                    newDt.ImportRow(row);
            }

            foreach (DataRow row in dt.Rows)
            {
                if (row["Name"].ToString().Trim().ToLower() == "other")
                    newDt.ImportRow(row);
            }

            dt = newDt;


            if (dt.Rows.Count > 0)
            {
                objResponse.success = "true";
                objResponse.message = dt.Rows.Count + " records found.";
                objResponse.StatusCode = "200";

                RestaurantTypes[] objRestaurantTypes = new RestaurantTypes[dt.Rows.Count];

                string cmsPath = Config.WebSiteUrl + Config.CMSFiles;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    objRestaurantTypes[i] = new RestaurantTypes();
                    objRestaurantTypes[i].ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    objRestaurantTypes[i].Name = Convert.ToString(dt.Rows[i]["Name"]);
                    objRestaurantTypes[i].Description = Convert.ToString(dt.Rows[i]["Description"]);


                    string imageName = Convert.ToString(dt.Rows[i]["ImageName"]).Trim();

                    if (!string.IsNullOrEmpty(imageName))
                    {
                        if (!imageName.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                            imageName = cmsPath + imageName;
                    }
                    else
                    {
                        imageName = "";
                    }

                    objRestaurantTypes[i].ImageName = imageName;
                }

                objResponse.RestaurantTypes = objRestaurantTypes;

                string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

                strResponseName = strResponseName.Replace("\"RestaurantTypes\"", "\"data\"");
                HttpContext.Current.Response.Write(strResponseName);
            }
            else
            {
                NoRecordExists();
            }
        }
        catch (Exception Ex)
        {
            ExceptionTRack(Ex.Message.ToString());
        }
        finally
        {
            HttpContext.Current.Response.End();
        }
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void DietaryTypesList()
    {
        try
        {
            Response objResponse = new Response();

            DataTable dt = DbConnectionDAL.GetDataTable(
                CommandType.StoredProcedure,
                "DietaryTypesList",
                new DbParameter[] { }
            );

            if (dt == null || dt.Rows.Count == 0)
            {
                NoRecordExists();
                return;
            }

            objResponse.success = "true";
            objResponse.message = dt.Rows.Count + " records found.";
            objResponse.StatusCode = "200";

            DietaryTypesModel[] arr = new DietaryTypesModel[dt.Rows.Count];
            string cmsPath = Config.WebSiteUrl + Config.CMSFiles;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                arr[i] = new DietaryTypesModel();

                arr[i].ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                arr[i].DietType = dt.Rows[i]["Name"].ToString();
                arr[i].Description = dt.Rows[i]["Description"] == DBNull.Value ? "" : dt.Rows[i]["Description"].ToString();

                string img = dt.Rows[i]["ImageName"] == DBNull.Value ? "" : dt.Rows[i]["ImageName"].ToString();
                if (!string.IsNullOrWhiteSpace(img) && !img.StartsWith("http"))
                    img = cmsPath + img;

                arr[i].ImageName = img;
            }

            var finalJson = new
            {
                success = "true",
                StatusCode = "200",
                message = dt.Rows.Count + " records found.",
                data = arr
            };

            string json = JsonConvert.SerializeObject(
                finalJson,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
            );

            HttpContext.Current.Response.Write(json);

        }
        catch (Exception Ex)
        {
            // Log error if needed
        }

        finally
        {
            HttpContext.Current.Response.End();
        }
    }

    //[WebMethod]
    //[ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    //public void RestaurantTypesList()
    //{
    //    string strResponse = string.Empty;
    //    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
    //    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
    //    try
    //    {
    //        Response objResponse = new Response();
    //        DataTable dt = new DataTable();
    //        RestaurantTypesBAL objRestaurantTypesBAL = new RestaurantTypesBAL();

    //        // 👇 Replace old method — it doesn’t return IconUrl/ImageName
    //        int CurrentPage = 1, TotalRecord = 0;
    //        dt = objRestaurantTypesBAL.GetList(ref CurrentPage, 1000, out TotalRecord, "Name", "ASC");

    //        if (dt.Rows.Count > 0)
    //        {
    //            objResponse.success = "true";
    //            objResponse.message = dt.Rows.Count + " records found.";
    //            objResponse.StatusCode = "200";

    //            RestaurantTypes[] objRestaurantTypes = new RestaurantTypes[dt.Rows.Count];
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                objRestaurantTypes[i] = new RestaurantTypes();
    //                objRestaurantTypes[i].ID = Convert.ToInt32(dt.Rows[i]["ID"]);
    //                objRestaurantTypes[i].Name = Convert.ToString(dt.Rows[i]["Name"]);
    //                //objRestaurantTypes[i].IconUrl = Convert.ToString(dt.Rows[i]["IconUrl"]);
    //                objRestaurantTypes[i].ImageName = Convert.ToString(dt.Rows[i]["ImageName"]);

    //                // (optional: if you want full image URL)
    //                // objRestaurantTypes[i].FullImagePath = Config.WebSiteUrl + Config.CMSFiles + Convert.ToString(dt.Rows[i]["ImageName"]);
    //            }

    //            // ✅ Assign the array to response
    //            objResponse.RestaurantTypes = objRestaurantTypes;

    //            string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings()
    //            {
    //                NullValueHandling = NullValueHandling.Ignore
    //            });

    //            // Keep same output style (rename RestaurantTypes → data)
    //            strResponseName = strResponseName.Replace("\"RestaurantTypes\"", "\"data\"");
    //            HttpContext.Current.Response.Write(strResponseName);
    //        }
    //        else
    //        {
    //            NoRecordExists();
    //        }
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionTRack(Ex.Message.ToString());
    //        HttpContext.Current.Response.End();
    //    }
    //    HttpContext.Current.Response.End();
    //}


    //---------------------------------------------

    //[WebMethod]
    //[ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    //public void RestaurantTypesList()
    //{
    //    string strResponse = string.Empty;
    //    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
    //    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
    //    try
    //    {
    //        Response objResponse = new Response();
    //        DataTable dt = new DataTable();
    //        RestaurantTypesBAL objRestaurantTypesBAL = new RestaurantTypesBAL();
    //        dt = objRestaurantTypesBAL.GetRestaurantTypes(1);

    //        if (dt.Rows.Count > 0)
    //        {
    //            objResponse.success = "true";
    //            objResponse.message = dt.Rows.Count + " records found.";

    //            RestaurantTypes[] objRestaurantTypes = new RestaurantTypes[dt.Rows.Count];
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                objRestaurantTypes[i] = new RestaurantTypes();
    //                objRestaurantTypes[i].ID = Convert.ToInt32(dt.Rows[i]["ID"]);
    //                objRestaurantTypes[i].Name = Convert.ToString(dt.Rows[i]["Name"]);
    //                objResponse.RestaurantTypes = objRestaurantTypes;
    //            }
    //            string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
    //            strResponseName = strResponseName.Replace("\"RestaurantTypes\"", "\"data\"");
    //            HttpContext.Current.Response.Write(strResponseName);
    //        }
    //        else
    //        {
    //            NoRecordExists();
    //        }
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
    //    }
    //    HttpContext.Current.Response.End();
    //}

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void ContactUsSubjectList()
    {
        //string strResponse = string.Empty;
        //System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        //List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        //try
        //{
        //    Response objResponse = new Response();
        //    DataTable dt = new DataTable();
        //    ContactUsSubjectBAL objContactUsSubjectBAL = new ContactUsSubjectBAL();

        //    dt = objContactUsSubjectBAL.ContactUsSubjectListALL();

        //    if (dt.Rows.Count > 0)
        //    {
        //        objResponse.success = "true";
        //        objResponse.message = dt.Rows.Count + " records found.";

        //        ContactUsSubject[] objContactUsSubject = new ContactUsSubject[dt.Rows.Count];
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            objContactUsSubject[i] = new ContactUsSubject();
        //            objContactUsSubject[i].ID = Convert.ToInt32(dt.Rows[i]["ID"]);
        //            objContactUsSubject[i].Name = Convert.ToString(dt.Rows[i]["Name"]);
        //            objContactUsSubject[i].Status = Convert.ToBoolean(dt.Rows[i]["Status"]);
        //            objResponse.ContactUsSubject = objContactUsSubject;
        //        }
        //        string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        //        strResponseName = strResponseName.Replace("\"ContactUsSubject\"", "\"data\"");
        //        HttpContext.Current.Response.Write(strResponseName);
        //    }
        //    else
        //    {
        //        NoRecordExists();
        //    }
        //}
        //catch (Exception Ex)
        //{
        //    ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
        //}
        HttpContext.Current.Response.End();
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void StateList()
    {
        string strResponse = string.Empty;
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        try
        {
            Response objResponse = new Response();

            DataTable dt = new DataTable();

            CommonBAL objCommonBAL = new CommonBAL();
            dt = objCommonBAL.StateList();

            if (dt.Rows.Count > 0)
            {
                objResponse.success = "true";
                objResponse.message = dt.Rows.Count + " records found.";

                int IsDayLightOn = 0;
                IsDayLightOn = Convert.ToInt32(dt.Rows[0]["DayLightOn"]);

                States[] objStates = new States[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    objStates[i] = new States();
                    objStates[i].ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    objStates[i].StateName = Convert.ToString(dt.Rows[i]["StateCode"]);
                    // if (IsDayLightOn == 0)
                    // {
                    //     objStates[i].StateName = Convert.ToString(dt.Rows[i]["StateCode"]) + " (" + Convert.ToString(dt.Rows[i]["TimezoneName"]) + " - " + Convert.ToString(dt.Rows[i]["StandardTimeZone"]) + ")";
                    // }
                    // else
                    //// {
                    //      objStates[i].StateName = Convert.ToString(dt.Rows[i]["StateCode"]) + " (" + Convert.ToString(dt.Rows[i]["TimezoneName"]) + " - " + Convert.ToString(dt.Rows[i]["DaylightTimeZone"]) + ")";
                    //  }
                    objResponse.States = objStates;
                }
                string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                strResponseName = strResponseName.Replace("\"States\"", "\"data\"");
                HttpContext.Current.Response.Write(strResponseName);
            }
            else
            {
                NoRecordExists();
            }
        }
        catch (Exception Ex)
        {
            ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
        }
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void StateHolidayList(int StateID)
    {
        string strResponse = string.Empty;
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        try
        {
            Response objResponse = new Response();

            DataTable dt = new DataTable();

            CommonBAL objCommonBAL = new CommonBAL();
            dt = objCommonBAL.StateHolidayList_Webservice(StateID);

            if (dt.Rows.Count > 0)
            {
                objResponse.success = "true";
                objResponse.message = dt.Rows.Count + " records found.";

                StateHolidays[] objStateHolidays = new StateHolidays[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    objStateHolidays[i] = new StateHolidays();
                    objStateHolidays[i].StateHolidayID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    objStateHolidays[i].StateID = Convert.ToInt32(dt.Rows[i]["StateID"]);
                    objStateHolidays[i].Title = Convert.ToString(dt.Rows[i]["Title"]);
                    objResponse.StateHolidays = objStateHolidays;
                }
                string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                strResponseName = strResponseName.Replace("\"StateHolidays\"", "\"data\"");
                HttpContext.Current.Response.Write(strResponseName);
            }
            else
            {
                NoRecordExists();
            }
        }
        catch (Exception Ex)
        {
            ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
        }
        HttpContext.Current.Response.End();
    }


    #endregion

    #region Sales Admin Business
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void SalesAdminBusinessList(long UserID, string SecretKey, string AuthToken)
    {
        bool IsValidated = false;
        if (ValidateRequestBAL.BusinessValidateClientRequest(Convert.ToInt64(UserID), Convert.ToString(SecretKey), Convert.ToString(AuthToken)))
        {
            IsValidated = true;
        }

        if (!IsValidated)
        {
            CommonAPI objCommonAPI = new CommonAPI();
            objCommonAPI.Unauthorized();
        }
        else
        {
            string strResponse = string.Empty;
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            try
            {
                Response objResponse = new Response();
                DataTable dt = new DataTable();
                SalesAdminBAL objSalesAdminBAL = new SalesAdminBAL();
                dt = objSalesAdminBAL.BusinessListAll();


                if (dt.Rows.Count > 0)
                {
                    objResponse.success = "true";
                    objResponse.message = dt.Rows.Count + " records found.";

                    if (dt.Rows.Count > 0)
                    {
                        objResponse.success = "true";
                        objResponse.message = dt.Rows.Count + " records found.";

                        Business[] objBusiness = new Business[dt.Rows.Count];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            objBusiness[i] = new Business();
                            objBusiness[i].ID = Convert.ToInt64(dt.Rows[i]["ID"]);
                            objBusiness[i].BusinessName = Convert.ToString(dt.Rows[i]["Name"]);
                            objBusiness[i].FullName = Convert.ToString(dt.Rows[i]["FullName"]);
                            objBusiness[i].EmailAddress = Convert.ToString(dt.Rows[i]["EmailAddress"]);
                            objBusiness[i].BusinessPhone = Convert.ToString(dt.Rows[i]["BusinessPhone"]);
                            objBusiness[i].Status = Convert.ToInt16(dt.Rows[i]["Status"]) == 1 ? "True" : "False";
                            objResponse.Business = objBusiness;
                        }
                        string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                        strResponseName = strResponseName.Replace("\"Business\"", "\"data\"");
                        HttpContext.Current.Response.Write(strResponseName);
                        //HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
                    }
                }
                else
                {
                    NoRecordExists();
                }
            }
            catch (Exception Ex)
            {
                ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
            }
        }
        HttpContext.Current.Response.End();
    }


    //[WebMethod]
    //[ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    //public void SalesAdminBusinessDetailByID(long UserID, string SecretKey, string AuthToken, long BusinessID)
    //{
    //    bool IsValidated = false;
    //    if (ValidateRequestBAL.BusinessValidateClientRequest(Convert.ToInt64(UserID), Convert.ToString(SecretKey), Convert.ToString(AuthToken)))
    //    {
    //        IsValidated = true;
    //    }

    //    if (!IsValidated)
    //    {
    //        CommonAPI objCommonAPI = new CommonAPI();
    //        objCommonAPI.Unauthorized();
    //    }
    //    else
    //    {
    //        string strResponse = string.Empty;
    //        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
    //        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
    //        try
    //        {
    //            Response objResponse = new Response();
    //            DataSet ds = new DataSet();
    //            DataTable dt = new DataTable();
    //            SalesAdminBAL objSalesAdminBAL = new SalesAdminBAL();
    //            objSalesAdminBAL.ID = BusinessID;

    //            ds = objSalesAdminBAL.BusinessDetailsByID();
    //            dt = ds.Tables[0];
    //            if (dt.Rows.Count > 0)
    //            {
    //                objResponse.success = "true";
    //                objResponse.message = dt.Rows.Count + " records found.";

    //                if (dt.Rows.Count > 0)
    //                {
    //                    objResponse.success = "true";
    //                    objResponse.message = dt.Rows.Count + " records found.";

    //                    BusinessProfile[] objBusinessProfile = new BusinessProfile[dt.Rows.Count];
    //                    for (int i = 0; i < dt.Rows.Count; i++)
    //                    {
    //                        objBusinessProfile[i] = new BusinessProfile();
    //                        objBusinessProfile[i].ID = Convert.ToInt64(dt.Rows[i]["ID"]);
    //                        objBusinessProfile[i].BusinessName = Convert.ToString(dt.Rows[i]["Name"]);
    //                        objBusinessProfile[i].ABN = Convert.ToString(dt.Rows[i]["ABN"]);
    //                        objBusinessProfile[i].StreetAddress = Convert.ToString(dt.Rows[i]["StreetAddress"]);
    //                        objBusinessProfile[i].Location = Convert.ToString(dt.Rows[i]["Location"]);
    //                        objBusinessProfile[i].FullName = Convert.ToString(dt.Rows[i]["FullName"]);
    //                        objBusinessProfile[i].EmailAddress = Convert.ToString(dt.Rows[i]["EmailAddress"]);
    //                        objBusinessProfile[i].Description = Convert.ToString(dt.Rows[i]["Description"]);
    //                        objBusinessProfile[i].BSBNo = Convert.ToString(dt.Rows[i]["BSBNo"]);
    //                        objBusinessProfile[i].AccountNumber = Convert.ToString(dt.Rows[i]["AccountNumber"]);
    //                        objBusinessProfile[i].BankName = Convert.ToString(dt.Rows[i]["BankName"]);
    //                        objBusinessProfile[i].AccountName = Convert.ToString(dt.Rows[i]["AccountName"]);
    //                        objBusinessProfile[i].BusinessPhone = Convert.ToString(dt.Rows[i]["BusinessPhone"]);
    //                        objBusinessProfile[i].Mobile = Convert.ToString(dt.Rows[i]["Mobile"]);
    //                        objBusinessProfile[i].LastName = Convert.ToString(dt.Rows[i]["LastName"]);
    //                        if (Convert.ToString(dt.Rows[i]["ProfilePhotoID"]) != string.Empty)
    //                            objBusinessProfile[i].ProfilePhotoID = Convert.ToInt64(dt.Rows[i]["ProfilePhotoID"]);
    //                        else
    //                            objBusinessProfile[i].ProfilePhotoID = 0;

    //                        objBusinessProfile[i].StateID = Convert.ToInt32(dt.Rows[i]["StateID"]);
    //                        objBusinessProfile[i].StateCode = Convert.ToString(dt.Rows[i]["StateCode"]);
    //                        objBusinessProfile[i].StateFullName = Convert.ToString(dt.Rows[i]["StateName"]);


    //                        if (Convert.ToString(dt.Rows[i]["ProfilePhoto"]) != string.Empty)
    //                        {
    //                            objBusinessProfile[i].ProfilePhoto = Config.WebSiteUrl + Config.CMSFiles + Convert.ToString(dt.Rows[i]["ProfilePhoto"]);

    //                        }
    //                        else
    //                        {
    //                            objBusinessProfile[i].ProfilePhoto = "";
    //                        }

    //                        objBusinessProfile[i].Status = Convert.ToInt16(dt.Rows[i]["Status"]) == 1 ? "True" : "False";


    //                        //Business Type
    //                        DataTable dtProdutType = new DataTable();
    //                        dtProdutType = ds.Tables[1];
    //                        if (dtProdutType.Rows.Count > 0)
    //                        {
    //                            objBusinessProfile[i].BusinessType = new BusinessType[dtProdutType.Rows.Count];
    //                            for (int j = 0; j < dtProdutType.Rows.Count; j++)
    //                            {
    //                                objBusinessProfile[i].BusinessType[j] = new BusinessType();
    //                                objBusinessProfile[i].BusinessType[j].ID = Convert.ToInt32(dtProdutType.Rows[j]["ID"]);
    //                                objBusinessProfile[i].BusinessType[j].Name = Convert.ToString(dtProdutType.Rows[j]["Name"]);
    //                                objBusinessProfile[i].BusinessType[j].IsSelected = Convert.ToInt32(dtProdutType.Rows[j]["Selected"]);

    //                            }
    //                        }
    //                        else
    //                        {
    //                            objBusinessProfile[i].BusinessType = new BusinessType[0];
    //                        }
    //                        //Business Type

    //                        //Food Items
    //                        DataTable dtFoodItems = new DataTable();
    //                        dtFoodItems = ds.Tables[2];
    //                        if (dtFoodItems.Rows.Count > 0)
    //                        {
    //                            objBusinessProfile[i].BusinessFoodItems = new BusinessFoodItems[dtFoodItems.Rows.Count];
    //                            for (int j = 0; j < dtFoodItems.Rows.Count; j++)
    //                            {
    //                                objBusinessProfile[i].BusinessFoodItems[j] = new BusinessFoodItems();
    //                                objBusinessProfile[i].BusinessFoodItems[j].ID = Convert.ToInt32(dtFoodItems.Rows[j]["ID"]);
    //                                objBusinessProfile[i].BusinessFoodItems[j].Name = Convert.ToString(dtFoodItems.Rows[j]["Name"]);
    //                                objBusinessProfile[i].BusinessFoodItems[j].IsSelected = Convert.ToInt32(dtFoodItems.Rows[j]["Selected"]);

    //                            }
    //                        }
    //                        else
    //                        {
    //                            objBusinessProfile[i].BusinessFoodItems = new BusinessFoodItems[0];
    //                        }
    //                        //Food Items

    //                        //Business Schedule Pickup Time 1 
    //                        DataTable dtSchedule = new DataTable();
    //                        dtSchedule = ds.Tables[3];
    //                        if (dtSchedule.Rows.Count > 0)
    //                        {
    //                            objBusinessProfile[i].BusinessSchedule = new BusinessSchedule[dtSchedule.Rows.Count];
    //                            for (int j = 0; j < dtSchedule.Rows.Count; j++)
    //                            {
    //                                objBusinessProfile[i].BusinessSchedule[j] = new BusinessSchedule();
    //                                objBusinessProfile[i].BusinessSchedule[j].DayNo = Convert.ToInt32(dtSchedule.Rows[j]["DayNo"]);
    //                                objBusinessProfile[i].BusinessSchedule[j].DayName = Convert.ToString(dtSchedule.Rows[j]["DayName"]);
    //                                objBusinessProfile[i].BusinessSchedule[j].NoOfItems = Convert.ToString(dtSchedule.Rows[j]["NoOfItems"]);
    //                                objBusinessProfile[i].BusinessSchedule[j].OriginalPrice = Convert.ToString(dtSchedule.Rows[j]["OriginalPrice"]);
    //                                objBusinessProfile[i].BusinessSchedule[j].DiscountID = Convert.ToInt32(dtSchedule.Rows[j]["DiscountID"]);
    //                                objBusinessProfile[i].BusinessSchedule[j].Discount = Convert.ToString(dtSchedule.Rows[j]["Discount"]);
    //                                objBusinessProfile[i].BusinessSchedule[j].PickupFromTime = Convert.ToDateTime(dtSchedule.Rows[j]["PickupFromTime"]).ToString("HH:mm");
    //                                objBusinessProfile[i].BusinessSchedule[j].PickupToTime = Convert.ToDateTime(dtSchedule.Rows[j]["PickupToTime"]).ToString("HH:mm");
    //                                objBusinessProfile[i].BusinessSchedule[j].setColor = Convert.ToInt32(dtSchedule.Rows[j]["ScheduleOn"]);
    //                            }
    //                        }
    //                        //Business Schedule Pickup Time 1 

    //                        //Business Schedule Pickup Time 2
    //                        dtSchedule = new DataTable();
    //                        dtSchedule = ds.Tables[5];
    //                        if (dtSchedule.Rows.Count > 0)
    //                        {
    //                            objBusinessProfile[i].BusinessSchedulePickupTime2 = new BusinessSchedulePickupTime2[dtSchedule.Rows.Count];
    //                            for (int j = 0; j < dtSchedule.Rows.Count; j++)
    //                            {
    //                                objBusinessProfile[i].BusinessSchedulePickupTime2[j] = new BusinessSchedulePickupTime2();
    //                                objBusinessProfile[i].BusinessSchedulePickupTime2[j].DayNo = Convert.ToInt32(dtSchedule.Rows[j]["DayNo"]);
    //                                objBusinessProfile[i].BusinessSchedulePickupTime2[j].DayName = Convert.ToString(dtSchedule.Rows[j]["DayName"]);
    //                                objBusinessProfile[i].BusinessSchedulePickupTime2[j].NoOfItems = Convert.ToString(dtSchedule.Rows[j]["NoOfItems"]);
    //                                objBusinessProfile[i].BusinessSchedulePickupTime2[j].OriginalPrice = Convert.ToString(dtSchedule.Rows[j]["OriginalPrice"]);
    //                                objBusinessProfile[i].BusinessSchedulePickupTime2[j].DiscountID = Convert.ToInt32(dtSchedule.Rows[j]["DiscountID"]);
    //                                objBusinessProfile[i].BusinessSchedulePickupTime2[j].Discount = Convert.ToString(dtSchedule.Rows[j]["Discount"]);
    //                                objBusinessProfile[i].BusinessSchedulePickupTime2[j].PickupFromTime = Convert.ToDateTime(dtSchedule.Rows[j]["PickupFromTime"]).ToString("HH:mm");
    //                                objBusinessProfile[i].BusinessSchedulePickupTime2[j].PickupToTime = Convert.ToDateTime(dtSchedule.Rows[j]["PickupToTime"]).ToString("HH:mm");
    //                                objBusinessProfile[i].BusinessSchedulePickupTime2[j].setColor = Convert.ToInt32(dtSchedule.Rows[j]["ScheduleOn"]);
    //                            }
    //                        }
    //                        //Business Schedule Pickup Time 2

    //                        //Business Schedule Pickup Time 3
    //                        dtSchedule = new DataTable();
    //                        dtSchedule = ds.Tables[6];
    //                        if (dtSchedule.Rows.Count > 0)
    //                        {
    //                            objBusinessProfile[i].BusinessSchedulePickupTime3 = new BusinessSchedulePickupTime3[dtSchedule.Rows.Count];
    //                            for (int j = 0; j < dtSchedule.Rows.Count; j++)
    //                            {
    //                                objBusinessProfile[i].BusinessSchedulePickupTime3[j] = new BusinessSchedulePickupTime3();
    //                                objBusinessProfile[i].BusinessSchedulePickupTime3[j].DayNo = Convert.ToInt32(dtSchedule.Rows[j]["DayNo"]);
    //                                objBusinessProfile[i].BusinessSchedulePickupTime3[j].DayName = Convert.ToString(dtSchedule.Rows[j]["DayName"]);
    //                                objBusinessProfile[i].BusinessSchedulePickupTime3[j].NoOfItems = Convert.ToString(dtSchedule.Rows[j]["NoOfItems"]);
    //                                objBusinessProfile[i].BusinessSchedulePickupTime3[j].OriginalPrice = Convert.ToString(dtSchedule.Rows[j]["OriginalPrice"]);
    //                                objBusinessProfile[i].BusinessSchedulePickupTime3[j].DiscountID = Convert.ToInt32(dtSchedule.Rows[j]["DiscountID"]);
    //                                objBusinessProfile[i].BusinessSchedulePickupTime3[j].Discount = Convert.ToString(dtSchedule.Rows[j]["Discount"]);
    //                                objBusinessProfile[i].BusinessSchedulePickupTime3[j].PickupFromTime = Convert.ToDateTime(dtSchedule.Rows[j]["PickupFromTime"]).ToString("HH:mm");
    //                                objBusinessProfile[i].BusinessSchedulePickupTime3[j].PickupToTime = Convert.ToDateTime(dtSchedule.Rows[j]["PickupToTime"]).ToString("HH:mm");
    //                                objBusinessProfile[i].BusinessSchedulePickupTime3[j].setColor = Convert.ToInt32(dtSchedule.Rows[j]["ScheduleOn"]);
    //                            }
    //                        }
    //                        //Business Schedule Pickup Time 2




    //                        //Restaurant Types
    //                        DataTable dtRestaurantTypes = new DataTable();
    //                        dtRestaurantTypes = ds.Tables[4];
    //                        if (dtRestaurantTypes.Rows.Count > 0)
    //                        {
    //                            objBusinessProfile[i].RestaurantTypes = new RestaurantTypes[dtRestaurantTypes.Rows.Count];
    //                            for (int j = 0; j < dtRestaurantTypes.Rows.Count; j++)
    //                            {
    //                                objBusinessProfile[i].RestaurantTypes[j] = new RestaurantTypes();
    //                                objBusinessProfile[i].RestaurantTypes[j].ID = Convert.ToInt32(dtRestaurantTypes.Rows[j]["ID"]);
    //                                objBusinessProfile[i].RestaurantTypes[j].Name = Convert.ToString(dtRestaurantTypes.Rows[j]["Name"]);

    //                            }
    //                        }
    //                        else
    //                        {
    //                            objBusinessProfile[i].RestaurantTypes = new RestaurantTypes[0];
    //                        }
    //                        //Restaurant Types

    //                        objResponse.BusinessProfile = objBusinessProfile;
    //                    }
    //                    string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
    //                    strResponseName = strResponseName.Replace("\"BusinessProfile\"", "\"data\"");
    //                    HttpContext.Current.Response.Write(strResponseName);
    //                    //HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
    //                }
    //            }
    //            else
    //            {
    //                NoRecordExists();
    //            }
    //        }
    //        catch (Exception Ex)
    //        {
    //            ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
    //        }
    //    }
    //    HttpContext.Current.Response.End();
    //}


    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void SalesAdminBusinessStatusChange(long UserID, string SecretKey, string AuthToken, long BusinessID, int Status)
    {
        bool IsValidated = false;
        if (ValidateRequestBAL.BusinessValidateClientRequest(Convert.ToInt64(UserID), Convert.ToString(SecretKey), Convert.ToString(AuthToken)))
        {
            IsValidated = true;
        }

        if (!IsValidated)
        {
            CommonAPI objCommonAPI = new CommonAPI();
            objCommonAPI.Unauthorized();
        }
        else
        {
            string strResponse = string.Empty;
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            try
            {
                Response objResponse = new Response();
                BusinessBAL objBusinessBAL = new BusinessBAL();
                objBusinessBAL.Status = Status;
                objBusinessBAL.ID = BusinessID;
                long result = objBusinessBAL.BusinessStatusChange();

                switch (result)
                {
                    case -1:
                        objResponse.success = "false";
                        objResponse.message = "Please try after sometime.";
                        break;
                    default:
                        // SendUserMail();                    
                        objResponse.success = "true";
                        objResponse.message = "Status has been changed successfully.";
                        try
                        {
                            if (Status == 1)
                            {
                                SendActivationEmailToAdmin(BusinessID);
                            }
                        }
                        catch (Exception ex)
                        {

                        }

                        break;
                }

                HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));

            }
            catch (Exception Ex)
            {
                ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
            }
        }
        HttpContext.Current.Response.End();
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void BusinessCurrentDayOrders(long UserID, string SecretKey, string AuthToken, long BusinessID, string FromDate, string ToDate)
    {
        bool IsValidated = false;
        if (ValidateRequestBAL.BusinessValidateClientRequest(Convert.ToInt64(UserID), Convert.ToString(SecretKey), Convert.ToString(AuthToken)))
        {
            IsValidated = true;
        }

        if (!IsValidated)
        {
            CommonAPI objCommonAPI = new CommonAPI();
            objCommonAPI.Unauthorized();
        }
        else
        {
            string strResponse = string.Empty;
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            try
            {
                Response objResponse = new Response();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                BusinessBAL objBusinessBAL = new BusinessBAL();
                objBusinessBAL.ID = BusinessID;

                // ds = objBusinessBAL.BusinessCurrentDayOrders(AuthToken);
                ds = BusinessCurrentDayOrdersDAL(BusinessID, AuthToken, FromDate, ToDate);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {

                    if (dt.Rows.Count > 0)
                    {
                        objResponse.success = "true";
                        objResponse.message = dt.Rows.Count + " records found.";

                        // Calculate Qty Totals
                        int GrandTotalQty = 0;
                        int GrandPurchaseQty = 0;
                        int GrandRemainingQty = 0;
                        int IsActive = 0;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            GrandTotalQty = GrandTotalQty + Convert.ToInt32(dt.Rows[i]["TotalQty"]);
                            GrandPurchaseQty = GrandPurchaseQty + Convert.ToInt32(dt.Rows[i]["PurchaseQty"]);
                            GrandRemainingQty = GrandRemainingQty + Convert.ToInt32(dt.Rows[i]["RemainingQty"]);
                            IsActive = Convert.ToInt32(dt.Rows[i]["Status"]);
                        }

                        //

                        BusinessCurrentDayOrders[] objBusinessCurrentDayOrders = new BusinessCurrentDayOrders[dt.Rows.Count];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            objBusinessCurrentDayOrders[i] = new BusinessCurrentDayOrders();
                            objBusinessCurrentDayOrders[i].ID = Convert.ToInt64(dt.Rows[i]["ID"]);
                            objBusinessCurrentDayOrders[i].BusinessName = Convert.ToString(dt.Rows[i]["Name"]);
                            objBusinessCurrentDayOrders[i].FullName = Convert.ToString(dt.Rows[i]["FullName"]);
                            objBusinessCurrentDayOrders[i].EmailAddress = Convert.ToString(dt.Rows[i]["EmailAddress"]);

                            objBusinessCurrentDayOrders[i].GrandTotalQty = Convert.ToString(GrandTotalQty);
                            objBusinessCurrentDayOrders[i].GrandPurchaseQty = Convert.ToString(GrandPurchaseQty);
                            objBusinessCurrentDayOrders[i].GrandRemainingQty = Convert.ToString(GrandRemainingQty);

                            if (IsActive == 3)
                            {
                                objBusinessCurrentDayOrders[i].IsActive = 0;
                            }
                            else
                            {
                                objBusinessCurrentDayOrders[i].IsActive = 1;
                            }

                            if (Convert.ToInt32(dt.Rows[i]["ScheduleOn"]) == 1)
                            {
                                objBusinessCurrentDayOrders[i].TotalQty = Convert.ToInt32(dt.Rows[i]["TotalQty"]);
                                objBusinessCurrentDayOrders[i].PurchaseQty = Convert.ToInt32(dt.Rows[i]["PurchaseQty"]);
                                if (Convert.ToInt32(dt.Rows[i]["RemainingQty"]) < 0)
                                    objBusinessCurrentDayOrders[i].RemainingQty = 0;
                                else
                                    objBusinessCurrentDayOrders[i].RemainingQty = Convert.ToInt32(dt.Rows[i]["RemainingQty"]);
                                objBusinessCurrentDayOrders[i].OriginalPrice = Convert.ToString(dt.Rows[i]["OriginalPrice"]);
                                objBusinessCurrentDayOrders[i].DiscountID = Convert.ToInt32(dt.Rows[i]["DiscountID"]);
                                objBusinessCurrentDayOrders[i].PickupFromTime = Convert.ToDateTime(dt.Rows[i]["PickupFromTime"]).ToString("HH:mm");
                                objBusinessCurrentDayOrders[i].PickupToTime = Convert.ToDateTime(dt.Rows[i]["PickupToTime"]).ToString("HH:mm");
                                objBusinessCurrentDayOrders[i].DiscountValue = Convert.ToString(dt.Rows[i]["DiscountValue"]);

                                objBusinessCurrentDayOrders[i].PickupTime = Convert.ToInt32(dt.Rows[i]["PickupTime"]);
                            }
                            else
                            {
                                objBusinessCurrentDayOrders[i].TotalQty = 0;
                                objBusinessCurrentDayOrders[i].PurchaseQty = 0;
                                objBusinessCurrentDayOrders[i].RemainingQty = 0;
                                objBusinessCurrentDayOrders[i].OriginalPrice = "0";
                                objBusinessCurrentDayOrders[i].DiscountID = 0;
                                objBusinessCurrentDayOrders[i].PickupFromTime = "";
                                objBusinessCurrentDayOrders[i].PickupToTime = "";
                                objBusinessCurrentDayOrders[i].DiscountValue = "0";
                                objBusinessCurrentDayOrders[i].PickupTime = Convert.ToInt32(dt.Rows[i]["PickupTime"]);
                            }

                            //Business Orders
                            DataTable dtOrders = new DataTable();
                            dtOrders = ds.Tables[1];
                            if (dtOrders.Rows.Count > 0)
                            {
                                objBusinessCurrentDayOrders[i].BusinessOrders = new BusinessOrders[dtOrders.Rows.Count];
                                for (int j = 0; j < dtOrders.Rows.Count; j++)
                                {
                                    objBusinessCurrentDayOrders[i].BusinessOrders[j] = new BusinessOrders();
                                    objBusinessCurrentDayOrders[i].BusinessOrders[j].Name = Convert.ToString(dtOrders.Rows[j]["Name"]);
                                    objBusinessCurrentDayOrders[i].BusinessOrders[j].Email = Convert.ToString(dtOrders.Rows[j]["Email"]);
                                    objBusinessCurrentDayOrders[i].BusinessOrders[j].Mobile = Convert.ToString(dtOrders.Rows[j]["Mobile"]);
                                    objBusinessCurrentDayOrders[i].BusinessOrders[j].Qty = Convert.ToString(dtOrders.Rows[j]["Qty"]);
                                    objBusinessCurrentDayOrders[i].BusinessOrders[j].OrderTime = Convert.ToDateTime(dtOrders.Rows[j]["CreatedOn"]).ToString("hh:mm tt");
                                    objBusinessCurrentDayOrders[i].BusinessOrders[j].IsCollected = Convert.ToInt32(dtOrders.Rows[j]["IsCollected"]);
                                    objBusinessCurrentDayOrders[i].BusinessOrders[j].GrandTotal = Convert.ToDouble(dtOrders.Rows[j]["GrandTotal"]).ToString("f2");

                                    objBusinessCurrentDayOrders[i].BusinessOrders[j].PickupFromTime = Convert.ToDateTime(dtOrders.Rows[j]["PickupFromTime"]).ToString("hh:mm tt");
                                    objBusinessCurrentDayOrders[i].BusinessOrders[j].PickupToTime = Convert.ToDateTime(dtOrders.Rows[j]["PickupToTime"]).ToString("hh:mm tt");

                                    objBusinessCurrentDayOrders[i].BusinessOrders[j].OrderID = Convert.ToString(dtOrders.Rows[j]["OrderID"]);
                                    objBusinessCurrentDayOrders[i].BusinessOrders[j].ID = Convert.ToInt64(dtOrders.Rows[j]["ID"]);

                                    objBusinessCurrentDayOrders[i].BusinessOrders[j].OriginalPrice = Convert.ToDouble(dtOrders.Rows[j]["OriginalPrice"]).ToString("f2");
                                    objBusinessCurrentDayOrders[i].BusinessOrders[j].DiscountValue = Convert.ToDouble(dtOrders.Rows[j]["DiscountValue"]).ToString("f2");
                                    objBusinessCurrentDayOrders[i].BusinessOrders[j].OrderStatusID = Convert.ToInt32(dtOrders.Rows[j]["OrderStatusID"]);
                                    objBusinessCurrentDayOrders[i].BusinessOrders[j].OrderStatusText = Convert.ToString(dtOrders.Rows[j]["OrderStatusText"]);

                                }
                            }
                            //Business Type

                            objResponse.BusinessCurrentDayOrders = objBusinessCurrentDayOrders;
                        }

                        string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                        strResponseName = strResponseName.Replace("\"BusinessCurrentDayOrders\"", "\"data\"");
                        HttpContext.Current.Response.Write(strResponseName);
                        //HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
                    }
                }
                else
                {
                    NoRecordExists();
                }
            }
            catch (Exception Ex)
            {
                ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
            }
        }
        HttpContext.Current.Response.End();
    }

    [WebMethod]

    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]

    public void BusinessRatings(long UserID, string SecretKey, string AuthToken, long BusinessID)

    {

        bool IsValidated = false;

        if (ValidateRequestBAL.BusinessValidateClientRequest(Convert.ToInt64(UserID), Convert.ToString(SecretKey), Convert.ToString(AuthToken)))

        {

            IsValidated = true;

        }

        if (!IsValidated)

        {

            CommonAPI objCommonAPI = new CommonAPI();

            objCommonAPI.Unauthorized();

        }

        else

        {
            string strResponse = string.Empty;

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();

            try

            {

                Response objResponse = new Response();

                DataSet ds = new DataSet();

                DataTable dt = new DataTable();


                BusinessBAL objBusinessBAL = new BusinessBAL();

                objBusinessBAL.ID = BusinessID;

                ds = BusinessRatingsDAL(BusinessID);

                dt = ds.Tables[0];

                if (dt.Rows.Count > 0)

                {

                    if (dt.Rows.Count > 0)

                    {

                        objResponse.success = "true";

                        objResponse.message = dt.Rows.Count + " records found.";

                        BusinessRatings[] objBusinessRatings = new BusinessRatings[dt.Rows.Count];

                        for (int i = 0; i < dt.Rows.Count; i++)

                        {

                            objBusinessRatings[i] = new BusinessRatings();

                            objBusinessRatings[i].ID = Convert.ToInt64(dt.Rows[i]["ID"]);

                            objBusinessRatings[i].BusinessName = Convert.ToString(dt.Rows[i]["BusinessName"]);

                            objBusinessRatings[i].FullName = Convert.ToString(dt.Rows[i]["FullName"]);

                            objBusinessRatings[i].EmailAddress = Convert.ToString(dt.Rows[i]["EmailAddress"]);

                            objBusinessRatings[i].AverageRating = Convert.ToString(dt.Rows[i]["OverallAverageRating"]);





                            //Business Ratings

                            DataTable dtOrders = new DataTable();

                            dtOrders = ds.Tables[1];

                            if (dtOrders.Rows.Count > 0)

                            {

                                objBusinessRatings[i].Ratings = new RatingDetails[dtOrders.Rows.Count];

                                for (int j = 0; j < dtOrders.Rows.Count; j++)

                                {

                                    objBusinessRatings[i].Ratings[j] = new RatingDetails();

                                    objBusinessRatings[i].Ratings[j].BusinessID = Convert.ToInt64(dtOrders.Rows[j]["BusinessID"]);

                                    objBusinessRatings[i].Ratings[j].UserID = Convert.ToInt64(dtOrders.Rows[j]["UserID"]);

                                    objBusinessRatings[i].Ratings[j].UserName = Convert.ToString(dtOrders.Rows[j]["UserName"]);

                                    objBusinessRatings[i].Ratings[j].UserEmail = Convert.ToString(dtOrders.Rows[j]["UserEmail"]);

                                    objBusinessRatings[i].Ratings[j].UserMobile = Convert.ToString(dtOrders.Rows[j]["UserMobile"]);

                                    objBusinessRatings[i].Ratings[j].Point1Rating = Convert.ToString(dtOrders.Rows[j]["Point1Rating"]);

                                    objBusinessRatings[i].Ratings[j].Point2Rating = Convert.ToString(dtOrders.Rows[j]["Point2Rating"]);

                                    objBusinessRatings[i].Ratings[j].Point3Rating = Convert.ToString(dtOrders.Rows[j]["Point3Rating"]);

                                    objBusinessRatings[i].Ratings[j].Point4Rating = Convert.ToString(dtOrders.Rows[j]["Point4Rating"]);

                                    objBusinessRatings[i].Ratings[j].AverageRating = Convert.ToString(dtOrders.Rows[j]["AverageRating"]);

                                    objBusinessRatings[i].Ratings[j].CreatedOn = Convert.ToDateTime(dtOrders.Rows[j]["CreatedOn"]).ToString("dd/MMM/yyyy");

                                }

                            }

                            else

                            {

                                objBusinessRatings[i].Ratings = new RatingDetails[0];

                            }

                            //Business Type

                            objResponse.BusinessRatings = objBusinessRatings;

                        }

                        string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

                        strResponseName = strResponseName.Replace("\"BusinessRatings\"", "\"data\"");

                        HttpContext.Current.Response.Write(strResponseName);

                        //HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));

                    }
                }

                else

                {

                    NoRecordExists();

                }

            }

            catch (Exception Ex)

            {

                ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();

            }

        }

        HttpContext.Current.Response.End();

    }

    #endregion

    #region Business
    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]

    public void BusinessDetailByID(long UserID, string SecretKey, string AuthToken, long BusinessID)
    {
        bool IsValidated = false;
        if (ValidateRequestBAL.BusinessValidateClientRequest(
                Convert.ToInt64(UserID),
                Convert.ToString(SecretKey),
                Convert.ToString(AuthToken)))
        {
            IsValidated = true;
        }

        if (!IsValidated)
        {
            CommonAPI objCommonAPI = new CommonAPI();
            objCommonAPI.Unauthorized();
            return;
        }

        string strResponse = string.Empty;
        JavaScriptSerializer serializer = new JavaScriptSerializer();

        try
        {
            Response objResponse = new Response();
            SalesAdminBAL objSalesAdminBAL = new SalesAdminBAL();
            objSalesAdminBAL.ID = BusinessID;

            DataSet ds = objSalesAdminBAL.BusinessDetailsByID();
            DataTable dt = ds.Tables[0];

            if (dt.Rows.Count > 0)
            {
                objResponse.success = "true";
                objResponse.message = dt.Rows.Count + " records found.";

                BusinessProfile[] objBusinessProfile = new BusinessProfile[dt.Rows.Count];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    objBusinessProfile[i] = new BusinessProfile();

                    objBusinessProfile[i].ID = Convert.ToInt64(dt.Rows[i]["ID"]);
                    objBusinessProfile[i].BusinessName = Convert.ToString(dt.Rows[i]["BusinessName"]);
                    objBusinessProfile[i].ABN = Convert.ToString(dt.Rows[i]["ABN"]);
                    objBusinessProfile[i].StreetAddress = Convert.ToString(dt.Rows[i]["StreetAddress"]);
                    objBusinessProfile[i].Location = Convert.ToString(dt.Rows[i]["Location"]);

                    objBusinessProfile[i].FullName = Convert.ToString(dt.Rows[i]["FullName"]);
                    //objBusinessProfile[i].LastName = Convert.ToString(dt.Rows[i]["LastName"]);

                    objBusinessProfile[i].EmailAddress = Convert.ToString(dt.Rows[i]["EmailAddress"]);
                    objBusinessProfile[i].FirstName = Convert.ToString(dt.Rows[i]["FirstName"]);
                    objBusinessProfile[i].LastName = Convert.ToString(dt.Rows[i]["LastName"]);
                    objBusinessProfile[i].Suburb = Convert.ToString(dt.Rows[i]["Suburb"]);
                    objBusinessProfile[i].State = Convert.ToString(dt.Rows[i]["State"]);
                    objBusinessProfile[i].Description = Convert.ToString(dt.Rows[i]["Description"]);
                    objBusinessProfile[i].BSBNo = Convert.ToString(dt.Rows[i]["BSBNo"]);
                    objBusinessProfile[i].AccountNumber = Convert.ToString(dt.Rows[i]["AccountNumber"]);
                    objBusinessProfile[i].BankName = Convert.ToString(dt.Rows[i]["BankName"]);
                    objBusinessProfile[i].AccountName = Convert.ToString(dt.Rows[i]["AccountName"]);

                    objBusinessProfile[i].Mobile = Convert.ToString(dt.Rows[i]["Mobile"]);
                    objBusinessProfile[i].BusinessPhone = Convert.ToString(dt.Rows[i]["BusinessPhone"]);
                    objBusinessProfile[i].PostCode = Convert.ToString(dt.Rows[i]["PostCode"]);
                    objBusinessProfile[i].StateID = Convert.ToInt32(dt.Rows[i]["StateID"]);
                    objBusinessProfile[i].StateCode = Convert.ToString(dt.Rows[i]["StateCode"]);
                    objBusinessProfile[i].StateFullName = Convert.ToString(dt.Rows[i]["StateFullName"]);
                    objBusinessProfile[i].Latitude = Convert.ToString(dt.Rows[i]["Latitude"]);
                    objBusinessProfile[i].Longitude = Convert.ToString(dt.Rows[i]["Longitude"]);
                    objBusinessProfile[i].GSTRegistered = Convert.ToInt32(dt.Rows[i]["GSTRegistered"]);
                    objBusinessProfile[i].ReceiveMarketingMails = Convert.ToInt32(dt.Rows[i]["ReceiveMarketingMails"]);
                    objBusinessProfile[i].Note = Convert.ToString(dt.Rows[i]["Note"]);
                    objBusinessProfile[i].AboutUs = Convert.ToString(dt.Rows[i]["AboutUs"]);
                    objBusinessProfile[i].RegisterGst = (byte)Convert.ToInt32(dt.Rows[i]["RegisterGst"]);
                    objBusinessProfile[i].CategoryTaxItemOrNot = (byte)Convert.ToInt32(dt.Rows[i]["CategoryTaxItemOrNot"]);
                    // ⭐ Add StoreName
                    objBusinessProfile[i].StoreName = dt.Columns.Contains("StoreName")
                        ? Convert.ToString(dt.Rows[i]["StoreName"])
                        : "";
                    // Profile Photo
                    if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["ProfilePhotoID"])))
                        objBusinessProfile[i].ProfilePhotoID = Convert.ToInt64(dt.Rows[i]["ProfilePhotoID"]);

                    string profilePhoto = Convert.ToString(dt.Rows[i]["ProfilePhoto"]);
                    if (!string.IsNullOrEmpty(profilePhoto))
                    {
                        // If already a full URL, keep as is
                        if (!profilePhoto.StartsWith("http"))
                        {
                            if (!profilePhoto.StartsWith("/"))
                                profilePhoto = "/source/CMSFiles/" + profilePhoto;

                            profilePhoto = Config.WebSiteUrl.TrimEnd('/') + profilePhoto;
                        }
                    }
                    objBusinessProfile[i].ProfilePhoto = profilePhoto;

                    // Store Images
                    if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["StoreImages"])))
                    {
                        string[] imgs = Convert.ToString(dt.Rows[i]["StoreImages"]).Split(',');
                        for (int j = 0; j < imgs.Length; j++)
                        {
                            string img = imgs[j].Trim();
                            if (!img.StartsWith("http"))
                            {
                                if (!img.StartsWith("/"))
                                    img = "/source/CMSFiles/" + img;

                                img = Config.WebSiteUrl.TrimEnd('/') + img;
                            }
                            imgs[j] = img;

                            imgs[j] = img;
                        }
                        objBusinessProfile[i].StoreImages = imgs;
                    }
                    else
                        objBusinessProfile[i].StoreImages = new string[] { };

                    objBusinessProfile[i].Note = dt.Columns.Contains("Note")
                      ? Convert.ToString(dt.Rows[i]["Note"])
                      : "";

                    objBusinessProfile[i].Status = Convert.ToString(dt.Rows[i]["Status"]);

                    DataTable dtProdutType = ds.Tables[1];

                    //var businessTypes = dtProdutType.Rows.Count > 0
                    //    ? dtProdutType.AsEnumerable().Select(r => new BusinessType
                    //    {
                    //        ID = r.Field<int>("ID"),
                    //        Name = r.Field<string>("Name"),
                    //        IsSelected = r.Field<int>("IsSelected")
                    //    }).ToList()
                    //    : new List<BusinessType>();

                    var businessTypes = dtProdutType.Rows.Count > 0
                    ? dtProdutType.AsEnumerable().Select(r =>
                    {
                        string img = dtProdutType.Columns.Contains("ImageName")
                        ? Convert.ToString(r["ImageName"]).Trim().Replace("\\", "/")
                        : "";

                        if (!string.IsNullOrEmpty(img))
                        {
                            if (!img.StartsWith("/"))
                                img = "/source/CMSFiles/" + img;
                            else
                                img = "/source/CMSFiles/" + img;

                            img = Config.WebSiteUrl.TrimEnd('/') + img;
                        }

                        if (!string.IsNullOrEmpty(img))
                        {
                            if (!img.StartsWith("http"))
                            {
                                if (!img.StartsWith("/"))
                                    img = "/" + img;

                                img = Config.WebSiteUrl.TrimEnd('/') + img;
                            }
                        }

                        return new BusinessType
                        {
                            ID = r.Field<int>("ID"),
                            Name = r.Field<string>("Name"),
                            Description = r.Field<string>("Description"),
                            IsSelected = r.Field<int>("IsSelected"),
                            ImageName = img
                        };
                    }).ToList()
                    : new List<BusinessType>();

                    businessTypes = businessTypes
                        .OrderBy(bt => bt.Name == "Other" ? 1 : 0)
                        .ThenBy(bt => bt.Name)
                        .ToList();

                    objBusinessProfile[i].BusinessType = businessTypes.ToArray();

                    //DataTable dtProdutType = ds.Tables[1];
                    //objBusinessProfile[i].BusinessType = dtProdutType.Rows.Count > 0
                    //    ? dtProdutType.AsEnumerable().Select(r => new BusinessType
                    //    {
                    //        ID = r.Field<int>("ID"),
                    //        Name = r.Field<string>("Name"),
                    //        IsSelected = r.Field<int>("IsSelected")
                    //    }).ToArray()
                    //    : new BusinessType[0];

                    DataTable dtFood = ds.Tables[2];
                    objBusinessProfile[i].BusinessFoodItems = dtFood.Rows.Count > 0
                        ? dtFood.AsEnumerable().Select(r => new BusinessFoodItems
                        {
                            ID = r.Field<int>("ID"),
                            Name = r.Field<string>("Name"),
                            IsSelected = r.Field<int>("IsSelected")
                        }).ToArray()
                        : new BusinessFoodItems[0];

                    // ⭐ Dietary Types (Tables 5 = ALL, 6 = SELECTED)
                    DataTable dtDietary = ds.Tables.Count > 5 ? ds.Tables[5] : new DataTable();
                    DataTable dtSelectedDiet = ds.Tables.Count > 6 ? ds.Tables[6] : new DataTable();

                    List<int> selectedDietIDs = new List<int>();
                    if (dtSelectedDiet.Rows.Count > 0)
                    {
                        selectedDietIDs = dtSelectedDiet.AsEnumerable()
                            .Select(r => Convert.ToInt32(r["DietTypeID"]))
                            .ToList();
                    }

                    var dietaryList =
                        dtDietary.Rows.Count > 0
                        ? dtDietary.AsEnumerable().Select(r =>
                        {
                            string img = r["ImageName"] == DBNull.Value ? "" : r["ImageName"].ToString().Trim();

                            if (!string.IsNullOrEmpty(img) && !img.StartsWith("http"))
                            {
                                if (!img.StartsWith("/"))
                                    img = "/source/CMSFiles/" + img;

                                img = Config.WebSiteUrl.TrimEnd('/') + img;
                            }

                            return new DietaryTypesModel
                            {
                                ID = Convert.ToInt32(r["ID"]),
                                DietType = Convert.ToString(r["DietType"]),
                                Description = r["Description"] == DBNull.Value ? "" : r["Description"].ToString(),
                                ImageName = img,
                                IsSelected = selectedDietIDs.Contains(Convert.ToInt32(r["ID"])) ? 1 : 0
                            };
                        }).ToArray()
                        : new DietaryTypesModel[0];


                    objBusinessProfile[i].DietaryTypes = dietaryList;

                    DataTable dtCurrentDay = ds.Tables[3];
                    DataTable dtCurrentDayPrices =
                    ds.Tables.Count > 7 ? ds.Tables[7] : new DataTable();


                    if (dtCurrentDay.Rows.Count > 0)
                    {
                        DataRow r = dtCurrentDay.Rows[0];

                        var prices = dtCurrentDayPrices.AsEnumerable().ToList();

                        decimal op1 = 0, dp1 = 0, op2 = 0, dp2 = 0, op3 = 0, dp3 = 0;
                        int np1 = 0, np2 = 0, np3 = 0;

                        if (prices.Count > 0)
                        {
                            op1 = Convert.ToDecimal(prices[0]["OriginalPrice"]);
                            dp1 = Convert.ToDecimal(prices[0]["DiscountedPrice"]);
                            np1 = Convert.ToInt32(prices[0]["NumberOfPack"]);
                        }

                        if (prices.Count > 1)
                        {
                            op2 = Convert.ToDecimal(prices[1]["OriginalPrice"]);
                            dp2 = Convert.ToDecimal(prices[1]["DiscountedPrice"]);
                            np2 = Convert.ToInt32(prices[1]["NumberOfPack"]);
                        }

                        if (prices.Count > 2)
                        {
                            op3 = Convert.ToDecimal(prices[2]["OriginalPrice"]);
                            dp3 = Convert.ToDecimal(prices[2]["DiscountedPrice"]);
                            np3 = Convert.ToInt32(prices[2]["NumberOfPack"]);
                        }

                        objBusinessProfile[i].CurrentDaySchedule = new CurrentDaySchedule
                        {
                            BusinessID = Convert.ToInt64(r["BusinessID"]),
                            CurrentDate = r["CurrentDate"] != DBNull.Value ? Convert.ToDateTime(r["CurrentDate"]).ToString("dd-MM-yyyy") : "",
                            Pickup_from = Convert.ToString(r["Pickup_from"]),
                            Pickup_To = Convert.ToString(r["Pickup_To"]),
                            Repeted = r["Repeted"] != DBNull.Value ? Convert.ToInt32(r["Repeted"]) : 0,

                            OriginalPrice1 = op1,
                            DiscountedPrice1 = dp1,
                            NumberOfPack1 = np1,

                            OriginalPrice2 = op2,
                            DiscountedPrice2 = dp2,
                            NumberOfPack2 = np2,

                            OriginalPrice3 = op3,
                            DiscountedPrice3 = dp3,
                            NumberOfPack3 = np3
                        };

                        DataTable dtSelectedDays = ds.Tables[9];
                        List<string> selectedDaysList = new List<string>();

                        if (dtSelectedDays.Rows.Count > 0)
                        {
                            selectedDaysList = dtSelectedDays.AsEnumerable()
                                .Select(x => x["DayName"].ToString().ToUpper())
                                .ToList();
                        }

                        objBusinessProfile[i].CurrentDaySchedule.SelectedDays = selectedDaysList.ToArray();
                    }
                    else
                    {
                        objBusinessProfile[i].CurrentDaySchedule = null;
                    }


                    DataTable dtWeekly = ds.Tables[4];
                    DataTable dtWeeklyPrices =
                        ds.Tables.Count > 8 ? ds.Tables[8] : new DataTable();

                    objBusinessProfile[i].WeeklySchedule = new List<WeeklySchedule>();


                    foreach (DataRow row in dtWeekly.Rows)
                    {
                        int scheduleId = Convert.ToInt32(row["ID"]);
                        var prices = dtWeeklyPrices.AsEnumerable()
                            .Where(p => Convert.ToInt32(p["WeeklyScheduleID"]) == scheduleId)
                            .ToList();

                        decimal wop1 = 0, wdp1 = 0, wop2 = 0, wdp2 = 0, wop3 = 0, wdp3 = 0;
                        int wnp1 = 0, wnp2 = 0, wnp3 = 0;

                        if (prices.Count > 0)
                        {
                            wop1 = Convert.ToDecimal(prices[0]["OriginalPrice"]);
                            wdp1 = Convert.ToDecimal(prices[0]["DiscountedPrice"]);
                            wnp1 = Convert.ToInt32(prices[0]["NumberOfPack"]);
                        }

                        if (prices.Count > 1)
                        {
                            wop2 = Convert.ToDecimal(prices[1]["OriginalPrice"]);
                            wdp2 = Convert.ToDecimal(prices[1]["DiscountedPrice"]);
                            wnp2 = Convert.ToInt32(prices[1]["NumberOfPack"]);
                        }

                        if (prices.Count > 2)
                        {
                            wop3 = Convert.ToDecimal(prices[2]["OriginalPrice"]);
                            wdp3 = Convert.ToDecimal(prices[2]["DiscountedPrice"]);
                            wnp3 = Convert.ToInt32(prices[2]["NumberOfPack"]);
                        }

                        objBusinessProfile[i].WeeklySchedule.Add(new WeeklySchedule()
                        {
                            ID = scheduleId,
                            BusinessID = Convert.ToInt64(row["BusinessID"]),
                            DayNumber = Convert.ToInt32(row["DayNumber"]),
                            DayName = Convert.ToString(row["DayName"]),
                            CurrentDate = Convert.ToString(row["CurrentDate"]),
                            Pickup_from = Convert.ToString(row["Pickup_from"]),
                            Pickup_To = Convert.ToString(row["Pickup_To"]),
                            Repeted = Convert.ToInt32(row["Repeted"]),
                            CreatedOn = Convert.ToString(row["CreatedOn"]),
                            UpdatedOn = Convert.ToString(row["UpdatedOn"]),

                            WOriginalPrice1 = wop1,
                            WDiscountedPrice1 = wdp1,
                            WNumberOfPack1 = wnp1,

                            WOriginalPrice2 = wop2,
                            WDiscountedPrice2 = wdp2,
                            WNumberOfPack2 = wnp2,

                            WOriginalPrice3 = wop3,
                            WDiscountedPrice3 = wdp3,
                            WNumberOfPack3 = wnp3
                        });
                    }
                }

                var cleanResponse = new
                {
                    success = "true",
                    data = objBusinessProfile
                };
                strResponse = serializer.Serialize(cleanResponse);
                HttpContext.Current.Response.Write(strResponse);
                return;
            }
        }
        catch (Exception ex)
        {
            CommonAPI objCommonAPI = new CommonAPI();
            objCommonAPI.Error(ex.Message);
        }
    }

    //public void BusinessDetailByID(long UserID, string SecretKey, string AuthToken, long BusinessID)
    //{
    //    bool IsValidated = false;
    //    if (ValidateRequestBAL.BusinessValidateClientRequest(Convert.ToInt64(UserID), Convert.ToString(SecretKey), Convert.ToString(AuthToken)))
    //    {
    //        IsValidated = true;
    //    }

    //    if (!IsValidated)
    //    {
    //        CommonAPI objCommonAPI = new CommonAPI();
    //        objCommonAPI.Unauthorized();
    //    }
    //    else
    //    {
    //        string strResponse = string.Empty;
    //        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
    //        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
    //        try
    //        {
    //            Response objResponse = new Response();
    //            DataSet ds = new DataSet();
    //            DataTable dt = new DataTable();
    //            SalesAdminBAL objSalesAdminBAL = new SalesAdminBAL();
    //            objSalesAdminBAL.ID = BusinessID;

    //            ds = objSalesAdminBAL.BusinessDetailsByID();
    //            dt = ds.Tables[0];
    //            if (dt.Rows.Count > 0)
    //            {
    //                objResponse.success = "true";
    //                objResponse.message = dt.Rows.Count + " records found.";

    //                if (dt.Rows.Count > 0)
    //                {
    //                    objResponse.success = "true";
    //                    objResponse.message = dt.Rows.Count + " records found.";

    //                    BusinessProfile[] objBusinessProfile = new BusinessProfile[dt.Rows.Count];
    //                    for (int i = 0; i < dt.Rows.Count; i++)
    //                    {
    //                        objBusinessProfile[i] = new BusinessProfile();
    //                        objBusinessProfile[i].ID = Convert.ToInt64(dt.Rows[i]["ID"]);
    //                        objBusinessProfile[i].BusinessName = Convert.ToString(dt.Rows[i]["Name"]);
    //                        objBusinessProfile[i].ABN = Convert.ToString(dt.Rows[i]["ABN"]);
    //                        objBusinessProfile[i].StreetAddress = Convert.ToString(dt.Rows[i]["StreetAddress"]);
    //                        objBusinessProfile[i].Location = Convert.ToString(dt.Rows[i]["Location"]);
    //                        string FullName = Convert.ToString(dt.Rows[i]["FullName"]);
    //                        //if (Convert.ToString(dt.Rows[i]["LastName"]) != "")
    //                        //{
    //                        //    FullName = FullName + " " + Convert.ToString(dt.Rows[i]["LastName"]);
    //                        //}
    //                        objBusinessProfile[i].FullName = FullName;
    //                        objBusinessProfile[i].LastName = Convert.ToString(dt.Rows[i]["LastName"]);
    //                        //objBusinessProfile[i].FullName = Convert.ToString(dt.Rows[i]["FullName"]);
    //                        objBusinessProfile[i].EmailAddress = Convert.ToString(dt.Rows[i]["EmailAddress"]);
    //                        objBusinessProfile[i].Description = Convert.ToString(dt.Rows[i]["Description"]);
    //                        objBusinessProfile[i].BSBNo = Convert.ToString(dt.Rows[i]["BSBNo"]);
    //                        objBusinessProfile[i].AccountNumber = Convert.ToString(dt.Rows[i]["AccountNumber"]);
    //                        objBusinessProfile[i].BankName = Convert.ToString(dt.Rows[i]["BankName"]);
    //                        objBusinessProfile[i].AccountName = Convert.ToString(dt.Rows[i]["AccountName"]);
    //                        objBusinessProfile[i].Mobile = Convert.ToString(dt.Rows[i]["Mobile"]);
    //                        objBusinessProfile[i].BusinessPhone = Convert.ToString(dt.Rows[i]["BusinessPhone"]);

    //                        objBusinessProfile[i].StateID = Convert.ToInt32(dt.Rows[i]["StateID"]);
    //                        objBusinessProfile[i].StateCode = Convert.ToString(dt.Rows[i]["StateCode"]);
    //                        objBusinessProfile[i].StateFullName = Convert.ToString(dt.Rows[i]["StateName"]);

    //                        if (dt.Columns.Contains("GSTRegistered"))
    //                        {
    //                            objBusinessProfile[i].GSTRegistered = Convert.ToInt32(dt.Rows[i]["GSTRegistered"]);
    //                        }
    //                        else
    //                        {
    //                            objBusinessProfile[i].GSTRegistered = 0;
    //                        }

    //                        if (dt.Columns.Contains("ReceiveMarketingMails"))
    //                        {
    //                            objBusinessProfile[i].ReceiveMarketingMails = Convert.ToInt32(dt.Rows[i]["ReceiveMarketingMails"]);
    //                        }
    //                        else
    //                        {
    //                            objBusinessProfile[i].ReceiveMarketingMails = 0;
    //                        }

    //                        if (Convert.ToString(dt.Rows[i]["ProfilePhotoID"]) != string.Empty)
    //                            objBusinessProfile[i].ProfilePhotoID = Convert.ToInt64(dt.Rows[i]["ProfilePhotoID"]);
    //                        else
    //                            objBusinessProfile[i].ProfilePhotoID = 0;
    //                        if (Convert.ToString(dt.Rows[i]["ProfilePhoto"]) != string.Empty)
    //                        {
    //                            //objBusinessProfile[i].ProfilePhoto = Config.WebSiteUrl + Config.CMSFiles + Convert.ToString(dt.Rows[i]["ProfilePhoto"]);
    //                            //objBusinessProfile[i].ProfilePhoto = Config.WebSiteUrl + Convert.ToString(dt.Rows[i]["ProfilePhoto"]);

    //                            string profilePhoto = Convert.ToString(dt.Rows[i]["ProfilePhoto"]);
    //                            if (!string.IsNullOrEmpty(profilePhoto))
    //                            {
    //                                // ensure the path starts with "/"
    //                                if (!profilePhoto.StartsWith("/"))
    //                                    profilePhoto = "/" + profilePhoto;

    //                                // ensure Config.WebSiteUrl ends without trailing slash
    //                                profilePhoto = Config.WebSiteUrl.TrimEnd('/') + profilePhoto;
    //                            }
    //                            objBusinessProfile[i].ProfilePhoto = profilePhoto;
    //                            //string profilePhoto = Convert.ToString(dt.Rows[i]["ProfilePhoto"]);
    //                            //if (!string.IsNullOrEmpty(profilePhoto))
    //                            //{
    //                            //    if (!profilePhoto.StartsWith("http", StringComparison.OrdinalIgnoreCase))
    //                            //        profilePhoto = Config.WebSiteUrl.TrimEnd('/') + profilePhoto;
    //                            //}
    //                            //objBusinessProfile[i].ProfilePhoto = profilePhoto;


    //                        }
    //                        else
    //                        {
    //                            objBusinessProfile[i].ProfilePhoto = "";
    //                        }

    //                        if (Convert.ToString(dt.Rows[i]["StoreImages"]) != string.Empty)
    //                        {
    //                            string[] images = Convert.ToString(dt.Rows[i]["StoreImages"]).Split(',');
    //                            for (int j = 0; j < images.Length; j++)
    //                            {
    //                                string img = images[j].Trim();
    //                                if (!string.IsNullOrEmpty(img))
    //                                {
    //                                    if (!img.StartsWith("/"))
    //                                        img = "/" + img;

    //                                    if (!img.StartsWith("http", StringComparison.OrdinalIgnoreCase))
    //                                        img = Config.WebSiteUrl.TrimEnd('/') + img;
    //                                }
    //                                images[j] = img;
    //                            }
    //                            objBusinessProfile[i].StoreImages = images;
    //                        }
    //                        else
    //                        {
    //                            objBusinessProfile[i].StoreImages = new string[] { };
    //                        }
    //                        //if (Convert.ToString(dt.Rows[i]["StoreImages"]) != string.Empty)
    //                        //{
    //                        //    string[] images = Convert.ToString(dt.Rows[i]["StoreImages"]).Split(',');
    //                        //    for (int j = 0; j < images.Length; j++)
    //                        //    {
    //                        //        string img = images[j].Trim();
    //                        //        if (!string.IsNullOrEmpty(img) && !img.StartsWith("http", StringComparison.OrdinalIgnoreCase))
    //                        //            img = Config.WebSiteUrl.TrimEnd('/') + img;
    //                        //        images[j] = img;
    //                        //    }
    //                        //    objBusinessProfile[i].StoreImages = images;

    //                        //}
    //                        //else
    //                        //{
    //                        //    objBusinessProfile[i].StoreImages = new string[] { };
    //                        //}


    //                        objBusinessProfile[i].Status = Convert.ToInt16(dt.Rows[i]["Status"]) == 1 ? "True" : "False";


    //                        //Business Type
    //                        DataTable dtProdutType = new DataTable();
    //                        dtProdutType = ds.Tables[1];
    //                        if (dtProdutType.Rows.Count > 0)
    //                        {
    //                            objBusinessProfile[i].BusinessType = new BusinessType[dtProdutType.Rows.Count];
    //                            for (int j = 0; j < dtProdutType.Rows.Count; j++)
    //                            {
    //                                objBusinessProfile[i].BusinessType[j] = new BusinessType();
    //                                objBusinessProfile[i].BusinessType[j].ID = Convert.ToInt32(dtProdutType.Rows[j]["ID"]);
    //                                objBusinessProfile[i].BusinessType[j].Name = Convert.ToString(dtProdutType.Rows[j]["Name"]);
    //                                objBusinessProfile[i].BusinessType[j].IsSelected = Convert.ToInt32(dtProdutType.Rows[j]["Selected"]);

    //                            }
    //                        }
    //                        else
    //                        {
    //                            objBusinessProfile[i].BusinessType = new BusinessType[0];
    //                        }
    //                        //Business Type

    //                        //Food Items
    //                        DataTable dtFoodItems = new DataTable();
    //                        dtFoodItems = ds.Tables[2];
    //                        if (dtFoodItems.Rows.Count > 0)
    //                        {
    //                            objBusinessProfile[i].BusinessFoodItems = new BusinessFoodItems[dtFoodItems.Rows.Count];
    //                            for (int j = 0; j < dtFoodItems.Rows.Count; j++)
    //                            {
    //                                objBusinessProfile[i].BusinessFoodItems[j] = new BusinessFoodItems();
    //                                objBusinessProfile[i].BusinessFoodItems[j].ID = Convert.ToInt32(dtFoodItems.Rows[j]["ID"]);
    //                                objBusinessProfile[i].BusinessFoodItems[j].Name = Convert.ToString(dtFoodItems.Rows[j]["Name"]);
    //                                objBusinessProfile[i].BusinessFoodItems[j].IsSelected = Convert.ToInt32(dtFoodItems.Rows[j]["Selected"]);

    //                            }
    //                        }
    //                        else
    //                        {
    //                            objBusinessProfile[i].BusinessFoodItems = new BusinessFoodItems[0];
    //                        }
    //                        //Food Items

    //                        //Business Schedule Pickup Time 1 
    //                        DataTable dtSchedule = new DataTable();
    //                        dtSchedule = ds.Tables[3];
    //                        if (dtSchedule.Rows.Count > 0)
    //                        {
    //                            objBusinessProfile[i].BusinessSchedule = new BusinessSchedule[dtSchedule.Rows.Count];
    //                            for (int j = 0; j < dtSchedule.Rows.Count; j++)
    //                            {
    //                                objBusinessProfile[i].BusinessSchedule[j] = new BusinessSchedule();
    //                                objBusinessProfile[i].BusinessSchedule[j].DayNo = Convert.ToInt32(dtSchedule.Rows[j]["DayNo"]);
    //                                objBusinessProfile[i].BusinessSchedule[j].DayName = Convert.ToString(dtSchedule.Rows[j]["DayName"]);
    //                                objBusinessProfile[i].BusinessSchedule[j].NoOfItems = Convert.ToString(dtSchedule.Rows[j]["NoOfItems"]);
    //                                objBusinessProfile[i].BusinessSchedule[j].OriginalPrice = Convert.ToString(dtSchedule.Rows[j]["OriginalPrice"]);
    //                                objBusinessProfile[i].BusinessSchedule[j].DiscountID = Convert.ToInt32(dtSchedule.Rows[j]["DiscountID"]);
    //                                objBusinessProfile[i].BusinessSchedule[j].Discount = Convert.ToString(dtSchedule.Rows[j]["Discount"]);
    //                                objBusinessProfile[i].BusinessSchedule[j].PickupFromTime = Convert.ToDateTime(dtSchedule.Rows[j]["PickupFromTime"]).ToString("HH:mm");
    //                                objBusinessProfile[i].BusinessSchedule[j].PickupToTime = Convert.ToDateTime(dtSchedule.Rows[j]["PickupToTime"]).ToString("HH:mm");
    //                                objBusinessProfile[i].BusinessSchedule[j].setColor = Convert.ToInt32(dtSchedule.Rows[j]["ScheduleOn"]);
    //                            }
    //                        }
    //                        //Business Schedule Pickup Time 1 

    //                        //Business Schedule Pickup Time 2
    //                        dtSchedule = new DataTable();
    //                        dtSchedule = ds.Tables[5];
    //                        if (dtSchedule.Rows.Count > 0)
    //                        {
    //                            objBusinessProfile[i].BusinessSchedulePickupTime2 = new BusinessSchedulePickupTime2[dtSchedule.Rows.Count];
    //                            for (int j = 0; j < dtSchedule.Rows.Count; j++)
    //                            {
    //                                objBusinessProfile[i].BusinessSchedulePickupTime2[j] = new BusinessSchedulePickupTime2();
    //                                objBusinessProfile[i].BusinessSchedulePickupTime2[j].DayNo = Convert.ToInt32(dtSchedule.Rows[j]["DayNo"]);
    //                                objBusinessProfile[i].BusinessSchedulePickupTime2[j].DayName = Convert.ToString(dtSchedule.Rows[j]["DayName"]);
    //                                objBusinessProfile[i].BusinessSchedulePickupTime2[j].NoOfItems = Convert.ToString(dtSchedule.Rows[j]["NoOfItems"]);
    //                                objBusinessProfile[i].BusinessSchedulePickupTime2[j].OriginalPrice = Convert.ToString(dtSchedule.Rows[j]["OriginalPrice"]);
    //                                objBusinessProfile[i].BusinessSchedulePickupTime2[j].DiscountID = Convert.ToInt32(dtSchedule.Rows[j]["DiscountID"]);
    //                                objBusinessProfile[i].BusinessSchedulePickupTime2[j].Discount = Convert.ToString(dtSchedule.Rows[j]["Discount"]);
    //                                objBusinessProfile[i].BusinessSchedulePickupTime2[j].PickupFromTime = Convert.ToDateTime(dtSchedule.Rows[j]["PickupFromTime"]).ToString("HH:mm");
    //                                objBusinessProfile[i].BusinessSchedulePickupTime2[j].PickupToTime = Convert.ToDateTime(dtSchedule.Rows[j]["PickupToTime"]).ToString("HH:mm");
    //                                objBusinessProfile[i].BusinessSchedulePickupTime2[j].setColor = Convert.ToInt32(dtSchedule.Rows[j]["ScheduleOn"]);
    //                            }
    //                        }
    //                        //Business Schedule Pickup Time 2

    //                        //Business Schedule Pickup Time 3
    //                        dtSchedule = new DataTable();
    //                        dtSchedule = ds.Tables[6];
    //                        if (dtSchedule.Rows.Count > 0)
    //                        {
    //                            objBusinessProfile[i].BusinessSchedulePickupTime3 = new BusinessSchedulePickupTime3[dtSchedule.Rows.Count];
    //                            for (int j = 0; j < dtSchedule.Rows.Count; j++)
    //                            {
    //                                objBusinessProfile[i].BusinessSchedulePickupTime3[j] = new BusinessSchedulePickupTime3();
    //                                objBusinessProfile[i].BusinessSchedulePickupTime3[j].DayNo = Convert.ToInt32(dtSchedule.Rows[j]["DayNo"]);
    //                                objBusinessProfile[i].BusinessSchedulePickupTime3[j].DayName = Convert.ToString(dtSchedule.Rows[j]["DayName"]);
    //                                objBusinessProfile[i].BusinessSchedulePickupTime3[j].NoOfItems = Convert.ToString(dtSchedule.Rows[j]["NoOfItems"]);
    //                                objBusinessProfile[i].BusinessSchedulePickupTime3[j].OriginalPrice = Convert.ToString(dtSchedule.Rows[j]["OriginalPrice"]);
    //                                objBusinessProfile[i].BusinessSchedulePickupTime3[j].DiscountID = Convert.ToInt32(dtSchedule.Rows[j]["DiscountID"]);
    //                                objBusinessProfile[i].BusinessSchedulePickupTime3[j].Discount = Convert.ToString(dtSchedule.Rows[j]["Discount"]);
    //                                objBusinessProfile[i].BusinessSchedulePickupTime3[j].PickupFromTime = Convert.ToDateTime(dtSchedule.Rows[j]["PickupFromTime"]).ToString("HH:mm");
    //                                objBusinessProfile[i].BusinessSchedulePickupTime3[j].PickupToTime = Convert.ToDateTime(dtSchedule.Rows[j]["PickupToTime"]).ToString("HH:mm");
    //                                objBusinessProfile[i].BusinessSchedulePickupTime3[j].setColor = Convert.ToInt32(dtSchedule.Rows[j]["ScheduleOn"]);
    //                            }
    //                        }
    //                        //Business Schedule Pickup Time 3

    //                        //Restaurant Types
    //                        DataTable dtRestaurantTypes = new DataTable();
    //                        dtRestaurantTypes = ds.Tables[4];
    //                        if (dtRestaurantTypes.Rows.Count > 0)
    //                        {
    //                            objBusinessProfile[i].RestaurantTypes = new RestaurantTypes[dtRestaurantTypes.Rows.Count];
    //                            for (int j = 0; j < dtRestaurantTypes.Rows.Count; j++)
    //                            {
    //                                objBusinessProfile[i].RestaurantTypes[j] = new RestaurantTypes();
    //                                objBusinessProfile[i].RestaurantTypes[j].ID = Convert.ToInt32(dtRestaurantTypes.Rows[j]["ID"]);
    //                                objBusinessProfile[i].RestaurantTypes[j].Name = Convert.ToString(dtRestaurantTypes.Rows[j]["Name"]);

    //                            }
    //                        }
    //                        else
    //                        {
    //                            objBusinessProfile[i].RestaurantTypes = new RestaurantTypes[0];
    //                        }
    //                        //Restaurant Types


    //                        objResponse.BusinessProfile = objBusinessProfile;
    //                    }
    //                    string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
    //                    strResponseName = strResponseName.Replace("\"BusinessProfile\"", "\"data\"");
    //                    HttpContext.Current.Response.Write(strResponseName);
    //                    //HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
    //                }
    //            }
    //            else
    //            {
    //                NoRecordExists();
    //            }
    //        }
    //        catch (Exception Ex)
    //        {
    //            ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
    //        }
    //    }
    //    HttpContext.Current.Response.End();
    //}

    //[WebMethod]
    //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    //public void UpdateBusinessProfilePhoto()
    //{
    //    HttpContext context = HttpContext.Current;
    //    Response objResponse = new Response();

    //    try
    //    {
    //        long UserID = Convert.ToInt64(context.Request["UserID"]);
    //        string SecretKey = Convert.ToString(context.Request["SecretKey"]);
    //        string AuthToken = Convert.ToString(context.Request["AuthToken"]);
    //        long BusinessID = Convert.ToInt64(context.Request["BusinessID"]);

    //        HttpPostedFile postedFile = context.Request.Files["ProfilePhoto"];

    //        bool IsValidated = ValidateRequestBAL.BusinessValidateClientRequest(UserID, SecretKey, AuthToken);
    //        if (!IsValidated)
    //        {
    //            CommonAPI objCommonAPI = new CommonAPI();
    //            objCommonAPI.Unauthorized();
    //            return;
    //        }

    //        string savedFilePath = "";


    //        if (postedFile != null && postedFile.ContentLength > 0)
    //        {
    //            string folderPath = HttpContext.Current.Server.MapPath("~/Uploads/ProfilePhotos/");
    //            if (!Directory.Exists(folderPath))
    //                Directory.CreateDirectory(folderPath);

    //            string fileExt = Path.GetExtension(postedFile.FileName);
    //            string newFileName = "ProfileIcon_" + DateTime.Now.ToString("yyyyMMddHHmmss") + fileExt;
    //            string fullPath = Path.Combine(folderPath, newFileName);

    //            postedFile.SaveAs(fullPath);


    //            savedFilePath = "Uploads/ProfilePhotos/" + newFileName;
    //        }

    //        BusinessBAL objBusinessBAL = new BusinessBAL();
    //        objBusinessBAL.ID = BusinessID;
    //        objBusinessBAL.ProfilePhoto = savedFilePath; 
    //        long result = objBusinessBAL.UpdateBusinessProfilePhotoPath(); 

    //        objResponse.success = "true";
    //        objResponse.message = "Profile photo updated successfully.";
    //        objResponse.Data = new { ProfilePhoto = Config.WebSiteUrl + savedFilePath };

    //        context.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings()
    //        {
    //            NullValueHandling = NullValueHandling.Ignore
    //        }));
    //    }
    //    catch (Exception Ex)
    //    {
    //        ExceptionTRack(Ex.Message.ToString());
    //        objResponse.success = "false";
    //        objResponse.message = "Error: " + Ex.Message;
    //        HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse));
    //    }

    //    HttpContext.Current.Response.End();
    //}

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    public void UpdateBusinessProfilePhoto()
    {
        HttpContext context = HttpContext.Current;
        Response objResponse = new Response();

        try
        {
            long UserID = Convert.ToInt64(context.Request.Form["UserID"]);
            string SecretKey = Convert.ToString(context.Request.Form["SecretKey"]);
            string AuthToken = Convert.ToString(context.Request.Form["AuthToken"]);
            long BusinessID = Convert.ToInt64(context.Request.Form["BusinessID"]);
            int ProfilePhotoID = Convert.ToInt32(context.Request.Form["ProfilePhotoID"]);

            bool IsValidated = ValidateRequestBAL.BusinessValidateClientRequest(UserID, SecretKey, AuthToken);
            if (!IsValidated)
            {
                new CommonAPI().Unauthorized();
                return;
            }

            string uploadFolder = context.Server.MapPath("~/source/CMSFiles/");
            //if (!Directory.Exists(uploadFolder))
            //    Directory.CreateDirectory(uploadFolder);

            string profilePhotoPath = "";

            //DataTable dtOld = DbConnectionDAL.GetDataTable(CommandType.Text,
            //    "SELECT ISNULL(StoreImages, '') AS StoreImages FROM Business WHERE ID=" + BusinessID);
            //List<string> finalStoreImages = new List<string>();
            string finalStoreImages = "";

            //if (dtOld.Rows.Count > 0 && !string.IsNullOrEmpty(Convert.ToString(dtOld.Rows[0]["StoreImages"])))
            //{
            //    finalStoreImages = Convert.ToString(dtOld.Rows[0]["StoreImages"]).Split(',').ToList();
            //}

            if (context.Request.Files["ProfilePhoto"] != null && context.Request.Files["ProfilePhoto"].ContentLength > 0)
            {
                HttpPostedFile profileFile = context.Request.Files["ProfilePhoto"];
                string fileName = "Profile_" + BusinessID + Path.GetExtension(profileFile.FileName);
                string savePath = Path.Combine(uploadFolder, fileName);
                profileFile.SaveAs(savePath);
                //profilePhotoPath = "/Uploads/Business/" + fileName;
                profilePhotoPath = fileName;
            }

            foreach (string key in context.Request.Files.AllKeys)
            {
                if (key.StartsWith("StoreImages["))
                {
                    int index = int.Parse(key.Replace("StoreImages[", "").Replace("]", ""));
                    HttpPostedFile file = context.Request.Files[key];

                    if (file != null && file.ContentLength > 0)
                    {
                        string fileName = "Store_" + BusinessID + "_" + index + Path.GetExtension(file.FileName);
                        string savePath = Path.Combine(uploadFolder, fileName);
                        file.SaveAs(savePath);
                        //string newPath = "/Uploads/Business/" + fileName;
                        // string newPath = Config.WebSiteUrl.TrimEnd('/') + "/Uploads/Business/" + fileName;
                        //  string newPath = fileName;
                        finalStoreImages = fileName;

                        //if (finalStoreImages.Count > index)
                        //    finalStoreImages[index] = newPath;
                        //else
                        //{
                        //    while (finalStoreImages.Count <= index)
                        //        finalStoreImages.Add(string.Empty);
                        //    finalStoreImages[index] = newPath;
                        //}
                    }
                }
            }

            // string finalStoreImageString = string.Join(",", finalStoreImages);

            BusinessBAL objBusinessBAL = new BusinessBAL();
            objBusinessBAL.ID = BusinessID;
            objBusinessBAL.ProfilePhotoID = ProfilePhotoID;
            objBusinessBAL.ProfilePhoto = profilePhotoPath;
            objBusinessBAL.StoreImages = finalStoreImages;

            long result = objBusinessBAL.UpdateBusinessProfilePhoto();

            //var storeImagesArray = new Dictionary<string, string>();
            //for (int i = 0; i < finalStoreImages.Count; i++)
            //{
            //    if (!string.IsNullOrEmpty(finalStoreImages[i]))
            //        storeImagesArray.Add("StoreImages[" + i + "]", finalStoreImages[i]);
            //}

            var responseData = new
            {
                ProfilePhoto = profilePhotoPath,
                StoreImages = finalStoreImages
            };

            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(new
            {
                success = "true",
                message = "Business profile photo and store images updated successfully.",
                data = responseData
            }, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
        }
        catch (Exception ex)
        {
            objResponse.success = "false";
            objResponse.message = ex.Message;
            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse));
        }

        HttpContext.Current.Response.End();
    }

    //[WebMethod]
    //[ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    //public void UpdateBusinessProfilePhoto(long UserID, string SecretKey, string AuthToken, long BusinessID, int ProfilePhotoID, string ProfilePhoto = null, string StoreImages = null)
    //{
    //    bool IsValidated = false;
    //    if (ValidateRequestBAL.BusinessValidateClientRequest(Convert.ToInt64(UserID), Convert.ToString(SecretKey), Convert.ToString(AuthToken)))
    //    {
    //        IsValidated = true;
    //    }

    //    if (!IsValidated)
    //    {
    //        CommonAPI objCommonAPI = new CommonAPI();
    //        objCommonAPI.Unauthorized();
    //    }
    //    else
    //    {
    //        string strResponse = string.Empty;
    //        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
    //        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
    //        try
    //        {
    //            Response objResponse = new Response();
    //            BusinessBAL objBusinessBAL = new BusinessBAL();
    //            objBusinessBAL.ID = BusinessID;
    //            objBusinessBAL.ProfilePhotoID = ProfilePhotoID;
    //            long result = objBusinessBAL.UpdateBusinessProfilePhoto();

    //            switch (result)
    //            {
    //                default:
    //                    objResponse.success = "true";
    //                    objResponse.message = "Profile photo updated successfully.";
    //                    break;
    //            }

    //            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));

    //        }
    //        catch (Exception Ex)
    //        {
    //            ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
    //        }
    //    }
    //    HttpContext.Current.Response.End();
    //}

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void UpdateBusinessWhatToExpact(long UserID, string SecretKey, string AuthToken, long BusinessID, string FoodItems)
    {
        bool IsValidated = false;
        if (ValidateRequestBAL.BusinessValidateClientRequest(Convert.ToInt64(UserID), Convert.ToString(SecretKey), Convert.ToString(AuthToken)))
        {
            IsValidated = true;
        }

        if (!IsValidated)
        {
            CommonAPI objCommonAPI = new CommonAPI();
            objCommonAPI.Unauthorized();
        }
        else
        {
            string strResponse = string.Empty;
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            try
            {
                Response objResponse = new Response();
                BusinessBAL objBusinessBAL = new BusinessBAL();
                objBusinessBAL.ID = BusinessID;

                long result = objBusinessBAL.UpdateBusinessWhatToExpact(FoodItems);

                switch (result)
                {
                    default:
                        objResponse.success = "true";
                        objResponse.message = "Food items updated successfully.";
                        break;
                }

                HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));

            }
            catch (Exception Ex)
            {
                ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
            }
        }
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void UpdateBusinessTypes(long UserID, string SecretKey, string AuthToken, long BusinessID, string BusinessTypes, byte RegisterGst, byte CategoryTaxItemOrNot)
    {
        bool IsValidated = false;
        if (ValidateRequestBAL.BusinessValidateClientRequest(Convert.ToInt64(UserID), Convert.ToString(SecretKey), Convert.ToString(AuthToken)))
        {
            IsValidated = true;
        }

        if (!IsValidated)
        {
            CommonAPI objCommonAPI = new CommonAPI();
            objCommonAPI.Unauthorized();
        }
        else
        {
            string strResponse = string.Empty;
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            try
            {
                Response objResponse = new Response();
                BusinessBAL objBusinessBAL = new BusinessBAL();
                objBusinessBAL.ID = BusinessID;
                objBusinessBAL.RegisterGst = RegisterGst;
                objBusinessBAL.CategoryTaxItemOrNot = CategoryTaxItemOrNot;

                long result = objBusinessBAL.UpdateBusinessTypes(BusinessTypes, RegisterGst, CategoryTaxItemOrNot);

                switch (result)
                {
                    default:
                        objResponse.success = "true";
                        objResponse.message = "Business types details updated successfully.";
                        break;
                }

                HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));

            }
            catch (Exception Ex)
            {
                ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
            }
        }
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void UpdateDietaryTypes(long UserID, string SecretKey, string AuthToken, long BusinessID, string DietaryTypes)
    {
        bool IsValidated = false;
        if (ValidateRequestBAL.BusinessValidateClientRequest(
            Convert.ToInt64(UserID),
            Convert.ToString(SecretKey),
            Convert.ToString(AuthToken)))
        {
            IsValidated = true;
        }

        if (!IsValidated)
        {
            CommonAPI objCommonAPI = new CommonAPI();
            objCommonAPI.Unauthorized();
            return;
        }

        try
        {
            Response objResponse = new Response();
            BusinessBAL objBusinessBAL = new BusinessBAL();
            objBusinessBAL.ID = BusinessID;

            // ⭐ Call BAL
            long result = BusinessBAL.UpdateDietaryTypes(BusinessID, DietaryTypes);

            objResponse.success = "true";
            objResponse.message = "Dietary types updated successfully.";

            HttpContext.Current.Response.Write(
                JsonConvert.SerializeObject(objResponse,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })
            );
        }
        catch (Exception ex)
        {
            ExceptionTRack(ex.Message);
        }

        HttpContext.Current.Response.End();
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void BusinessReport(long UserID, string SecretKey, string AuthToken, long BusinessID, string FromDate, string ToDate)
    {
        bool IsValidated = false;
        if (ValidateRequestBAL.BusinessValidateClientRequest(Convert.ToInt64(UserID), Convert.ToString(SecretKey), Convert.ToString(AuthToken)))
        {
            IsValidated = true;
        }

        if (!IsValidated)
        {
            CommonAPI objCommonAPI = new CommonAPI();
            objCommonAPI.Unauthorized();
        }
        else
        {
            string strResponse = string.Empty;
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            try
            {
                Response objResponse = new Response();
                DataTable dt = new DataTable();
                SalesAdminBAL objSalesAdminBAL = new SalesAdminBAL();
                objSalesAdminBAL.ID = BusinessID;

                dt = objSalesAdminBAL.BusinessMonthlyReport(Convert.ToDateTime(FromDate), Convert.ToDateTime(ToDate));

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows.Count > 0)
                    {
                        objResponse.success = "true";
                        objResponse.message = dt.Rows.Count + " records found.";

                        BusinessReport[] objBusinessReport = new BusinessReport[dt.Rows.Count];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            objBusinessReport[i] = new BusinessReport();
                            objBusinessReport[i].Month = Convert.ToString(dt.Rows[i]["MN"]);
                            objBusinessReport[i].Year = Convert.ToString(dt.Rows[i]["Y"]);
                            objBusinessReport[i].Price = Convert.ToDouble(dt.Rows[i]["P"]).ToString("f2");

                            objResponse.BusinessReport = objBusinessReport;
                        }
                        string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                        strResponseName = strResponseName.Replace("\"BusinessReport\"", "\"data\"");
                        HttpContext.Current.Response.Write(strResponseName);
                    }
                }
                else
                {
                    NoRecordExists();
                }
            }
            catch (Exception Ex)
            {
                ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
            }
        }
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void BusinessCustomHolidayList(long UserID, string SecretKey, string AuthToken, long BusinessID)
    {
        bool IsValidated = false;
        if (ValidateRequestBAL.BusinessValidateClientRequest(Convert.ToInt64(UserID), Convert.ToString(SecretKey), Convert.ToString(AuthToken)))
        {
            IsValidated = true;
        }

        if (!IsValidated)
        {
            CommonAPI objCommonAPI = new CommonAPI();
            objCommonAPI.Unauthorized();
        }
        else
        {
            string strResponse = string.Empty;
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            try
            {
                Response objResponse = new Response();
                DataTable dt = new DataTable();
                CommonBAL objCommonBAL = new CommonBAL();

                dt = objCommonBAL.BusinessCustomHolidayList_Webservice(BusinessID);

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows.Count > 0)
                    {
                        objResponse.success = "true";
                        objResponse.message = dt.Rows.Count + " records found.";

                        BusinessCustomHolidays[] objBusinessCustomHolidays = new BusinessCustomHolidays[dt.Rows.Count];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            objBusinessCustomHolidays[i] = new BusinessCustomHolidays();
                            objBusinessCustomHolidays[i].BusinessHolidayID = Convert.ToInt64(dt.Rows[i]["ID"]);
                            objBusinessCustomHolidays[i].BusinessID = Convert.ToInt64(dt.Rows[i]["BusinessID"]);
                            objBusinessCustomHolidays[i].HolidayFromDate = Convert.ToDateTime(dt.Rows[i]["HolidayFromDate"]).ToString("dd/MMM/yyyy");
                            objBusinessCustomHolidays[i].HolidayToDate = Convert.ToDateTime(dt.Rows[i]["HolidayToDate"]).ToString("dd/MMM/yyyy");
                            objBusinessCustomHolidays[i].Title = Convert.ToString(dt.Rows[i]["Title"]);

                            objResponse.BusinessCustomHolidays = objBusinessCustomHolidays;
                        }
                        string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                        strResponseName = strResponseName.Replace("\"BusinessCustomHolidays\"", "\"data\"");
                        HttpContext.Current.Response.Write(strResponseName);
                    }
                }
                else
                {
                    NoRecordExists();
                }
            }
            catch (Exception Ex)
            {
                ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
            }
        }
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void BusinessPublicHolidayList(long UserID, string SecretKey, string AuthToken, long BusinessID, int StateID)
    {
        bool IsValidated = false;
        if (ValidateRequestBAL.BusinessValidateClientRequest(Convert.ToInt64(UserID), Convert.ToString(SecretKey), Convert.ToString(AuthToken)))
        {
            IsValidated = true;
        }

        if (!IsValidated)
        {
            CommonAPI objCommonAPI = new CommonAPI();
            objCommonAPI.Unauthorized();
        }
        else
        {
            string strResponse = string.Empty;
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            try
            {
                Response objResponse = new Response();
                DataTable dt = new DataTable();
                CommonBAL objCommonBAL = new CommonBAL();

                dt = objCommonBAL.BusinessPublicHolidayList_Webservice(BusinessID, StateID);

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows.Count > 0)
                    {
                        objResponse.success = "true";
                        objResponse.message = dt.Rows.Count + " records found.";

                        BusinessPublicHolidays[] objBusinessPublicHolidays = new BusinessPublicHolidays[dt.Rows.Count];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            objBusinessPublicHolidays[i] = new BusinessPublicHolidays();
                            objBusinessPublicHolidays[i].StateHolidayID = Convert.ToInt32(dt.Rows[i]["StateHolidayID"]);
                            objBusinessPublicHolidays[i].BusinessID = Convert.ToInt64(dt.Rows[i]["BusinessID"]);
                            objBusinessPublicHolidays[i].OnOff = Convert.ToInt32(dt.Rows[i]["OnOff"]);
                            objBusinessPublicHolidays[i].Title = Convert.ToString(dt.Rows[i]["Title"]);

                            objResponse.BusinessPublicHolidays = objBusinessPublicHolidays;
                        }
                        string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                        strResponseName = strResponseName.Replace("\"BusinessPublicHolidays\"", "\"data\"");
                        HttpContext.Current.Response.Write(strResponseName);
                    }
                }
                else
                {
                    NoRecordExists();
                }
            }
            catch (Exception Ex)
            {
                ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
            }
        }
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void BusinessReportDetails(long UserID, string SecretKey, string AuthToken, long BusinessID)
    {
        bool IsValidated = false;
        if (ValidateRequestBAL.BusinessValidateClientRequest(Convert.ToInt64(UserID), Convert.ToString(SecretKey), Convert.ToString(AuthToken)))
        {
            IsValidated = true;
        }

        if (!IsValidated)
        {
            CommonAPI objCommonAPI = new CommonAPI();
            objCommonAPI.Unauthorized();
        }
        else
        {
            string strResponse = string.Empty;
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            try
            {
                Response objResponse = new Response();
                objResponse.success = "true";
                objResponse.message = "1 records found.";

                BusinessReportDetails objBusinessReportDetails = new BusinessReportDetails();

                objBusinessReportDetails.TotalIncome = "$1250";
                objBusinessReportDetails.Co2Saved = "2.5 kg";
                objBusinessReportDetails.MealsRescued = "156";
                objBusinessReportDetails.FoodWastePrevented = "$4.99";
                objBusinessReportDetails.Summary = Convert.ToString(new GeneralSettings().getConfigValue("analyaticcode"));
                objResponse.BusinessReportDetails = objBusinessReportDetails;

                string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                strResponseName = strResponseName.Replace("\"BusinessReportDetails\"", "\"data\"");
                HttpContext.Current.Response.Write(strResponseName);
            }
            catch (Exception Ex)
            {
                ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
            }
        }
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void BusinessSubscriptionStatus(long UserID, string SecretKey, string AuthToken, long BusinessID)
    {
        bool IsValidated = false;
        if (ValidateRequestBAL.BusinessValidateClientRequest(Convert.ToInt64(UserID), Convert.ToString(SecretKey), Convert.ToString(AuthToken)))
        {
            IsValidated = true;
        }

        if (!IsValidated)
        {
            CommonAPI objCommonAPI = new CommonAPI();
            objCommonAPI.Unauthorized();
        }
        else
        {
            string strResponse = string.Empty;
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            try
            {
                Response objResponse = new Response();
                objResponse.success = "true";
                objResponse.message = "1 records found.";

                DataTable dt = new DataTable();
                dt = BusinessSubscriptionStatus(BusinessID);


                if (dt.Rows.Count > 0)
                {

                    BusinessSubscriptionStatusDetails objBusinessSubscriptionStatusDetails = new BusinessSubscriptionStatusDetails();

                    objBusinessSubscriptionStatusDetails.SubscriptionActive = Convert.ToString(dt.Rows[0]["SubscriptionActive"]);
                    if (Convert.ToString(dt.Rows[0]["DeactivatedDate"]) == string.Empty)
                    {
                        objBusinessSubscriptionStatusDetails.DeactivatedDate = "";
                    }
                    else
                    {
                        objBusinessSubscriptionStatusDetails.DeactivatedDate = Convert.ToDateTime(dt.Rows[0]["DeactivatedDate"]).ToString("dd/MMM/yyyy");
                    }
                    objBusinessSubscriptionStatusDetails.StartDate = Convert.ToDateTime(dt.Rows[0]["StartDate"]).ToString("dd/MMM/yyyy");
                    objBusinessSubscriptionStatusDetails.EndDate = Convert.ToDateTime(dt.Rows[0]["EndDate"]).ToString("dd/MMM/yyyy");

                    objResponse.BusinessSubscriptionStatusDetails = objBusinessSubscriptionStatusDetails;

                    string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                    strResponseName = strResponseName.Replace("\"BusinessSubscriptionStatusDetails\"", "\"data\"");
                    HttpContext.Current.Response.Write(strResponseName);
                }
                else
                {
                    objResponse.success = "false";
                    objResponse.message = "No subscription found.";
                    BusinessSubscriptionStatusDetails objBusinessSubscriptionStatusDetails = new BusinessSubscriptionStatusDetails();
                    objResponse.BusinessSubscriptionStatusDetails = objBusinessSubscriptionStatusDetails;


                    HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));

                }
            }
            catch (Exception Ex)
            {
                ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
            }
        }
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void BusinessCardList(long UserID, string SecretKey, string AuthToken, long BusinessID)
    {
        bool IsValidated = false;

        if (ValidateRequestBAL.BusinessValidateClientRequest(Convert.ToInt64(UserID), Convert.ToString(SecretKey), Convert.ToString(AuthToken)))
        {
            IsValidated = true;
        }

        if (!IsValidated)
        {
            CommonAPI objCommonAPI = new CommonAPI();
            objCommonAPI.Unauthorized();
        }
        else
        {
            string strResponse = string.Empty;
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            try
            {
                Response objResponse = new Response();
                DataTable dt = new DataTable();

                dt = GetBusinessCardList(BusinessID);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        objResponse.success = "true";
                        objResponse.message = dt.Rows.Count + " records found.";

                        BusinessCards[] objBusinessCards = new BusinessCards[dt.Rows.Count];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            objBusinessCards[i] = new BusinessCards();
                            objBusinessCards[i].ID = Convert.ToInt64(dt.Rows[i]["ID"]);
                            objBusinessCards[i].CustomerID = Convert.ToString(dt.Rows[i]["CustomerID"]);
                            objBusinessCards[i].LastDigits = Convert.ToString(dt.Rows[i]["LastDigits"]);
                            objBusinessCards[i].CardType = Convert.ToString(dt.Rows[i]["CardType"]);
                            objBusinessCards[i].IsDefault = Convert.ToInt32(dt.Rows[i]["IsDefault"]);
                            objBusinessCards[i].ExpiryMonth = Convert.ToString(dt.Rows[i]["ExpiryMonth"]);
                            objBusinessCards[i].ExpiryYear = Convert.ToString(dt.Rows[i]["ExpiryYear"]);

                            objResponse.BusinessCards = objBusinessCards;
                        }
                        string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                        strResponseName = strResponseName.Replace("\"BusinessCards\"", "\"data\"");
                        HttpContext.Current.Response.Write(strResponseName);

                    }
                    else
                    {
                        NoRecordExists();
                    }
                }
                else
                {
                    NoRecordExists();
                }
            }
            catch (Exception Ex)
            {
                ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
            }
        }
        HttpContext.Current.Response.End();
    }

    #endregion


    #region User

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void UserDetailByID(long UserID, string SecretKey, string AuthToken, long ID)
    {
        bool IsValidated = false;
        // ================User Last Login Location Track=============================
        string strSuburb = string.Empty;
        if (HttpContext.Current.Request["Suburb"] != null)
        {
            strSuburb = Convert.ToString(HttpContext.Current.Request["Suburb"]);
        }
        // ================User Last Login Location Track=============================

        if (ValidateRequestBAL.UserValidateClientRequest(Convert.ToInt64(UserID), Convert.ToString(SecretKey), Convert.ToString(AuthToken), strSuburb))
        {
            IsValidated = true;
        }

        if (!IsValidated)
        {
            CommonAPI objCommonAPI = new CommonAPI();
            objCommonAPI.Unauthorized();
        }
        else
        {
            string strResponse = string.Empty;
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            try
            {
                Response objResponse = new Response();
                DataTable dt = new DataTable();
                UsersBAL objUsersBAL = new UsersBAL();

                objUsersBAL.ID = ID;

                dt = objUsersBAL.UserDetailsByID();
                if (dt.Rows.Count > 0)
                {
                    objResponse.success = "true";
                    objResponse.message = dt.Rows.Count + " records found.";


                    UserProfile[] objUserProfile = new UserProfile[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        objUserProfile[i] = new UserProfile();
                        objUserProfile[i].ID = Convert.ToInt64(dt.Rows[i]["ID"]);
                        objUserProfile[i].FullName = Convert.ToString(dt.Rows[i]["Name"]);
                        objUserProfile[i].LastName = Convert.ToString(dt.Rows[i]["LastName"]);
                        objUserProfile[i].EmailAddress = Convert.ToString(dt.Rows[i]["Email"]);
                        objUserProfile[i].Mobile = Convert.ToString(dt.Rows[i]["Mobile"]);
                        if (Convert.ToString(dt.Rows[i]["StateID"]) != string.Empty)
                            objUserProfile[i].StateID = Convert.ToInt32(dt.Rows[i]["StateID"]);
                        else
                            objUserProfile[i].StateID = 0;
                        objUserProfile[i].StateName = Convert.ToString(dt.Rows[i]["StateName"]);

                        //objUserProfile[i].Gender = Convert.ToString(dt.Rows[i]["Gender"]);
                        objUserProfile[i].StreetAddress = Convert.ToString(dt.Rows[i]["StreetAddress"]);
                        objUserProfile[i].Location = Convert.ToString(dt.Rows[i]["Location"]);
                        //if (Convert.ToString(dt.Rows[i]["BirthDate"]) != string.Empty)
                        //    objUserProfile[i].BirthDate = Convert.ToDateTime(dt.Rows[i]["BirthDate"]).ToString("MM/dd/yyyy");
                        //else
                        //    objUserProfile[i].BirthDate = string.Empty;
                        objUserProfile[i].RewardsPoints = Convert.ToString(dt.Rows[i]["RewardsPoints"]).Replace(".00", "");
                        objResponse.UserProfile = objUserProfile;
                    }
                    string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                    strResponseName = strResponseName.Replace("\"UserProfile\"", "\"data\"");
                    HttpContext.Current.Response.Write(strResponseName);
                    //HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));

                }
                else
                {
                    NoRecordExists();
                }
            }
            catch (Exception Ex)
            {
                ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
            }
        }
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void ProductList(long UserID, string SecretKey, string AuthToken, string CaregoryID, string Latitude, string Longitude, int Distance, decimal MinPrice, decimal MaxPrice)
    {
        bool IsValidated = false;
        // ================User Last Login Location Track=============================
        string strSuburb = string.Empty;
        string strSuburb1 = string.Empty;
        if (HttpContext.Current.Request["Suburb1"] != null)
        {
            strSuburb1 = Convert.ToString(HttpContext.Current.Request["Suburb1"]);
        }

        string strSuburb2 = string.Empty;
        if (HttpContext.Current.Request["Suburb2"] != null)
        {
            strSuburb2 = Convert.ToString(HttpContext.Current.Request["Suburb2"]);
        }
        string strSuburb3 = string.Empty;
        if (HttpContext.Current.Request["Suburb3"] != null)
        {
            strSuburb3 = Convert.ToString(HttpContext.Current.Request["Suburb3"]);
        }
        if (strSuburb1 != string.Empty && strSuburb2 != string.Empty && strSuburb3 != string.Empty)
        {
            strSuburb = strSuburb1.Trim() + "###" + strSuburb2.Trim() + "###" + strSuburb3.Trim();
        }
        //if (Postcode != "")
        //{
        //    strSuburb = strSuburb + "$$$" + Postcode;
        //}

        // ================User Last Login Location Track=============================

        if (UserID == 0)
        {
            if (ValidateRequestBAL.UserValidateClientRequestWithoutLogin(Convert.ToString(SecretKey), Convert.ToString(AuthToken)))
            {
                IsValidated = true;
            }
        }
        else
        {

            if (ValidateRequestBAL.UserValidateClientRequest(Convert.ToInt64(UserID), Convert.ToString(SecretKey), Convert.ToString(AuthToken), strSuburb))
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
            string strResponse = string.Empty;
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            try
            {
                Response objResponse = new Response();
                DataTable dt = new DataTable();


                string strCategory = string.Empty;
                string strSearch1 = string.Empty;
                string strSearch2 = string.Empty;
                string strSearch3 = string.Empty;

                #region Old code
                //int postcode = 0;

                //if (Location != string.Empty)
                //{
                //    string strLoc = Convert.ToString(Location);
                //    if (strLoc.IndexOf(",") >= 0)
                //    {
                //        string[] strLocSplit = strLoc.Split(',');
                //        for (int i = 1; i <= strLocSplit.Length; i++)
                //        {
                //            if (i == 1)
                //            {
                //                strSearch1 = Convert.ToString(strLocSplit[0]).Trim();
                //            }
                //            if (i == 2)
                //            {
                //                strSearch2 = Convert.ToString(strLocSplit[1]).Trim();
                //            }
                //            if (i == 3)
                //            {
                //                strSearch3 = Convert.ToString(strLocSplit[2]).Trim();
                //            }
                //        }
                //    }
                //    else
                //    {
                //        strSearch1 = strLoc;
                //    }

                //}
                //try
                //{

                //    if (strSearch1 != string.Empty)
                //    {
                //        if (Convert.ToInt32(strSearch1) % 1 == 0)
                //        {
                //            postcode = Convert.ToInt32(strSearch1);
                //        }
                //    }
                //}
                //catch (Exception ex) { }


                //try
                //{
                //    if (strSearch2 != string.Empty)
                //    {
                //        if (Convert.ToInt32(strSearch2) % 1 == 0)
                //        {
                //            postcode = Convert.ToInt32(strSearch2);
                //        }
                //    }
                //}
                //catch (Exception ex) { }


                //try
                //{
                //    if (strSearch3 != string.Empty)
                //    {
                //        if (Convert.ToInt32(strSearch3) % 1 == 0)
                //        {
                //            postcode = Convert.ToInt32(strSearch3);
                //        }
                //    }
                //}
                //catch (Exception ex) { }

                //if (Postcode != string.Empty)
                //{
                //    postcode = Convert.ToInt32(Postcode);
                //}

                #endregion

                int TotalRecord = 0;
                BusinessBAL objBusinessBAL = new BusinessBAL();
                int CurrentPage = 1;
                int TomorrowCurrentPage = 1;
                // dt = objBusinessBAL.BusinessProductList(UserID, CaregoryID, strSearch1, strSearch2, strSearch3, postcode, ref CurrentPage, 1000, out TotalRecord, Latitude, Longitude);
                string RestaurantType = "";
                int ActiveDeals = 0;
                int PickupNow = 0;
                string PickupFromTime = "";
                string PickupToTime = "";
                if (HttpContext.Current.Request["ActiveDeals"] != null)
                {
                    ActiveDeals = Convert.ToInt32(HttpContext.Current.Request["ActiveDeals"]);
                }
                if (HttpContext.Current.Request["PickupNow"] != null)
                {
                    PickupNow = Convert.ToInt32(HttpContext.Current.Request["PickupNow"]);
                }
                if (HttpContext.Current.Request["RestaurantTypes"] != null)
                {
                    RestaurantType = Convert.ToString(HttpContext.Current.Request["RestaurantTypes"]);
                }
                if (HttpContext.Current.Request["PickupFromTime"] != null)
                {
                    PickupFromTime = Convert.ToString(HttpContext.Current.Request["PickupFromTime"]);
                }
                if (HttpContext.Current.Request["PickupToTime"] != null)
                {
                    PickupToTime = Convert.ToString(HttpContext.Current.Request["PickupToTime"]);
                }
                int Version = 0;
                if (HttpContext.Current.Request["Version"] != null)
                {
                    Version = 1;
                }

                string StateCode = "VIC";
                if (HttpContext.Current.Request["StateCode"] != null)
                {
                    StateCode = Convert.ToString(HttpContext.Current.Request["StateCode"]).ToUpper();
                }

                string DietaryFilter = "";
                if (HttpContext.Current.Request["DietaryFilter"] != null)
                {
                    DietaryFilter = Convert.ToString(HttpContext.Current.Request["DietaryFilter"]);
                }

                if (HttpContext.Current.Request["SearchText"] != null)
                {
                    strSearch1 = Convert.ToString(HttpContext.Current.Request["SearchText"]);
                }


                // Today Products -----------------------
                dt = BusinessProductListDAL(UserID, CaregoryID, strSearch1, strSearch2, strSearch3, 0, ref CurrentPage, 10000, out TotalRecord, Latitude, Longitude, RestaurantType, ActiveDeals, PickupNow, Version, PickupFromTime, PickupToTime, StateCode, Distance, DietaryFilter, MinPrice, MaxPrice, 0);
                DataView dvProducts = new DataView(dt);
                dvProducts.Sort = "GeoDistance ASC, Name ASC";
                dt = dvProducts.ToTable();

                // Tomorrow Products -----------------------
                DataTable dtTommrow = new DataTable();
                dtTommrow = BusinessProductListDAL(UserID, CaregoryID, strSearch1, strSearch2, strSearch3, 0, ref TomorrowCurrentPage, 10000, out TotalRecord, Latitude, Longitude, RestaurantType, ActiveDeals, PickupNow, Version, PickupFromTime, PickupToTime, StateCode, Distance, DietaryFilter, MinPrice, MaxPrice, 1);
                DataView dvTomorrowProducts = new DataView(dtTommrow);
                dvTomorrowProducts.Sort = "GeoDistance ASC, Name ASC";
                dtTommrow = dvTomorrowProducts.ToTable();

                if (dt.Rows.Count == 0 && dtTommrow.Rows.Count == 0)
                {
                    NoRecordExists();
                }
                else
                {
                    objResponse.success = "true";
                    objResponse.message = dt.Rows.Count + " records found.";

                    // Today Products -----------------------
                    if (dt.Rows.Count > 0)
                    {
                        Products[] objProducts = new Products[dt.Rows.Count];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            objProducts[i] = new Products();
                            objProducts[i].ID = Convert.ToInt64(dt.Rows[i]["PriceID"]);
                            objProducts[i].BusinessName = Convert.ToString(dt.Rows[i]["Name"]);
                            objProducts[i].FullName = Convert.ToString(dt.Rows[i]["FullName"]);
                            objProducts[i].EmailAddress = Convert.ToString(dt.Rows[i]["EmailAddress"]);
                            objProducts[i].BusinessPhone = Convert.ToString(dt.Rows[i]["BusinessPhone"]);
                            objProducts[i].Mobile = Convert.ToString(dt.Rows[i]["Mobile"]);
                            objProducts[i].StreetAddress = Convert.ToString(dt.Rows[i]["StreetAddress"]);
                            objProducts[i].Location = Convert.ToString(dt.Rows[i]["Location"]);
                            objProducts[i].TotalQty = Convert.ToInt32(dt.Rows[i]["TotalQty"]);
                            objProducts[i].PurchaseQty = Convert.ToInt32(dt.Rows[i]["PurchaseQty"]);

                            if (Convert.ToInt32(dt.Rows[i]["RemainingQty"]) < 0)
                                objProducts[i].RemainingQty = 0;
                            else
                                objProducts[i].RemainingQty = Convert.ToInt32(dt.Rows[i]["FinalRemainingQty"]);
                            objProducts[i].Latitude = Convert.ToString(dt.Rows[i]["Latitude"]);
                            objProducts[i].Longitude = Convert.ToString(dt.Rows[i]["Longitude"]);
                            objProducts[i].IsFavourite = Convert.ToInt32(dt.Rows[i]["ISFavourite"]);
                            objProducts[i].OriginalPrice = Convert.ToDouble(dt.Rows[i]["OriginalPrice"]).ToString("f2");
                            objProducts[i].DiscountPrice = Convert.ToDouble(dt.Rows[i]["DiscountPrice"]).ToString("f2");
                            objProducts[i].Description = Convert.ToString(dt.Rows[i]["Description"]);
                            if (Convert.ToString(dt.Rows[i]["Pickup_from"]) != string.Empty)
                            {
                                var fromTime = (TimeSpan)dt.Rows[i]["Pickup_from"];
                                objProducts[i].PickupFromTime = DateTime.Today
                                                                    .Add(fromTime)
                                                                    .ToString("hh:mm tt");

                            }
                            else
                                objProducts[i].PickupFromTime = string.Empty;


                            if (Convert.ToString(dt.Rows[i]["Pickup_To"]) != string.Empty)
                            {
                                var toTime = (TimeSpan)dt.Rows[i]["Pickup_To"];
                                objProducts[i].PickupToTime = DateTime.Today
                                                                    .Add(toTime)
                                                                    .ToString("hh:mm tt");
                            }
                            else
                                objProducts[i].PickupToTime = string.Empty;

                            if (Convert.ToString(dt.Rows[i]["GeoDistance"]) != string.Empty)
                            {
                                if (Convert.ToDecimal(dt.Rows[i]["GeoDistance"]) * 1000 > 1000)
                                    objProducts[i].Distance = Convert.ToInt32(dt.Rows[i]["GeoDistance"]) + " KM";
                                else
                                    objProducts[i].Distance = Convert.ToInt32(Convert.ToDecimal(dt.Rows[i]["GeoDistance"]) * 1000) + " M";
                            }
                            else
                                objProducts[i].Distance = "0M";

                            if (Convert.ToString(dt.Rows[i]["StoreImages"]) != string.Empty)
                                objProducts[i].StoreImages = Config.WebSiteUrl + Config.CMSFiles + Convert.ToString(dt.Rows[i]["StoreImages"]);
                            else
                                objProducts[i].StoreImages = "";

                            if (Convert.ToString(dt.Rows[i]["ProfilePhoto"]) != string.Empty)
                                objProducts[i].ProfilePhoto = Config.WebSiteUrl + Config.CMSFiles + Convert.ToString(dt.Rows[i]["ProfilePhoto"]);
                            else
                                objProducts[i].ProfilePhoto = "";

                            objProducts[i].Rating = "0";
                            objProducts[i].DietryType = Convert.ToString(dt.Rows[i]["DietryType"]);
                            objProducts[i].StoreName = Convert.ToString(dt.Rows[i]["StoreName"]);

                            objResponse.Products = objProducts;
                        }

                    }
                    else
                    {
                        objResponse.Products = new Products[0];
                    }


                    // Tomorrow Products -----------------------
                    if (dtTommrow.Rows.Count > 0)
                    {
                        Products[] objTomorrowProducts = new Products[dtTommrow.Rows.Count];
                        for (int i = 0; i < dtTommrow.Rows.Count; i++)
                        {
                            objTomorrowProducts[i] = new Products();
                            objTomorrowProducts[i].ID = Convert.ToInt64(dtTommrow.Rows[i]["ID"]);
                            objTomorrowProducts[i].BusinessName = Convert.ToString(dtTommrow.Rows[i]["Name"]);
                            objTomorrowProducts[i].FullName = Convert.ToString(dtTommrow.Rows[i]["FullName"]);
                            objTomorrowProducts[i].EmailAddress = Convert.ToString(dtTommrow.Rows[i]["EmailAddress"]);
                            objTomorrowProducts[i].BusinessPhone = Convert.ToString(dtTommrow.Rows[i]["BusinessPhone"]);
                            objTomorrowProducts[i].Mobile = Convert.ToString(dtTommrow.Rows[i]["Mobile"]);
                            objTomorrowProducts[i].StreetAddress = Convert.ToString(dtTommrow.Rows[i]["StreetAddress"]);
                            objTomorrowProducts[i].Location = Convert.ToString(dtTommrow.Rows[i]["Location"]);
                            objTomorrowProducts[i].TotalQty = Convert.ToInt32(dtTommrow.Rows[i]["TotalQty"]);
                            objTomorrowProducts[i].PurchaseQty = Convert.ToInt32(dtTommrow.Rows[i]["PurchaseQty"]);

                            if (Convert.ToInt32(dtTommrow.Rows[i]["RemainingQty"]) < 0)
                                objTomorrowProducts[i].RemainingQty = 0;
                            else
                                objTomorrowProducts[i].RemainingQty = Convert.ToInt32(dtTommrow.Rows[i]["FinalRemainingQty"]);
                            objTomorrowProducts[i].Latitude = Convert.ToString(dtTommrow.Rows[i]["Latitude"]);
                            objTomorrowProducts[i].Longitude = Convert.ToString(dtTommrow.Rows[i]["Longitude"]);
                            objTomorrowProducts[i].IsFavourite = Convert.ToInt32(dtTommrow.Rows[i]["ISFavourite"]);
                            objTomorrowProducts[i].OriginalPrice = Convert.ToDouble(dtTommrow.Rows[i]["OriginalPrice"]).ToString("f2");
                            objTomorrowProducts[i].DiscountPrice = Convert.ToDouble(dtTommrow.Rows[i]["DiscountPrice"]).ToString("f2");
                            objTomorrowProducts[i].Description = Convert.ToString(dtTommrow.Rows[i]["Description"]);
                            if (Convert.ToString(dtTommrow.Rows[i]["Pickup_from"]) != string.Empty)
                            {
                                var fromTime = (TimeSpan)dtTommrow.Rows[i]["Pickup_from"];
                                objTomorrowProducts[i].PickupFromTime = DateTime.Today
                                                                    .Add(fromTime)
                                                                    .ToString("hh:mm tt");

                            }
                            else
                                objTomorrowProducts[i].PickupFromTime = string.Empty;


                            if (Convert.ToString(dtTommrow.Rows[i]["Pickup_To"]) != string.Empty)
                            {
                                var toTime = (TimeSpan)dtTommrow.Rows[i]["Pickup_To"];
                                objTomorrowProducts[i].PickupToTime = DateTime.Today
                                                                    .Add(toTime)
                                                                    .ToString("hh:mm tt");
                            }
                            else
                                objTomorrowProducts[i].PickupToTime = string.Empty;

                            if (Convert.ToString(dtTommrow.Rows[i]["GeoDistance"]) != string.Empty)
                            {
                                if (Convert.ToDecimal(dtTommrow.Rows[i]["GeoDistance"]) * 1000 > 1000)
                                    objTomorrowProducts[i].Distance = Convert.ToInt32(dtTommrow.Rows[i]["GeoDistance"]) + " KM";
                                else
                                    objTomorrowProducts[i].Distance = Convert.ToInt32(Convert.ToDecimal(dtTommrow.Rows[i]["GeoDistance"]) * 1000) + " M";
                            }
                            else
                                objTomorrowProducts[i].Distance = "0M";

                            if (Convert.ToString(dtTommrow.Rows[i]["StoreImages"]) != string.Empty)
                                objTomorrowProducts[i].StoreImages = Config.WebSiteUrl + Config.CMSFiles + Convert.ToString(dtTommrow.Rows[i]["StoreImages"]);
                            else
                                objTomorrowProducts[i].StoreImages = "";

                            if (Convert.ToString(dtTommrow.Rows[i]["ProfilePhoto"]) != string.Empty)
                                objTomorrowProducts[i].ProfilePhoto = Config.WebSiteUrl + Config.CMSFiles + Convert.ToString(dtTommrow.Rows[i]["ProfilePhoto"]);
                            else
                                objTomorrowProducts[i].ProfilePhoto = "";

                            objTomorrowProducts[i].Rating = "0";
                            objTomorrowProducts[i].DietryType = Convert.ToString(dtTommrow.Rows[i]["DietryType"]);
                            objTomorrowProducts[i].StoreName = Convert.ToString(dtTommrow.Rows[i]["StoreName"]);

                            objResponse.TomorrowProducts = objTomorrowProducts;
                        }
                    }
                    else
                    {
                        objResponse.TomorrowProducts = new Products[0];
                    }



                    string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                    strResponseName = strResponseName.Replace("\"Products\"", "\"data\"");
                    strResponseName = strResponseName.Replace("\"TomorrowProducts\"", "\"tomorrow\"");
                    HttpContext.Current.Response.Write(strResponseName);
                    //HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));

                }

            }
            catch (Exception Ex)
            {
                ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
            }
        }
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void ProductListWithPaging(long UserID, string SecretKey, string AuthToken, string CaregoryID, string Location, string Latitude, string Longitude, string Postcode, int CurrentPage, int PageSize)
    {

        // ================User Last Login Location Track=============================
        string strSuburb = string.Empty;
        string strSuburb1 = string.Empty;
        if (HttpContext.Current.Request["Suburb1"] != null)
        {
            strSuburb1 = Convert.ToString(HttpContext.Current.Request["Suburb1"]);
        }

        string strSuburb2 = string.Empty;
        if (HttpContext.Current.Request["Suburb2"] != null)
        {
            strSuburb2 = Convert.ToString(HttpContext.Current.Request["Suburb2"]);
        }
        string strSuburb3 = string.Empty;
        if (HttpContext.Current.Request["Suburb3"] != null)
        {
            strSuburb3 = Convert.ToString(HttpContext.Current.Request["Suburb3"]);
        }
        if (strSuburb1 != string.Empty && strSuburb2 != string.Empty && strSuburb3 != string.Empty)
        {
            strSuburb = strSuburb1.Trim() + "###" + strSuburb2.Trim() + "###" + strSuburb3.Trim();
        }

        if (Postcode != "")
        {
            strSuburb = strSuburb + "$$$" + Postcode;
        }

        // ================User Last Login Location Track=============================

        bool IsValidated = false;
        if (UserID == 0)
        {
            if (ValidateRequestBAL.UserValidateClientRequestWithoutLogin(Convert.ToString(SecretKey), Convert.ToString(AuthToken)))
            {
                IsValidated = true;
            }
        }
        else
        {
            //WriteLogFile("UserID : " + UserID.ToString() + " --> SecretKey: " + SecretKey + " --> AuthToken : " + AuthToken + " --> strSuburb:" + strSuburb);
            if (ValidateRequestBAL.UserValidateClientRequest(Convert.ToInt64(UserID), Convert.ToString(SecretKey), Convert.ToString(AuthToken), strSuburb))
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

            string strResponse = string.Empty;
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            try
            {
                ProductResponse objResponse = new ProductResponse();
                DataTable dt = new DataTable();


                string strCategory = string.Empty;
                string strSearch1 = string.Empty;
                string strSearch2 = string.Empty;
                string strSearch3 = string.Empty;
                int postcode = 0;

                if (Location != string.Empty)
                {
                    string strLoc = Convert.ToString(Location);
                    if (strLoc.IndexOf(",") >= 0)
                    {
                        string[] strLocSplit = strLoc.Split(',');
                        for (int i = 1; i <= strLocSplit.Length; i++)
                        {
                            if (i == 1)
                            {
                                strSearch1 = Convert.ToString(strLocSplit[0]).Trim();
                            }
                            if (i == 2)
                            {
                                strSearch2 = Convert.ToString(strLocSplit[1]).Trim();
                            }
                            if (i == 3)
                            {
                                strSearch3 = Convert.ToString(strLocSplit[2]).Trim();
                            }
                        }
                    }
                    else
                    {
                        strSearch1 = strLoc;
                    }

                }
                try
                {

                    if (strSearch1 != string.Empty)
                    {
                        if (Convert.ToInt32(strSearch1) % 1 == 0)
                        {
                            postcode = Convert.ToInt32(strSearch1);
                        }
                    }
                }
                catch (Exception ex) { }


                try
                {
                    if (strSearch2 != string.Empty)
                    {
                        if (Convert.ToInt32(strSearch2) % 1 == 0)
                        {
                            postcode = Convert.ToInt32(strSearch2);
                        }
                    }
                }
                catch (Exception ex) { }


                try
                {
                    if (strSearch3 != string.Empty)
                    {
                        if (Convert.ToInt32(strSearch3) % 1 == 0)
                        {
                            postcode = Convert.ToInt32(strSearch3);
                        }
                    }
                }
                catch (Exception ex) { }

                if (Postcode != string.Empty)
                {
                    postcode = Convert.ToInt32(Postcode);
                }
                int TotalRecord = 0;
                BusinessBAL objBusinessBAL = new BusinessBAL();
                //int CurrentPage = 1;
                // dt = objBusinessBAL.BusinessProductList(UserID, CaregoryID, strSearch1, strSearch2, strSearch3, postcode, ref CurrentPage, 1000, out TotalRecord, Latitude, Longitude);

                string RestaurantType = "";
                int ActiveDeals = 0;
                int PickupNow = 0;
                string PickupFromTime = "";
                string PickupToTime = "";
                if (HttpContext.Current.Request["ActiveDeals"] != null)
                {
                    ActiveDeals = Convert.ToInt32(HttpContext.Current.Request["ActiveDeals"]);
                }
                if (HttpContext.Current.Request["PickupNow"] != null)
                {
                    PickupNow = Convert.ToInt32(HttpContext.Current.Request["PickupNow"]);
                }
                if (HttpContext.Current.Request["RestaurantTypes"] != null)
                {
                    RestaurantType = Convert.ToString(HttpContext.Current.Request["RestaurantTypes"]);
                }
                if (HttpContext.Current.Request["PickupFromTime"] != null)
                {
                    PickupFromTime = Convert.ToString(HttpContext.Current.Request["PickupFromTime"]);
                }
                if (HttpContext.Current.Request["PickupToTime"] != null)
                {
                    PickupToTime = Convert.ToString(HttpContext.Current.Request["PickupToTime"]);
                }

                int Version = 0;
                if (HttpContext.Current.Request["Version"] != null)
                {
                    Version = 1;
                }



                //string LogResponse = "API: ProductListWithPaging, UserID: " + Convert.ToString(UserID) + " SecretKey: " + SecretKey
                //    + " AuthToken:" + AuthToken + " CaregoryID: " + Convert.ToString(CaregoryID) + " Location: "
                //    + Location + " Latitude:" + Convert.ToString(Latitude) + " Longitude:" + Convert.ToString(Longitude)
                //    + " Postcode: " + Convert.ToString(Postcode) + " CurrentPage:" + Convert.ToString(CurrentPage)
                //    + " PageSize: " + Convert.ToString(PageSize)
                //    + " ActiveDeals: " + Convert.ToString(ActiveDeals)
                //    + " PickupNow: " + Convert.ToString(PickupNow)
                //    + " RestaurantType: " + Convert.ToString(RestaurantType)
                //    + " PickupFromTime: " + Convert.ToString(PickupFromTime)
                //    + " PickupToTime: " + Convert.ToString(PickupToTime);


                //WriteLogFile("Request: " + LogResponse);


                string StateCode = "VIC";
                if (HttpContext.Current.Request["StateCode"] != null)
                {
                    StateCode = Convert.ToString(HttpContext.Current.Request["StateCode"]).ToUpper();

                }

                decimal Distance = 0;

                dt = BusinessProductListDAL(UserID, CaregoryID, strSearch1, strSearch2, strSearch3, postcode, ref CurrentPage, PageSize, out TotalRecord, Latitude, Longitude, RestaurantType, ActiveDeals, PickupNow, Version, PickupFromTime, PickupToTime, StateCode, Distance, "", 0, 0, 0);
                DataView dvProducts = new DataView(dt);
                dvProducts.Sort = "GeoDistance ASC, Name ASC";
                dt = dvProducts.ToTable();

                if (dt.Rows.Count > 0)
                {
                    objResponse.success = "true";
                    objResponse.message = dt.Rows.Count + " records found.";
                    objResponse.TotalRecords = TotalRecord;

                    Products[] objProducts = new Products[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        objProducts[i] = new Products();
                        objProducts[i].ID = Convert.ToInt64(dt.Rows[i]["ID"]);
                        objProducts[i].BusinessName = Convert.ToString(dt.Rows[i]["Name"]);
                        objProducts[i].FullName = Convert.ToString(dt.Rows[i]["FullName"]);
                        objProducts[i].EmailAddress = Convert.ToString(dt.Rows[i]["EmailAddress"]);
                        objProducts[i].BusinessPhone = Convert.ToString(dt.Rows[i]["BusinessPhone"]);
                        objProducts[i].Mobile = Convert.ToString(dt.Rows[i]["Mobile"]);
                        if (Convert.ToString(dt.Rows[i]["ImageName"]) != string.Empty)
                            objProducts[i].ImageFile = Config.WebSiteUrl + Config.CMSFiles + Convert.ToString(dt.Rows[i]["ImageName"]);
                        else
                            objProducts[i].ImageFile = string.Empty;
                        objProducts[i].StreetAddress = Convert.ToString(dt.Rows[i]["StreetAddress"]);
                        objProducts[i].Location = Convert.ToString(dt.Rows[i]["Location"]);
                        objProducts[i].TotalQty = Convert.ToInt32(dt.Rows[i]["TotalQty"]);
                        objProducts[i].PurchaseQty = Convert.ToInt32(dt.Rows[i]["PurchaseQty"]);

                        if (Convert.ToInt32(dt.Rows[i]["RemainingQty"]) < 0)
                            objProducts[i].RemainingQty = 0;
                        else
                            objProducts[i].RemainingQty = Convert.ToInt32(dt.Rows[i]["FinalRemainingQty"]);


                        objProducts[i].Latitude = Convert.ToString(dt.Rows[i]["Latitude"]);
                        objProducts[i].Longitude = Convert.ToString(dt.Rows[i]["Longitude"]);

                        //if (objProducts[i].RemainingQty > 10)
                        //{
                        //    objProducts[i].ServingLeft = GetServingLeft(objProducts[i].RemainingQty);
                        //}
                        //else
                        //{
                        //    objProducts[i].ServingLeft = objProducts[i].RemainingQty.ToString();
                        //}

                        objProducts[i].IsFavourite = Convert.ToInt32(dt.Rows[i]["ISFavourite"]);
                        objProducts[i].OriginalPrice = Convert.ToDouble(dt.Rows[i]["OriginalPrice"]).ToString("f2");
                        objProducts[i].DiscountPrice = Convert.ToDouble(dt.Rows[i]["DiscountPrice"]).ToString("f2");
                        // objProducts[i].FoodItems = Convert.ToString(dt.Rows[i]["FoodItems"]);
                        objProducts[i].Description = Convert.ToString(dt.Rows[i]["Description"]);
                        if (Convert.ToString(dt.Rows[i]["PickupFromTime"]) != string.Empty)
                            objProducts[i].PickupFromTime = Convert.ToDateTime(dt.Rows[i]["PickupFromTime"]).ToString("hh:mm tt");
                        else
                            objProducts[i].PickupFromTime = string.Empty;


                        if (Convert.ToString(dt.Rows[i]["PickupToTime"]) != string.Empty)
                            objProducts[i].PickupToTime = Convert.ToDateTime(dt.Rows[i]["PickupToTime"]).ToString("hh:mm tt");
                        else
                            objProducts[i].PickupToTime = string.Empty;

                        if (Convert.ToString(dt.Rows[i]["GeoDistance"]) != string.Empty)
                        {
                            if (Convert.ToDecimal(dt.Rows[i]["GeoDistance"]) * 1000 > 1000)
                                objProducts[i].Distance = Convert.ToInt32(dt.Rows[i]["GeoDistance"]) + " KM";
                            else
                                objProducts[i].Distance = Convert.ToInt32(Convert.ToDecimal(dt.Rows[i]["GeoDistance"]) * 1000) + " M";
                        }
                        else
                            objProducts[i].Distance = "0M";
                        //3 Feb , 2018
                        //if (Convert.ToString(dt.Rows[i]["Discount"]) != string.Empty)
                        //{
                        //    objProducts[i].Discount = Convert.ToDouble(dt.Rows[i]["Discount"]).ToString("f2").Replace(".00", "") + "% OFF";
                        //}
                        //else
                        //{
                        //    objProducts[i].Discount = "0% OFF";
                        //}

                        //if (Convert.ToString(dt.Rows[i]["PickUpTimes"]).IndexOf(',') >= 0)
                        //    objProducts[i].PickupTimes = Convert.ToString(dt.Rows[i]["PickUpTimes"]);
                        //else
                        //    objProducts[i].PickupTimes = Convert.ToDateTime(dt.Rows[i]["PickupFromTime"]).ToString("h:mmtt") + " - " + Convert.ToDateTime(dt.Rows[i]["PickupToTime"]).ToString("h:mmtt");
                        objResponse.Products = objProducts;
                    }
                    string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                    strResponseName = strResponseName.Replace("\"Products\"", "\"data\"");
                    // WriteLogFile("Response: " + strResponseName);
                    HttpContext.Current.Response.Write(strResponseName);
                    //HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));

                }
                else
                {
                    NoRecordExists();
                }
            }
            catch (Exception Ex)
            {
                ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
            }
        }
        HttpContext.Current.Response.End();
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void ProductDetail(long UserID, string SecretKey, string AuthToken, long ProductID)
    {
        bool IsValidated = false;
        // ================User Last Login Location Track=============================
        string strSuburb = string.Empty;
        if (HttpContext.Current.Request["Suburb"] != null)
        {
            strSuburb = Convert.ToString(HttpContext.Current.Request["Suburb"]);
        }
        // ================User Last Login Location Track=============================

        if (UserID == 0)
        {
            if (ValidateRequestBAL.UserValidateClientRequestWithoutLogin(Convert.ToString(SecretKey), Convert.ToString(AuthToken)))
            {
                IsValidated = true;
            }
        }
        else
        {
            if (ValidateRequestBAL.UserValidateClientRequest(Convert.ToInt64(UserID), Convert.ToString(SecretKey), Convert.ToString(AuthToken), strSuburb))
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
            string strResponse = string.Empty;
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            try
            {
                string strLatitude = "";
                string strLongitude = "";
                if (HttpContext.Current.Request["Latitude"] != null)
                {
                    strLatitude = Convert.ToString(HttpContext.Current.Request["Latitude"]);
                }
                if (HttpContext.Current.Request["Longitude"] != null)
                {
                    strLongitude = Convert.ToString(HttpContext.Current.Request["Longitude"]);
                }
                string StateCode = "VIC";
                if (HttpContext.Current.Request["StateCode"] != null)
                {
                    StateCode = Convert.ToString(HttpContext.Current.Request["StateCode"]);
                }

                Response objResponse = new Response();
                DataTable dt = new DataTable();
                BusinessBAL objBusinessBAL = new BusinessBAL();
                objBusinessBAL.ID = ProductID;
                //dt = objBusinessBAL.BusinessDetailsByID(Convert.ToInt64(UserID));
                dt = BusinessDetailsByIDDAL(Convert.ToInt64(UserID), ProductID, strLatitude, strLongitude, StateCode);


                if (dt.Rows.Count > 0)
                {
                    objResponse.success = "true";
                    objResponse.message = dt.Rows.Count + " records found.";

                    Products[] objProducts = new Products[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        objProducts[i] = new Products();
                        objProducts[i].ID = Convert.ToInt64(dt.Rows[i]["PriceID"]);
                        objProducts[i].BusinessName = Convert.ToString(dt.Rows[i]["Name"]);
                        objProducts[i].FullName = Convert.ToString(dt.Rows[i]["FullName"]);
                        objProducts[i].EmailAddress = Convert.ToString(dt.Rows[i]["EmailAddress"]);
                        objProducts[i].BusinessPhone = Convert.ToString(dt.Rows[i]["BusinessPhone"]);
                        objProducts[i].Mobile = Convert.ToString(dt.Rows[i]["Mobile"]);
                        if (Convert.ToString(dt.Rows[i]["StoreImages"]) != string.Empty)
                            objProducts[i].StoreImages = Config.WebSiteUrl + Config.CMSFiles + Convert.ToString(dt.Rows[i]["StoreImages"]);
                        else
                            objProducts[i].StoreImages = string.Empty;

                        if (Convert.ToString(dt.Rows[i]["ProfilePhoto"]) != string.Empty)
                            objProducts[i].ProfilePhoto = Config.WebSiteUrl + Config.CMSFiles + Convert.ToString(dt.Rows[i]["ProfilePhoto"]);
                        else
                            objProducts[i].ProfilePhoto = string.Empty;


                        objProducts[i].StreetAddress = Convert.ToString(dt.Rows[i]["StreetAddress"]);
                        objProducts[i].Location = Convert.ToString(dt.Rows[i]["Location"]);
                        objProducts[i].TotalQty = Convert.ToInt32(dt.Rows[i]["TotalQty"]);
                        objProducts[i].PurchaseQty = Convert.ToInt32(dt.Rows[i]["PurchaseQty"]);
                        if (Convert.ToInt32(dt.Rows[i]["RemainingQty"]) < 0)
                            objProducts[i].RemainingQty = 0;
                        else
                            objProducts[i].RemainingQty = Convert.ToInt32(dt.Rows[i]["RemainingQty"]);

                        objProducts[i].OriginalPrice = Convert.ToDouble(dt.Rows[i]["OriginalPrice"]).ToString("f2");
                        objProducts[i].DiscountPrice = Convert.ToDouble(dt.Rows[i]["DiscountPrice"]).ToString("f2");
                        //objProducts[i].FoodItems = Convert.ToString(dt.Rows[i]["FoodItems"]);
                        objProducts[i].Description = Convert.ToString(dt.Rows[i]["Description"]);
                        objProducts[i].Note = Convert.ToString(dt.Rows[i]["Note"]);
                        objProducts[i].IsFavourite = Convert.ToInt32(dt.Rows[i]["IsFavourite"]);


                        if (Convert.ToString(dt.Rows[i]["Pickup_from"]) != string.Empty)
                        {
                            var fromTime = (TimeSpan)dt.Rows[i]["Pickup_from"];
                            objProducts[i].PickupFromTime = DateTime.Today
                                                                .Add(fromTime)
                                                                .ToString("hh:mm tt");
                        }
                        else
                            objProducts[i].PickupFromTime = string.Empty;

                        if (Convert.ToString(dt.Rows[i]["Pickup_To"]) != string.Empty)
                        {
                            var toTime = (TimeSpan)dt.Rows[i]["Pickup_To"];
                            objProducts[i].PickupToTime = DateTime.Today
                                                                .Add(toTime)
                                                                .ToString("hh:mm tt");
                        }
                        else
                            objProducts[i].PickupToTime = string.Empty;



                        //if (Convert.ToString(dt.Rows[i]["PickupFromTime"]) != string.Empty)
                        //    objProducts[i].PickupFromTime = Convert.ToDateTime(dt.Rows[i]["PickupFromTime"]).ToString("hh:mm tt");
                        //else
                        //    objProducts[i].PickupFromTime = string.Empty;


                        //if (Convert.ToString(dt.Rows[i]["PickupToTime"]) != string.Empty)
                        //    objProducts[i].PickupToTime = Convert.ToDateTime(dt.Rows[i]["PickupToTime"]).ToString("hh:mm tt");
                        //else
                        //    objProducts[i].PickupToTime = string.Empty;

                        objProducts[i].Latitude = Convert.ToString(dt.Rows[i]["Latitude"]);
                        objProducts[i].Longitude = Convert.ToString(dt.Rows[i]["Longitude"]);
                        objProducts[i].RestaurantTypes = Convert.ToString(dt.Rows[i]["RestaurantTypes"]);
                        objProducts[i].RestaurantTypesValues = Convert.ToString(dt.Rows[i]["RestaurantTypesValues"]);
                        objProducts[i].StoreName = Convert.ToString(dt.Rows[i]["StoreName"]);

                        //objProducts[i].Distance = string.Empty;
                        if (Convert.ToString(dt.Rows[i]["GeoDistance"]) != string.Empty)
                        {
                            if (Convert.ToDecimal(dt.Rows[i]["GeoDistance"]) * 1000 > 1000)
                                objProducts[i].Distance = Convert.ToInt32(dt.Rows[i]["GeoDistance"]) + " KM";
                            else
                                objProducts[i].Distance = Convert.ToInt32(Convert.ToDecimal(dt.Rows[i]["GeoDistance"]) * 1000) + " M";
                        }
                        else
                            objProducts[i].Distance = "0M";
                        ////3 Feb , 2018
                        //if (Convert.ToString(dt.Rows[i]["Discount"]) != string.Empty)
                        //{
                        //    objProducts[i].Discount = Convert.ToDouble(dt.Rows[i]["Discount"]).ToString("f2").Replace(".00", "") + "% OFF";
                        //}
                        //else
                        //{
                        //    objProducts[i].Discount = "0% OFF";
                        //}
                        //objProducts[i].PickupTimes = Convert.ToString(dt.Rows[i]["PickUpTimes"]);
                        objResponse.Products = objProducts;
                    }
                    string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                    strResponseName = strResponseName.Replace("\"Products\"", "\"data\"");
                    HttpContext.Current.Response.Write(strResponseName);
                    //HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));

                }
                else
                {
                    NoRecordExists();
                }
            }
            catch (Exception Ex)
            {
                ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
            }
        }
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void OrderSummaryByID(long UserID, string SecretKey, string AuthToken, long OrderID)
    {
        bool IsValidated = false;
        // ================User Last Login Location Track=============================
        string strSuburb = string.Empty;
        if (HttpContext.Current.Request["Suburb"] != null)
        {
            strSuburb = Convert.ToString(HttpContext.Current.Request["Suburb"]);
        }
        // ================User Last Login Location Track=============================

        if (ValidateRequestBAL.UserValidateClientRequest(Convert.ToInt64(UserID), Convert.ToString(SecretKey), Convert.ToString(AuthToken), strSuburb))
        {
            IsValidated = true;
        }

        if (!IsValidated)
        {
            CommonAPI objCommonAPI = new CommonAPI();
            objCommonAPI.Unauthorized();
        }
        else
        {
            string strResponse = string.Empty;
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            try
            {
                Response objResponse = new Response();
                DataTable dt = new DataTable();
                CartBAL objCartBAL = new CartBAL();

                dt = objCartBAL.OrderSummary(OrderID);

                if (dt.Rows.Count > 0)
                {
                    objResponse.success = "true";
                    objResponse.message = dt.Rows.Count + " records found.";

                    OrderSummary[] objOrderSummary = new OrderSummary[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        objOrderSummary[i] = new OrderSummary();
                        objOrderSummary[i].OrderNo = Convert.ToString(dt.Rows[i]["OrderID"]);

                        objOrderSummary[i].OrderID = Convert.ToInt64(dt.Rows[i]["OrderNo"]);
                        objOrderSummary[i].BusinessID = Convert.ToInt64(dt.Rows[i]["BusinessID"]);
                        objOrderSummary[i].BusinessName = Convert.ToString(dt.Rows[i]["BusinessName"]);
                        objOrderSummary[i].BusinessPhone = Convert.ToString(dt.Rows[i]["BusinessPhone"]);
                        objOrderSummary[i].Mobile = Convert.ToString(dt.Rows[i]["Mobile"]);
                        objOrderSummary[i].StreetAddress = Convert.ToString(dt.Rows[i]["StreetAddress"]);
                        objOrderSummary[i].Location = Convert.ToString(dt.Rows[i]["Location"]);
                        objOrderSummary[i].UserID = Convert.ToInt64(dt.Rows[i]["UserID"]);
                        objOrderSummary[i].PickupDate = Convert.ToDateTime(dt.Rows[i]["PickupDate"]).ToString("dd/MM/yyyy");
                        if (Convert.ToString(dt.Rows[i]["PickupFromTime"]) != string.Empty)
                            objOrderSummary[i].PickupFromTime = Convert.ToDateTime(dt.Rows[i]["PickupFromTime"]).ToString("hh:mm tt");
                        else
                            objOrderSummary[i].PickupFromTime = string.Empty;


                        if (Convert.ToString(dt.Rows[i]["PickupToTime"]) != string.Empty)
                            objOrderSummary[i].PickupToTime = Convert.ToDateTime(dt.Rows[i]["PickupToTime"]).ToString("hh:mm tt");
                        else
                            objOrderSummary[i].PickupToTime = string.Empty;

                        objOrderSummary[i].Qty = Convert.ToString(dt.Rows[i]["Qty"]);
                        objOrderSummary[i].OriginalPrice = Convert.ToDouble(dt.Rows[i]["OriginalPrice"]).ToString("f2");
                        objOrderSummary[i].Donation = Convert.ToDouble(dt.Rows[i]["Donation"]).ToString("f2");
                        objOrderSummary[i].TransactionFee = Convert.ToDouble(dt.Rows[i]["TransactionFee"]).ToString("f2");
                        objOrderSummary[i].BiteloopFee = Convert.ToDouble(dt.Rows[i]["BringMeHomeFee"]).ToString("f2");
                        objOrderSummary[i].GrandTotal = Convert.ToDouble(dt.Rows[i]["GrandTotal"]).ToString("f2");
                        objOrderSummary[i].ItemPrice = Convert.ToDouble(dt.Rows[i]["ItemPrice"]).ToString("f2");
                        objOrderSummary[i].OrderStatusText = Convert.ToString(dt.Rows[i]["OrderStatusText"]);

                        objOrderSummary[i].PromocodeDiscountAmount = "0.00";

                        objOrderSummary[i].RedeemPoints = "0.00";

                        objOrderSummary[i].Latitude = Convert.ToString(dt.Rows[i]["Latitude"]);

                        objOrderSummary[i].Longitude = Convert.ToString(dt.Rows[i]["Longitude"]);

                        objOrderSummary[i].Note = Convert.ToString(dt.Rows[i]["Note"]);

                        if (Convert.ToBoolean(dt.Rows[i]["IsCollected"]) == true)
                            objOrderSummary[i].IsCollected = 1;
                        else
                            objOrderSummary[i].IsCollected = 0;

                        objResponse.OrderSummary = objOrderSummary;
                    }
                    string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                    strResponseName = strResponseName.Replace("\"OrderSummary\"", "\"data\"");

                    HttpContext.Current.Response.Write(strResponseName);
                    //HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));

                }
                else
                {
                    NoRecordExists();
                }
            }
            catch (Exception Ex)
            {
                ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
            }
        }
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void UserOrders(long UserID, string SecretKey, string AuthToken)
    {
        bool IsValidated = false;
        // ================User Last Login Location Track=============================
        string strSuburb = string.Empty;
        if (HttpContext.Current.Request["Suburb"] != null)
        {
            strSuburb = Convert.ToString(HttpContext.Current.Request["Suburb"]);
        }
        // ================User Last Login Location Track=============================

        if (ValidateRequestBAL.UserValidateClientRequest(Convert.ToInt64(UserID), Convert.ToString(SecretKey), Convert.ToString(AuthToken), strSuburb))
        {
            IsValidated = true;
        }

        if (!IsValidated)
        {
            CommonAPI objCommonAPI = new CommonAPI();
            objCommonAPI.Unauthorized();
        }
        else
        {
            string strResponse = string.Empty;
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            try
            {
                Response objResponse = new Response();
                DataTable dt = new DataTable();
                CartBAL objCartBAL = new CartBAL();
                objCartBAL.UserID = UserID;
                dt = objCartBAL.UserOrders();

                if (dt.Rows.Count > 0)
                {
                    objResponse.success = "true";
                    objResponse.message = dt.Rows.Count + " records found.";

                    OrderSummary[] objOrderSummary = new OrderSummary[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        objOrderSummary[i] = new OrderSummary();
                        objOrderSummary[i].OrderNo = Convert.ToString(dt.Rows[i]["OrderID"]);

                        objOrderSummary[i].OrderID = Convert.ToInt64(dt.Rows[i]["OrderNo"]);
                        objOrderSummary[i].BusinessID = Convert.ToInt64(dt.Rows[i]["BusinessID"]);
                        objOrderSummary[i].BusinessName = Convert.ToString(dt.Rows[i]["BusinessName"]);
                        objOrderSummary[i].BusinessPhone = Convert.ToString(dt.Rows[i]["BusinessPhone"]);
                        objOrderSummary[i].Mobile = Convert.ToString(dt.Rows[i]["Mobile"]);
                        objOrderSummary[i].StreetAddress = Convert.ToString(dt.Rows[i]["StreetAddress"]);
                        objOrderSummary[i].Location = Convert.ToString(dt.Rows[i]["Location"]);

                        objOrderSummary[i].Latitude = Convert.ToString(dt.Rows[i]["Latitude"]);
                        objOrderSummary[i].Longitude = Convert.ToString(dt.Rows[i]["Longitude"]);

                        objOrderSummary[i].UserID = Convert.ToInt64(dt.Rows[i]["UserID"]);
                        objOrderSummary[i].PickupDate = Convert.ToDateTime(dt.Rows[i]["PickupDate"]).ToString("dd/MM/yyyy");
                        if (Convert.ToString(dt.Rows[i]["PickupFromTime"]) != string.Empty)
                            objOrderSummary[i].PickupFromTime = Convert.ToDateTime(dt.Rows[i]["PickupFromTime"]).ToString("hh:mm tt");
                        else
                            objOrderSummary[i].PickupFromTime = string.Empty;


                        if (Convert.ToString(dt.Rows[i]["PickupToTime"]) != string.Empty)
                            objOrderSummary[i].PickupToTime = Convert.ToDateTime(dt.Rows[i]["PickupToTime"]).ToString("hh:mm tt");
                        else
                            objOrderSummary[i].PickupToTime = string.Empty;

                        objOrderSummary[i].Qty = Convert.ToString(dt.Rows[i]["Qty"]);
                        objOrderSummary[i].OriginalPrice = Convert.ToDouble(dt.Rows[i]["OriginalPrice"]).ToString("f2");
                        objOrderSummary[i].Donation = Convert.ToDouble(dt.Rows[i]["Donation"]).ToString("f2");
                        objOrderSummary[i].TransactionFee = Convert.ToDouble(dt.Rows[i]["TransactionFee"]).ToString("f2");
                        objOrderSummary[i].BiteloopFee = Convert.ToDouble(dt.Rows[i]["BringMeHomeFee"]).ToString("f2");
                        objOrderSummary[i].GrandTotal = Convert.ToDouble(dt.Rows[i]["GrandTotal"]).ToString("f2");
                        objOrderSummary[i].ItemPrice = Convert.ToDouble(dt.Rows[i]["ItemPrice"]).ToString("f2");

                        objOrderSummary[i].IsCollected = Convert.ToInt32(dt.Rows[i]["OrderStatusDetails"]);
                        //if (Convert.ToBoolean(dt.Rows[i]["IsCollected"]) == true)
                        //    objOrderSummary[i].IsCollected = 1;
                        //else
                        //    objOrderSummary[i].IsCollected = 0;

                        objOrderSummary[i].RewardsPoints = Convert.ToString(dt.Rows[i]["RewardsAmount"]).Replace(".00", "");

                        // objOrderSummary[i].PromocodeDiscountAmount = Convert.ToString(dt.Rows[i]["PromocodeDiscountAmount"]);
                        objOrderSummary[i].StoreName = Convert.ToString(dt.Rows[i]["StoreName"]);
                        objOrderSummary[i].PickupCode = Convert.ToString(dt.Rows[i]["PickupCode"]);
                        objOrderSummary[i].OrderStatusID = Convert.ToInt32(dt.Rows[i]["OrderStatusID"]);
                        objOrderSummary[i].OrderStatusText = Convert.ToString(dt.Rows[i]["OrderStatusText"]);
                        objOrderSummary[i].Note = Convert.ToString(dt.Rows[i]["Note"]);
                        objResponse.OrderSummary = objOrderSummary;
                    }
                    string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                    strResponseName = strResponseName.Replace("\"OrderSummary\"", "\"data\"");
                    HttpContext.Current.Response.Write(strResponseName);
                    //HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));

                }
                else
                {
                    NoRecordExists();
                }
            }
            catch (Exception Ex)
            {
                ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
            }
        }
        HttpContext.Current.Response.End();
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void ConfirmCollected(long UserID, string SecretKey, string AuthToken, long OrderID)
    {
        bool IsValidated = false;
        // ================User Last Login Location Track=============================
        string strSuburb = string.Empty;
        if (HttpContext.Current.Request["Suburb"] != null)
        {
            strSuburb = Convert.ToString(HttpContext.Current.Request["Suburb"]);
        }
        // ================User Last Login Location Track=============================

        if (ValidateRequestBAL.UserValidateClientRequest(Convert.ToInt64(UserID), Convert.ToString(SecretKey), Convert.ToString(AuthToken), strSuburb))
        {
            IsValidated = true;
        }

        if (!IsValidated)
        {
            CommonAPI objCommonAPI = new CommonAPI();
            objCommonAPI.Unauthorized();
        }
        else
        {
            string strResponse = string.Empty;
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            try
            {
                Response objResponse = new Response();
                CartBAL objCartBAL = new CartBAL();
                long result = objCartBAL.ConfirmCollected(OrderID);

                switch (result)
                {
                    default:
                        objResponse.success = "true";
                        objResponse.message = "Thank you for order collection confirmation.";
                        DataTable dt = new DataTable();
                        dt = objCartBAL.OrderUsersDeviceKey(OrderID);
                        if (dt.Rows.Count > 0)
                        {
                            SendOrderPickupNotifications(dt);
                        }
                        break;
                }

                HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));

            }
            catch (Exception Ex)
            {
                ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
            }
        }
        HttpContext.Current.Response.End();
    }



    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void UserFavouriteBusinessList(long UserID, string SecretKey, string AuthToken)
    {
        bool IsValidated = false;
        // ================User Last Login Location Track=============================
        string strSuburb = string.Empty;
        if (HttpContext.Current.Request["Suburb"] != null)
        {
            strSuburb = Convert.ToString(HttpContext.Current.Request["Suburb"]);
        }
        // ================User Last Login Location Track=============================

        if (ValidateRequestBAL.UserValidateClientRequest(Convert.ToInt64(UserID), Convert.ToString(SecretKey), Convert.ToString(AuthToken), strSuburb))
        {
            IsValidated = true;
        }

        if (!IsValidated)
        {
            CommonAPI objCommonAPI = new CommonAPI();
            objCommonAPI.Unauthorized();
        }
        else
        {
            string strResponse = string.Empty;
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            try
            {
                Response objResponse = new Response();
                DataTable dt = new DataTable();

                UsersBAL objUsersBAL = new UsersBAL();
                objUsersBAL.ID = Convert.ToInt64(UserID);

                //dt = objUsersBAL.UserFavouriteBusinessList();

                string StateCode = "VIC";
                if (HttpContext.Current.Request["StateCode"] != null)
                {
                    StateCode = Convert.ToString(HttpContext.Current.Request["StateCode"]).ToUpper();
                }

                dt = UserFavouriteBusinessListDAL(UserID, "", "", StateCode);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        objResponse.success = "true";
                        objResponse.message = dt.Rows.Count + " records found.";

                        Products[] objProducts = new Products[dt.Rows.Count];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            objProducts[i] = new Products();
                            objProducts[i].ID = Convert.ToInt64(dt.Rows[i]["ID"]);
                            objProducts[i].BusinessName = Convert.ToString(dt.Rows[i]["Name"]);
                            objProducts[i].FullName = Convert.ToString(dt.Rows[i]["FullName"]);
                            objProducts[i].EmailAddress = Convert.ToString(dt.Rows[i]["EmailAddress"]);
                            objProducts[i].BusinessPhone = Convert.ToString(dt.Rows[i]["BusinessPhone"]);
                            objProducts[i].Mobile = Convert.ToString(dt.Rows[i]["Mobile"]);
                            //if (Convert.ToString(dt.Rows[i]["ImageName"]) != string.Empty)
                            //    objProducts[i].ImageFile = Config.WebSiteUrl + Config.CMSFiles + Convert.ToString(dt.Rows[i]["ImageName"]);
                            //else
                            //    objProducts[i].ImageFile = string.Empty;


                            if (Convert.ToString(dt.Rows[i]["StoreImages"]) != string.Empty)
                                objProducts[i].StoreImages = Config.WebSiteUrl + Config.CMSFiles + Convert.ToString(dt.Rows[i]["StoreImages"]);
                            else
                                objProducts[i].StoreImages = "";

                            if (Convert.ToString(dt.Rows[i]["ProfilePhoto"]) != string.Empty)
                                objProducts[i].ProfilePhoto = Config.WebSiteUrl + Config.CMSFiles + Convert.ToString(dt.Rows[i]["ProfilePhoto"]);
                            else
                                objProducts[i].ProfilePhoto = "";


                            objProducts[i].StreetAddress = Convert.ToString(dt.Rows[i]["StreetAddress"]);
                            objProducts[i].Location = Convert.ToString(dt.Rows[i]["Location"]);
                            objProducts[i].TotalQty = Convert.ToInt32(dt.Rows[i]["TotalQty"]);
                            objProducts[i].PurchaseQty = Convert.ToInt32(dt.Rows[i]["PurchaseQty"]);
                            if (Convert.ToInt32(dt.Rows[i]["RemainingQty"]) < 0)
                                objProducts[i].RemainingQty = 0;
                            else
                                objProducts[i].RemainingQty = Convert.ToInt32(dt.Rows[i]["RemainingQty"]);

                            objProducts[i].OriginalPrice = Convert.ToDouble(dt.Rows[i]["OriginalPrice"]).ToString("f2");
                            objProducts[i].DiscountPrice = Convert.ToDouble(dt.Rows[i]["DiscountPrice"]).ToString("f2");
                            // objProducts[i].FoodItems = Convert.ToString(dt.Rows[i]["FoodItems"]);
                            //objProducts[i].Description = Convert.ToString(dt.Rows[i]["Description"]);
                            //if (Convert.ToString(dt.Rows[i]["PickupFromTime"]) != string.Empty)
                            //    objProducts[i].PickupFromTime = Convert.ToDateTime(dt.Rows[i]["PickupFromTime"]).ToString("hh:mm tt");
                            //else
                            //    objProducts[i].PickupFromTime = string.Empty;


                            //if (Convert.ToString(dt.Rows[i]["PickupToTime"]) != string.Empty)
                            //    objProducts[i].PickupToTime = Convert.ToDateTime(dt.Rows[i]["PickupToTime"]).ToString("hh:mm tt");
                            //else
                            //    objProducts[i].PickupToTime = string.Empty;

                            if (Convert.ToString(dt.Rows[i]["Pickup_from"]) != string.Empty)
                            {
                                var fromTime = (TimeSpan)dt.Rows[i]["Pickup_from"];
                                objProducts[i].PickupFromTime = DateTime.Today
                                                                    .Add(fromTime)
                                                                    .ToString("hh:mm tt");
                            }
                            else
                                objProducts[i].PickupFromTime = string.Empty;

                            if (Convert.ToString(dt.Rows[i]["Pickup_To"]) != string.Empty)
                            {
                                var toTime = (TimeSpan)dt.Rows[i]["Pickup_To"];
                                objProducts[i].PickupToTime = DateTime.Today
                                                                    .Add(toTime)
                                                                    .ToString("hh:mm tt");
                            }
                            else
                                objProducts[i].PickupToTime = string.Empty;

                            //objProducts[i].Distance = string.Empty;

                            if (Convert.ToString(dt.Rows[i]["GeoDistance"]) != string.Empty)
                            {
                                if (Convert.ToDecimal(dt.Rows[i]["GeoDistance"]) * 1000 > 1000)
                                    objProducts[i].Distance = Convert.ToInt32(dt.Rows[i]["GeoDistance"]) + " KM";
                                else
                                    objProducts[i].Distance = Convert.ToInt32(Convert.ToDecimal(dt.Rows[i]["GeoDistance"]) * 1000) + " M";
                            }
                            else
                                objProducts[i].Distance = "0M";

                            objProducts[i].StoreName = Convert.ToString(dt.Rows[i]["StoreName"]);

                            ////3 Feb , 2018
                            //if (Convert.ToString(dt.Rows[i]["Discount"]) != string.Empty)
                            //{
                            //    objProducts[i].Discount = Convert.ToDouble(dt.Rows[i]["Discount"]).ToString("f2").Replace(".00", "") + "% OFF";
                            //}
                            //else
                            //{
                            //    objProducts[i].Discount = "0% OFF";
                            //}
                            //objProducts[i].PickupTimes = Convert.ToString(dt.Rows[i]["PickUpTimes"]);
                            objResponse.Products = objProducts;
                        }
                        string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                        strResponseName = strResponseName.Replace("\"Products\"", "\"data\"");
                        HttpContext.Current.Response.Write(strResponseName);
                        //HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));

                    }
                    else
                    {
                        NoRecordExists();
                    }
                }
                else
                {
                    NoRecordExists();
                }
            }
            catch (Exception Ex)
            {
                ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
            }
        }
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void UserFavouriteBusinessListWithDistance(long UserID, string SecretKey, string AuthToken, string Latitude, string Longitude)
    {
        bool IsValidated = false;
        // ================User Last Login Location Track=============================
        string strSuburb = string.Empty;
        if (HttpContext.Current.Request["Suburb"] != null)
        {
            strSuburb = Convert.ToString(HttpContext.Current.Request["Suburb"]);
        }
        // ================User Last Login Location Track=============================

        if (ValidateRequestBAL.UserValidateClientRequest(Convert.ToInt64(UserID), Convert.ToString(SecretKey), Convert.ToString(AuthToken), strSuburb))
        {
            IsValidated = true;
        }

        if (!IsValidated)
        {
            CommonAPI objCommonAPI = new CommonAPI();
            objCommonAPI.Unauthorized();
        }
        else
        {
            string strResponse = string.Empty;
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            try
            {
                Response objResponse = new Response();
                DataTable dt = new DataTable();

                UsersBAL objUsersBAL = new UsersBAL();
                objUsersBAL.ID = Convert.ToInt64(UserID);

                string StateCode = "VIC";
                if (HttpContext.Current.Request["StateCode"] != null)
                {
                    StateCode = Convert.ToString(HttpContext.Current.Request["StateCode"]).ToUpper();
                }

                //dt = objUsersBAL.UserFavouriteBusinessList();
                dt = UserFavouriteBusinessListDAL(UserID, Latitude, Longitude, StateCode);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        objResponse.success = "true";
                        objResponse.message = dt.Rows.Count + " records found.";

                        Products[] objProducts = new Products[dt.Rows.Count];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            objProducts[i] = new Products();
                            objProducts[i].ID = Convert.ToInt64(dt.Rows[i]["ID"]);
                            objProducts[i].BusinessName = Convert.ToString(dt.Rows[i]["Name"]);
                            objProducts[i].FullName = Convert.ToString(dt.Rows[i]["FullName"]);
                            objProducts[i].EmailAddress = Convert.ToString(dt.Rows[i]["EmailAddress"]);
                            objProducts[i].BusinessPhone = Convert.ToString(dt.Rows[i]["BusinessPhone"]);
                            objProducts[i].Mobile = Convert.ToString(dt.Rows[i]["Mobile"]);
                            if (Convert.ToString(dt.Rows[i]["ImageName"]) != string.Empty)
                                objProducts[i].ImageFile = Config.WebSiteUrl + Config.CMSFiles + Convert.ToString(dt.Rows[i]["ImageName"]);
                            else
                                objProducts[i].ImageFile = string.Empty;
                            objProducts[i].StreetAddress = Convert.ToString(dt.Rows[i]["StreetAddress"]);
                            objProducts[i].Location = Convert.ToString(dt.Rows[i]["Location"]);
                            objProducts[i].TotalQty = Convert.ToInt32(dt.Rows[i]["TotalQty"]);
                            objProducts[i].IsFavourite = Convert.ToInt32(1);
                            objProducts[i].PurchaseQty = Convert.ToInt32(dt.Rows[i]["PurchaseQty"]);
                            if (Convert.ToInt32(dt.Rows[i]["RemainingQty"]) < 0)
                                objProducts[i].RemainingQty = 0;
                            else
                                objProducts[i].RemainingQty = Convert.ToInt32(dt.Rows[i]["RemainingQty"]);

                            objProducts[i].OriginalPrice = Convert.ToDouble(dt.Rows[i]["OriginalPrice"]).ToString("f2");
                            objProducts[i].DiscountPrice = Convert.ToDouble(dt.Rows[i]["DiscountPrice"]).ToString("f2");
                            // objProducts[i].FoodItems = Convert.ToString(dt.Rows[i]["FoodItems"]);
                            objProducts[i].Description = Convert.ToString(dt.Rows[i]["Description"]);
                            if (Convert.ToString(dt.Rows[i]["PickupFromTime"]) != string.Empty)
                                objProducts[i].PickupFromTime = Convert.ToDateTime(dt.Rows[i]["PickupFromTime"]).ToString("hh:mm tt");
                            else
                                objProducts[i].PickupFromTime = string.Empty;


                            if (Convert.ToString(dt.Rows[i]["PickupToTime"]) != string.Empty)
                                objProducts[i].PickupToTime = Convert.ToDateTime(dt.Rows[i]["PickupToTime"]).ToString("hh:mm tt");
                            else
                                objProducts[i].PickupToTime = string.Empty;

                            //objProducts[i].Distance = Convert.ToString(dt.Rows[i]["GeoDistance"]);


                            if (Convert.ToString(dt.Rows[i]["GeoDistance"]) != string.Empty)
                            {
                                if (Convert.ToDecimal(dt.Rows[i]["GeoDistance"]) * 1000 > 1000)
                                    objProducts[i].Distance = Convert.ToInt32(dt.Rows[i]["GeoDistance"]) + " KM";
                                else
                                    objProducts[i].Distance = Convert.ToInt32(Convert.ToDecimal(dt.Rows[i]["GeoDistance"]) * 1000) + " M";
                            }
                            else
                                objProducts[i].Distance = "0M";
                            objProducts[i].Latitude = Convert.ToString(dt.Rows[i]["Latitude"]);
                            objProducts[i].Longitude = Convert.ToString(dt.Rows[i]["Longitude"]);
                            ////3 Feb , 2018
                            //if (Convert.ToString(dt.Rows[i]["Discount"]) != string.Empty)
                            //{
                            //    objProducts[i].Discount = Convert.ToDouble(dt.Rows[i]["Discount"]).ToString("f2").Replace(".00", "") + "% OFF";
                            //}
                            //else
                            //{
                            //    objProducts[i].Discount = "0% OFF";
                            //}
                            //objProducts[i].PickupTimes = Convert.ToString(dt.Rows[i]["PickUpTimes"]);
                            objResponse.Products = objProducts;
                        }
                        string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                        strResponseName = strResponseName.Replace("\"Products\"", "\"data\"");
                        HttpContext.Current.Response.Write(strResponseName);
                        //HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));

                    }
                    else
                    {
                        NoRecordExists();
                    }
                }
                else
                {
                    NoRecordExists();
                }
            }
            catch (Exception Ex)
            {
                ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
            }
        }
        HttpContext.Current.Response.End();
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void UserCardDetails(long UserID, string SecretKey, string AuthToken)
    {
        bool IsValidated = false;
        // ================User Last Login Location Track=============================
        string strSuburb = string.Empty;
        if (HttpContext.Current.Request["Suburb"] != null)
        {
            strSuburb = Convert.ToString(HttpContext.Current.Request["Suburb"]);
        }
        // ================User Last Login Location Track=============================


        if (ValidateRequestBAL.UserValidateClientRequest(Convert.ToInt64(UserID), Convert.ToString(SecretKey), Convert.ToString(AuthToken), strSuburb))
        {
            IsValidated = true;
        }

        if (!IsValidated)
        {
            CommonAPI objCommonAPI = new CommonAPI();
            objCommonAPI.Unauthorized();
        }
        else
        {
            string strResponse = string.Empty;
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            try
            {
                Response objResponse = new Response();
                DataTable dt = new DataTable();

                UsersBAL objUsersBAL = new UsersBAL();
                objUsersBAL.ID = Convert.ToInt64(UserID);

                dt = objUsersBAL.UserCardList();

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        objResponse.success = "true";
                        objResponse.message = dt.Rows.Count + " records found.";

                        UserCards[] objUserCards = new UserCards[dt.Rows.Count];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            objUserCards[i] = new UserCards();
                            objUserCards[i].ID = Convert.ToInt64(dt.Rows[i]["ID"]);
                            objUserCards[i].CustomerID = Convert.ToString(dt.Rows[i]["CustomerID"]);
                            objUserCards[i].LastDigits = Convert.ToString(dt.Rows[i]["LastDigits"]);
                            objUserCards[i].CardType = Convert.ToString(dt.Rows[i]["CardType"]);
                            objUserCards[i].Mobile = Convert.ToString(dt.Rows[i]["Mobile"]);
                            objUserCards[i].IsDefault = Convert.ToInt32(dt.Rows[i]["IsDefault"]);

                            objUserCards[i].ExpiryMonth = Convert.ToString(dt.Rows[i]["ExpiryMonth"]);
                            objUserCards[i].ExpiryYear = Convert.ToString(dt.Rows[i]["ExpiryYear"]);

                            objResponse.UserCards = objUserCards;
                        }
                        string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                        strResponseName = strResponseName.Replace("\"UserCards\"", "\"data\"");
                        HttpContext.Current.Response.Write(strResponseName);

                    }
                    else
                    {
                        NoRecordExists();
                    }
                }
                else
                {
                    NoRecordExists();
                }
            }
            catch (Exception Ex)
            {
                ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
            }
        }
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void UserImpactAndRewards(long UserID, string SecretKey, string AuthToken)
    {
        bool IsValidated = false;
        // ================User Last Login Location Track=============================
        string strSuburb = string.Empty;
        if (HttpContext.Current.Request["Suburb"] != null)
        {
            strSuburb = Convert.ToString(HttpContext.Current.Request["Suburb"]);
        }
        // ================User Last Login Location Track=============================

        if (ValidateRequestBAL.UserValidateClientRequest(Convert.ToInt64(UserID), Convert.ToString(SecretKey), Convert.ToString(AuthToken), strSuburb))
        {
            IsValidated = true;
        }

        if (!IsValidated)
        {
            CommonAPI objCommonAPI = new CommonAPI();
            objCommonAPI.Unauthorized();
        }
        else
        {
            string strResponse = string.Empty;
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            try
            {
                Response objResponse = new Response();
                DataTable dt = new DataTable();

                UsersBAL objUsersBAL = new UsersBAL();
                objUsersBAL.ID = Convert.ToInt64(UserID);

                dt = objUsersBAL.UserImpactAndRewards();

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        objResponse.success = "true";
                        objResponse.message = dt.Rows.Count + " records found.";

                        ImpactAndRewards[] objImpactAndRewards = new ImpactAndRewards[dt.Rows.Count];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            objImpactAndRewards[i] = new ImpactAndRewards();
                            objImpactAndRewards[i].YouSaved = "$" + Convert.ToString(dt.Rows[i]["YouSaved"]);
                            objImpactAndRewards[i].YouRescued = Convert.ToString(dt.Rows[i]["YouRescued"]) + " meals";
                            objImpactAndRewards[i].TogetherSaved = Convert.ToString(dt.Rows[i]["TogetherSaved"]);// + " meals";
                            objImpactAndRewards[i].RedeemableAmount = "$" + Convert.ToString(dt.Rows[i]["RedeemableAmount"]);
                            objImpactAndRewards[i].RewardsPoints = Convert.ToString(dt.Rows[i]["RewardsPoints"]);

                            if (Convert.ToString(dt.Rows[i]["TogetherSaved"]) != string.Empty)
                            {
                                //1.  objImpactAndRewards[i].Co2Text = "That's over " + (Convert.ToInt32(dt.Rows[i]["TogetherSaved"]) * 1.05) + " Kg of CO2*.";
                                //2. objImpactAndRewards[i].Co2Text = "That's over " + (Convert.ToInt32(dt.Rows[i]["TogetherSaved"]) * 2) + " Kg of CO2*.";
                                objImpactAndRewards[i].Co2Text = (Convert.ToInt32(dt.Rows[i]["TogetherSaved"]) * 0.95).ToString();// "That's over " + (Convert.ToInt32(dt.Rows[i]["TogetherSaved"]) * 0.95) + " Kg of CO2*.";
                            }
                            else
                            {
                                objImpactAndRewards[i].Co2Text = "0";// "That's over 0 Kg of CO2*.";
                            }
                            objResponse.ImpactAndRewards = objImpactAndRewards;
                        }
                        string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                        strResponseName = strResponseName.Replace("\"ImpactAndRewards\"", "\"data\"");
                        HttpContext.Current.Response.Write(strResponseName);
                    }
                    else
                    {
                        NoRecordExists();
                    }
                }
                else
                {
                    NoRecordExists();
                }
            }
            catch (Exception Ex)
            {
                ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
            }
        }
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void GetBusinessPickupTimes(long UserID, string SecretKey, string AuthToken, long BusinessID)
    {
        bool IsValidated = false;
        // ================User Last Login Location Track=============================
        string strSuburb = string.Empty;
        if (HttpContext.Current.Request["Suburb"] != null)
        {
            strSuburb = Convert.ToString(HttpContext.Current.Request["Suburb"]);
        }
        // ================User Last Login Location Track=============================

        if (ValidateRequestBAL.UserValidateClientRequest(Convert.ToInt64(UserID), Convert.ToString(SecretKey), Convert.ToString(AuthToken), strSuburb))
        {
            IsValidated = true;
        }

        if (!IsValidated)
        {
            CommonAPI objCommonAPI = new CommonAPI();
            objCommonAPI.Unauthorized();
        }
        else
        {
            string strResponse = string.Empty;
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            try
            {
                Response objResponse = new Response();
                DataTable dt = new DataTable();

                BusinessBAL objBusinessBAL = new BusinessBAL();
                objBusinessBAL.ID = Convert.ToInt64(BusinessID);


                string StateCode = "VIC";
                if (HttpContext.Current.Request["StateCode"] != null)
                {
                    StateCode = Convert.ToString(HttpContext.Current.Request["StateCode"]).ToUpper();
                }

                //dt = objBusinessBAL.BusinessPickupTimesByBusinessID(objBusinessBAL, StateCode);
                dt = BusinessPickupTimesByBusinessID(objBusinessBAL, StateCode);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        objResponse.success = "true";
                        objResponse.message = dt.Rows.Count + " records found.";

                        BusinessPickupTimes[] objBusinessPickupTimes = new BusinessPickupTimes[dt.Rows.Count];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            objBusinessPickupTimes[i] = new BusinessPickupTimes();
                            objBusinessPickupTimes[i].BusinessID = Convert.ToInt64(dt.Rows[i]["BusinessID"]);
                            objBusinessPickupTimes[i].PickUpTimeID = Convert.ToInt64(dt.Rows[i]["PickUpTimeID"]);
                            objBusinessPickupTimes[i].PickupFromTime = Convert.ToDateTime(dt.Rows[i]["PickupFromTime"]).ToString("hh:mm tt").ToUpper();
                            objBusinessPickupTimes[i].PickupToTime = Convert.ToDateTime(dt.Rows[i]["PickupToTime"]).ToString("hh:mm tt").ToUpper();
                            if (Convert.ToInt32(dt.Rows[i]["RemainingQty"]) < 0)
                                objBusinessPickupTimes[i].RemainingQty = 0;
                            else
                                objBusinessPickupTimes[i].RemainingQty = Convert.ToInt32(dt.Rows[i]["RemainingQty"]);
                            objResponse.BusinessPickupTimes = objBusinessPickupTimes;
                        }
                        string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                        strResponseName = strResponseName.Replace("\"BusinessPickupTimes\"", "\"data\"");
                        HttpContext.Current.Response.Write(strResponseName);
                    }
                    else
                    {
                        NoRecordExists();
                    }
                }
                else
                {
                    NoRecordExists();
                }
            }
            catch (Exception Ex)
            {
                ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
            }
        }
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void UserRewardsPointsForCartReedem(long UserID, string SecretKey, string AuthToken)
    {
        bool IsValidated = false;
        // ================User Last Login Location Track=============================
        string strSuburb = string.Empty;
        if (HttpContext.Current.Request["Suburb"] != null)
        {
            strSuburb = Convert.ToString(HttpContext.Current.Request["Suburb"]);
        }
        // ================User Last Login Location Track=============================

        if (ValidateRequestBAL.UserValidateClientRequest(Convert.ToInt64(UserID), Convert.ToString(SecretKey), Convert.ToString(AuthToken), strSuburb))
        {
            IsValidated = true;
        }

        if (!IsValidated)
        {
            CommonAPI objCommonAPI = new CommonAPI();
            objCommonAPI.Unauthorized();
        }
        else
        {
            string strResponse = string.Empty;
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            try
            {
                Response objResponse = new Response();
                DataTable dt = new DataTable();
                UsersBAL objUsersBAL = new UsersBAL();

                objUsersBAL.ID = UserID;

                dt = objUsersBAL.UserDetailsByID();
                if (dt.Rows.Count > 0)
                {
                    objResponse.success = "true";
                    int RewardsPoints = Convert.ToInt32(dt.Rows[0]["RewardsPoints"]);
                    int loop = RewardsPoints / 5000;


                    objResponse.message = dt.Rows.Count + " records found.";

                    UserRewardsPoints[] objUserRewardsPoints = new UserRewardsPoints[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        objUserRewardsPoints[i] = new UserRewardsPoints();
                        objUserRewardsPoints[i].UserID = Convert.ToInt64(dt.Rows[i]["ID"]);
                        long RPoints = Convert.ToInt64(dt.Rows[i]["RewardsPoints"]);
                        objUserRewardsPoints[i].RewardsPoints = RPoints.ToString("#,##0");

                        if (RewardsPoints >= 5000)
                        {
                            objUserRewardsPoints[i].ReedemPoints = new ReedemPoints[loop + 1];
                            decimal RewardsSettings = Convert.ToDecimal(dt.Rows[i]["RewardsSettings"]);

                            for (int j = 0; j <= loop; j++)
                            {
                                objUserRewardsPoints[i].ReedemPoints[j] = new ReedemPoints();
                                if (j == 0)
                                {
                                    objUserRewardsPoints[i].ReedemPoints[j].Text = "Don't use points now";
                                }
                                else
                                {
                                    objUserRewardsPoints[i].ReedemPoints[j].Text = "Use " + (j * 5000).ToString("#,##0") + " points for $" + Convert.ToString((RewardsSettings * (j * 5000)) / 5000).Replace(".00", "");
                                }
                                objUserRewardsPoints[i].ReedemPoints[j].Points = Convert.ToInt64(j * 5000);
                                objUserRewardsPoints[i].ReedemPoints[j].Amount = Convert.ToString((RewardsSettings * (j * 5000)) / 5000).Replace(".00", "");
                            }
                        }
                        else
                        {
                            objUserRewardsPoints[i].ReedemPoints = new ReedemPoints[0];
                        }
                        objResponse.UserRewardsPoints = objUserRewardsPoints;
                    }
                    string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                    strResponseName = strResponseName.Replace("\"UserRewardsPoints\"", "\"data\"");
                    HttpContext.Current.Response.Write(strResponseName);


                }
                else
                {
                    NoRecordExists();
                }
            }
            catch (Exception Ex)
            {
                ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
            }
        }
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void UserCardSetDefault(long UserID, string SecretKey, string AuthToken, long CardID)
    {
        bool IsValidated = false;
        // ================User Last Login Location Track=============================
        string strSuburb = string.Empty;
        if (HttpContext.Current.Request["Suburb"] != null)
        {
            strSuburb = Convert.ToString(HttpContext.Current.Request["Suburb"]);
        }
        // ================User Last Login Location Track=============================


        if (ValidateRequestBAL.UserValidateClientRequest(Convert.ToInt64(UserID), Convert.ToString(SecretKey), Convert.ToString(AuthToken), strSuburb))
        {
            IsValidated = true;
        }

        if (!IsValidated)
        {
            CommonAPI objCommonAPI = new CommonAPI();
            objCommonAPI.Unauthorized();
        }
        else
        {
            string strResponse = string.Empty;
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            try
            {
                Response objResponse = new Response();

                UserAPIBAL objUserAPIBAL = new UserAPIBAL();
                objUserAPIBAL.UserCardSetDefault(UserID, CardID);

                objResponse.success = "true";
                objResponse.message = "Default Card saved successfully.";

                string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                HttpContext.Current.Response.Write(strResponseName);
            }
            catch (Exception Ex)
            {
                ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
            }
        }
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void GetReferCode(long UserID, string SecretKey, string AuthToken)
    {
        bool IsValidated = false;
        // ================User Last Login Location Track=============================
        string strSuburb = string.Empty;
        if (HttpContext.Current.Request["Suburb"] != null)
        {
            strSuburb = Convert.ToString(HttpContext.Current.Request["Suburb"]);
        }
        // ================User Last Login Location Track=============================

        if (ValidateRequestBAL.UserValidateClientRequest(Convert.ToInt64(UserID), Convert.ToString(SecretKey), Convert.ToString(AuthToken), strSuburb))
        {
            IsValidated = true;
        }

        if (!IsValidated)
        {
            CommonAPI objCommonAPI = new CommonAPI();
            objCommonAPI.Unauthorized();
        }
        else
        {
            string strResponse = string.Empty;
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            try
            {
                Response objResponse = new Response();
                DataTable dt = new DataTable();

                UserAPIBAL objUsersBAL = new UserAPIBAL();

                dt = objUsersBAL.UserReferCode(UserID);

                DataTable dtCMS = new DataTable();
                CMSBAL objCMSBAL = new CMSBAL();
                objCMSBAL.ID = 6;
                dtCMS = objCMSBAL.GetCMSByID_Webservice();
                string strReferTitle = "";
                string strReferDesc = "";
                if (dtCMS.Rows.Count > 0)
                {
                    strReferTitle = Convert.ToString(dtCMS.Rows[0]["PageTitle"]);
                    strReferDesc = Convert.ToString(dtCMS.Rows[0]["PageDescription"]);
                }

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        objResponse.success = "true";
                        objResponse.message = dt.Rows.Count + " records found.";

                        UserReferCodes[] objUserReferCodes = new UserReferCodes[dt.Rows.Count];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            objUserReferCodes[i] = new UserReferCodes();
                            objUserReferCodes[i].ID = Convert.ToString(dt.Rows[i]["ID"]);
                            objUserReferCodes[i].ReferCode = Convert.ToString(dt.Rows[i]["ReferralCode"]);

                            string strValue = Convert.ToString(new GeneralSettings().getConfigValue("ReferralCodeAmount"));
                            // objUserReferCodes[i].Title = "Refer Friends";
                            //objUserReferCodes[i].Description = "Hi! Get $" + Convert.ToString(strValue) + " off your first order at Bring Me Home and I’ll get something too. Use " + Convert.ToString(dt.Rows[i]["ReferralCode"]) + " at checkout.";
                            objUserReferCodes[i].Title = strReferTitle;
                            objUserReferCodes[i].Description = strReferDesc;

                            objUserReferCodes[i].ReferAmount = Convert.ToString(strValue);
                            strValue = "Share your code with friends! Once they've signed up and applied the code, they'll get $" + strValue + " to spend on rescuing food and you'll get $" + strValue + " right after they've made their first purchase.";
                            objUserReferCodes[i].ReferText = strValue;



                            objResponse.UserReferCodes = objUserReferCodes;
                        }
                        string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                        strResponseName = strResponseName.Replace("\"UserReferCodes\"", "\"data\"");
                        HttpContext.Current.Response.Write(strResponseName);
                    }
                    else
                    {
                        NoRecordExists();
                    }
                }
                else
                {
                    NoRecordExists();
                }
            }
            catch (Exception Ex)
            {
                ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
            }
        }
        HttpContext.Current.Response.End();
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void GetUserReferCodesForCheckout(long UserID, string SecretKey, string AuthToken)
    {
        bool IsValidated = false;
        // ================User Last Login Location Track=============================
        string strSuburb = string.Empty;
        if (HttpContext.Current.Request["Suburb"] != null)
        {
            strSuburb = Convert.ToString(HttpContext.Current.Request["Suburb"]);
        }
        // ================User Last Login Location Track=============================

        if (ValidateRequestBAL.UserValidateClientRequest(Convert.ToInt64(UserID), Convert.ToString(SecretKey), Convert.ToString(AuthToken), strSuburb))
        {
            IsValidated = true;
        }

        if (!IsValidated)
        {
            CommonAPI objCommonAPI = new CommonAPI();
            objCommonAPI.Unauthorized();
        }
        else
        {
            string strResponse = string.Empty;
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            try
            {
                Response objResponse = new Response();
                DataTable dt = new DataTable();

                UserAPIBAL objUsersBAL = new UserAPIBAL();

                dt = objUsersBAL.UserReferralListForCheckOut(UserID);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        objResponse.success = "true";
                        objResponse.message = dt.Rows.Count + " records found.";

                        UserReferCodesForCheckOut[] objUserReferCodesForCheckOut = new UserReferCodesForCheckOut[dt.Rows.Count];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            objUserReferCodesForCheckOut[i] = new UserReferCodesForCheckOut();
                            objUserReferCodesForCheckOut[i].Code = Convert.ToString(dt.Rows[i]["Code"]);

                            if (Convert.ToString(dt.Rows[i]["DiscountType"]) == "1")
                                objUserReferCodesForCheckOut[i].DiscountType = "F";
                            else
                                objUserReferCodesForCheckOut[i].DiscountType = "P";

                            objUserReferCodesForCheckOut[i].Amount = Convert.ToDecimal(dt.Rows[i]["DiscountAmount"]);

                            objResponse.UserReferCodesForCheckOut = objUserReferCodesForCheckOut;
                        }
                        string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                        strResponseName = strResponseName.Replace("\"UserReferCodesForCheckOut\"", "\"data\"");
                        HttpContext.Current.Response.Write(strResponseName);
                    }
                    else
                    {
                        NoRecordExists();
                    }
                }
                else
                {
                    NoRecordExists();
                }
            }
            catch (Exception Ex)
            {
                ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
            }
        }
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void GetUserPromoCodes(long UserID, string SecretKey, string AuthToken)
    {
        bool IsValidated = false;
        // ================User Last Login Location Track=============================
        string strSuburb = string.Empty;
        if (HttpContext.Current.Request["Suburb"] != null)
        {
            strSuburb = Convert.ToString(HttpContext.Current.Request["Suburb"]);
        }
        // ================User Last Login Location Track=============================

        if (ValidateRequestBAL.UserValidateClientRequest(Convert.ToInt64(UserID), Convert.ToString(SecretKey), Convert.ToString(AuthToken), strSuburb))
        {
            IsValidated = true;
        }

        if (!IsValidated)
        {
            CommonAPI objCommonAPI = new CommonAPI();
            objCommonAPI.Unauthorized();
        }
        else
        {
            string strResponse = string.Empty;
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            try
            {
                Response objResponse = new Response();
                DataTable dt = new DataTable();

                UserAPIBAL objUsersBAL = new UserAPIBAL();

                dt = objUsersBAL.UserPromoCodeList(UserID);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        objResponse.success = "true";
                        objResponse.message = dt.Rows.Count + " records found.";

                        UserReferCodesForCheckOut[] objUserReferCodesForCheckOut = new UserReferCodesForCheckOut[dt.Rows.Count];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            objUserReferCodesForCheckOut[i] = new UserReferCodesForCheckOut();
                            objUserReferCodesForCheckOut[i].Code = Convert.ToString(dt.Rows[i]["Code"]);

                            if (Convert.ToString(dt.Rows[i]["DiscountType"]) == "1")
                                objUserReferCodesForCheckOut[i].DiscountType = "F";
                            else
                                objUserReferCodesForCheckOut[i].DiscountType = "P";

                            objUserReferCodesForCheckOut[i].Amount = Convert.ToDecimal(dt.Rows[i]["DiscountAmount"]);
                            objUserReferCodesForCheckOut[i].MinimumOrderAmount = Convert.ToDecimal(dt.Rows[i]["MinOrderAmount"]);

                            if (Convert.ToString(dt.Rows[i]["IsPromoCode"]) == "1")
                            {
                                objUserReferCodesForCheckOut[i].ExpiryText = "Promo Expires " + Convert.ToDateTime(dt.Rows[i]["Expired"]).ToString("dd/MM/yy");
                            }
                            else
                            {
                                objUserReferCodesForCheckOut[i].ExpiryText = "Friend Referral Code";
                            }

                            objResponse.UserReferCodesForCheckOut = objUserReferCodesForCheckOut;
                        }
                        string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                        strResponseName = strResponseName.Replace("\"UserReferCodesForCheckOut\"", "\"data\"");
                        HttpContext.Current.Response.Write(strResponseName);
                    }
                    else
                    {
                        NoRecordExists();
                    }
                }
                else
                {
                    NoRecordExists();
                }
            }
            catch (Exception Ex)
            {
                ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
            }
        }
        HttpContext.Current.Response.End();
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void GetUserPromoCodesForCheckout(long UserID, string SecretKey, string AuthToken, long BusinessID)
    {
        bool IsValidated = false;
        // ================User Last Login Location Track=============================
        string strSuburb = string.Empty;
        if (HttpContext.Current.Request["Suburb"] != null)
        {
            strSuburb = Convert.ToString(HttpContext.Current.Request["Suburb"]);
        }
        // ================User Last Login Location Track=============================

        if (ValidateRequestBAL.UserValidateClientRequest(Convert.ToInt64(UserID), Convert.ToString(SecretKey), Convert.ToString(AuthToken), strSuburb))
        {
            IsValidated = true;
        }

        if (!IsValidated)
        {
            CommonAPI objCommonAPI = new CommonAPI();
            objCommonAPI.Unauthorized();
        }
        else
        {
            string strResponse = string.Empty;
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            try
            {
                Response objResponse = new Response();
                DataTable dt = new DataTable();

                UserAPIBAL objUsersBAL = new UserAPIBAL();

                dt = objUsersBAL.UserPromoCodeListForCheckOut(UserID, BusinessID);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        objResponse.success = "true";
                        objResponse.message = (dt.Rows.Count + 1) + " records found.";

                        UserReferCodesForCheckOut[] objUserReferCodesForCheckOut = new UserReferCodesForCheckOut[dt.Rows.Count + 1];
                        objUserReferCodesForCheckOut[0] = new UserReferCodesForCheckOut();
                        objUserReferCodesForCheckOut[0].Code = "Don't use code";
                        objUserReferCodesForCheckOut[0].UserPromoCodeID = "0";
                        objUserReferCodesForCheckOut[0].DiscountType = "F";
                        objUserReferCodesForCheckOut[0].Amount = 0;
                        objUserReferCodesForCheckOut[0].MinimumOrderAmount = 0;
                        objUserReferCodesForCheckOut[0].ExpiryText = "";
                        objResponse.UserReferCodesForCheckOut = objUserReferCodesForCheckOut;

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            objUserReferCodesForCheckOut[i + 1] = new UserReferCodesForCheckOut();


                            objUserReferCodesForCheckOut[i + 1].Code = Convert.ToString(dt.Rows[i]["Code"]);
                            objUserReferCodesForCheckOut[i + 1].UserPromoCodeID = Convert.ToString(dt.Rows[i]["UserPromoCodeID"]);

                            if (Convert.ToString(dt.Rows[i]["DiscountType"]) == "1")
                                objUserReferCodesForCheckOut[i + 1].DiscountType = "F";
                            else
                                objUserReferCodesForCheckOut[i + 1].DiscountType = "P";

                            objUserReferCodesForCheckOut[i + 1].Amount = Convert.ToDecimal(dt.Rows[i]["DiscountAmount"]);
                            objUserReferCodesForCheckOut[i + 1].MinimumOrderAmount = Convert.ToDecimal(dt.Rows[i]["MinOrderAmount"]);

                            if (Convert.ToString(dt.Rows[i]["IsPromoCode"]) == "1")
                            {
                                objUserReferCodesForCheckOut[i + 1].ExpiryText = "Promo Expires " + Convert.ToDateTime(dt.Rows[i]["Expired"]).ToString("dd/MM/yy");
                            }
                            else
                            {
                                objUserReferCodesForCheckOut[i + 1].ExpiryText = "Friend Referral Code";
                            }


                            objResponse.UserReferCodesForCheckOut = objUserReferCodesForCheckOut;
                        }
                        string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                        strResponseName = strResponseName.Replace("\"UserReferCodesForCheckOut\"", "\"data\"");
                        HttpContext.Current.Response.Write(strResponseName);
                    }
                    else
                    {
                        NoRecordExists();
                    }
                }
                else
                {
                    NoRecordExists();
                }
            }
            catch (Exception Ex)
            {
                ExceptionTRack(Ex.Message.ToString()); HttpContext.Current.Response.End();
            }
        }
        HttpContext.Current.Response.End();
    }


    #endregion


    #region Common Methods
    public void Unauthorized()
    {
        // HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        Response objResponse = new Response();
        objResponse.success = "false";
        objResponse.StatusCode = "401";
        objResponse.message = "Currently you are logged in another device.\n\nPlease verify your account to continue";
        HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));

        //List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        //Dictionary<string, object> row;
        //System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        //row = new Dictionary<string, object>();
        //row.Add("success", "false");
        //row.Add("message", "401 Unauthorized");
        //rows.Add(row);
        //HttpContext.Current.Response.Write(serializer.Serialize(rows));
    }
    public void NoRecordExists()
    {
        Response objResponse = new Response();
        objResponse.success = "false";
        objResponse.message = "No records exists.";
        HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
    }
    public void ExceptionTRack(string strException)
    {
        Response objResponse = new Response();
        objResponse.success = "false";
        objResponse.message = strException;
        HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
        //List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        //Dictionary<string, object> row;
        //System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

        //row = new Dictionary<string, object>();
        //row.Add("success", "false");
        //row.Add("message", strException);
        //rows.Add(row);
        //HttpContext.Current.Response.Write(serializer.Serialize(rows));
    }
    public void InvalidLogin()
    {
        Response objResponse = new Response();
        objResponse.success = "false";
        objResponse.message = "Invalid Email or Password.";
        HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
    }
    #endregion

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void BindBusinessForPromocode()
    {

        Response objResponse = new Response();
        try
        {

            DataTable dt = new DataTable();
            CommonBAL objCommonBAL = new CommonBAL();
            dt = objCommonBAL.GetBusinessOrUsers(1);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {

                    FoodItems[] objFoodItems = new FoodItems[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        objFoodItems[i] = new FoodItems();
                        objFoodItems[i].ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                        objFoodItems[i].Name = Convert.ToString(dt.Rows[i]["Name"]);
                    }

                    objResponse.FoodItems = objFoodItems;
                }
                string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                strResponseName = strResponseName.Replace("\"FoodItems\"", "\"data\"");
                HttpContext.Current.Response.Write(strResponseName);
            }
        }
        catch (Exception Ex)
        {

        }

        HttpContext.Current.Response.End();
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void BindBusinessForPromocodeWithState(int StateID)
    {

        Response objResponse = new Response();
        try
        {

            DataTable dt = new DataTable();
            CommonBAL objCommonBAL = new CommonBAL();
            dt = GetBusinessOrUsersWithState(1, StateID);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {

                    BusinessStates[] objBusinessStates = new BusinessStates[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        objBusinessStates[i] = new BusinessStates();
                        objBusinessStates[i].ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                        objBusinessStates[i].Name = Convert.ToString(dt.Rows[i]["Name"]);
                        objBusinessStates[i].StateCode = Convert.ToString(dt.Rows[i]["StateCode"]);
                    }

                    objResponse.BusinessStates = objBusinessStates;
                }
                string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                strResponseName = strResponseName.Replace("\"BusinessStates\"", "\"data\"");
                HttpContext.Current.Response.Write(strResponseName);
            }
        }
        catch (Exception Ex)
        {

        }

        HttpContext.Current.Response.End();
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void BindUsersForPromocode()
    {
        Response objResponse = new Response();
        try
        {
            DataTable dt = new DataTable();
            CommonBAL objCommonBAL = new CommonBAL();
            dt = objCommonBAL.GetBusinessOrUsers(0);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {

                    FoodItems[] objFoodItems = new FoodItems[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        objFoodItems[i] = new FoodItems();
                        objFoodItems[i].ID = Convert.ToInt32(dt.Rows[i]["ID"]);
                        objFoodItems[i].Name = Convert.ToString(dt.Rows[i]["Name"]);
                    }

                    objResponse.FoodItems = objFoodItems;
                }
                string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                strResponseName = strResponseName.Replace("\"FoodItems\"", "\"data\"");
                HttpContext.Current.Response.Write(strResponseName);
            }
        }
        catch (Exception Ex)
        {

        }

        HttpContext.Current.Response.End();
    }

    //[WebMethod]
    ////[ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]  


    //[ScriptService]

    //public void BindBusinessForPromocode()
    //{
    //    string Json = "";
    //    CommonBAL objCommonBAL = new CommonBAL();
    //    DataTable dt = new DataTable();
    //    dt = objCommonBAL.GetBusinessOrUsers(1);
    //    Json = JsonConvert.SerializeObject(dt);

    //    //System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

    //    HttpContext.Current.Response.Write(Json);

    //}

    private string GetServingLeft(int ServingQty)
    {
        string result = "";
        if (ServingQty > 10 & ServingQty < 20)
        {
            result = "10+";
        }
        else if (ServingQty > 20 & ServingQty < 30)
        {
            result = "20+";
        }
        else if (ServingQty > 30 & ServingQty < 40)
        {
            result = "30+";
        }
        else if (ServingQty > 40 & ServingQty < 50)
        {
            result = "40+";
        }
        else if (ServingQty > 50 & ServingQty < 60)
        {
            result = "50+";
        }
        else if (ServingQty > 60 & ServingQty < 70)
        {
            result = "60+";
        }
        else if (ServingQty > 70 & ServingQty < 80)
        {
            result = "70+";
        }
        else if (ServingQty > 80 & ServingQty < 90)
        {
            result = "80+";
        }
        else if (ServingQty > 90 & ServingQty < 100)
        {
            result = "90+";
        }
        else if (ServingQty > 100)
        {
            result = "100+";
        }
        return result;
    }

    public DataTable GetDonationsByID(string strKeyword, string StateCode)
    {
        DAL.DbParameter[] dbParam = new DAL.DbParameter[] {
            new DAL.DbParameter("@Keyword", DAL.DbParameter.DbType.VarChar, 2000, strKeyword),
            new DAL.DbParameter("@StateCode", DAL.DbParameter.DbType.VarChar, 200, StateCode)
        };
        DataTable table = new DataTable();
        return DAL.DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "BusinessLocationAutocomplete", dbParam);
    }

    private DataTable BusinessProductListDAL(long UserID, string CategoryIDs, string Search1, string Search2, string Search3, int Postcode, ref int CurrentPage, int RecordPerPage, out int TotalRecord, string Latitude, string Longitude, string RestaurantType, int ActiveDeals, int PickupNow, int Version, string PickupFromTime, string PickupToTime, string StateCode, decimal Distance, string DietaryFilter, decimal MinPrice, decimal MaxPrice, int IsTomorrow)
    {
        DAL.DbParameter[] dbParam = new DAL.DbParameter[] {
                new DAL.DbParameter("@UserID", DAL.DbParameter.DbType.Int, 200, UserID),
                new DAL.DbParameter("@CategoryIDs", DAL.DbParameter.DbType.VarChar, 2000, CategoryIDs),
                new DAL.DbParameter("@Search1", DAL.DbParameter.DbType.VarChar, 2000, Search1),
                new DAL.DbParameter("@Search2", DAL.DbParameter.DbType.VarChar, 2000, Search2),
                new DAL.DbParameter("@Search3", DAL.DbParameter.DbType.VarChar, 2000, Search3),
                new DAL.DbParameter("@Postcode", DAL.DbParameter.DbType.VarChar, 2000, Postcode),
                new DAL.DbParameter("@CurrentPage", DAL.DbParameter.DbType.Int, 20, (int) CurrentPage),
                new DAL.DbParameter("@RecordPerPage", DAL.DbParameter.DbType.Int, 20, RecordPerPage),
                new DAL.DbParameter("@TotalRecord", DAL.DbParameter.DbType.Int, 20, ParameterDirection.Output),
                new DAL.DbParameter("@Lat1", DAL.DbParameter.DbType.VarChar, 500, Latitude),
                new DAL.DbParameter("@Lon1", DAL.DbParameter.DbType.VarChar, 500, Longitude),
                new DAL.DbParameter("@RestaurantTypes", DAL.DbParameter.DbType.VarChar, 1000, RestaurantType),

                new DAL.DbParameter("@ActiveDeals", DAL.DbParameter.DbType.Int, 4, ActiveDeals),
                new DAL.DbParameter("@PickupNow", DAL.DbParameter.DbType.Int, 4, PickupNow),
                new DAL.DbParameter("@Version", DAL.DbParameter.DbType.Int, 4, Version),
                new DAL.DbParameter("@PickupFromTime", DAL.DbParameter.DbType.VarChar, 400, PickupFromTime),
                new DAL.DbParameter("@PickupToTime", DAL.DbParameter.DbType.VarChar, 400, PickupToTime),
                new DAL.DbParameter("@StateCode", DAL.DbParameter.DbType.VarChar, 400, StateCode),
                new DAL.DbParameter("@Distance", DAL.DbParameter.DbType.VarChar, 400, Distance),
                new DAL.DbParameter("@MinPrice", DAL.DbParameter.DbType.VarChar, 400, MinPrice),
                new DAL.DbParameter("@MaxPrice", DAL.DbParameter.DbType.VarChar, 400, MaxPrice),
                new DAL.DbParameter("@DietaryFilter", DAL.DbParameter.DbType.VarChar, 400, DietaryFilter),
                new DAL.DbParameter("@IsTomorrow", DAL.DbParameter.DbType.Int, 400, IsTomorrow  )

            };
        DataTable table = new DataTable();
        table = DAL.DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "BusinessProductList", dbParam);
        CurrentPage = Convert.ToInt32(dbParam[6].Value);
        TotalRecord = Convert.ToInt32(dbParam[8].Value);
        return table;
    }
    private void SendActivationEmailToAdmin(long BusinessID)
    {
        string strVendorName = string.Empty;
        string strEmailAddress = string.Empty;
        DataTable dtVendor = new DataTable();
        BusinessBAL objB = new BusinessBAL();
        objB.ID = BusinessID;
        dtVendor = objB.BusinessDetailsByIDForContactEnquiry();
        if (dtVendor.Rows.Count > 0)
        {
            strVendorName = Convert.ToString(dtVendor.Rows[0]["Name"]);
            strEmailAddress = Convert.ToString(dtVendor.Rows[0]["EmailAddress"]);
        }

        string strHeader = "<!doctype html><html><head><meta charset='utf-8'><title>Registration</title><link href='https://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet'><style>body {	padding: 0px;	margin: 0px;}</style></head><body>";
        string strFooter = "</body></html>";

        EmailTemplateBAL objEmail = new EmailTemplateBAL();
        DataTable dt = new DataTable();
        dt = objEmail.GetByID(6, 1);
        if (dt.Rows.Count > 0)
        {

            string strSubject = string.Empty;
            strSubject = Convert.ToString(dt.Rows[0]["Subject"]);
            System.Text.StringBuilder sbEmailTemplate = new System.Text.StringBuilder(strHeader + Convert.ToString(dt.Rows[0]["Template"]) + strFooter);

            sbEmailTemplate.Replace("###BusinessName###", strVendorName);
            sbEmailTemplate.Replace("###BusinessEmail###", strEmailAddress);
            sbEmailTemplate.Replace("###WebsiteName###", Convert.ToString(Config.WebsiteName));
            sbEmailTemplate.Replace("###siteurl###", Config.WebSiteUrl);
            sbEmailTemplate.Replace("###SiteName###", Config.WebsiteName);

            GeneralSettings.SendEmail(new GeneralSettings().getConfigValue("abnno"), new GeneralSettings().getConfigValue("abnno"), strSubject.Replace("###BusinessName###", strVendorName), sbEmailTemplate.ToString());

        }
    }
    private void SendOrderPickupNotifications(DataTable dtUsers)
    {
        SendNotification objSendNotification = new SendNotification();
        string strMessage = string.Empty;
        string strMessageBusiness = string.Empty;
        string strUsers = string.Empty;
        string strBusiness = string.Empty;
        for (int i = 0; i < dtUsers.Rows.Count; i++)
        {
            strMessageBusiness = "Order #" + Convert.ToInt64(dtUsers.Rows[i]["OrderID"]) + " has been redeemed successfully.";
            strMessage = "Thanks for collecting your food, enjoy!";
            if (Convert.ToString(dtUsers.Rows[i]["DeviceKey"]) != string.Empty)
            {
                int NotificationCount = 0;
                if (Convert.ToString(dtUsers.Rows[i]["NotificationCount"]) == string.Empty)
                {
                    NotificationCount = Convert.ToInt32(dtUsers.Rows[i]["NotificationCount"]);
                    NotificationCount = NotificationCount + 1;
                }
                if (Convert.ToString(dtUsers.Rows[i]["UserType"]).ToLower() == "u")
                {
                    strUsers = strUsers + Convert.ToString(dtUsers.Rows[i]["UserID"]) + ",";
                    objSendNotification.CallNotification(Convert.ToString(dtUsers.Rows[i]["DeviceKey"]), strMessage, "BMH Notification", Convert.ToString(dtUsers.Rows[i]["OrderID"]), "OrderNotification", Convert.ToString(dtUsers.Rows[i]["DeviceType"]), Convert.ToString(dtUsers.Rows[i]["UserType"]), NotificationCount);
                }
                else
                {
                    strBusiness = strBusiness + Convert.ToString(dtUsers.Rows[i]["UserID"]) + ",";
                    objSendNotification.CallNotification(Convert.ToString(dtUsers.Rows[i]["DeviceKey"]), strMessageBusiness, "BMH Notification", Convert.ToString(dtUsers.Rows[i]["OrderID"]), "OrderNotification", Convert.ToString(dtUsers.Rows[i]["DeviceType"]), Convert.ToString(dtUsers.Rows[i]["UserType"]), NotificationCount);
                }
            }
        }
        if (strUsers.Length > 0)
        {
            strUsers = strUsers.Substring(0, strUsers.Length - 1);
        }
        if (strBusiness.Length > 0)
        {
            strBusiness = strBusiness.Substring(0, strBusiness.Length - 1);
        }

        if (strBusiness != string.Empty || strUsers != string.Empty)
        {
            GeneralBAL objGeneralBAL = new GeneralBAL();
            objGeneralBAL.UpdateNoticationsCountForUsers(strUsers, strBusiness);
        }
    }
    private DataTable UserFavouriteBusinessListDAL(long UserID, string Lat, string Long, string StateCode)
    {
        DAL.DbParameter[] dbParam = new DAL.DbParameter[] {
                new DAL.DbParameter("@UserID", DAL.DbParameter.DbType.Int, 200, UserID),
                new DAL.DbParameter("@Lat1", DAL.DbParameter.DbType.VarChar, 2000, Lat),
                new DAL.DbParameter("@Lon1", DAL.DbParameter.DbType.VarChar, 2000, Long),
                new DAL.DbParameter("@StateCode", DAL.DbParameter.DbType.VarChar, 20, StateCode)
                };
        return DAL.DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "UserFavouriteBusinessList", dbParam);
    }

    private DataTable BusinessDetailsByIDDAL(long UserID, long ID, string Latitude, string Longitude, string StateCode)
    {
        DAL.DbParameter[] dbParam = new DAL.DbParameter[] {
                new DAL.DbParameter("@ID", DAL.DbParameter.DbType.Int, 20, ID),
                new DAL.DbParameter("@UserID", DAL.DbParameter.DbType.Int, 20, UserID),
                new DAL.DbParameter("@Lat1", DAL.DbParameter.DbType.VarChar, 500, Latitude),
                new DAL.DbParameter("@Lon1", DAL.DbParameter.DbType.VarChar, 500, Longitude),
                new DAL.DbParameter("@StateCode", DAL.DbParameter.DbType.VarChar, 10, StateCode)
            };
        return DAL.DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "BusinessDetailsByID", dbParam);
    }
    public DataTable BusinessPickupTimesByBusinessID(BusinessBAL objBusinessBAL, string StateCode)
    {
        DAL.DbParameter[] dbParam = new DAL.DbParameter[] {
                new  DAL.DbParameter("@ID",  DAL.DbParameter.DbType.Int, 20, objBusinessBAL.ID),
                new  DAL.DbParameter("@StateCode",  DAL.DbParameter.DbType.VarChar, 20, StateCode)
            };
        return DAL.DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "BusinessPickupTImesByBusinessID", dbParam);
    }
    public DataTable AppleUserListDetails(string AppleID)
    {
        DAL.DbParameter[] dbParam = new DAL.DbParameter[] {
                new  DAL.DbParameter("@AppleID",  DAL.DbParameter.DbType.VarChar, 2000, AppleID)
            };
        return DAL.DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "AppleUserDetailsGet", dbParam);
    }

    public DataTable GetBusinessOrUsersWithState(int IsBusiness, int StateID)
    {
        DAL.DbParameter[] dbParam = new DAL.DbParameter[]
            {
                new DAL.DbParameter("@IsBusiness", DAL.DbParameter.DbType.Int, 4, IsBusiness),
                new DAL.DbParameter("@StateID", DAL.DbParameter.DbType.Int, 4, StateID),
             };
        return DAL.DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "GetBusinessOrUsersWithState", dbParam);
    }

    public DataTable BusinessSubscriptionStatus(long BusinessID)
    {
        DAL.DbParameter[] dbParam = new DAL.DbParameter[] {
            new DAL.DbParameter("@BusinessID", DAL.DbParameter.DbType.Int, 2000, BusinessID)
        };
        DataTable table = new DataTable();
        return DAL.DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "BusinessSubscriptionStatus", dbParam);
    }
    public DataTable GetBusinessCardList(long BusinessID)
    {
        DbParameter[] dbParam = new DbParameter[] {
            new DbParameter("@BusinessID", DbParameter.DbType.Int, 200, BusinessID)
        };
        return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "BusinessCardList", dbParam);
    }
    private void WriteLogFile(string LogMessage)
    {
        try
        {
            string path = System.Web.Hosting.HostingEnvironment.MapPath("~/source") + "\\LogFile.txt";
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(LogMessage);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(LogMessage);
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    private DataSet BusinessCurrentDayOrdersDAL(long BusinessID, string AuthToken, string FromDate, string ToDate)
    {
        DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@BusinessID", DbParameter.DbType.Int, 200, BusinessID),
                new DbParameter("@AuthToken", DbParameter.DbType.VarChar, 2000, AuthToken),
                new DbParameter("@FromDate", DbParameter.DbType.VarChar, 2000, FromDate),
                new DbParameter("@ToDate", DbParameter.DbType.VarChar, 2000, ToDate)
            };
        return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "BusinessCurrentDayOrders", dbParam);
    }

    private DataSet BusinessRatingsDAL(long BusinessID)

    {

        DbParameter[] dbParam = new DbParameter[] {

        new DbParameter("@BusinessID", DbParameter.DbType.Int, 200, BusinessID)

};

        return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "BusinessRatings", dbParam);
    }
}