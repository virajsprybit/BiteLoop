<%@ WebHandler Language="C#" Class="Upload" %>

using System;
using System.Web;
using System.IO;
using BAL;

public class Upload : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Expires = -1;


      
        if (context.Request.Files.Count > 0)
        {
            HttpPostedFile ObjFile = context.Request.Files[0];
            int KeyIndex = 0;
            foreach (string Key in context.Request.Form.Keys)
            {
                if (Int32.TryParse(Key, out KeyIndex))
                {
                    if (context.Request[Key] == context.Request["FileName"])
                    {
                        try
                        {
                            string savepath = "";
                            string tempPath = "";
                            tempPath = context.Request["folder"];
                            string File = DateTime.Now.Ticks.ToString() + System.IO.Path.GetExtension(ObjFile.FileName);
                            int intResult = 0;

                                
                            savepath = context.Server.MapPath(tempPath);
                            if (!Directory.Exists(savepath))
                                Directory.CreateDirectory(savepath);
                            if (intResult > 0)
                            {
                                ObjFile.SaveAs(savepath + @"\" + File);
                            }
                            context.Response.Write(tempPath + "/" + File);
                            context.Response.StatusCode = 200;
                        }
                        catch (Exception ex)
                        {
                            context.Response.Write("Error: " + ex.Message);
                        }
                    }
                }
            }
        }
    }



    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}