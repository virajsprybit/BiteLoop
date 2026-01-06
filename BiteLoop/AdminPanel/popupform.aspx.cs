using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BAL;
using Utility;
using DAL;

public partial class controlpanel_popupform : AdminAuthentication
{
    private new int ID;
    protected string Category = "";
    protected string FoodItems = "";
    private void BindEnquiry()
    {
        ContactUsBAL sbal = new ContactUsBAL();
        DataTable contactUSByID = new DataTable();
        sbal.ID = this.ID;
        contactUSByID = sbal.GetContactUSByID();
        if (contactUSByID.Rows.Count > 0)
        {
            rptContactUs.DataSource = contactUSByID;
            rptContactUs.DataBind();
            divContactUs.Visible = true;
        }
    }
    private void BindEnquiryVendor()
    {

        DataTable contactUSByID = new DataTable();
        contactUSByID = GetVendorContactUSByID(this.ID);
        if (contactUSByID.Rows.Count > 0)
        {
            rptContactUs.DataSource = contactUSByID;
            rptContactUs.DataBind();
            divContactUs.Visible = true;
        }
    }
    private void BindPartners()
    {
        SalesAdminBAL objSalesAdminBAL = new SalesAdminBAL();
        objSalesAdminBAL.ID = ID;
        DataSet ds = new DataSet();
        ds = objSalesAdminBAL.BusinessDetailsByID();


        if (ds.Tables[1].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                Category = Category + Convert.ToString(ds.Tables[1].Rows[i]["Name"]) + ", ";
            }
            if (Category.Length > 0)
            {
                Category = Category.Substring(0, Category.Length - 2);
            }
        }
        if (ds.Tables[2].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
            {
                FoodItems = FoodItems + Convert.ToString(ds.Tables[2].Rows[i]["Name"]) + ", ";
            }
            if (FoodItems.Length > 0)
            {
                FoodItems = FoodItems.Substring(0, FoodItems.Length - 2);
            }
        }
        if (ds.Tables[0].Rows.Count > 0)
        {
            rptPartners.DataSource = ds.Tables[0];
            rptPartners.DataBind();
            divPartners.Visible = true;
        }
    }

    private void BindUser()
    {
        UsersBAL objUsersBAL = new UsersBAL();
        objUsersBAL.ID = ID;
        DataTable dt = new DataTable();
        dt = objUsersBAL.UserDetailsByID();
        if (dt.Rows.Count > 0)
        {
            rptUsers.DataSource = dt;
            rptUsers.DataBind();
            divUser.Visible = true;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Request["type"]) == "enquiry")
        {
            int.TryParse(Request["id"], out this.ID);
            rptContactUs.Visible = true;
            BindEnquiry();
            h4head.InnerText = "Contact Us enquiry";
        }
        if (Convert.ToString(Request["type"]) == "vendorenquiry")
        {
            int.TryParse(Request["id"], out this.ID);
            rptContactUs.Visible = true;
            BindEnquiryVendor();
            h4head.InnerText = "Vendor Contact Enquiry";
        }
        if (Convert.ToString(Request["type"]) == "user")
        {
            int.TryParse(Request["id"], out this.ID);
            rptUsers.Visible = true;
            BindUser();
            h1USer.InnerText = "User";
        }
        if (Convert.ToString(Request["type"]) == "vendor")
        {
            int.TryParse(Request["id"], out this.ID);
            rptPartners.Visible = true;
            BindPartners();

            Heading.InnerText = "Vendor";

        }


    }

    public DataTable GetVendorContactUSByID(long VendorContactID)
    {
        DbParameter[] dbParam = new DbParameter[] { new DbParameter("@ID", DbParameter.DbType.Int, 20, VendorContactID) };
        DataTable table = new DataTable();
        return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "ContactUSByIDForVendor", dbParam);
    }
}
