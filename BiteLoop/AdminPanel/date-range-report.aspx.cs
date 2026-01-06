using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using Utility;

public partial class AdminPanel_DateRange_List : AdminAuthentication
{

    CartBAL objCartBAL = new CartBAL();

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request["searchorder"] != null)
        {
            BindList();
        }
        
        if (Request["ysnExport"] != null)
        {
            BindList();
        }
        
        //if (Request.Form.Keys.Count > 0)
        //{

        //    if (!int.TryParse(Request["page"], out CurrentPage))
        //        CurrentPage = 1;
        //    if (!int.TryParse(Request["hdnRecPerPage"], out RecordPerPage))
        //        RecordPerPage = 25;
        //}

        //if (!string.IsNullOrEmpty(Request["sorttype"]))
        //    SortType = Request["sorttype"];
        //else
        //    SortType = "DESC";

        //if (!string.IsNullOrEmpty(Request["sortcol"]))
        //    SortColumn = Request["sortcol"];
        //else
        //    SortColumn = "BusinessOrder.CreatedOn";
        BindPartners();
        //BindList();

    }
    #endregion

    #region  Bind Partner Payment List
    private void BindPartners()
    {
        UsersBAL objUsersBAL = new UsersBAL();
        DataSet ds = new DataSet();
        ds = objUsersBAL.BusinessUsersDropdown();
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlBusiness.DataSource = ds.Tables[0];
            ddlBusiness.DataTextField = "VendorUniqueIDName";
            ddlBusiness.DataValueField = "ID";
            ddlBusiness.DataBind();
        }
      
        ddlBusiness.Items.Insert(0, new ListItem("-- Select Vendor --", "0"));
      
    }
    private void BindList()
    {
        DateTime StartDate;
        DateTime EndDate;
        string strDate = Convert.ToString( Request["date"]);
        if (strDate.IndexOf('-') >= 0)
        {
            StartDate = Convert.ToDateTime(Convert.ToString(strDate.Split('-')[0]).Trim());
            EndDate = Convert.ToDateTime(Convert.ToString(strDate.Split('-')[1]).Trim());
        }
        else
        {
            StartDate = Convert.ToDateTime(Convert.ToString(strDate).Trim());
            EndDate = Convert.ToDateTime(Convert.ToString(strDate).Trim());
        }
        long BusinessID = Convert.ToInt64(Request["businessid"]);

        DataTable dt = new DataTable();
        CartBAL objCartBAL = new CartBAL();
        objCartBAL.BusinessID = BusinessID;

        if (!string.IsNullOrEmpty(base.Request["ysnExport"]))
        {            
            dt = objCartBAL.BusinessTimelyReport(StartDate, EndDate);
            Common.ExportToCSVComma("VendorReport-" + DateTime.Now.ToString("dd-MM-yyyy") + ".csv", dt, true);
        }
        else
        {


            dt = objCartBAL.BusinessTimelyReport(StartDate, EndDate);
            if (dt.Rows.Count > 0)
            {
                rptRecord.DataSource = dt;
                rptRecord.DataBind();
                trNoRecords.Visible = false;
            }
            else
            {
                trNoRecords.Visible = true;
            }
            Response.Write(Common.RenderControl(divList, Common.RenderControlName.HtmlGeneric));
            Response.End();
        }
        //int TotalRecord = 0;
        //long BusinessID = 0;
        //long UserID = 0;
        //string Email = string.Empty;
        //if (Request[ddlBusiness.UniqueID] != null)
        //{
        //    if (Convert.ToString(Request[ddlBusiness.UniqueID]).Trim() != string.Empty)
        //        BusinessID = Convert.ToInt64(Request[ddlBusiness.UniqueID]);
        //}
      
        //DataTable dt = new DataTable();
        //objCartBAL.UserID = UserID;
        //objCartBAL.BusinessID = BusinessID;

        //dt = objCartBAL.UserOrdersReport(ref CurrentPage, RecordPerPage, out TotalRecord, SortColumn, SortType, Email);

        //if (dt.Rows.Count > 0)
        //{
        //    rptRecord.Visible = true;
        //    rptRecord.DataSource = dt;
        //    rptRecord.DataBind();
        //    CtrlPage1.Visible = true;
        //    CtrlPage1.TotalRecord = TotalRecord;
        //    CtrlPage1.CurrentPage = CurrentPage;
        //    Common.BindAdminPagingControl(CurrentPage, RecordPerPage, PagingLimit, TotalRecord, (Repeater)(CtrlPage1.FindControl("rptPaging")));
        //    trNoRecords.Visible = false;
        //}
        //else
        //{
        //    rptRecord.Visible = false;
        //    trNoRecords.Visible = true;
        //    CtrlPage1.Visible = false;
        //}
        //if (Request.Form.Keys.Count > 0)
        //{
        //    if (string.IsNullOrEmpty(Request["type"]))
        //    {
        //        Response.Write("<script>DisplMsg('" + divMsg.ClientID + "','','')</script>");
        //    }
        //    Response.Write(Common.RenderControl(divList, Common.RenderControlName.HtmlGeneric));
        //    Response.Write("<script>page=" + CurrentPage + ";BindCtrl();$('#divTotRec').html('" + TotalRecord + " records');</script>");
        //    Response.End();
        //}
    }


    #endregion
  
}