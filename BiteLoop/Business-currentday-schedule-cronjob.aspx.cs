using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using Utility;
using Newtonsoft.Json;

public partial class Business_currentday_schedule_cronjob : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["scheduleupdate"] != null)
        {
            BusinessCurrentScheduleUpdate();
        }
    }

    private void BusinessCurrentScheduleUpdate()
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
}