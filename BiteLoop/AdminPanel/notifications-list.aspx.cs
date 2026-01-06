using BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using System.Data;

public partial class AdminPanel_Notifications_List : AdminAuthentication
{
    private NotificationsBAL objNotificationsBAL = new NotificationsBAL();
    public static string strColumn = string.Empty;
    private string strEmail = string.Empty;
    private string strName = string.Empty;
    private string strSubject = string.Empty;
    public static string strTypes = string.Empty;

    private void BindContactList()
    {
        this.objNotificationsBAL.ID = 0;
        int intTotalRecord = 0;
        DataTable dtTable = new DataTable();
        dtTable = this.objNotificationsBAL.GetList(ref this.CurrentPage, base.RecordPerPage, out intTotalRecord, base.SortColumn, base.SortType);

        if (dtTable.Rows.Count > 0)
        {
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
                base.Response.Write(Common.ScriptStartTag + "DisplMsg('" + this.divMsg.ClientID + "','','')" + Common.ScriptEndTag);
            }
            base.Response.Write(Common.RenderControl(this.divList, Common.RenderControlName.HtmlGeneric));
            base.Response.Write(string.Concat(new object[] { Common.ScriptStartTag, "page=", base.CurrentPage, ";BindCtrl();$('#divTotRec').html('", intTotalRecord, " records');", Common.ScriptEndTag }));
            base.Response.End();
        }
    }

    private void UsersOperation()
    {
        Common.DataBaseOperation ObjOpr = Common.DataBaseOperation.None;
        string strMessage = string.Empty;
        switch (Request["type"])
        {
            case "remove":
                ObjOpr = Common.DataBaseOperation.Remove;
                strMessage = "Selected record(s) has been removed successfully.";
                break;
            case "active":
                ObjOpr = Common.DataBaseOperation.Active;
                strMessage = "Selected record(s) has been changed successfully.";
                break;
            case "inactive":
                ObjOpr = Common.DataBaseOperation.InActive;
                strMessage = "Selected record(s) has been changed successfully.";
                break;
        }
        this.objNotificationsBAL.Operation(Convert.ToString(base.Request["hdnID"]), ObjOpr);
        base.Response.Write("<script>DisplMsg('" + this.divMsg.ClientID + "','" + strMessage + "','alert-message success')</script>");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (base.Request.Form.Keys.Count > 0)
        {
            if (!int.TryParse(base.Request["page"], out this.CurrentPage))
            {
                base.CurrentPage = 1;
            }
            if (!string.IsNullOrEmpty(base.Request["type"]))
            {
                this.UsersOperation();
            }
            if (!int.TryParse(base.Request["hdnRecPerPage"], out this.RecordPerPage))
            {
                base.RecordPerPage = 10;
            }
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
            base.SortColumn = "Notifications.ID";
        }
        this.BindContactList();
    }

}