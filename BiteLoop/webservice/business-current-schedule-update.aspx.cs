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
using DAL;
public partial class webservice_business_current_Schedule_save : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Keys.Count > 0)
        {
            BusinessCurrentScheduleUpdate();
        }
    }

    private void BusinessCurrentScheduleUpdate()
    {
        Response objResponse = new Response();
        bool IsValidated = false;
        if (Request["UserID"] != null && Request["SecretKey"] != null && Request["AuthToken"] != null)
        {
            if (ValidateRequestBAL.BusinessValidateClientRequest(Convert.ToInt64(Request["UserID"]), Convert.ToString(Request["SecretKey"]), Convert.ToString(Request["AuthToken"])))
            {
                IsValidated = true;
            }
        }
        if (!IsValidated)
        {
            CommonAPI objCommonAPI = new CommonAPI();
            objCommonAPI.Unauthorized();
        }
        else
        {
            int Version = 0;           

            BusinessBAL objBusinessBAL = new BusinessBAL();
            objBusinessBAL.ID = Convert.ToInt64(Request["BusinessID"]);
            if (Request["Version"] != null)
            {
                Version = Convert.ToInt32(Request["Version"]);
            }
            long result = 0;
            #region Pickup Tme 1

            if (Request["NoOfItems"] != null)
            {
                if (Convert.ToInt32(Request["NoOfItems"]) != -1)
                {

                    objBusinessBAL.NoOfItems = Convert.ToInt32(Request["NoOfItems"]);
                    objBusinessBAL.OriginalPrice = Convert.ToDecimal(Request["OriginalPrice"]);
                    objBusinessBAL.DiscountID = Convert.ToDecimal(Request["DiscountID"]);
                    objBusinessBAL.PickupFromTime = Convert.ToDateTime("01/01/1990 " + Convert.ToString(Request["PickupFromTime"]));
                    objBusinessBAL.PickupToTime = Convert.ToDateTime("01/01/1990 " + Convert.ToString(Request["PickupToTime"]));
                    result = objBusinessBAL.BusinessCurrentDayScheduleUpdate(Version, 1);
                }
            }

            #endregion

            #region Pickup Tme 2

            if (Request["NoOfItems2"] != null)
            {
                if (Convert.ToInt32(Request["NoOfItems2"]) != -1)
                {

                    objBusinessBAL.NoOfItems = Convert.ToInt32(Request["NoOfItems2"]);
                    objBusinessBAL.OriginalPrice = Convert.ToDecimal(Request["OriginalPrice"]);
                    objBusinessBAL.DiscountID = Convert.ToDecimal(Request["DiscountID2"]);
                    objBusinessBAL.PickupFromTime = Convert.ToDateTime("01/01/1990 " + Convert.ToString(Request["PickupFromTime2"]));
                    objBusinessBAL.PickupToTime = Convert.ToDateTime("01/01/1990 " + Convert.ToString(Request["PickupToTime2"]));
                    result = objBusinessBAL.BusinessCurrentDayScheduleUpdate(Version, 2);
                }

            }
            #endregion

            #region Pickup Tme 3

            if (Request["NoOfItems3"] != null)
            {
                if (Convert.ToInt32(Request["NoOfItems3"]) != -1)
                {
                    objBusinessBAL.NoOfItems = Convert.ToInt32(Request["NoOfItems3"]);
                    objBusinessBAL.OriginalPrice = Convert.ToDecimal(Request["OriginalPrice"]);
                    objBusinessBAL.DiscountID = Convert.ToDecimal(Request["DiscountID3"]);
                    objBusinessBAL.PickupFromTime = Convert.ToDateTime("01/01/1990 " + Convert.ToString(Request["PickupFromTime3"]));
                    objBusinessBAL.PickupToTime = Convert.ToDateTime("01/01/1990 " + Convert.ToString(Request["PickupToTime3"]));
                    result = objBusinessBAL.BusinessCurrentDayScheduleUpdate(Version, 3);
                }
            }
            #endregion



            switch (result)
            {
                default:
                    objResponse.success = "true";
                    objResponse.message = "Today's Schedule has been updated successfully.";
                    SendNitifications(objBusinessBAL.ID);
                    break;
            }

            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));

        }
        Response.End();
    }

    private void SendNitifications(long BID)
    {
        SendNotification objSendNotification = new SendNotification();
        DataSet ds = new DataSet();
        DataTable dtUsers = new DataTable();
        string strMessage = "";
        string strBusinesName = "";
        ds = GetBusinessFavouriteUsers(BID);
        strBusinesName = Convert.ToString(ds.Tables[1].Rows[0]["BusinessName"]);        
        strMessage = strBusinesName + " just listed more food on the app. Rescue 'em, before they run out!";
        dtUsers = ds.Tables[0];

        if (dtUsers.Rows.Count > 0)
        {
            for (int i = 0; i < dtUsers.Rows.Count; i++)
            {
                if (Convert.ToString(dtUsers.Rows[i]["DeviceKey"]) != string.Empty && Convert.ToString(dtUsers.Rows[i]["DeviceType"]) != string.Empty)
                {
                    objSendNotification.CallNotification(Convert.ToString(dtUsers.Rows[i]["DeviceKey"]), strMessage, "Bring Me Home", Convert.ToString(BID), "FavBusinessDetails", Convert.ToString(dtUsers.Rows[i]["DeviceType"]), "U", 1);                    
                }
            }                   
        }
    }

    public DataSet GetBusinessFavouriteUsers(long BID)
    {
        DbParameter[] dbParam = new DbParameter[] 
        {
            new DbParameter("@BusinessID", DbParameter.DbType.Int, 200, BID) 
       };
        
        return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "BusinessFavouriteUsers", dbParam);
    }
}