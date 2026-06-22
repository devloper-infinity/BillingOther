using BillingOther.App_Code.DAL;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BillingOther.Accounts
{
    public partial class RateRevisionUpdates : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindInvoiceRemarks();
        }

        public void BindInvoiceRemarks()
        {
            DataTable dt = GetRateRevisionUpdates();
            grdTaxDetails.DataSource = dt;
            grdTaxDetails.DataBind();

        }

        public DataTable GetRateRevisionUpdates()
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetRateRevisionUpdates");
            DataTable dt = SQLHelper.ExecuteDataTableCmd_billing(cmd);
            return dt;
        }

        protected void grdExcelReport_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == "Number")
                {
                    e.Value = string.Format("{0}", e.ListSourceRowIndex + 1);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void btnExpoetToExcel_Click(object sender, EventArgs e)
        {
            grdExportExceldata.WriteXlsxToResponse();
        }
    }
}