using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using BiteLoop.Common;
public partial class AdminPanel_ContactusSubject_Modify : AdminAuthentication
{
    ContactUsSubjectBAL objContactUsSubjectBAL = new ContactUsSubjectBAL();    

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
        objContactUsSubjectBAL.ID = ID;

        if (Request.Form.Keys.Count > 0)
        { SaveInfo(); }
        else
        {
            if (objContactUsSubjectBAL.ID != 0)
            { BindControls(); }
        }

    }
    #endregion

    #region Bind Controls
    private void BindControls()
    {
        if (ID > 0)
        {
            DataTable dt = new DataTable();
            objContactUsSubjectBAL.ID = ID;
            dt = objContactUsSubjectBAL.GetContactUsSubjectByID();
            if (dt.Rows.Count > 0)
            {
                tbxTitle.Value = Convert.ToString(dt.Rows[0]["Name"]);                
            }
        }
    }
    #endregion

    #region Save Information
    private void SaveInfo()
    {

        objContactUsSubjectBAL.ID = ID;
        objContactUsSubjectBAL.Name = Convert.ToString(Request[tbxTitle.UniqueID]).Trim();

        int intResult = objContactUsSubjectBAL.Save();

        switch (intResult)
        {
            case -1:

                Response.Write(Common.ShowMessage("Subject already exists. Please try another Subject.", "alert-message error", divMsg.ClientID));
                Response.Write(Common.ScriptStartTag + "parent.ScrollTop();" + Common.ScriptEndTag);
                break;
            default:
                Response.Write(Common.ShowMessage("Subject information has been saved successfully.", "alert-message success", divMsg.ClientID));
                Response.Write(Common.ScriptStartTag + "parent.ScrollTop();" + Common.ScriptEndTag);
                Response.Write(Common.ScriptStartTag + "parent.window.setTimeout(\"parent.window.location.href = 'contactus-subject-list.aspx'\",1000);" + Common.ScriptEndTag);
                break;
        }
        Response.End();

    }
    #endregion
    
}