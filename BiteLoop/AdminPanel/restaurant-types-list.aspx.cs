using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using Utility;

public partial class AdminPanel_RestaurantTypes_List : AdminAuthentication
{
    #region Private Members
    private new string Title = string.Empty;
    #endregion

    RestaurantTypesBAL objRestaurantTypesBAL = new RestaurantTypesBAL();

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.Form.Keys.Count > 0)
        {
            Title = Request["txtTitle"];
            if (!int.TryParse(Request["page"], out CurrentPage))
                CurrentPage = 1;
            if (!string.IsNullOrEmpty(Request["type"]))
                Operation();
            if (!int.TryParse(Request["hdnRecPerPage"], out RecordPerPage))
                RecordPerPage = 25;
        }
       
        if (!string.IsNullOrEmpty(Request["sorttype"]))
            SortType = Request["sorttype"];
        else
            SortType = "ASC";

        if (!string.IsNullOrEmpty(Request["sortcol"]))
            SortColumn = Request["sortcol"];
        else
            SortColumn = "Name";
        BindList();

    }
    #endregion

    #region  Bind RestaurantTypes List

    //private void BindList()
    //{
    //    int TotalRecord = 0;
    //    DataTable dt = new DataTable();
    //    objRestaurantTypesBAL.Name = Title;
    //    dt = objRestaurantTypesBAL.GetList(ref CurrentPage, RecordPerPage, out TotalRecord, SortColumn, SortType);

    //    // Build the base URL dynamically (similar to your working API)
    //    string baseUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/BitLoop/source/CMSFiles/";

    //    // Update the ImageName column to have full URL
    //    if (dt.Rows.Count > 0 && dt.Columns.Contains("ImageName"))
    //    {
    //        foreach (DataRow row in dt.Rows)
    //        {
    //            if (row["ImageName"] != DBNull.Value && !string.IsNullOrEmpty(row["ImageName"].ToString()))
    //            {
    //                string imageName = row["ImageName"].ToString();
    //                // Prevent double adding base URL if it already has http
    //                if (!imageName.StartsWith("http", StringComparison.OrdinalIgnoreCase))
    //                {
    //                    row["ImageName"] = baseUrl + imageName;
    //                }
    //            }
    //        }
    //    }

    //    if (dt.Rows.Count > 0)
    //    {
    //        rptRecord.Visible = true;
    //        rptRecord.DataSource = dt;
    //        rptRecord.DataBind();
    //        CtrlPage1.Visible = true;
    //        CtrlPage1.TotalRecord = TotalRecord;
    //        CtrlPage1.CurrentPage = CurrentPage;
    //        Common.BindAdminPagingControl(CurrentPage, RecordPerPage, PagingLimit, TotalRecord, (Repeater)(CtrlPage1.FindControl("rptPaging")));
    //        trNoRecords.Visible = false;
    //    }
    //    else
    //    {
    //        rptRecord.Visible = false;
    //        trNoRecords.Visible = true;
    //        CtrlPage1.Visible = false;
    //    }
    //    if (Request.Form.Keys.Count > 0)
    //    {
    //        if (string.IsNullOrEmpty(Request["type"]))
    //        {
    //            Response.Write("<script>DisplMsg('" + divMsg.ClientID + "','','')</script>");
    //        }
    //        Response.Write(Common.RenderControl(divList, Common.RenderControlName.HtmlGeneric));
    //        Response.Write("<script>page=" + CurrentPage + ";BindCtrl();$('#divTotRec').html('" + TotalRecord + " records');</script>");
    //        Response.End();
    //    }
    //}


    private void BindList()
    {

        int TotalRecord = 0;
        DataTable dt = new DataTable();
        objRestaurantTypesBAL.Name = Title;
        dt = objRestaurantTypesBAL.GetList(ref CurrentPage, RecordPerPage, out TotalRecord, SortColumn, SortType);

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
            Response.Write("<script>page=" + CurrentPage + ";BindCtrl();$('#divTotRec').html('" + TotalRecord + " records');</script>");
            Response.End();
        }
    }

    #endregion

    #region RestaurantTypes Operation
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
        RestaurantTypesBAL ObjRestaurantTypesBAL = new RestaurantTypesBAL();
        if (!string.IsNullOrEmpty(Request["ID"]))
        {
            ObjRestaurantTypesBAL.RestaurantTypesOperation(Convert.ToString(Request["ID"]), ObjOpr);
            Response.Write(Javascript.DisplayMsg(divMsg.ClientID, strMsg, Javascript.MessageType.Success, true));
        }
        else
        {
            ObjRestaurantTypesBAL.RestaurantTypesOperation(Convert.ToString(Request["hdnID"]), ObjOpr);
            Response.Write("<script>DisplMsg('" + divMsg.ClientID + "','" + strMsg + "','alert-message success')</script>");
        }
    }
    #endregion
}