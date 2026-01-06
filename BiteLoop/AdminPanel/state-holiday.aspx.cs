using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using System.Threading;
using System.Globalization;
using BiteLoop.Common;

public partial class AdminPanel_State_holiday : System.Web.UI.Page
{
    protected int CurrentYear = DateTime.Now.Year;
    
    protected void Page_Load(object sender, EventArgs e)
    {       
        if (Request["getEvents"] != null)
        {
           BindEvents();
        }
        if (Request["savestateholiday"] != null)
        {
            SaveStateHoliday();
        }
        CurrentYear = DateTime.Now.Year;        
        BindState();
    }
    private void BindState()
    {
        CommonBAL objCommonBAL = new CommonBAL();
        DataTable dt = new DataTable();
        dt = objCommonBAL.StateList();
        if (dt.Rows.Count > 0)
        {
            ddlState.DataSource = dt;
            ddlState.DataTextField = "StateName";
            ddlState.DataValueField= "ID";
            ddlState.DataBind();
        }      

    }
    private void SaveStateHoliday()
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        CommonBAL objCommonBAL = new CommonBAL();

        long result = objCommonBAL.StateHolidaySave(Convert.ToInt32(Request["stateid"]), Convert.ToDateTime(Request["start"]), Convert.ToString(Request["title"]));
        if (result > 0)
        {
            Response.Write(result);
        }
        Response.End();

    }

    private void BindEvents()
    {
        DataTable dt = new DataTable();
        CommonBAL objCommonBAL = new CommonBAL();
        int Year = DateTime.Now.Year;
        if (Request["start"] != null)
        {
            Year = Convert.ToDateTime(Request["start"]).AddMonths(3).Year;
        }
        dt = objCommonBAL.StateHolidayList(Year, Convert.ToInt32(Request["stateid"]));
       
        
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;

        try
        {
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        if (col.ColumnName.ToLower() == "start" || col.ColumnName.ToLower() == "holidaydate" || col.ColumnName.ToLower() == "end")
                        {
                            row.Add(col.ColumnName, Convert.ToDateTime(dr[col]).ToString("dd/MMM/yyyy"));
                        }
                        else
                        {
                            row.Add(col.ColumnName, dr[col]);
                        }
                    }
                    rows.Add(row);
                }
            }

        }
        catch (Exception Ex)
        {

        }
        Response.Write(serializer.Serialize(rows));
        Response.End();
    }

}