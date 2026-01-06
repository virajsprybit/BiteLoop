using BAL;
using BiteLoop.Common;
using DAL;
using PAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Utility;

public partial class AdminPanel_vendor_Modify : AdminAuthentication
{
    #region Private Members
    private long _ID = 0;
    BusinessBAL objBusinessBAL = new BusinessBAL();
    protected int SelectedProfilePhotoID = 0;
    protected string GoogleMapApiKey = "";
    #endregion

    #region Public Members
    public new long ID
    {
        get
        {
            return _ID;
        }
    }
    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        string businessName = string.Empty;
        string abn = string.Empty;
        string description = string.Empty;
        string emailAddress = string.Empty;

        // Loop through all keys and assign to variables
        foreach (string key in Request.Form.Keys)
        {
            if (key.EndsWith("txtBusinessName"))
                businessName = Request.Form[key];

            if (key.EndsWith("txtABN"))
                abn = Request.Form[key];

            if (key.EndsWith("hdnContent"))
                description = Request.Form[key];

            if (key.EndsWith("txtEmail"))
                emailAddress = Request.Form[key];

            // Add more fields as needed
        }

        // Now you can easily inspect these variables in debugger
        // Or use them to populate your BusinessBAL object
        BusinessBAL objBusinessBAL = new BusinessBAL();
        objBusinessBAL.ID = Convert.ToInt64(ID); // Your existing ID
        objBusinessBAL.Name = businessName;
        objBusinessBAL.ABN = abn;
        objBusinessBAL.Description = description;
        objBusinessBAL.EmailAddress = emailAddress;
        Int64.TryParse(Request["id"], out _ID);
        objBusinessBAL.ID = ID;

