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

public partial class AdminPanel_UserOrders_List : AdminAuthentication
{

    CartBAL objCartBAL = new CartBAL();
    protected long VID = 0;
    protected long UID = 0;
    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["OrderStatus"] != null)
        {
            UpdateOrderStatus();
        }
        if (Request["vid"] != null)
        {
            VID = Convert.ToInt64(Request["vid"]);
        }
        if (Request["uid"] != null)
        {
            UID = Convert.ToInt64(Request["uid"]);
        }

        if (Request.Form.Keys.Count > 0)
        {

            if (!int.TryParse(Request["page"], out CurrentPage))
                CurrentPage = 1;
            if (!int.TryParse(Request["hdnRecPerPage"], out RecordPerPage))
                RecordPerPage = 25;
        }

        if (!string.IsNullOrEmpty(Request["sorttype"]))
            SortType = Request["sorttype"];
        else
            SortType = "DESC";

        if (!string.IsNullOrEmpty(Request["sortcol"]))
            SortColumn = Request["sortcol"];
        else
            SortColumn = "BusinessOrder.CreatedOn";
        BindPartners();
        BindState();
        BindList();

    }
    #endregion
    private void BindState()
    {
        DataTable dt = new DataTable();
        CommonBAL objCommonBAL = new CommonBAL();
        dt = objCommonBAL.StateList();
        if (dt.Rows.Count > 0)
        {
            ddlState.DataSource = dt;
            ddlState.DataTextField = "StateCode";
            ddlState.DataValueField = "StateCode";
            ddlState.DataBind();
        }
        ddlState.Items.Insert(0, new ListItem("--All States--", ""));
    }
    #region  Bind Partner Payment List
    private void BindPartners()
    {
        UsersBAL objUsersBAL = new UsersBAL();
        DataSet ds = new DataSet();
        ds = objUsersBAL.BusinessUsersDropdown();
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlBusiness.DataSource = ds.Tables[0];
            ddlBusiness.DataTextField = "Name";
            ddlBusiness.DataValueField = "ID";
            ddlBusiness.DataBind();
        }
        if (ds.Tables[1].Rows.Count > 0)
        {
            ddlUsers.DataSource = ds.Tables[1];
            ddlUsers.DataTextField = "Name";
            ddlUsers.DataValueField = "ID";
            ddlUsers.DataBind();
        }
        ddlBusiness.Items.Insert(0, new ListItem("-- Select Vendor --", "0"));
        ddlUsers.Items.Insert(0, new ListItem("-- Select User --", "0"));
    }
    private void BindList()
    {

        int TotalRecord = 0;
        long BusinessID = 0;
        long UserID = 0;
        string StateName = "";
        string Email = string.Empty;
        if (Request["vid"] != null)
        {
            BusinessID = Convert.ToInt64(Request["vid"]);
        }
        if (Request["uid"] != null)
        {
            UserID = Convert.ToInt64(Request["uid"]);
        }
        if (Request[ddlBusiness.UniqueID] != null)
        {
            if (Convert.ToString(Request[ddlBusiness.UniqueID]).Trim() != string.Empty)
                BusinessID = Convert.ToInt64(Request[ddlBusiness.UniqueID]);
        }
        if (Request[ddlUsers.UniqueID] != null)
        {
            if (Convert.ToString(Request[ddlUsers.UniqueID]).Trim() != string.Empty)
                UserID = Convert.ToInt64(Request[ddlUsers.UniqueID]);
        }
        if (Request[ddlState.UniqueID] != null)
        {
            if (Convert.ToString(Request[ddlState.UniqueID]).Trim() != string.Empty)
                StateName = Convert.ToString(Request[ddlState.UniqueID]);
        }


        DateTime StartDate = Convert.ToDateTime("01/01/1900");
        DateTime EndDate = Convert.ToDateTime("01/01/1900");
        string OrderID = "";
        if (Request[txtStartDate.UniqueID] != null)
        {
            if (Convert.ToString(Request[txtStartDate.UniqueID]).Trim() != string.Empty)
                StartDate = Convert.ToDateTime(Request[txtStartDate.UniqueID]);
            else
                StartDate = Convert.ToDateTime("01/01/1900");
        }
        if (Request[txtEndDate.UniqueID] != null)
        {
            if (Convert.ToString(Request[txtEndDate.UniqueID]).Trim() != string.Empty)
                EndDate = Convert.ToDateTime(Request[txtEndDate.UniqueID]);
            else
                EndDate = Convert.ToDateTime("01/01/1900");
        }
        if (Request[txtOrderID.UniqueID] != null)
        {
            if (Convert.ToString(Request[txtOrderID.UniqueID]).Trim() != string.Empty)
                OrderID = Convert.ToString(Request[txtOrderID.UniqueID]);
        }


        DataTable dt = new DataTable();
        objCartBAL.UserID = UserID;
        objCartBAL.BusinessID = BusinessID;

        if (!string.IsNullOrEmpty(base.Request["ysnExport"]))
        {
            // dt = objCartBAL.UserOrdersReport(ref CurrentPage, -1, out TotalRecord, SortColumn, "ASC", Email, StartDate, EndDate);
            dt = UserOrdersReportDAL(ref CurrentPage, -1, out TotalRecord, SortColumn, "ASC", Email, StartDate, EndDate, OrderID, objCartBAL, StateName);

            Common.ExportToCSVComma("OrderReport-" + DateTime.Now.ToString("dd-MM-yyyy") + ".csv", dt, true);
        }
        else
        {
            //dt = objCartBAL.UserOrdersReport(ref CurrentPage, RecordPerPage, out TotalRecord, SortColumn, SortType, Email, StartDate, EndDate);
            dt = UserOrdersReportDAL(ref CurrentPage, RecordPerPage, out TotalRecord, SortColumn, SortType, Email, StartDate, EndDate, OrderID, objCartBAL, StateName);

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
    }

    private void UpdateOrderStatus()
    {
        if (Request["OrderStatus"] != null)
        {
            OrderStatusChange(Convert.ToInt64(Request["OrderID"]), Convert.ToString(Request["OrderStatus"]));
        }
        Response.Write("success");
        Response.End();
    }
    private void OrderStatusChange(long OrderID, string strStatus)
    {
        DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@OrderID", DbParameter.DbType.Int, 40, OrderID),
                new DbParameter("@OrderStatus", DbParameter.DbType.VarChar, 200, strStatus)
            };
        DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "OrderStatusUpdate", dbParam);
    }
    private DataTable UserOrdersReportDAL(ref int intCurrentPage, int RecordPerPage, out int intTotalRecord, string strSortColumn, string strSortType, string Email, DateTime StartDate, DateTime EndDate, string OrderID, CartBAL objCartBAL, string StateName)
    {
        DbParameter[] dbParam = new DbParameter[13];
        dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 40, objCartBAL.ID);
        dbParam[1] = new DbParameter("@BusinessID", DbParameter.DbType.Int, 40, objCartBAL.BusinessID);
        dbParam[2] = new DbParameter("@UserID", DbParameter.DbType.Int, 40, objCartBAL.UserID);
        dbParam[3] = new DbParameter("@Email", DbParameter.DbType.VarChar, 500, Email);
        dbParam[4] = new DbParameter("@CurrentPage", DbParameter.DbType.Int, 4, (int)intCurrentPage, ParameterDirection.InputOutput);
        dbParam[5] = new DbParameter("@RecordPerPage", DbParameter.DbType.Int, 4, RecordPerPage);
        dbParam[6] = new DbParameter("@TotalRecord", DbParameter.DbType.Int, 4, ParameterDirection.Output);
        if ((strSortColumn != string.Empty) && (strSortType != string.Empty))
        {
            dbParam[7] = new DbParameter("@SortOrd", DbParameter.DbType.VarChar, 10, strSortType);
            dbParam[8] = new DbParameter("@SortColumn", DbParameter.DbType.VarChar, 50, strSortColumn);
        }
        dbParam[9] = new DbParameter("@StartDate", DbParameter.DbType.DateTime, 40, StartDate);
        dbParam[10] = new DbParameter("@EndDate", DbParameter.DbType.DateTime, 40, EndDate);
        dbParam[11] = new DbParameter("@OrderID", DbParameter.DbType.VarChar, 40, OrderID);
        dbParam[12] = new DbParameter("@StateName", DbParameter.DbType.VarChar, 40, StateName);


        DataTable table = new DataTable();
        table = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "UserOrdersReport", dbParam);
        intCurrentPage = Convert.ToInt32(dbParam[4].Value);
        intTotalRecord = Convert.ToInt32(dbParam[6].Value);
        return table;
    }


    #endregion

}