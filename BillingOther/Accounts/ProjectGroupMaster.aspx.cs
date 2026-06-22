using BillingOther.App_Code.DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace BillingOther.Accounts
{
    public partial class ProjectGroupMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl h4 = (HtmlGenericControl)Master.FindControl("lblHeader");
            h4.InnerText = "Project Group Master";
            if (!IsPostBack)
            {
                BindProjects();
            }
            GetAllProjectGroups();
        }

        public void BindProjects()
        {
            DataTable dt = new DALTracking().GetAllProjectByUserRights();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ddlProjectNumber.Items.Add(new ListItem(Convert.ToString(dt.Rows[i]["ProjectName"]), Convert.ToString(dt.Rows[i]["ProjectId"])));
                }
            }
            ddlProjectNumber.Items.Insert(0, new ListItem("Select"));
        }

        public void GetAllProjectGroups()
        {
            grdProjectGroup.DataSource = GetAllProjectGroup();
            grdProjectGroup.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            foreach (ListItem item in ddlProjectNumber.Items)
            {
                if (item.Selected)
                {
                    Hashtable htParam = new Hashtable();
                    htParam.Add("GroupNumber", txtGroupNumber.Text.Trim());
                    htParam.Add("ProjectID", item.Value);
                    htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
                    int ReturnValue = InsertProjectGroup(htParam);
                    dvError.Style.Add("display", "");
                    dvError.Attributes.Add("class", "alert alert-success background-success");
                    dvError.InnerHtml = "Configuration saved successfully.";              
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                }
            }
            GetAllProjectGroups();
        }

        #region Database
        public int InsertProjectGroup(Hashtable htParam)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_InsertProjectGroups");
            SQLHelper.AddParamToSQLCmd(cmd, "@GroupNumber", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, htParam["GroupNumber"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@ProjectID", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, htParam["ProjectID"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@AddedBy", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, htParam["AddedBy"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@ReturnValue", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.ReturnValue, null);
            SQLHelper.ExecuteNonQueryCmdBilling(cmd);

            int ReturnValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
            return ReturnValue;
        }

        public DataTable GetAllProjectGroup()
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetAllProjectGroups");
            DataTable dt = SQLHelper.ExecuteDataTableCmd_billing(cmd);
            return dt;
        }

        #endregion

        protected void grdProjectGroup_CustomUnboundColumnData(object sender, DevExpress.Web.ASPxGridViewColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Number")
            {
                e.Value = string.Format("{0}", e.ListSourceRowIndex + 1);
            }
        }
    }
}