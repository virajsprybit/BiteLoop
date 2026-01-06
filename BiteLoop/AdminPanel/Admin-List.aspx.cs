using BAL;
using System;
using System.Data;
using System.Web.UI.WebControls;
using Utility;
 

public partial class AdminPanel_Admin_List : AdminAuthentication
{
    AdminBAL objAdmin = new AdminBAL();

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Keys.Count > 0)
        {
            if (!int.TryParse(Request["page"], out CurrentPage))
                CurrentPage = 1;
            if (!string.IsNullOrEmpty(Request["type"]))
                AdminOperation();
            if (!int.TryParse(Request["hdnRecPerPage"], out RecordPerPage))
                RecordPerPage = 10;
        }
        if (!string.IsNullOrEmpty(Request["sorttype"]))
            SortType = Request["sorttype"];
        else
            SortType = "ASC";

        if (!string.IsNullOrEmpty(Request["sortcol"]))
            SortColumn = Request["sortcol"];
        else
            SortColumn = "ID";
        BindList();

    }
    #endregion

    #region  Bind User List
    private void BindList()
    {

        objAdmin.FirstName = Request["tbxFname"];
        objAdmin.LastName = Request["tbxLname"];
        objAdmin.UserName = Request["tbxUname"];
        objAdmin.EmailID = Request["tbxEmail"];
        int intTotalRecord = 0;
        DataTable dtblAdminList = new DataTable();
        int IsSuperAdmin = 0;
        if (Session["IsSuperAdmin"] != null)
        {
            if (Convert.ToBoolean(Session["IsSuperAdmin"]))
            {
                IsSuperAdmin = 1;
            }
            else
            {
                objAdmin.ID = Convert.ToInt32(Session["UserID"]);
            }
        }
        dtblAdminList = objAdmin.GetList(IsSuperAdmin, ref CurrentPage, RecordPerPage, out intTotalRecord, SortColumn, SortType);

        if (dtblAdminList.Rows.Count > 0)
        {
            rptRecord.Visible = true;
            rptRecord.DataSource = dtblAdminList;
            rptRecord.DataBind();
            CtrlPage1.Visible = true;
            CtrlPage1.TotalRecord = intTotalRecord;
            CtrlPage1.CurrentPage = CurrentPage;
            Common.BindAdminPagingControl(CurrentPage, RecordPerPage, PagingLimit, intTotalRecord, (Repeater)(CtrlPage1.FindControl("rptPaging")));
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
            Response.Write("<script>page=" + CurrentPage + ";BindCtrl();$('#divTotRec').html('" + intTotalRecord + " records');</script>");
            Response.End();
        }

    }
    #endregion

    #region Admin Operation
    private void AdminOperation()
    {
        Common.DataBaseOperation objOpr = Common.DataBaseOperation.None;
        string strMsg = string.Empty;
        switch (Request["type"])
        {
            case "remove":
                objOpr = Common.DataBaseOperation.Remove;
                strMsg = "Selected record(s) has been removed successfully.";
                break;
            case "active":
                objOpr = Common.DataBaseOperation.Active;
                strMsg = "Selected record(s) has been changed successfully.";
                break;
            case "inactive":
                objOpr = Common.DataBaseOperation.InActive;
                strMsg = "Selected record(s) has been changed successfully.";
                break;
        }


        if (!string.IsNullOrEmpty(Request["ID"]))
        {
            objAdmin.Operation(Convert.ToString(Request["ID"]), objOpr, Convert.ToInt64(Session["UserID"]));
            Response.Write(Javascript.DisplayMsg(divMsg.ClientID, strMsg, Javascript.MessageType.Success, true));
        }
        else
        {
            objAdmin.Operation(Convert.ToString(Request["hdnID"]), objOpr, Convert.ToInt64(Session["UserID"]));
            Response.Write(Javascript.DisplayMsg(divMsg.ClientID, strMsg, Javascript.MessageType.Success, true));
        }


    }
    #endregion
     
}