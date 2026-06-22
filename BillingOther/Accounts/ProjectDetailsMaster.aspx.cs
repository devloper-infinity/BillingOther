using BillingOther.App_Code.DAL;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace BillingOther.Accounts
{
    public partial class ProjectDetailsMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl h4 = (HtmlGenericControl)Master.FindControl("lblHeader");
            h4.InnerText = "Project Details";
            BindProjectInformationGrid();
        }

        public void BindProjectInformationGrid()
        {
            grdNewProAppReq.DataSource = ViewAllProjectApp();
            grdNewProAppReq.DataBind();
        }

        protected void grdNewProAppReq_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            int index = -1;
            if (int.TryParse(e.Parameters, out index))
                grdNewProAppReq.SettingsEditing.Mode = (GridViewEditingMode)index;
        }

        protected void grdNewProAppReq_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Number")
            {
                e.Value = string.Format("{0}", e.ListSourceRowIndex + 1);
            }

        }
        protected void grdNewProAppReq_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            string PAI_ErpProjectID = "";
            if (e.ButtonID == "Edit")
            {
                PAI_ErpProjectID = grdNewProAppReq.GetRowValues(e.VisibleIndex, "PAI_ErpProjectID").ToString();
                string DomainId = grdNewProAppReq.GetRowValues(e.VisibleIndex, "PAI_DomainId").ToString();
                if (PAI_ErpProjectID == "395" || PAI_ErpProjectID == "87" || DomainId == "36" || PAI_ErpProjectID == "426")
                    ASPxGridView.RedirectOnCallback("~/Accounts/ProjectDetails.aspx?ERPProjectId=" + PAI_ErpProjectID);
                else if (DomainId == "19")
                {
                    ASPxGridView.RedirectOnCallback("~/Accounts/ProjectDetailsTyping.aspx?ERPProjectId=" + PAI_ErpProjectID);
                }
                else if (DomainId == "2")
                {
                    if (PAI_ErpProjectID == "203" || PAI_ErpProjectID == "400" || PAI_ErpProjectID == "385" || PAI_ErpProjectID == "373" || PAI_ErpProjectID == "352")
                        ASPxGridView.RedirectOnCallback("~/Accounts/ProjectDetails.aspx?ERPProjectId=" + PAI_ErpProjectID);
                    else
                        ASPxGridView.RedirectOnCallback("~/Accounts/ProjectDetailsFreight.aspx?ERPProjectId=" + PAI_ErpProjectID);
                }
                else
                {
                    //if (int.Parse(HttpContext.Current.User.Identity.Name.ToString()) == 5)
                    //    ASPxGridView.RedirectOnCallback("~/ProjectDetailsForBDM.aspx?ERPProjectId=" + PAI_ErpProjectID);
                    //else
                    ASPxGridView.RedirectOnCallback("~/Accounts/ProjectDetails.aspx?ERPProjectId=" + PAI_ErpProjectID);
                }
            }
        }


        #region Database
        public DataTable ViewAllProjectApp()
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "[usp_GetAllProjectDetails_BDM]");
            SQLHelper.AddParamToSQLCmd(cmd, "@EmployeeID", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.Input, int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
            DataTable dt = SQLHelper.ExecuteDataTableCmd_billing(cmd);
            return dt;
        }
        #endregion
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Accounts/ProjectDetails.aspx");
        }
    }
}