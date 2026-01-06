using BAL;
using BiteLoop.Common;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

public partial class AdminPanel_Issue_List : AdminAuthentication
{
    private IssuesBAL objIssuesBAL = new IssuesBAL();
    public static string strColumn = string.Empty;
    private string strEmail = string.Empty;
    private string strName = string.Empty;
    private string strSubject = string.Empty;
    private string strState = string.Empty;
    private string strSuburb = string.Empty;
    private string strPostcode = string.Empty;
    public static string strTypes = string.Empty;
    protected string UserName = string.Empty;
    protected long UID = 0;
    private void BindContactList()
    {
        if (Request["uid"] != null)
        {
            this.objIssuesBAL.ID = Convert.ToInt16(Request["uid"]);
        }
        else
        {
            this.objIssuesBAL.ID = 0;
        }

        this.objIssuesBAL.CaseNumber = this.strName.Trim();
        this.objIssuesBAL.OrderUniqueID = this.strEmail.Trim();
        int intTotalRecord = 0;
        DataTable dtTable = new DataTable();
        dtTable = GetIssueList(ref this.CurrentPage, base.RecordPerPage, out intTotalRecord, base.SortColumn, base.SortType, objIssuesBAL, this.strState, this.strSuburb, this.strPostcode);

        if (!string.IsNullOrEmpty(base.Request["ysnExport"]))
        {
            this.objIssuesBAL.CaseNumber = Convert.ToString(base.Request["txtName"]);
            this.objIssuesBAL.OrderUniqueID = Convert.ToString(base.Request["txtEmail"]);
            this.strState = base.Request["ddlState"];
            this.strSuburb = base.Request["ddlSuburb"];
            this.strPostcode = base.Request["txtPostcode"];

            if ((dtTable != null) || (dtTable.Rows.Count > 0))
            {
                if (string.IsNullOrEmpty(strTypes))
                {
                    strTypes = "DESC";
                }
                if (string.IsNullOrEmpty(strColumn))
                {
                    strColumn = "Issues.ID";
                }
                //  dtTable = this.objUsersBAL.GetList(ref this.CurrentPage, -1, out intTotalRecord, strColumn, strTypes);
                dtTable = GetIssueList(ref this.CurrentPage, -1, out intTotalRecord, strColumn, strTypes, objIssuesBAL, this.strState, this.strSuburb, this.strPostcode);
                Common.ExportToCSVComma("IssuesList-" + DateTime.Now.ToString("dd-MM-yyyy") + ".csv", dtTable, true);
            }
        }
        else if (dtTable.Rows.Count > 0)
        {
            if (Request["uid"] != null)
            {
                UserName = Convert.ToString(dtTable.Rows[0]["Name"]);
            }

            this.rptRecord.Visible = true;
            this.rptRecord.DataSource = dtTable;
            this.rptRecord.DataBind();
            this.CtrlPage1.Visible = true;
            this.CtrlPage1.TotalRecord = intTotalRecord;
            this.CtrlPage1.CurrentPage = base.CurrentPage;
            Common.BindAdminPagingControl(base.CurrentPage, base.RecordPerPage, base.PagingLimit, intTotalRecord, (Repeater)this.CtrlPage1.FindControl("rptPaging"));
            this.trNoRecords.Visible = false;
        }
        else
        {
            this.rptRecord.Visible = false;
            this.trNoRecords.Visible = true;
            this.CtrlPage1.Visible = false;
        }
        if (base.Request.Form.Keys.Count > 0)
        {
            if (string.IsNullOrEmpty(base.Request["type"]))
            {
                // base.Response.Write(Common.ScriptStartTag + "DisplMsg('" + this.divMsg.ClientID + "','','')" + Common.ScriptEndTag);
            }
            // base.Response.Write(Common.RenderControl(this.divList, Common.RenderControlName.HtmlGeneric));
            base.Response.Write(string.Concat(new object[] { Common.ScriptStartTag, "page=", base.CurrentPage, ";BindCtrl();$('#divTotRec').html('", intTotalRecord, " records');", Common.ScriptEndTag }));
            base.Response.End();
        }
    }

    protected string GetImages(string images)
    {
        if (string.IsNullOrWhiteSpace(images))
            return string.Empty;

        var imagePaths = images
            .Split(',', (char)StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim());

        StringBuilder sb = new StringBuilder();

        foreach (var path in imagePaths)
        {
            sb.AppendFormat(
                "<a href='{0}' target='_blank'>" +
                "<img src='{0}' style='width:80px;height:80px;margin:8px;padding:4px;border:1px solid #000;' />" +
                "</a>",
                path
            );
        }

        return sb.ToString();
    }

