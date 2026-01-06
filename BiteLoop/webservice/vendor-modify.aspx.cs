using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using Utility;
using DAL;
using BiteLoop.Common;
using System.Configuration;
using System.Text;

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

        Int64.TryParse(Request["id"], out _ID);
        objBusinessBAL.ID = ID;

        if (Request.Form.Keys.Count > 0)
        {
            SaveInfo();
        }
        else
        {
            BindDropdownsAndControls();
            BindState();
            if (objBusinessBAL.ID != 0)
            {
                tbPassword.Visible = true;
                BindControls();
            }
            else
            {
                tbPassword.Visible = true;
            }
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
                rptFoodItems.DataSource = ds.Tables[2];
                rptFoodItems.DataBind();
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                rptCategory.DataSource = ds.Tables[0];
                rptCategory.DataBind();
            }
            if (ds.Tables[4].Rows.Count > 0)
            {
                rptRestaurantTypes.DataSource = ds.Tables[4];
                rptRestaurantTypes.DataBind();
            }
            if (ds.Tables[3].Rows.Count > 0)
            {
                rptProfilePhotos.DataSource = ds.Tables[3];
                rptProfilePhotos.DataBind();
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
            txtABN.Value = Convert.ToString(ds.Tables[0].Rows[0]["ABN"]);
            txtStreetAddress.Value = Convert.ToString(ds.Tables[0].Rows[0]["StreetAddress"]);
            txtLocation.Value = Convert.ToString(ds.Tables[0].Rows[0]["Location"]);
            hdnLocation.Value = Convert.ToString(ds.Tables[0].Rows[0]["Location"]);
            txtFullName.Value = Convert.ToString(ds.Tables[0].Rows[0]["FullName"]);
            txtBusinessPhone.Value = Convert.ToString(ds.Tables[0].Rows[0]["BusinessPhone"]);
            txtphone.Value = Convert.ToString(ds.Tables[0].Rows[0]["Mobile"]);
            txtEmail.Value = Convert.ToString(ds.Tables[0].Rows[0]["EmailAddress"]);

            StringBuilder sbTemplate = new StringBuilder(Convert.ToString(ds.Tables[0].Rows[0]["Description"]));
            sbTemplate.Replace("{%WebSiteUrl%}", Config.WebSiteUrl);
            tareaDescription.Text = sbTemplate.ToString();

            txtBSBNo.Value = Convert.ToString(ds.Tables[0].Rows[0]["BSBNo"]);
            txtAccountNo.Value = Convert.ToString(ds.Tables[0].Rows[0]["AccountNumber"]);
            txtBankName.Value = Convert.ToString(ds.Tables[0].Rows[0]["BankName"]);
            txtAccountName.Value = Convert.ToString(ds.Tables[0].Rows[0]["AccountName"]);

            trConfirmPwd.Visible = false;
            divlblPwd.Visible = true;
            divPwd.Style.Add("display", "none");
            txtPassword.Attributes.Add("value", "");
            lblPassword.InnerText = "";

            txtComission.Value = "";
            txtPersonIncharge.Value = "";
            ddlStatus.Value = Convert.ToString(ds.Tables[0].Rows[0]["Status"]);
            txtLastName.Value = "";
            txtBMHComission.Value = "";
            ddlMultipleStore.Value = "";
            ddlBYOContainers.Value = "";
            ddlState.Value = Convert.ToString(ds.Tables[0].Rows[0]["StateCode"]);

            txtLatitude.Value = "";
            txtLongitude.Value = "";

            if (!string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["ProfilePhotoID"])))
                SelectedProfilePhotoID = Convert.ToInt32(ds.Tables[0].Rows[0]["ProfilePhotoID"]);

            // Categories
            string strCategory = string.Join(",", ds.Tables[1].AsEnumerable().Select(r => r["ID"].ToString()));
            hdnCategory.Value = strCategory;

            // Food Items
            string strFoodItems = string.Join(",", ds.Tables[2].AsEnumerable().Select(r => r["ID"].ToString()));
            hdnFoodItems.Value = strFoodItems;

            // Restaurant Types
            if (ds.Tables.Count > 4 && ds.Tables[4].Rows.Count > 0)
            {
                string strRestaurantTypes = string.Join(",",
                    ds.Tables[4].AsEnumerable().Select(r => r["ID"].ToString()));
                hdnRestaurantTypes.Value = strRestaurantTypes;
            }
            else
            {
                hdnRestaurantTypes.Value = "";
            }

            // IMPORTANT: All schedule-binding code REMOVED (Option A)
            // Tables 3, 5, 6 are ignored
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

        BusinessBAL objBusinessBAL = new BusinessBAL();
        objBusinessBAL.ID = Convert.ToInt64(ID);
        objBusinessBAL.Name = Convert.ToString(Request[txtBusinessName.UniqueID]);
        objBusinessBAL.ABN = Convert.ToString(Request[txtABN.UniqueID]);
        objBusinessBAL.StreetAddress = Convert.ToString(Request[txtStreetAddress.UniqueID]);
        objBusinessBAL.Location = Convert.ToString(Request[txtLocation.UniqueID]);
        objBusinessBAL.FullName = Convert.ToString(Request[txtFullName.UniqueID]);
        objBusinessBAL.BusinessPhone = Convert.ToString(Request[txtBusinessPhone.UniqueID]);
        objBusinessBAL.Mobile = Convert.ToString(Request[txtphone.UniqueID]);
        objBusinessBAL.EmailAddress = Convert.ToString(Request[txtEmail.UniqueID]);
        //objBusinessBAL.ProfilePhotoID = Convert.ToInt32(Request["ProfilePhotoID"]);
        //objBusinessBAL.Description = Convert.ToString(Request[tareaDescription.UniqueID]);

        System.Text.StringBuilder sbTemplate = new System.Text.StringBuilder(Server.HtmlDecode(Request[hdnContent.UniqueID]));
        sbTemplate.Replace(Config.WebSiteUrl, "{%WebSiteUrl%}");
        objBusinessBAL.Description = sbTemplate.ToString();

        objBusinessBAL.BSBNo = Convert.ToString(Request[txtBSBNo.UniqueID]);
        objBusinessBAL.AccountNumber = Convert.ToString(Request[txtAccountNo.UniqueID]);
        objBusinessBAL.BankName = Convert.ToString(Request[txtBankName.UniqueID]);
        objBusinessBAL.AccountName = Convert.ToString(Request[txtAccountName.UniqueID]);
        //objBusinessBAL.Status = Convert.ToInt32(Request["Status"]);
        objBusinessBAL.BusinessType = Convert.ToString(Request[hdnCategory.UniqueID]);
        objBusinessBAL.FoodItems = Convert.ToString(Request[hdnFoodItems.UniqueID]);
        objBusinessBAL.RestaurantTypes = Convert.ToString(Request[hdnRestaurantTypes.UniqueID]);
        objBusinessBAL.Password = Utility.Security.EncryptDescrypt.EncryptString(Convert.ToString(Request[txtPassword.UniqueID]));


        // objBusinessBAL.Latitude = Convert.ToString(Request["Latitude"]);
        //objBusinessBAL.Longitude = Convert.ToString(Request["Longitude"]);
        //objBusinessBAL.PostCode = Convert.ToString(Request["PostCode"]);



        #region Pickup Time 1
        string MondaySchedule = string.Empty, TuesdaySchedule = string.Empty, WednesdaySchedule = string.Empty, ThirsdaySchedule = string.Empty, FridaySchedule = string.Empty, SaturdaySchedule = string.Empty, SundaySchedule = string.Empty;

        if (Request[MondayOn.UniqueID] != null)
        {
            if (Convert.ToInt32(Request[MondayOn.UniqueID]) == 1)
            {
                string MDiscount = "0.00";
                if (Convert.ToString(Request[MondayDiscount.UniqueID]) != string.Empty && Convert.ToString(Request[MondayOriginalPrice.UniqueID]) != string.Empty)
                {
                    MDiscount = Convert.ToDouble(Convert.ToDouble(Request[MondayOriginalPrice.UniqueID]) - Convert.ToDouble(Request[MondayDiscount.UniqueID])).ToString("f2");
                }
                MondaySchedule = Convert.ToString(Request[MondayNoOfItems.UniqueID]) + "##" + Convert.ToString(Request[MondayOriginalPrice.UniqueID]) + "##" + Convert.ToString(MDiscount) + "##01/01/1990 " + Convert.ToString(Request[MondayFromTime.UniqueID]) + "##01/01/1990 " + Convert.ToString(Request[MondayToTime.UniqueID]);
            }
        }
        objBusinessBAL.MondaySchedule = MondaySchedule;



        if (Request[TuesdayOn.UniqueID] != null)
        {
            if (Convert.ToInt32(Request[TuesdayOn.UniqueID]) == 1)
            {
                string TDiscount = "0.00";
                if (Convert.ToString(Request[TuesdayDiscount.UniqueID]) != string.Empty && Convert.ToString(Request[TuesdayOriginalPrice.UniqueID]) != string.Empty)
                {
                    TDiscount = Convert.ToDouble(Convert.ToDouble(Request[TuesdayOriginalPrice.UniqueID]) - Convert.ToDouble(Request[TuesdayDiscount.UniqueID])).ToString("f2");
                }
                TuesdaySchedule = Convert.ToString(Request[TuesdayNoOfItems.UniqueID]) + "##" + Convert.ToString(Request[TuesdayOriginalPrice.UniqueID]) + "##" + Convert.ToString(TDiscount) + "##01/01/1990 " + Convert.ToString(Request[TuesdayFromTime.UniqueID]) + "##01/01/1990 " + Convert.ToString(Request[TuesdayToTime.UniqueID]);
            }
        }

        objBusinessBAL.TuesdaySchedule = TuesdaySchedule;
        if (Request[WednesdayOn.UniqueID] != null)
        {
            if (Convert.ToInt32(Request[WednesdayOn.UniqueID]) == 1)
            {
                string WDiscount = "0.00";
                if (Convert.ToString(Request[WednesdayDiscount.UniqueID]) != string.Empty && Convert.ToString(Request[WednesdayOriginalPrice.UniqueID]) != string.Empty)
                {
                    WDiscount = Convert.ToDouble(Convert.ToDouble(Request[WednesdayOriginalPrice.UniqueID]) - Convert.ToDouble(Request[WednesdayDiscount.UniqueID])).ToString("f2");
                }
                WednesdaySchedule = Convert.ToString(Request[WednesdayNoOfItems.UniqueID]) + "##" + Convert.ToString(Request[WednesdayOriginalPrice.UniqueID]) + "##" + Convert.ToString(WDiscount) + "##01/01/1990 " + Convert.ToString(Request[WednesdayFromTime.UniqueID]) + "##01/01/1990 " + Convert.ToString(Request[WednesdayToTime.UniqueID]);
            }
        }

        objBusinessBAL.WednesdaySchedule = WednesdaySchedule;
        if (Request[ThirsdayOn.UniqueID] != null)
        {
            if (Convert.ToInt32(Request[ThirsdayOn.UniqueID]) == 1)
            {
                string THDiscount = "0.00";
                if (Convert.ToString(Request[ThirsdayDiscount.UniqueID]) != string.Empty && Convert.ToString(Request[ThirsdayOriginalPrice.UniqueID]) != string.Empty)
                {
                    THDiscount = Convert.ToDouble(Convert.ToDouble(Request[ThirsdayOriginalPrice.UniqueID]) - Convert.ToDouble(Request[ThirsdayDiscount.UniqueID])).ToString("f2");
                }
                ThirsdaySchedule = Convert.ToString(Request[ThirsdayNoOfItems.UniqueID]) + "##" + Convert.ToString(Request[ThirsdayOriginalPrice.UniqueID]) + "##" + Convert.ToString(THDiscount) + "##01/01/1990 " + Convert.ToString(Request[ThirsdayFromTime.UniqueID]) + "##01/01/1990 " + Convert.ToString(Request[ThirsdayToTime.UniqueID]);
            }
        }

        objBusinessBAL.ThirsdaySchedule = ThirsdaySchedule;
        if (Request[FridayOn.UniqueID] != null)
        {
            if (Convert.ToInt32(Request[FridayOn.UniqueID]) == 1)
            {
                string FDiscount = "0.00";
                if (Convert.ToString(Request[FridayDiscount.UniqueID]) != string.Empty && Convert.ToString(Request[FridayOriginalPrice.UniqueID]) != string.Empty)
                {
                    FDiscount = Convert.ToDouble(Convert.ToDouble(Request[FridayOriginalPrice.UniqueID]) - Convert.ToDouble(Request[FridayDiscount.UniqueID])).ToString("f2");
                }
                FridaySchedule = Convert.ToString(Request[FridayNoOfItems.UniqueID]) + "##" + Convert.ToString(Request[FridayOriginalPrice.UniqueID]) + "##" + Convert.ToString(FDiscount) + "##01/01/1990 " + Convert.ToString(Request[FridayFromTime.UniqueID]) + "##01/01/1990 " + Convert.ToString(Request[FridayToTime.UniqueID]);
            }
        }

        objBusinessBAL.FridaySchedule = FridaySchedule;
        if (Request[SaturdayOn.UniqueID] != null)
        {
            if (Convert.ToInt32(Request[SaturdayOn.UniqueID]) == 1)
            {
                string SADiscount = "0.00";
                if (Convert.ToString(Request[SaturdayDiscount.UniqueID]) != string.Empty && Convert.ToString(Request[SaturdayOriginalPrice.UniqueID]) != string.Empty)
                {
                    SADiscount = Convert.ToDouble(Convert.ToDouble(Request[SaturdayOriginalPrice.UniqueID]) - Convert.ToDouble(Request[SaturdayDiscount.UniqueID])).ToString("f2");
                }
                SaturdaySchedule = Convert.ToString(Request[SaturdayNoOfItems.UniqueID]) + "##" + Convert.ToString(Request[SaturdayOriginalPrice.UniqueID]) + "##" + Convert.ToString(SADiscount) + "##01/01/1990 " + Convert.ToString(Request[SaturdayFromTime.UniqueID]) + "##01/01/1990 " + Convert.ToString(Request[SaturdayToTime.UniqueID]);
            }
        }

        objBusinessBAL.SaturdaySchedule = SaturdaySchedule;
        if (Request[SundayOn.UniqueID] != null)
        {
            if (Convert.ToInt32(Request[SundayOn.UniqueID]) == 1)
            {
                string SUDiscount = "0.00";
                if (Convert.ToString(Request[SundayDiscount.UniqueID]) != string.Empty && Convert.ToString(Request[SundayOriginalPrice.UniqueID]) != string.Empty)
                {
                    SUDiscount = Convert.ToDouble(Convert.ToDouble(Request[SundayOriginalPrice.UniqueID]) - Convert.ToDouble(Request[SundayDiscount.UniqueID])).ToString("f2");
                }
                SundaySchedule = Convert.ToString(Request[SundayNoOfItems.UniqueID]) + "##" + Convert.ToString(Request[SundayOriginalPrice.UniqueID]) + "##" + Convert.ToString(SUDiscount) + "##01/01/1990 " + Convert.ToString(Request[SundayFromTime.UniqueID]) + "##01/01/1990 " + Convert.ToString(Request[SundayToTime.UniqueID]);
            }
        }
        objBusinessBAL.SundaySchedule = SundaySchedule;

        #endregion


        #region Pickup Time 2
        string Monday2Schedule = string.Empty, Tuesday2Schedule = string.Empty, Wednesday2Schedule = string.Empty, Thirsday2Schedule = string.Empty, Friday2Schedule = string.Empty, Saturday2Schedule = string.Empty, Sunday2Schedule = string.Empty;

        if (Request[Monday2On.UniqueID] != null)
        {
            if (Convert.ToInt32(Request[Monday2On.UniqueID]) == 1)
            {
                string MDiscount = "0.00";
                if (Convert.ToString(Request[Monday2Discount.UniqueID]) != string.Empty && Convert.ToString(Request[Monday2OriginalPrice.UniqueID]) != string.Empty)
                {
                    MDiscount = Convert.ToDouble(Convert.ToDouble(Request[Monday2OriginalPrice.UniqueID]) - Convert.ToDouble(Request[Monday2Discount.UniqueID])).ToString("f2");
                }
                Monday2Schedule = Convert.ToString(Request[Monday2NoOfItems.UniqueID]) + "##" + Convert.ToString(Request[Monday2OriginalPrice.UniqueID]) + "##" + Convert.ToString(MDiscount) + "##01/01/1990 " + Convert.ToString(Request[Monday2FromTime.UniqueID]) + "##01/01/1990 " + Convert.ToString(Request[Monday2ToTime.UniqueID]);
            }
        }
        objBusinessBAL.Monday2Schedule = Monday2Schedule;



        if (Request[Tuesday2On.UniqueID] != null)
        {
            if (Convert.ToInt32(Request[Tuesday2On.UniqueID]) == 1)
            {
                string TDiscount = "0.00";
                if (Convert.ToString(Request[Tuesday2Discount.UniqueID]) != string.Empty && Convert.ToString(Request[Tuesday2OriginalPrice.UniqueID]) != string.Empty)
                {
                    TDiscount = Convert.ToDouble(Convert.ToDouble(Request[Tuesday2OriginalPrice.UniqueID]) - Convert.ToDouble(Request[Tuesday2Discount.UniqueID])).ToString("f2");
                }
                Tuesday2Schedule = Convert.ToString(Request[Tuesday2NoOfItems.UniqueID]) + "##" + Convert.ToString(Request[Tuesday2OriginalPrice.UniqueID]) + "##" + Convert.ToString(TDiscount) + "##01/01/1990 " + Convert.ToString(Request[Tuesday2FromTime.UniqueID]) + "##01/01/1990 " + Convert.ToString(Request[Tuesday2ToTime.UniqueID]);
            }
        }

        objBusinessBAL.Tuesday2Schedule = Tuesday2Schedule;
        if (Request[Wednesday2On.UniqueID] != null)
        {
            if (Convert.ToInt32(Request[Wednesday2On.UniqueID]) == 1)
            {
                string WDiscount = "0.00";
                if (Convert.ToString(Request[Wednesday2Discount.UniqueID]) != string.Empty && Convert.ToString(Request[Wednesday2OriginalPrice.UniqueID]) != string.Empty)
                {
                    WDiscount = Convert.ToDouble(Convert.ToDouble(Request[Wednesday2OriginalPrice.UniqueID]) - Convert.ToDouble(Request[Wednesday2Discount.UniqueID])).ToString("f2");
                }
                Wednesday2Schedule = Convert.ToString(Request[Wednesday2NoOfItems.UniqueID]) + "##" + Convert.ToString(Request[Wednesday2OriginalPrice.UniqueID]) + "##" + Convert.ToString(WDiscount) + "##01/01/1990 " + Convert.ToString(Request[Wednesday2FromTime.UniqueID]) + "##01/01/1990 " + Convert.ToString(Request[Wednesday2ToTime.UniqueID]);
            }
        }

        objBusinessBAL.Wednesday2Schedule = Wednesday2Schedule;
        if (Request[Thirsday2On.UniqueID] != null)
        {
            if (Convert.ToInt32(Request[Thirsday2On.UniqueID]) == 1)
            {
                string THDiscount = "0.00";
                if (Convert.ToString(Request[Thirsday2Discount.UniqueID]) != string.Empty && Convert.ToString(Request[Thirsday2OriginalPrice.UniqueID]) != string.Empty)
                {
                    THDiscount = Convert.ToDouble(Convert.ToDouble(Request[Thirsday2OriginalPrice.UniqueID]) - Convert.ToDouble(Request[Thirsday2Discount.UniqueID])).ToString("f2");
                }
                Thirsday2Schedule = Convert.ToString(Request[Thirsday2NoOfItems.UniqueID]) + "##" + Convert.ToString(Request[Thirsday2OriginalPrice.UniqueID]) + "##" + Convert.ToString(THDiscount) + "##01/01/1990 " + Convert.ToString(Request[Thirsday2FromTime.UniqueID]) + "##01/01/1990 " + Convert.ToString(Request[Thirsday2ToTime.UniqueID]);
            }
        }

        objBusinessBAL.Thirsday2Schedule = Thirsday2Schedule;
        if (Request[Friday2On.UniqueID] != null)
        {
            if (Convert.ToInt32(Request[Friday2On.UniqueID]) == 1)
            {
                string FDiscount = "0.00";
                if (Convert.ToString(Request[Friday2Discount.UniqueID]) != string.Empty && Convert.ToString(Request[Friday2OriginalPrice.UniqueID]) != string.Empty)
                {
                    FDiscount = Convert.ToDouble(Convert.ToDouble(Request[Friday2OriginalPrice.UniqueID]) - Convert.ToDouble(Request[Friday2Discount.UniqueID])).ToString("f2");
                }
                Friday2Schedule = Convert.ToString(Request[Friday2NoOfItems.UniqueID]) + "##" + Convert.ToString(Request[Friday2OriginalPrice.UniqueID]) + "##" + Convert.ToString(FDiscount) + "##01/01/1990 " + Convert.ToString(Request[Friday2FromTime.UniqueID]) + "##01/01/1990 " + Convert.ToString(Request[Friday2ToTime.UniqueID]);
            }
        }

        objBusinessBAL.Friday2Schedule = Friday2Schedule;
        if (Request[Saturday2On.UniqueID] != null)
        {
            if (Convert.ToInt32(Request[Saturday2On.UniqueID]) == 1)
            {
                string SADiscount = "0.00";
                if (Convert.ToString(Request[Saturday2Discount.UniqueID]) != string.Empty && Convert.ToString(Request[Saturday2OriginalPrice.UniqueID]) != string.Empty)
                {
                    SADiscount = Convert.ToDouble(Convert.ToDouble(Request[Saturday2OriginalPrice.UniqueID]) - Convert.ToDouble(Request[Saturday2Discount.UniqueID])).ToString("f2");
                }
                Saturday2Schedule = Convert.ToString(Request[Saturday2NoOfItems.UniqueID]) + "##" + Convert.ToString(Request[Saturday2OriginalPrice.UniqueID]) + "##" + Convert.ToString(SADiscount) + "##01/01/1990 " + Convert.ToString(Request[Saturday2FromTime.UniqueID]) + "##01/01/1990 " + Convert.ToString(Request[Saturday2ToTime.UniqueID]);
            }
        }

        objBusinessBAL.Saturday2Schedule = Saturday2Schedule;
        if (Request[Sunday2On.UniqueID] != null)
        {
            if (Convert.ToInt32(Request[Sunday2On.UniqueID]) == 1)
            {
                string SUDiscount = "0.00";
                if (Convert.ToString(Request[Sunday2Discount.UniqueID]) != string.Empty && Convert.ToString(Request[Sunday2OriginalPrice.UniqueID]) != string.Empty)
                {
                    SUDiscount = Convert.ToDouble(Convert.ToDouble(Request[Sunday2OriginalPrice.UniqueID]) - Convert.ToDouble(Request[Sunday2Discount.UniqueID])).ToString("f2");
                }
                Sunday2Schedule = Convert.ToString(Request[Sunday2NoOfItems.UniqueID]) + "##" + Convert.ToString(Request[Sunday2OriginalPrice.UniqueID]) + "##" + Convert.ToString(SUDiscount) + "##01/01/1990 " + Convert.ToString(Request[Sunday2FromTime.UniqueID]) + "##01/01/1990 " + Convert.ToString(Request[Sunday2ToTime.UniqueID]);
            }
        }
        objBusinessBAL.Sunday2Schedule = Sunday2Schedule;

        #endregion

        #region Pickup Time 3
        string Monday3Schedule = string.Empty, Tuesday3Schedule = string.Empty, Wednesday3Schedule = string.Empty, Thirsday3Schedule = string.Empty, Friday3Schedule = string.Empty, Saturday3Schedule = string.Empty, Sunday3Schedule = string.Empty;

        if (Request[Monday3On.UniqueID] != null)
        {
            if (Convert.ToInt32(Request[Monday3On.UniqueID]) == 1)
            {
                string MDiscount = "0.00";
                if (Convert.ToString(Request[Monday3Discount.UniqueID]) != string.Empty && Convert.ToString(Request[Monday3OriginalPrice.UniqueID]) != string.Empty)
                {
                    MDiscount = Convert.ToDouble(Convert.ToDouble(Request[Monday3OriginalPrice.UniqueID]) - Convert.ToDouble(Request[Monday3Discount.UniqueID])).ToString("f2");
                }
                Monday3Schedule = Convert.ToString(Request[Monday3NoOfItems.UniqueID]) + "##" + Convert.ToString(Request[Monday3OriginalPrice.UniqueID]) + "##" + Convert.ToString(MDiscount) + "##01/01/1990 " + Convert.ToString(Request[Monday3FromTime.UniqueID]) + "##01/01/1990 " + Convert.ToString(Request[Monday3ToTime.UniqueID]);
            }
        }
        objBusinessBAL.Monday3Schedule = Monday3Schedule;



        if (Request[Tuesday3On.UniqueID] != null)
        {
            if (Convert.ToInt32(Request[Tuesday3On.UniqueID]) == 1)
            {
                string TDiscount = "0.00";
                if (Convert.ToString(Request[Tuesday3Discount.UniqueID]) != string.Empty && Convert.ToString(Request[Tuesday3OriginalPrice.UniqueID]) != string.Empty)
                {
                    TDiscount = Convert.ToDouble(Convert.ToDouble(Request[Tuesday3OriginalPrice.UniqueID]) - Convert.ToDouble(Request[Tuesday3Discount.UniqueID])).ToString("f2");
                }
                Tuesday3Schedule = Convert.ToString(Request[Tuesday3NoOfItems.UniqueID]) + "##" + Convert.ToString(Request[Tuesday3OriginalPrice.UniqueID]) + "##" + Convert.ToString(TDiscount) + "##01/01/1990 " + Convert.ToString(Request[Tuesday3FromTime.UniqueID]) + "##01/01/1990 " + Convert.ToString(Request[Tuesday3ToTime.UniqueID]);
            }
        }

        objBusinessBAL.Tuesday3Schedule = Tuesday3Schedule;
        if (Request[Wednesday3On.UniqueID] != null)
        {
            if (Convert.ToInt32(Request[Wednesday3On.UniqueID]) == 1)
            {
                string WDiscount = "0.00";
                if (Convert.ToString(Request[Wednesday3Discount.UniqueID]) != string.Empty && Convert.ToString(Request[Wednesday3OriginalPrice.UniqueID]) != string.Empty)
                {
                    WDiscount = Convert.ToDouble(Convert.ToDouble(Request[Wednesday3OriginalPrice.UniqueID]) - Convert.ToDouble(Request[Wednesday3Discount.UniqueID])).ToString("f2");
                }
                Wednesday3Schedule = Convert.ToString(Request[Wednesday3NoOfItems.UniqueID]) + "##" + Convert.ToString(Request[Wednesday3OriginalPrice.UniqueID]) + "##" + Convert.ToString(WDiscount) + "##01/01/1990 " + Convert.ToString(Request[Wednesday3FromTime.UniqueID]) + "##01/01/1990 " + Convert.ToString(Request[Wednesday3ToTime.UniqueID]);
            }
        }

        objBusinessBAL.Wednesday3Schedule = Wednesday3Schedule;
        if (Request[Thirsday3On.UniqueID] != null)
        {
            if (Convert.ToInt32(Request[Thirsday3On.UniqueID]) == 1)
            {
                string THDiscount = "0.00";
                if (Convert.ToString(Request[Thirsday3Discount.UniqueID]) != string.Empty && Convert.ToString(Request[Thirsday3OriginalPrice.UniqueID]) != string.Empty)
                {
                    THDiscount = Convert.ToDouble(Convert.ToDouble(Request[Thirsday3OriginalPrice.UniqueID]) - Convert.ToDouble(Request[Thirsday3Discount.UniqueID])).ToString("f2");
                }
                Thirsday3Schedule = Convert.ToString(Request[Thirsday3NoOfItems.UniqueID]) + "##" + Convert.ToString(Request[Thirsday3OriginalPrice.UniqueID]) + "##" + Convert.ToString(THDiscount) + "##01/01/1990 " + Convert.ToString(Request[Thirsday3FromTime.UniqueID]) + "##01/01/1990 " + Convert.ToString(Request[Thirsday3ToTime.UniqueID]);
            }
        }

        objBusinessBAL.Thirsday3Schedule = Thirsday3Schedule;
        if (Request[Friday3On.UniqueID] != null)
        {
            if (Convert.ToInt32(Request[Friday3On.UniqueID]) == 1)
            {
                string FDiscount = "0.00";
                if (Convert.ToString(Request[Friday3Discount.UniqueID]) != string.Empty && Convert.ToString(Request[Friday3OriginalPrice.UniqueID]) != string.Empty)
                {
                    FDiscount = Convert.ToDouble(Convert.ToDouble(Request[Friday3OriginalPrice.UniqueID]) - Convert.ToDouble(Request[Friday3Discount.UniqueID])).ToString("f2");
                }
                Friday3Schedule = Convert.ToString(Request[Friday3NoOfItems.UniqueID]) + "##" + Convert.ToString(Request[Friday3OriginalPrice.UniqueID]) + "##" + Convert.ToString(FDiscount) + "##01/01/1990 " + Convert.ToString(Request[Friday3FromTime.UniqueID]) + "##01/01/1990 " + Convert.ToString(Request[Friday3ToTime.UniqueID]);
            }
        }

        objBusinessBAL.Friday3Schedule = Friday3Schedule;
        if (Request[Saturday3On.UniqueID] != null)
        {
            if (Convert.ToInt32(Request[Saturday3On.UniqueID]) == 1)
            {
                string SADiscount = "0.00";
                if (Convert.ToString(Request[Saturday3Discount.UniqueID]) != string.Empty && Convert.ToString(Request[Saturday3OriginalPrice.UniqueID]) != string.Empty)
                {
                    SADiscount = Convert.ToDouble(Convert.ToDouble(Request[Saturday3OriginalPrice.UniqueID]) - Convert.ToDouble(Request[Saturday3Discount.UniqueID])).ToString("f2");
                }
                Saturday3Schedule = Convert.ToString(Request[Saturday3NoOfItems.UniqueID]) + "##" + Convert.ToString(Request[Saturday3OriginalPrice.UniqueID]) + "##" + Convert.ToString(SADiscount) + "##01/01/1990 " + Convert.ToString(Request[Saturday3FromTime.UniqueID]) + "##01/01/1990 " + Convert.ToString(Request[Saturday3ToTime.UniqueID]);
            }
        }

        objBusinessBAL.Saturday3Schedule = Saturday3Schedule;
        if (Request[Sunday3On.UniqueID] != null)
        {
            if (Convert.ToInt32(Request[Sunday3On.UniqueID]) == 1)
            {
                string SUDiscount = "0.00";
                if (Convert.ToString(Request[Sunday3Discount.UniqueID]) != string.Empty && Convert.ToString(Request[Sunday3OriginalPrice.UniqueID]) != string.Empty)
                {
                    SUDiscount = Convert.ToDouble(Convert.ToDouble(Request[Sunday3OriginalPrice.UniqueID]) - Convert.ToDouble(Request[Sunday3Discount.UniqueID])).ToString("f2");
                }
                Sunday3Schedule = Convert.ToString(Request[Sunday3NoOfItems.UniqueID]) + "##" + Convert.ToString(Request[Sunday3OriginalPrice.UniqueID]) + "##" + Convert.ToString(SUDiscount) + "##01/01/1990 " + Convert.ToString(Request[Sunday3FromTime.UniqueID]) + "##01/01/1990 " + Convert.ToString(Request[Sunday3ToTime.UniqueID]);
            }
        }
        objBusinessBAL.Sunday3Schedule = Sunday3Schedule;

        #endregion

        string strPassword = string.Empty;
        objBusinessBAL.Status = Convert.ToInt32(Request[ddlStatus.UniqueID]);
        int MultipleStore = 0;
        MultipleStore = Convert.ToInt32(Request[ddlMultipleStore.UniqueID]);
        int BYOContainers = 0;
        BYOContainers = Convert.ToInt32(Request[ddlBYOContainers.UniqueID]);

        string LastName = Convert.ToString(Request[txtLastName.UniqueID]);
        string State = Convert.ToString(Request[ddlState.UniqueID]);
        int ProfilePhotoID = Convert.ToInt32(Request[hdnProfilePhotoID.UniqueID]);


        // objBusinessBAL.Latitude = Convert.ToString(Request["Latitude"]);
        //objBusinessBAL.Longitude = Convert.ToString(Request["Longitude"]);


       objBusinessBAL.Latitude = Convert.ToString(Request[txtLatitude.UniqueID]);
       objBusinessBAL.Longitude = Convert.ToString(Request[txtLongitude.UniqueID]);

        // long result = objBusinessBAL.SaveAdmin();
        long result = SaveAdminDAL(objBusinessBAL, Convert.ToString(Request[txtComission.UniqueID]), Convert.ToString(Request[txtPersonIncharge.UniqueID]), Convert.ToDecimal(Request[txtBMHComission.UniqueID]), LastName, MultipleStore, BYOContainers, State, ProfilePhotoID);

        switch (result)
        {
            case -1:
                Response.Write("duplicate");
                break;
            default:
                Response.Write("success");
                break;
        }
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
        Response.End();
    }
    public long SaveAdminDAL(BusinessBAL objBusinessBAL, string Commission, string PersonIncharge, decimal BMHComission, string strLastName, int MultipleStore, int BYOContainers, string State, int ProfilePhotoID)
    {
        DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.Int, 40, objBusinessBAL.ID), 
                new DbParameter("@Name", DbParameter.DbType.VarChar, 200, objBusinessBAL.Name),
                new DbParameter("@ABN", DbParameter.DbType.VarChar, 200, objBusinessBAL.ABN),
                new DbParameter("@StreetAddress", DbParameter.DbType.VarChar, 500, objBusinessBAL.StreetAddress),
                new DbParameter("@Location", DbParameter.DbType.VarChar, 500, objBusinessBAL.Location),
                new DbParameter("@FullName", DbParameter.DbType.VarChar, 200, objBusinessBAL.FullName),
                new DbParameter("@EmailAddress", DbParameter.DbType.VarChar, 200, objBusinessBAL.EmailAddress),
                new DbParameter("@BusinessPhone", DbParameter.DbType.VarChar, 20, objBusinessBAL.BusinessPhone),
                new DbParameter("@Mobile", DbParameter.DbType.VarChar, 500, objBusinessBAL.Mobile),                
                new DbParameter("@Description", DbParameter.DbType.VarChar, 8000, objBusinessBAL.Description),
                new DbParameter("@BSBNo", DbParameter.DbType.VarChar, 200, objBusinessBAL.BSBNo),
                new DbParameter("@AccountNumber", DbParameter.DbType.VarChar, 200, objBusinessBAL.AccountNumber),
                new DbParameter("@BankName", DbParameter.DbType.VarChar, 200, objBusinessBAL.BankName),
                new DbParameter("@AccountName", DbParameter.DbType.VarChar, 200, objBusinessBAL.AccountName),                
                new DbParameter("@BusinessType", DbParameter.DbType.VarChar, 500, objBusinessBAL.BusinessType),
                new DbParameter("@FoodItems", DbParameter.DbType.VarChar, 500, objBusinessBAL.FoodItems),
                new DbParameter("@MondaySchedule", DbParameter.DbType.VarChar, 8000, objBusinessBAL.MondaySchedule),
                new DbParameter("@TuesdaySchedule", DbParameter.DbType.VarChar, 8000, objBusinessBAL.TuesdaySchedule),
                new DbParameter("@WednesdaySchedule", DbParameter.DbType.VarChar, 8000, objBusinessBAL.WednesdaySchedule),
                new DbParameter("@ThirsdaySchedule", DbParameter.DbType.VarChar, 8000, objBusinessBAL.ThirsdaySchedule),
                new DbParameter("@FridaySchedule", DbParameter.DbType.VarChar, 8000, objBusinessBAL.FridaySchedule),
                new DbParameter("@SaturdaySchedule", DbParameter.DbType.VarChar, 8000, objBusinessBAL.SaturdaySchedule),
                new DbParameter("@SundayScheduleOn", DbParameter.DbType.VarChar, 8000, objBusinessBAL.SundaySchedule),                
                new DbParameter("@Monday2Schedule", DbParameter.DbType.VarChar, 8000, objBusinessBAL.Monday2Schedule),
                new DbParameter("@Tuesday2Schedule", DbParameter.DbType.VarChar, 8000, objBusinessBAL.Tuesday2Schedule),
                new DbParameter("@Wednesday2Schedule", DbParameter.DbType.VarChar, 8000, objBusinessBAL.Wednesday2Schedule),
                new DbParameter("@Thirsday2Schedule", DbParameter.DbType.VarChar, 8000, objBusinessBAL.Thirsday2Schedule),
                new DbParameter("@Friday2Schedule", DbParameter.DbType.VarChar, 8000, objBusinessBAL.Friday2Schedule),
                new DbParameter("@Saturday2Schedule", DbParameter.DbType.VarChar, 8000, objBusinessBAL.Saturday2Schedule),
                new DbParameter("@Sunday2ScheduleOn", DbParameter.DbType.VarChar, 8000, objBusinessBAL.Sunday2Schedule),                
                new DbParameter("@Monday3Schedule", DbParameter.DbType.VarChar, 8000, objBusinessBAL.Monday3Schedule),
                new DbParameter("@Tuesday3Schedule", DbParameter.DbType.VarChar, 8000, objBusinessBAL.Tuesday3Schedule),
                new DbParameter("@Wednesday3Schedule", DbParameter.DbType.VarChar, 8000, objBusinessBAL.Wednesday3Schedule),
                new DbParameter("@Thirsday3Schedule", DbParameter.DbType.VarChar, 8000, objBusinessBAL.Thirsday3Schedule),
                new DbParameter("@Friday3Schedule", DbParameter.DbType.VarChar, 8000, objBusinessBAL.Friday3Schedule),
                new DbParameter("@Saturday3Schedule", DbParameter.DbType.VarChar, 8000, objBusinessBAL.Saturday3Schedule),
                new DbParameter("@Sunday3ScheduleOn", DbParameter.DbType.VarChar, 8000, objBusinessBAL.Sunday3Schedule),
                new DbParameter("@Password", DbParameter.DbType.VarChar, 500, objBusinessBAL.Password),      
                new DbParameter("@Latitude", DbParameter.DbType.VarChar, 500, objBusinessBAL.Latitude),                
                new DbParameter("@Longitude", DbParameter.DbType.VarChar, 500, objBusinessBAL.Longitude),                
                new DbParameter("@PostCode", DbParameter.DbType.VarChar, 20, objBusinessBAL.PostCode),
                new DbParameter("@RestaurantTypes", DbParameter.DbType.VarChar, 80000, objBusinessBAL.RestaurantTypes),
                new DbParameter("@Commission", DbParameter.DbType.VarChar, 80000, Commission),
                new DbParameter("@Status", DbParameter.DbType.VarChar, 80000, objBusinessBAL.Status),
                new DbParameter("@PersonIncharge", DbParameter.DbType.VarChar, 80000, PersonIncharge),
                new DbParameter("@BMHComissionRate", DbParameter.DbType.Decimal, 80, BMHComission),
                new DbParameter("@LastName", DbParameter.DbType.VarChar, 800, strLastName),
                new DbParameter("@MultipleStore", DbParameter.DbType.Int, 4, MultipleStore),
                new DbParameter("@BYOContainers", DbParameter.DbType.Int, 4, BYOContainers),
                new DbParameter("@State", DbParameter.DbType.VarChar, 40, State),
                new DbParameter("@ProfilePhotoID", DbParameter.DbType.Int, 40, ProfilePhotoID),                
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output
                )                
            };
        DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "BusinessModifyAdmin", dbParam);
        return Convert.ToInt64(dbParam[dbParam.Length - 1].Value);
    }
    #endregion
}