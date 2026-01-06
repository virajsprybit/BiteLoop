using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using Utility;
using Newtonsoft.Json;
using System.Data;
using System.IO;
using DAL;

public partial class Business_OrderNotifications_cronjob : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["OrderNotification"] != null)
        {
            OrderNotification();
        }
    }

    private void OrderNotification()
    {
        try
        {
            DataSet ds = new DataSet();
            DataTable dtUsers = new DataTable();
            BusinessBAL objBusinessBAL = new BusinessBAL();
            ds = objBusinessBAL.GetOrderNotificationList();
            dtUsers = ds.Tables[0];
            SendNotification objSendNotification = new SendNotification();
            string strMessage = string.Empty;
            string strUsers = string.Empty;
            string strBusiness = string.Empty;
            string strBusinessDeviceKey = string.Empty;
            string strUsersDeviceKey = string.Empty;

            string strBusinessXML = string.Empty;
            string strUsersXML = string.Empty;
            // Process Order Notifications

            string strOrderID = string.Empty;
            if (dtUsers.Rows.Count > 0)
            {

                strMessage = "Your order is now ready for pick up!";
                for (int i = 0; i < dtUsers.Rows.Count; i++)
                {
                    strOrderID = strOrderID + Convert.ToString(dtUsers.Rows[i]["OrderID"]) + ",";
                    if (Convert.ToString(dtUsers.Rows[i]["DeviceKey"]) != string.Empty)
                    {
                        strUsers = strUsers + Convert.ToString(dtUsers.Rows[i]["UserID"]) + ",";

                        int NotificationCount = 0;
                        if (Convert.ToString(dtUsers.Rows[i]["NotificationCount"]) != string.Empty)
                        {
                            NotificationCount = Convert.ToInt32(dtUsers.Rows[i]["NotificationCount"]);
                            NotificationCount = NotificationCount + 1;
                        }
                        objSendNotification.CallNotification(Convert.ToString(dtUsers.Rows[i]["DeviceKey"]), strMessage, "Order Pickup", Convert.ToString(dtUsers.Rows[i]["OrderID"]), "BMHOrderPickup", Convert.ToString(dtUsers.Rows[i]["DeviceType"]), "U", NotificationCount);
                    }
                }

                if (strOrderID.Length > 0)
                {
                    strOrderID = strOrderID.Substring(0, strOrderID.Length - 1);

                    objBusinessBAL.UpdateOrderNotications(strOrderID);
                }
            }

            // Process Order Notifications


            // Process Notification mudules
            WriteLogFile("ds.Tables[1].Rows.Count: " + ds.Tables[1].Rows.Count);
            if (ds.Tables[1].Rows.Count > 0)
            {
                strOrderID = string.Empty;
                string strNotificationArray = string.Empty;
                string strNotificationArrayUsers = string.Empty;

                //Process Business
                //DataTable dtBusiness = ds.Tables[2];
                dtUsers = new DataTable();
                dtUsers = ds.Tables[2];
                WriteLogFile("Users 1: " + dtUsers.Rows.Count.ToString());
                for (int i = 0; i < dtUsers.Rows.Count; i++)
                {
                    strMessage = Convert.ToString(dtUsers.Rows[i]["NotificationText"]);
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
                        if (Convert.ToString(dtUsers.Rows[i]["DeviceType"]).ToUpper() == "I")
                        {
                            //strNotificationArray = strNotificationArray + GetIOSNotificationString(strNotificationArray, Convert.ToString(dtUsers.Rows[i]["Title"]), "B", strMessage, Convert.ToString(NotificationCount), Convert.ToString(dtUsers.Rows[i]["NotificationID"]), "BMHNotification", Convert.ToString(dtUsers.Rows[i]["DeviceKey"]));
                            objSendNotification.CallNotification(Convert.ToString(dtUsers.Rows[i]["DeviceKey"]), strMessage, Convert.ToString(dtUsers.Rows[i]["Title"]), Convert.ToString(dtUsers.Rows[i]["NotificationID"]), "BMHNotification", Convert.ToString(dtUsers.Rows[i]["DeviceType"]), "B", NotificationCount);
                        }
                        else
                        {
                            objSendNotification.CallNotification(Convert.ToString(dtUsers.Rows[i]["DeviceKey"]), strMessage, Convert.ToString(dtUsers.Rows[i]["Title"]), Convert.ToString(dtUsers.Rows[i]["NotificationID"]), "BMHNotification", Convert.ToString(dtUsers.Rows[i]["DeviceType"]), "B", NotificationCount);
                        }
                    }
                }
                //Process Business


                //Process Sales Admin
                //dtBusiness = ds.Tables[3];
                dtUsers = new DataTable();
                dtUsers = ds.Tables[3];
                WriteLogFile("Users 2: " + dtUsers.Rows.Count.ToString());
                for (int i = 0; i < dtUsers.Rows.Count; i++)
                {
                    strMessage = Convert.ToString(dtUsers.Rows[i]["NotificationText"]);
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
                        if (Convert.ToString(dtUsers.Rows[i]["DeviceType"]).ToUpper() == "I")
                        {
                            //strNotificationArray = strNotificationArray + GetIOSNotificationString(strNotificationArray, Convert.ToString(dtUsers.Rows[i]["Title"]), "B", strMessage, Convert.ToString(NotificationCount), Convert.ToString(dtUsers.Rows[i]["NotificationID"]), "BMHNotification", Convert.ToString(dtUsers.Rows[i]["DeviceKey"]));
                            objSendNotification.CallNotification(Convert.ToString(dtUsers.Rows[i]["DeviceKey"]), strMessage, Convert.ToString(dtUsers.Rows[i]["Title"]), Convert.ToString(dtUsers.Rows[i]["NotificationID"]), "BMHNotification", Convert.ToString(dtUsers.Rows[i]["DeviceType"]), "B", NotificationCount);
                        }
                        else
                        {
                            objSendNotification.CallNotification(Convert.ToString(dtUsers.Rows[i]["DeviceKey"]), strMessage, Convert.ToString(dtUsers.Rows[i]["Title"]), Convert.ToString(dtUsers.Rows[i]["NotificationID"]), "BMHNotification", Convert.ToString(dtUsers.Rows[i]["DeviceType"]), "B", NotificationCount);
                        }
                    }
                }
                //Process Business

                //Process User
                //dtBusiness = ds.Tables[4];
                dtUsers = new DataTable();
                dtUsers = ds.Tables[4];
                WriteLogFile("Users 3: " + dtUsers.Rows.Count.ToString());

                for (int i = 0; i < dtUsers.Rows.Count; i++)
                {
                    strMessage = Convert.ToString(dtUsers.Rows[i]["NotificationText"]);
                    int NotificationCount = 0;
                    if (Convert.ToString(dtUsers.Rows[i]["NotificationCount"]) != string.Empty)
                    {
                        NotificationCount = Convert.ToInt32(dtUsers.Rows[i]["NotificationCount"]);
                        NotificationCount = NotificationCount + 1;
                    }
                    WriteLogFile("DeviceKey: " + Convert.ToString(dtUsers.Rows[i]["DeviceKey"]));
                    if (Convert.ToString(dtUsers.Rows[i]["DeviceKey"]) != string.Empty)
                    {

                        strUsers = strUsers + Convert.ToString(dtUsers.Rows[i]["UserID"]) + "###";
                        strUsersDeviceKey = strUsersDeviceKey + Convert.ToString(dtUsers.Rows[i]["DeviceKey"]) + "###";
                        strUsersXML = strUsersXML + "<Notification ID='" + Convert.ToString(dtUsers.Rows[i]["NotificationID"]) + "' DeviceKey='" + Convert.ToString(dtUsers.Rows[i]["DeviceKey"]) + "' UserID='" + Convert.ToString(dtUsers.Rows[i]["UserID"]) + "' />";
                        if (Convert.ToString(dtUsers.Rows[i]["DeviceType"]).ToUpper() == "I")
                        {
                            //strNotificationArrayUsers = strNotificationArrayUsers + GetIOSNotificationString(strNotificationArrayUsers, Convert.ToString(dtUsers.Rows[i]["Title"]), "U", strMessage, Convert.ToString(NotificationCount), Convert.ToString(dtUsers.Rows[i]["NotificationID"]), "BMHNotification", Convert.ToString(dtUsers.Rows[i]["DeviceKey"]));
                            objSendNotification.CallNotification(Convert.ToString(dtUsers.Rows[i]["DeviceKey"]), strMessage, Convert.ToString(dtUsers.Rows[i]["Title"]), Convert.ToString(dtUsers.Rows[i]["NotificationID"]), "BMHNotification", Convert.ToString(dtUsers.Rows[i]["DeviceType"]), "U", NotificationCount);
                        }
                        else
                        {
                            objSendNotification.CallNotification(Convert.ToString(dtUsers.Rows[i]["DeviceKey"]), strMessage, Convert.ToString(dtUsers.Rows[i]["Title"]), Convert.ToString(dtUsers.Rows[i]["NotificationID"]), "BMHNotification", Convert.ToString(dtUsers.Rows[i]["DeviceType"]), "U", NotificationCount);
                        }
                    }
                }
                //Process Business
                long NotificationID = 0;
                if (ds.Tables[1].Rows.Count > 0)
                {
                    NotificationID = Convert.ToInt64(ds.Tables[1].Rows[0]["NotificationID"]);
                }
                if (strNotificationArray != string.Empty)
                {
                    objSendNotification.SendNotificationToIOSMultiple("[" + strNotificationArray + "]");
                }
                if (strNotificationArrayUsers != string.Empty)
                {
                    objSendNotification.SendNotificationToIOSMultipleUsers("[" + strNotificationArrayUsers + "]");
                }
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    strOrderID = strOrderID + Convert.ToString(ds.Tables[1].Rows[i]["NotificationID"]) + ",";
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


            // Process Notification mudules


        }
        catch (Exception ex)
        {
            lbkMessage.Text = ex.Message.ToString();
            WriteLogFile(ex.Message.ToString());

        }
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
                    sw.WriteLine(LogMessage);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(LogMessage);
                }
            }
        }
        catch (Exception ex)
        {

        }

    }

    private string GetIOSNotificationString(string strNotification, string title, string AppType, string message, string NotificationCount, string strMessageID, string NotificationType, string deviceId)
    {
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

    public void UpdateNoticationsCountForUsersWithDeviceKey(string strUsers, string strBusiness, string strBusinessXML, string strUsersXML)
    {
        DbParameter[] dbParam = new DbParameter[] {                 
                new DbParameter("@Users", DbParameter.DbType.VarChar, 8000, strUsers),
                new DbParameter("@Business", DbParameter.DbType.VarChar, 8000, strBusiness),
                new DbParameter("@BusinessXML", DbParameter.DbType.Xml, 80000, strBusinessXML),
                new DbParameter("@UserXML", DbParameter.DbType.Xml, 80000, strUsersXML)
                };
        DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "UpdateNoticationsCountForUsersWithDeviceKeyXML", dbParam);
    }
}