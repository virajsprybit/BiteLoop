using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using Utility;

public partial class AdminPanel_Faq_Modify : System.Web.UI.Page
{
    FAQBAL objFAQBAL = new FAQBAL();

    #region Private Members
    private int _ID = 0;
    string strImageName = string.Empty;
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
        objFAQBAL.ID = ID;

        if (Request.Form.Keys.Count > 0)
        {
            SaveInfo();
        }
        else
        {
            if (objFAQBAL.ID != 0)
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
        objFAQBAL.ID = ID;
        dtblNews = objFAQBAL.GetFAQListByID();
        if (dtblNews.Rows.Count > 0)
        {
            tbxTitle.Value = Convert.ToString(dtblNews.Rows[0]["Question"]);
            //tbxAnswer.Text = Convert.ToString(dtblNews.Rows[0]["Answer"]);
            System.Text.StringBuilder sbTemplate = new System.Text.StringBuilder(Convert.ToString(dtblNews.Rows[0]["Answer"]));
            sbTemplate.Replace("{%WebSiteUrl%}", Config.WebSiteUrl);
            tbxAnswer.Text = sbTemplate.ToString();
        } 
    }

    #endregion

    #region Save Information
    private void SaveInfo()
    { 
        objFAQBAL.ID = ID;
        objFAQBAL.Question = Request[tbxTitle.UniqueID];
        System.Text.StringBuilder sbTemplate = new System.Text.StringBuilder(Server.HtmlDecode(Request[hdnContent.UniqueID]));
        sbTemplate.Replace(Config.WebSiteUrl, "{%WebSiteUrl%}");

        objFAQBAL.Answer = sbTemplate.ToString();   

        int intResult = objFAQBAL.FAQModify();
        switch (intResult)
        {
            case 0:                
                Response.Write(Common.ShowMessage("Duplicate Question found.", "alert-message error", divMsg.ClientID));
                Response.Write(Common.ScriptStartTag + "parent.ScrollTop();" + Common.ScriptEndTag);
                break;
            default:                
                Response.Write(Common.ShowMessage("FAQ has been saved successfully.", "alert-message success", divMsg.ClientID));
                Response.Write(Common.ScriptStartTag + "parent.ScrollTop();" + Common.ScriptEndTag);
                Response.Write(Common.ScriptStartTag + "parent.window.setTimeout(\"parent.window.location.href = 'faq-list.aspx'\",1000);" + Common.ScriptEndTag);
                break;
        }
        Response.End();
    }

    #endregion

     
}