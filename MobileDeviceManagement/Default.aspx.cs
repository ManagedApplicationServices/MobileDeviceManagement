using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobileDeviceManagement
{
    public partial class _Default : Page
    {
        SqlConnection DBConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            BindGvForms();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGvForms();
        }
        protected void BindGvForms()
        {
            string getFormsQuery = "SELECT [ID] AS                                        [#]\n"
                                    + "     , [FullName] AS                                  [Full Name]\n"
                                    + "     , [WitnessedBy] AS                               [Witnessed By]\n"
                                    + "     , [DeviceName] AS                                [Device Name]\n"
                                    + "     , [Manufacturer]\n"
                                    + "     , [Model]\n"
                                    + "     , [Type]\n"
                                    + "     , [Serial]\n"
                                    + "     , [AssetNo] AS                                   [Asset Tag]\n"
                                    + "     , CONVERT( VARCHAR(10), [DateCollected], 103) AS [Date Collected]\n"
                                    + "FROM [MobileDeviceForm] AS [mdf] ";
            string filter = "";
            if (txtSearch.Text.Length > 0)
            {
                filter = "WHERE [mdf].[ID] LIKE '%'+'" + txtSearch.Text + "'+'%'\n"
                            + "      OR [mdf].[FullName] LIKE '%'+'" + txtSearch.Text + "'+'%'\n"
                            + "      OR [mdf].[WitnessedBy] LIKE '%'+'" + txtSearch.Text + "'+'%'\n"
                            + "      OR [mdf].[DeviceName] LIKE '%'+'" + txtSearch.Text + "'+'%'\n"
                            + "      OR [mdf].[Manufacturer] LIKE '%'+'" + txtSearch.Text + "'+'%'\n"
                            + "      OR [mdf].[Model] LIKE '%'+'" + txtSearch.Text + "'+'%'\n"
                            + "      OR [mdf].[Type] LIKE '%'+'" + txtSearch.Text + "'+'%'\n"
                            + "      OR [mdf].[Serial] LIKE '%'+'" + txtSearch.Text + "'+'%'\n"
                            + "      OR [mdf].[AssetNo] LIKE '%'+'" + txtSearch.Text + "'+'%' ";
            }
            string order = "ORDER BY [ID] DESC ";
            getFormsQuery += filter;
            getFormsQuery += order;

            gvForms.DataSource = GetData(getFormsQuery);
            gvForms.DataBind();

            gvForms.UseAccessibleHeader = true;
            gvForms.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        protected void gvForms_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "remove")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvForms.Rows[index];
                string id = gvForms.DataKeys[index].Values[0].ToString();
                string deleteFormQuery = "DELETE FROM [MobileDeviceForm] WHERE [MobileDeviceForm].[ID] = '" + id + "';\n"
                                       + "DELETE FROM [MobileDeviceFormItem] WHERE [MobileDeviceFormItem].[MobileDeviceFormId] = '" + id + "'; ";
                SqlCommand deleteFormCmd = new SqlCommand(deleteFormQuery, DBConn);
                DBConn.Open();
                deleteFormCmd.ExecuteNonQuery();
                DBConn.Close();
                string message = "Form " + id + " has been deleted";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + message + "');", true);
                BindGvForms();
            }
            if (e.CommandName == "view")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvForms.Rows[index];
                string id = row.Cells[0].Text;
                Page.Response.Redirect("~/Form.aspx?id=" + id);
            }
        }
        protected void gvForms_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvForms.PageIndex = e.NewPageIndex;
            BindGvForms();
        }
        protected void gvForms_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Repeater rFormItems = e.Row.FindControl("rFormItems") as Repeater;
                string id = gvForms.DataKeys[e.Row.RowIndex].Values[0].ToString();
                rFormItems.DataSource = GetData("SELECT m.[Item] as [Item] FROM [MobileDeviceFormItem] AS mdfi INNER JOIN [MDFItems] AS m ON [m].[ID] = [mdfi].ItemId WHERE [mdfi].[MobileDeviceFormId] = '" + id + "'");
                rFormItems.DataBind();
            }
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