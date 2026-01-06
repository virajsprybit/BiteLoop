using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Newtonsoft.Json;
using System.Web;
using System.IO;

public partial class webservice_user_report_issue_list : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["UserID"] != null)
            {
                LoadIssueList();
            }
            else
            {
                WriteJsonResponse(new
                {
                    success = "false",
                    message = "UserID is required.",
                    StatusCode = "400",
                    data = new object[] { }
                });
            }
        }
    }


    private void LoadIssueList()
    {
        string UserID = Convert.ToString(Request.QueryString["UserID"] ?? "").Trim();

        if (UserID == "")
        {
            WriteJsonResponse(new
            {
                success = "false",
                message = "UserID is required.",
                StatusCode = "400",
                data = new object[] { }
            });
            return;
        }

        string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        string baseUrl = ConfigurationManager.AppSettings["WebSiteUrl"] + "source/CMSFiles/";

        DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(connStr))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("UserReportIssue_List", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", UserID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
        }

        if (dt.Rows.Count == 0)
        {
            WriteJsonResponse(new
            {
                success = "true",
                message = "No issue reports found.",
                StatusCode = "200",
                data = new object[] { }
            });
            return;
        }

        var list = new System.Collections.Generic.List<object>();

        foreach (DataRow row in dt.Rows)
        {
            string imagesCsv = row["Images"].ToString();
            string[] imgs = imagesCsv.Split(',');

            string img1 = imgs.Length > 0 && imgs[0] != "" ? baseUrl + Path.GetFileName(imgs[0]) : "";
            string img2 = imgs.Length > 1 && imgs[1] != "" ? baseUrl + Path.GetFileName(imgs[1]) : "";
            string img3 = imgs.Length > 2 && imgs[2] != "" ? baseUrl + Path.GetFileName(imgs[2]) : "";

            list.Add(new
            {
                //ReportID = row["ReportID"].ToString(),
                BusinessID = row["BusinessID"].ToString(),
                OrderUniqueID = row["OrderUniqueID"].ToString(),
                CaseNumber = row["CaseNumber"].ToString(),
                IssueDescription = row["IssueDescription"].ToString(),
                //CreatedOn = Convert.ToDateTime(row["CreatedOn"]).ToString("yyyy-MM-dd HH:mm:ss"),

                Image1 = img1,
                Image2 = img2,
                Image3 = img3
            });
        }

        WriteJsonResponse(new
        {
            success = "true",
            message = "Issue List Loaded Successfully.",
            StatusCode = "200",
            data = list
        });
    }

    private void WriteJsonResponse(object obj)
    {
        HttpContext.Current.Response.Write(
            JsonConvert.SerializeObject(obj, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            })
        );
        Response.End();
    }
}