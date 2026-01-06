using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using System.Threading;
using System.Globalization;
using BiteLoop.Common;
using Utility;

public partial class AdminPanel_Vendor_holiday_List : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["BindBusiness"] != null)
        {
            BindPartners();
        }

        if (Request["DeleteCustomHoliday"] != null)
        {
            DeleteCustomHoliday();
        }
        if (Request["SavePublicHoliday"] != null)
        {
            SavePublicHoliday();
        }
        if (Request["BindHolidays"] != null)
        {
            BindPublicHolidays();
        }
        if (Request["SaveCustomHoliday"] != null)
        {
            SaveCustomHoliday();
        }


        BindPartners();
        BindState();
    }
    private void BindPartners()
    {
        UsersBAL objUsersBAL = new UsersBAL();
        DataSet ds = new DataSet();
        ds = objUsersBAL.BusinessUsersDropdown();
        DataView dv = new DataView(ds.Tables[0]);


        if (Request["BindBusiness"] != null)
        {
            if (Convert.ToString(Request["state"]) != "")
                dv.RowFilter = "State = '" + Convert.ToString(Request["state"]).ToUpper() + "' AND Name <> ''";
            else
                dv.RowFilter = "Name <> ''";
        }
        else
        {
            dv.RowFilter = "Name <> ''";
        }
        if (dv.Count > 0)
        {
            ddlVendor.DataSource = dv;
            ddlVendor.DataTextField = "Name";
            ddlVendor.DataValueField = "ID";
            ddlVendor.DataBind();
        }
        ddlVendor.Items.Insert(0, new ListItem("-- Select Vendor --", "0"));

        if (Request["BindBusiness"] != null)
        {
            Response.Write(Common.RenderControl(divBusinesses, Common.RenderControlName.HtmlGeneric));
            Response.End();
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
        ddlState.Items.Insert(0, new ListItem("-- All States --", ""));
    }


    #region Public Holidays

    private void SavePublicHoliday()
    {
        CommonBAL objCommonBAL = new CommonBAL();
        objCommonBAL.BusinessHolidaySave_Webservice(Convert.ToInt64(Request["BusinessID"]), Convert.ToInt32(Request["StateHolidayID"]), Convert.ToInt32(Request["OnOff"]));
        Response.Write("success");
        Response.End();
    }
    private void BindPublicHolidays()
    {
        DataTable dt = new DataTable();
        CommonBAL objCommonBAL = new CommonBAL();
        dt = objCommonBAL.BusinessPublicHolidayList_Webservice(Convert.ToInt64(Request["BusinessID"]), 0);
        if (dt.Rows.Count > 0)
        {
            rptPublicHolidays.DataSource = dt;
            rptPublicHolidays.DataBind();
        }

        dt = objCommonBAL.BusinessCustomHolidayList_Webservice(Convert.ToInt64(Request["BusinessID"]));
        if (dt.Rows.Count > 0)
        {
            rptCustomHolidays.DataSource = dt;
            rptCustomHolidays.DataBind();
        }



        Response.Write(Common.RenderControl(divVendorHolidays, Common.RenderControlName.HtmlGeneric));
        Response.End();

    }
    private void DeleteCustomHoliday()
    {
        CommonBAL objCommonBAL = new CommonBAL();
        objCommonBAL.BusinessCustomHolidayDelete_Webservice(Convert.ToInt64(Request["ID"]), Convert.ToInt64(Request["BusinessID"]));
        DataTable dt = new DataTable();
        dt = objCommonBAL.BusinessCustomHolidayList_Webservice(Convert.ToInt64(Request["BusinessID"]));
        if (dt.Rows.Count > 0)
        {
            rptCustomHolidays.DataSource = dt;
            rptCustomHolidays.DataBind();
        }

        Response.Write(Common.RenderControl(divCustomHolidays, Common.RenderControlName.HtmlGeneric));
        Response.End();
    }
    private void SaveCustomHoliday()
    {
        CommonBAL objCommonBAL = new CommonBAL();
        objCommonBAL.BusinessCustomHolidaySave_Webservice(Convert.ToInt64(Request["ID"]), Convert.ToInt64(Request["BusinessID"]), Convert.ToDateTime(Request["FromDate"]), Convert.ToDateTime(Request["ToDate"]), Convert.ToString(Request["Title"]));

        DataTable dt = new DataTable();
        dt = objCommonBAL.BusinessCustomHolidayList_Webservice(Convert.ToInt64(Request["BusinessID"]));
        if (dt.Rows.Count > 0)
        {
            rptCustomHolidays.DataSource = dt;
            rptCustomHolidays.DataBind();
        }

        Response.Write(Common.RenderControl(divCustomHolidays, Common.RenderControlName.HtmlGeneric));
        Response.End();
    }
    #endregion
}