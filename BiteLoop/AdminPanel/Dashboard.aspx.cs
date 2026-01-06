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
using System.Net;
using System.IO;
using BiteLoop.Common;
public partial class AdminPanel_Dashboard : AdminAuthentication
{
    public string strIPAddress = string.Empty;
    public string strDate = string.Empty;
    protected int AdminType = 2;
    protected string UserName = string.Empty;

    protected string SoldQty = "0";
    protected string TotalAmountBeforeBMH = "0";
    protected string BMH = "0";
    protected string GrandTotal = "0";
    protected string Donation = "0";
    protected string TransactionFee = "0";

    protected string ReportBMH = "";
    protected string ReportMonths = "";
    protected string ReportGrandTotal = "";
    protected string ReportTotal = "";
    protected string ReportTotalQty = "";
    protected string ReportTotalDonation = "";
    protected string ReportTotalSignedIns = "";

    protected string ReportDisplayBMH = "";
    protected string ReportDisplayGrandTotal = "";
    protected string ReportDisplayTotal = "";
    protected string ReportDisplayDonation = "";
    

    protected string UserSignedIn = "";
    protected string BusinessSignedIn = "";
    protected string SelectedState = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["yearlyreport"] != null)
            {
                DashBoardReport(Convert.ToInt32(Request["year"]));
            }

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            AdminBAL objAdmin = new AdminBAL();
            DataTable dt = new DataTable();

            if (Session["UserID"] != null)
            {
                dt = objAdmin.GetAdminDetails(Convert.ToInt32(Session["UserID"]));
            }
            else
            {
                dt = objAdmin.GetAdminDetails(AdminAuthentication.AdminID);
            }

            if (dt.Rows.Count > 0)
            {
                strDate = Convert.ToDateTime(dt.Rows[0]["AccessDate"]).ToLongDateString();
                strIPAddress = Convert.ToString(dt.Rows[0]["IPAddress"]);
            }

        }
        if (Request["state"] != null)
        {
            SelectedState = Convert.ToString(Request["state"]);
        }

        if (Session["AdminType"] != null)
        {
            AdminType = Convert.ToInt32(Session["AdminType"]);
        }
        if (Session["FirstName"] != null)
        {
            UserName = Convert.ToString(Session["FirstName"]);
        }
        BindYear();
        AdminDashBoard();
        DashBoardReport(DateTime.Now.Year);
        //BindStateWiseReport();
    }
    //private void BindStateWiseReport()
    //{
    //    DataTable dt = new DataTable();
    //    CommonBAL objCommonBAL = new CommonBAL();
    //    dt = objCommonBAL.AdminDashBoardStateReport();
    //    if (dt.Rows.Count > 0)
    //    {
    //        rptRecord.DataSource = dt;
    //        rptRecord.DataBind();
    //    }
    //}
    private void BindYear()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add("Year");

        for (int i = 2017; i <= DateTime.Now.Year; i++)
        {
            dr = dt.NewRow();
            dr["Year"] = i.ToString();
            dt.Rows.Add(dr);
        }
        DataView dv = new DataView(dt);
        dv.Sort= "Year DESC";

        ddlYearYearly.DataSource = dv;
        ddlYearYearly.DataTextField = "Year";
        ddlYearYearly.DataValueField = "Year";
        ddlYearYearly.DataBind();
        ddlYearYearly.Value = DateTime.Now.Year.ToString();

    }
    private void AdminDashBoard()
    {
        DataTable dt = new DataTable();
        //AdminBAL objAdminBAL = new AdminBAL();
        //dt = objAdminBAL.AdminDashboard();
        CommonBAL objCommonBAL = new CommonBAL();
        dt = objCommonBAL.AdminDashboard(SelectedState);
        if (dt.Rows.Count > 0)
        {
            if (Convert.ToString(dt.Rows[0]["SoldQty"]) != string.Empty)
                SoldQty = Convert.ToString(dt.Rows[0]["SoldQty"]);

            if (Convert.ToString(dt.Rows[0]["TotalAmountBeforeBMH"]) != string.Empty)
                TotalAmountBeforeBMH = Convert.ToString(dt.Rows[0]["TotalAmountBeforeBMH"]);

            if (Convert.ToString(dt.Rows[0]["BMH"]) != string.Empty)
                BMH = Convert.ToString(dt.Rows[0]["BMH"]);

            if (Convert.ToString(dt.Rows[0]["GrandTotal"]) != string.Empty)
                GrandTotal = Convert.ToString(dt.Rows[0]["GrandTotal"]);

            if (Convert.ToString(dt.Rows[0]["Donation"]) != string.Empty)
                Donation = Convert.ToString(dt.Rows[0]["Donation"]);

            if (Convert.ToString(dt.Rows[0]["TransactionFee"]) != string.Empty)
                TransactionFee = Convert.ToString(dt.Rows[0]["TransactionFee"]);
        }
        else
        {
            SoldQty = "0";
            TotalAmountBeforeBMH = "0";
            BMH = "0";
            GrandTotal = "0";
            Donation = "0";
        }

    }
    private void DashBoardReport(int Year)
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        //AdminBAL objAdminBAL = new AdminBAL();
        CommonBAL objCommonBAL = new CommonBAL();
        ds = objCommonBAL.DashboardReportStateWise(Year, SelectedState);
        dt = ds.Tables[0];
        if (dt.Rows.Count > 0)
        {
            ReportGrandTotal = Convert.ToString(dt.Rows[0]["GrandTotal"]);
            ReportMonths = Convert.ToString(dt.Rows[0]["Month"]);
            ReportBMH = Convert.ToString(dt.Rows[0]["BMH"]);
            ReportTotal = Convert.ToString(dt.Rows[0]["TotalAmount"]);
            ReportTotalQty = Convert.ToString(dt.Rows[0]["Qty"]);
            ReportTotalSignedIns = Convert.ToString(dt.Rows[0]["TotalUsersRegistered"]);
            ReportTotalDonation = Convert.ToString(dt.Rows[0]["Donation"]);
        }

        if (ds.Tables[1].Rows.Count > 0)
        {
            ReportDisplayBMH = Convert.ToString(ds.Tables[1].Rows[0]["BMH"]);
            ReportDisplayGrandTotal = Convert.ToString(ds.Tables[1].Rows[0]["GrandTotal"]);
            ReportDisplayTotal = Convert.ToString(ds.Tables[1].Rows[0]["TotalAmount"]);
            ReportDisplayDonation = Convert.ToString(ds.Tables[1].Rows[0]["Donation"]);
        }

        if (ds.Tables[2].Rows.Count > 0)
        {
            UserSignedIn = Convert.ToString(ds.Tables[2].Rows[0]["UsersCount"]);
        }


        if (ds.Tables[3].Rows.Count > 0)
        {
            BusinessSignedIn = Convert.ToString(ds.Tables[3].Rows[0]["BusinessCount"]);

        }

        if (Request["yearlyreport"] != null)
        {
            Response.Write(ReportGrandTotal + "^^^" + ReportMonths + "^^^" + ReportBMH + "^^^" + ReportTotal + "^^^" + ReportDisplayBMH + "^^^" + ReportDisplayGrandTotal + "^^^" + ReportDisplayTotal + "^^^" + ReportTotalQty + "^^^" + ReportTotalSignedIns + "^^^" + ReportTotalDonation);
            Response.End();
        }
    }

}