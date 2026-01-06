using BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using System.Data;
using BiteLoop.Common;
using DAL;

public partial class AdminPanel_User_List : AdminAuthentication
{
    private UsersBAL objUsersBAL = new UsersBAL();
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
            this.objUsersBAL.ID = Convert.ToInt64(Request["uid"]);
        }
        else
        {
            this.objUsersBAL.ID = 0;
        }

        this.objUsersBAL.Name = this.strName.Trim();
        this.objUsersBAL.Email = this.strEmail.Trim();
        int intTotalRecord = 0;
        DataTable dtTable = new DataTable();
        //  dtTable = this.objUsersBAL.GetList(ref this.CurrentPage, base.RecordPerPage, out intTotalRecord, base.SortColumn, base.SortType);
        dtTable = GetUserList(ref this.CurrentPage, base.RecordPerPage, out intTotalRecord, base.SortColumn, base.SortType, objUsersBAL, this.strState, this.strSuburb, this.strPostcode);

        if (!string.IsNullOrEmpty(base.Request["ysnExport"]))
        {
            this.objUsersBAL.Name = Convert.ToString(base.Request["txtName"]);
            this.objUsersBAL.Email = Convert.ToString(base.Request["txtEmail"]);
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
                    strColumn = "Users.ID";
                }
                //  dtTable = this.objUsersBAL.GetList(ref this.CurrentPage, -1, out intTotalRecord, strColumn, strTypes);
                dtTable = GetUserList(ref this.CurrentPage, -1, out intTotalRecord, strColumn, strTypes, objUsersBAL, this.strState, this.strSuburb, this.strPostcode);
                Common.ExportToCSVComma("UsersList-" + DateTime.Now.ToString("dd-MM-yyyy") + ".csv", dtTable, true);
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
        this.objUsersBAL.Operation(Convert.ToString(base.Request["hdnID"]), ObjOpr);
        base.Response.Write("<script>DisplMsg('" + this.divMsg.ClientID + "','" + strMessage + "','alert-message success')</script>");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["uid"] != null)
        {
            UID = Convert.ToInt64(Request["uid"]);
        }
        if (Request["bindsuburb"] != null)
        {
            BindSuburb();
        }
        if (base.Request.Form.Keys.Count > 0)
        {
            this.strName = base.Request["txtName"];
            this.strEmail = base.Request["txtEmail"];
            this.strState = base.Request["ctl00$CPHContent$ddlState"];
            this.strSuburb = base.Request["ctl00$CPHContent$ddlSuburb"];
            this.strPostcode = base.Request["txtPostcode"];


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
            base.SortColumn = "Users.ID";
        }
        BindState();
        this.BindContactList();
    }
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

        ddlState.Items.Insert(dt.Rows.Count, new ListItem("Other", "-1"));
        ddlState.Items.Insert(0, new ListItem("--All States--", ""));
    }
    private void BindSuburb()
    {
        DataTable dt = new DataTable();
        CommonBAL objCommonBAL = new CommonBAL();
        dt = objCommonBAL.SuburbListByStateCode(Convert.ToString(Request["statecode"]));
        if (dt.Rows.Count > 0)
        {
            ddlSuburb.DataSource = dt;
            ddlSuburb.DataTextField = "suburb";
            ddlSuburb.DataValueField = "suburb";
            ddlSuburb.DataBind();
        }
        ddlSuburb.Items.Insert(0, new ListItem("--All Suburb--", ""));
        Response.Write(Common.RenderControl(divSuburb, Common.RenderControlName.HtmlGeneric));
        Response.End();
    }
    protected string GetLocationDetails(string Location)
    {
        string strLocation = "";

        if (Location != string.Empty)
        {
            if (Location.IndexOf("###") > 0)
            {
                Location = Location.Replace("###", "^");
                string[] strLocationSplit = Location.Split('^');
                //string strGoogleMapURL = "http://www.google.com/maps/place/" + Convert.ToString(strLocationSplit[0]) + "," + Convert.ToString(strLocationSplit[1]);
                // strLocation = "<a href='" + strGoogleMapURL + "' target='_blank'>" + Convert.ToString(strLocationSplit[2]) + "</a>";
                strLocation = Convert.ToString(strLocationSplit[2]);
            }
        }

        return strLocation;
    }
    // protected string GetLocationLatitudeLongitude(string Location)
    //{
    //    string strLocation = "";

    //    if (Location != string.Empty)
    //    {
    //        if (Location.IndexOf("###") > 0)
    //        {
    //            Location = Location.Replace("###","^");
    //            string[] strLocationSplit = Location.Split('^');

    //            strLocation = "<img src='images/location.png' width='32' />";// +Convert.ToString(strLocationSplit[2]);
    //        }
    //    }

    //    return strLocation;
    //}

    private DataTable GetUserList(ref int intCurrentPage, int intRecordPerPage, out int intTotalRecord, string strSortColumn, string strSortType, UsersBAL objUsersBAL, string State, string Suburb, string Postcode)
    {
        DbParameter[] dbParam = new DbParameter[11];
        dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 20, objUsersBAL.ID);
        dbParam[1] = new DbParameter("@Name", DbParameter.DbType.VarChar, 100, objUsersBAL.Name);
        dbParam[2] = new DbParameter("@Email", DbParameter.DbType.VarChar, 100, objUsersBAL.Email);
        dbParam[3] = new DbParameter("@CurrentPage", DbParameter.DbType.Int, 10, (int)intCurrentPage);
        dbParam[3].ParamDirection = ParameterDirection.InputOutput;
        dbParam[4] = new DbParameter("@RecordPerPage", DbParameter.DbType.Int, 10, intRecordPerPage);
        dbParam[5] = new DbParameter("@TotalRecord", DbParameter.DbType.Int, 4, ParameterDirection.Output);
        if ((strSortColumn != string.Empty) && (strSortType != string.Empty))
        {
            dbParam[6] = new DbParameter("@SortOrd", DbParameter.DbType.VarChar, 20, strSortType);
            dbParam[7] = new DbParameter("@SortColumn", DbParameter.DbType.VarChar, 20, strSortColumn);
        }
        dbParam[8] = new DbParameter("@State", DbParameter.DbType.VarChar, 20, State);
        dbParam[9] = new DbParameter("@Suburb", DbParameter.DbType.VarChar, 2000, Suburb);
        dbParam[10] = new DbParameter("@Postcode", DbParameter.DbType.VarChar, 20, Postcode);
        DataTable table = new DataTable();
        table = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "UsersList", dbParam);
        intCurrentPage = Convert.ToInt32(dbParam[3].Value);
        intTotalRecord = Convert.ToInt32(dbParam[5].Value);
        return table;
    }
}