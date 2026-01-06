using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

public partial class AdminPanel_Category_Modify : AdminAuthentication
{
    CategoryBAL objCategoryBAL = new CategoryBAL();

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
        objCategoryBAL.ID = ID;

        if (Request.Form.Keys.Count > 0)
        { SaveInfo(); }
        else
        {
            if (objCategoryBAL.ID != 0)
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
            objCategoryBAL.ID = ID;
            dt = objCategoryBAL.GetCategoryByID();
            if (dt.Rows.Count > 0)
            {
                tbxTitle.Value = Convert.ToString(dt.Rows[0]["Name"]);
                tbxURL.Value = Convert.ToString(dt.Rows[0]["CategoryURL"]);
                if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["ImageName"])))
                {
                    hdnImage.Value = Convert.ToString(dt.Rows[0]["ImageName"]);
                    imgImage.Src = Config.VirtualDir + "thumb.aspx?path=" + Config.CMSFiles + Convert.ToString(dt.Rows[0]["ImageName"]) + "&width=152&height=112";
                    imgImage.Visible = true;
                }
                else
                {
                    imgImage.Visible = false;
                    hdnImage.Value = string.Empty;
                }
            }
        }
    }
    #endregion

    #region Save Information
    private void SaveInfo()
    {
        if (Request.Files[fupdImage.UniqueID] != null)
        {
            if (Request.Files[fupdImage.UniqueID].ContentLength > 0)
            {
                strImageName = System.IO.Path.GetExtension(fupdImage.PostedFile.FileName);
                if (IsImage(fupdImage.PostedFile.FileName.ToLower()) == false)
                {
                    Response.Write(Common.ShowMessage("Please upload only .JPG, .JPEG, .PNG, .BMP, .GIF image files.", "alert-message error", divMsg.ClientID));
                }
                strImageName = Convert.ToString(DateTime.Now.Day) + Convert.ToString(DateTime.Now.Month) + Convert.ToString(DateTime.Now.Year) + Convert.ToString(DateTime.Now.Date.Hour) + Convert.ToString(DateTime.Now.Minute) + Convert.ToString(DateTime.Now.Second) + Convert.ToString(DateTime.Now.Ticks) + strImageName;
                if (ID != 0)
                {
                    DeleteCategoryFile(Convert.ToString(Request[hdnCategoryFile.UniqueID]));
                }
                Request.Files[fupdImage.UniqueID].SaveAs(Request.PhysicalApplicationPath + "/" + Config.CMSFiles + strImageName);
            }
            else
            {
                if (!string.IsNullOrEmpty(Request[hdnImage.UniqueID]))
                {
                    strImageName = Request[hdnImage.UniqueID];
                }
                else
                {
                    strImageName = string.Empty;
                }
            }
        }

        objCategoryBAL.ImageName = strImageName;
        objCategoryBAL.ID = ID;
        objCategoryBAL.Name = Convert.ToString(Request[tbxTitle.UniqueID]).Trim();
        objCategoryBAL.CategoryURL = Convert.ToString(Request[tbxURL.UniqueID]).Trim();
        
        int intResult = objCategoryBAL.Save();

        switch (intResult)
        {
            case -1:

                Response.Write(Common.ShowMessage("Category Title already exists. So please try another Category Title.", "alert-message error", divMsg.ClientID));
                Response.Write(Common.ScriptStartTag + "parent.ScrollTop();" + Common.ScriptEndTag);
                break;
            default:
                Response.Write(Common.ShowMessage("Category information has been saved successfully.", "alert-message success", divMsg.ClientID));
                Response.Write(Common.ScriptStartTag + "parent.ScrollTop();" + Common.ScriptEndTag);
                Response.Write(Common.ScriptStartTag + "parent.window.setTimeout(\"parent.window.location.href = 'Category-list.aspx'\",1000);" + Common.ScriptEndTag);
                break;
        }
        Response.End();

    }
    #endregion

    #region General
    
    private void DeleteCategoryFile(string strImage)
    {
        if (!string.IsNullOrEmpty(strImage))
        {
            if (Common.IsFileExist(Config.CMSFiles, strImage))
            {
                Common.FileDelete(Config.CMSFiles, strImage);
            }
        }
    }
    public static bool IsImage(string strImageName)
    {
        bool blnReturnValue = false;
        switch (System.IO.Path.GetExtension(strImageName).ToLower())
        {
            case ".jpg":
            case ".jpeg":
            case ".gif":
            case ".png":
            case ".bmp":
                blnReturnValue = true;
                break;
        }
        return blnReturnValue;
    }

    #endregion
}