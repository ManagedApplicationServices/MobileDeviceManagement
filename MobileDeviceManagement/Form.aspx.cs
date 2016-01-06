using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SelectPdf;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace MobileDeviceManagement
{
    public partial class Form : System.Web.UI.Page
    {
        string formId = "";
        SqlConnection DBConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        string hideBtn = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            formId = Request.QueryString["id"];
            Page.Title = "Form " + formId;
            hideBtn = Request.QueryString["hideBtn"];

            if(!IsPostBack)
            {
                if (hideBtn == "true")
                {
                    btnPDF.Visible = false;
                    btnBack.Visible = false;
                }
                string getFormQuery = "SELECT *\n"
                   + "     , CONVERT( VARCHAR(10), [DateCollected], 103) AS [date]\n"
                   + "FROM [MobileDeviceForm] AS [mdf]\n"
                   + "WHERE [mdf].[ID] = '" + formId + "'";
                SqlCommand getFormCmd = new SqlCommand(getFormQuery, DBConn);
                DBConn.Open();
                SqlDataReader dr = getFormCmd.ExecuteReader();
                while (dr.Read())
                {
                    lblFullName1.Text = dr["FullName"].ToString();
                    lblFullName2.InnerText = dr["FullName"].ToString();
                    lblDate1.Text = dr["date"].ToString();
                    lblDate2.InnerText = dr["date"].ToString();
                    lblDeviceName.InnerText = dr["DeviceName"].ToString();
                    lblMf.InnerText = dr["Manufacturer"].ToString();
                    lblModel.InnerText = dr["Model"].ToString();
                    lblType.InnerText = dr["Type"].ToString();
                    lblSerial.InnerText = dr["Serial"].ToString();
                    lblAsset.InnerText = dr["AssetNo"].ToString();
                    lblWitnessed.InnerText = dr["WitnessedBy"].ToString();
                }
                DBConn.Close();

                rItems.DataSource = GetData("SELECT m.[Item] as [Item] FROM [MobileDeviceFormItem] AS mdfi INNER JOIN [MDFItems] AS m ON [m].[ID] = [mdfi].ItemId WHERE [mdfi].[MobileDeviceFormId] = '" + formId + "'");
                rItems.DataBind();
            }

        }

        protected void btnPDF_Click(object sender, EventArgs e)
        {
            string url = @"http://localhost/MobileDeviceManagement/Form.aspx?id=" + formId +"&hideBtn=true";

            string pdf_page_size = "A4";
            PdfPageSize pageSize = (PdfPageSize)Enum.Parse(typeof(PdfPageSize),
                pdf_page_size, true);

            string pdf_orientation = "Portrait";
            PdfPageOrientation pdfOrientation =
                (PdfPageOrientation)Enum.Parse(typeof(PdfPageOrientation),
                pdf_orientation, true);

            int webPageWidth = 1024;

            int webPageHeight = 0;

            // instantiate a html to pdf converter object
            HtmlToPdf converter = new HtmlToPdf();

            // set converter options
            converter.Options.PdfPageSize = pageSize;
            converter.Options.PdfPageOrientation = pdfOrientation;
            converter.Options.WebPageWidth = webPageWidth;
            converter.Options.WebPageHeight = webPageHeight;

            // create a new pdf document converting an url
            PdfDocument doc = converter.ConvertUrl(url);

            // save pdf document
            doc.Save(Response, false, "Form " + formId + ".pdf");

            // close pdf document
            doc.Close();
        }

        private static DataTable GetData(string query)
        {
            string strConnString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(strConnString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = query;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataSet ds = new DataSet())
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }
    }
}