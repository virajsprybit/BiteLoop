using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using DAL;
using System.Data.SqlClient;

public partial class AdminPanel_Notification_Group_Modify : AdminAuthentication
{
    NotificationGroupBAL objNotificationsBAL = new NotificationGroupBAL();

    #region Private Members
    private int _ID = 0;
    protected string strVendors = string.Empty;
    protected string strSalesAdmin = string.Empty;
    protected string strUsers = string.Empty;        
    #endregion

    #region Public Members
    public new int ID
    {
        get
        {
            return _ID;
        }
    }
    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {

        Int32.TryParse(Request["id"], out _ID);
        objNotificationsBAL.ID = ID;

        if (Request.Form.Keys.Count > 0)
        { SaveInfo(); }
        else
        {
            if (objNotificationsBAL.ID != 0)
            { BindControls(); }
        }

        BindUserVendors();

    }
    #endregion

    private void BindUserVendors()
    {
        NotificationGroupBAL objNotificationsBAL = new NotificationGroupBAL();
        DataSet ds = new DataSet();
        ds = objNotificationsBAL.NotificationsUsersVendors();

        if (ds.Tables[0].Rows.Count > 0)
        {
            rptUsers.DataSource = ds.Tables[0];
            rptUsers.DataBind();
        }

        if (ds.Tables[1].Rows.Count > 0)
        {
            rptSalesAdmin.DataSource = ds.Tables[1];
            rptSalesAdmin.DataBind();
        }

        if (ds.Tables[2].Rows.Count > 0)
        {
            rptVendors.DataSource = ds.Tables[2];
            rptVendors.DataBind();
        }
    }



    #region Bind Controls
    private void BindControls()
    {
        if (ID > 0)
        {
            DataTable dt = new DataTable();
            objNotificationsBAL.ID = ID;

            dt = objNotificationsBAL.GetByID();
            if (dt.Rows.Count > 0)
            {
                txtTitle.Value = Convert.ToString(dt.Rows[0]["Title"]);
                strVendors = Convert.ToString(dt.Rows[0]["Vendors"]);
                strUsers = Convert.ToString(dt.Rows[0]["Uses"]);
                strSalesAdmin = Convert.ToString(dt.Rows[0]["SalesAdmin"]);
            }
        }
    }
    #endregion

    #region Save Information
    private void SaveInfo()
    {
        //  DateTime s = Convert.ToDateTime(Request[txtdatetime.UniqueID]);

        objNotificationsBAL.ID = ID;
        objNotificationsBAL.Title = Convert.ToString(Request[txtTitle.UniqueID]).Trim();
        objNotificationsBAL.Vendors = Convert.ToString(Request[hdnVendors.UniqueID]).Trim();
        objNotificationsBAL.Users = Convert.ToString(Request[hdnUsers.UniqueID]).Trim();
        objNotificationsBAL.SalesAdmin = Convert.ToString(Request[hdnSalesAdmin.UniqueID]).Trim();

        
        long intResult = objNotificationsBAL.Save();       


        switch (intResult)
        {
            default:
               
                Response.Write(Common.ShowMessage("Notification Group has been saved successfully.", "alert-message success", divMsg.ClientID));
                Response.Write(Common.ScriptStartTag + "parent.ScrollTop();" + Common.ScriptEndTag);
                Response.Write(Common.ScriptStartTag + "parent.window.setTimeout(\"parent.window.location.href = 'notifications-group-list.aspx'\",1000);" + Common.ScriptEndTag);
                break;
        }
        Response.End();

    }

      #endregion

 
    //public long NotiFicationDetailsSave(NotificationGroupBAL objNotificationsBAL)
    //{
    //    DbParameter[] dbParam = new DbParameter[] { 
    //            new DbParameter("@ID", DbParameter.DbType.Int, 40, objNotificationsBAL.ID), 
    //            new DbParameter("@Message", DbParameter.DbType.VarChar, 8000, objNotificationsBAL.Message), 
    //            new DbParameter("@Vendors", DbParameter.DbType.VarChar, 80000, objNotificationsBAL.Vendors),        
    //            new DbParameter("@Users", DbParameter.DbType.VarChar, 80000, objNotificationsBAL.Users),        
    //            new DbParameter("@SalesAdmin", DbParameter.DbType.VarChar, 80000, objNotificationsBAL.SalesAdmin),        
    //            new DbParameter("@IsSchedule", DbParameter.DbType.Int, 4, objNotificationsBAL.IsSchedule),
    //            new DbParameter("@ScheduleTime", DbParameter.DbType.DateTime, 8000, objNotificationsBAL.ScheduleTime),
    //            new DbParameter("@ReturnVal", DbParameter.DbType.Int, 40, ParameterDirection.Output) 
    //        };
    //    DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "NotificationModify", dbParam);
    //    return Convert.ToInt64(dbParam[7].Value);
    //}
}