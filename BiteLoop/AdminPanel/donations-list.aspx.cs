using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using Utility;

public partial class AdminPanel_donations_List : AdminAuthentication
{
    #region Private Members
    private string strTitle = string.Empty, strNewsDate = string.Empty;
    private string strAnswer = string.Empty;
    private int intStatus = 2;
    DonationsBAL objDonationsBAL = new DonationsBAL();

    string Fdate = string.Empty;
    string Tdate = string.Empty;
    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        RecordPerPage = 1000;
        if (!string.IsNullOrEmpty(Request["resequence"]))
        {
            UpdateFAQSequence();
        }
        if (Request.Form.Keys.Count > 0)
        {
            strTitle = Request["tbxTitle"];
            strAnswer = Request["tbxAnswer"];
            Fdate = Request["tbxFromDate"];
            Tdate = Request["tbxToDate"];
            if (!int.TryParse(Request["page"], out CurrentPage))
                CurrentPage = 1;
            if (!string.IsNullOrEmpty(Request["type"]))
                Operation();
            if (!int.TryParse(Request["hdnRecPerPage"], out RecordPerPage))
                RecordPerPage = 1000;

        }
        if (!string.IsNullOrEmpty(Request["sorttype"]))
            SortType = Request["sorttype"];
        else
            SortType = "ASC";

        if (!string.IsNullOrEmpty(Request["sortcol"]))
            SortColumn = Request["sortcol"];
        else
            SortColumn = "SequenceNo";
        BindList();

    }
    #endregion

    #region  Bind testimonial List
    private void BindList()
    {

        int TotalRecord = 0;
        objDonationsBAL.ID = 0;

        DataTable dt = new DataTable();
        dt = objDonationsBAL.GetList(ref CurrentPage, RecordPerPage, out TotalRecord, SortColumn, SortType);
        //dt = objDonationsBAL.GetFAQListByID();
        if (dt.Rows.Count > 0)
        {
            rptRecord.Visible = true;
            rptRecord.DataSource = dt;
            rptRecord.DataBind();
            CtrlPage1.Visible = true;
            CtrlPage1.TotalRecord = TotalRecord;
            CtrlPage1.CurrentPage = CurrentPage;
            Common.BindAdminPagingControl(CurrentPage, RecordPerPage, PagingLimit, TotalRecord, (Repeater)(CtrlPage1.FindControl("rptPaging")));
            trNoRecords.Visible = false;
        }
        else
        {
            rptRecord.Visible = false;
            trNoRecords.Visible = true;
            CtrlPage1.Visible = false;
        }


        if (Request.Form.Keys.Count > 0)
        {
            if (string.IsNullOrEmpty(Request["type"]))
            {
                Response.Write("<script>DisplMsg('" + divMsg.ClientID + "','','')</script>");
            }
            Response.Write(Common.RenderControl(divList, Common.RenderControlName.HtmlGeneric));
            Response.Write("<script>page=" + CurrentPage + ";BindCtrl();$('#divTotRec').html('" + TotalRecord + " records');SortTable();</script>");
            Response.End();
        }
    }
    #endregion

    #region FAQ Operation


    private void Operation()
    {
        Common.DataBaseOperation ObjOpr = Common.DataBaseOperation.None;
        string strMsg = string.Empty;
        switch (Request["type"])
        {
            case "remove":
                ObjOpr = Common.DataBaseOperation.Remove;
                strMsg = "Selected record(s) has been removed successfully.";
                break;
            case "active":
                ObjOpr = Common.DataBaseOperation.Active;
                strMsg = "Selected record(s) has been changed successfully.";
                break;
            case "inactive":
                ObjOpr = Common.DataBaseOperation.InActive;
                strMsg = "Selected record(s) has been changed successfully.";
                break;
        }

        if (!string.IsNullOrEmpty(Request["ID"]))
        {
            objDonationsBAL.DonationsOperation(Convert.ToString(Request["ID"]), ObjOpr);
            Response.Write(Javascript.DisplayMsg(divMsg.ClientID, strMsg, Javascript.MessageType.Success, true));
        }
        else
        {
            objDonationsBAL.DonationsOperation(Convert.ToString(Request["hdnID"]), ObjOpr);
            Response.Write("<script>DisplMsg('" + divMsg.ClientID + "','" + strMsg + "','alert-message success')</script>");
        }
    }
    #endregion

    private void UpdateFAQSequence()
    {

        objDonationsBAL.UpdateDonationsSequence(Convert.ToString(Request["DonationID"]).Trim(), Convert.ToString(Request["Seq"]).Trim());
        SortType = "ASC";
        SortColumn = "SequenceNo";
        BindList();
        Response.Write(Common.RenderControl(divList, Common.RenderControlName.HtmlGeneric));
        Response.End();
    }
}