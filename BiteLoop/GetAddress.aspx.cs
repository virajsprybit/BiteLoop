using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GetAddress : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        FillCityStateCountry();
    }
    private void FillCityStateCountry()
    {
        txtCity.Text = hdCity.Value;
        txtState.Text = hdState.Value;
        txtCountry.Text = hdCountry.Value;
    }    
}