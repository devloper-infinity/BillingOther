using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DevExpress.Web;
using System.Collections;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Drawing;
using System.Net.Mime;
using BillingOther.App_Code.DAL;
using BillingOther.App_Code.BLL;

namespace BillingOther.Accounts
{
    public partial class AllProjectBillingReport : System.Web.UI.Page
    {
        SendMail SendEmail = new SendMail();
        bllTracking blltracking = new bllTracking();
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl h4 = (HtmlGenericControl)Master.FindControl("lblHeader");
            h4.InnerText = "All Project Billing Report";
            if (!IsPostBack)
            {

                ddlPeriodBind();
                BindProjects();
                //if (ddlMonth.SelectedIndex == 0)
                //    BindGrid();
                //else
                //    BindGridSummaryMonthWise();

            }
            //string InvDetails = Convert.ToString(Session["InvDetails"]);
            //if (InvDetails != "")
            //{
            //    BindInvoiceRecievedDetails(InvDetails);
            //}
            int InvId = Convert.ToInt32(Session["InvoiceIdRemark"]);
            BindGrid();
            if (InvId != 0)
            {
                BindInvoiceRemarks(InvId);
            }

        }
        public void BindGrid()
        {

            DataTable dt = new DataTable();
            //dt = blltracking.GetAllProjectforClientApproved(int.Parse(ddlProjects.SelectedItem.Value), ddlDatePeriod.SelectedItem.ToString());
            dt = GetAllProjectSummary(txtFromDate.Text, txtToDate.Text);
            grdClientDetails.DataSource = dt;
            grdClientDetails.DataBind();
        }

