using System;
using System.Web;
using System.Web.UI;
using Newtonsoft.Json;
using BAL;
using System.Data;

public partial class webservice_update_weekly_schedual : System.Web.UI.Page
{
    protected override void Render(HtmlTextWriter writer)
    {
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.ContentType = "application/json";
        Response.TrySkipIisCustomErrors = true;

        if (Request.Form["BusinessID"] == null || Request.Form["DayName"] == null)
        {
            Response.StatusCode = 400;
            Response.Write(JsonConvert.SerializeObject(new
            {
                statusCode = 400,
                success = false,
                message = "Required parameters: BusinessID and DayName."
            }));
            return;
        }

        UpdateWeeklySchedule();
    }

    private void UpdateWeeklySchedule()
    {
        try
        {
            
            BusinessBAL.WeeklyScheduleBAL ws = new BusinessBAL.WeeklyScheduleBAL();

            ws.BusinessID = Convert.ToInt64(Request.Form["BusinessID"]);
            ws.DayName = Request.Form["DayName"];
            //ws.PackSize = Request.Form["PackSize"];
            ws.Pickup_from = Request.Form["Pickup_from"];
            ws.Pickup_To = Request.Form["Pickup_To"];
            ws.WOriginalPrice1 = Convert.ToDecimal(Request.Form["WOriginalPrice1"]);
            ws.WDiscountedPrice1 = Convert.ToDecimal(Request.Form["WDiscountedPrice1"]);
            ws.WNumberOfPack1 = Convert.ToInt32(Request.Form["WNumberOfPack1"]);

            ws.WOriginalPrice2 = Convert.ToDecimal(Request.Form["WOriginalPrice2"]);
            ws.WDiscountedPrice2 = Convert.ToDecimal(Request.Form["WDiscountedPrice2"]);
            ws.WNumberOfPack2 = Convert.ToInt32(Request.Form["WNumberOfPack2"]);

            ws.WOriginalPrice3 = Convert.ToDecimal(Request.Form["WOriginalPrice3"]);
            ws.WDiscountedPrice3 = Convert.ToDecimal(Request.Form["WDiscountedPrice3"]);
            ws.WNumberOfPack3 = Convert.ToInt32(Request.Form["WNumberOfPack3"]);


            //int numOfPack;
            //if (int.TryParse(Request.Form["NumberOfPack"], out numOfPack))
            //    ws.NumberOfPack = numOfPack;


            DataTable dt = ws.UpdateWeeklySchedule();

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                var schedule = new
                {
                    ID = row["ID"],
                    BusinessID = row["BusinessID"],
                    DayNumber = row["DayNumber"],
                    DayName = row["DayName"],
                    //PackSize = row["PackSize"],
                    //NumberOfPack = row["NumberOfPack"],
                    Pickup_from = FormatTime(row["Pickup_from"]),
                    Pickup_To = FormatTime(row["Pickup_To"]),
                    CurrentDate = FormatDate(row["CurrentDate"]),


                    WOriginalPrice1 = ws.WOriginalPrice1,
                    WDiscountedPrice1 = ws.WDiscountedPrice1,
                    WNumberOfPack1 = ws.WNumberOfPack1,

                    WOriginalPrice2 = ws.WOriginalPrice2,
                    WDiscountedPrice2 = ws.WDiscountedPrice2,
                    WNumberOfPack2 = ws.WNumberOfPack2,

                    WOriginalPrice3 = ws.WOriginalPrice3,
                    WDiscountedPrice3 = ws.WDiscountedPrice3,
                    WNumberOfPack3 = ws.WNumberOfPack3


                };

                Response.StatusCode = 200;
                Response.Write(JsonConvert.SerializeObject(new
                {
                    statusCode = 200,
                    success = true,
                    message = "Weekly schedule updated successfully.",
                    WeeklySchedule = schedule
                }));
            }
            else
            {
                Response.StatusCode = 404;
                Response.Write(JsonConvert.SerializeObject(new
                {
                    statusCode = 404,
                    success = false,
                    message = "This day doesn't have any schedule."
                }));
            }
        }
        catch (Exception ex)
        {
            Response.StatusCode = 500;
            Response.Write(JsonConvert.SerializeObject(new
            {
                statusCode = 500,
                success = false,
                message = ex.Message
            }));
        }
    }
    
    private string FormatTime(object timeObj)
    {
        if (timeObj == null || timeObj == DBNull.Value)
            return "";

        string timeStr = timeObj.ToString();

        
        TimeSpan ts;
        if (TimeSpan.TryParse(timeStr, out ts))
            return DateTime.Today.Add(ts).ToString("hh:mm tt");

        
        DateTime dt;
        if (DateTime.TryParse(timeStr, out dt))
            return dt.ToString("hh:mm tt");

        
        return timeStr;
    }
    
    private string FormatDate(object dateObj)
    {
        if (dateObj == null || dateObj == DBNull.Value)
            return "";

        DateTime dt;
        if (DateTime.TryParse(dateObj.ToString(), out dt))
            return dt.ToString("yyyy-MM-dd");   

        return dateObj.ToString();
    }
}