    protected string GetStatusOptions(object statusObj)
    {
        string status = statusObj.ToString() ?? "Pending";
        StringBuilder sb = new StringBuilder();

        if (status == "Pending")
        {
            sb.Append("<option value='Pending' selected>Pending</option>");
            sb.Append("<option value='Rejected'>Rejected</option>");
            sb.Append("<option value='Resolved'>Resolved</option>");
        }
        else if (status == "Rejected")
        {
            sb.Append("<option value='Rejected' selected>Rejected</option>");
        }
        else if (status == "Resolved")
        {
            sb.Append("<option value='Resolved' selected>Resolved</option>");
        }

        return sb.ToString();
    }

    private void IssuesOperation()
    {
        string actionType = Request.Form[hdnType.UniqueID];
        string recordID = Request.Form[hdnID.UniqueID];

        Common.DataBaseOperation ObjOpr = Common.DataBaseOperation.None;
        string strMessage = string.Empty;

        switch (actionType)
        {
            case "Rejected":
                ObjOpr = Common.DataBaseOperation.InActive;
                strMessage = "Selected record has been rejected.";
                break;
            case "Resolved":
                ObjOpr = Common.DataBaseOperation.Active;
                strMessage = "Selected record has been resolved.";
                break;
            default:
                return;
        }
        this.objIssuesBAL.Operation(recordID, actionType);

        Response.Redirect("issue-list.aspx?msg=" + Server.UrlEncode(strMessage));
    }




    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["uid"] != null)
        {
            UID = Convert.ToInt64(Request["uid"]);
        }
        if (base.Request.Form.Keys.Count > 0)
        {
            this.strName = base.Request["txtName"];
            this.strEmail = base.Request["txtEmail"];
            this.strState = base.Request["ctl00$CPHContent$ddlState"];
            this.strSuburb = base.Request["ctl00$CPHContent$ddlSuburb"];
            this.strPostcode = base.Request["txtPostcode"];
            var keys = string.Join(", ", Request.Form.AllKeys);
            var value = Request.Form["hdnType"];
            //Response.Write($"<br>Form keys: {keys}<br>hdnType value: {value}");
            if (!int.TryParse(base.Request["page"], out this.CurrentPage))
            {
                base.CurrentPage = 1;
            }
            if (!int.TryParse(base.Request["hdnRecPerPage"], out this.RecordPerPage))
            {
                base.RecordPerPage = 10;
            }
            if (!string.IsNullOrEmpty(Request.Form[hdnType.UniqueID]))
            {
                this.IssuesOperation();
            }
        }
        if (!IsPostBack && !string.IsNullOrEmpty(Request["msg"]))
        {
            divMsg.InnerHtml = Request["msg"];
            divMsg.Attributes["class"] = "alert-message success";
            divMsg.Style["display"] = "block";
        }
        if (!string.IsNullOrEmpty(base.Request["sorttype"]))
        {
            base.SortType = base.Request["sorttype"];
            strTypes = base.SortType;
        }
        else
        {
            base.SortType = "DESC";
        }
        if (!string.IsNullOrEmpty(base.Request["sortcol"]))
        {
            base.SortColumn = base.Request["sortcol"];
            strColumn = base.SortColumn;
        }
        else
        {
            base.SortColumn = "Issues.ID";
        }
        this.BindContactList();
    }

    private DataTable GetIssueList(ref int intCurrentPage, int intRecordPerPage, out int intTotalRecord, string strSortColumn, string strSortType, IssuesBAL objIssuesBAL, string State, string Suburb, string Postcode)
    {
        DbParameter[] dbParam = new DbParameter[5];
        dbParam[0] = new DbParameter("@TotalRecord", DbParameter.DbType.Int, 4, 10);
        dbParam[1] = new DbParameter("@CurrentPage", DbParameter.DbType.Int, 4, 1);
        dbParam[2] = new DbParameter("@RecordPerPage", DbParameter.DbType.Int, 4, intRecordPerPage);
        dbParam[3] = new DbParameter("@SortOrd", DbParameter.DbType.VarChar, 20, strSortType);
        dbParam[4] = new DbParameter("@SortColumn", DbParameter.DbType.VarChar, 20, "ID");
        DataTable table = new DataTable();
        table = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "IssuesList", dbParam);
        intCurrentPage = Convert.ToInt32(dbParam[0].Value);
        intTotalRecord = 100;
        return table;
    }
}