        public DataTable GetAllProjectSummary(string FromDate, string ToDate)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "[WBT_usp_GetAllProjectSummary]");
            SQLHelper.AddParamToSQLCmd(cmd, "@FromMonth", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, FromDate);
            SQLHelper.AddParamToSQLCmd(cmd, "@ToMonth", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, ToDate);


            DataTable dt = SQLHelper.ExecuteDataTableCmd_billing(cmd);
            return dt;
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        public void BindProjects()
        {
            ddlProjects.DataSource = blltracking.GetAllProjectByUserRights();
            ddlProjects.DataTextField = "ProjectName";
            ddlProjects.DataValueField = "ProjectId";
            ddlProjects.DataBind();
            ddlProjects.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
            // ddlProjects.Items.Add(new System.Web.UI.WebControls.ListItem("Select", "0", true));
            // ddlProjects.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select"));
        }
        protected void grdBilling_DataBound(object sender, EventArgs e)
        {

        }

        protected void ddlPeriodBind()
        {
            DateTime now = DateTime.Now;
            DateTime fromDate = now.AddMonths(-5);
            DateTime fromDateNew = now.AddMonths(-5);
            ddlDatePeriod.Items.Clear();
            ddlDatePeriod.Items.Add("Select");
            for (int i = 0; i < 6; i++)
            {
                string Month = fromDate.ToString("MMM");
                var startDate = new DateTime(fromDate.Year, fromDate.Month, 1);
                string start = startDate.ToString("dd-MMM-yyyy");
                var endDate = (startDate.AddMonths(1).AddDays(-1));
                string End = endDate.ToString("dd-MMM-yyyy");

                string FirstHalf = Convert.ToString("01-" + Month + "-" + fromDate.Year + " ~ 15-" + Month + "-" + fromDate.Year);
                string secondHalf = Convert.ToString("16-" + Month + "-" + fromDate.Year + " ~ " + End);

                ddlDatePeriod.Items.Add(FirstHalf);
                ddlDatePeriod.Items.Add(secondHalf);

                fromDate = fromDateNew.AddMonths(i + 1);
            }

        }

        #region FinalSummary

        public DataTable GetInvoiceDetails(string ProjectNumber, string BillingPeriod)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "[IB_usp_GetInvoiceDetails_ForRead]");
            SQLHelper.AddParamToSQLCmd(cmd, "@ProjectNumber", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, ProjectNumber);
            SQLHelper.AddParamToSQLCmd(cmd, "@BillingPeriod", System.Data.SqlDbType.NVarChar, 200, System.Data.ParameterDirection.Input, BillingPeriod);
            DataTable dt = SQLHelper.ExecuteDataTableCmd_billing(cmd);
            return dt;
        }

        public void BindInvoiceRecievedDetails(string InvDetails)
        {
            string[] Details = InvDetails.Split('|');
            if (Details.Length > 1)
            {
                string ProjectNumber = Details[0];
                string BillingPeriod = Details[1];
                lblProjectPopup.Text = ProjectNumber;
                lblInvoiceNoPopup.Text = BillingPeriod;
                DataTable dt = new DataTable();
                //dt = blltracking.GetInvoiceDetails(InvoiceId);
                dt = GetInvoiceDetails(ProjectNumber, BillingPeriod);
                if (Convert.ToString(dt.Rows[0]["InvoiceRecievedByClientDate"]) != "")
                    dxInvrecieved.Date = Convert.ToDateTime(dt.Rows[0]["InvoiceRecievedByClientDate"]);
                else
                    dxInvrecieved.Text = "";
                if (Convert.ToString(dt.Rows[0]["CommunicationReciept"]) == "")
                    ddlReciep.SelectedIndex = 0;
                else
                    ddlReciep.SelectedValue = Convert.ToString(dt.Rows[0]["CommunicationReciept"]);
                txtInvRemark.Text = Convert.ToString(dt.Rows[0]["Remark"]);
                if (Convert.ToString(dt.Rows[0]["NoDisputeConfirmByClient"]) != "")
                    dxInvConfirm.Date = Convert.ToDateTime(dt.Rows[0]["NoDisputeConfirmByClient"]);
                else
                    dxInvConfirm.Text = "";
                if (Convert.ToString(dt.Rows[0]["InvCompleteDate"]) != "")
                    dxDate.Date = Convert.ToDateTime(dt.Rows[0]["InvCompleteDate"]);
                else
                    dxDate.Text = "";
            }
        }

        protected void btnUpdateInvoice_Click(object sender, EventArgs e)
        {
            string InvDetails = Convert.ToString(Session["InvDetails"]);
            string[] Details = InvDetails.Split('|');
            if (Details.Length > 1)
            {
                string ProjectNumber = Details[0];
                string BillingPeriod = Details[1];
                DataTable dt = GetInvoiceDetails(ProjectNumber, BillingPeriod);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        int InvoiceId = Convert.ToInt32(dt.Rows[0]["InvoiceID"]);
                        Hashtable htparam = new Hashtable();
                        htparam.Add("InvoiceID", InvoiceId);
                        if (dxInvrecieved.Text != "")
                        {
                            htparam.Add("InvoiceRecievedByClientDate", Convert.ToDateTime(dxInvrecieved.Text.Trim()));
                        }
                        htparam.Add("CommunicationReciept", Convert.ToString(ddlReciep.SelectedItem.Text));
                        htparam.Add("Remark", Convert.ToString(txtInvRemark.Text.Trim()));
                        if (dxInvConfirm.Text != "")
                        {
                            htparam.Add("NoDisputeConfirmByClient", Convert.ToString(dxInvConfirm.Text.Trim()));
                        }
                        if (dxDate.Text != "")
                        {
                            htparam.Add("InvCompleteDate", Convert.ToString(dxDate.Text.Trim()));
                        }
                        int result = blltracking.UpdateInvoiceDetails(htparam);
                        dvError.Style.Add("display", "");
                        lblError.Text = " Details added Successfully.";
                        dvError.Visible = true;

                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                        lblError.ForeColor = Color.Green;
                        //dxInvrecieved.Text = "";
                        //dxInvConfirm.Text = "";
                        //ddlReciep.SelectedIndex = 0;
                        //txtInvRemark.Text = "";
                        // dxDate.Text = "";
                        BindGrid();
                        BindInvoiceRecievedDetails(InvDetails);
                    }
                }
            }


        }

        protected void CallbackPanelOrderFinalSummary_Callback(object sender, CallbackEventArgsBase e)
        {
            Session["InvDetails"] = null;
            try
            {
                if (!string.IsNullOrEmpty(e.Parameter))
                {
                    string InvDetails = Convert.ToString(e.Parameter);
                    Session["InvDetails"] = InvDetails;
                    BindInvoiceRecievedDetails(InvDetails);
                    //  BindOrderDetails(e.Parameter);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void grdOrderFinalSummary_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
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

        protected void grdOrderFinalSummary_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {

        }
        protected void grdOrderFinalSummary_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {

        }
        protected void grdOrderFinalSummary_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

        }
        #endregion

        # region ExcelReport
        public void BindInvoiceRemarks(int InvoiceId)
        {
            DataTable dt = blltracking.GetInvoiceRemarkDetails(InvoiceId);
            grdTaxDetails.DataSource = dt;
            grdTaxDetails.DataBind();

        }
        protected void CallbackPanelExcelReport_Callback(object sender, CallbackEventArgsBase e)
        {
            try
            {

                if (!string.IsNullOrEmpty(e.Parameter))
                {
                    int InvoiceId = Convert.ToInt32(e.Parameter);
                    Session["InvoiceIdRemark"] = InvoiceId;
                    BindInvoiceRemarks(InvoiceId);
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnUpdateRemark_Click(object sender, EventArgs e)
        {
            if (txtRemark.Text.Trim() == "")
            {
                dvError.Style.Add("display", "");
                lblerrorRemark.Text = "Please enter remark";
                dvError.Visible = true;
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                lblerrorRemark.ForeColor = Color.Red;
                return;
            }

            Hashtable htparam = new Hashtable();
            htparam.Add("InvoiceID", Convert.ToInt32(Session["InvoiceIdRemark"]));
            htparam.Add("InvoiceRemark", Convert.ToString(txtRemark.Text.Trim()));
            htparam.Add("AddedBy", Convert.ToInt32(HttpContext.Current.User.Identity.Name));
            int result = blltracking.UpdateInvoiceRemark(htparam);
            BindInvoiceRemarks(Convert.ToInt32(Session["InvoiceIdRemark"]));
            lblerrorRemark.Text = "";
            txtRemark.Text = "";
        }

        protected void grdExcelReport_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            try
            {
                int index = -1;
                if (int.TryParse(e.Parameters, out index))
                    grdTaxDetails.SettingsEditing.Mode = (GridViewEditingMode)index;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
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
        protected void btnExportExcelReport_Click(object sender, EventArgs e)
        {
            try
            {
                this.grdExportExceldata.WriteXlsToResponse();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }


        #endregion

        protected void grdClientDetails_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
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



        protected void lnkDisp_Init(object sender, EventArgs e)
        {
            popupControl.HeaderText = "Download Report";

            ASPxHyperLink link = (ASPxHyperLink)sender;
            GridViewDataItemTemplateContainer templateContainer = (GridViewDataItemTemplateContainer)link.NamingContainer;
            int rowVisibleIndex = templateContainer.VisibleIndex;
            string InvoiceID = templateContainer.Grid.GetRowValues(rowVisibleIndex, "InvoiceID").ToString();
            string ProjectID = templateContainer.Grid.GetRowValues(rowVisibleIndex, "ProjectID").ToString();
            string ProjectName = templateContainer.Grid.GetRowValues(rowVisibleIndex, "ProjectName").ToString();
            string BillingPeriod = templateContainer.Grid.GetRowValues(rowVisibleIndex, "BillingPeriod").ToString();
            //string contentUrl = "Reportgeneration.aspx?InvoiceId=" + InvoiceID + "&ProjectID=" + ProjectID + "&BillingPeriod=" + BillingPeriod;
            //string contentUrl = "SummaryReportGeneration.aspx?ProjectGroup=" + ProjectName + "&BillingPeriod=" + BillingPeriod;
            string contentUrl = "Reportgeneration.aspx?InvoiceId=" + InvoiceID + "&ProjectID=" + ProjectID + "&BillingPeriod=" + BillingPeriod;
            link.NavigateUrl = "javascript:void(0);";
            link.ClientSideEvents.Click = string.Format("function(s, e) {{ OnMoreInfoClick('{0}'); }}", contentUrl);
            // link.Text = string.Format(searchBy);
            link.NavigateUrl = "javascript:close();";
        }

        protected void lnkExcel_Init(object sender, EventArgs e)
        {
            popupControl.HeaderText = "Download Report";

            ASPxHyperLink link = (ASPxHyperLink)sender;
            GridViewDataItemTemplateContainer templateContainer = (GridViewDataItemTemplateContainer)link.NamingContainer;
            int rowVisibleIndex = templateContainer.VisibleIndex;
            string InvoiceID = templateContainer.Grid.GetRowValues(rowVisibleIndex, "InvoiceID").ToString();
            string ProjectID = templateContainer.Grid.GetRowValues(rowVisibleIndex, "ProjectID").ToString();
            string BillingPeriod = templateContainer.Grid.GetRowValues(rowVisibleIndex, "BillingPeriod").ToString();
            string contentUrl = "ReportGenerationExcel.aspx?InvoiceId=" + InvoiceID + "&ProjectID=" + ProjectID + "&BillingPeriod=" + BillingPeriod;
            link.NavigateUrl = "javascript:void(0);";
            link.ClientSideEvents.Click = string.Format("function(s, e) {{ OnMoreInfoClick('{0}'); }}", contentUrl);
            // link.Text = string.Format(searchBy);
            link.NavigateUrl = "javascript:close();";
        }

        protected void ASPxCallbackPanel1_Callback(object sender, CallbackEventArgsBase e)
        {
            try
            {

                if (!string.IsNullOrEmpty(e.Parameter))
                {
                    string test = e.Parameter;
                    string[] authorsList = test.Split('|');
                    string GroupName = authorsList[0];
                    string billingPeriod = authorsList[1];
                    Session["GroupName"] = GroupName;
                    Session["billingPeriod"] = billingPeriod;
                    BindEmilTemplateForSendMail(GroupName, billingPeriod, 0);
                    //bindclients(invoiceidsendemail);
                }
                //if (!string.IsNullOrEmpty(e.Parameter))
                //{
                //    int InvoiceIdSendEmail = Convert.ToInt32(e.Parameter);
                //    Session["InvoiceIdEmailSent"] = InvoiceIdSendEmail;
                //    BindClients(InvoiceIdSendEmail);
                //}
            }
            catch (Exception ex)
            {

            }
        }

        public DataTable GetSummaryReportInvoice(string GroupName, string BillingPeriod)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "[WBT_usp_GetAllGroupwiseInvoiceNumber_ForOtherDomain]");
            SQLHelper.AddParamToSQLCmd(cmd, "@BillingPeriod", System.Data.SqlDbType.NVarChar, 1000, System.Data.ParameterDirection.Input, BillingPeriod);
            SQLHelper.AddParamToSQLCmd(cmd, "@GroupName", System.Data.SqlDbType.NVarChar, 1000, System.Data.ParameterDirection.Input, GroupName);

            DataTable dt = SQLHelper.ExecuteDataTableCmd_billing(cmd);
            return dt;
        }

        public DataTable GetClientDetails(string GroupName, string BillingPeriod)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetClientInformation_OtherDomain");
            SQLHelper.AddParamToSQLCmd(cmd, "@GroupName", System.Data.SqlDbType.NVarChar, 1000, System.Data.ParameterDirection.Input, GroupName);
            SQLHelper.AddParamToSQLCmd(cmd, "@BillingPeriod", System.Data.SqlDbType.NVarChar, 1000, System.Data.ParameterDirection.Input, BillingPeriod);

            DataTable dt = SQLHelper.ExecuteDataTableCmd_billing(cmd);
            return dt;
        }

        public void BindEmilTemplateForSendMail(string Groupname, string BillingPeriod, int ClientId)
        {
            string Header = "<html><head><meta content='text/html; charset=utf-8' http-equiv='Content-Type'><title></title><style type='text/css'>a:hover { text-decoration: none !important; }.header h1 {color: #fff !important; font: normal 33px Georgia, serif; margin: 0; padding: 0; line-height: 33px;}.header p {color: #dfa575; font: normal 11px Georgia, serif; margin: 0; padding: 0; line-height: 11px; letter-spacing: 2px}.content h2 {color:#8598a3 !important; font-weight: normal; margin: 0; padding: 0; font-style: italic; line-height: 30px; font-size: 30px;font-family: Georgia, serif; }.content p {color:#767676; font-weight: normal; margin: 0; padding: 0; line-height: 20px; font-size: 12px;font-family: Georgia, serif;}.content a {color: #d18648; text-decoration: none;}.footer p {padding: 0; font-size: 11px; color:#fff; margin: 0; font-family: Georgia, serif;}.footer a {color: #f7a766; text-decoration: none;}</style></head><body><table cellpadding='0' cellspacing='0' border='1'><tr><td ><table cellpadding='0' cellspacing='0' border='0' align='center' width='100%' style='font-family: Georgia, serif;' class='header'><tr><td bgcolor='#16a085' height='70' align='center'><h1 style='color: #fff; font: normal 25px Verdana; margin: 0; padding: 0; line-height: 33px;'>Infinity Invoice</h1></td></tr><tr><td style='font-size: 1px; height: 5px; line-height: 1px;' height='5'>&nbsp;</td></tr></table>";

            string Footer = "<table cellpadding='0' cellspacing='0' border='0' align='center' width='100%' style='font-family: Georgia, serif; line-height: 10px; margin-top:30px;' bgcolor='#16a085' class='footer'><tr><td bgcolor='#16a085'  align='center' style='padding: 15px 0 10px; font-size: 11px; color:#fff; margin: 0; line-height: 1.2;font-family: Verdana;' valign='top'><p style='padding: 0; font-size: 11px; color:#fff; margin: 0; font-family: Georgia, serif;'>!!! This is software generated e-mail...Please do not reply. !!</p></td></tr> </table></td></tr></table></body></html>";
            StringBuilder htmlBody_Service = new StringBuilder();
            DataTable DTClientDetails = GetSummaryReportInvoice(Groupname, BillingPeriod);
            DataTable dtClientInfo = GetClientDetails(Groupname, BillingPeriod);
            DataTable DtInvoice = new bllTracking().GetSummaryReportAttachments(Groupname, BillingPeriod);
            // string ClientEmail = Convert.ToString(DTClientDetails.Rows[0]["EmailID"]);
            string clientName = "";
            if (dtClientInfo != null)
            {
                if (dtClientInfo.Rows.Count > 0)
                {
                    clientName = Convert.ToString(dtClientInfo.Rows[0]["PAI_Contact_Person"]);
                }
            }
            try
            {
                clientName = clientName.Substring(0, clientName.IndexOf(" "));
            }
            catch { }
            htmlBody_Service.Append(Header);
            htmlBody_Service.Append("<table width=\"650px\" style='margin-left:10px;'><tr><td align=\"left\"><b>Hello " + clientName + ",</b></td></tr><tr>");
            htmlBody_Service.Append("<td align=\"left\">Good Morning!! <br /><br />Please find attached invoice# " + Convert.ToString(DTClientDetails.Rows[0]["InvNo"]) + " for your review.");
            htmlBody_Service.Append("<br /><br />Kindly, For all invoices related queries direct email to following.");
            //htmlBody_Service.Append("<br /><br />Just a note, regarding invoice related queries, please deal with only myself and Mahesh");
            htmlBody_Service.Append("<br /><br /><a href='mailTo:anita@Infinity-data.com'>anita@Infinity-data.com</a>");
            htmlBody_Service.Append("<br /><a href='mailTo:mdk@Infinity-data.com'>mdk@Infinity-data.com</a>");
            htmlBody_Service.Append("</td></tr></table><br /><br /><br /><br /><table width=\"650px\" style='margin-left:10px;'><tr><td align=\"left\">Thanks,<br />Anita Londhe<br />VP Controller<br />Infinity IPS<br /><a href='mailTo:anita@Infinity-data.com'>anita@Infinity-data.com</a><br /><a href='www.infinity-data.com'>www.infinity-data.com</a></td></tr> </table>");

            htmlBody_Service.Append("<table width=\"650px\"><tr><td align=\"left\"></td></tr></table>");
            htmlBody_Service.Append("<br /><span style='color:red; font-size:14px;'>**WE DO NOT ACCEPT OR REQUEST CHANGES TO WIRING INSTRUCTION VIA EMAIL - Always call to verify**</span>");
            htmlBody_Service.Append("<br /><span>**********************************************************************************************************</span>");
            htmlBody_Service.Append("<br /><br /><span style='font-size:11px;'>Disclaimer: The information contained in this e-mail and any attachments may be confidential or privileged under applicable law, or otherwise may be protected from disclosure to anyone other than the intended recipient(s). Any use, distribution, or copying of this e-mail, including any of its contents or attachments by any person other than the intended recipient, or for any purpose other than its intended use, is strictly prohibited. If you believe you have received this e-mail in error, please notify us by e-mail and permanently delete the e-mail and any attachments, and do not save, copy, disclose, or reply on any part of the information contained in this e-mail or its attachments</span>");

            htmlBody_Service.Append(Footer);
            dvEmailTemplate.InnerHtml = htmlBody_Service.ToString();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string GroupName = Session["GroupName"].ToString();
            string billingPeriod = Session["billingPeriod"].ToString();
            //  string InvId = Session["InvoiceIdEmailSent"].ToString();
            string strClientid = Convert.ToString(hfClient.Value); //ddlClientList.SelectedItem.Value;
            if (strClientid == "") { strClientid = "0"; }
            int result = UpdateInvoiceClient_OtherDomain(GroupName, billingPeriod);

            // string ClientId = "1";
            SendInvoiceCreationEmailForSummaryReport(GroupName, billingPeriod, 0);
            hfClient.Value = "";
            //BindClients(int.Parse(InvId));

            dvError.Style.Add("display", "");
            dvError.Visible = true;
            lblError.Text = "Email Sent successfully!";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            //txtProjectname.Focus();
            lblError.ForeColor = Color.Green;
            popupSendInvReport.ShowOnPageLoad = false;
            BindGrid();
        }

        public void SendInvoiceCreationEmailForSummaryReport(string Groupname, string BillingPeriod, int ClientId)
        {
            string Header = "<html><head><meta content='text/html; charset=utf-8' http-equiv='Content-Type'><title></title><style type='text/css'>a:hover { text-decoration: none !important; }.header h1 {color: #fff !important; font: normal 33px Georgia, serif; margin: 0; padding: 0; line-height: 33px;}.header p {color: #dfa575; font: normal 11px Georgia, serif; margin: 0; padding: 0; line-height: 11px; letter-spacing: 2px}.content h2 {color:#8598a3 !important; font-weight: normal; margin: 0; padding: 0; font-style: italic; line-height: 30px; font-size: 30px;font-family: Georgia, serif; }.content p {color:#767676; font-weight: normal; margin: 0; padding: 0; line-height: 20px; font-size: 12px;font-family: Georgia, serif;}.content a {color: #d18648; text-decoration: none;}.footer p {padding: 0; font-size: 11px; color:#fff; margin: 0; font-family: Georgia, serif;}.footer a {color: #f7a766; text-decoration: none;}</style></head><body><table cellpadding='0' cellspacing='0' border='1'><tr><td ><table cellpadding='0' cellspacing='0' border='0' align='center' width='100%' style='font-family: Georgia, serif;' class='header'><tr><td bgcolor='#16a085' height='70' align='center'><h1 style='color: #fff; font: normal 25px Verdana; margin: 0; padding: 0; line-height: 33px;'>Infinity Invoice</h1></td></tr><tr><td style='font-size: 1px; height: 5px; line-height: 1px;' height='5'>&nbsp;</td></tr></table>";

            string Footer = "<table cellpadding='0' cellspacing='0' border='0' align='center' width='100%' style='font-family: Georgia, serif; line-height: 10px; margin-top:30px;' bgcolor='#16a085' class='footer'><tr><td bgcolor='#16a085'  align='center' style='padding: 15px 0 10px; font-size: 11px; color:#fff; margin: 0; line-height: 1.2;font-family: Verdana;' valign='top'><p style='padding: 0; font-size: 11px; color:#fff; margin: 0; font-family: Georgia, serif;'>!!! This is software generated e-mail...Please do not reply. !!</p></td></tr> </table></td></tr></table></body></html>";
            StringBuilder htmlBody = new StringBuilder();
            StringBuilder htmlBody_Service = new StringBuilder();
            string ToAddress = "";
            string ToCC = "";
            string sub = "";
            string ToBCC = "";

            //string ToBCC = "s.swapnali@infinityinternationals.us";

            //DataTable dt = new bllLogin().GetUserInformation(AddedBy);

            DataTable DTClientDetails = GetSummaryReportInvoice(Groupname, BillingPeriod);
            DataTable dtClientInfo = GetClientDetails(Groupname, BillingPeriod);
            DataTable DtInvoice = new bllTracking().GetSummaryReportAttachments(Groupname, BillingPeriod);
            string ClientEmail = Convert.ToString(dtClientInfo.Rows[0]["PAI_Email_Id"]);
            string ToCCs = Convert.ToString(dtClientInfo.Rows[0]["CEC_CC"]);
            try
            {
                if (ToCCs.Contains(","))
                {
                    string[] ToCCStr = ToCCs.Split(',');
                    foreach (string cc in ToCCStr)
                    {
                        if (cc != "")
                        {
                            ToCC += cc + ".pointofmail.com,";
                        }
                    }
                }
                else
                {
                    ToCC = Convert.ToString(dtClientInfo.Rows[0]["CEC_CC"]);
                }
                ToCC = ToCC.Substring(0, ToCC.LastIndexOf(','));
            }
            catch { }
            string clientName = "";
            if (dtClientInfo != null)
            {
                if (dtClientInfo.Rows.Count > 0)
                {
                    clientName = Convert.ToString(dtClientInfo.Rows[0]["PAI_Contact_Person"]);
                }
            }
            try
            {
                clientName = clientName.Substring(0, clientName.IndexOf(" "));
            }
            catch { }

            htmlBody_Service.Append("<table width=\"650px\" style='margin-left:10px;'><tr><td align=\"left\"><b>Hello " + clientName + ",</b></td></tr><tr>");
            htmlBody_Service.Append("<td align=\"left\">Good Morning!! <br /><br />Please find attached invoice# " + Convert.ToString(DTClientDetails.Rows[0]["InvNo"]) + " for your review.");
            htmlBody_Service.Append("<br /><br />Kindly, For all invoices related queries direct email to following.");
            //htmlBody_Service.Append("<br /><br />Just a note, regarding invoice related queries, please deal with only myself and Mahesh");
            htmlBody_Service.Append("<br /><br /><a href='mailTo:anita@Infinity-data.com'>anita@Infinity-data.com</a>");
            htmlBody_Service.Append("<br /><a href='mailTo:mdk@Infinity-data.com'>mdk@Infinity-data.com</a>");
            htmlBody_Service.Append("</td></tr></table><br /><br /><br /><br /><table width=\"650px\" style='margin-left:10px;'><tr><td align=\"left\">Thanks,<br />Anita Londhe<br />VP Controller<br />Infinity IPS<br /><a href='mailTo:anita@Infinity-data.com'>anita@Infinity-data.com</a><br /><a href='www.infinity-data.com'>www.infinity-data.com</a></td></tr> </table>");

            htmlBody_Service.Append("<table width=\"650px\"><tr><td align=\"left\"></td></tr></table>");
            htmlBody_Service.Append("<br /><span style='color:red; font-size:14px;'>**WE DO NOT ACCEPT OR REQUEST CHANGES TO WIRING INSTRUCTION VIA EMAIL - Always call to verify**</span>");
            htmlBody_Service.Append("<br /><span>**********************************************************************************************************</span>");
            htmlBody_Service.Append("<br /><br /><span style='font-size:11px;'>Disclaimer: The information contained in this e-mail and any attachments may be confidential or privileged under applicable law, or otherwise may be protected from disclosure to anyone other than the intended recipient(s). Any use, distribution, or copying of this e-mail, including any of its contents or attachments by any person other than the intended recipient, or for any purpose other than its intended use, is strictly prohibited. If you believe you have received this e-mail in error, please notify us by e-mail and permanently delete the e-mail and any attachments, and do not save, copy, disclose, or reply on any part of the information contained in this e-mail or its attachments</span>");

            string path = "E:/EmailPages/HRMS_BillingSummaryReportEmail_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".html";
            FileStream fp = File.Create(path);
            fp.Close();
            using (StreamWriter w = new StreamWriter(path, true))
            {
                w.WriteLine(Header + htmlBody_Service + Footer); // Write the text
            }
            string Subject = "Infinity invoice " + Convert.ToString(DtInvoice.Rows[0]["Invoice_GroupNumber"]);
            ToAddress = ClientEmail + ".pointofmail.com"; //Convert.ToString(dt.Rows[0]["OfficialEmailID"]);
            //ToAddress = "alpana@infinitytitleinfo.com";
            //ToCC = "";
            //ToBCC = "n.nilkanth@infinityinternationals.us";
            if (ToAddress == "")
            {
                ToAddress = "anita@infinity-data.com.pointofmail.com";
            }
            StringBuilder body = new StringBuilder();
            body.Append(Header);
            body.Append(htmlBody_Service);
            body.Append(Footer);
            //sendMail("", Subject, body, "b.shubhangi@infinityinternationals.us");
            //sendMail("", Subject, body, "");

            string Attachment = System.Web.HttpContext.Current.Server.MapPath(DtInvoice.Rows[0]["PGA_AttachmentsPDF"].ToString());

            Hashtable htParam = new Hashtable();
            htParam.Add("EmailType", "");
            htParam.Add("To", Convert.ToString(ToAddress));
            htParam.Add("CC", ToCC);
            htParam.Add("BCC", ToBCC);
            htParam.Add("Subject", Subject);
            htParam.Add("Body", path);
            htParam.Add("Attachment", Attachment);
            htParam.Add("DraftNo", "1");
            int ReturnValue = new SendMail().InsertAutoEmailTracking(htParam);
        }

        public DataTable GetInvoiceNumber(string ProjectNumber, string BillingPeriod)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetInvocieNumber_QC");
            SQLHelper.AddParamToSQLCmd(cmd, "@ProjectGroup", System.Data.SqlDbType.NVarChar, 1000, System.Data.ParameterDirection.Input, ProjectNumber);
            SQLHelper.AddParamToSQLCmd(cmd, "@BillingPeriod", System.Data.SqlDbType.NVarChar, 1000, System.Data.ParameterDirection.Input, BillingPeriod);
            DataTable dt = SQLHelper.ExecuteDataTableCmd_billing(cmd);
            return dt;
        }



        public int UpdateInvoiceClient_OtherDomain(string GroupName, string BillingPeriod)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "IB_usp_UpdateInvoiceClientId_OtherDomain");
            SQLHelper.AddParamToSQLCmd(cmd, "@GroupName", System.Data.SqlDbType.NVarChar, 200, System.Data.ParameterDirection.Input, GroupName);
            SQLHelper.AddParamToSQLCmd(cmd, "@BillingPeriod", System.Data.SqlDbType.NVarChar, 200, System.Data.ParameterDirection.Input, BillingPeriod);

            SQLHelper.AddParamToSQLCmd(cmd, "@ReturnValue", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.ReturnValue, null);
            SQLHelper.ExecuteNonQueryCmdBilling(cmd);
            int ReturnValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
            cmd.Dispose();
            return ReturnValue;
        }
        public void BindClients(int InvoiceID)
        {
            //DataTable dt;

            //dt = blltracking.GetAllClientListProjectWise(InvoiceID);
            //ddlClientList.DataSource = dt;
            //ddlClientList.DataValueField = "ClientId";
            //ddlClientList.DataTextField = "ClientName";
            //ddlClientList.DataBind();
            //ddlClientList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
        }
        public void BindClientstest(int InvoiceID)
        {
            //DataTable dt;

            //dt = blltracking.GetAllClientListProjectWise(InvoiceID);
            //ddlClientList.DataSource = dt;
            //ddlClientList.DataValueField = "ClientId";
            //ddlClientList.DataTextField = "ClientName";
            //ddlClientList.DataBind();
            //ddlClientList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
        }
        protected void ASPxCallbackPanel2_Callback(object sender, CallbackEventArgsBase e)
        {
            if (!string.IsNullOrEmpty(e.Parameter))
            {
                int InvoiceIdTestEmail = Convert.ToInt32(e.Parameter);
                Session["InvoiceIdTestEmail"] = InvoiceIdTestEmail;
                BindClientstest(InvoiceIdTestEmail);
            }
            ASPxCallbackPanel2.JSProperties["cpWasSuccessful"] = "true";
        }

        protected void btnsendEmail_Click(object sender, EventArgs e)
        {
            string InvId = Session["InvoiceIdTestEmail"].ToString();
            string ClientId = "1";
            SendEmail.SendTestEmail(int.Parse(InvId), int.Parse(HttpContext.Current.User.Identity.Name.ToString()), int.Parse(ClientId), txttestEmailids.Text);
            dvError.Style.Add("display", "");
            dvError.Visible = true;
            lblError.Text = "Email Sent successfully!";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            //txtProjectname.Focus();
            lblError.ForeColor = Color.Green;
            popupSendTestEmail.ShowOnPageLoad = false;
        }

        protected void ASPxCallbackPanelViewEmailTemplate_Callback(object sender, CallbackEventArgsBase e)
        {
            if (!string.IsNullOrEmpty(e.Parameter))
            {
                int InvoiceIdPreviewEmail = Convert.ToInt32(e.Parameter);
                Session["InvoiceIdPreviewEmail"] = InvoiceIdPreviewEmail;
                BindEmailDetails(InvoiceIdPreviewEmail);
            }

        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            //string InvId = Session["InvoiceIdPreviewEmail"].ToString();
            //string ClientId = "1";
            //if (!Directory.Exists(Server.MapPath(@"~/EmailPages/")))
            //{
            //    Directory.CreateDirectory(Server.MapPath(@"~/EmailPages/"));
            //}
            //string strPath = Server.MapPath(@"~/EmailPages/");
            //string Path = SendEmail.ViewInvoiceEmailTemplate(int.Parse(InvId), int.Parse(HttpContext.Current.User.Identity.Name.ToString()), int.Parse(ClientId), strPath);

            //using (System.IO.StreamReader reader = new System.IO.StreamReader(Server.MapPath(@"~/EmailPages/" + Path)))
            //{
            //    dvHtml.InnerHtml = reader.ReadToEnd();
            //}

            ////string htmlContent = Convert.ToString(ReadHtmlFile(Server.MapPath(Path)));
            ////Response.ContentType = "text/HTML";
            ////Response.AppendHeader("Content-Disposition", "attachment; filename=test.html");
            ////Response.TransmitFile(Path);
            ////Response.End();
            ////ASPxPopupControl3.ShowOnPageLoad = false;

        }

        public void BindEmailDetails(int InvoiceIdPreviewEmail)
        {
            DataTable dt = blltracking.EmailDetailsByInvoiceID(InvoiceIdPreviewEmail);
            if (dt.Rows.Count > 0)
            {
                txtTo.Text = Convert.ToString(dt.Rows[0]["CEC_To"]);
                txtCC.Text = Convert.ToString(dt.Rows[0]["CEC_CC"]);
                txtBCC.Text = Convert.ToString(dt.Rows[0]["CEC_BCC"]);
                lblDateSent.Text = Convert.ToString(dt.Rows[0]["EmailSentDate"]);

                lnkAttachment.Text = Convert.ToString(dt.Rows[0]["InvoiceNumber"]);
                txtSubject.Text = "Infinity invoice " + Convert.ToString(dt.Rows[0]["InvoiceNumber"]);
                string InvId = Session["InvoiceIdPreviewEmail"].ToString();
                string ClientId = "1";
                if (!Directory.Exists(Server.MapPath(@"~/EmailPages/")))
                {
                    Directory.CreateDirectory(Server.MapPath(@"~/EmailPages/"));
                }
                string strPath = Server.MapPath(@"~/EmailPages/");
                string Path = SendEmail.ViewInvoiceEmailTemplate(int.Parse(InvId), int.Parse(HttpContext.Current.User.Identity.Name.ToString()), int.Parse(ClientId), strPath);

                using (System.IO.StreamReader reader = new System.IO.StreamReader(Server.MapPath(@"~/EmailPages/" + Path)))
                {
                    dvHtml.InnerHtml = reader.ReadToEnd();
                }
            }
        }

        protected void lnkAttachment_Click(object sender, EventArgs e)
        {
            int InvIdPreviewEmail = Convert.ToInt32(Session["InvoiceIdPreviewEmail"]);
            DataTable dt = blltracking.EmailDetailsByInvoiceID(InvIdPreviewEmail);
            string Filename = Convert.ToString(dt.Rows[0]["InvoiceNumber"]);
            string Invpath = Convert.ToString(dt.Rows[0]["Invpath"]);
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + Filename + ".pdf");
            // String.Format("attachment;filename={0}", "attachment.pdf"));
            Response.ContentType = "application/pdf";
            Response.TransmitFile(Server.MapPath(Invpath));
            Response.End();
        }

        protected void ddlClientList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Session["ClientID"] = ddlClientList.SelectedValue;
        }

        protected void grdForExport_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
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

        protected void btnShowFilter_Click(object sender, EventArgs e)
        {
            if (ddlMonth.SelectedIndex == 0)
                BindGrid();
            else
                BindGridSummaryMonthWise();
        }

        public void BindGridSummaryMonthWise()
        {
            DataTable dt = new DataTable();
            dt = GetAllProjectforClientApproved((ddlMonth.SelectedValue), ddlYear.SelectedValue);
            grdClientDetails.DataSource = dt;
            grdClientDetails.DataBind();
        }

        public DataTable GetAllProjectforClientApproved(string Month, string Year)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "IB_usp_GetAllProjectForClientApproval_Filter");
            SQLHelper.AddParamToSQLCmd(cmd, "@Month", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, Month);
            SQLHelper.AddParamToSQLCmd(cmd, "@Year", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, Year);


            DataTable dt = SQLHelper.ExecuteDataTableCmd_billing(cmd);
            return dt;
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            // DataTable dt = new DataTable();
            //// dt = GetAllProjectforClientApproved((ddlMonth.SelectedValue), ddlYear.SelectedValue);
            // dt = GetAllProjectSummary(txtFromDate.Text, txtToDate.Text);
            // grdForExport.DataSource = dt;
            // grdForExport.DataBind();
            grdClientDetails.Settings.ShowFooter = true;
            BindGrid();
            var exportOptions = new XlsxExportOptionsEx();
            exportOptions.ExportType = DevExpress.Export.ExportType.WYSIWYG;
            grdExport.WriteXlsxToResponse(exportOptions);
            grdClientDetails.Settings.ShowFooter = false;

        }

        protected void btnExportToPDF_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            BindGrid();
            //grdExport.WritePdfToResponse();
            using (MemoryStream ms = new MemoryStream())
            {
                PrintingSystem ps = new PrintingSystem();
                SetPictureWatermark(ps);
                PrintableComponentLink pcl = new PrintableComponentLink(ps);
                pcl.Component = grdExport;
                pcl.Margins.Left = pcl.Margins.Right = 50;
                pcl.Landscape = true;
                pcl.CreateDocument(false);
                pcl.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                pcl.ExportToPdf(ms);
                WriteResponsePdf(this.Response, ms.ToArray(), System.Net.Mime.DispositionTypeNames.Attachment.ToString());
            }
        }

        public void SetPictureWatermark(PrintingSystem ps)
        {
            // Create the picture watermark.
            Watermark pictureWatermark = new Watermark();

            // Set watermark options.
            pictureWatermark.Image = Bitmap.FromFile(Server.MapPath("~\\Images\\InfinityLogo.jpg"));
            pictureWatermark.ImageAlign = ContentAlignment.MiddleCenter;
            pictureWatermark.ImageTiling = false;
            pictureWatermark.ImageViewMode = ImageViewMode.Zoom;
            pictureWatermark.ImageTransparency = 240;
            pictureWatermark.ShowBehind = false;
            //pictureWatermark.PageRange = "2,4";
            // Set the watermark to a document.
            ps.Watermark.CopyFrom(pictureWatermark);
        }

        public void WriteResponsePdf(HttpResponse response, byte[] filearray, string type)
        {
            response.ClearContent();
            response.Buffer = true;
            response.Cache.SetCacheability(HttpCacheability.Private);
            response.ContentType = "application/pdf";
            ContentDisposition contentDisposition = new ContentDisposition();
            contentDisposition.FileName = "Billing Summary Report.pdf";
            contentDisposition.DispositionType = type;
            response.AddHeader("Content-Disposition", contentDisposition.ToString());
            response.BinaryWrite(filearray);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            try
            {
                response.End();
            }
            catch (System.Threading.ThreadAbortException)
            {
            }

        }

        public static void WriteResponseExcel(HttpResponse response, byte[] filearray, string type)
        {
            response.ClearContent();
            response.Buffer = true;
            response.Cache.SetCacheability(HttpCacheability.Private);
            response.ContentType = "application/octet-stream";
            ContentDisposition contentDisposition = new ContentDisposition();
            contentDisposition.FileName = "Billing Summary Report.xlsx";
            contentDisposition.DispositionType = type;
            response.AddHeader("Content-Disposition", contentDisposition.ToString());
            response.BinaryWrite(filearray);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            try
            {
                response.End();
            }
            catch (System.Threading.ThreadAbortException)
            {
            }

        }

        protected void lblOrderCount_Load(object sender, EventArgs e)
        {
            ASPxGridView grid = grdClientDetails;
            GridViewDataColumn col = grid.Columns["OrderCount"] as GridViewDataColumn;
            ASPxSummaryItem summary = grid.TotalSummary["OrderCount", DevExpress.Data.SummaryItemType.Sum];
            string text = summary.GetTotalFooterDisplayText(col, grid.GetTotalSummaryValue(summary));
            ASPxLabel label = (ASPxLabel)sender;
            label.Text = string.Format("{0}\r\n({1})", col.Caption, text);
        }

        protected void lblTotalAmount_Load(object sender, EventArgs e)
        {
            ASPxGridView grid = grdClientDetails;
            GridViewDataColumn col = grid.Columns["TotalAmount"] as GridViewDataColumn;
            ASPxSummaryItem summary = grid.TotalSummary["TotalAmount", DevExpress.Data.SummaryItemType.Sum];
            string text = summary.GetTotalFooterDisplayText(col, grid.GetTotalSummaryValue(summary));
            ASPxLabel label = (ASPxLabel)sender;
            label.Text = string.Format("{0}\r\n({1})", col.Caption, text);
        }
    }
}