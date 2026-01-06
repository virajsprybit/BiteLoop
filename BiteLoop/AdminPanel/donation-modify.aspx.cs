using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using Utility;

public partial class AdminPanel_Donation_Modify : System.Web.UI.Page
{    
    DonationsBAL objDonationsBAL = new DonationsBAL();

    #region Private Members
    private int _ID = 0;
   
    #endregion

    #region Public Members
    public new int ID
    {
        get
        {
            return _ID;
        }
    }
    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {   
        Int32.TryParse(Request["id"], out _ID);
        objDonationsBAL.ID = ID;

        if (Request.Form.Keys.Count > 0)
        {
            SaveInfo();
        }
        else
        {
            if (objDonationsBAL.ID != 0)
            {
                BindControls();
            }
        }

    }
    #endregion

    #region Bind Controls
    private void BindControls()
    {
        DataTable dtblNews = new DataTable();
        objDonationsBAL.ID = ID;
        dtblNews = objDonationsBAL.GetDonationsByID();
        if (dtblNews.Rows.Count > 0)
        {
            tbxTitle.Value = Convert.ToString(dtblNews.Rows[0]["Donation"]);          
        } 
    }

    #endregion

    #region Save Information
    private void SaveInfo()
    {
        objDonationsBAL.ID = ID;
        objDonationsBAL.Donation = Convert.ToDecimal( Request[tbxTitle.UniqueID]);
        
        int intResult = objDonationsBAL.Save();
        switch (intResult)
        {
            case 0:                
                Response.Write(Common.ShowMessage("Duplicate Donation Amount found.", "alert-message error", divMsg.ClientID));
                Response.Write(Common.ScriptStartTag + "parent.ScrollTop();" + Common.ScriptEndTag);
                break;
            default:
                Response.Write(Common.ShowMessage("Donation details has been saved successfully.", "alert-message success", divMsg.ClientID));
                Response.Write(Common.ScriptStartTag + "parent.ScrollTop();" + Common.ScriptEndTag);
                Response.Write(Common.ScriptStartTag + "parent.window.setTimeout(\"parent.window.location.href = 'donations-list.aspx'\",1000);" + Common.ScriptEndTag);
                break;
        }
        Response.End();
    }

    #endregion

     
}