        if (Request.HttpMethod == "POST")
        {
            SaveInfo();
        }
        else
        {
            BindDropdownsAndControls();
            BindState();
            BindControls();
            //if (objBusinessBAL.ID != 0)
            //{
            //    tbPassword.Visible = true;

            //}
            //else
            //{
            //    tbPassword.Visible = true;
            //}
        }
        GoogleMapApiKey = Convert.ToString(ConfigurationManager.AppSettings["GoogleMapApiKey"]).Trim();
    }
    #endregion

    #region Bind Controls

    private void BindDropdownsAndControls()
    {
        DataSet ds = new DataSet();
        BusinessBAL objBusinessBAl = new BusinessBAL();
        objBusinessBAl.ID = ID;
        ds = objBusinessBAl.BusinessDropdownRegistration();
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[2].Rows.Count > 0)
            {
                //rptFoodItems.DataSource = ds.Tables[2];
                //rptFoodItems.DataBind();
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                //rptCategory.DataSource = ds.Tables[0];
                //rptCategory.DataBind();
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                rptRestaurantTypes.DataSource = ds.Tables[0];
                rptRestaurantTypes.DataBind();
            }
            if (ds.Tables[4].Rows.Count > 0)
            {
                DietaryType.DataSource = ds.Tables[4];
                DietaryType.DataBind();
            }
            if (ds.Tables[3].Rows.Count > 0)
            {
                //rptProfilePhotos.DataSource = ds.Tables[3];
                //rptProfilePhotos.DataBind();
            }


            //if (ds.Tables[1].Rows.Count > 0)
            //{
            //    MondayDiscount.DataSource = ds.Tables[1];
            //    MondayDiscount.DataTextField = "Discount";
            //    MondayDiscount.DataValueField = "ID";
            //    MondayDiscount.DataBind();

            //    TuesdayDiscount.DataSource = ds.Tables[1];
            //    TuesdayDiscount.DataTextField = "Discount";
            //    TuesdayDiscount.DataValueField = "ID";
            //    TuesdayDiscount.DataBind();


            //    WednesdayDiscount.DataSource = ds.Tables[1];
            //    WednesdayDiscount.DataTextField = "Discount";
            //    WednesdayDiscount.DataValueField = "ID";
            //    WednesdayDiscount.DataBind();


            //    ThirsdayDiscount.DataSource = ds.Tables[1];
            //    ThirsdayDiscount.DataTextField = "Discount";
            //    ThirsdayDiscount.DataValueField = "ID";
            //    ThirsdayDiscount.DataBind();

            //    FridayDiscount.DataSource = ds.Tables[1];
            //    FridayDiscount.DataTextField = "Discount";
            //    FridayDiscount.DataValueField = "ID";
            //    FridayDiscount.DataBind();

            //    SaturdayDiscount.DataSource = ds.Tables[1];
            //    SaturdayDiscount.DataTextField = "Discount";
            //    SaturdayDiscount.DataValueField = "ID";
            //    SaturdayDiscount.DataBind();

            //    SundayDiscount.DataSource = ds.Tables[1];
            //    SundayDiscount.DataTextField = "Discount";
            //    SundayDiscount.DataValueField = "ID";
            //    SundayDiscount.DataBind();
            //}
            //MondayDiscount.Items.Insert(0, new ListItem("--Select--", "0"));
            //TuesdayDiscount.Items.Insert(0, new ListItem("--Select--", "0"));
            //WednesdayDiscount.Items.Insert(0, new ListItem("--Select--", "0"));
            //ThirsdayDiscount.Items.Insert(0, new ListItem("--Select--", "0"));
            //FridayDiscount.Items.Insert(0, new ListItem("--Select--", "0"));
            //SaturdayDiscount.Items.Insert(0, new ListItem("--Select--", "0"));
            //SundayDiscount.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }
    private void BindState()
    {
        DataTable dt = new DataTable();
        CommonBAL objCommonBAL = new CommonBAL();
        dt = objCommonBAL.StateList();
        if (dt != null && dt.Rows.Count > 0)
        {
            ddlState.DataSource = dt;
            ddlState.DataTextField = "StateCode";
            ddlState.DataValueField = "StateCode";
            ddlState.DataBind();
        }
        //ddlState.Items.Insert(0, new ListItem("--Select--", ""));
    }

    private void BindControls()
    {
        SalesAdminBAL objSalesAdminBAL = new SalesAdminBAL();
        objSalesAdminBAL.ID = ID;
        DataSet ds = objSalesAdminBAL.BusinessDetailsByID();

        if (ds.Tables[0].Rows.Count > 0)
        {
            txtBusinessName.Value = Convert.ToString(ds.Tables[0].Rows[0]["BusinessName"]);
            txtBusinessMobile.Value = Convert.ToString(ds.Tables[0].Rows[0]["Mobile"]);
            txtABN.Value = Convert.ToString(ds.Tables[0].Rows[0]["ABN"]);
            txtABNBank.Value = Convert.ToString(ds.Tables[0].Rows[0]["ABN"]);
            //txtAddress.Value = Convert.ToString(ds.Tables[0].Rows[0]["StreetAddress"]);
            txtLocation.Value = Convert.ToString(ds.Tables[0].Rows[0]["Location"]);
            txtBusinessEmail.Value = Convert.ToString(ds.Tables[0].Rows[0]["EmailAddress"]);
            //hdnLocation.Value = Convert.ToString(ds.Tables[0].Rows[0]["Location"]);
            //txtFullName.Value = Convert.ToString(ds.Tables[0].Rows[0]["FullName"]);
            txtBusinessPhone.Value = Convert.ToString(ds.Tables[0].Rows[0]["BusinessPhone"]);
            //txtphone.Value = Convert.ToString(ds.Tables[0].Rows[0]["Mobile"]);
            //txtEmail.Value = Convert.ToString(ds.Tables[0].Rows[0]["EmailAddress"]);

            StringBuilder sbTemplate = new StringBuilder(Convert.ToString(ds.Tables[0].Rows[0]["AboutUs"]));
            sbTemplate.Replace("{%WebSiteUrl%}", Config.WebSiteUrl);
            tareaAboutUs.Text = sbTemplate.ToString();

            StringBuilder sbTemplate1 = new StringBuilder(Convert.ToString(ds.Tables[0].Rows[0]["Description"]));
            sbTemplate.Replace("{%WebSiteUrl%}", Config.WebSiteUrl);
            tareaDescription.Text = sbTemplate1.ToString();


            //var selectedID = ds.Tables[1].AsEnumerable().Where(r => r.Field<int>("IsSelected") == 1).Select(r => r.Field<int>("ID")).FirstOrDefault();
            string selectedName = ds.Tables[1].AsEnumerable().Where(r => r.Field<int>("IsSelected") == 1).Select(r => r.Field<string>("Name")).FirstOrDefault() ?? string.Empty;

            var selectedID = ds.Tables[1].AsEnumerable()
                    .Where(r => r.Field<int>("IsSelected") == 1)
                    .Select(r => r.Field<int>("ID"))
                    .FirstOrDefault();

            foreach (RepeaterItem item in rptRestaurantTypes.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    HtmlInputCheckBox chk = (HtmlInputCheckBox)item.FindControl("clsBusinessTypes");
                    if (chk != null && chk.Value == selectedID.ToString())
                    {
                        chk.Checked = true;
                        break;
                    }
                }
            }

            //foreach (RepeaterItem item in DietaryType.Items)
            //{
            //    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            //    {
            //        HtmlInputCheckBox chk = (HtmlInputCheckBox)item.FindControl("chkRestaurantTypes");
            //        if (chk != null && chk.Value == selectedID.ToString())
            //        {
            //            chk.Checked = true;
            //            break; 
            //        }
            //    }
            //}
            string selectedNames = Convert.ToString(ds.Tables[0].Rows[0]["DietryIDs"]);
            string[] selectedNameArray = selectedNames.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                                     .Select(n => n.Trim())
                                                     .ToArray();

            foreach (RepeaterItem item in DietaryType.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    HtmlInputCheckBox chk = (HtmlInputCheckBox)item.FindControl("clsDietTypes");
                    if (chk != null && selectedNameArray.Contains(chk.Value))
                    {
                        chk.Checked = true;
                    }
                }
            }

            // store old filename
            //hdnOldPhoto.Value = photoName;

            // show existing image
            string photoName = ds.Tables[0].Rows[0]["ProfilePhoto"].ToString();
            if (photoName != "")
            {
                imgProfile.ImageUrl = "~/" + Config.CMSFiles + photoName;
            }
            else
            {
                imgProfile.ImageUrl = "~/" + Config.CMSFiles + "NoUserPhotoSelected.png";
            }
            string storeImage = ds.Tables[0].Rows[0]["StoreImages"].ToString();
            if (storeImage != "")
            {
                imgStore.ImageUrl = "~/" + Config.CMSFiles + storeImage;
            }
            else
            {
                imgStore.ImageUrl = "~/" + Config.CMSFiles + "NoStorePhotoSelected.jpg";
            }
            // store in hidden field
            //hdnOldStoreImage.Value = storeImage;

            // Display existing image if file exists

            //string savePath = Server.MapPath("~/" + Config.CMSFiles + photoName);

            //HttpPostedFile file = Request.Files[fupdImage.UniqueID];
            //file.SaveAs(savePath);

            // save uploaded file to server
            //fupdImage.SaveAs(savePath);


            //Request.Files[fupdImage.UniqueID].SaveAs(savePath);

            txtBSBNo.Value = Convert.ToString(ds.Tables[0].Rows[0]["BSBNo"]);
            txtAccountNo.Value = Convert.ToString(ds.Tables[0].Rows[0]["AccountNumber"]);
            txtBankName.Value = Convert.ToString(ds.Tables[0].Rows[0]["BankName"]);
            txtAccountName.Value = Convert.ToString(ds.Tables[0].Rows[0]["AccountName"]);

            //trConfirmPwd.Visible = false;
            //divlblPwd.Visible = true;
            //divPwd.Style.Add("display", "none");
            //txtPassword.Attributes.Add("value", "");
            //lblPassword.InnerText = "";

            //txtComission.Value = "";
            //txtPersonIncharge.Value = "";
            //ddlStatus.Value = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
            //txtLastName.Value = "";
            //txtBMHComission.Value = "";
            //ddlMultipleStore.Value = "";
            //ddlBYOContainers.Value = "";
            ddlState.Value = Convert.ToString(ds.Tables[0].Rows[0]["StateCode"]);

            //txtLatitude.Value = "";
            //txtLongitude.Value = "";

            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["ProfilePhotoID"])))
                SelectedProfilePhotoID = Convert.ToInt32(ds.Tables[0].Rows[0]["ProfilePhotoID"]);

            // Categories
            //string strCategory = string.Join(",", ds.Tables[1].AsEnumerable().Select(r => r["ID"].ToString()));
            //hdnCategory.Value = strCategory;

            //// Food Items
            //string strFoodItems = string.Join(",", ds.Tables[2].AsEnumerable().Select(r => r["ID"].ToString()));
            //hdnFoodItems.Value = strFoodItems;

            //// Restaurant Types
            //if (ds.Tables.Count > 4 && ds.Tables[2].Rows.Count > 0)
            //{
            //    string strRestaurantTypes = string.Join(",",
            //        ds.Tables[2].AsEnumerable().Select(r => r["ID"].ToString()));
            //    hdnRestaurantTypes.Value = strRestaurantTypes;
            //}
            //else
            //{
            //    hdnRestaurantTypes.Value = "";
            //}

            //bool isToday = Convert.ToDateTime(ds.Tables[7].Rows[0]["CreatedDate"]).Date == DateTime.Now.Date;
            //CurrentDayOn.Checked = isToday;
            //CurrentDayOff.Checked = !isToday;
            //hdnCurrentDayOn.Value = isToday ? "1" : "0";


            if (ds.Tables[7].Rows.Count > 0)
            {
                if (Convert.ToDateTime(ds.Tables[7].Rows[0]["CurrentDate"]).Date == DateTime.Now.Date)
                {
                    hdnCurrentDayOn.Value = "1";
                    CurrentDayOn.Checked = true;
                    CurrentDayOff.Checked = false;
                    CurrentDayOriginalPrice1.Value = Convert.ToString(ds.Tables[7].Rows[0]["OriginalPrice"]);
                    CurrentDayNoOfItems1.Value = Convert.ToString(ds.Tables[7].Rows[0]["NumberOfPack"]);
                    CurrentDayDiscount1.Value = Convert.ToString(ds.Tables[7].Rows[0]["DiscountedPrice"]);
                    CurrentDayOriginalPrice2.Value = Convert.ToString(ds.Tables[7].Rows[1]["OriginalPrice"]);
                    CurrentDayNoOfItems2.Value = Convert.ToString(ds.Tables[7].Rows[1]["NumberOfPack"]);
                    CurrentDayDiscount2.Value = Convert.ToString(ds.Tables[7].Rows[1]["DiscountedPrice"]);
                    CurrentDayOriginalPrice3.Value = Convert.ToString(ds.Tables[7].Rows[2]["OriginalPrice"]);
                    CurrentDayNoOfItems3.Value = Convert.ToString(ds.Tables[7].Rows[2]["NumberOfPack"]);
                    CurrentDayDiscount3.Value = Convert.ToString(ds.Tables[7].Rows[2]["DiscountedPrice"]);
                    CurrentDayFromTime.Value = ds.Tables[3].Rows[0]["Pickup_from"] == DBNull.Value ? "" : ((TimeSpan)ds.Tables[3].Rows[0]["Pickup_from"]).ToString(@"hh\:mm");
                    CurrentDayToTime.Value = ds.Tables[3].Rows[0]["Pickup_To"] == DBNull.Value ? "" : ((TimeSpan)ds.Tables[3].Rows[0]["Pickup_To"]).ToString(@"hh\:mm");

                }
                else if (Convert.ToDateTime(ds.Tables[7].Rows[0]["CurrentDate"]).Date == DateTime.Now.Date.AddDays(1))
                {
                    hdnCurrentDayOn.Value = "0";
                    CurrentDayOff.Checked = true;
                    CurrentDayOn.Checked = false;
                    CurrentDayOriginalPrice1.Value = Convert.ToString(ds.Tables[7].Rows[0]["OriginalPrice"]);
                    CurrentDayNoOfItems1.Value = Convert.ToString(ds.Tables[7].Rows[0]["NumberOfPack"]);
                    CurrentDayDiscount1.Value = Convert.ToString(ds.Tables[7].Rows[0]["DiscountedPrice"]);
                    CurrentDayOriginalPrice2.Value = Convert.ToString(ds.Tables[7].Rows[1]["OriginalPrice"]);
                    CurrentDayNoOfItems2.Value = Convert.ToString(ds.Tables[7].Rows[1]["NumberOfPack"]);
                    CurrentDayDiscount2.Value = Convert.ToString(ds.Tables[7].Rows[1]["DiscountedPrice"]);
                    CurrentDayOriginalPrice3.Value = Convert.ToString(ds.Tables[7].Rows[2]["OriginalPrice"]);
                    CurrentDayNoOfItems3.Value = Convert.ToString(ds.Tables[7].Rows[2]["NumberOfPack"]);
                    CurrentDayDiscount3.Value = Convert.ToString(ds.Tables[7].Rows[2]["DiscountedPrice"]);
                    CurrentDayFromTime.Value = Convert.ToDateTime(ds.Tables[3].Rows[0]["Pickup_from"]).ToString("HH:mm");
                    CurrentDayToTime.Value = Convert.ToDateTime(ds.Tables[3].Rows[0]["Pickup_To"]).ToString("HH:mm");
                }
                else
                {
                    CurrentDayOriginalPrice1.Value = "";
                    CurrentDayNoOfItems1.Value = "";
                    CurrentDayDiscount1.Value = "";
                    CurrentDayOriginalPrice2.Value = "";
                    CurrentDayNoOfItems2.Value = "";
                    CurrentDayDiscount2.Value = "";
                    CurrentDayOriginalPrice3.Value = "";
                    CurrentDayNoOfItems3.Value = "";
                    CurrentDayDiscount3.Value = "";
                    CurrentDayFromTime.Value = "00:00";
                    CurrentDayToTime.Value = "00:00";
                }
            }
            else
            {
                CurrentDayOriginalPrice1.Value = "";
                CurrentDayNoOfItems1.Value = "";
                CurrentDayDiscount1.Value = "";
                CurrentDayOriginalPrice2.Value = "";
                CurrentDayNoOfItems2.Value = "";
                CurrentDayDiscount2.Value = "";
                CurrentDayOriginalPrice3.Value = "";
                CurrentDayNoOfItems3.Value = "";
                CurrentDayDiscount3.Value = "";
                CurrentDayFromTime.Value = "00:00";
                CurrentDayToTime.Value = "00:00";
            }

            if (ds.Tables[9].Select("TRIM(DayName) = 'MON'").Count() > 0)
            {
                var weeklyScheduleId = ds.Tables[4].Select("TRIM(DayName) = 'MON'").FirstOrDefault()["ID"].ToString();
                var monRows = ds.Tables[8].Select("WeeklyScheduleID = '" + weeklyScheduleId + "'");

                hdnMondayOn.Value = "1";
                MondayOn.Checked = true;
                MondayOriginalPrice1.Value = Convert.ToString(monRows[0]["OriginalPrice"]);
                MondayNoOfItems1.Value = Convert.ToString(monRows[0]["NumberOfPack"]);
                MondayDiscount1.Value = Convert.ToString(monRows[0]["DiscountedPrice"]);
                MondayOriginalPrice2.Value = Convert.ToString(monRows[1]["OriginalPrice"]);
                MondayNoOfItems2.Value = Convert.ToString(monRows[1]["NumberOfPack"]);
                MondayDiscount2.Value = Convert.ToString(monRows[1]["DiscountedPrice"]);
                MondayOriginalPrice3.Value = Convert.ToString(monRows[2]["OriginalPrice"]);
                MondayNoOfItems3.Value = Convert.ToString(monRows[2]["NumberOfPack"]);
                MondayDiscount3.Value = Convert.ToString(monRows[2]["DiscountedPrice"]);
                //MondayFromTime.Value = Convert.ToDateTime(ds.Tables[4].Rows[0]["Pickup_from"]).ToString("HH:mm");
                MondayFromTime.Value = ds.Tables[4].Rows[0]["Pickup_from"] == DBNull.Value ? "" : ((TimeSpan)ds.Tables[4].Rows[0]["Pickup_from"]).ToString(@"hh\:mm");

                //MondayToTime.Value = Convert.ToDateTime(ds.Tables[4].Rows[0]["Pickup_To"]).ToString("HH:mm");
                MondayToTime.Value = ds.Tables[4].Rows[0]["Pickup_To"] == DBNull.Value ? "" : ((TimeSpan)ds.Tables[4].Rows[0]["Pickup_To"]).ToString(@"hh\:mm");

            }
            else
            {
                MondayOn.Checked = false;
                hdnMondayOn.Value = "0";
            }

            if (ds.Tables[9].Select("TRIM(DayName) = 'TUE'").Count() > 0)
            {
                var weeklyScheduleId = ds.Tables[4].Select("TRIM(DayName) = 'TUE'").FirstOrDefault()["ID"].ToString();
                var tueRows = ds.Tables[8].Select("WeeklyScheduleID = '" + weeklyScheduleId + "'");

                hdnTuesdayOn.Value = "1";
                TuesdayOn.Checked = true;
                TuesdayOriginalPrice1.Value = Convert.ToString(tueRows[0]["OriginalPrice"]);
                TuesdayNoOfItems1.Value = Convert.ToString(tueRows[0]["NumberOfPack"]);
                TuesdayDiscount1.Value = Convert.ToString(tueRows[0]["DiscountedPrice"]);
                TuesdayOriginalPrice2.Value = Convert.ToString(tueRows[1]["OriginalPrice"]);
                TuesdayNoOfItems2.Value = Convert.ToString(tueRows[1]["NumberOfPack"]);
                TuesdayDiscount2.Value = Convert.ToString(tueRows[1]["DiscountedPrice"]);
                TuesdayOriginalPrice3.Value = Convert.ToString(tueRows[2]["OriginalPrice"]);
                TuesdayNoOfItems3.Value = Convert.ToString(tueRows[2]["NumberOfPack"]);
                TuesdayDiscount3.Value = Convert.ToString(tueRows[2]["DiscountedPrice"]);
                TimeSpan t1; TuesdayFromTime.Value = TimeSpan.TryParse(ds.Tables[4].Rows[0]["Pickup_from"] == DBNull.Value ? null : ds.Tables[4].Rows[0]["Pickup_from"].ToString(), out t1) ? t1.ToString(@"hh\:mm") : "";
                TimeSpan t2; TuesdayToTime.Value = TimeSpan.TryParse(ds.Tables[4].Rows[0]["Pickup_To"] == DBNull.Value ? null : ds.Tables[4].Rows[0]["Pickup_To"].ToString(), out t2) ? t2.ToString(@"hh\:mm") : "";


            }
            else
            {
                TuesdayOn.Checked = false;
                hdnTuesdayOn.Value = "0";
            }

            if (ds.Tables[9].Select("TRIM(DayName) = 'WED'").Count() > 0)
            {
                var weeklyScheduleId = ds.Tables[4].Select("TRIM(DayName) = 'WED'").FirstOrDefault()["ID"].ToString();
                var wedRows = ds.Tables[8].Select("WeeklyScheduleID = '" + weeklyScheduleId + "'");

                hdnWednesdayOn.Value = "1";
                WednesdayOn.Checked = true;
                WednesdayOriginalPrice1.Value = Convert.ToString(wedRows[0]["OriginalPrice"]);
                WednesdayNoOfItems1.Value = Convert.ToString(wedRows[0]["NumberOfPack"]);
                WednesdayDiscount1.Value = Convert.ToString(wedRows[0]["DiscountedPrice"]);
                WednesdayOriginalPrice2.Value = Convert.ToString(wedRows[1]["OriginalPrice"]);
                WednesdayNoOfItems2.Value = Convert.ToString(wedRows[1]["NumberOfPack"]);
                WednesdayDiscount2.Value = Convert.ToString(wedRows[1]["DiscountedPrice"]);
                WednesdayOriginalPrice3.Value = Convert.ToString(wedRows[2]["OriginalPrice"]);
                WednesdayNoOfItems3.Value = Convert.ToString(wedRows[2]["NumberOfPack"]);
                WednesdayDiscount3.Value = Convert.ToString(wedRows[2]["DiscountedPrice"]);
                //WednesdayFromTime.Value = Convert.ToDateTime(ds.Tables[4].Rows[0]["Pickup_from"]).ToString("HH:mm");
                //WednesdayToTime.Value = Convert.ToDateTime(ds.Tables[4].Rows[0]["Pickup_To"]).ToString("HH:mm");
                TimeSpan t1; WednesdayFromTime.Value = TimeSpan.TryParse(ds.Tables[4].Rows[0]["Pickup_from"] == DBNull.Value ? null : ds.Tables[4].Rows[0]["Pickup_from"].ToString(), out t1) ? t1.ToString(@"hh\:mm") : "";
                TimeSpan t2; WednesdayToTime.Value = TimeSpan.TryParse(ds.Tables[4].Rows[0]["Pickup_To"] == DBNull.Value ? null : ds.Tables[4].Rows[0]["Pickup_To"].ToString(), out t2) ? t2.ToString(@"hh\:mm") : "";
            }
            else
            {
                WednesdayOn.Checked = false;
                hdnWednesdayOn.Value = "0";
            }

            if (ds.Tables[9].Select("TRIM(DayName) = 'THU'").Count() > 0)
            {
                var weeklyScheduleId = ds.Tables[4].Select("TRIM(DayName) = 'THU'").FirstOrDefault()["ID"].ToString();
                var thuRows = ds.Tables[8].Select("WeeklyScheduleID = '" + weeklyScheduleId + "'");

                hdnThursdayOn.Value = "1";
                ThirsdayOn.Checked = true;
                ThursdayOriginalPrice1.Value = Convert.ToString(thuRows[0]["OriginalPrice"]);
                ThursdayNoOfItems1.Value = Convert.ToString(thuRows[0]["NumberOfPack"]);
                ThursdayDiscount1.Value = Convert.ToString(thuRows[0]["DiscountedPrice"]);
                ThursdayOriginalPrice2.Value = Convert.ToString(thuRows[1]["OriginalPrice"]);
                ThursdayNoOfItems2.Value = Convert.ToString(thuRows[1]["NumberOfPack"]);
                ThursdayDiscount2.Value = Convert.ToString(thuRows[1]["DiscountedPrice"]);
                ThursdayOriginalPrice3.Value = Convert.ToString(thuRows[2]["OriginalPrice"]);
                ThursdayNoOfItems3.Value = Convert.ToString(thuRows[2]["NumberOfPack"]);
                ThursdayDiscount3.Value = Convert.ToString(thuRows[2]["DiscountedPrice"]);
                //ThursdayFromTime.Value = Convert.ToDateTime(ds.Tables[4].Rows[0]["Pickup_from"]).ToString("HH:mm");
                //ThursdayToTime.Value = Convert.ToDateTime(ds.Tables[4].Rows[0]["Pickup_To"]).ToString("HH:mm");
                TimeSpan t1; ThursdayFromTime.Value = TimeSpan.TryParse(ds.Tables[4].Rows[0]["Pickup_from"] == DBNull.Value ? null : ds.Tables[4].Rows[0]["Pickup_from"].ToString(), out t1) ? t1.ToString(@"hh\:mm") : "";
                TimeSpan t2; ThursdayToTime.Value = TimeSpan.TryParse(ds.Tables[4].Rows[0]["Pickup_To"] == DBNull.Value ? null : ds.Tables[4].Rows[0]["Pickup_To"].ToString(), out t2) ? t2.ToString(@"hh\:mm") : "";
            }
            else
            {
                ThirsdayOn.Checked = false;
                hdnThursdayOn.Value = "0";
            }

            if (ds.Tables[9].Select("TRIM(DayName) = 'FRI'").Count() > 0)
            {
                var weeklyScheduleId = ds.Tables[4].Select("TRIM(DayName) = 'FRI'").FirstOrDefault()["ID"].ToString();
                var friRows = ds.Tables[8].Select("WeeklyScheduleID = '" + weeklyScheduleId + "'");

                hdnFridayOn.Value = "1";
                FridayOn.Checked = true;
                FridayOriginalPrice1.Value = Convert.ToString(friRows[0]["OriginalPrice"]);
                FridayNoOfItems1.Value = Convert.ToString(friRows[0]["NumberOfPack"]);
                FridayDiscount1.Value = Convert.ToString(friRows[0]["DiscountedPrice"]);
                FridayOriginalPrice2.Value = Convert.ToString(friRows[1]["OriginalPrice"]);
                FridayNoOfItems2.Value = Convert.ToString(friRows[1]["NumberOfPack"]);
                FridayDiscount2.Value = Convert.ToString(friRows[1]["DiscountedPrice"]);
                FridayOriginalPrice3.Value = Convert.ToString(friRows[2]["OriginalPrice"]);
                FridayNoOfItems3.Value = Convert.ToString(friRows[2]["NumberOfPack"]);
                FridayDiscount3.Value = Convert.ToString(friRows[2]["DiscountedPrice"]);
                //FridayFromTime.Value = Convert.ToDateTime(ds.Tables[4].Rows[0]["Pickup_from"]).ToString("HH:mm");
                //FridayToTime.Value = Convert.ToDateTime(ds.Tables[4].Rows[0]["Pickup_To"]).ToString("HH:mm");
                TimeSpan t1; FridayFromTime.Value = TimeSpan.TryParse(ds.Tables[4].Rows[0]["Pickup_from"] == DBNull.Value ? null : ds.Tables[4].Rows[0]["Pickup_from"].ToString(), out t1) ? t1.ToString(@"hh\:mm") : "";
                TimeSpan t2; FridayToTime.Value = TimeSpan.TryParse(ds.Tables[4].Rows[0]["Pickup_To"] == DBNull.Value ? null : ds.Tables[4].Rows[0]["Pickup_To"].ToString(), out t2) ? t2.ToString(@"hh\:mm") : "";
            }
            else
            {
                FridayOn.Checked = false;
                hdnFridayOn.Value = "0";
            }

            if (ds.Tables[9].Select("TRIM(DayName) = 'SAT'").Count() > 0)
            {
                var weeklyScheduleId = ds.Tables[4].Select("TRIM(DayName) = 'SAT'").FirstOrDefault()["ID"].ToString();
                var satRows = ds.Tables[8].Select("WeeklyScheduleID = '" + weeklyScheduleId + "'");

                hdnSaturdayOn.Value = "1";
                SaturdayOn.Checked = true;
                SaturdayOriginalPrice1.Value = Convert.ToString(satRows[0]["OriginalPrice"]);
                SaturdayNoOfItems1.Value = Convert.ToString(satRows[0]["NumberOfPack"]);
                SaturdayDiscount1.Value = Convert.ToString(satRows[0]["DiscountedPrice"]);
                SaturdayOriginalPrice2.Value = Convert.ToString(satRows[1]["OriginalPrice"]);
                SaturdayNoOfItems2.Value = Convert.ToString(satRows[1]["NumberOfPack"]);
                SaturdayDiscount2.Value = Convert.ToString(satRows[1]["DiscountedPrice"]);
                SaturdayOriginalPrice3.Value = Convert.ToString(satRows[2]["OriginalPrice"]);
                SaturdayNoOfItems3.Value = Convert.ToString(satRows[2]["NumberOfPack"]);
                SaturdayDiscount3.Value = Convert.ToString(satRows[2]["DiscountedPrice"]);
                SaturdayFromTime.Value = Convert.ToDateTime(ds.Tables[4].Rows[0]["Pickup_from"]).ToString("HH:mm");
                SaturdayToTime.Value = Convert.ToDateTime(ds.Tables[4].Rows[0]["Pickup_To"]).ToString("HH:mm");
                TimeSpan t1; SaturdayFromTime.Value = TimeSpan.TryParse(ds.Tables[4].Rows[0]["Pickup_from"] == DBNull.Value ? null : ds.Tables[4].Rows[0]["Pickup_from"].ToString(), out t1) ? t1.ToString(@"hh\:mm") : "";
                TimeSpan t2; SaturdayToTime.Value = TimeSpan.TryParse(ds.Tables[4].Rows[0]["Pickup_To"] == DBNull.Value ? null : ds.Tables[4].Rows[0]["Pickup_To"].ToString(), out t2) ? t2.ToString(@"hh\:mm") : "";
            }
            else
            {
                SaturdayOn.Checked = false;
                hdnSaturdayOn.Value = "0";
            }

            if (ds.Tables[9].Select("TRIM(DayName) = 'SUN'").Count() > 0)
            {
                var weeklyScheduleId = ds.Tables[4].Select("TRIM(DayName) = 'FRI'").FirstOrDefault()["ID"].ToString();
                var sunRows = ds.Tables[8].Select("WeeklyScheduleID = '" + weeklyScheduleId + "'");

                hdnSundayOn.Value = "1";
                SundayOn.Checked = true;
                SundayOriginalPrice1.Value = Convert.ToString(sunRows[0]["OriginalPrice"]);
                SundayNoOfItems1.Value = Convert.ToString(sunRows[0]["NumberOfPack"]);
                SundayDiscount1.Value = Convert.ToString(sunRows[0]["DiscountedPrice"]);
                SundayOriginalPrice2.Value = Convert.ToString(sunRows[1]["OriginalPrice"]);
                SundayNoOfItems2.Value = Convert.ToString(sunRows[1]["NumberOfPack"]);
                SundayDiscount2.Value = Convert.ToString(sunRows[1]["DiscountedPrice"]);
                SundayOriginalPrice3.Value = Convert.ToString(sunRows[2]["OriginalPrice"]);
                SundayNoOfItems3.Value = Convert.ToString(sunRows[2]["NumberOfPack"]);
                SundayDiscount3.Value = Convert.ToString(sunRows[2]["DiscountedPrice"]);
                //SundayFromTime.Value = Convert.ToDateTime(ds.Tables[4].Rows[0]["Pickup_from"]).ToString("HH:mm");
                //SundayToTime.Value = Convert.ToDateTime(ds.Tables[4].Rows[0]["Pickup_To"]).ToString("HH:mm");
                TimeSpan t1; SundayFromTime.Value = TimeSpan.TryParse(ds.Tables[4].Rows[0]["Pickup_from"] == DBNull.Value ? null : ds.Tables[4].Rows[0]["Pickup_from"].ToString(), out t1) ? t1.ToString(@"hh\:mm") : "";
                TimeSpan t2; SundayToTime.Value = TimeSpan.TryParse(ds.Tables[4].Rows[0]["Pickup_To"] == DBNull.Value ? null : ds.Tables[4].Rows[0]["Pickup_To"].ToString(), out t2) ? t2.ToString(@"hh\:mm") : "";
            }
            else
            {
                SundayOn.Checked = false;
                hdnSundayOn.Value = "0";
            }

            BusinessBAL objBusinessBAL = new BusinessBAL();
            objBusinessBAL.ID = Convert.ToInt64(ID);
            objBusinessBAL.Name = Convert.ToString(Request[txtBusinessName.UniqueID]);
            objBusinessBAL.ABN = Convert.ToString(Request[txtABN.UniqueID]);
            //objBusinessBAL.StreetAddress = Convert.ToString(Request[txtStreetAddress.UniqueID]);
            objBusinessBAL.Location = Convert.ToString(Request[txtLocation.UniqueID]);
            //objBusinessBAL.FullName = Convert.ToString(Request[txtFullName.UniqueID]);
            objBusinessBAL.BusinessPhone = Convert.ToString(Request[txtBusinessPhone.UniqueID]);
            //objBusinessBAL.Mobile = Convert.ToString(Request[txtphone.UniqueID]);
            // objBusinessBAL.EmailAddress = Convert.ToString(Request[txtEmail.UniqueID]);
            //objBusinessBAL.ProfilePhotoID = Convert.ToInt32(Request["ProfilePhotoID"]);
            //objBusinessBAL.Description = Convert.ToString(Request[tareaDescription.UniqueID]);

            //System.Text.StringBuilder sbTemplate = new System.Text.StringBuilder(Server.HtmlDecode(Request[hdnContent.UniqueID]));
            sbTemplate.Replace(Config.WebSiteUrl, "{%WebSiteUrl%}");
            objBusinessBAL.Description = sbTemplate1.ToString();

            objBusinessBAL.BSBNo = Convert.ToString(Request[txtBSBNo.UniqueID]);
            objBusinessBAL.AccountNumber = Convert.ToString(Request[txtAccountNo.UniqueID]);
            objBusinessBAL.BankName = Convert.ToString(Request[txtBankName.UniqueID]);
            objBusinessBAL.AccountName = Convert.ToString(Request[txtAccountName.UniqueID]);
            //objBusinessBAL.Status = Convert.ToInt32(Request["Status"]);
            objBusinessBAL.BusinessType = Convert.ToString(Request[hdnCategory.UniqueID]);
            objBusinessBAL.FoodItems = Convert.ToString(Request[hdnFoodItems.UniqueID]);
            objBusinessBAL.RestaurantTypes = Convert.ToString(Request[hdnRestaurantTypes.UniqueID]);



            //string day = "Today";
            //bool isOn = true;
            //string noOfItems = ds.Tables[7].Rows[i]["NumberOfPack"].ToString();
            //string originalPrice = ds.Tables[7].Rows[i]["OriginalPrice"].ToString();
            //string discountedPrice = ds.Tables[7].Rows[i]["DiscountedPrice"].ToString();
            //string fromTime = ds.Tables[3].Rows[0]["Pickup_from"].ToString();
            //string toTime = ds.Tables[3].Rows[0]["Pickup_To"].ToString();

            //string dayPrefix = day == "Today" ? "Today" : "Tomorrow";
            //TextBox txtNoOfItems = (TextBox)FindControl("txt" + dayPrefix + "NumberOfPack" + i);
            //TextBox txtOriginalPrice = (TextBox)FindControl("txt" + dayPrefix + "OriginalPrice" + i);
            //TextBox txtDiscountedPrice = (TextBox)FindControl("txt" + dayPrefix + "DiscountedPrice" + i);
            //TextBox txtFromTime = (TextBox)FindControl("txt" + dayPrefix + "Pickup_from");
            //TextBox txtToTime = (TextBox)FindControl("txt" + dayPrefix + "Pickup_To");

            //if (txtNoOfItems != null) txtNoOfItems.Text = noOfItems;
            //if (txtOriginalPrice != null) txtOriginalPrice.Text = originalPrice;
            //if (txtDiscountedPrice != null) txtDiscountedPrice.Text = discountedPrice;
            //if (txtFromTime != null) txtFromTime.Text = fromTime;
            //if (txtToTime != null) txtToTime.Text = toTime;

            //RadioButton rdoOn = (RadioButton)FindControl("rdo" + dayPrefix + "On");
            //RadioButton rdoOff = (RadioButton)FindControl("rdo" + dayPrefix + "Off");
            //if (rdoOn != null && rdoOff != null)
            //{
            //    rdoOn.Checked = isOn;
            //    rdoOff.Checked = !isOn;
            //}

        }
    }


    //private void BindControls()
    //{
    //    SalesAdminBAL objSalesAdminBAL = new SalesAdminBAL();
    //    objSalesAdminBAL.ID = ID;
    //    DataSet ds = new DataSet();
    //    ds = objSalesAdminBAL.BusinessDetailsByID();
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        txtBusinessName.Value = Convert.ToString(ds.Tables[0].Rows[0]["BusinessName"]);
    //        txtABN.Value = Convert.ToString(ds.Tables[0].Rows[0]["ABN"]);
    //        txtStreetAddress.Value = Convert.ToString(ds.Tables[0].Rows[0]["StreetAddress"]);
    //        txtLocation.Value = Convert.ToString(ds.Tables[0].Rows[0]["Location"]);
    //        hdnLocation.Value = Convert.ToString(ds.Tables[0].Rows[0]["Location"]);
    //        txtFullName.Value = Convert.ToString(ds.Tables[0].Rows[0]["FullName"]);
    //        txtBusinessPhone.Value = Convert.ToString(ds.Tables[0].Rows[0]["BusinessPhone"]);
    //        txtphone.Value = Convert.ToString(ds.Tables[0].Rows[0]["Mobile"]);
    //        txtEmail.Value = Convert.ToString(ds.Tables[0].Rows[0]["EmailAddress"]);
    //        //tareaDescription.Value = Convert.ToString(ds.Tables[0].Rows[0]["Description"]);
    //        System.Text.StringBuilder sbTemplate = new System.Text.StringBuilder(Convert.ToString(ds.Tables[0].Rows[0]["Description"]));
    //        sbTemplate.Replace("{%WebSiteUrl%}", Config.WebSiteUrl);
    //        tareaDescription.Text = sbTemplate.ToString();

    //        txtBSBNo.Value = Convert.ToString(ds.Tables[0].Rows[0]["BSBNo"]);
    //        txtAccountNo.Value = Convert.ToString(ds.Tables[0].Rows[0]["AccountNumber"]);
    //        txtBankName.Value = Convert.ToString(ds.Tables[0].Rows[0]["BankName"]);
    //        txtAccountName.Value = Convert.ToString(ds.Tables[0].Rows[0]["AccountName"]);
    //        trConfirmPwd.Visible = false;
    //        divlblPwd.Visible = true;
    //        divPwd.Style.Add("display", "none");
    //        txtPassword.Attributes.Add("value", "");
    //        lblPassword.InnerText = "";

    //        //hdnCategory.Value = "";
    //        //hdnFoodItems.Value = "";
    //        txtComission.Value = "";     // Or hide it

    //        txtPersonIncharge.Value = "";
    //        ddlStatus.Value = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
    //        txtLastName.Value = "";
    //        txtBMHComission.Value = "";
    //        ddlMultipleStore.Value = "";
    //        ddlBYOContainers.Value = "";
    //        ddlState.Value = Convert.ToString(ds.Tables[0].Rows[0]["StateCode"]);
    //        ;

    //        txtLatitude.Value = "";
    //        txtLongitude.Value = "";


    //        if (Convert.ToString(ds.Tables[0].Rows[0]["ProfilePhotoID"]) != "")
    //            SelectedProfilePhotoID = Convert.ToInt32(ds.Tables[0].Rows[0]["ProfilePhotoID"]);

    //        string strCategory = string.Empty;

    //        if (ds.Tables[1].Rows.Count > 0)
    //        {
    //            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
    //            {
    //                strCategory += Convert.ToString(ds.Tables[1].Rows[i]["ID"]) + ",";
    //            }
    //        }

    //        if (strCategory.Length > 0)
    //        {
    //            strCategory = strCategory.Substring(0, strCategory.Length - 1);
    //        }

    //        hdnCategory.Value = strCategory;


    //        //string strCategory = string.Empty;
    //        //if (ds.Tables[1].Rows.Count > 0)
    //        //{
    //        //    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
    //        //    {
    //        //        strCategory = strCategory + Convert.ToString(ds.Tables[1].Rows[i]["CategoryID"]) + ",";
    //        //    }
    //        //}
    //        //if (strCategory.Length > 0)
    //        //{
    //        //    strCategory = strCategory.Substring(0, strCategory.Length - 1);
    //        //}
    //        //hdnCategory.Value = strCategory;


    //        string strFoodItems = string.Empty;
    //        if (ds.Tables[2].Rows.Count > 0)
    //        {
    //            for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
    //            {
    //                strFoodItems = strFoodItems + Convert.ToString(ds.Tables[2].Rows[i]["ID"]) + ",";
    //            }
    //        }
    //        if (strFoodItems.Length > 0)
    //        {
    //            strFoodItems = strFoodItems.Substring(0, strFoodItems.Length - 1);
    //        }
    //        hdnFoodItems.Value = strFoodItems;

    //        //-------------------3 Feb, 2019

    //        string strRestaurantTypes = string.Empty;

    //        if (ds.Tables.Count > 4 && ds.Tables[4].Rows.Count > 0)
    //        {
    //            strRestaurantTypes = string.Join(",",
    //                ds.Tables[4].AsEnumerable().Select(r => r["ID"].ToString()));
    //        }

    //        hdnRestaurantTypes.Value = strRestaurantTypes;


    //        //string strRestaurantTypes = string.Empty;
    //        //if (ds.Tables[4].Rows.Count > 0)
    //        //{
    //        //    for (int i = 0; i < ds.Tables[4].Rows.Count; i++)
    //        //    {
    //        //        strRestaurantTypes = strRestaurantTypes + Convert.ToString(ds.Tables[4].Rows[i]["RestaurantTypeID"]) + ",";
    //        //    }
    //        //}
    //        //if (strRestaurantTypes.Length > 0)
    //        //{
    //        //    strRestaurantTypes = strRestaurantTypes.Substring(0, strRestaurantTypes.Length - 1);
    //        //}
    //        //hdnRestaurantTypes.Value = strRestaurantTypes;

    //        if (ds.Tables[3].Rows.Count > 0)
    //        {
    //            for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
    //            {
    //                int PickUpTime = 1;
    //                int ScheduleOn = Convert.ToInt32(ds.Tables[3].Rows[i]["ScheduleOn"]);
    //                int DayNo = Convert.ToInt32(ds.Tables[3].Rows[i]["DayNo"]);
    //                if (ScheduleOn == 1)
    //                    PickUpTime = Convert.ToInt32(ds.Tables[3].Rows[i]["PickUpTime"]);

    //                #region Pickup Time 1

    //                if (PickUpTime == 1)
    //                {

    //                    if (DayNo == 1)
    //                    {
    //                        if (ScheduleOn == 1)
    //                        {
    //                            hdnMondayOn.Value = "1";
    //                            MondayOn.Checked = true;
    //                            MondayOriginalPrice.Value = Convert.ToString(ds.Tables[3].Rows[i]["OriginalPrice"]);
    //                            MondayNoOfItems.Value = Convert.ToString(ds.Tables[3].Rows[i]["NoOfItems"]);
    //                            //MondayDiscount.Value = Convert.ToString(ds.Tables[3].Rows[i]["DiscountID"]);
    //                            MondayDiscount.Value = Convert.ToDouble(Convert.ToDouble(ds.Tables[3].Rows[i]["OriginalPrice"]) - Convert.ToDouble(ds.Tables[3].Rows[i]["DiscountID"])).ToString("f2");


    //                            if (Convert.ToString(ds.Tables[3].Rows[i]["PickupFromTime"]) != string.Empty)
    //                            {
    //                                MondayFromTime.Value = Convert.ToDateTime(ds.Tables[3].Rows[i]["PickupFromTime"]).ToString("HH:mm");
    //                            }
    //                            if (Convert.ToString(ds.Tables[3].Rows[i]["PickupToTime"]) != string.Empty)
    //                            {
    //                                MondayToTime.Value = Convert.ToDateTime(ds.Tables[3].Rows[i]["PickupToTime"]).ToString("HH:mm");
    //                            }
    //                        }
    //                        else
    //                        {
    //                            MondayOn.Checked = false;
    //                            hdnMondayOn.Value = "0";
    //                        }
    //                    }
    //                    if (DayNo == 2)
    //                    {
    //                        if (ScheduleOn == 1)
    //                        {
    //                            hdnTuesdayOn.Value = "1";
    //                            TuesdayOn.Checked = true;
    //                            TuesdayOriginalPrice.Value = Convert.ToString(ds.Tables[3].Rows[i]["OriginalPrice"]);
    //                            TuesdayNoOfItems.Value = Convert.ToString(ds.Tables[3].Rows[i]["NoOfItems"]);
    //                            //TuesdayDiscount.Value = Convert.ToString(ds.Tables[3].Rows[i]["DiscountID"]);
    //                            TuesdayDiscount.Value = Convert.ToDouble(Convert.ToDouble(ds.Tables[3].Rows[i]["OriginalPrice"]) - Convert.ToDouble(ds.Tables[3].Rows[i]["DiscountID"])).ToString("f2");
    //                            if (Convert.ToString(ds.Tables[3].Rows[i]["PickupFromTime"]) != string.Empty)
    //                            {
    //                                TuesdayFromTime.Value = Convert.ToDateTime(ds.Tables[3].Rows[i]["PickupFromTime"]).ToString("HH:mm");
    //                            }
    //                            if (Convert.ToString(ds.Tables[3].Rows[i]["PickupToTime"]) != string.Empty)
    //                            {
    //                                TuesdayToTime.Value = Convert.ToDateTime(ds.Tables[3].Rows[i]["PickupToTime"]).ToString("HH:mm");
    //                            }
    //                        }
    //                        else
    //                        {
    //                            TuesdayOn.Checked = false;
    //                            hdnTuesdayOn.Value = "0";
    //                        }
    //                    }
    //                    if (DayNo == 3)
    //                    {

    //                        if (ScheduleOn == 1)
    //                        {
    //                            hdnWednesdayOn.Value = "1";
    //                            WednesdayOn.Checked = true;
    //                            WednesdayOriginalPrice.Value = Convert.ToString(ds.Tables[3].Rows[i]["OriginalPrice"]);
    //                            WednesdayNoOfItems.Value = Convert.ToString(ds.Tables[3].Rows[i]["NoOfItems"]);
    //                            //WednesdayDiscount.Value = Convert.ToString(ds.Tables[3].Rows[i]["DiscountID"]);
    //                            WednesdayDiscount.Value = Convert.ToDouble(Convert.ToDouble(ds.Tables[3].Rows[i]["OriginalPrice"]) - Convert.ToDouble(ds.Tables[3].Rows[i]["DiscountID"])).ToString("f2");
    //                            if (Convert.ToString(ds.Tables[3].Rows[i]["PickupFromTime"]) != string.Empty)
    //                            {
    //                                WednesdayFromTime.Value = Convert.ToDateTime(ds.Tables[3].Rows[i]["PickupFromTime"]).ToString("HH:mm");
    //                            }
    //                            if (Convert.ToString(ds.Tables[3].Rows[i]["PickupToTime"]) != string.Empty)
    //                            {
    //                                WednesdayToTime.Value = Convert.ToDateTime(ds.Tables[3].Rows[i]["PickupToTime"]).ToString("HH:mm");
    //                            }
    //                        }
    //                        else
    //                        {
    //                            WednesdayOn.Checked = false;
    //                            hdnWednesdayOn.Value = "0";
    //                        }
    //                    }
    //                    if (DayNo == 4)
    //                    {
    //                        if (ScheduleOn == 1)
    //                        {
    //                            hdnThursdayOn.Value = "1";
    //                            ThirsdayOn.Checked = true;
    //                            ThirsdayOriginalPrice.Value = Convert.ToString(ds.Tables[3].Rows[i]["OriginalPrice"]);
    //                            ThirsdayNoOfItems.Value = Convert.ToString(ds.Tables[3].Rows[i]["NoOfItems"]);
    //                            //ThirsdayDiscount.Value = Convert.ToString(ds.Tables[3].Rows[i]["DiscountID"]);
    //                            ThirsdayDiscount.Value = Convert.ToDouble(Convert.ToDouble(ds.Tables[3].Rows[i]["OriginalPrice"]) - Convert.ToDouble(ds.Tables[3].Rows[i]["DiscountID"])).ToString("f2");
    //                            if (Convert.ToString(ds.Tables[3].Rows[i]["PickupFromTime"]) != string.Empty)
    //                            {
    //                                ThirsdayFromTime.Value = Convert.ToDateTime(ds.Tables[3].Rows[i]["PickupFromTime"]).ToString("HH:mm");
    //                            }
    //                            if (Convert.ToString(ds.Tables[3].Rows[i]["PickupToTime"]) != string.Empty)
    //                            {
    //                                ThirsdayToTime.Value = Convert.ToDateTime(ds.Tables[3].Rows[i]["PickupToTime"]).ToString("HH:mm");
    //                            }
    //                        }
    //                        else
    //                        {
    //                            ThirsdayOn.Checked = false;
    //                            hdnThursdayOn.Value = "0";
    //                        }
    //                    }
    //                    if (DayNo == 5)
    //                    {
    //                        if (ScheduleOn == 1)
    //                        {
    //                            hdnFridayOn.Value = "1";
    //                            FridayOn.Checked = true;
    //                            FridayOriginalPrice.Value = Convert.ToString(ds.Tables[3].Rows[i]["OriginalPrice"]);
    //                            FridayNoOfItems.Value = Convert.ToString(ds.Tables[3].Rows[i]["NoOfItems"]);
    //                            //FridayDiscount.Value = Convert.ToString(ds.Tables[3].Rows[i]["DiscountID"]);
    //                            FridayDiscount.Value = Convert.ToDouble(Convert.ToDouble(ds.Tables[3].Rows[i]["OriginalPrice"]) - Convert.ToDouble(ds.Tables[3].Rows[i]["DiscountID"])).ToString("f2");
    //                            if (Convert.ToString(ds.Tables[3].Rows[i]["PickupFromTime"]) != string.Empty)
    //                            {
    //                                FridayFromTime.Value = Convert.ToDateTime(ds.Tables[3].Rows[i]["PickupFromTime"]).ToString("HH:mm");
    //                            }
    //                            if (Convert.ToString(ds.Tables[3].Rows[i]["PickupToTime"]) != string.Empty)
    //                            {
    //                                FridayToTime.Value = Convert.ToDateTime(ds.Tables[3].Rows[i]["PickupToTime"]).ToString("HH:mm");
    //                            }
    //                        }
    //                        else
    //                        {
    //                            FridayOn.Checked = false;
    //                            hdnFridayOn.Value = "0";
    //                        }
    //                    }
    //                    if (DayNo == 6)
    //                    {
    //                        if (ScheduleOn == 1)
    //                        {
    //                            hdnSaturdayOn.Value = "1";
    //                            SaturdayOn.Checked = true;
    //                            SaturdayOriginalPrice.Value = Convert.ToString(ds.Tables[3].Rows[i]["OriginalPrice"]);
    //                            SaturdayNoOfItems.Value = Convert.ToString(ds.Tables[3].Rows[i]["NoOfItems"]);
    //                            //SaturdayDiscount.Value = Convert.ToString(ds.Tables[3].Rows[i]["DiscountID"]);
    //                            SaturdayDiscount.Value = Convert.ToDouble(Convert.ToDouble(ds.Tables[3].Rows[i]["OriginalPrice"]) - Convert.ToDouble(ds.Tables[3].Rows[i]["DiscountID"])).ToString("f2");
    //                            if (Convert.ToString(ds.Tables[3].Rows[i]["PickupFromTime"]) != string.Empty)
    //                            {
    //                                SaturdayFromTime.Value = Convert.ToDateTime(ds.Tables[3].Rows[i]["PickupFromTime"]).ToString("HH:mm");
    //                            }
    //                            if (Convert.ToString(ds.Tables[3].Rows[i]["PickupToTime"]) != string.Empty)
    //                            {
    //                                SaturdayToTime.Value = Convert.ToDateTime(ds.Tables[3].Rows[i]["PickupToTime"]).ToString("HH:mm");
    //                            }
    //                        }
    //                        else
    //                        {
    //                            SaturdayOn.Checked = false;
    //                            hdnSaturdayOn.Value = "0";
    //                        }
    //                    }

    //                    if (DayNo == 7)
    //                    {
    //                        if (ScheduleOn == 1)
    //                        {
    //                            hdnSundayOn.Value = "1";
    //                            SundayOn.Checked = true;
    //                            SundayOriginalPrice.Value = Convert.ToString(ds.Tables[3].Rows[i]["OriginalPrice"]);
    //                            SundayNoOfItems.Value = Convert.ToString(ds.Tables[3].Rows[i]["NoOfItems"]);
    //                            //SundayDiscount.Value = Convert.ToString(ds.Tables[3].Rows[i]["DiscountID"]);
    //                            SundayDiscount.Value = Convert.ToDouble(Convert.ToDouble(ds.Tables[3].Rows[i]["OriginalPrice"]) - Convert.ToDouble(ds.Tables[3].Rows[i]["DiscountID"])).ToString("f2");
    //                            if (Convert.ToString(ds.Tables[3].Rows[i]["PickupFromTime"]) != string.Empty)
    //                            {
    //                                SundayFromTime.Value = Convert.ToDateTime(ds.Tables[3].Rows[i]["PickupFromTime"]).ToString("HH:mm");
    //                            }
    //                            if (Convert.ToString(ds.Tables[3].Rows[i]["PickupToTime"]) != string.Empty)
    //                            {
    //                                SundayToTime.Value = Convert.ToDateTime(ds.Tables[3].Rows[i]["PickupToTime"]).ToString("HH:mm");
    //                            }
    //                        }
    //                        else
    //                        {
    //                            SundayOn.Checked = false;
    //                            hdnSundayOn.Value = "0";
    //                        }
    //                    }
    //                }

    //                #endregion
    //            }
    //        }


    //        if (ds.Tables[5].Rows.Count > 0)
    //        {
    //            for (int i = 0; i < ds.Tables[5].Rows.Count; i++)
    //            {
    //                int PickUpTime = 2;
    //                int ScheduleOn = Convert.ToInt32(ds.Tables[5].Rows[i]["ScheduleOn"]);
    //                int DayNo = Convert.ToInt32(ds.Tables[5].Rows[i]["DayNo"]);
    //                if (ScheduleOn == 1)
    //                    PickUpTime = Convert.ToInt32(ds.Tables[5].Rows[i]["PickUpTime"]);

    //                #region Pickup Time 2

    //                if (PickUpTime == 2)
    //                {

    //                    if (DayNo == 1)
    //                    {
    //                        if (ScheduleOn == 1)
    //                        {
    //                            hdnMonday2On.Value = "1";
    //                            Monday2On.Checked = true;
    //                            Monday2OriginalPrice.Value = Convert.ToString(ds.Tables[5].Rows[i]["OriginalPrice"]);
    //                            Monday2NoOfItems.Value = Convert.ToString(ds.Tables[5].Rows[i]["NoOfItems"]);
    //                            //Monday2Discount.Value = Convert.ToString(ds.Tables[5].Rows[i]["DiscountID"]);
    //                            Monday2Discount.Value = Convert.ToDouble(Convert.ToDouble(ds.Tables[5].Rows[i]["OriginalPrice"]) - Convert.ToDouble(ds.Tables[5].Rows[i]["DiscountID"])).ToString("f2");


    //                            if (Convert.ToString(ds.Tables[5].Rows[i]["PickupFromTime"]) != string.Empty)
    //                            {
    //                                Monday2FromTime.Value = Convert.ToDateTime(ds.Tables[5].Rows[i]["PickupFromTime"]).ToString("HH:mm");
    //                            }
    //                            if (Convert.ToString(ds.Tables[5].Rows[i]["PickupToTime"]) != string.Empty)
    //                            {
    //                                Monday2ToTime.Value = Convert.ToDateTime(ds.Tables[5].Rows[i]["PickupToTime"]).ToString("HH:mm");
    //                            }
    //                        }
    //                        else
    //                        {
    //                            Monday2On.Checked = false;
    //                            hdnMonday2On.Value = "0";
    //                        }
    //                    }
    //                    if (DayNo == 2)
    //                    {
    //                        if (ScheduleOn == 1)
    //                        {
    //                            hdnTuesday2On.Value = "1";
    //                            Tuesday2On.Checked = true;
    //                            Tuesday2OriginalPrice.Value = Convert.ToString(ds.Tables[5].Rows[i]["OriginalPrice"]);
    //                            Tuesday2NoOfItems.Value = Convert.ToString(ds.Tables[5].Rows[i]["NoOfItems"]);
    //                            //Tuesday2Discount.Value = Convert.ToString(ds.Tables[5].Rows[i]["DiscountID"]);
    //                            Tuesday2Discount.Value = Convert.ToDouble(Convert.ToDouble(ds.Tables[5].Rows[i]["OriginalPrice"]) - Convert.ToDouble(ds.Tables[5].Rows[i]["DiscountID"])).ToString("f2");
    //                            if (Convert.ToString(ds.Tables[5].Rows[i]["PickupFromTime"]) != string.Empty)
    //                            {
    //                                Tuesday2FromTime.Value = Convert.ToDateTime(ds.Tables[5].Rows[i]["PickupFromTime"]).ToString("HH:mm");
    //                            }
    //                            if (Convert.ToString(ds.Tables[5].Rows[i]["PickupToTime"]) != string.Empty)
    //                            {
    //                                Tuesday2ToTime.Value = Convert.ToDateTime(ds.Tables[5].Rows[i]["PickupToTime"]).ToString("HH:mm");
    //                            }
    //                        }
    //                        else
    //                        {
    //                            Tuesday2On.Checked = false;
    //                            hdnTuesday2On.Value = "0";
    //                        }
    //                    }
    //                    if (DayNo == 3)
    //                    {

    //                        if (ScheduleOn == 1)
    //                        {
    //                            hdnWednesday2On.Value = "1";
    //                            Wednesday2On.Checked = true;
    //                            Wednesday2OriginalPrice.Value = Convert.ToString(ds.Tables[5].Rows[i]["OriginalPrice"]);
    //                            Wednesday2NoOfItems.Value = Convert.ToString(ds.Tables[5].Rows[i]["NoOfItems"]);
    //                            //Wednesday2Discount.Value = Convert.ToString(ds.Tables[5].Rows[i]["DiscountID"]);
    //                            Wednesday2Discount.Value = Convert.ToDouble(Convert.ToDouble(ds.Tables[5].Rows[i]["OriginalPrice"]) - Convert.ToDouble(ds.Tables[5].Rows[i]["DiscountID"])).ToString("f2");
    //                            if (Convert.ToString(ds.Tables[5].Rows[i]["PickupFromTime"]) != string.Empty)
    //                            {
    //                                Wednesday2FromTime.Value = Convert.ToDateTime(ds.Tables[5].Rows[i]["PickupFromTime"]).ToString("HH:mm");
    //                            }
    //                            if (Convert.ToString(ds.Tables[5].Rows[i]["PickupToTime"]) != string.Empty)
    //                            {
    //                                Wednesday2ToTime.Value = Convert.ToDateTime(ds.Tables[5].Rows[i]["PickupToTime"]).ToString("HH:mm");
    //                            }
    //                        }
    //                        else
    //                        {
    //                            Wednesday2On.Checked = false;
    //                            hdnWednesday2On.Value = "0";
    //                        }
    //                    }
    //                    if (DayNo == 4)
    //                    {
    //                        if (ScheduleOn == 1)
    //                        {
    //                            hdnThursday2On.Value = "1";
    //                            Thirsday2On.Checked = true;
    //                            Thirsday2OriginalPrice.Value = Convert.ToString(ds.Tables[5].Rows[i]["OriginalPrice"]);
    //                            Thirsday2NoOfItems.Value = Convert.ToString(ds.Tables[5].Rows[i]["NoOfItems"]);
    //                            //Thirsday2Discount.Value = Convert.ToString(ds.Tables[5].Rows[i]["DiscountID"]);
    //                            Thirsday2Discount.Value = Convert.ToDouble(Convert.ToDouble(ds.Tables[5].Rows[i]["OriginalPrice"]) - Convert.ToDouble(ds.Tables[5].Rows[i]["DiscountID"])).ToString("f2");
    //                            if (Convert.ToString(ds.Tables[5].Rows[i]["PickupFromTime"]) != string.Empty)
    //                            {
    //                                Thirsday2FromTime.Value = Convert.ToDateTime(ds.Tables[5].Rows[i]["PickupFromTime"]).ToString("HH:mm");
    //                            }
    //                            if (Convert.ToString(ds.Tables[5].Rows[i]["PickupToTime"]) != string.Empty)
    //                            {
    //                                Thirsday2ToTime.Value = Convert.ToDateTime(ds.Tables[5].Rows[i]["PickupToTime"]).ToString("HH:mm");
    //                            }
    //                        }
    //                        else
    //                        {
    //                            Thirsday2On.Checked = false;
    //                            hdnThursday2On.Value = "0";
    //                        }
    //                    }
    //                    if (DayNo == 5)
    //                    {
    //                        if (ScheduleOn == 1)
    //                        {
    //                            hdnFriday2On.Value = "1";
    //                            Friday2On.Checked = true;
    //                            Friday2OriginalPrice.Value = Convert.ToString(ds.Tables[5].Rows[i]["OriginalPrice"]);
    //                            Friday2NoOfItems.Value = Convert.ToString(ds.Tables[5].Rows[i]["NoOfItems"]);
    //                            //Friday2Discount.Value = Convert.ToString(ds.Tables[5].Rows[i]["DiscountID"]);
    //                            Friday2Discount.Value = Convert.ToDouble(Convert.ToDouble(ds.Tables[5].Rows[i]["OriginalPrice"]) - Convert.ToDouble(ds.Tables[5].Rows[i]["DiscountID"])).ToString("f2");
    //                            if (Convert.ToString(ds.Tables[5].Rows[i]["PickupFromTime"]) != string.Empty)
    //                            {
    //                                Friday2FromTime.Value = Convert.ToDateTime(ds.Tables[5].Rows[i]["PickupFromTime"]).ToString("HH:mm");
    //                            }
    //                            if (Convert.ToString(ds.Tables[5].Rows[i]["PickupToTime"]) != string.Empty)
    //                            {
    //                                Friday2ToTime.Value = Convert.ToDateTime(ds.Tables[5].Rows[i]["PickupToTime"]).ToString("HH:mm");
    //                            }
    //                        }
    //                        else
    //                        {
    //                            Friday2On.Checked = false;
    //                            hdnFriday2On.Value = "0";
    //                        }
    //                    }
    //                    if (DayNo == 6)
    //                    {
    //                        if (ScheduleOn == 1)
    //                        {
    //                            hdnSaturday2On.Value = "1";
    //                            Saturday2On.Checked = true;
    //                            Saturday2OriginalPrice.Value = Convert.ToString(ds.Tables[5].Rows[i]["OriginalPrice"]);
    //                            Saturday2NoOfItems.Value = Convert.ToString(ds.Tables[5].Rows[i]["NoOfItems"]);
    //                            //Saturday2Discount.Value = Convert.ToString(ds.Tables[5].Rows[i]["DiscountID"]);
    //                            Saturday2Discount.Value = Convert.ToDouble(Convert.ToDouble(ds.Tables[5].Rows[i]["OriginalPrice"]) - Convert.ToDouble(ds.Tables[5].Rows[i]["DiscountID"])).ToString("f2");
    //                            if (Convert.ToString(ds.Tables[5].Rows[i]["PickupFromTime"]) != string.Empty)
    //                            {
    //                                Saturday2FromTime.Value = Convert.ToDateTime(ds.Tables[5].Rows[i]["PickupFromTime"]).ToString("HH:mm");
    //                            }
    //                            if (Convert.ToString(ds.Tables[5].Rows[i]["PickupToTime"]) != string.Empty)
    //                            {
    //                                Saturday2ToTime.Value = Convert.ToDateTime(ds.Tables[5].Rows[i]["PickupToTime"]).ToString("HH:mm");
    //                            }
    //                        }
    //                        else
    //                        {
    //                            Saturday2On.Checked = false;
    //                            hdnSaturday2On.Value = "0";
    //                        }
    //                    }

    //                    if (DayNo == 7)
    //                    {
    //                        if (ScheduleOn == 1)
    //                        {
    //                            hdnSunday2On.Value = "1";
    //                            Sunday2On.Checked = true;
    //                            Sunday2OriginalPrice.Value = Convert.ToString(ds.Tables[5].Rows[i]["OriginalPrice"]);
    //                            Sunday2NoOfItems.Value = Convert.ToString(ds.Tables[5].Rows[i]["NoOfItems"]);
    //                            //Sunday2Discount.Value = Convert.ToString(ds.Tables[5].Rows[i]["DiscountID"]);
    //                            Sunday2Discount.Value = Convert.ToDouble(Convert.ToDouble(ds.Tables[5].Rows[i]["OriginalPrice"]) - Convert.ToDouble(ds.Tables[5].Rows[i]["DiscountID"])).ToString("f2");
    //                            if (Convert.ToString(ds.Tables[5].Rows[i]["PickupFromTime"]) != string.Empty)
    //                            {
    //                                Sunday2FromTime.Value = Convert.ToDateTime(ds.Tables[5].Rows[i]["PickupFromTime"]).ToString("HH:mm");
    //                            }
    //                            if (Convert.ToString(ds.Tables[5].Rows[i]["PickupToTime"]) != string.Empty)
    //                            {
    //                                Sunday2ToTime.Value = Convert.ToDateTime(ds.Tables[5].Rows[i]["PickupToTime"]).ToString("HH:mm");
    //                            }
    //                        }
    //                        else
    //                        {
    //                            Sunday2On.Checked = false;
    //                            hdnSunday2On.Value = "0";
    //                        }
    //                    }
    //                }

    //                #endregion
    //            }
    //        }
    //        if (ds.Tables[6].Rows.Count > 0)
    //        {
    //            for (int i = 0; i < ds.Tables[6].Rows.Count; i++)
    //            {
    //                int PickUpTime = 3;
    //                int ScheduleOn = Convert.ToInt32(ds.Tables[6].Rows[i]["ScheduleOn"]);
    //                int DayNo = Convert.ToInt32(ds.Tables[6].Rows[i]["DayNo"]);
    //                if (ScheduleOn == 1)
    //                    PickUpTime = Convert.ToInt32(ds.Tables[6].Rows[i]["PickUpTime"]);


    //                #region Pickup Time 3

    //                if (PickUpTime == 3)
    //                {

    //                    if (DayNo == 1)
    //                    {
    //                        if (ScheduleOn == 1)
    //                        {
    //                            hdnMonday3On.Value = "1";
    //                            Monday3On.Checked = true;
    //                            Monday3OriginalPrice.Value = Convert.ToString(ds.Tables[6].Rows[i]["OriginalPrice"]);
    //                            Monday3NoOfItems.Value = Convert.ToString(ds.Tables[6].Rows[i]["NoOfItems"]);
    //                            //Monday3Discount.Value = Convert.ToString(ds.Tables[6].Rows[i]["DiscountID"]);
    //                            Monday3Discount.Value = Convert.ToDouble(Convert.ToDouble(ds.Tables[6].Rows[i]["OriginalPrice"]) - Convert.ToDouble(ds.Tables[6].Rows[i]["DiscountID"])).ToString("f2");


    //                            if (Convert.ToString(ds.Tables[6].Rows[i]["PickupFromTime"]) != string.Empty)
    //                            {
    //                                Monday3FromTime.Value = Convert.ToDateTime(ds.Tables[6].Rows[i]["PickupFromTime"]).ToString("HH:mm");
    //                            }
    //                            if (Convert.ToString(ds.Tables[6].Rows[i]["PickupToTime"]) != string.Empty)
    //                            {
    //                                Monday3ToTime.Value = Convert.ToDateTime(ds.Tables[6].Rows[i]["PickupToTime"]).ToString("HH:mm");
    //                            }
    //                        }
    //                        else
    //                        {
    //                            Monday3On.Checked = false;
    //                            hdnMonday3On.Value = "0";
    //                        }
    //                    }
    //                    if (DayNo == 2)
    //                    {
    //                        if (ScheduleOn == 1)
    //                        {
    //                            hdnTuesday3On.Value = "1";
    //                            Tuesday3On.Checked = true;
    //                            Tuesday3OriginalPrice.Value = Convert.ToString(ds.Tables[6].Rows[i]["OriginalPrice"]);
    //                            Tuesday3NoOfItems.Value = Convert.ToString(ds.Tables[6].Rows[i]["NoOfItems"]);
    //                            //Tuesday3Discount.Value = Convert.ToString(ds.Tables[6].Rows[i]["DiscountID"]);
    //                            Tuesday3Discount.Value = Convert.ToDouble(Convert.ToDouble(ds.Tables[6].Rows[i]["OriginalPrice"]) - Convert.ToDouble(ds.Tables[6].Rows[i]["DiscountID"])).ToString("f2");
    //                            if (Convert.ToString(ds.Tables[6].Rows[i]["PickupFromTime"]) != string.Empty)
    //                            {
    //                                Tuesday3FromTime.Value = Convert.ToDateTime(ds.Tables[6].Rows[i]["PickupFromTime"]).ToString("HH:mm");
    //                            }
    //                            if (Convert.ToString(ds.Tables[6].Rows[i]["PickupToTime"]) != string.Empty)
    //                            {
    //                                Tuesday3ToTime.Value = Convert.ToDateTime(ds.Tables[6].Rows[i]["PickupToTime"]).ToString("HH:mm");
    //                            }
    //                        }
    //                        else
    //                        {
    //                            Tuesday3On.Checked = false;
    //                            hdnTuesday3On.Value = "0";
    //                        }
    //                    }
    //                    if (DayNo == 3)
    //                    {

    //                        if (ScheduleOn == 1)
    //                        {
    //                            hdnWednesday3On.Value = "1";
    //                            Wednesday3On.Checked = true;
    //                            Wednesday3OriginalPrice.Value = Convert.ToString(ds.Tables[6].Rows[i]["OriginalPrice"]);
    //                            Wednesday3NoOfItems.Value = Convert.ToString(ds.Tables[6].Rows[i]["NoOfItems"]);
    //                            //Wednesday3Discount.Value = Convert.ToString(ds.Tables[6].Rows[i]["DiscountID"]);
    //                            Wednesday3Discount.Value = Convert.ToDouble(Convert.ToDouble(ds.Tables[6].Rows[i]["OriginalPrice"]) - Convert.ToDouble(ds.Tables[6].Rows[i]["DiscountID"])).ToString("f2");
    //                            if (Convert.ToString(ds.Tables[6].Rows[i]["PickupFromTime"]) != string.Empty)
    //                            {
    //                                Wednesday3FromTime.Value = Convert.ToDateTime(ds.Tables[6].Rows[i]["PickupFromTime"]).ToString("HH:mm");
    //                            }
    //                            if (Convert.ToString(ds.Tables[6].Rows[i]["PickupToTime"]) != string.Empty)
    //                            {
    //                                Wednesday3ToTime.Value = Convert.ToDateTime(ds.Tables[6].Rows[i]["PickupToTime"]).ToString("HH:mm");
    //                            }
    //                        }
    //                        else
    //                        {
    //                            Wednesday3On.Checked = false;
    //                            hdnWednesday3On.Value = "0";
    //                        }
    //                    }
    //                    if (DayNo == 4)
    //                    {
    //                        if (ScheduleOn == 1)
    //                        {
    //                            hdnThursday3On.Value = "1";
    //                            Thirsday3On.Checked = true;
    //                            Thirsday3OriginalPrice.Value = Convert.ToString(ds.Tables[6].Rows[i]["OriginalPrice"]);
    //                            Thirsday3NoOfItems.Value = Convert.ToString(ds.Tables[6].Rows[i]["NoOfItems"]);
    //                            //Thirsday3Discount.Value = Convert.ToString(ds.Tables[6].Rows[i]["DiscountID"]);
    //                            Thirsday3Discount.Value = Convert.ToDouble(Convert.ToDouble(ds.Tables[6].Rows[i]["OriginalPrice"]) - Convert.ToDouble(ds.Tables[6].Rows[i]["DiscountID"])).ToString("f2");
    //                            if (Convert.ToString(ds.Tables[6].Rows[i]["PickupFromTime"]) != string.Empty)
    //                            {
    //                                Thirsday3FromTime.Value = Convert.ToDateTime(ds.Tables[6].Rows[i]["PickupFromTime"]).ToString("HH:mm");
    //                            }
    //                            if (Convert.ToString(ds.Tables[6].Rows[i]["PickupToTime"]) != string.Empty)
    //                            {
    //                                Thirsday3ToTime.Value = Convert.ToDateTime(ds.Tables[6].Rows[i]["PickupToTime"]).ToString("HH:mm");
    //                            }
    //                        }
    //                        else
    //                        {
    //                            Thirsday3On.Checked = false;
    //                            hdnThursday3On.Value = "0";
    //                        }
    //                    }
    //                    if (DayNo == 5)
    //                    {
    //                        if (ScheduleOn == 1)
    //                        {
    //                            hdnFriday3On.Value = "1";
    //                            Friday3On.Checked = true;
    //                            Friday3OriginalPrice.Value = Convert.ToString(ds.Tables[6].Rows[i]["OriginalPrice"]);
    //                            Friday3NoOfItems.Value = Convert.ToString(ds.Tables[6].Rows[i]["NoOfItems"]);
    //                            //Friday3Discount.Value = Convert.ToString(ds.Tables[6].Rows[i]["DiscountID"]);
    //                            Friday3Discount.Value = Convert.ToDouble(Convert.ToDouble(ds.Tables[6].Rows[i]["OriginalPrice"]) - Convert.ToDouble(ds.Tables[6].Rows[i]["DiscountID"])).ToString("f2");
    //                            if (Convert.ToString(ds.Tables[6].Rows[i]["PickupFromTime"]) != string.Empty)
    //                            {
    //                                Friday3FromTime.Value = Convert.ToDateTime(ds.Tables[6].Rows[i]["PickupFromTime"]).ToString("HH:mm");
    //                            }
    //                            if (Convert.ToString(ds.Tables[6].Rows[i]["PickupToTime"]) != string.Empty)
    //                            {
    //                                Friday3ToTime.Value = Convert.ToDateTime(ds.Tables[6].Rows[i]["PickupToTime"]).ToString("HH:mm");
    //                            }
    //                        }
    //                        else
    //                        {
    //                            Friday3On.Checked = false;
    //                            hdnFriday3On.Value = "0";
    //                        }
    //                    }
    //                    if (DayNo == 6)
    //                    {
    //                        if (ScheduleOn == 1)
    //                        {
    //                            hdnSaturday3On.Value = "1";
    //                            Saturday3On.Checked = true;
    //                            Saturday3OriginalPrice.Value = Convert.ToString(ds.Tables[6].Rows[i]["OriginalPrice"]);
    //                            Saturday3NoOfItems.Value = Convert.ToString(ds.Tables[6].Rows[i]["NoOfItems"]);
    //                            //Saturday3Discount.Value = Convert.ToString(ds.Tables[6].Rows[i]["DiscountID"]);
    //                            Saturday3Discount.Value = Convert.ToDouble(Convert.ToDouble(ds.Tables[6].Rows[i]["OriginalPrice"]) - Convert.ToDouble(ds.Tables[6].Rows[i]["DiscountID"])).ToString("f2");
    //                            if (Convert.ToString(ds.Tables[6].Rows[i]["PickupFromTime"]) != string.Empty)
    //                            {
    //                                Saturday3FromTime.Value = Convert.ToDateTime(ds.Tables[6].Rows[i]["PickupFromTime"]).ToString("HH:mm");
    //                            }
    //                            if (Convert.ToString(ds.Tables[6].Rows[i]["PickupToTime"]) != string.Empty)
    //                            {
    //                                Saturday3ToTime.Value = Convert.ToDateTime(ds.Tables[6].Rows[i]["PickupToTime"]).ToString("HH:mm");
    //                            }
    //                        }
    //                        else
    //                        {
    //                            Saturday3On.Checked = false;
    //                            hdnSaturday3On.Value = "0";
    //                        }
    //                    }

    //                    if (DayNo == 7)
    //                    {
    //                        if (ScheduleOn == 1)
    //                        {
    //                            hdnSunday3On.Value = "1";
    //                            Sunday3On.Checked = true;
    //                            Sunday3OriginalPrice.Value = Convert.ToString(ds.Tables[6].Rows[i]["OriginalPrice"]);
    //                            Sunday3NoOfItems.Value = Convert.ToString(ds.Tables[6].Rows[i]["NoOfItems"]);
    //                            //Sunday3Discount.Value = Convert.ToString(ds.Tables[6].Rows[i]["DiscountID"]);
    //                            Sunday3Discount.Value = Convert.ToDouble(Convert.ToDouble(ds.Tables[6].Rows[i]["OriginalPrice"]) - Convert.ToDouble(ds.Tables[6].Rows[i]["DiscountID"])).ToString("f2");
    //                            if (Convert.ToString(ds.Tables[6].Rows[i]["PickupFromTime"]) != string.Empty)
    //                            {
    //                                Sunday3FromTime.Value = Convert.ToDateTime(ds.Tables[6].Rows[i]["PickupFromTime"]).ToString("HH:mm");
    //                            }
    //                            if (Convert.ToString(ds.Tables[6].Rows[i]["PickupToTime"]) != string.Empty)
    //                            {
    //                                Sunday3ToTime.Value = Convert.ToDateTime(ds.Tables[6].Rows[i]["PickupToTime"]).ToString("HH:mm");
    //                            }
    //                        }
    //                        else
    //                        {
    //                            Sunday3On.Checked = false;
    //                            hdnSunday3On.Value = "0";
    //                        }
    //                    }
    //                }

    //                #endregion

    //            }
    //        }


    //        //objBusinessBAL.Password = Utility.Security.EncryptDescrypt.EncryptString(Convert.ToString(Request[txtPassword.UniqueID]));
    //    }

    //    //DataTable dtblAdminInfo = new DataTable();
    //    //objSalesAdminBAL.ID = ID;
    //    //dtblAdminInfo = objSalesAdminBAL.GetByID();
    //    //if (dtblAdminInfo.Rows.Count > 0)
    //    //{
    //    //    txtFirstName.Value = Convert.ToString(dtblAdminInfo.Rows[0]["FullName"]).Trim();
    //    //    txtEmail.Value = Convert.ToString(dtblAdminInfo.Rows[0]["EmailAddress"]).Trim();
    //    //    txtphone.Value = Convert.ToString(dtblAdminInfo.Rows[0]["Mobile"]).Trim();
    //    //    trConfirmPwd.Visible = false;
    //    //    divlblPwd.Visible = true;
    //    //    divPwd.Style.Add("display", "none");
    //    //    string strPwd = Utility.Security.EncryptDescrypt.DecryptString(Convert.ToString(dtblAdminInfo.Rows[0]["Password"]));
    //    //    hdnpwd.Value = Convert.ToString(dtblAdminInfo.Rows[0]["Password"]).Trim();
    //    //    lblPassword.InnerText = strPwd;
    //    //    txtPassword.Attributes.Add("value", strPwd);
    //    //    txtPassword.Attributes.Add("type", "text");            

    //    //}         
    //}
    #endregion

    #region Save Information
    private void SaveInfo()
    {
        //objBusinessBAL.FullName = Convert.ToString(Request[txtFullName.UniqueID]);
        //objBusinessBAL.BusinessPhone = Convert.ToString(Request[txtBusinessPhone.UniqueID]);
        //objBusinessBAL.Mobile = Convert.ToString(Request[txtphone.UniqueID]);
        // objBusinessBAL.EmailAddress = Convert.ToString(Request[txtEmail.UniqueID]);
        //objBusinessBAL.ProfilePhotoID = Convert.ToInt32(Request["ProfilePhotoID"]);
        //objBusinessBAL.Description = Convert.ToString(Request[tareaDescription.UniqueID]);

        System.Text.StringBuilder sbTemplate = new System.Text.StringBuilder(Server.HtmlDecode(Request[hdnContent.UniqueID]));
        sbTemplate.Replace(Config.WebSiteUrl, "{%WebSiteUrl%}");
        //objBusinessBAL.Description = sbTemplate.ToString();

        //objBusinessBAL.BSBNo = Convert.ToString(Request[txtBSBNo.UniqueID]);
        //objBusinessBAL.AccountNumber = Convert.ToString(Request[txtAccountNo.UniqueID]);
        //objBusinessBAL.BankName = Convert.ToString(Request[txtBankName.UniqueID]);
        //objBusinessBAL.AccountName = Convert.ToString(Request[txtAccountName.UniqueID]);
        //objBusinessBAL.Status = Convert.ToInt32(Request["Status"]);
        //objBusinessBAL.BusinessType = Convert.ToString(Request[hdnCategory.UniqueID]);
        //objBusinessBAL.FoodItems = Convert.ToString(Request[hdnFoodItems.UniqueID]);
        //objBusinessBAL.RestaurantTypes = Convert.ToString(Request[hdnRestaurantTypes.UniqueID]);
        //  objBusinessBAL.Password = Utility.Security.EncryptDescrypt.EncryptString(Convert.ToString(Request[txtPassword.UniqueID]));


        // objBusinessBAL.Latitude = Convert.ToString(Request["Latitude"]);
        //objBusinessBAL.Longitude = Convert.ToString(Request["Longitude"]);
        //objBusinessBAL.PostCode = Convert.ToString(Request["PostCode"]);



        #region Pickup Time 1

        string CurrentDaySchedule = string.Empty, TuesdaySchedule = string.Empty, WednesdaySchedule = string.Empty, ThirsdaySchedule = string.Empty, FridaySchedule = string.Empty, SaturdaySchedule = string.Empty, SundaySchedule = string.Empty;

        //if (Request[MondayOn.UniqueID] != null)
        //{
        //    if (Convert.ToInt32(Request[MondayOn.UniqueID]) == 1)
        //    {
        //        string MDiscount = "0.00";
        //        if (Convert.ToString(Request[MondayDiscount.UniqueID]) != string.Empty && Convert.ToString(Request[MondayOriginalPrice.UniqueID]) != string.Empty)
        //        {
        //            MDiscount = Convert.ToDouble(Convert.ToDouble(Request[MondayOriginalPrice.UniqueID]) - Convert.ToDouble(Request[MondayDiscount.UniqueID])).ToString("f2");
        //        }
        //        CurrentDaySchedule = Convert.ToString(Request[MondayNoOfItems.UniqueID]) + "##" + Convert.ToString(Request[MondayOriginalPrice.UniqueID]) + "##" + Convert.ToString(MDiscount) + "##01/01/1990 " + Convert.ToString(Request[MondayFromTime.UniqueID]) + "##01/01/1990 " + Convert.ToString(Request[MondayToTime.UniqueID]);
        //    }
        //}
        //objBusinessBAL.CurrentDaySchedule = CurrentDaySchedule;


        string MondaySchedule = string.Empty;

        //if (Request[MondayOn.UniqueID] != null)
        //{
        //    if (Convert.ToInt32(Request[MondayOn.UniqueID]) == 1)
        //    {
        //        string MDiscount = "0.00";
        //        if (Convert.ToString(Request[MondayDiscount.UniqueID]) != string.Empty && Convert.ToString(Request[MondayOriginalPrice.UniqueID]) != string.Empty)
        //        {
        //            MDiscount = Convert.ToDouble(Convert.ToDouble(Request[MondayOriginalPrice.UniqueID]) - Convert.ToDouble(Request[MondayDiscount.UniqueID])).ToString("f2");
        //        }
        //        MondaySchedule = Convert.ToString(Request[MondayNoOfItems.UniqueID]) + "##" + Convert.ToString(Request[MondayOriginalPrice.UniqueID]) + "##" + Convert.ToString(MDiscount) + "##01/01/1990 " + Convert.ToString(Request[MondayFromTime.UniqueID]) + "##01/01/1990 " + Convert.ToString(Request[MondayToTime.UniqueID]);
        //    }
        //}
        //objBusinessBAL.MondaySchedule = MondaySchedule;



        //if (Request[TuesdayOn.UniqueID] != null)
        //{
        //    if (Convert.ToInt32(Request[TuesdayOn.UniqueID]) == 1)
        //    {
        //        string TDiscount = "0.00";
        //        if (Convert.ToString(Request[TuesdayDiscount.UniqueID]) != string.Empty && Convert.ToString(Request[TuesdayOriginalPrice.UniqueID]) != string.Empty)
        //        {
        //            TDiscount = Convert.ToDouble(Convert.ToDouble(Request[TuesdayOriginalPrice.UniqueID]) - Convert.ToDouble(Request[TuesdayDiscount.UniqueID])).ToString("f2");
        //        }
        //        TuesdaySchedule = Convert.ToString(Request[TuesdayNoOfItems.UniqueID]) + "##" + Convert.ToString(Request[TuesdayOriginalPrice.UniqueID]) + "##" + Convert.ToString(TDiscount) + "##01/01/1990 " + Convert.ToString(Request[TuesdayFromTime.UniqueID]) + "##01/01/1990 " + Convert.ToString(Request[TuesdayToTime.UniqueID]);
        //    }
        //}

        //objBusinessBAL.TuesdaySchedule = TuesdaySchedule;
        //if (Request[WednesdayOn.UniqueID] != null)
        //{
        //    if (Convert.ToInt32(Request[WednesdayOn.UniqueID]) == 1)
        //    {
        //        string WDiscount = "0.00";
        //        if (Convert.ToString(Request[WednesdayDiscount.UniqueID]) != string.Empty && Convert.ToString(Request[WednesdayOriginalPrice.UniqueID]) != string.Empty)
        //        {
        //            WDiscount = Convert.ToDouble(Convert.ToDouble(Request[WednesdayOriginalPrice.UniqueID]) - Convert.ToDouble(Request[WednesdayDiscount.UniqueID])).ToString("f2");
        //        }
        //        WednesdaySchedule = Convert.ToString(Request[WednesdayNoOfItems.UniqueID]) + "##" + Convert.ToString(Request[WednesdayOriginalPrice.UniqueID]) + "##" + Convert.ToString(WDiscount) + "##01/01/1990 " + Convert.ToString(Request[WednesdayFromTime.UniqueID]) + "##01/01/1990 " + Convert.ToString(Request[WednesdayToTime.UniqueID]);
        //    }
        //}

        //objBusinessBAL.WednesdaySchedule = WednesdaySchedule;
        //if (Request[ThirsdayOn.UniqueID] != null)
        //{
        //    if (Convert.ToInt32(Request[ThirsdayOn.UniqueID]) == 1)
        //    {
        //        string THDiscount = "0.00";
        //        if (Convert.ToString(Request[ThirsdayDiscount.UniqueID]) != string.Empty && Convert.ToString(Request[ThirsdayOriginalPrice.UniqueID]) != string.Empty)
        //        {
        //            THDiscount = Convert.ToDouble(Convert.ToDouble(Request[ThirsdayOriginalPrice.UniqueID]) - Convert.ToDouble(Request[ThirsdayDiscount.UniqueID])).ToString("f2");
        //        }
        //        ThirsdaySchedule = Convert.ToString(Request[ThirsdayNoOfItems.UniqueID]) + "##" + Convert.ToString(Request[ThirsdayOriginalPrice.UniqueID]) + "##" + Convert.ToString(THDiscount) + "##01/01/1990 " + Convert.ToString(Request[ThirsdayFromTime.UniqueID]) + "##01/01/1990 " + Convert.ToString(Request[ThirsdayToTime.UniqueID]);
        //    }
        //}

        //objBusinessBAL.ThirsdaySchedule = ThirsdaySchedule;
        //if (Request[FridayOn.UniqueID] != null)
        //{
        //    if (Convert.ToInt32(Request[FridayOn.UniqueID]) == 1)
        //    {
        //        string FDiscount = "0.00";
        //        if (Convert.ToString(Request[FridayDiscount.UniqueID]) != string.Empty && Convert.ToString(Request[FridayOriginalPrice.UniqueID]) != string.Empty)
        //        {
        //            FDiscount = Convert.ToDouble(Convert.ToDouble(Request[FridayOriginalPrice.UniqueID]) - Convert.ToDouble(Request[FridayDiscount.UniqueID])).ToString("f2");
        //        }
        //        FridaySchedule = Convert.ToString(Request[FridayNoOfItems.UniqueID]) + "##" + Convert.ToString(Request[FridayOriginalPrice.UniqueID]) + "##" + Convert.ToString(FDiscount) + "##01/01/1990 " + Convert.ToString(Request[FridayFromTime.UniqueID]) + "##01/01/1990 " + Convert.ToString(Request[FridayToTime.UniqueID]);
        //    }
        //}

        //objBusinessBAL.FridaySchedule = FridaySchedule;
        //if (Request[SaturdayOn.UniqueID] != null)
        //{
        //    if (Convert.ToInt32(Request[SaturdayOn.UniqueID]) == 1)
        //    {
        //        string SADiscount = "0.00";
        //        if (Convert.ToString(Request[SaturdayDiscount.UniqueID]) != string.Empty && Convert.ToString(Request[SaturdayOriginalPrice.UniqueID]) != string.Empty)
        //        {
        //            SADiscount = Convert.ToDouble(Convert.ToDouble(Request[SaturdayOriginalPrice.UniqueID]) - Convert.ToDouble(Request[SaturdayDiscount.UniqueID])).ToString("f2");
        //        }
        //        SaturdaySchedule = Convert.ToString(Request[SaturdayNoOfItems.UniqueID]) + "##" + Convert.ToString(Request[SaturdayOriginalPrice.UniqueID]) + "##" + Convert.ToString(SADiscount) + "##01/01/1990 " + Convert.ToString(Request[SaturdayFromTime.UniqueID]) + "##01/01/1990 " + Convert.ToString(Request[SaturdayToTime.UniqueID]);
        //    }
        //}

        //objBusinessBAL.SaturdaySchedule = SaturdaySchedule;
        //if (Request[SundayOn.UniqueID] != null)
        //{
        //    if (Convert.ToInt32(Request[SundayOn.UniqueID]) == 1)
        //    {
        //        string SUDiscount = "0.00";
        //        if (Convert.ToString(Request[SundayDiscount.UniqueID]) != string.Empty && Convert.ToString(Request[SundayOriginalPrice.UniqueID]) != string.Empty)
        //        {
        //            SUDiscount = Convert.ToDouble(Convert.ToDouble(Request[SundayOriginalPrice.UniqueID]) - Convert.ToDouble(Request[SundayDiscount.UniqueID])).ToString("f2");
        //        }
        //        SundaySchedule = Convert.ToString(Request[SundayNoOfItems.UniqueID]) + "##" + Convert.ToString(Request[SundayOriginalPrice.UniqueID]) + "##" + Convert.ToString(SUDiscount) + "##01/01/1990 " + Convert.ToString(Request[SundayFromTime.UniqueID]) + "##01/01/1990 " + Convert.ToString(Request[SundayToTime.UniqueID]);
        //    }
        //}
        //objBusinessBAL.SundaySchedule = SundaySchedule;

        #endregion


        #region Pickup Time 2
        string Monday2Schedule = string.Empty, Tuesday2Schedule = string.Empty, Wednesday2Schedule = string.Empty, Thirsday2Schedule = string.Empty, Friday2Schedule = string.Empty, Saturday2Schedule = string.Empty, Sunday2Schedule = string.Empty;

        //if (Request[Monday2On.UniqueID] != null)
        //{
        //    if (Convert.ToInt32(Request[Monday2On.UniqueID]) == 1)
        //    {
        //        string MDiscount = "0.00";
        //        if (Convert.ToString(Request[Monday2Discount.UniqueID]) != string.Empty && Convert.ToString(Request[Monday2OriginalPrice.UniqueID]) != string.Empty)
        //        {
        //            MDiscount = Convert.ToDouble(Convert.ToDouble(Request[Monday2OriginalPrice.UniqueID]) - Convert.ToDouble(Request[Monday2Discount.UniqueID])).ToString("f2");
        //        }
        //        Monday2Schedule = Convert.ToString(Request[Monday2NoOfItems.UniqueID]) + "##" + Convert.ToString(Request[Monday2OriginalPrice.UniqueID]) + "##" + Convert.ToString(MDiscount) + "##01/01/1990 " + Convert.ToString(Request[Monday2FromTime.UniqueID]) + "##01/01/1990 " + Convert.ToString(Request[Monday2ToTime.UniqueID]);
        //    }
        //}
        //objBusinessBAL.Monday2Schedule = Monday2Schedule;



        //if (Request[Tuesday2On.UniqueID] != null)
        //{
        //    if (Convert.ToInt32(Request[Tuesday2On.UniqueID]) == 1)
        //    {
        //        string TDiscount = "0.00";
        //        if (Convert.ToString(Request[Tuesday2Discount.UniqueID]) != string.Empty && Convert.ToString(Request[Tuesday2OriginalPrice.UniqueID]) != string.Empty)
        //        {
        //            TDiscount = Convert.ToDouble(Convert.ToDouble(Request[Tuesday2OriginalPrice.UniqueID]) - Convert.ToDouble(Request[Tuesday2Discount.UniqueID])).ToString("f2");
        //        }
        //        Tuesday2Schedule = Convert.ToString(Request[Tuesday2NoOfItems.UniqueID]) + "##" + Convert.ToString(Request[Tuesday2OriginalPrice.UniqueID]) + "##" + Convert.ToString(TDiscount) + "##01/01/1990 " + Convert.ToString(Request[Tuesday2FromTime.UniqueID]) + "##01/01/1990 " + Convert.ToString(Request[Tuesday2ToTime.UniqueID]);
        //    }
        //}

        //objBusinessBAL.Tuesday2Schedule = Tuesday2Schedule;
        //if (Request[Wednesday2On.UniqueID] != null)
        //{
        //    if (Convert.ToInt32(Request[Wednesday2On.UniqueID]) == 1)
        //    {
        //        string WDiscount = "0.00";
        //        if (Convert.ToString(Request[Wednesday2Discount.UniqueID]) != string.Empty && Convert.ToString(Request[Wednesday2OriginalPrice.UniqueID]) != string.Empty)
        //        {
        //            WDiscount = Convert.ToDouble(Convert.ToDouble(Request[Wednesday2OriginalPrice.UniqueID]) - Convert.ToDouble(Request[Wednesday2Discount.UniqueID])).ToString("f2");
        //        }
        //        Wednesday2Schedule = Convert.ToString(Request[Wednesday2NoOfItems.UniqueID]) + "##" + Convert.ToString(Request[Wednesday2OriginalPrice.UniqueID]) + "##" + Convert.ToString(WDiscount) + "##01/01/1990 " + Convert.ToString(Request[Wednesday2FromTime.UniqueID]) + "##01/01/1990 " + Convert.ToString(Request[Wednesday2ToTime.UniqueID]);
        //    }
        //}

        //objBusinessBAL.Wednesday2Schedule = Wednesday2Schedule;
        //if (Request[Thirsday2On.UniqueID] != null)
        //{
        //    if (Convert.ToInt32(Request[Thirsday2On.UniqueID]) == 1)
        //    {
        //        string THDiscount = "0.00";
        //        if (Convert.ToString(Request[Thirsday2Discount.UniqueID]) != string.Empty && Convert.ToString(Request[Thirsday2OriginalPrice.UniqueID]) != string.Empty)
        //        {
        //            THDiscount = Convert.ToDouble(Convert.ToDouble(Request[Thirsday2OriginalPrice.UniqueID]) - Convert.ToDouble(Request[Thirsday2Discount.UniqueID])).ToString("f2");
        //        }
        //        Thirsday2Schedule = Convert.ToString(Request[Thirsday2NoOfItems.UniqueID]) + "##" + Convert.ToString(Request[Thirsday2OriginalPrice.UniqueID]) + "##" + Convert.ToString(THDiscount) + "##01/01/1990 " + Convert.ToString(Request[Thirsday2FromTime.UniqueID]) + "##01/01/1990 " + Convert.ToString(Request[Thirsday2ToTime.UniqueID]);
        //    }
        //}

        //objBusinessBAL.Thirsday2Schedule = Thirsday2Schedule;
        //if (Request[Friday2On.UniqueID] != null)
        //{
        //    if (Convert.ToInt32(Request[Friday2On.UniqueID]) == 1)
        //    {
        //        string FDiscount = "0.00";
        //        if (Convert.ToString(Request[Friday2Discount.UniqueID]) != string.Empty && Convert.ToString(Request[Friday2OriginalPrice.UniqueID]) != string.Empty)
        //        {
        //            FDiscount = Convert.ToDouble(Convert.ToDouble(Request[Friday2OriginalPrice.UniqueID]) - Convert.ToDouble(Request[Friday2Discount.UniqueID])).ToString("f2");
        //        }
        //        Friday2Schedule = Convert.ToString(Request[Friday2NoOfItems.UniqueID]) + "##" + Convert.ToString(Request[Friday2OriginalPrice.UniqueID]) + "##" + Convert.ToString(FDiscount) + "##01/01/1990 " + Convert.ToString(Request[Friday2FromTime.UniqueID]) + "##01/01/1990 " + Convert.ToString(Request[Friday2ToTime.UniqueID]);
        //    }
        //}

        //objBusinessBAL.Friday2Schedule = Friday2Schedule;
        //if (Request[Saturday2On.UniqueID] != null)
        //{
        //    if (Convert.ToInt32(Request[Saturday2On.UniqueID]) == 1)
        //    {
        //        string SADiscount = "0.00";
        //        if (Convert.ToString(Request[Saturday2Discount.UniqueID]) != string.Empty && Convert.ToString(Request[Saturday2OriginalPrice.UniqueID]) != string.Empty)
        //        {
        //            SADiscount = Convert.ToDouble(Convert.ToDouble(Request[Saturday2OriginalPrice.UniqueID]) - Convert.ToDouble(Request[Saturday2Discount.UniqueID])).ToString("f2");
        //        }
        //        Saturday2Schedule = Convert.ToString(Request[Saturday2NoOfItems.UniqueID]) + "##" + Convert.ToString(Request[Saturday2OriginalPrice.UniqueID]) + "##" + Convert.ToString(SADiscount) + "##01/01/1990 " + Convert.ToString(Request[Saturday2FromTime.UniqueID]) + "##01/01/1990 " + Convert.ToString(Request[Saturday2ToTime.UniqueID]);
        //    }
        //}

        //objBusinessBAL.Saturday2Schedule = Saturday2Schedule;
        //if (Request[Sunday2On.UniqueID] != null)
        //{
        //    if (Convert.ToInt32(Request[Sunday2On.UniqueID]) == 1)
        //    {
        //        string SUDiscount = "0.00";
        //        if (Convert.ToString(Request[Sunday2Discount.UniqueID]) != string.Empty && Convert.ToString(Request[Sunday2OriginalPrice.UniqueID]) != string.Empty)
        //        {
        //            SUDiscount = Convert.ToDouble(Convert.ToDouble(Request[Sunday2OriginalPrice.UniqueID]) - Convert.ToDouble(Request[Sunday2Discount.UniqueID])).ToString("f2");
        //        }
        //        Sunday2Schedule = Convert.ToString(Request[Sunday2NoOfItems.UniqueID]) + "##" + Convert.ToString(Request[Sunday2OriginalPrice.UniqueID]) + "##" + Convert.ToString(SUDiscount) + "##01/01/1990 " + Convert.ToString(Request[Sunday2FromTime.UniqueID]) + "##01/01/1990 " + Convert.ToString(Request[Sunday2ToTime.UniqueID]);
        //    }
        //}
        //objBusinessBAL.Sunday2Schedule = Sunday2Schedule;

        #endregion

        #region Pickup Time 3
        string Monday3Schedule = string.Empty, Tuesday3Schedule = string.Empty, Wednesday3Schedule = string.Empty, Thirsday3Schedule = string.Empty, Friday3Schedule = string.Empty, Saturday3Schedule = string.Empty, Sunday3Schedule = string.Empty;

        //if (Request[Monday3On.UniqueID] != null)
        //{
        //    if (Convert.ToInt32(Request[Monday3On.UniqueID]) == 1)
        //    {
        //        string MDiscount = "0.00";
        //        if (Convert.ToString(Request[Monday3Discount.UniqueID]) != string.Empty && Convert.ToString(Request[Monday3OriginalPrice.UniqueID]) != string.Empty)
        //        {
        //            MDiscount = Convert.ToDouble(Convert.ToDouble(Request[Monday3OriginalPrice.UniqueID]) - Convert.ToDouble(Request[Monday3Discount.UniqueID])).ToString("f2");
        //        }
        //        Monday3Schedule = Convert.ToString(Request[Monday3NoOfItems.UniqueID]) + "##" + Convert.ToString(Request[Monday3OriginalPrice.UniqueID]) + "##" + Convert.ToString(MDiscount) + "##01/01/1990 " + Convert.ToString(Request[Monday3FromTime.UniqueID]) + "##01/01/1990 " + Convert.ToString(Request[Monday3ToTime.UniqueID]);
        //    }
        //}
        //objBusinessBAL.Monday3Schedule = Monday3Schedule;



        //if (Request[Tuesday3On.UniqueID] != null)
        //{
        //    if (Convert.ToInt32(Request[Tuesday3On.UniqueID]) == 1)
        //    {
        //        string TDiscount = "0.00";
        //        if (Convert.ToString(Request[Tuesday3Discount.UniqueID]) != string.Empty && Convert.ToString(Request[Tuesday3OriginalPrice.UniqueID]) != string.Empty)
        //        {
        //            TDiscount = Convert.ToDouble(Convert.ToDouble(Request[Tuesday3OriginalPrice.UniqueID]) - Convert.ToDouble(Request[Tuesday3Discount.UniqueID])).ToString("f2");
        //        }
        //        Tuesday3Schedule = Convert.ToString(Request[Tuesday3NoOfItems.UniqueID]) + "##" + Convert.ToString(Request[Tuesday3OriginalPrice.UniqueID]) + "##" + Convert.ToString(TDiscount) + "##01/01/1990 " + Convert.ToString(Request[Tuesday3FromTime.UniqueID]) + "##01/01/1990 " + Convert.ToString(Request[Tuesday3ToTime.UniqueID]);
        //    }
        //}

        //objBusinessBAL.Tuesday3Schedule = Tuesday3Schedule;
        //if (Request[Wednesday3On.UniqueID] != null)
        //{
        //    if (Convert.ToInt32(Request[Wednesday3On.UniqueID]) == 1)
        //    {
        //        string WDiscount = "0.00";
        //        if (Convert.ToString(Request[Wednesday3Discount.UniqueID]) != string.Empty && Convert.ToString(Request[Wednesday3OriginalPrice.UniqueID]) != string.Empty)
        //        {
        //            WDiscount = Convert.ToDouble(Convert.ToDouble(Request[Wednesday3OriginalPrice.UniqueID]) - Convert.ToDouble(Request[Wednesday3Discount.UniqueID])).ToString("f2");
        //        }
        //        Wednesday3Schedule = Convert.ToString(Request[Wednesday3NoOfItems.UniqueID]) + "##" + Convert.ToString(Request[Wednesday3OriginalPrice.UniqueID]) + "##" + Convert.ToString(WDiscount) + "##01/01/1990 " + Convert.ToString(Request[Wednesday3FromTime.UniqueID]) + "##01/01/1990 " + Convert.ToString(Request[Wednesday3ToTime.UniqueID]);
        //    }
        //}

        //objBusinessBAL.Wednesday3Schedule = Wednesday3Schedule;
        //if (Request[Thirsday3On.UniqueID] != null)
        //{
        //    if (Convert.ToInt32(Request[Thirsday3On.UniqueID]) == 1)
        //    {
        //        string THDiscount = "0.00";
        //        if (Convert.ToString(Request[Thirsday3Discount.UniqueID]) != string.Empty && Convert.ToString(Request[Thirsday3OriginalPrice.UniqueID]) != string.Empty)
        //        {
        //            THDiscount = Convert.ToDouble(Convert.ToDouble(Request[Thirsday3OriginalPrice.UniqueID]) - Convert.ToDouble(Request[Thirsday3Discount.UniqueID])).ToString("f2");
        //        }
        //        Thirsday3Schedule = Convert.ToString(Request[Thirsday3NoOfItems.UniqueID]) + "##" + Convert.ToString(Request[Thirsday3OriginalPrice.UniqueID]) + "##" + Convert.ToString(THDiscount) + "##01/01/1990 " + Convert.ToString(Request[Thirsday3FromTime.UniqueID]) + "##01/01/1990 " + Convert.ToString(Request[Thirsday3ToTime.UniqueID]);
        //    }
        //}

        //objBusinessBAL.Thirsday3Schedule = Thirsday3Schedule;
        //if (Request[Friday3On.UniqueID] != null)
        //{
        //    if (Convert.ToInt32(Request[Friday3On.UniqueID]) == 1)
        //    {
        //        string FDiscount = "0.00";
        //        if (Convert.ToString(Request[Friday3Discount.UniqueID]) != string.Empty && Convert.ToString(Request[Friday3OriginalPrice.UniqueID]) != string.Empty)
        //        {
        //            FDiscount = Convert.ToDouble(Convert.ToDouble(Request[Friday3OriginalPrice.UniqueID]) - Convert.ToDouble(Request[Friday3Discount.UniqueID])).ToString("f2");
        //        }
        //        Friday3Schedule = Convert.ToString(Request[Friday3NoOfItems.UniqueID]) + "##" + Convert.ToString(Request[Friday3OriginalPrice.UniqueID]) + "##" + Convert.ToString(FDiscount) + "##01/01/1990 " + Convert.ToString(Request[Friday3FromTime.UniqueID]) + "##01/01/1990 " + Convert.ToString(Request[Friday3ToTime.UniqueID]);
        //    }
        //}

        //objBusinessBAL.Friday3Schedule = Friday3Schedule;
        //if (Request[Saturday3On.UniqueID] != null)
        //{
        //    if (Convert.ToInt32(Request[Saturday3On.UniqueID]) == 1)
        //    {
        //        string SADiscount = "0.00";
        //        if (Convert.ToString(Request[Saturday3Discount.UniqueID]) != string.Empty && Convert.ToString(Request[Saturday3OriginalPrice.UniqueID]) != string.Empty)
        //        {
        //            SADiscount = Convert.ToDouble(Convert.ToDouble(Request[Saturday3OriginalPrice.UniqueID]) - Convert.ToDouble(Request[Saturday3Discount.UniqueID])).ToString("f2");
        //        }
        //        Saturday3Schedule = Convert.ToString(Request[Saturday3NoOfItems.UniqueID]) + "##" + Convert.ToString(Request[Saturday3OriginalPrice.UniqueID]) + "##" + Convert.ToString(SADiscount) + "##01/01/1990 " + Convert.ToString(Request[Saturday3FromTime.UniqueID]) + "##01/01/1990 " + Convert.ToString(Request[Saturday3ToTime.UniqueID]);
        //    }
        //}

        //objBusinessBAL.Saturday3Schedule = Saturday3Schedule;
        //if (Request[Sunday3On.UniqueID] != null)
        //{
        //    if (Convert.ToInt32(Request[Sunday3On.UniqueID]) == 1)
        //    {
        //        string SUDiscount = "0.00";
        //        if (Convert.ToString(Request[Sunday3Discount.UniqueID]) != string.Empty && Convert.ToString(Request[Sunday3OriginalPrice.UniqueID]) != string.Empty)
        //        {
        //            SUDiscount = Convert.ToDouble(Convert.ToDouble(Request[Sunday3OriginalPrice.UniqueID]) - Convert.ToDouble(Request[Sunday3Discount.UniqueID])).ToString("f2");
        //        }
        //        Sunday3Schedule = Convert.ToString(Request[Sunday3NoOfItems.UniqueID]) + "##" + Convert.ToString(Request[Sunday3OriginalPrice.UniqueID]) + "##" + Convert.ToString(SUDiscount) + "##01/01/1990 " + Convert.ToString(Request[Sunday3FromTime.UniqueID]) + "##01/01/1990 " + Convert.ToString(Request[Sunday3ToTime.UniqueID]);
        //    }
        //}
        //objBusinessBAL.Sunday3Schedule = Sunday3Schedule;

        #endregion

        string strPassword = string.Empty;
        //objBusinessBAL.Status = Convert.ToInt32(Request[ddlStatus.UniqueID]);
        int MultipleStore = 0;
        //MultipleStore = Convert.ToInt32(Request[ddlMultipleStore.UniqueID]);
        int BYOContainers = 0;
        //BYOContainers = Convert.ToInt32(Request[ddlBYOContainers.UniqueID]);

        //string LastName = Convert.ToString(Request[txtLastName.UniqueID]);
        string State = Convert.ToString(Request[ddlState.UniqueID]);
        int ProfilePhotoID = Convert.ToInt32(Request[hdnProfilePhotoID.UniqueID]);

        SalesAdminBAL objSalesAdminBAL = new SalesAdminBAL();
        objSalesAdminBAL.ID = ID;
        DataSet ds = objSalesAdminBAL.BusinessDetailsByID();

        BusinessPALAdminScreen objBusinessPAL = new BusinessPALAdminScreen();
        objBusinessPAL.ID = ID;

        //string savePath = Server.MapPath("~/" + Config.CMSFiles + "/");

        if (Request.Files["profilePhoto"] != null && Request.Files["profilePhoto"].ContentLength > 0)
        {
            var file = Request.Files["profilePhoto"];
            string fileName = Path.GetFileName(file.FileName);
            string savePath = Server.MapPath("~/" + Config.CMSFiles + fileName);
            file.SaveAs(savePath);
            objBusinessPAL.ProfilePhoto = fileName;
        }
        else
        {
            objBusinessPAL.ProfilePhoto = Convert.ToString(ds.Tables[0].Rows[0]["ProfilePhoto"]);
        }

        if (Request.Files["storePhoto"] != null && Request.Files["storePhoto"].ContentLength > 0)
        {
            var file = Request.Files["storePhoto"];
            string fileName = Path.GetFileName(file.FileName);
            string savePath = Server.MapPath("~/" + Config.CMSFiles + fileName);
            file.SaveAs(savePath);
            objBusinessPAL.StorePhoto = fileName;
        }
        else
        {
            objBusinessPAL.StorePhoto = Convert.ToString(ds.Tables[0].Rows[0]["StoreImages"]);
        }

        // Basic info
        objBusinessPAL.BusinessName = string.IsNullOrEmpty(Request.Form["businessName"]) ? Convert.ToString(ds.Tables[0].Rows[0]["BusinessName"]) : Request.Form["businessName"];
        objBusinessPAL.Location = string.IsNullOrEmpty(Request.Form["location"]) ? Convert.ToString(ds.Tables[0].Rows[0]["Location"]) : Request.Form["location"];
        objBusinessPAL.AccountName = string.IsNullOrEmpty(Request.Form["accountName"]) ? Convert.ToString(ds.Tables[0].Rows[0]["AccountName"]) : Request.Form["accountName"];
        objBusinessPAL.State = string.IsNullOrEmpty(Request.Form["state"]) ? Convert.ToString(ds.Tables[0].Rows[0]["State"]) : Request.Form["state"];
        objBusinessPAL.BusinessPhone = string.IsNullOrEmpty(Request.Form["businessPhone"]) ? Convert.ToString(ds.Tables[0].Rows[0]["BusinessPhone"]) : Request.Form["businessPhone"];
        objBusinessPAL.BusinessMobile = string.IsNullOrEmpty(Request.Form["businessMobile"]) ? Convert.ToString(ds.Tables[0].Rows[0]["BusinessMobile"]) : Request.Form["businessMobile"];
        objBusinessPAL.StreetAddress = string.IsNullOrEmpty(Request.Form["streetAddress"]) ? Convert.ToString(ds.Tables[0].Rows[0]["StreetAddress"]) : Request.Form["streetAddress"];
        objBusinessPAL.EmailAddress = string.IsNullOrEmpty(Request.Form["emailAddress"]) ? Convert.ToString(ds.Tables[0].Rows[0]["EmailAddress"]) : Request.Form["emailAddress"];
        objBusinessPAL.ABN = string.IsNullOrEmpty(Request.Form["abn"]) ? Convert.ToString(ds.Tables[0].Rows[0]["ABN"]) : Request.Form["abn"];
        objBusinessPAL.BSBNo = string.IsNullOrEmpty(Request.Form["bsbno"]) ? Convert.ToString(ds.Tables[0].Rows[0]["BSBNo"]) : Request.Form["bsbno"];
        objBusinessPAL.AccountNumber = string.IsNullOrEmpty(Request.Form["accountNumber"]) ? Convert.ToString(ds.Tables[0].Rows[0]["AccountNumber"]) : Request.Form["accountNumber"];
        objBusinessPAL.SelectedBusiness = string.IsNullOrEmpty(Request.Form["selectedBusiness"]) ? "" : Request.Form["selectedBusiness"];
        objBusinessPAL.SelectedDietTypes = string.IsNullOrEmpty(Request.Form["selectedDietTypes"]) ? "" : Request.Form["selectedDietTypes"];
        objBusinessPAL.BankName = string.IsNullOrEmpty(Request.Form["bankName"]) ? Convert.ToString(ds.Tables[0].Rows[0]["BankName"]) : Request.Form["bankName"];
        //objBusinessPAL.ProfilePhoto = (Request.Form["profilePhoto"]);
        //objBusinessPAL.StorePhoto = (Request.Form["storePhoto"]);
        //objBusinessPAL.SelectedCategory = string.IsNullOrEmpty(Request.Form["restaurantType"]) ? "" : Request.Form["restaurantType"];

        // Current Day
        objBusinessPAL.CurrentDayNoOfItems1 = string.IsNullOrEmpty(Request.Form["currentDayNoOfItems1"]) ? 0 : Convert.ToInt32(Request.Form["currentDayNoOfItems1"]);
        objBusinessPAL.CurrentDayOriginalPrice1 = string.IsNullOrEmpty(Request.Form["currentDayOriginalPrice1"]) ? 0m : Convert.ToDecimal(30); //Convert.ToDecimal(Request.Form["currentDayOriginalPrice1"]);
        objBusinessPAL.CurrentDayDiscount1 = string.IsNullOrEmpty(Request.Form["currentDayDiscount1"]) ? 0m : Convert.ToDecimal(9.99); // Convert.ToDecimal(Request.Form["currentDayDiscount1"]);
        objBusinessPAL.CurrentDayNoOfItems2 = string.IsNullOrEmpty(Request.Form["currentDayNoOfItems2"]) ? 0 : Convert.ToInt32(Request.Form["currentDayNoOfItems2"]);
        objBusinessPAL.CurrentDayOriginalPrice2 = string.IsNullOrEmpty(Request.Form["currentDayOriginalPrice2"]) ? 0m : Convert.ToDecimal(45); // Convert.ToDecimal(Request.Form["currentDayOriginalPrice2"]);
        objBusinessPAL.CurrentDayDiscount2 = string.IsNullOrEmpty(Request.Form["currentDayDiscount2"]) ? 0m : Convert.ToDecimal(14.99); // Convert.ToDecimal(Request.Form["currentDayDiscount2"]);
        objBusinessPAL.CurrentDayNoOfItems3 = string.IsNullOrEmpty(Request.Form["currentDayNoOfItems3"]) ? 0 : Convert.ToInt32(Request.Form["currentDayNoOfItems3"]);
        objBusinessPAL.CurrentDayOriginalPrice3 = string.IsNullOrEmpty(Request.Form["currentDayOriginalPrice3"]) ? 0m : Convert.ToDecimal(60); // Convert.ToDecimal(Request.Form["currentDayOriginalPrice3"]);
        objBusinessPAL.CurrentDayDiscount3 = string.IsNullOrEmpty(Request.Form["currentDayDiscount3"]) ? 0m : Convert.ToDecimal(19.99); // Convert.ToDecimal(Request.Form["currentDayDiscount3"]);
        objBusinessPAL.CurrentDayFromTime = string.IsNullOrEmpty(Request.Form["currentDayFromTime"]) ? "" : Request.Form["currentDayFromTime"];
        objBusinessPAL.CurrentDayToTime = string.IsNullOrEmpty(Request.Form["currentDayToTime"]) ? "" : Request.Form["currentDayToTime"];
        objBusinessPAL.CurrentDate = DateTime.Parse(Request.Form["currentDate"]);

        // Monday
        objBusinessPAL.MondayNoOfItems1 = string.IsNullOrEmpty(Request.Form["mondayNoOfItems1"]) ? 0 : Convert.ToInt32(Request.Form["mondayNoOfItems1"]);
        objBusinessPAL.MondayOriginalPrice1 = string.IsNullOrEmpty(Request.Form["mondayOriginalPrice1"]) ? 0m : Convert.ToDecimal(Request.Form["mondayOriginalPrice1"]);
        objBusinessPAL.MondayDiscount1 = string.IsNullOrEmpty(Request.Form["mondayDiscount1"]) ? 0m : Convert.ToDecimal(Request.Form["mondayDiscount1"]);
        objBusinessPAL.MondayNoOfItems2 = string.IsNullOrEmpty(Request.Form["mondayNoOfItems2"]) ? 0 : Convert.ToInt32(Request.Form["mondayNoOfItems2"]);
        objBusinessPAL.MondayOriginalPrice2 = string.IsNullOrEmpty(Request.Form["mondayOriginalPrice2"]) ? 0m : Convert.ToDecimal(Request.Form["mondayOriginalPrice2"]);
        objBusinessPAL.MondayDiscount2 = string.IsNullOrEmpty(Request.Form["mondayDiscount2"]) ? 0m : Convert.ToDecimal(Request.Form["mondayDiscount2"]);
        objBusinessPAL.MondayNoOfItems3 = string.IsNullOrEmpty(Request.Form["mondayNoOfItems3"]) ? 0 : Convert.ToInt32(Request.Form["mondayNoOfItems3"]);
        objBusinessPAL.MondayOriginalPrice3 = string.IsNullOrEmpty(Request.Form["mondayOriginalPrice3"]) ? 0m : Convert.ToDecimal(Request.Form["mondayOriginalPrice3"]);
        objBusinessPAL.MondayDiscount3 = string.IsNullOrEmpty(Request.Form["mondayDiscount3"]) ? 0m : Convert.ToDecimal(Request.Form["mondayDiscount3"]);
        objBusinessPAL.MondayPickUpFromTime = string.IsNullOrEmpty(Request.Form["mondayFromTime"]) ? "" : Request.Form["mondayFromTime"];
        objBusinessPAL.MondayPickUpToTime = string.IsNullOrEmpty(Request.Form["mondayToTime"]) ? "" : Request.Form["mondayToTime"];

        // Tuesday
        objBusinessPAL.TuesdayNoOfItems1 = string.IsNullOrEmpty(Request.Form["tuesdayNoOfItems1"]) ? 0 : Convert.ToInt32(Request.Form["tuesdayNoOfItems1"]);
        objBusinessPAL.TuesdayOriginalPrice1 = string.IsNullOrEmpty(Request.Form["tuesdayOriginalPrice1"]) ? 0m : Convert.ToDecimal(Request.Form["tuesdayOriginalPrice1"]);
        objBusinessPAL.TuesdayDiscount1 = string.IsNullOrEmpty(Request.Form["tuesdayDiscount1"]) ? 0m : Convert.ToDecimal(Request.Form["tuesdayDiscount1"]);
        objBusinessPAL.TuesdayNoOfItems2 = string.IsNullOrEmpty(Request.Form["tuesdayNoOfItems2"]) ? 0 : Convert.ToInt32(Request.Form["tuesdayNoOfItems2"]);
        objBusinessPAL.TuesdayOriginalPrice2 = string.IsNullOrEmpty(Request.Form["tuesdayOriginalPrice2"]) ? 0m : Convert.ToDecimal(Request.Form["tuesdayOriginalPrice2"]);
        objBusinessPAL.TuesdayDiscount2 = string.IsNullOrEmpty(Request.Form["tuesdayDiscount2"]) ? 0m : Convert.ToDecimal(Request.Form["tuesdayDiscount2"]);
        objBusinessPAL.TuesdayNoOfItems3 = string.IsNullOrEmpty(Request.Form["tuesdayNoOfItems3"]) ? 0 : Convert.ToInt32(Request.Form["tuesdayNoOfItems3"]);
        objBusinessPAL.TuesdayOriginalPrice3 = string.IsNullOrEmpty(Request.Form["tuesdayOriginalPrice3"]) ? 0m : Convert.ToDecimal(Request.Form["tuesdayOriginalPrice3"]);
        objBusinessPAL.TuesdayDiscount3 = string.IsNullOrEmpty(Request.Form["tuesdayDiscount3"]) ? 0m : Convert.ToDecimal(Request.Form["tuesdayDiscount3"]);
        objBusinessPAL.TuesdayPickUpFromTime = string.IsNullOrEmpty(Request.Form["tuesdayFromTime"]) ? "" : Request.Form["tuesdayFromTime"];
        objBusinessPAL.TuesdayPickUpToTime = string.IsNullOrEmpty(Request.Form["tuesdayToTime"]) ? "" : Request.Form["tuesdayToTime"];

        // Wednesday
        objBusinessPAL.WednesdayNoOfItems1 = string.IsNullOrEmpty(Request.Form["wednesdayNoOfItems1"]) ? 0 : Convert.ToInt32(Request.Form["wednesdayNoOfItems1"]);
        objBusinessPAL.WednesdayOriginalPrice1 = string.IsNullOrEmpty(Request.Form["wednesdayOriginalPrice1"]) ? 0m : Convert.ToDecimal(Request.Form["wednesdayOriginalPrice1"]);
        objBusinessPAL.WednesdayDiscount1 = string.IsNullOrEmpty(Request.Form["wednesdayDiscount1"]) ? 0m : Convert.ToDecimal(Request.Form["wednesdayDiscount1"]);
        objBusinessPAL.WednesdayNoOfItems2 = string.IsNullOrEmpty(Request.Form["wednesdayNoOfItems2"]) ? 0 : Convert.ToInt32(Request.Form["wednesdayNoOfItems2"]);
        objBusinessPAL.WednesdayOriginalPrice2 = string.IsNullOrEmpty(Request.Form["wednesdayOriginalPrice2"]) ? 0m : Convert.ToDecimal(Request.Form["wednesdayOriginalPrice2"]);
        objBusinessPAL.WednesdayDiscount2 = string.IsNullOrEmpty(Request.Form["wednesdayDiscount2"]) ? 0m : Convert.ToDecimal(Request.Form["wednesdayDiscount2"]);
        objBusinessPAL.WednesdayNoOfItems3 = string.IsNullOrEmpty(Request.Form["wednesdayNoOfItems3"]) ? 0 : Convert.ToInt32(Request.Form["wednesdayNoOfItems3"]);
        objBusinessPAL.WednesdayOriginalPrice3 = string.IsNullOrEmpty(Request.Form["wednesdayOriginalPrice3"]) ? 0m : Convert.ToDecimal(Request.Form["wednesdayOriginalPrice3"]);
        objBusinessPAL.WednesdayDiscount3 = string.IsNullOrEmpty(Request.Form["wednesdayDiscount3"]) ? 0m : Convert.ToDecimal(Request.Form["wednesdayDiscount3"]);
        objBusinessPAL.WednesdayPickUpFromTime = string.IsNullOrEmpty(Request.Form["wednesdayFromTime"]) ? "" : Request.Form["wednesdayFromTime"];
        objBusinessPAL.WednesdayPickUpToTime = string.IsNullOrEmpty(Request.Form["wednesdayToTime"]) ? "" : Request.Form["wednesdayToTime"];

        // Thursday
        objBusinessPAL.ThursdayNoOfItems1 = string.IsNullOrEmpty(Request.Form["thursdayNoOfItems1"]) ? 0 : Convert.ToInt32(Request.Form["thursdayNoOfItems1"]);
        objBusinessPAL.ThursdayOriginalPrice1 = string.IsNullOrEmpty(Request.Form["thursdayOriginalPrice1"]) ? 0m : Convert.ToDecimal(Request.Form["thursdayOriginalPrice1"]);
        objBusinessPAL.ThursdayDiscount1 = string.IsNullOrEmpty(Request.Form["thursdayDiscount1"]) ? 0m : Convert.ToDecimal(Request.Form["thursdayDiscount1"]);
        objBusinessPAL.ThursdayNoOfItems2 = string.IsNullOrEmpty(Request.Form["thursdayNoOfItems2"]) ? 0 : Convert.ToInt32(Request.Form["thursdayNoOfItems2"]);
        objBusinessPAL.ThursdayOriginalPrice2 = string.IsNullOrEmpty(Request.Form["thursdayOriginalPrice2"]) ? 0m : Convert.ToDecimal(Request.Form["thursdayOriginalPrice2"]);
        objBusinessPAL.ThursdayDiscount2 = string.IsNullOrEmpty(Request.Form["thursdayDiscount2"]) ? 0m : Convert.ToDecimal(Request.Form["thursdayDiscount2"]);
        objBusinessPAL.ThursdayNoOfItems3 = string.IsNullOrEmpty(Request.Form["thursdayNoOfItems3"]) ? 0 : Convert.ToInt32(Request.Form["thursdayNoOfItems3"]);
        objBusinessPAL.ThursdayOriginalPrice3 = string.IsNullOrEmpty(Request.Form["thursdayOriginalPrice3"]) ? 0m : Convert.ToDecimal(Request.Form["thursdayOriginalPrice3"]);
        objBusinessPAL.ThursdayDiscount3 = string.IsNullOrEmpty(Request.Form["thursdayDiscount3"]) ? 0m : Convert.ToDecimal(Request.Form["thursdayDiscount3"]);
        objBusinessPAL.ThursdayPickUpFromTime = string.IsNullOrEmpty(Request.Form["thursdayFromTime"]) ? "" : Request.Form["thursdayFromTime"];
        objBusinessPAL.ThursdayPickUpToTime = string.IsNullOrEmpty(Request.Form["thursdayToTime"]) ? "" : Request.Form["thursdayToTime"];

        // Friday
        objBusinessPAL.FridayNoOfItems1 = string.IsNullOrEmpty(Request.Form["fridayNoOfItems1"]) ? 0 : Convert.ToInt32(Request.Form["fridayNoOfItems1"]);
        objBusinessPAL.FridayOriginalPrice1 = string.IsNullOrEmpty(Request.Form["fridayOriginalPrice1"]) ? 0m : Convert.ToDecimal(Request.Form["fridayOriginalPrice1"]);
        objBusinessPAL.FridayDiscount1 = string.IsNullOrEmpty(Request.Form["fridayDiscount1"]) ? 0m : Convert.ToDecimal(Request.Form["fridayDiscount1"]);
        objBusinessPAL.FridayNoOfItems2 = string.IsNullOrEmpty(Request.Form["fridayNoOfItems2"]) ? 0 : Convert.ToInt32(Request.Form["fridayNoOfItems2"]);
        objBusinessPAL.FridayOriginalPrice2 = string.IsNullOrEmpty(Request.Form["fridayOriginalPrice2"]) ? 0m : Convert.ToDecimal(Request.Form["fridayOriginalPrice2"]);
        objBusinessPAL.FridayDiscount2 = string.IsNullOrEmpty(Request.Form["fridayDiscount2"]) ? 0m : Convert.ToDecimal(Request.Form["fridayDiscount2"]);
        objBusinessPAL.FridayNoOfItems3 = string.IsNullOrEmpty(Request.Form["fridayNoOfItems3"]) ? 0 : Convert.ToInt32(Request.Form["fridayNoOfItems3"]);
        objBusinessPAL.FridayOriginalPrice3 = string.IsNullOrEmpty(Request.Form["fridayOriginalPrice3"]) ? 0m : Convert.ToDecimal(Request.Form["fridayOriginalPrice3"]);
        objBusinessPAL.FridayDiscount3 = string.IsNullOrEmpty(Request.Form["fridayDiscount3"]) ? 0m : Convert.ToDecimal(Request.Form["fridayDiscount3"]);
        objBusinessPAL.FridayPickUpFromTime = string.IsNullOrEmpty(Request.Form["fridayFromTime"]) ? "" : Request.Form["fridayFromTime"];
        objBusinessPAL.FridayPickUpToTime = string.IsNullOrEmpty(Request.Form["fridayToTime"]) ? "" : Request.Form["fridayToTime"];

        // Saturday
        objBusinessPAL.SaturdayNoOfItems1 = string.IsNullOrEmpty(Request.Form["saturdayNoOfItems1"]) ? 0 : Convert.ToInt32(Request.Form["saturdayNoOfItems1"]);
        objBusinessPAL.SaturdayOriginalPrice1 = string.IsNullOrEmpty(Request.Form["saturdayOriginalPrice1"]) ? 0m : Convert.ToDecimal(Request.Form["saturdayOriginalPrice1"]);
        objBusinessPAL.SaturdayDiscount1 = string.IsNullOrEmpty(Request.Form["saturdayDiscount1"]) ? 0m : Convert.ToDecimal(Request.Form["saturdayDiscount1"]);
        objBusinessPAL.SaturdayNoOfItems2 = string.IsNullOrEmpty(Request.Form["saturdayNoOfItems2"]) ? 0 : Convert.ToInt32(Request.Form["saturdayNoOfItems2"]);
        objBusinessPAL.SaturdayOriginalPrice2 = string.IsNullOrEmpty(Request.Form["saturdayOriginalPrice2"]) ? 0m : Convert.ToDecimal(Request.Form["saturdayOriginalPrice2"]);
        objBusinessPAL.SaturdayDiscount2 = string.IsNullOrEmpty(Request.Form["saturdayDiscount2"]) ? 0m : Convert.ToDecimal(Request.Form["saturdayDiscount2"]);
        objBusinessPAL.SaturdayNoOfItems3 = string.IsNullOrEmpty(Request.Form["saturdayNoOfItems3"]) ? 0 : Convert.ToInt32(Request.Form["saturdayNoOfItems3"]);
        objBusinessPAL.SaturdayOriginalPrice3 = string.IsNullOrEmpty(Request.Form["saturdayOriginalPrice3"]) ? 0m : Convert.ToDecimal(Request.Form["saturdayOriginalPrice3"]);
        objBusinessPAL.SaturdayDiscount3 = string.IsNullOrEmpty(Request.Form["saturdayDiscount3"]) ? 0m : Convert.ToDecimal(Request.Form["saturdayDiscount3"]);
        objBusinessPAL.SaturdayPickUpFromTime = string.IsNullOrEmpty(Request.Form["saturdayFromTime"]) ? "" : Request.Form["saturdayFromTime"];
        objBusinessPAL.SaturdayPickUpToTime = string.IsNullOrEmpty(Request.Form["saturdayToTime"]) ? "" : Request.Form["saturdayToTime"];

        // Sunday
        objBusinessPAL.SundayNoOfItems1 = string.IsNullOrEmpty(Request.Form["sundayNoOfItems1"]) ? 0 : Convert.ToInt32(Request.Form["sundayNoOfItems1"]);
        objBusinessPAL.SundayOriginalPrice1 = string.IsNullOrEmpty(Request.Form["sundayOriginalPrice1"]) ? 0m : Convert.ToDecimal(Request.Form["sundayOriginalPrice1"]);
        objBusinessPAL.SundayDiscount1 = string.IsNullOrEmpty(Request.Form["sundayDiscount1"]) ? 0m : Convert.ToDecimal(Request.Form["sundayDiscount1"]);
        objBusinessPAL.SundayNoOfItems2 = string.IsNullOrEmpty(Request.Form["sundayNoOfItems2"]) ? 0 : Convert.ToInt32(Request.Form["sundayNoOfItems2"]);
        objBusinessPAL.SundayOriginalPrice2 = string.IsNullOrEmpty(Request.Form["sundayOriginalPrice2"]) ? 0m : Convert.ToDecimal(Request.Form["sundayOriginalPrice2"]);
        objBusinessPAL.SundayDiscount2 = string.IsNullOrEmpty(Request.Form["sundayDiscount2"]) ? 0m : Convert.ToDecimal(Request.Form["sundayDiscount2"]);
        objBusinessPAL.SundayNoOfItems3 = string.IsNullOrEmpty(Request.Form["sundayNoOfItems3"]) ? 0 : Convert.ToInt32(Request.Form["sundayNoOfItems3"]);
        objBusinessPAL.SundayOriginalPrice3 = string.IsNullOrEmpty(Request.Form["sundayOriginalPrice3"]) ? 0m : Convert.ToDecimal(Request.Form["sundayOriginalPrice3"]);
        objBusinessPAL.SundayDiscount3 = string.IsNullOrEmpty(Request.Form["sundayDiscount3"]) ? 0m : Convert.ToDecimal(Request.Form["sundayDiscount3"]);
        objBusinessPAL.SundayPickUpFromTime = string.IsNullOrEmpty(Request.Form["sundayFromTime"]) ? "" : Request.Form["sundayFromTime"];
        objBusinessPAL.SundayPickUpToTime = string.IsNullOrEmpty(Request.Form["sundayToTime"]) ? "" : Request.Form["sundayToTime"];

        objBusinessPAL.AboutUs = string.IsNullOrEmpty(Request.Form["aboutUs"]) ? "" : Convert.ToString(ds.Tables[0].Rows[0]["AboutUs"]);

        objBusinessPAL.Description = !string.IsNullOrEmpty(Request["description"]) ? Request["description"]
        : (ds.Tables[0].Rows.Count > 0 ? Convert.ToString(ds.Tables[0].Rows[0]["Description"]) : "");


        long result = SaveAdminDAL(objBusinessPAL, LastName, MultipleStore, BYOContainers, State, ProfilePhotoID);

        switch (result)
        {
            case -1:
                Response.Write("duplicate");
                break;
            default:
                Response.Write("success");
                break;
        }
        bool saved = true;
        Response.Clear();
        Response.ContentType = "text/plain";
        Response.Write(saved ? "success" : "error");
        Response.End();


        // objBusinessBAL.Latitude = Convert.ToString(Request["Latitude"]);
        //objBusinessBAL.Longitude = Convert.ToString(Request["Longitude"]);


        //objBusinessBAL.Latitude = Convert.ToString(Request[txtLatitude.UniqueID]);
        //objBusinessBAL.Longitude = Convert.ToString(Request[txtLongitude.UniqueID]);

        // long result = objBusinessBAL.SaveAdmin();

        //objSalesAdminBAL.ID = ID;
        //string strPasword = string.Empty;
        //if (!string.IsNullOrEmpty(Request[txtPassword.UniqueID]))
        //{
        //    strPasword = Request[txtPassword.UniqueID];
        //}
        //else
        //{
        //    strPasword = Utility.Security.EncryptDescrypt.DecryptString(Request[hdnpwd.UniqueID]);
        //}

        //objSalesAdminBAL.FirstName = Request[txtFirstName.UniqueID].Trim();        

        //objSalesAdminBAL.Password = Utility.Security.EncryptDescrypt.EncryptString(strPasword);
        //objSalesAdminBAL.EmailID = Request[txtEmail.UniqueID].Trim();
        //objSalesAdminBAL.Phone = Request[txtphone.UniqueID].Trim();        
        //switch (objSalesAdminBAL.Save())
        //{
        //    case 0: 
        //      //Response.Write(Common.ShowMessage("This email address already exists. So please try another email address.", "alert-message error", divMsg.ClientID));
        //      //Response.Write(Common.ScriptStartTag + "parent.ScrollTop();" + Common.ScriptEndTag);
        //        Response.Write("duplicate");
        //        break;
        //    default:
        //        Response.Write("success");
        //        //Response.Write(Common.ShowMessage("Sales Admin information has been saved.", "alert-message success", divMsg.ClientID)); 
        //        //Response.Write(Common.ScriptStartTag + "parent.ScrollTop();" + Common.ScriptEndTag);
        //        //Response.Write(Javascript.ScriptStartTag + "window.setTimeout(\"window.location.href='sales-admin-list.aspx'\",2000)" + Javascript.ScriptEndTag);
        //        break;
        //}
    }
    public long SaveAdminDAL(BusinessPALAdminScreen objBusinessPAL, string strLastName, int MultipleStore, int BYOContainers, string State, int ProfilePhotoID)
    {

        DbParameter[] dbParam = new DbParameter[]
 {
    new DbParameter("@ID", DbParameter.DbType.Int, 8, objBusinessPAL.ID),
    new DbParameter("@BusinessName", DbParameter.DbType.VarChar, 200, objBusinessPAL.BusinessName != null ? objBusinessPAL.BusinessName : ""),
    new DbParameter("@BusinessType", DbParameter.DbType.VarChar, 200, objBusinessPAL.SelectedBusiness != null ? objBusinessPAL.SelectedBusiness : ""),
    new DbParameter("@DietaryIDs", DbParameter.DbType.VarChar, 200, objBusinessPAL.SelectedDietTypes != null ? objBusinessPAL.SelectedDietTypes : ""),
    new DbParameter("@EmailAddress", DbParameter.DbType.VarChar, 200, objBusinessPAL.EmailAddress != null ? objBusinessPAL.EmailAddress : ""),
    new DbParameter("@ABN", DbParameter.DbType.VarChar, 200, objBusinessPAL.ABN != null ? objBusinessPAL.ABN : ""),
    new DbParameter("@StreetAddress", DbParameter.DbType.VarChar, 500, objBusinessPAL.StreetAddress != null ? objBusinessPAL.StreetAddress : ""),
    new DbParameter("@Location", DbParameter.DbType.VarChar, 500, objBusinessPAL.Location != null ? objBusinessPAL.Location : ""),
    new DbParameter("@AccountName", DbParameter.DbType.VarChar, 200, objBusinessPAL.AccountName != null ? objBusinessPAL.AccountName : ""),
    new DbParameter("@BusinessPhone", DbParameter.DbType.VarChar, 20, objBusinessPAL.BusinessPhone != null ? objBusinessPAL.BusinessPhone : ""),
    new DbParameter("@BusinessMobile", DbParameter.DbType.VarChar, 20, objBusinessPAL.BusinessMobile != null ? objBusinessPAL.BusinessMobile : ""),
    new DbParameter("@BSBNo", DbParameter.DbType.VarChar, 200, objBusinessPAL.BSBNo != null ? objBusinessPAL.BSBNo : ""),
    new DbParameter("@AccountNumber", DbParameter.DbType.VarChar, 200, objBusinessPAL.AccountNumber != null ? objBusinessPAL.AccountNumber : ""),
    new DbParameter("@BankName", DbParameter.DbType.VarChar, 200, objBusinessPAL.BankName != null ? objBusinessPAL.BankName : ""),
    new DbParameter("@ProfilePhoto", DbParameter.DbType.VarChar, 500, objBusinessPAL.ProfilePhoto),
    new DbParameter("@StorePhoto", DbParameter.DbType.VarChar, 500, objBusinessPAL.StorePhoto),

    new DbParameter("@CurrentDayNoOfItems1", DbParameter.DbType.Int, 4, objBusinessPAL.CurrentDayNoOfItems1 != null ? objBusinessPAL.CurrentDayNoOfItems1 : 0),
    new DbParameter("@CurrentDayOriginalPrice1", DbParameter.DbType.Decimal, 18, objBusinessPAL.CurrentDayOriginalPrice1 != null ? objBusinessPAL.CurrentDayOriginalPrice1 : 0m),
    new DbParameter("@CurrentDayDiscount1", DbParameter.DbType.Decimal, 18, objBusinessPAL.CurrentDayDiscount1 != null ? objBusinessPAL.CurrentDayDiscount1 : 0m),
    new DbParameter("@CurrentDayNoOfItems2", DbParameter.DbType.Int, 4, objBusinessPAL.CurrentDayNoOfItems2 != null ? objBusinessPAL.CurrentDayNoOfItems2 : 0),
    new DbParameter("@CurrentDayOriginalPrice2", DbParameter.DbType.Decimal, 18, objBusinessPAL.CurrentDayOriginalPrice2 != null ? objBusinessPAL.CurrentDayOriginalPrice2 : 0m),
    new DbParameter("@CurrentDayDiscount2", DbParameter.DbType.Decimal, 18, objBusinessPAL.CurrentDayDiscount2 != null ? objBusinessPAL.CurrentDayDiscount2 : 0m),
    new DbParameter("@CurrentDayNoOfItems3", DbParameter.DbType.Int, 4, objBusinessPAL.CurrentDayNoOfItems3 != null ? objBusinessPAL.CurrentDayNoOfItems3 : 0),
    new DbParameter("@CurrentDayOriginalPrice3", DbParameter.DbType.Decimal, 18, objBusinessPAL.CurrentDayOriginalPrice3 != null ? objBusinessPAL.CurrentDayOriginalPrice3 : 0m),
    new DbParameter("@CurrentDayDiscount3", DbParameter.DbType.Decimal, 18, objBusinessPAL.CurrentDayDiscount3 != null ? objBusinessPAL.CurrentDayDiscount3 : 0m),
    new DbParameter("@CurrentDayFromTime", DbParameter.DbType.VarChar, 50, objBusinessPAL.CurrentDayFromTime != null ? objBusinessPAL.CurrentDayFromTime : ""),
    new DbParameter("@CurrentDayToTime", DbParameter.DbType.VarChar, 50, objBusinessPAL.CurrentDayToTime != null ? objBusinessPAL.CurrentDayToTime : ""),
    new DbParameter("@CurrentDate", DbParameter.DbType.DateTime, 50, objBusinessPAL.CurrentDate),

    // Monday
    new DbParameter("@MondayPickUpFromTime", DbParameter.DbType.VarChar, 8, objBusinessPAL.MondayPickUpFromTime != null ? objBusinessPAL.MondayPickUpFromTime : ""),
    new DbParameter("@MondayPickUpToTime", DbParameter.DbType.VarChar, 8, objBusinessPAL.MondayPickUpToTime != null ? objBusinessPAL.MondayPickUpToTime : ""),
    new DbParameter("@MondayNoOfItems1", DbParameter.DbType.Int, 4, objBusinessPAL.MondayNoOfItems1 != null ? objBusinessPAL.MondayNoOfItems1 : 0),
    new DbParameter("@MondayOriginalPrice1", DbParameter.DbType.Decimal, 18, objBusinessPAL.MondayOriginalPrice1 != null ? objBusinessPAL.MondayOriginalPrice1 : 0m),
    new DbParameter("@MondayDiscount1", DbParameter.DbType.Decimal, 18, objBusinessPAL.MondayDiscount1 != null ? objBusinessPAL.MondayDiscount1 : 0m),
    new DbParameter("@MondayNoOfItems2", DbParameter.DbType.Int, 4, objBusinessPAL.MondayNoOfItems2 != null ? objBusinessPAL.MondayNoOfItems2 : 0),
    new DbParameter("@MondayOriginalPrice2", DbParameter.DbType.Decimal, 18, objBusinessPAL.MondayOriginalPrice2 != null ? objBusinessPAL.MondayOriginalPrice2 : 0m),
    new DbParameter("@MondayDiscount2", DbParameter.DbType.Decimal, 18, objBusinessPAL.MondayDiscount2 != null ? objBusinessPAL.MondayDiscount2 : 0m),
    new DbParameter("@MondayNoOfItems3", DbParameter.DbType.Int, 4, objBusinessPAL.MondayNoOfItems3 != null ? objBusinessPAL.MondayNoOfItems3 : 0),
    new DbParameter("@MondayOriginalPrice3", DbParameter.DbType.Decimal, 18, objBusinessPAL.MondayOriginalPrice3 != null ? objBusinessPAL.MondayOriginalPrice3 : 0m),
    new DbParameter("@MondayDiscount3", DbParameter.DbType.Decimal, 18, objBusinessPAL.MondayDiscount3 != null ? objBusinessPAL.MondayDiscount3 : 0m),

    // Tuesday
    new DbParameter("@TuesdayPickUpFromTime", DbParameter.DbType.VarChar, 8, objBusinessPAL.TuesdayPickUpFromTime != null ? objBusinessPAL.TuesdayPickUpFromTime : ""),
    new DbParameter("@TuesdayPickUpToTime", DbParameter.DbType.VarChar, 8, objBusinessPAL.TuesdayPickUpToTime != null ? objBusinessPAL.TuesdayPickUpToTime : ""),

    new DbParameter("@TuesdayNoOfItems1", DbParameter.DbType.Int, 4, objBusinessPAL.TuesdayNoOfItems1 != null ? objBusinessPAL.TuesdayNoOfItems1 : 0),
    new DbParameter("@TuesdayOriginalPrice1", DbParameter.DbType.Decimal, 18, objBusinessPAL.TuesdayOriginalPrice1 != null ? objBusinessPAL.TuesdayOriginalPrice1 : 0m),
    new DbParameter("@TuesdayDiscount1", DbParameter.DbType.Decimal, 18, objBusinessPAL.TuesdayDiscount1 != null ? objBusinessPAL.TuesdayDiscount1 : 0m),
    new DbParameter("@TuesdayNoOfItems2", DbParameter.DbType.Int, 4, objBusinessPAL.TuesdayNoOfItems2 != null ? objBusinessPAL.TuesdayNoOfItems2 : 0),
    new DbParameter("@TuesdayOriginalPrice2", DbParameter.DbType.Decimal, 18, objBusinessPAL.TuesdayOriginalPrice2 != null ? objBusinessPAL.TuesdayOriginalPrice2 : 0m),
    new DbParameter("@TuesdayDiscount2", DbParameter.DbType.Decimal, 18, objBusinessPAL.TuesdayDiscount2 != null ? objBusinessPAL.TuesdayDiscount2 : 0m),
    new DbParameter("@TuesdayNoOfItems3", DbParameter.DbType.Int, 4, objBusinessPAL.TuesdayNoOfItems3 != null ? objBusinessPAL.TuesdayNoOfItems3 : 0),
    new DbParameter("@TuesdayOriginalPrice3", DbParameter.DbType.Decimal, 18, objBusinessPAL.TuesdayOriginalPrice3 != null ? objBusinessPAL.TuesdayOriginalPrice3 : 0m),
    new DbParameter("@TuesdayDiscount3", DbParameter.DbType.Decimal, 18, objBusinessPAL.TuesdayDiscount3 != null ? objBusinessPAL.TuesdayDiscount3 : 0m),

    // Wednesday
    new DbParameter("@WednesdayPickUpFromTime", DbParameter.DbType.VarChar, 8, objBusinessPAL.WednesdayPickUpFromTime != null ? objBusinessPAL.WednesdayPickUpFromTime : ""),
    new DbParameter("@WednesdayPickUpToTime", DbParameter.DbType.VarChar, 8, objBusinessPAL.WednesdayPickUpToTime != null ? objBusinessPAL.WednesdayPickUpToTime : ""),

    new DbParameter("@WednesdayNoOfItems1", DbParameter.DbType.Int, 4, objBusinessPAL.WednesdayNoOfItems1 != null ? objBusinessPAL.WednesdayNoOfItems1 : 0),
    new DbParameter("@WednesdayOriginalPrice1", DbParameter.DbType.Decimal, 18, objBusinessPAL.WednesdayOriginalPrice1 != null ? objBusinessPAL.WednesdayOriginalPrice1 : 0m),
    new DbParameter("@WednesdayDiscount1", DbParameter.DbType.Decimal, 18, objBusinessPAL.WednesdayDiscount1 != null ? objBusinessPAL.WednesdayDiscount1 : 0m),
    new DbParameter("@WednesdayNoOfItems2", DbParameter.DbType.Int, 4, objBusinessPAL.WednesdayNoOfItems2 != null ? objBusinessPAL.WednesdayNoOfItems2 : 0),
    new DbParameter("@WednesdayOriginalPrice2", DbParameter.DbType.Decimal, 18, objBusinessPAL.WednesdayOriginalPrice2 != null ? objBusinessPAL.WednesdayOriginalPrice2 : 0m),
    new DbParameter("@WednesdayDiscount2", DbParameter.DbType.Decimal, 18, objBusinessPAL.WednesdayDiscount2 != null ? objBusinessPAL.WednesdayDiscount2 : 0m),
    new DbParameter("@WednesdayNoOfItems3", DbParameter.DbType.Int, 4, objBusinessPAL.WednesdayNoOfItems3 != null ? objBusinessPAL.WednesdayNoOfItems3 : 0),
    new DbParameter("@WednesdayOriginalPrice3", DbParameter.DbType.Decimal, 18, objBusinessPAL.WednesdayOriginalPrice3 != null ? objBusinessPAL.WednesdayOriginalPrice3 : 0m),
    new DbParameter("@WednesdayDiscount3", DbParameter.DbType.Decimal, 18, objBusinessPAL.WednesdayDiscount3 != null ? objBusinessPAL.WednesdayDiscount3 : 0m),

    // Thursday
    new DbParameter("@ThursdayPickUpFromTime", DbParameter.DbType.VarChar, 8, objBusinessPAL.ThursdayPickUpFromTime != null ? objBusinessPAL.ThursdayPickUpFromTime : ""),
    new DbParameter("@ThursdayPickUpToTime", DbParameter.DbType.VarChar, 8, objBusinessPAL.ThursdayPickUpToTime != null ? objBusinessPAL.ThursdayPickUpToTime : ""),

    new DbParameter("@ThursdayNoOfItems1", DbParameter.DbType.Int, 4, objBusinessPAL.ThursdayNoOfItems1 != null ? objBusinessPAL.ThursdayNoOfItems1 : 0),
    new DbParameter("@ThursdayOriginalPrice1", DbParameter.DbType.Decimal, 18, objBusinessPAL.ThursdayOriginalPrice1 != null ? objBusinessPAL.ThursdayOriginalPrice1 : 0m),
    new DbParameter("@ThursdayDiscount1", DbParameter.DbType.Decimal, 18, objBusinessPAL.ThursdayDiscount1 != null ? objBusinessPAL.ThursdayDiscount1 : 0m),
    new DbParameter("@ThursdayNoOfItems2", DbParameter.DbType.Int, 4, objBusinessPAL.ThursdayNoOfItems2 != null ? objBusinessPAL.ThursdayNoOfItems2 : 0),
    new DbParameter("@ThursdayOriginalPrice2", DbParameter.DbType.Decimal, 18, objBusinessPAL.ThursdayOriginalPrice2 != null ? objBusinessPAL.ThursdayOriginalPrice2 : 0m),
    new DbParameter("@ThursdayDiscount2", DbParameter.DbType.Decimal, 18, objBusinessPAL.ThursdayDiscount2 != null ? objBusinessPAL.ThursdayDiscount2 : 0m),
    new DbParameter("@ThursdayNoOfItems3", DbParameter.DbType.Int, 4, objBusinessPAL.ThursdayNoOfItems3 != null ? objBusinessPAL.ThursdayNoOfItems3 : 0),
    new DbParameter("@ThursdayOriginalPrice3", DbParameter.DbType.Decimal, 18, objBusinessPAL.ThursdayOriginalPrice3 != null ? objBusinessPAL.ThursdayOriginalPrice3 : 0m),
    new DbParameter("@ThursdayDiscount3", DbParameter.DbType.Decimal, 18, objBusinessPAL.ThursdayDiscount3 != null ? objBusinessPAL.ThursdayDiscount3 : 0m),

    // Friday
    new DbParameter("@FridayPickUpFromTime", DbParameter.DbType.VarChar, 8, objBusinessPAL.FridayPickUpFromTime != null ? objBusinessPAL.FridayPickUpFromTime : ""),
    new DbParameter("@FridayPickUpToTime", DbParameter.DbType.VarChar, 8, objBusinessPAL.FridayPickUpToTime != null ? objBusinessPAL.FridayPickUpToTime : ""),

    new DbParameter("@FridayNoOfItems1", DbParameter.DbType.Int, 4, objBusinessPAL.FridayNoOfItems1 != null ? objBusinessPAL.FridayNoOfItems1 : 0),
    new DbParameter("@FridayOriginalPrice1", DbParameter.DbType.Decimal, 18, objBusinessPAL.FridayOriginalPrice1 != null ? objBusinessPAL.FridayOriginalPrice1 : 0m),
    new DbParameter("@FridayDiscount1", DbParameter.DbType.Decimal, 18, objBusinessPAL.FridayDiscount1 != null ? objBusinessPAL.FridayDiscount1 : 0m),
    new DbParameter("@FridayNoOfItems2", DbParameter.DbType.Int, 4, objBusinessPAL.FridayNoOfItems2 != null ? objBusinessPAL.FridayNoOfItems2 : 0),
    new DbParameter("@FridayOriginalPrice2", DbParameter.DbType.Decimal, 18, objBusinessPAL.FridayOriginalPrice2 != null ? objBusinessPAL.FridayOriginalPrice2 : 0m),
    new DbParameter("@FridayDiscount2", DbParameter.DbType.Decimal, 18, objBusinessPAL.FridayDiscount2 != null ? objBusinessPAL.FridayDiscount2 : 0m),
    new DbParameter("@FridayNoOfItems3", DbParameter.DbType.Int, 4, objBusinessPAL.FridayNoOfItems3 != null ? objBusinessPAL.FridayNoOfItems3 : 0),
    new DbParameter("@FridayOriginalPrice3", DbParameter.DbType.Decimal, 18, objBusinessPAL.FridayOriginalPrice3 != null ? objBusinessPAL.FridayOriginalPrice3 : 0m),
    new DbParameter("@FridayDiscount3", DbParameter.DbType.Decimal, 18, objBusinessPAL.FridayDiscount3 != null ? objBusinessPAL.FridayDiscount3 : 0m),

    // Saturday
    new DbParameter("@SaturdayPickUpFromTime", DbParameter.DbType.VarChar, 8, objBusinessPAL.SaturdayPickUpFromTime != null ? objBusinessPAL.SaturdayPickUpFromTime : ""),
    new DbParameter("@SaturdayPickUpToTime", DbParameter.DbType.VarChar, 8, objBusinessPAL.SaturdayPickUpToTime != null ? objBusinessPAL.SaturdayPickUpToTime : ""),

    new DbParameter("@SaturdayNoOfItems1", DbParameter.DbType.Int, 4, objBusinessPAL.SaturdayNoOfItems1 != null ? objBusinessPAL.SaturdayNoOfItems1 : 0),
    new DbParameter("@SaturdayOriginalPrice1", DbParameter.DbType.Decimal, 18, objBusinessPAL.SaturdayOriginalPrice1 != null ? objBusinessPAL.SaturdayOriginalPrice1 : 0m),
    new DbParameter("@SaturdayDiscount1", DbParameter.DbType.Decimal, 18, objBusinessPAL.SaturdayDiscount1 != null ? objBusinessPAL.SaturdayDiscount1 : 0m),
    new DbParameter("@SaturdayNoOfItems2", DbParameter.DbType.Int, 4, objBusinessPAL.SaturdayNoOfItems2 != null ? objBusinessPAL.SaturdayNoOfItems2 : 0),
    new DbParameter("@SaturdayOriginalPrice2", DbParameter.DbType.Decimal, 18, objBusinessPAL.SaturdayOriginalPrice2 != null ? objBusinessPAL.SaturdayOriginalPrice2 : 0m),
    new DbParameter("@SaturdayDiscount2", DbParameter.DbType.Decimal, 18, objBusinessPAL.SaturdayDiscount2 != null ? objBusinessPAL.SaturdayDiscount2 : 0m),
    new DbParameter("@SaturdayNoOfItems3", DbParameter.DbType.Int, 4, objBusinessPAL.SaturdayNoOfItems3 != null ? objBusinessPAL.SaturdayNoOfItems3 : 0),
    new DbParameter("@SaturdayOriginalPrice3", DbParameter.DbType.Decimal, 18, objBusinessPAL.SaturdayOriginalPrice3 != null ? objBusinessPAL.SaturdayOriginalPrice3 : 0m),
    new DbParameter("@SaturdayDiscount3", DbParameter.DbType.Decimal, 18, objBusinessPAL.SaturdayDiscount3 != null ? objBusinessPAL.SaturdayDiscount3 : 0m),

    // Sunday
    new DbParameter("@SundayPickUpFromTime", DbParameter.DbType.VarChar, 8, objBusinessPAL.SundayPickUpFromTime != null ? objBusinessPAL.SundayPickUpFromTime : ""),
    new DbParameter("@SundayPickUpToTime", DbParameter.DbType.VarChar, 8, objBusinessPAL.SundayPickUpToTime != null ? objBusinessPAL.SundayPickUpToTime : ""),

    new DbParameter("@SundayNoOfItems1", DbParameter.DbType.Int, 4, objBusinessPAL.SundayNoOfItems1 != null ? objBusinessPAL.SundayNoOfItems1 : 0),
    new DbParameter("@SundayOriginalPrice1", DbParameter.DbType.Decimal, 18, objBusinessPAL.SundayOriginalPrice1 != null ? objBusinessPAL.SundayOriginalPrice1 : 0m),
    new DbParameter("@SundayDiscount1", DbParameter.DbType.Decimal, 18, objBusinessPAL.SundayDiscount1 != null ? objBusinessPAL.SundayDiscount1 : 0m),
    new DbParameter("@SundayNoOfItems2", DbParameter.DbType.Int, 4, objBusinessPAL.SundayNoOfItems2 != null ? objBusinessPAL.SundayNoOfItems2 : 0),
    new DbParameter("@SundayOriginalPrice2", DbParameter.DbType.Decimal, 18, objBusinessPAL.SundayOriginalPrice2 != null ? objBusinessPAL.SundayOriginalPrice2 : 0m),
    new DbParameter("@SundayDiscount2", DbParameter.DbType.Decimal, 18, objBusinessPAL.SundayDiscount2 != null ? objBusinessPAL.SundayDiscount2 : 0m),
    new DbParameter("@SundayNoOfItems3", DbParameter.DbType.Int, 4, objBusinessPAL.SundayNoOfItems3 != null ? objBusinessPAL.SundayNoOfItems3 : 0),
    new DbParameter("@SundayOriginalPrice3", DbParameter.DbType.Decimal, 18, objBusinessPAL.SundayOriginalPrice3 != null ? objBusinessPAL.SundayOriginalPrice3 : 0m),
    new DbParameter("@SundayDiscount3", DbParameter.DbType.Decimal, 18, objBusinessPAL.SundayDiscount3 != null ? objBusinessPAL.SundayDiscount3 : 0m),

    new DbParameter("@State", DbParameter.DbType.VarChar, 10, objBusinessPAL.State != null ? objBusinessPAL.State : ""),
    new DbParameter("@AboutUs", DbParameter.DbType.VarChar, 500, objBusinessPAL.AboutUs != null ? objBusinessPAL.AboutUs : "")
 };

        DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "BusinessModifyAdmin", dbParam);
        return 1;
    }
    #endregion
}