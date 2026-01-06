using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using Utility;

public partial class AdminPanel_SalesAdmin_Modify : AdminAuthentication
{
    #region Private Members
    private int _ID = 0;
    SalesAdminBAL objSalesAdminBAL = new SalesAdminBAL();
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
        objSalesAdminBAL.ID = ID;

        if (Request.Form.Keys.Count > 0)
        {
            SaveInfo();
        }
        else
        {  
            if (objSalesAdminBAL.ID != 0)
            {
                tbPassword.Visible = true;
                BindControls();
            }
            else
            {
                tbPassword.Visible = true;
            }
        }

    }
    #endregion

    #region Bind Controls

  
    private void BindControls()
    {
        DataTable dtblAdminInfo = new DataTable();
        objSalesAdminBAL.ID = ID;
        dtblAdminInfo = objSalesAdminBAL.GetByID();
        if (dtblAdminInfo.Rows.Count > 0)
        {
            tbxFirstName.Value = Convert.ToString(dtblAdminInfo.Rows[0]["FullName"]).Trim();
            tbxEmail.Value = Convert.ToString(dtblAdminInfo.Rows[0]["EmailAddress"]).Trim();
            tbxphone.Value = Convert.ToString(dtblAdminInfo.Rows[0]["Mobile"]).Trim();
            trConfirmPwd.Visible = false;
            divlblPwd.Visible = true;
            divPwd.Style.Add("display", "none");
            string strPwd = Utility.Security.EncryptDescrypt.DecryptString(Convert.ToString(dtblAdminInfo.Rows[0]["Password"]));
            hdnpwd.Value = Convert.ToString(dtblAdminInfo.Rows[0]["Password"]).Trim();
            lblPassword.InnerText = strPwd;
            tbxPassword.Attributes.Add("value", strPwd);
            tbxPassword.Attributes.Add("type", "text");            
           
        }         
    }
    #endregion

    

    #region Save Information
    private void SaveInfo()
    {
        objSalesAdminBAL.ID = ID;
        string strPasword = string.Empty;
        if (!string.IsNullOrEmpty(Request[tbxPassword.UniqueID]))
        {
            strPasword = Request[tbxPassword.UniqueID];
        }
        else
        {
            strPasword = Utility.Security.EncryptDescrypt.DecryptString(Request[hdnpwd.UniqueID]);
        }

        objSalesAdminBAL.FirstName = Request[tbxFirstName.UniqueID].Trim();        
        
        objSalesAdminBAL.Password = Utility.Security.EncryptDescrypt.EncryptString(strPasword);
        objSalesAdminBAL.EmailID = Request[tbxEmail.UniqueID].Trim();
        objSalesAdminBAL.Phone = Request[tbxphone.UniqueID].Trim();        
        switch (objSalesAdminBAL.Save())
        {
            case 0: 
              //Response.Write(Common.ShowMessage("This email address already exists. So please try another email address.", "alert-message error", divMsg.ClientID));
              //Response.Write(Common.ScriptStartTag + "parent.ScrollTop();" + Common.ScriptEndTag);
                Response.Write("duplicate");
                break;
            default:
                Response.Write("success");
                //Response.Write(Common.ShowMessage("Sales Admin information has been saved.", "alert-message success", divMsg.ClientID)); 
                //Response.Write(Common.ScriptStartTag + "parent.ScrollTop();" + Common.ScriptEndTag);
                //Response.Write(Javascript.ScriptStartTag + "window.setTimeout(\"window.location.href='sales-admin-list.aspx'\",2000)" + Javascript.ScriptEndTag);
                break;
        }
        Response.End();
    }
    #endregion
}