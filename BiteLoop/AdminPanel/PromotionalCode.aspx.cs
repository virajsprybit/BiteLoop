using BAL;
using System;
using System.Data;
using Utility;
using System.Web.UI.WebControls;
using BiteLoop.Common;
using System.Web.Services;
using Newtonsoft.Json;
using System.Web.Script.Services;

public partial class PromotionalCode : System.Web.UI.Page
{
    private int _ID = 0;
    PromotionalCodeBAL objPCBAL = new PromotionalCodeBAL();
    protected string strPages = string.Empty;

    protected string strBusiness = string.Empty;
    protected string strUsers = string.Empty;


    public new int ID
    {
        get
        {
            return _ID;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Int32.TryParse(Request["id"], out _ID);
        objPCBAL.ID = ID;

        if (Request.Form.Keys.Count > 0)
        {
            SaveInfo();
        }
        else
        {
            BindState();
            if (objPCBAL.ID != 0)
            {
                BindControls();
            }
        }
    }
    private void BindState()
    {
        CommonBAL objCommonBAL = new CommonBAL();
        DataTable dt = new DataTable();
        dt = objCommonBAL.StateList();
        if (dt.Rows.Count > 0)
        {
            ddlState.DataSource = dt;
            ddlState.DataTextField = "StateCode";
            ddlState.DataValueField = "ID";
            ddlState.DataBind();
        }
        ddlState.Items.Insert(0, new ListItem("-- All --", "0"));
    }
  
    private void BindControls()
    {
        DataTable dt = new DataTable();
        objPCBAL.ID = ID;
        dt = objPCBAL.GetByID();
        if (dt != null && dt.Rows.Count > 0)
        {
            hdnBusiness.Value = Convert.ToString(dt.Rows[0]["Business"]).Trim();
            hdnUsers.Value = Convert.ToString(dt.Rows[0]["Users"]).Trim();
            tbxCouponCode.Value = Convert.ToString(dt.Rows[0]["CouponCode"]).Trim();
            ddlDiscountType.Value = Convert.ToString(dt.Rows[0]["DiscountType"]).Trim();
            tbxDiscountAmount.Value = Convert.ToString(dt.Rows[0]["DiscountAmount"]).Trim();
            tbxMinimumAmount.Value = Convert.ToString(dt.Rows[0]["MinOrderAmount"]).Trim();
            tbxCouponStartTime.Value = Convert.ToDateTime(dt.Rows[0]["CouponStartTime"]).ToString("dd/MMM/yyyy");
            tbxCouponEndTime.Value = Convert.ToDateTime(dt.Rows[0]["CouponEndTime"]).ToString("dd/MMM/yyyy");
            ddlState.Value = Convert.ToString(dt.Rows[0]["State"]).Trim();
            ddlSingleUser.Value = Convert.ToString(dt.Rows[0]["SingleUse"]).Trim();  
            
            strPages = Convert.ToString(dt.Rows[0]["CouponCode"]).Trim();
        }
    }
    #region Save Information
    private void SaveInfo()
    {
        objPCBAL.ID = ID;
        objPCBAL.CouponCode = Request[tbxCouponCode.UniqueID].Trim();
        objPCBAL.DiscountType = Convert.ToInt32(Request[ddlDiscountType.UniqueID]);
        objPCBAL.DiscountAmount = Convert.ToDecimal(Request[tbxDiscountAmount.UniqueID]);
        objPCBAL.CouponStartTime = Convert.ToDateTime(Request[tbxCouponStartTime.UniqueID]);
        objPCBAL.CouponEndTime = Convert.ToDateTime(Request[tbxCouponEndTime.UniqueID]);
        objPCBAL.State = Convert.ToInt32(Request[ddlState.UniqueID]);
        objPCBAL.MinOrderAmount = Convert.ToDecimal(Request[tbxMinimumAmount.UniqueID]);


        if (Convert.ToInt32(Request[ddlSingleUser.UniqueID]) == 1)
        {
            objPCBAL.SingleUse = true;
        }
        else
        {
            objPCBAL.SingleUse = false;
        }
        switch (objPCBAL.Save(Convert.ToString(Request[hdnBusiness.UniqueID]), Convert.ToString(Request[hdnUsers.UniqueID])))
        {
            case 0:
                ShowMessage("Promotional Code already exists.", "alert-message error", divMsg.ClientID);
                break;
            default:
                ShowMessage("Promotional Code has been saved successfully.", "alert-message success", divMsg.ClientID);
                Response.Write(Common.ScriptStartTag + "parent.ScrollTop();" + Common.ScriptEndTag);
                Response.Write(Common.ScriptStartTag + "parent.window.setTimeout(\"parent.window.location.href = 'promotionalcodelist.aspx'\",2000);" + Common.ScriptEndTag);
                break;
        }
        Response.End();
    }
    #endregion
   
    private void ShowMessage(string strMessage, string strMessageType, string divMessage)
    {
        Response.Write("<link href='" + Config.VirtualDir + "style/style.css' rel='Stylesheet' type='text/css' />");
        Response.Write("<script type='text/javascript' language='javascript' src='" + Config.VirtualDir + "js/jquery.1.10.1.min.js'></script>");
        Response.Write("<script type='text/javascript' language='javascript' src='" + Config.VirtualDir + "js/general.js'></script>");
        Response.Write("<script type='text/javascript'>var virtualDir = '" + Config.VirtualDir + "';</script>");
        Response.Write("$(document.ready(function {");
        Response.Write(Common.ScriptStartTag + "parent.$('#" + divMessage + "').show();" + Common.ScriptEndTag);
        Response.Write(Common.ScriptStartTag + "parent.$('#" + divMessage + "').html('" + strMessage + "');" + Common.ScriptEndTag);
        Response.Write(Common.ScriptStartTag + "parent.$('#" + divMessage + "').attr('class', '" + strMessageType + "');" + Common.ScriptEndTag);
        Response.Write(Javascript.ScriptStartTag + "window.setTimeout(\"parent.$('#" + divMessage + "').fadeOut(600);\",5000)" + Javascript.ScriptEndTag);
        Response.Write("});");
    }

    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public static string BindBusinessForPromocode()
    //{
    //    string Json = "";
    //    CommonBAL objCommonBAL = new CommonBAL();
    //    DataTable dt = new DataTable();
    //    dt = objCommonBAL.GetBusinessOrUsers(1);
    //    Json = JsonConvert.SerializeObject(dt);  

    //    return Json;
    //}

}