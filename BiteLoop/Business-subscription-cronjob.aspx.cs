using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using Utility;
using Newtonsoft.Json;
using DAL;
using System.Data;

public partial class Business_currentday_schedule_cronjob : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["sub"] != null)
        {
            BusinessSubscriptionUpdate();
        }
    }

    private void BusinessSubscriptionUpdate()
    {
        try
        {
            BusinessBAL objBusinessBAL = new BusinessBAL();
            objBusinessBAL.BusinessCurrentDayScheduleUpdateCronJob();
            lbkMessage.Text = "Schedule Updated Successfully";

        }
        catch (Exception ex)
        {
            lbkMessage.Text = ex.Message.ToString();

        }
    }
    public void BusinessCurrentDayScheduleUpdateCronJob()
    {
        DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "BusinessSubscriptionUpdate");

    }
}