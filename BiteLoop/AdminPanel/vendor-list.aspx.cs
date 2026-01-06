using BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using System.Data;
using DAL;
using BiteLoop.Common;
using System.Web.Services;
using System.Web.Script.Services;
using Newtonsoft.Json;
using BiteLoop.Common;
public partial class AdminPanel_Vendor_List : AdminAuthentication
{
    private BusinessBAL objBusinessBAL = new BusinessBAL();
    public static string strColumn = string.Empty;
    private string strEmail = string.Empty;
    private string strName = string.Empty;
    private string strLocation = string.Empty;
    private string strSuburb = string.Empty;
    private string strPostCode = string.Empty;

    private int intStatus = -1;
    private string strSubject = string.Empty;
    public static string strTypes = string.Empty;
    protected string VendorName = string.Empty;
    protected long VID = 0;
    private void BindContactList()
    {
        if (Request["vid"] != null)
        {
            this.objBusinessBAL.ID = Convert.ToInt64(Request["vid"]);
        }
        else
        {
            this.objBusinessBAL.ID = 0;
        }
        this.objBusinessBAL.Name = this.strName.Trim();
        this.objBusinessBAL.EmailAddress = this.strEmail.Trim();
        this.objBusinessBAL.Status = this.intStatus;
        this.objBusinessBAL.Location = this.strLocation;
        string Suburb = this.strSuburb;
        this.objBusinessBAL.PostCode = this.strPostCode;

        int intTotalRecord = 0;
        DataTable dtTable = new DataTable();
        //dtTable = this.objBusinessBAL.GetList(ref this.CurrentPage, base.RecordPerPage, out intTotalRecord, base.SortColumn, base.SortType);
        dtTable = GetListDAL(ref this.CurrentPage, base.RecordPerPage, out intTotalRecord, base.SortColumn, base.SortType, objBusinessBAL, Suburb);

        if (!string.IsNullOrEmpty(base.Request["ysnExport"]))
        {
            this.objBusinessBAL.Name = Convert.ToString(base.Request["txtName"]);
            this.objBusinessBAL.EmailAddress = Convert.ToString(base.Request["txtEmail"]);
            if (Request["ddlStatus"] != null)
            {
                this.objBusinessBAL.Status = Convert.ToInt32(base.Request["ddlStatus"]);
            }
            else
            {
                this.objBusinessBAL.Status = -1;
            }
            this.objBusinessBAL.Location = Convert.ToString(base.Request["ddlState"]);
            Suburb = Convert.ToString(base.Request["ddlSuburb"]);
            this.objBusinessBAL.PostCode = Convert.ToString(base.Request["txtPostCode"]);

            if ((dtTable != null) || (dtTable.Rows.Count > 0))
            {
                if (string.IsNullOrEmpty(strTypes))
                {
                    strTypes = "DESC";
                }
                if (string.IsNullOrEmpty(strColumn))
                {
                    strColumn = "Business.ID";
                }
                // dtTable = this.objBusinessBAL.GetList(ref this.CurrentPage, -1, out intTotalRecord, strColumn, strTypes);
                dtTable = GetListDAL(ref this.CurrentPage, -1, out intTotalRecord, strColumn, strTypes, objBusinessBAL, Suburb);
                Common.ExportToCSVComma("BusinessList-" + DateTime.Now.ToString("dd-MM-yyyy") + ".csv", dtTable, true);
            }
        }
        else if (dtTable.Rows.Count > 0)
        {
            if (Request["vid"] != null)
            {
                VendorName = Convert.ToString(dtTable.Rows[0]["Name"]);
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

    private void BusinessOperation()
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
            case "cancel":
                ObjOpr = Common.DataBaseOperation.None;
                strMessage = "Selected record(s) has been cancelled successfully.";
                break;
        }
        this.objBusinessBAL.Operation(Convert.ToString(base.Request["hdnID"]), ObjOpr);
        base.Response.Write("<script>DisplMsg('" + this.divMsg.ClientID + "','" + strMessage + "','alert-message success')</script>");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["vid"] != null)
        {
            VID = Convert.ToInt64(Request["vid"]);
        }
        if (base.Request.Form.Keys.Count > 0)
        {
            this.strName = base.Request["txtName"];
            this.strEmail = base.Request["txtEmail"];
            this.intStatus = Convert.ToInt32(base.Request["ddlStatus"]);

            if (Request[ddlState.UniqueID] != null)
            {
                if (Convert.ToString(Request[ddlState.UniqueID]).Trim() != string.Empty)
                    this.strLocation = Convert.ToString(Request[ddlState.UniqueID]);
            }
            if (Request[ddlSuburb.UniqueID] != null)
            {
                if (Convert.ToString(Request[ddlSuburb.UniqueID]).Trim() != string.Empty)
                    this.strSuburb = Convert.ToString(Request[ddlSuburb.UniqueID]);
            }
            this.strPostCode = base.Request["txtPostCode"];
            if (!int.TryParse(base.Request["page"], out this.CurrentPage))
            {
                base.CurrentPage = 1;
            }
            if (!string.IsNullOrEmpty(base.Request["type"]))
            {
                this.BusinessOperation();
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
            base.SortColumn = "Business.ID";
        }
        this.BindContactList();
        this.BindState();

    }

    private DataTable GetListDAL(ref int intCurrentPage, int intRecordPerPage, out int intTotalRecord, string strSortColumn, string strSortType, BusinessBAL objBusinessBAL, string Suburb)
    {
        DbParameter[] dbParam = new DbParameter[12];
        dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 20, objBusinessBAL.ID);
        dbParam[1] = new DbParameter("@Name", DbParameter.DbType.VarChar, 100, objBusinessBAL.Name);
        dbParam[2] = new DbParameter("@Email", DbParameter.DbType.VarChar, 100, objBusinessBAL.EmailAddress);
        dbParam[3] = new DbParameter("@CurrentPage", DbParameter.DbType.Int, 10, (int)intCurrentPage);
        dbParam[3].ParamDirection = ParameterDirection.InputOutput;
        dbParam[4] = new DbParameter("@RecordPerPage", DbParameter.DbType.Int, 10, intRecordPerPage);
        dbParam[5] = new DbParameter("@TotalRecord", DbParameter.DbType.Int, 4, ParameterDirection.Output);
        if ((strSortColumn != string.Empty) && (strSortType != string.Empty))
        {
            dbParam[6] = new DbParameter("@SortOrd", DbParameter.DbType.VarChar, 20, strSortType);
            dbParam[7] = new DbParameter("@SortColumn", DbParameter.DbType.VarChar, 20, strSortColumn);
        }
        dbParam[8] = new DbParameter("@Status", DbParameter.DbType.Int, 20, objBusinessBAL.Status);
        dbParam[9] = new DbParameter("@StateCode", DbParameter.DbType.VarChar, 20, objBusinessBAL.Location);
        dbParam[10] = new DbParameter("@Suburb", DbParameter.DbType.VarChar, 255, Suburb);
        dbParam[11] = new DbParameter("@PostCode", DbParameter.DbType.VarChar, 10, objBusinessBAL.PostCode);

        DataTable table = new DataTable();
        table = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "BusinessList", dbParam);
        intCurrentPage = Convert.ToInt32(dbParam[3].Value);
        intTotalRecord = Convert.ToInt32(dbParam[5].Value);
        return table;
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
        ddlState.Items.Insert(0, new ListItem("--All States--", ""));
    }

    [System.Web.Services.WebMethod]
    public static StateSuburbs[] BindSuburb(string stateCode)
    {
        List<StateSuburbs> List = new List<StateSuburbs>();
        if (stateCode != "")
        {
            DataTable dt = new DataTable();
            CommonBAL objCommonBAL = new CommonBAL();
            dt = objCommonBAL.SuburbListByStateCode(stateCode);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StateSuburbs obj = new StateSuburbs();
                    obj.Suburb = Convert.ToString(dt.Rows[i]["suburb"]);
                    List.Add(obj);
                }
            }
        }
        if (List.Count == 0)
        {
            List = null;
        }
        return List.ToArray();
    }
}