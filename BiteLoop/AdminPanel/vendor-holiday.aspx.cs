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

public partial class AdminPanel_vendor_holiday : System.Web.UI.Page
{
    protected int CurrentYear = DateTime.Now.Year;
    protected string BusinessName = "";
    protected void Page_Load(object sender, EventArgs e)
    {       
        if (Request["getEvents"] != null)
        {
           BindEvents();

        }
        if (Request["saveleave"] != null)
        {
            SaveLeave();
        }
        CurrentYear = DateTime.Now.Year;
        hdnBusinessID.Value = Utility.Security.Rijndael128Algorithm.DecryptString(Convert.ToString(Request["v"]));
        BindVendorName();
    }
    private void BindVendorName()
    {
        DataTable dt = new DataTable();
        BusinessBAL objBusinessBAL = new BusinessBAL();
        objBusinessBAL.ID = Convert.ToInt64(Utility.Security.Rijndael128Algorithm.DecryptString(Convert.ToString(Request["v"])));

        dt = objBusinessBAL.BusinessDetailsByIDForContactEnquiry();
        if (dt.Rows.Count > 0)
        {
            BusinessName = Convert.ToString(dt.Rows[0]["Name"]);
        }
    }
    private void SaveLeave()
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        CommonBAL objCommonBAL = new CommonBAL();

        long result = objCommonBAL.BusinessHolidaySave(Convert.ToInt64(Request["businessid"]), Convert.ToDateTime(Request["start"]), Convert.ToString(Request["title"]));
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
            Year = Convert.ToDateTime(Request["start"]).Year;
        }
        dt = objCommonBAL.BusinessHolidayList(Year, Convert.ToInt64(Request["businessid"]));
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
                        row.Add(col.ColumnName, dr[col]);
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