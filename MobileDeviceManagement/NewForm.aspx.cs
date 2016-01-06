using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MobileDeviceManagement
{
    public partial class NewForm : System.Web.UI.Page
    {
        SqlConnection DBConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        public static List<string> formItems = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            string user = Page.User.Identity.Name;
            DirectorySearcher dirSearcher = new DirectorySearcher();
            DirectoryEntry entry = new DirectoryEntry(dirSearcher.SearchRoot.Path);
            dirSearcher.Filter = "(&(objectClass=user)(objectcategory=person)(samaccountname=" + user.Replace(@"RSP\", "") + "*))";
            SearchResult srfullName = dirSearcher.FindOne();
            string propName = "displayname";
            ResultPropertyValueCollection valColl = srfullName.Properties[propName];
            string fullname = Convert.ToString(valColl[0]);

            if (!IsPostBack)
            {
                txtWitnessed.Text = fullname;
                txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtDate.Enabled = false;

                string getItems = "SELECT * FROM [MDFItems] AS [m]";
                rItems.DataSource = GetData(getItems);
                rItems.DataBind();

                formItems.Clear();
            }
        }
        protected void lbNext_Click(object sender, EventArgs e)
        {
            string insertFormQuery = "INSERT INTO [MobileDeviceForm]\n"
              + "(\n"
              + "    [FullName],\n"
              + "    [WitnessedBy],\n"
              + "    [DeviceName],\n"
              + "    [Manufacturer],\n"
              + "    [Model],\n"
              + "    [Type],\n"
              + "    [Serial],\n"
              + "    [AssetNo],\n"
              + "    [DateCollected]\n"
              + ")\n"
              + "OUTPUT INSERTED.[ID]\n"
              + "VALUES\n"
              + "(\n"
              + "    @fullName, -- FullName - varchar\n"
              + "    @witnessedBy, -- WitnessedBy - varchar\n"
              + "    @deviceName, -- DeviceName - varchar\n"
              + "    @manufacturer, -- Manufacturer - varchar\n"
              + "    @model, -- Model - varchar\n"
              + "    @type, -- Type - varchar\n"
              + "    @serial, -- Serial - varchar\n"
              + "    @assetNo, -- AssetNo - varchar\n"
              + "    @dateCollected -- DateCollected - datetime\n"
              + ")";
            SqlCommand insertFormCmd = new SqlCommand(insertFormQuery, DBConn);
            insertFormCmd.Parameters.AddWithValue("@fullName", txtFullName.Text);
            insertFormCmd.Parameters.AddWithValue("@witnessedBy", txtWitnessed.Text);
            insertFormCmd.Parameters.AddWithValue("@deviceName", txtDeviceName.Text);
            insertFormCmd.Parameters.AddWithValue("@manufacturer", txtMf.Text);
            insertFormCmd.Parameters.AddWithValue("@model", txtModel.Text);
            insertFormCmd.Parameters.AddWithValue("@type", txtType.Text);
            insertFormCmd.Parameters.AddWithValue("@serial", txtSerial.Text);
            insertFormCmd.Parameters.AddWithValue("@assetNo", txtAsset.Text);
            insertFormCmd.Parameters.AddWithValue("@dateCollected", txtDate.Text);
            DBConn.Open();
            int formId = Convert.ToInt32(insertFormCmd.ExecuteScalar());
            DBConn.Close();

            DBConn.Open();
            for (int i = 0; i < formItems.Count; i++)
            {
                string insertItemQuery = "INSERT INTO [MobileDeviceFormItem]\n"
              + "(\n"
              + "    [MobileDeviceFormId],\n"
              + "    [ItemId]\n"
              + ")\n"
              + "VALUES\n"
              + "(\n"
              + "    @formId, -- MobileDeviceFormId - int\n"
              + "    @itemId -- ItemId - int\n"
              + ")";
                SqlCommand insertItemCmd = new SqlCommand(insertItemQuery, DBConn);
                insertItemCmd.Parameters.AddWithValue("@formId", formId);
                insertItemCmd.Parameters.AddWithValue("@itemId", formItems[i]);
                insertItemCmd.ExecuteNonQuery();
            }
            DBConn.Close();

            formItems.Clear();

            Page.Response.Redirect("~/Form.aspx?id=" + formId);
        }
        [WebMethod]
        public static void insertItem(string id)
        {
            formItems.Add(id);
        }
        [WebMethod]
        public static void removeItem(string id)
        {
            formItems.Remove(id);
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