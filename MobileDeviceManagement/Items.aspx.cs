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
    public partial class Items : System.Web.UI.Page
    {
        SqlConnection DBConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            BindGvItems();
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtItem.Text.Length > 0)
            {
                string InsertItemQuery = "INSERT INTO [MDFItems]\n"
                                       + "(\n"
                                       + "    [Item]\n"
                                       + ")\n"
                                       + "VALUES\n"
                                       + "(\n"
                                       + "    @item -- Item - varchar\n"
                                       + ") ";
                SqlCommand InsertItemCmd = new SqlCommand(InsertItemQuery, DBConn);
                InsertItemCmd.Parameters.AddWithValue("@item", txtItem.Text);
                DBConn.Open();
                InsertItemCmd.ExecuteNonQuery();
                DBConn.Close();

                string message = txtItem.Text + " added";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + message + "');", true);
                BindGvItems();

                txtItem.Text = "";
            }
        }
        protected void BindGvItems()
        {
            string GetItemsQuery = "SELECT [ID] AS   [#]\n"
                                   + "     , [Item] AS [Item]\n"
                                   + "FROM [MDFItems] AS [m]";

            gvItems.DataSource = GetData(GetItemsQuery);
            gvItems.DataBind();

            gvItems.UseAccessibleHeader = true;
            gvItems.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        protected void gvItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "remove")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvItems.Rows[index];
                string id = gvItems.DataKeys[index].Values[0].ToString();
                string GetItemCountQuery = "SELECT COUNT(*)\n"
                                           + "FROM [MobileDeviceFormItem] AS [mdfi]\n"
                                           + "WHERE [mdfi].[ItemId] = '" + id + "';";
                SqlCommand getItemCountCmd = new SqlCommand(GetItemCountQuery, DBConn);
                DBConn.Open();
                int count = Convert.ToInt32(getItemCountCmd.ExecuteScalar());
                DBConn.Close();
                if (count == 0)
                {
                    string deleteItemQuery = "DELETE FROM [MDFItems] WHERE [ID] = '" + id + "'; ";
                    SqlCommand deleteFormCmd = new SqlCommand(deleteItemQuery, DBConn);
                    DBConn.Open();
                    deleteFormCmd.ExecuteNonQuery();
                    DBConn.Close();
                    string message = "Item " + id + " has been deleted";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + message + "');", true);
                    BindGvItems();
                }
                else
                {
                    string message = "Item " + id + " exists in form , cannot be deleted";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('" + message + "');", true);
                }
            }
        }
        protected void gvItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvItems.PageIndex = e.NewPageIndex;
            BindGvItems();
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