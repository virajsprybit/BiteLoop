using System;
using System.Data;
using System.Web;
using System.Web.UI;
using Newtonsoft.Json;
using DAL;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

public partial class webservice_List_Day_Schedual : System.Web.UI.Page
{
    
    protected override void Render(HtmlTextWriter writer)
    {
        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();
        Response.ContentType = "application/json";
        Response.TrySkipIisCustomErrors = true;

        if (Request.Form["BusinessID"] != null)
        {
            GetScheduleMergedWithID();
        }
        else
        {
            WriteJsonAndStop(new
            {
                statusCode = 400,
                success = false,
                message = "BusinessID is required"
            }, 400);
        }
    }

    private void GetScheduleMergedWithID()
    {
        try
        {
            long businessId = Convert.ToInt64(Request.Form["BusinessID"]);

            DbParameter[] parameters =
            {
                new DbParameter("@BusinessID", DbParameter.DbType.Int, 0, businessId)
            };

            DataSet ds = DbConnectionDAL.GetDataSet(
                CommandType.StoredProcedure,
                "SP_GetBusinessScheduleMergedWithID",
                parameters
            );

            if (ds != null && ds.Tables.Count >= 1 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow currentDay = ds.Tables[0].Rows[0];
                List<string> selectedDays = new List<string>();

                if (ds.Tables.Count > 2)
                {
                    foreach (DataRow row in ds.Tables[2].Rows)
                    {
                        selectedDays.Add(row["DayName"].ToString().ToUpper());
                    }
                }

                //string packSize = FormatNumber(currentDay["PackSize"]);
                //string numberOfPack = FormatNumber(currentDay["NumberOfPack"]);
                string pickupFrom = FormatTime(currentDay["Pickup_from"]);
                string pickupTo = FormatTime(currentDay["Pickup_To"]);
                string createdDate = FormatDate(currentDay["CreatedDate"]);

                // Fetch Current Day Prices (Table 3)
                decimal op1 = 0, dp1 = 0, op2 = 0, dp2 = 0, op3 = 0, dp3 = 0;
                int np1 = 0, np2 = 0, np3 = 0;

                if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    var cdPrices = ds.Tables[1].AsEnumerable().ToList();

                    if (cdPrices.Count > 0)
                    {
                        op1 = Convert.ToDecimal(cdPrices[0]["OriginalPrice"]);
                        dp1 = Convert.ToDecimal(cdPrices[0]["DiscountedPrice"]);
                        np1 = Convert.ToInt32(cdPrices[0]["NumberOfPack"]);
                    }
                    if (cdPrices.Count > 1)
                    {
                        op2 = Convert.ToDecimal(cdPrices[1]["OriginalPrice"]);
                        dp2 = Convert.ToDecimal(cdPrices[1]["DiscountedPrice"]);
                        np2 = Convert.ToInt32(cdPrices[1]["NumberOfPack"]);
                    }
                    if (cdPrices.Count > 2)
                    {
                        op3 = Convert.ToDecimal(cdPrices[2]["OriginalPrice"]);
                        dp3 = Convert.ToDecimal(cdPrices[2]["DiscountedPrice"]);
                        np3 = Convert.ToInt32(cdPrices[2]["NumberOfPack"]);
                    }
                }


                string currentDate = (/*string.IsNullOrEmpty(packSize*/
                                      //&& string.IsNullOrEmpty(numberOfPack)
                                      string.IsNullOrEmpty(pickupFrom)
                                      && string.IsNullOrEmpty(pickupTo))
                                      ? "" : FormatDate(currentDay["CurrentDate"]);

                var scheduleObj = new Dictionary<string, object>();
                scheduleObj["ID"] = currentDay["ID"];
                scheduleObj["BusinessID"] = currentDay["BusinessID"];
                scheduleObj["CurrentDate"] = currentDate;
                //scheduleObj["PackSize"] = packSize;
                //scheduleObj["NumberOfPack"] = numberOfPack;
                scheduleObj["Pickup_from"] = pickupFrom;
                scheduleObj["Pickup_To"] = pickupTo;
                scheduleObj["Repeted"] = currentDay["Repeted"];
                scheduleObj["CreatedDate"] = createdDate;
                scheduleObj["OriginalPrice1"] = op1;
                scheduleObj["DiscountedPrice1"] = dp1;
                scheduleObj["NumberOfPack1"] = np1;

                scheduleObj["OriginalPrice2"] = op2;
                scheduleObj["DiscountedPrice2"] = dp2;
                scheduleObj["NumberOfPack2"] = np2;

                scheduleObj["OriginalPrice3"] = op3;
                scheduleObj["DiscountedPrice3"] = dp3;
                scheduleObj["NumberOfPack3"] = np3;
                scheduleObj["selectedDay"] = selectedDays;


                List<object> weeklyScheduleList = new List<object>();

                if (ds.Tables.Count > 1)
                {
                    DataTable dtWeekly = ds.Tables[2];
                    DataTable dtWeeklyPrices = ds.Tables.Count > 3 ? ds.Tables[3] : new DataTable();


                    foreach (DataRow row in dtWeekly.Rows)
                    {
                        int scheduleId = Convert.ToInt32(row["ID"]);

                        var prices = dtWeeklyPrices.AsEnumerable()
                            .Where(p => Convert.ToInt32(p["WeeklyScheduleID"]) == scheduleId)
                            .ToList();

                        decimal w_op1 = 0, w_dp1 = 0, w_op2 = 0, w_dp2 = 0, w_op3 = 0, w_dp3 = 0;
                        int w_np1 = 0, w_np2 = 0, w_np3 = 0;

                        if (prices.Count > 0)
                        {
                            w_op1 = Convert.ToDecimal(prices[0]["OriginalPrice"]);
                            w_dp1 = Convert.ToDecimal(prices[0]["DiscountedPrice"]);
                            w_np1 = Convert.ToInt32(prices[0]["NumberOfPack"]);
                        }

                        if (prices.Count > 1)
                        {
                            w_op2 = Convert.ToDecimal(prices[1]["OriginalPrice"]);
                            w_dp2 = Convert.ToDecimal(prices[1]["DiscountedPrice"]);
                            w_np2 = Convert.ToInt32(prices[1]["NumberOfPack"]);
                        }

                        if (prices.Count > 2)
                        {
                            w_op3 = Convert.ToDecimal(prices[2]["OriginalPrice"]);
                            w_dp3 = Convert.ToDecimal(prices[2]["DiscountedPrice"]);
                            w_np3 = Convert.ToInt32(prices[2]["NumberOfPack"]);
                        }

                        weeklyScheduleList.Add(new
                        {
                            ID = scheduleId,
                            BusinessID = row["BusinessID"],
                            DayNumber = row["DayNumber"],
                            DayName = row["DayName"],
                            Pickup_from = FormatTime(row["Pickup_from"]),
                            Pickup_To = FormatTime(row["Pickup_To"]),
                            CurrentDate = FormatDate(row["CurrentDate"]),

                            WOriginalPrice1 = w_op1,
                            WDiscountedPrice1 = w_dp1,
                            WNumberOfPack1 = w_np1,

                            WOriginalPrice2 = w_op2,
                            WDiscountedPrice2 = w_dp2,
                            WNumberOfPack2 = w_np2,

                            WOriginalPrice3 = w_op3,
                            WDiscountedPrice3 = w_dp3,
                            WNumberOfPack3 = w_np3
                        });
                    }
                }

                scheduleObj["WeeklySchedule"] = weeklyScheduleList;


                //List<object> weeklyScheduleList = new List<object>();
                //if (ds.Tables.Count > 1)
                //{
                //    foreach (DataRow row in ds.Tables[1].Rows)
                //    {
                //        string wPickupFrom = FormatTime(row["Pickup_from"]);
                //        string wPickupTo = FormatTime(row["Pickup_To"]);
                //        string wCurrentDate = FormatDate(row["CurrentDate"]);

                //        weeklyScheduleList.Add(new
                //        {
                //            ID = row["ID"],
                //            BusinessID = currentDay["BusinessID"],
                //            DayNumber = row["DayNumber"],
                //            DayName = row["DayName"],
                //            //PackSize = FormatNumber(row["PackSize"]),
                //            //NumberOfPack = FormatNumber(row["NumberOfPack"]),
                //            Pickup_from = wPickupFrom,
                //            Pickup_To = wPickupTo,
                //            CurrentDate = wCurrentDate
                //        });
                //    }
                //}

                //scheduleObj["WeeklySchedule"] = weeklyScheduleList;

                WriteJsonAndStop(new
                {
                    statusCode = 200,
                    success = true,
                    ScheduleList = scheduleObj
                }, 200);
            }
            else
            {
                WriteJsonAndStop(new
                {
                    statusCode = 404,
                    success = false,
                    message = "No schedule data found for the provided BusinessID."
                }, 404);
            }
        }
        catch (Exception ex)
        {
            WriteJsonAndStop(new
            {
                statusCode = 500,
                success = false,
                message = ex.Message
            }, 500);
        }
    }

    private void WriteJsonAndStop(object obj, int statusCode)
    {
        Response.Clear();
        Response.StatusCode = statusCode;
        Response.ContentType = "application/json";

        Response.Write(JsonConvert.SerializeObject(obj, Formatting.Indented));

        HttpContext.Current.ApplicationInstance.CompleteRequest();
    }

    private string FormatTime(object dbTime)
    {
        if (dbTime == DBNull.Value || dbTime == null || string.IsNullOrWhiteSpace(dbTime.ToString()))
            return "";

        string timeStr = dbTime.ToString();
        if (timeStr == "08:00" || timeStr == "08:00:00" || timeStr == "17:00" || timeStr == "17:00:00")
            return "";

        TimeSpan ts;
        if (TimeSpan.TryParse(timeStr, out ts))
        {
            DateTime dt = DateTime.Today.Add(ts);
            return dt.ToString("hh:mm tt");
        }

        DateTime dt2;
        if (DateTime.TryParse(timeStr, out dt2))
            return dt2.ToString("hh:mm tt");

        return "";
    }

    private string FormatDate(object dbDate)
    {
        if (dbDate == DBNull.Value || dbDate == null || string.IsNullOrWhiteSpace(dbDate.ToString()))
            return "";

        DateTime dt;
        if (DateTime.TryParse(dbDate.ToString(), out dt))
        {
            if (dt == DateTime.MinValue || dt == new DateTime(1900, 1, 1))
                return "";

            return dt.ToString("yyyy-MM-dd");
        }
        return "";
    }

    private string FormatNumber(object dbValue)
    {
        if (dbValue == DBNull.Value || dbValue == null)
            return "";

        if (dbValue.ToString() == "0")
            return "";

        return dbValue.ToString();
    }
}