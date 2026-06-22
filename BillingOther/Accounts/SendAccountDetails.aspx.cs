using BillingOther.App_Code.BLL;
using DevExpress.Web;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace BillingOther.Accounts
{
    public partial class SendAccountDetails : System.Web.UI.Page
    {

        bllTracking blltracking = new bllTracking();
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl h4 = (HtmlGenericControl)Master.FindControl("lblHeader");
            h4.InnerText = "Yet To Be Billed";

            if (!IsPostBack)
            {
                ddlPeriodBind();
                BindDomain();
            }
        }

        public void BindDomain()
        {
            DataTable dtdomain = new DataTable();
            dtdomain = blltracking.getalldomains(Convert.ToInt32(HttpContext.Current.User.Identity.Name));
            ddldomain.DataSource = dtdomain;
            ddldomain.DataValueField = "DomainID";
            ddldomain.DataTextField = "DomainName";
            ddldomain.DataBind();
            ddldomain.Items.Insert(0, new ListItem("Select"));
        }

        public void BindProjects(int DomainID)
        {
            DataTable dt = blltracking.GetAllProjectByDomainWise(DomainID, Convert.ToInt32(HttpContext.Current.User.Identity.Name));
            if (dt.Rows.Count > 0)
            {
                ddlProjects.DataSource = dt;
                ddlProjects.DataTextField = "ProjectName";
                ddlProjects.DataValueField = "ProjectId";
                ddlProjects.DataBind();
                ddlProjects.Items.Insert(0, new ListItem("Select"));
            }
            else
            {
                ddlProjects.Items.Clear();
            }

        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            string BillingPeriod = (ddlPeriod.SelectedItem.Text == "Select" ? "Select" : ddlPeriod.SelectedItem.Text);
            BindGridDomainWise(Convert.ToInt32(ddlProjects.SelectedValue), Convert.ToInt32(ddldomain.SelectedValue), BillingPeriod);

        }

        public void BindGridDomainWise(int ProjectId, int DomainID, string BillingPeriod)
        {
            DataTable dt = new DataTable();
            dt = blltracking.GetAllProjectSendToAccountsDetailsBasedonDomain(ProjectId, BillingPeriod, DomainID);
            if (dt.Rows.Count > 0)
            {
                grdBillingDetails.DataSource = dt;
                grdBillingDetails.DataBind();

                lblRecords.Text = "Total Records : " + dt.Rows.Count.ToString();
            }
            else
            {
                grdBillingDetails.DataSource = null;
                grdBillingDetails.DataBind();
                lblRecords.Text = "Total Records : " + dt.Rows.Count.ToString();
            }
        }

        protected void ddlPeriodBind()
        {
            DateTime now = DateTime.Now;
            DateTime fromDate = now.AddMonths(-5);
            DateTime fromDateNew = now.AddMonths(-5);
            ddlPeriod.Items.Clear();
            ddlPeriod.Items.Add("Select");
            for (int i = 0; i < 6; i++)
            {
                string Month = fromDate.ToString("MMM");
                var startDate = new DateTime(fromDate.Year, fromDate.Month, 1);
                string start = startDate.ToString("dd-MMM-yyyy");
                var endDate = (startDate.AddMonths(1).AddDays(-1));
                string End = endDate.ToString("dd-MMM-yyyy");

                string FirstHalf = Convert.ToString("01-" + Month + "-" + fromDate.Year + " ~ 15-" + Month + "-" + fromDate.Year);
                string secondHalf = Convert.ToString("16-" + Month + "-" + fromDate.Year + " ~ " + End);

                ddlPeriod.Items.Add(FirstHalf);
                ddlPeriod.Items.Add(secondHalf);
                fromDate = fromDateNew.AddMonths(i + 1);
            }
        }

        public void BindGridDomainWise(int DomainID)
        {
            DataTable dt = new DataTable();
            dt = blltracking.GetAllProjectSendToAccountsDetailsBasedonDomain(0, null, DomainID);
            if (dt.Rows.Count > 0)
            {
                grdBillingDetails.DataSource = dt;
                grdBillingDetails.DataBind();

                lblRecords.Text = "Total Records : " + dt.Rows.Count.ToString();
            }
            else
            {
                grdBillingDetails.DataSource = null;
                grdBillingDetails.DataBind();
                lblRecords.Text = "Total Records : " + dt.Rows.Count.ToString();
            }
        }

        public void BindGrid()
        {
            DataTable dt = new DataTable();
            //dt = blltracking.GetAllProjectSendToAccountsDetailsBasedonDomain(int.Parse(ddlProjects.SelectedValue), "Select",0);
            dt = blltracking.GetAllProjectSendToAccountsDetails(int.Parse(ddlProjects.SelectedValue), ddlPeriod.SelectedItem.ToString());

            grdBillingDetails.DataSource = dt;
            grdBillingDetails.DataBind();

            lblRecords.Text = "Total Records : " + dt.Rows.Count.ToString();
        }

        public void BindProjects()
        {
            ddlProjects.DataSource = blltracking.GetAllProjectByUserRights();
            ddlProjects.DataTextField = "ProjectName";
            ddlProjects.DataValueField = "ProjectId";
            ddlProjects.DataBind();
            ddlProjects.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
        }

        protected void grdBilling_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (e.ButtonID == "SendAccounts")
            {
                string Code = "";
                string BillingPeriod = "";
                string ProjectName = "";
                Code = grdBillingDetails.GetRowValues(e.VisibleIndex, "ProjectID").ToString();
                BillingPeriod = grdBillingDetails.GetRowValues(e.VisibleIndex, "BillingPeriod").ToString();
                ProjectName = grdBillingDetails.GetRowValues(e.VisibleIndex, "ProjectName").ToString();
                ASPxGridView.RedirectOnCallback("~/Accounts/SendToAccounts.aspx?ProjectID=" + Code + "&BillingPeriod=" + BillingPeriod + "&ProjectName=" + ProjectName);
            }
        }

        protected void grdBillingDetails_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
        {

            if (e.Column.FieldName == "Number")
            {
                e.Value = string.Format("{0}", e.ListSourceRowIndex + 1);
            }
        }

        protected void ddldomain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddldomain.SelectedValue != "Select")
            {
                BindProjects(Convert.ToInt32(ddldomain.SelectedValue));
                BindGridDomainWise(Convert.ToInt32(ddldomain.SelectedValue));
                Session["DomainID"] = ddldomain.SelectedValue;
            }
            else
            {
                Session["DomainID"] = null;
                ddlProjects.Items.Clear();
                //ddlFieldName.Items.Clear();
            }
        }
    }
}