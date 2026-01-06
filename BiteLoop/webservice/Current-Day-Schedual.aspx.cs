using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using Newtonsoft.Json;
using BAL;
using Utility;

public partial class webservice_Current_Day_Schedual : System.Web.UI.Page
{
    protected override void Render(HtmlTextWriter writer)
    {
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Keys.Count > 0)
        {
            InsertSchedule();
        }
        else
        {
            Response.Write("{\"success\":\"0\",\"message\":\"No parameters received.\"}");
            Response.End();
        }
    }

    private void InsertSchedule()
    {
        try
        {
            string pickupFromRaw = Request.Form["Pickup_from"];
            string pickupToRaw = Request.Form["Pickup_To"];
            string selectedDaysRaw = Request.Form["SelectedDays"];
            int repeted = Convert.ToInt32(Request.Form["Repeted"]);

            DateTime pickupFromTime;
            DateTime pickupToTime;

            if (!DateTime.TryParse(pickupFromRaw, out pickupFromTime))
                pickupFromTime = DateTime.Now;

            if (!DateTime.TryParse(pickupToRaw, out pickupToTime))
                pickupToTime = DateTime.Now.AddHours(1);

            string formattedFrom = pickupFromTime.ToString("hh:mm tt");
            string formattedTo = pickupToTime.ToString("hh:mm tt");

            BAL.BusinessBAL.CurrentDayScheduleBAL obj = new BAL.BusinessBAL.CurrentDayScheduleBAL
            {
                BusinessID = Convert.ToInt64(Request.Form["BusinessID"]),
                CurrentDate = Request.Form["CurrentDate"] ?? DateTime.Now.ToString("yyyy-MM-dd"),
                //PackSize = Request.Form["PackSize"],
                //NumberOfPack = Convert.ToInt32(Request.Form["NumberOfPack"]),
                Pickup_from = formattedFrom,
                Repeted = repeted,
                Pickup_To = formattedTo,
                OriginalPrice1 = Convert.ToDecimal(Request.Form["OriginalPrice1"]),
                DiscountedPrice1 = Convert.ToDecimal(Request.Form["DiscountedPrice1"]),
                NumberOfPack1 = Convert.ToInt32(Request.Form["NumberOfPack1"]),

                OriginalPrice2 = Convert.ToDecimal(Request.Form["OriginalPrice2"]),
                DiscountedPrice2 = Convert.ToDecimal(Request.Form["DiscountedPrice2"]),
                NumberOfPack2 = Convert.ToInt32(Request.Form["NumberOfPack2"]),

                OriginalPrice3 = Convert.ToDecimal(Request.Form["OriginalPrice3"]),
                DiscountedPrice3 = Convert.ToDecimal(Request.Form["DiscountedPrice3"]),
                NumberOfPack3 = Convert.ToInt32(Request.Form["NumberOfPack3"]),

            };

            if (obj.NumberOfPack1 == 0 && obj.NumberOfPack2 == 0 && obj.NumberOfPack3 == 0)
            {
                var responseError = new
                {
                    success = "false",
                    message = "At least one NumberOfPack must be greater than zero.",
                    StatusCode = "400",
                    TotalRecords = 0
                };

                Response.ContentType = "application/json";
                Response.Write(JsonConvert.SerializeObject(responseError, Formatting.Indented));
                Response.Flush();
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                return;   
            }

            string selectedDaysJson = selectedDaysRaw ?? "[]";
            int insertedId = obj.InsertCurrentDaySchedule(selectedDaysJson);

            List<string> selectedDays = new List<string>();

            if (repeted == 1)
            {
                if (!string.IsNullOrEmpty(selectedDaysRaw))
                {
                    selectedDays = JsonConvert.DeserializeObject<List<string>>(selectedDaysRaw);
                }
            }

            object response;
            if (insertedId > 0)
            {
                response = new
                {
                    success = "true",
                    message = "Inserted successfully",
                    StatusCode = "200",
                    TotalRecords = 1,
                    Data = new
                    {
                        obj.BusinessID,
                        obj.CurrentDate,
                        //obj.PackSize,
                        //obj.NumberOfPack,
                        obj.Pickup_from,
                        obj.Pickup_To,
                        obj.Repeted,
                        obj.OriginalPrice1,
                        obj.DiscountedPrice1,
                        obj.NumberOfPack1,
                        obj.OriginalPrice2,
                        obj.DiscountedPrice2,
                        obj.NumberOfPack2,
                        obj.OriginalPrice3,
                        obj.DiscountedPrice3,
                        obj.NumberOfPack3,
                        SelectedDays = repeted == 1 ? selectedDays : new List<string>(),

                    }
                };
            }
            else
            {
                response = new
                {
                    success = "false",
                    message = "Insertion failed",
                    StatusCode = "400",
                    TotalRecords = 0,
                    Data = (object)null
                };
            }

            Response.ContentType = "application/json";
            Response.Write(JsonConvert.SerializeObject(response, Formatting.Indented));
            Response.Flush();
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        catch (Exception ex)
        {
            var response = new
            {
                success = "false",
                message = ex.Message,
                StatusCode = "500",
                TotalRecords = 0,
                Data = (object)null
            };

            Response.ContentType = "application/json";
            Response.Write(JsonConvert.SerializeObject(response, Formatting.Indented));
            Response.Flush();
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }
}
