using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Newtonsoft.Json;
using System.Web;
using System.IO;
using System.Collections.Generic;

public partial class webservice_user_report_issue : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Keys.Count > 0)
        {
            ReportIssue();
        }   
    }

    private void ReportIssue()
    {
        string OrderID = Convert.ToString(Request["OrderID"] ?? "").Trim();
        string IssueDescription = Convert.ToString(Request["IssueDescription"] ?? "").Trim();
        string Image1 = Convert.ToString(Request["Image1"] ?? "").Trim();
        string Image2 = Convert.ToString(Request["Image2"] ?? "").Trim();
        string Image3 = Convert.ToString(Request["Image3"] ?? "").Trim();

        if (OrderID == "" || IssueDescription == "")
        {
            WriteJsonResponse(new
            {
                success = "false",
                message = "OrderID and IssueDescription are required.",
                StatusCode = "400",
                data = new object[] { }
            });
            return;
        }

        string caseTempName = "temp_" + DateTime.Now.Ticks;

        string img1 = SaveImage(Image1, caseTempName, 1);
        string img2 = SaveImage(Image2, caseTempName, 2);
        string img3 = SaveImage(Image3, caseTempName, 3);

        List<string> imagesList = new List<string>();
        if (img1 != "") imagesList.Add(img1);
        if (img2 != "") imagesList.Add(img2);
        if (img3 != "") imagesList.Add(img3);

        string images = string.Join(",", imagesList.ToArray());

        int returnVal = 0;
        string caseNumber = "";
        string businessID = "";
        string orderUniqueID = "";

        string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connStr))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("UserReportIssue_Insert", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@OrderID", OrderID);
                cmd.Parameters.AddWithValue("@IssueDescription", IssueDescription);
                cmd.Parameters.AddWithValue("@Images", images);

                SqlParameter caseParam = new SqlParameter("@CaseNumber", SqlDbType.VarChar, 20);
                caseParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(caseParam);

                SqlParameter businessParam = new SqlParameter("@BusinessID", SqlDbType.BigInt);
                businessParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(businessParam);

                SqlParameter orderParam = new SqlParameter("@OrderUniqueIDOut", SqlDbType.BigInt);
                orderParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(orderParam);

                SqlParameter output = new SqlParameter("@ReturnVal", SqlDbType.Int);
                output.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(output);

                cmd.ExecuteNonQuery();

                returnVal = Convert.ToInt32(output.Value);
                caseNumber = Convert.ToString(caseParam.Value);
                businessID = Convert.ToString(businessParam.Value);
                orderUniqueID = Convert.ToString(orderParam.Value);
            }

            string fullImg1 = string.IsNullOrEmpty(img1) ? "" : baseUrl + Path.GetFileName(img1);
            string fullImg2 = string.IsNullOrEmpty(img2) ? "" : baseUrl + Path.GetFileName(img2);
            string fullImg3 = string.IsNullOrEmpty(img3) ? "" : baseUrl + Path.GetFileName(img3);

            if (returnVal == 1)
            {
                WriteJsonResponse(new
                {
                    success = "true",
                    message = "Your issue has been submitted successfully.",
                    StatusCode = "200",
                    data = new[]
                    {
                        new {
                             BusinessID = businessID,
                             OrderUniqueID = orderUniqueID,
                             CaseNumber = caseNumber,
                             IssueDescription = IssueDescription,
                             Image1 = fullImg1,
                             Image2 = fullImg2,
                             Image3 = fullImg3
                        }
                    }
                });
                return;
            }

            if (returnVal == -1)
            {
                WriteJsonResponse(new
                {
                    success = "false",
                    message = "Invalid OrderID.",
                    StatusCode = "200",
                    data = new object[] { }
                });
                return;
            }

            WriteJsonResponse(new
            {
                success = "false",
                message = "Something went wrong. Please try again.",
                StatusCode = "500",
                data = new object[] { }
            });
        }
    }

    private string SaveImage(string base64, string caseNum, int num)
    {
        try
        {
            HttpPostedFile file = Request.Files["Image" + num];

            if (file != null && file.ContentLength > 0)
            {
                string folder = HttpContext.Current.Server.MapPath("~/source/CMSFiles/");
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                string fileName = caseNum + "_" + num + Path.GetExtension(file.FileName);
                string filePath = Path.Combine(folder, fileName);

                file.SaveAs(filePath);

                return "/source/CMSFiles/" + fileName;
            }

            if (!string.IsNullOrWhiteSpace(base64))
            {
                byte[] bytes = Convert.FromBase64String(base64);

                string folder = HttpContext.Current.Server.MapPath("~/source/CMSFiles/");
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                string fileName = caseNum + "_" + num + ".jpg";
                File.WriteAllBytes(Path.Combine(folder, fileName), bytes);

                return "/source/CMSFiles/" + fileName;
            }
        }
        catch
        {

        }
        return "";
    }

    string baseUrl = ConfigurationManager.AppSettings["WebSiteUrl"] + "source/CMSFiles/";

    private void WriteJsonResponse(object obj)
    {
        HttpContext.Current.Response.Write(
            JsonConvert.SerializeObject(obj)
        );
        Response.End();
    }

    private void WriteResponse(Response obj)
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