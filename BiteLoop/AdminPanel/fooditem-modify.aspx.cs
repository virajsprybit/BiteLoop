using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

public partial class AdminPanel_FoodItems_Modify : AdminAuthentication
{
    FoodItemsBAL objFoodItemsBAL = new FoodItemsBAL();

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
        objFoodItemsBAL.ID = ID;

        if (Request.Form.Keys.Count > 0)
        { SaveInfo(); }
        else
        {
            if (objFoodItemsBAL.ID != 0)
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
            objFoodItemsBAL.ID = ID;
            dt = objFoodItemsBAL.GetFoodItemsByID();
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
     
        objFoodItemsBAL.ID = ID;
        objFoodItemsBAL.Name = Convert.ToString(Request[tbxTitle.UniqueID]).Trim();     
        
        int intResult = objFoodItemsBAL.Save();

        switch (intResult)
        {
            case -1:

                Response.Write(Common.ShowMessage("Title already exists. So please try another Title.", "alert-message error", divMsg.ClientID));
                Response.Write(Common.ScriptStartTag + "parent.ScrollTop();" + Common.ScriptEndTag);
                break;
            default:
                Response.Write(Common.ShowMessage("Food Item information has been saved successfully.", "alert-message success", divMsg.ClientID));
                Response.Write(Common.ScriptStartTag + "parent.ScrollTop();" + Common.ScriptEndTag);
                Response.Write(Common.ScriptStartTag + "parent.window.setTimeout(\"parent.window.location.href = 'fooditems-list.aspx'\",1000);" + Common.ScriptEndTag);
                break;
        }
        Response.End();

    }
    #endregion
    
}