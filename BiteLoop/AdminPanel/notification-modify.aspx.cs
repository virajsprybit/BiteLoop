using BAL;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

public partial class AdminPanel_Notification_Modify : AdminAuthentication
{
    NotificationsBAL objNotificationsBAL = new NotificationsBAL();
    protected int IsSend = 0;
    #region Private Members
    private int _ID = 0;
    protected string strVendors = string.Empty;
    protected string strSalesAdmin = string.Empty;
    protected string strUsers = string.Empty;
    protected string strSelectedGroups = string.Empty;
    protected string strDate = string.Empty;
    protected int ShowDate = 0;
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
        BindGroups();

    }

    private void BindGroups()
    {
        NotificationGroupBAL objNotificationGroupBAL = new NotificationGroupBAL();
        DataTable dt = new DataTable();

        dt = objNotificationGroupBAL.NotificationGroupsListAdmin();
        if (dt.Rows.Count > 0)
        {
            rptGroup.DataSource = dt;
            rptGroup.DataBind();
        }
    }
    #endregion

    private void BindUserVendors()
    {
        NotificationsBAL objNotificationsBAL = new NotificationsBAL();
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
                if (Convert.ToInt16(dt.Rows[0]["IsSend"]) == 1)
                {
                    IsSend = 1;
                }
                tareaMessage.Value = Convert.ToString(dt.Rows[0]["Message"]);
                txtTitle.Value = Convert.ToString(dt.Rows[0]["Title"]);
                if (Convert.ToInt32(dt.Rows[0]["IsSchedule"]) == 1)
                {
                    chkIsSchedule.Checked = true;
                    strDate = Convert.ToDateTime(dt.Rows[0]["ScheduleTime"]).ToString("dd/MMM/yyyy HH:mm");
                    ShowDate = 1;
                }
                strVendors = Convert.ToString(dt.Rows[0]["Vendors"]);
                strUsers = Convert.ToString(dt.Rows[0]["Uses"]);
                strSalesAdmin = Convert.ToString(dt.Rows[0]["SalesAdmin"]);
                strSelectedGroups = Convert.ToString(dt.Rows[0]["SelectedGroups"]);
            }
        }
    }
    #endregion

    #region Save Information
    private void SaveInfo()
    {
        //  DateTime s = Convert.ToDateTime(Request[txtdatetime.UniqueID]);

        objNotificationsBAL.ID = ID;
        objNotificationsBAL.Message = Convert.ToString(Request[tareaMessage.UniqueID]).Trim();
        objNotificationsBAL.Vendors = Convert.ToString(Request[hdnVendors.UniqueID]).Trim();
        objNotificationsBAL.Users = Convert.ToString(Request[hdnUsers.UniqueID]).Trim();
        objNotificationsBAL.SalesAdmin = Convert.ToString(Request[hdnSalesAdmin.UniqueID]).Trim();
        objNotificationsBAL.Title = Convert.ToString(Request[txtTitle.UniqueID]).Trim();
        objNotificationsBAL.Groups = Convert.ToString(Request[hdnSelectedGroups.UniqueID]).Trim();

        if (Request[chkIsSchedule.UniqueID] != null)
        {
            objNotificationsBAL.ScheduleTime = Convert.ToDateTime(Request["txtDate"]);
            objNotificationsBAL.IsSchedule = 1;
        }
        else
        {
            objNotificationsBAL.IsSchedule = 0;
            objNotificationsBAL.ScheduleTime = Convert.ToDateTime("01/01/1990");
        }


        //long intResult = objNotificationsBAL.Save();
        long intResult = NotiFicationDetailsSave(objNotificationsBAL);


        switch (intResult)
        {
            default:

                if (objNotificationsBAL.IsSchedule == 0)
                {
                    CallNotifications(intResult);
                }

                Response.Write(Common.ShowMessage("Notification has been saved successfully.", "alert-message success", divMsg.ClientID));
                Response.Write(Common.ScriptStartTag + "parent.ScrollTop();" + Common.ScriptEndTag);
                Response.Write(Common.ScriptStartTag + "parent.window.setTimeout(\"parent.window.location.href = 'notifications-list.aspx'\",1000);" + Common.ScriptEndTag);
                break;
        }
        Response.End();

    }

    private async Task CallNotifications(long NotificationID)
    {
        DataSet ds = new DataSet();
        BusinessBAL objBusinessBAL = new BusinessBAL();
        ds = objBusinessBAL.GetOrderNotificationListByNotificationID(NotificationID);
        string strOrderID = string.Empty;
        string strMessage = string.Empty;
        DataTable dtUsers = new DataTable();
        SendNotification objSendNotification = new SendNotification();
        string strUsers = string.Empty;
        string strBusiness = string.Empty;
        string strBusinessDeviceKey = string.Empty;
        string strUsersDeviceKey = string.Empty;

        string strBusinessXML = string.Empty;
        string strUsersXML = string.Empty;
        // Process Notification mudules

        if (ds.Tables[0].Rows.Count > 0)
        {
            strOrderID = string.Empty;
            string strNotificationArray = string.Empty;
            string strNotificationArrayUsers = string.Empty;

            //Process Business
            //DataTable dtBusiness = ds.Tables[1];
            dtUsers = new DataTable();
            dtUsers = ds.Tables[1];
            for (int i = 0; i < dtUsers.Rows.Count; i++)
            {
                strMessage = Convert.ToString(dtUsers.Rows[i]["NotificationText"]);
                strMessage = EncodeNonAsciiCharacters(strMessage);
                int NotificationCount = 0;
                if (Convert.ToString(dtUsers.Rows[i]["NotificationCount"]) != string.Empty)
                {
                    NotificationCount = Convert.ToInt32(dtUsers.Rows[i]["NotificationCount"]);
                    NotificationCount = NotificationCount + 1;
                }

                if (Convert.ToString(dtUsers.Rows[i]["DeviceKey"]) != string.Empty)
                {
                    strBusiness = strBusiness + Convert.ToString(dtUsers.Rows[i]["UserID"]) + "###";
                    strBusinessDeviceKey = strBusinessDeviceKey + Convert.ToString(dtUsers.Rows[i]["DeviceKey"]) + "###";

                    strBusinessXML = strBusinessXML + "<Notification ID='" + Convert.ToString(dtUsers.Rows[i]["NotificationID"]) + "' DeviceKey='" + Convert.ToString(dtUsers.Rows[i]["DeviceKey"]) + "' UserID='" + Convert.ToString(dtUsers.Rows[i]["UserID"]) + "' />";

                    string deviceKey = Convert.ToString(dtUsers.Rows[i]["DeviceKey"]);
                    if (string.IsNullOrEmpty(deviceKey))
                        continue;

                    string title = Convert.ToString(dtUsers.Rows[i]["title"]);
                    string notificationId = Convert.ToString(dtUsers.Rows[i]["notificationid"]);
                    string message = EncodeNonAsciiCharacters(
                        Convert.ToString(dtUsers.Rows[i]["NotificationText"])
                    );

                    await objSendNotification.SendPushNotification(
                        deviceKey,
                        Server.MapPath("~/biteloop-b7f33-firebase-adminsdk-fbsvc-dd6ff02569.json"),
                        title,
                        strMessage,
                        notificationId,
                        "0"
                    );




                    //Task.Run(async () =>
                    //{

                    //    await objSendNotification.SendPushNotification(
                    //        Convert.ToString(dtUsers.Rows[i]["DeviceKey"]),
                    //        Server.MapPath("~/biteloop-b7f33-firebase-adminsdk-fbsvc-dd6ff02569.json"),
                    //        Convert.ToString(dtUsers.Rows[i]["title"]),
                    //        strMessage,
                    //        Convert.ToString(dtUsers.Rows[i]["notificationid"]),
                    //        "0"
                    //        );
                    //});

                    //if (Convert.ToString(dtUsers.Rows[i]["DeviceType"]).ToUpper() == "I")
                    //{
                    //    //strNotificationArray = strNotificationArray + GetIOSNotificationString(strNotificationArray, Convert.ToString(dtUsers.Rows[i]["Title"]), "B", strMessage, Convert.ToString(NotificationCount), Convert.ToString(dtUsers.Rows[i]["NotificationID"]), "BMHNotification", Convert.ToString(dtUsers.Rows[i]["DeviceKey"]));
                    //    objSendNotification.CallNotification(Convert.ToString(dtUsers.Rows[i]["DeviceKey"]), strMessage, Convert.ToString(dtUsers.Rows[i]["Title"]), Convert.ToString(dtUsers.Rows[i]["NotificationID"]), "BMHNotification", Convert.ToString(dtUsers.Rows[i]["DeviceType"]), "B", NotificationCount);
                    //}
                    //else
                    //{
                    //    objSendNotification.CallNotification(Convert.ToString(dtUsers.Rows[i]["DeviceKey"]), strMessage, Convert.ToString(dtUsers.Rows[i]["Title"]), Convert.ToString(dtUsers.Rows[i]["NotificationID"]), "BMHNotification", Convert.ToString(dtUsers.Rows[i]["DeviceType"]), "B", NotificationCount);
                    //}
                }
            }
            //Process Business


            //Process Sales Admin
            //dtBusiness = ds.Tables[2];
            dtUsers = new DataTable();
            dtUsers = ds.Tables[2];
            for (int i = 0; i < dtUsers.Rows.Count; i++)
            {
                strMessage = Convert.ToString(dtUsers.Rows[i]["NotificationText"]);
                strMessage = EncodeNonAsciiCharacters(strMessage);
                int NotificationCount = 0;
                if (Convert.ToString(dtUsers.Rows[i]["NotificationCount"]) != string.Empty)
                {
                    NotificationCount = Convert.ToInt32(dtUsers.Rows[i]["NotificationCount"]);
                    NotificationCount = NotificationCount + 1;
                }

                if (Convert.ToString(dtUsers.Rows[i]["DeviceKey"]) != string.Empty)
                {
                    strBusiness = strBusiness + Convert.ToString(dtUsers.Rows[i]["UserID"]) + "###";
                    strBusinessDeviceKey = strBusinessDeviceKey + Convert.ToString(dtUsers.Rows[i]["DeviceKey"]) + "###";
                    strBusinessXML = strBusinessXML + "<Notification ID='" + Convert.ToString(dtUsers.Rows[i]["NotificationID"]) + "' DeviceKey='" + Convert.ToString(dtUsers.Rows[i]["DeviceKey"]) + "' UserID='" + Convert.ToString(dtUsers.Rows[i]["UserID"]) + "' />";
                    //SendNotification objSendNotification = new SendNotification();



                    string deviceKey = Convert.ToString(dtUsers.Rows[i]["DeviceKey"]);
                    if (string.IsNullOrEmpty(deviceKey))
                        continue;

                    string title = Convert.ToString(dtUsers.Rows[i]["title"]);
                    string notificationId = Convert.ToString(dtUsers.Rows[i]["notificationid"]);
                    string message = EncodeNonAsciiCharacters(
                        Convert.ToString(dtUsers.Rows[i]["NotificationText"])
                    );

                    await objSendNotification.SendPushNotification(
                        deviceKey,
                        Server.MapPath("~/biteloop-b7f33-firebase-adminsdk-fbsvc-dd6ff02569.json"),
                        title,
                        strMessage,
                        notificationId,
                        "0"
                    );



                    //Task.Run(async () =>
                    //{
                    //    await objSendNotification.SendPushNotification(
                    //        Convert.ToString(dtUsers.Rows[i]["DeviceKey"]),
                    //        Server.MapPath("~/biteloop-b7f33-firebase-adminsdk-fbsvc-dd6ff02569.json"),
                    //        Convert.ToString(dtUsers.Rows[i]["title"]),
                    //        strMessage,
                    //        Convert.ToString(dtUsers.Rows[i]["notificationid"]),
                    //        "0"
                    //        );
                    //});
                    //if (convert.tostring(dtusers.rows[i]["devicetype"]).toupper() == "i")
                    //{
                    //    //strnotificationarray = strnotificationarray + getiosnotificationstring(strnotificationarray, convert.tostring(dtusers.rows[i]["title"]), "b", strmessage, convert.tostring(notificationcount), convert.tostring(dtusers.rows[i]["notificationid"]), "bmhnotification", convert.tostring(dtusers.rows[i]["devicekey"]));
                    //    objsendnotification.callnotification(convert.tostring(dtusers.rows[i]["devicekey"]), strmessage, convert.tostring(dtusers.rows[i]["title"]), convert.tostring(dtusers.rows[i]["notificationid"]), "bmhnotification", convert.tostring(dtusers.rows[i]["devicetype"]), "b", notificationcount);
                    //}
                    //else
                    //{
                    //    objsendnotification.callnotification(convert.tostring(dtusers.rows[i]["devicekey"]), strmessage, convert.tostring(dtusers.rows[i]["title"]), convert.tostring(dtusers.rows[i]["notificationid"]), "bmhnotification", convert.tostring(dtusers.rows[i]["devicetype"]), "b", notificationcount);
                    //}
                }
            }
            //Process Business

            //Process User
            //dtBusiness = ds.Tables[3];
            dtUsers = new DataTable();
            dtUsers = ds.Tables[3];
            for (int i = 0; i < dtUsers.Rows.Count; i++)
            {
                strMessage = Convert.ToString(dtUsers.Rows[i]["NotificationText"]);
                strMessage = EncodeNonAsciiCharacters(strMessage);
                int NotificationCount = 0;
                if (Convert.ToString(dtUsers.Rows[i]["NotificationCount"]) != string.Empty)
                {
                    NotificationCount = Convert.ToInt32(dtUsers.Rows[i]["NotificationCount"]);
                    NotificationCount = NotificationCount + 1;
                }

                if (Convert.ToString(dtUsers.Rows[i]["DeviceKey"]) != string.Empty)
                {
                    strUsers = strUsers + Convert.ToString(dtUsers.Rows[i]["UserID"]) + "###";
                    strUsersDeviceKey = strUsersDeviceKey + Convert.ToString(dtUsers.Rows[i]["DeviceKey"]) + "###";
                    strUsersXML = strUsersXML + "<Notification ID='" + Convert.ToString(dtUsers.Rows[i]["NotificationID"]) + "' DeviceKey='" + Convert.ToString(dtUsers.Rows[i]["DeviceKey"]) + "' UserID='" + Convert.ToString(dtUsers.Rows[i]["UserID"]) + "' />";


                    string deviceKey = Convert.ToString(dtUsers.Rows[i]["DeviceKey"]);
                    if (string.IsNullOrEmpty(deviceKey))
                        continue;

                    string title = Convert.ToString(dtUsers.Rows[i]["title"]);
                    string notificationId = Convert.ToString(dtUsers.Rows[i]["notificationid"]);
                    string message = EncodeNonAsciiCharacters(
                        Convert.ToString(dtUsers.Rows[i]["NotificationText"])
                    );

                    await objSendNotification.SendPushNotification(
                        deviceKey,
                        Server.MapPath("~/biteloop-b7f33-firebase-adminsdk-fbsvc-dd6ff02569.json"),
                        title,
                        strMessage,
                        notificationId,
                        "0"
                    );


                    //WriteLogFile("6: " + Convert.ToString(dtUsers.Rows[i]["DeviceKey"]) + " --> " + Convert.ToString(dtUsers.Rows[i]["title"]) + " --> " + strMessage);
                    //Task.Run(async () =>
                    //{
                    //    WriteLogFile("7: " + Convert.ToString(dtUsers.Rows[i]["DeviceKey"]) + " --> " + Convert.ToString(dtUsers.Rows[i]["title"]) + " --> " + strMessage);

                    //    await objSendNotification.SendPushNotification(
                    //        Convert.ToString(dtUsers.Rows[i]["DeviceKey"]),
                    //        Server.MapPath("~/biteloop-b7f33-firebase-adminsdk-fbsvc-dd6ff02569.json"),
                    //        Convert.ToString(dtUsers.Rows[i]["title"]),
                    //        strMessage,
                    //        Convert.ToString(dtUsers.Rows[i]["notificationid"]),
                    //        "0"
                    //        );
                    //});
                    //if (Convert.ToString(dtUsers.Rows[i]["DeviceType"]).ToUpper() == "I")
                    //{
                    //    //strNotificationArrayUsers = strNotificationArrayUsers + GetIOSNotificationString(strNotificationArrayUsers, Convert.ToString(dtUsers.Rows[i]["Title"]), "U", strMessage, Convert.ToString(NotificationCount), Convert.ToString(dtUsers.Rows[i]["NotificationID"]), "BMHNotification", Convert.ToString(dtUsers.Rows[i]["DeviceKey"]));
                    //    objSendNotification.CallNotification(Convert.ToString(dtUsers.Rows[i]["DeviceKey"]), strMessage, Convert.ToString(dtUsers.Rows[i]["Title"]), Convert.ToString(dtUsers.Rows[i]["NotificationID"]), "BMHNotification", Convert.ToString(dtUsers.Rows[i]["DeviceType"]), "U", NotificationCount);
                    //}
                    //else
                    //{
                    //    objSendNotification.CallNotification(Convert.ToString(dtUsers.Rows[i]["DeviceKey"]), strMessage, Convert.ToString(dtUsers.Rows[i]["Title"]), Convert.ToString(dtUsers.Rows[i]["NotificationID"]), "BMHNotification", Convert.ToString(dtUsers.Rows[i]["DeviceType"]), "U", NotificationCount);
                    //}
                }
            }
            //Process Business


            //Process IOS Notifications

            //if (strNotificationArray != string.Empty)
            //{
            //    objSendNotification.SendNotificationToIOSMultiple("[" + strNotificationArray + "]");
            //}
            //if (strNotificationArrayUsers != string.Empty)
            //{
            //    objSendNotification.SendNotificationToIOSMultipleUsers("[" + strNotificationArrayUsers + "]");
            //}
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strOrderID = strOrderID + Convert.ToString(ds.Tables[0].Rows[i]["NotificationID"]) + ",";
            }
            if (strOrderID.Length > 0)
            {
                strOrderID = strOrderID.Substring(0, strOrderID.Length - 1);
                objBusinessBAL.UpdateNoticationsForNitificationModule(strOrderID);
            }

            if (strUsers.Length > 0)
            {
                strUsers = strUsers.Substring(0, strUsers.Length - 3);
            }
            if (strBusiness.Length > 0)
            {
                strBusiness = strBusiness.Substring(0, strBusiness.Length - 3);
            }
            if (strBusinessDeviceKey.Length > 0)
            {
                strBusinessDeviceKey = strBusinessDeviceKey.Substring(0, strBusinessDeviceKey.Length - 3);
            }
            if (strUsersDeviceKey.Length > 0)
            {
                strUsersDeviceKey = strUsersDeviceKey.Substring(0, strUsersDeviceKey.Length - 3);
            }


            if (strBusiness != string.Empty || strUsers != string.Empty)
            {
                //GeneralBAL objGeneralBAL = new GeneralBAL();
                //objGeneralBAL.UpdateNoticationsCountForUsers(strUsers, strBusiness);
                UpdateNoticationsCountForUsersWithDeviceKey(strUsers, strBusiness, "<Notifications>" + strBusinessXML + "</Notifications>", "<Notifications>" + strUsersXML + "</Notifications>");
            }
        }
    }

    private string GetIOSNotificationString(string strNotification, string title, string AppType, string message, string NotificationCount, string strMessageID, string NotificationType, string deviceId)
    {
        message = EncodeNonAsciiCharacters(message);

        string strText = string.Empty;
        if (strNotification == string.Empty)
            strText = "{";
        else
            strText = ",{";
        strText = strText + "\"title\": \"" + title + "\",";
        strText = strText + "\"AppType\": \"" + AppType + "\",";
        strText = strText + "\"description\": \"" + message + "\",";
        strText = strText + "\"badge\": \"" + NotificationCount + "\",";
        strText = strText + "\"id\": \"" + strMessageID + "\",";
        strText = strText + "\"NotificationType\": \"" + NotificationType + "\",";
        strText = strText + "\"device_token\": [";
        strText = strText + "\"" + deviceId + "\"";
        strText = strText + "]";
        strText = strText + "}";

        return strText;
    }
    #endregion

    public void UpdateNoticationsCountForUsersWithDeviceKey(string strUsers, string strBusiness, string strBusinessXML, string strUsersXML)
    {

        using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 3000;
                cmd.CommandText = "UpdateNoticationsCountForUsersWithDeviceKeyXML";
                cmd.Parameters.Add(new SqlParameter("@Users", strUsers));
                cmd.Parameters.Add(new SqlParameter("@Business", strBusiness));
                cmd.Parameters.Add(new SqlParameter("@BusinessXML", strBusinessXML));
                cmd.Parameters.Add(new SqlParameter("@UserXML", strUsersXML));

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();



            }
            catch (Exception ex)
            {

            }
        }
        //DbParameter[] dbParam = new DbParameter[] {                 
        //        new DbParameter("@Users", DbParameter.DbType.VarChar, 8000, strUsers),
        //        new DbParameter("@Business", DbParameter.DbType.VarChar, 8000, strBusiness),
        //        new DbParameter("@BusinessXML", DbParameter.DbType.VarChar, 80000, strBusinessXML),
        //        new DbParameter("@UserXML", DbParameter.DbType.VarChar, 80000, strUsersXML)
        //        };
        //DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "UpdateNoticationsCountForUsersWithDeviceKeyXML", dbParam);
    }

    public long NotiFicationDetailsSave(NotificationsBAL objNotificationsBAL)
    {
        DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@ID", DbParameter.DbType.Int, 40, objNotificationsBAL.ID),
                new DbParameter("@Message", DbParameter.DbType.NVarChar, 8000, objNotificationsBAL.Message),
                new DbParameter("@Vendors", DbParameter.DbType.VarChar, 80000, objNotificationsBAL.Vendors),
                new DbParameter("@Users", DbParameter.DbType.VarChar, 80000, objNotificationsBAL.Users),
                new DbParameter("@SalesAdmin", DbParameter.DbType.VarChar, 80000, objNotificationsBAL.SalesAdmin),
                new DbParameter("@IsSchedule", DbParameter.DbType.Int, 4, objNotificationsBAL.IsSchedule),
                new DbParameter("@ScheduleTime", DbParameter.DbType.DateTime, 8000, objNotificationsBAL.ScheduleTime),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 40, ParameterDirection.Output),
                new DbParameter("@Title", DbParameter.DbType.NVarChar, 8000, objNotificationsBAL.Title),
                new DbParameter("@Groups", DbParameter.DbType.VarChar, 8000, objNotificationsBAL.Groups)
            };
        DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "NotificationModify", dbParam);
        return Convert.ToInt64(dbParam[7].Value);
    }

    string EncodeNonAsciiCharacters(string value)
    {
        StringBuilder sb = new StringBuilder();
        foreach (char c in value)
        {
            if (c > 127)
            {
                // This character is too big for ASCII
                string encodedValue = "\\u" + ((int)c).ToString("x4");
                sb.Append(encodedValue);
            }
            else
            {
                sb.Append(c);
            }
        }
        return sb.ToString();
    }

    private void WriteLogFile(string LogMessage)
    {
        try
        {

            string path = System.Web.Hosting.HostingEnvironment.MapPath("~/source") + "\\LogFile.txt";
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("---------------------------------------------------------------------");
                    sw.WriteLine(LogMessage);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine("---------------------------------------------------------------------");
                    sw.WriteLine(LogMessage);
                }
            }
        }
        catch (Exception ex)
        {

        }

    }

}
