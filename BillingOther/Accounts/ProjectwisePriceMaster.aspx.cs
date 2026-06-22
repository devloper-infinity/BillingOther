using BillingOther.App_Code.BLL;
using BillingOther.App_Code.DAL;
using DevExpress.Web;
using DevExpress.XtraPrinting;
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
    public partial class ProjectwisePriceMaster : System.Web.UI.Page
    {
        int rowInGroupNumber = 1;
        bool isFirstDisplayedRow = true;
        private List<int> groupIndexes = new List<int>();
        bllTracking bllMaster = new bllTracking();
      
        public void BindDomain()
        {
            DataTable dtdomain = new DataTable();
            dtdomain = new bllTracking().getalldomains(Convert.ToInt32(HttpContext.Current.User.Identity.Name));
            ddlDomain.DataSource = dtdomain;
            ddlDomain.DataValueField = "DomainID";
            ddlDomain.DataTextField = "DomainName";
            ddlDomain.DataBind();
            ddlDomain.Items.Insert(0, new ListItem("Select"));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl h4 = (HtmlGenericControl)Master.FindControl("lblHeader");
            h4.InnerText = "Project wise Price Details";
            if (!IsPostBack)
            {
                BindDomain();
            }
            BindGrid();
        }

        public DataTable GetAllProjectBillingParameters(int DomainID)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "[usp_getallbillingParametersForCosting_ForAllProject]");
            SQLHelper.AddParamToSQLCmd(cmd, "@DomainID", System.Data.SqlDbType.Int, 10, System.Data.ParameterDirection.Input, DomainID);
            DataTable dt = SQLHelper.ExecuteDataTableCmd_billing(cmd);
            return dt;
        }

        public void BindGrid()
        {
            if (ddlDomain.SelectedIndex > 0)
            {
                DataTable dt = GetAllProjectBillingParameters(Convert.ToInt32(ddlDomain.SelectedValue));
                grdPricing.DataSource = dt;
                grdPricing.DataBind();

                grdPricing.GroupBy(grdPricing.Columns["ProjectName"]);
            }
        }

        protected void grdProject_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            try
            {
                if (e.ButtonID == "CostDetails")
                {
                    string ProjectId = grdProject.GetRowValues(e.VisibleIndex, "ProjectId").ToString();
                    string DomainId = grdProject.GetRowValues(e.VisibleIndex, "DomainId").ToString();
                    ASPxGridView.RedirectOnCallback("~/ProjectwisePriceDetails.aspx?ProjectId=" + ProjectId + "&DomainId=" + DomainId);
                }
            }
            catch { }
        }

        protected void grdProject_CustomUnboundColumnData(object sender, DevExpress.Web.ASPxGridViewColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Number")
            {
                e.Value = string.Format("{0}", e.ListSourceRowIndex + 1);
            }
        }

        protected void grdPricing_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Number")
            {
                e.Value = string.Format("{0}", e.ListSourceRowIndex + 1);
            }

        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            var exportOptions = new XlsExportOptionsEx();
            exportOptions.ExportType = DevExpress.Export.ExportType.WYSIWYG;
            this.gridExport.WriteXlsToResponse(exportOptions);
        }

        protected void grdPricing_CustomColumnDisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName != "Number")
                return;
            ASPxGridView g = sender as ASPxGridView;
            if (isFirstDisplayedRow)
            {
                rowInGroupNumber = e.VisibleRowIndex - GetParentGroupIndex(e.VisibleRowIndex);
                isFirstDisplayedRow = false;
            }
            else
            {
                if (IsRowIsFirstGroup(e.VisibleRowIndex))
                    rowInGroupNumber = 1;
                else
                    rowInGroupNumber++;
            }

            e.Value = rowInGroupNumber;
            e.DisplayText = rowInGroupNumber.ToString();
        }

        private void CollectGroupIndexes()
        {
            groupIndexes.Clear();
            for (int i = 0; i < grdPricing.VisibleRowCount; i++)
            {
                if (grdPricing.IsGroupRow(i))
                    groupIndexes.Add(i);
            }
        }

        private bool IsRowIsFirstGroup(int index)
        {
            return grdPricing.IsGroupRow(index - 1);
        }

        private int GetParentGroupIndex(int index)
        {
            return groupIndexes.FindLast(delegate (int i) { return i < index; });
        }

        protected void grdPricing_BeforeGetCallbackResult(object sender, EventArgs e)
        {
            CollectGroupIndexes();
        }
    }
}