using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using Utility;
using Newtonsoft.Json;

public partial class webservice_business_schedule : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Keys.Count > 0)
        {
            BusinessScheduleUpdate();
        }
    }

    private void BusinessScheduleUpdate()
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
            BusinessBAL objBusinessBAL = new BusinessBAL();
            objBusinessBAL.ID = Convert.ToInt64(Request["ID"]);

            

            long result = 0;

            #region Pickup Time 1

            string MondaySchedule = string.Empty, TuesdaySchedule = string.Empty, WednesdaySchedule = string.Empty, ThirsdaySchedule = string.Empty, FridaySchedule = string.Empty, SaturdaySchedule = string.Empty, SundaySchedule = string.Empty;

            if (Convert.ToString(Request["MondayScheduleOn"]) != string.Empty)            
            {
                if (Convert.ToInt32(Request["MondayScheduleOn"]) == 1)
                {
                    MondaySchedule = Convert.ToString(Request["MondayNoOfItems"]) + "##" + Convert.ToString(Request["MondayOriginalPrice"]) + "##" + Convert.ToString(Request["MondayDiscount"]) + "##01/01/1990 " + Convert.ToString(Request["MondayFromTime"]) + "##01/01/1990 " + Convert.ToString(Request["MondayToTime"]);                    
                }
            }
            objBusinessBAL.MondaySchedule = MondaySchedule;
            if (Convert.ToString(Request["TuesdayScheduleOn"]) != string.Empty)                        
            {
                if (Convert.ToInt32(Request["TuesdayScheduleOn"]) == 1)
                {
                    TuesdaySchedule = Convert.ToString(Request["TuesdayNoOfItems"]) + "##" + Convert.ToString(Request["TuesdayOriginalPrice"]) + "##" + Convert.ToString(Request["TuesdayDiscount"]) + "##01/01/1990 " + Convert.ToString(Request["TuesdayFromTime"]) + "##01/01/1990 " + Convert.ToString(Request["TuesdayToTime"]);                    
                }
            }
            objBusinessBAL.TuesdaySchedule = TuesdaySchedule;
            if (Convert.ToString(Request["WednesdayScheduleOn"]) != string.Empty)            
            {
                if (Convert.ToInt32(Request["WednesdayScheduleOn"]) == 1)
                {
                    WednesdaySchedule = Convert.ToString(Request["WednesdayNoOfItems"]) + "##" + Convert.ToString(Request["WednesdayOriginalPrice"]) + "##" + Convert.ToString(Request["WednesdayDiscount"]) + "##01/01/1990 " + Convert.ToString(Request["WednesdayFromTime"]) + "##01/01/1990 " + Convert.ToString(Request["WednesdayToTime"]);
                }
            }
            objBusinessBAL.WednesdaySchedule = WednesdaySchedule;
            if (Convert.ToString(Request["ThirsdayScheduleOn"]) != string.Empty)                        
            {
                if (Convert.ToInt32(Request["ThirsdayScheduleOn"]) == 1)
                {
                    ThirsdaySchedule = Convert.ToString(Request["ThirsdayNoOfItems"]) + "##" + Convert.ToString(Request["ThirsdayOriginalPrice"]) + "##" + Convert.ToString(Request["ThirsdayDiscount"]) + "##01/01/1990 " + Convert.ToString(Request["ThirsdayFromTime"]) + "##01/01/1990 " + Convert.ToString(Request["ThirsdayToTime"]);
                }
            }
            objBusinessBAL.ThirsdaySchedule = ThirsdaySchedule;
            if (Convert.ToString(Request["FridayScheduleOn"]) != string.Empty)                        
            {
                if (Convert.ToInt32(Request["FridayScheduleOn"]) == 1)
                {
                    FridaySchedule = Convert.ToString(Request["FridayNoOfItems"]) + "##" + Convert.ToString(Request["FridayOriginalPrice"]) + "##" + Convert.ToString(Request["FridayDiscount"]) + "##01/01/1990 " + Convert.ToString(Request["FridayFromTime"]) + "##01/01/1990 " + Convert.ToString(Request["FridayToTime"]);
                }
            }
            objBusinessBAL.FridaySchedule = FridaySchedule;
            if (Convert.ToString(Request["SaturdayScheduleOn"]) != string.Empty)                        
            {
                if (Convert.ToInt32(Request["SaturdayScheduleOn"]) == 1)
                {
                    SaturdaySchedule = Convert.ToString(Request["SaturdayNoOfItems"]) + "##" + Convert.ToString(Request["SaturdayOriginalPrice"]) + "##" + Convert.ToString(Request["SaturdayDiscount"]) + "##01/01/1990 " + Convert.ToString(Request["SaturdayFromTime"]) + "##01/01/1990 " + Convert.ToString(Request["SaturdayToTime"]);
                }
            }
            objBusinessBAL.SaturdaySchedule = SaturdaySchedule;
            if (Convert.ToString(Request["SundayScheduleOn"]) != string.Empty)                        
            {
                if (Convert.ToInt32(Request["SundayScheduleOn"]) == 1)
                {
                    SundaySchedule = Convert.ToString(Request["SundayNoOfItems"]) + "##" + Convert.ToString(Request["SundayOriginalPrice"]) + "##" + Convert.ToString(Request["SundayDiscount"]) + "##01/01/1990 " + Convert.ToString(Request["SundayFromTime"]) + "##01/01/1990 " + Convert.ToString(Request["SundayToTime"]);
                }
            }
            objBusinessBAL.SundaySchedule = SundaySchedule;
            int PickUpTime = 1;
            if (Request["PickUpTime"] != null)
            {
                PickUpTime = Convert.ToInt32(Request["PickUpTime"]);
            }

            result = objBusinessBAL.BusinessScheduleModify(PickUpTime);

            #endregion          

            #region Backup
            //if (Request["Version"] != null)
            //{
            //    #region Pickup Time 2


            //    string Monday2Schedule = string.Empty, Tuesday2Schedule = string.Empty, Wednesday2Schedule = string.Empty, Thirsday2Schedule = string.Empty, Friday2Schedule = string.Empty, Saturday2Schedule = string.Empty, Sunday2Schedule = string.Empty;

            //    if (Convert.ToString(Request["Monday2ScheduleOn"]) != string.Empty)
            //    {
            //        if (Convert.ToInt32(Request["Monday2ScheduleOn"]) == 1)
            //        {
            //            Monday2Schedule = Convert.ToString(Request["Monday2NoOfItems"]) + "##" + Convert.ToString(Request["Monday2OriginalPrice"]) + "##" + Convert.ToString(Request["Monday2Discount"]) + "##01/01/1990 " + Convert.ToString(Request["Monday2FromTime"]) + "##01/01/1990 " + Convert.ToString(Request["Monday2ToTime"]);
            //        }
            //    }
            //    objBusinessBAL.Monday2Schedule = Monday2Schedule;
            //    if (Convert.ToString(Request["Tuesday2ScheduleOn"]) != string.Empty)
            //    {
            //        if (Convert.ToInt32(Request["Tuesday2ScheduleOn"]) == 1)
            //        {
            //            Tuesday2Schedule = Convert.ToString(Request["Tuesday2NoOfItems"]) + "##" + Convert.ToString(Request["Tuesday2OriginalPrice"]) + "##" + Convert.ToString(Request["Tuesday2Discount"]) + "##01/01/1990 " + Convert.ToString(Request["Tuesday2FromTime"]) + "##01/01/1990 " + Convert.ToString(Request["Tuesday2ToTime"]);
            //        }
            //    }
            //    objBusinessBAL.Tuesday2Schedule = Tuesday2Schedule;
            //    if (Convert.ToString(Request["Wednesday2ScheduleOn"]) != string.Empty)
            //    {
            //        if (Convert.ToInt32(Request["Wednesday2ScheduleOn"]) == 1)
            //        {
            //            Wednesday2Schedule = Convert.ToString(Request["Wednesday2NoOfItems"]) + "##" + Convert.ToString(Request["Wednesday2OriginalPrice"]) + "##" + Convert.ToString(Request["Wednesday2Discount"]) + "##01/01/1990 " + Convert.ToString(Request["Wednesday2FromTime"]) + "##01/01/1990 " + Convert.ToString(Request["Wednesday2ToTime"]);
            //        }
            //    }
            //    objBusinessBAL.Wednesday2Schedule = Wednesday2Schedule;
            //    if (Convert.ToString(Request["Thirsday2ScheduleOn"]) != string.Empty)
            //    {
            //        if (Convert.ToInt32(Request["Thirsday2ScheduleOn"]) == 1)
            //        {
            //            Thirsday2Schedule = Convert.ToString(Request["Thirsday2NoOfItems"]) + "##" + Convert.ToString(Request["Thirsday2OriginalPrice"]) + "##" + Convert.ToString(Request["Thirsday2Discount"]) + "##01/01/1990 " + Convert.ToString(Request["Thirsday2FromTime"]) + "##01/01/1990 " + Convert.ToString(Request["Thirsday2ToTime"]);
            //        }
            //    }
            //    objBusinessBAL.Thirsday2Schedule = Thirsday2Schedule;
            //    if (Convert.ToString(Request["Friday2ScheduleOn"]) != string.Empty)
            //    {
            //        if (Convert.ToInt32(Request["Friday2ScheduleOn"]) == 1)
            //        {
            //            Friday2Schedule = Convert.ToString(Request["Friday2NoOfItems"]) + "##" + Convert.ToString(Request["Friday2OriginalPrice"]) + "##" + Convert.ToString(Request["Friday2Discount"]) + "##01/01/1990 " + Convert.ToString(Request["Friday2FromTime"]) + "##01/01/1990 " + Convert.ToString(Request["Friday2ToTime"]);
            //        }
            //    }
            //    objBusinessBAL.Friday2Schedule = Friday2Schedule;
            //    if (Convert.ToString(Request["Saturday2ScheduleOn"]) != string.Empty)
            //    {
            //        if (Convert.ToInt32(Request["Saturday2ScheduleOn"]) == 1)
            //        {
            //            Saturday2Schedule = Convert.ToString(Request["Saturday2NoOfItems"]) + "##" + Convert.ToString(Request["Saturday2OriginalPrice"]) + "##" + Convert.ToString(Request["Saturday2Discount"]) + "##01/01/1990 " + Convert.ToString(Request["Saturday2FromTime"]) + "##01/01/1990 " + Convert.ToString(Request["Saturday2ToTime"]);
            //        }
            //    }
            //    objBusinessBAL.Saturday2Schedule = Saturday2Schedule;
            //    if (Convert.ToString(Request["Sunday2ScheduleOn"]) != string.Empty)
            //    {
            //        if (Convert.ToInt32(Request["Sunday2ScheduleOn"]) == 1)
            //        {
            //            Sunday2Schedule = Convert.ToString(Request["Sunday2NoOfItems"]) + "##" + Convert.ToString(Request["Sunday2OriginalPrice"]) + "##" + Convert.ToString(Request["Sunday2Discount"]) + "##01/01/1990 " + Convert.ToString(Request["Sunday2FromTime"]) + "##01/01/1990 " + Convert.ToString(Request["Sunday2ToTime"]);
            //        }
            //    }
            //    objBusinessBAL.Sunday2Schedule = Sunday2Schedule;
            //    result = objBusinessBAL.BusinessScheduleModify(2);

            //    #endregion

            //    #region Pickup Time 3

            //    string Monday3Schedule = string.Empty, Tuesday3Schedule = string.Empty, Wednesday3Schedule = string.Empty, Thirsday3Schedule = string.Empty, Friday3Schedule = string.Empty, Saturday3Schedule = string.Empty, Sunday3Schedule = string.Empty;

            //    if (Convert.ToString(Request["Monday3ScheduleOn"]) != string.Empty)
            //    {
            //        if (Convert.ToInt32(Request["Monday3ScheduleOn"]) == 1)
            //        {
            //            Monday3Schedule = Convert.ToString(Request["Monday3NoOfItems"]) + "##" + Convert.ToString(Request["Monday3OriginalPrice"]) + "##" + Convert.ToString(Request["Monday3Discount"]) + "##01/01/1990 " + Convert.ToString(Request["Monday3FromTime"]) + "##01/01/1990 " + Convert.ToString(Request["Monday3ToTime"]);
            //        }
            //    }
            //    objBusinessBAL.Monday3Schedule = Monday3Schedule;
            //    if (Convert.ToString(Request["Tuesday3ScheduleOn"]) != string.Empty)
            //    {
            //        if (Convert.ToInt32(Request["Tuesday3ScheduleOn"]) == 1)
            //        {
            //            Tuesday3Schedule = Convert.ToString(Request["Tuesday3NoOfItems"]) + "##" + Convert.ToString(Request["Tuesday3OriginalPrice"]) + "##" + Convert.ToString(Request["Tuesday3Discount"]) + "##01/01/1990 " + Convert.ToString(Request["Tuesday3FromTime"]) + "##01/01/1990 " + Convert.ToString(Request["Tuesday3ToTime"]);
            //        }
            //    }
            //    objBusinessBAL.Tuesday3Schedule = Tuesday3Schedule;
            //    if (Convert.ToString(Request["Wednesday3ScheduleOn"]) != string.Empty)
            //    {
            //        if (Convert.ToInt32(Request["Wednesday3ScheduleOn"]) == 1)
            //        {
            //            Wednesday3Schedule = Convert.ToString(Request["Wednesday3NoOfItems"]) + "##" + Convert.ToString(Request["Wednesday3OriginalPrice"]) + "##" + Convert.ToString(Request["Wednesday3Discount"]) + "##01/01/1990 " + Convert.ToString(Request["Wednesday3FromTime"]) + "##01/01/1990 " + Convert.ToString(Request["Wednesday3ToTime"]);
            //        }
            //    }
            //    objBusinessBAL.Wednesday3Schedule = Wednesday3Schedule;
            //    if (Convert.ToString(Request["Thirsday3ScheduleOn"]) != string.Empty)
            //    {
            //        if (Convert.ToInt32(Request["Thirsday3ScheduleOn"]) == 1)
            //        {
            //            Thirsday3Schedule = Convert.ToString(Request["Thirsday3NoOfItems"]) + "##" + Convert.ToString(Request["Thirsday3OriginalPrice"]) + "##" + Convert.ToString(Request["Thirsday3Discount"]) + "##01/01/1990 " + Convert.ToString(Request["Thirsday3FromTime"]) + "##01/01/1990 " + Convert.ToString(Request["Thirsday3ToTime"]);
            //        }
            //    }
            //    objBusinessBAL.Thirsday3Schedule = Thirsday3Schedule;
            //    if (Convert.ToString(Request["Friday3ScheduleOn"]) != string.Empty)
            //    {
            //        if (Convert.ToInt32(Request["Friday3ScheduleOn"]) == 1)
            //        {
            //            Friday3Schedule = Convert.ToString(Request["Friday3NoOfItems"]) + "##" + Convert.ToString(Request["Friday3OriginalPrice"]) + "##" + Convert.ToString(Request["Friday3Discount"]) + "##01/01/1990 " + Convert.ToString(Request["Friday3FromTime"]) + "##01/01/1990 " + Convert.ToString(Request["Friday3ToTime"]);
            //        }
            //    }
            //    objBusinessBAL.Friday3Schedule = Friday3Schedule;
            //    if (Convert.ToString(Request["Saturday3ScheduleOn"]) != string.Empty)
            //    {
            //        if (Convert.ToInt32(Request["Saturday3ScheduleOn"]) == 1)
            //        {
            //            Saturday3Schedule = Convert.ToString(Request["Saturday3NoOfItems"]) + "##" + Convert.ToString(Request["Saturday3OriginalPrice"]) + "##" + Convert.ToString(Request["Saturday3Discount"]) + "##01/01/1990 " + Convert.ToString(Request["Saturday3FromTime"]) + "##01/01/1990 " + Convert.ToString(Request["Saturday3ToTime"]);
            //        }
            //    }
            //    objBusinessBAL.Saturday3Schedule = Saturday3Schedule;
            //    if (Convert.ToString(Request["Sunday3ScheduleOn"]) != string.Empty)
            //    {
            //        if (Convert.ToInt32(Request["Sunday3ScheduleOn"]) == 1)
            //        {
            //            Sunday3Schedule = Convert.ToString(Request["Sunday3NoOfItems"]) + "##" + Convert.ToString(Request["Sunday3OriginalPrice"]) + "##" + Convert.ToString(Request["Sunday3Discount"]) + "##01/01/1990 " + Convert.ToString(Request["Sunday3FromTime"]) + "##01/01/1990 " + Convert.ToString(Request["Sunday3ToTime"]);
            //        }
            //    }
            //    objBusinessBAL.Sunday3Schedule = Sunday3Schedule;
            //    result = objBusinessBAL.BusinessScheduleModify(3);

            //    #endregion
            //}
            #endregion


            switch (result)
            {              
                default:             
                      objResponse.success = "true";
                      objResponse.message = "Schedule updated successfully.";
                    break;
            }

            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
          
        }
        Response.End();
    }
}