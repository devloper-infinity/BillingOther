using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using DevExpress.Web;
using System.Drawing;
using System.Web.UI.HtmlControls;
using BillingOther.App_Code.BLL;

namespace BillingOther.Accounts
{
    public partial class VerifyCostMaster : System.Web.UI.Page
    {
        bllTracking bllMaster = new bllTracking();
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl h4 = (HtmlGenericControl)Master.FindControl("lblHeader");
            h4.InnerText = "Verify Project Price";
            if (!Page.IsPostBack)
            {
                bindgrid();
                bindmodifiedgrid();
            }
        }

        public void bindgrid()
        {
            DataTable dt = bllMaster.BindVerifyProject();
            ASPxReoprt.DataSource = dt;
            ASPxReoprt.DataBind();
        }

        public void bindmodifiedgrid()
        {
            DataTable dt = bllMaster.BindVerifyProjectForModifiedRates();
            ASPxGridView1.DataSource = dt;
            ASPxGridView1.DataBind();
        }

        protected void ASPxReoprt_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            try
            {
                string ProjectId = "";
                string DomainId = "";
                if (e.ButtonID == "CostDetails")
                {
                    ProjectId = ASPxReoprt.GetRowValues(e.VisibleIndex, "ProjectId").ToString();
                    DomainId = ASPxReoprt.GetRowValues(e.VisibleIndex, "DomainId").ToString();
                    //string Type = ASPxReoprt.GetRowValues(e.VisibleIndex, "Type").ToString();
                    //string ProjectName = ASPxReoprt.GetRowValues(e.VisibleIndex, "Project_Name").ToString();
                    if (ProjectId == "373" || ProjectId == "385" || ProjectId == "203" || ProjectId == "400" || ProjectId == "87" || DomainId == "36" || ProjectId == "426")
                        ASPxGridView.RedirectOnCallback("~/Accounts/VerifyProjectCost.aspx?ProjectId=" + ProjectId + "&DomainId=" + DomainId);
                    else if (DomainId == "19")
                        ASPxGridView.RedirectOnCallback("~/Accounts/VerifyProjectCostTyping.aspx?ProjectId=" + ProjectId + "&DomainId=" + DomainId);
                    else if (DomainId == "2")
                        ASPxGridView.RedirectOnCallback("~/Accounts/VerifyProjectCostFreight.aspx?ProjectId=" + ProjectId + "&DomainId=" + DomainId);
                    else
                        ASPxGridView.RedirectOnCallback("~/Accounts/VerifyProjectCost.aspx?ProjectId=" + ProjectId + "&DomainId=" + DomainId);
                }
            }
            catch { }
        }

        protected void ASPxReoprt_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Number")
            {
                e.Value = string.Format("{0}", e.ListSourceRowIndex + 1);
            }
        }

        protected void ASPxGridView1_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            try
            {
                string ProjectId = "";
                string DomainId = "";
                if (e.ButtonID == "ViewModifiedRates")
                {
                    ProjectId = ASPxGridView1.GetRowValues(e.VisibleIndex, "ProjectId").ToString();
                    //string Type = ASPxReoprt.GetRowValues(e.VisibleIndex, "Type").ToString();
                    //string ProjectName = ASPxReoprt.GetRowValues(e.VisibleIndex, "Project_Name").ToString();
                    DomainId = ASPxGridView1.GetRowValues(e.VisibleIndex, "DomainId").ToString();
                    if (ProjectId == "373" || ProjectId == "385" || ProjectId == "203" || ProjectId == "400" || ProjectId == "87" || DomainId == "36" || ProjectId == "426")
                        ASPxGridView.RedirectOnCallback("~/Accounts/VerifyProjectCost.aspx?ProjectId=" + ProjectId + "&DomainId=" + DomainId);
                    else if (DomainId == "19")
                        ASPxGridView.RedirectOnCallback("~/Accounts/VerifyProjectCostTyping.aspx?ProjectId=" + ProjectId + "&DomainId=" + DomainId);
                    else if (DomainId == "2")
                        ASPxGridView.RedirectOnCallback("~/Accounts/VerifyProjectCostFreight.aspx?ProjectId=" + ProjectId + "&DomainId=" + DomainId);
                    else
                        ASPxGridView.RedirectOnCallback("~/Accounts/VerifyProjectCost.aspx?ProjectId=" + ProjectId + "&DomainId=" + DomainId);
                }
            }
            catch { }

        }

        protected void ASPxGridView1_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Number")
            {
                e.Value = string.Format("{0}", e.ListSourceRowIndex + 1);
            }
        }
    }
}