using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Web;
using System.Drawing;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using BillingOther.App_Code.BLL;
using BillingOther.App_Code.DAL;

namespace BillingOther.Accounts
{
    public partial class ProjectDetails : System.Web.UI.Page
    {
        bllTracking bllMaster = new bllTracking();
        List<Record> list = new List<Record>();
        List<RecordCosting> listCosting = new List<RecordCosting>();
        List<RecordUW> listUW = new List<RecordUW>();
        List<RecordCostingUW> listCostingUW = new List<RecordCostingUW>();

        protected void Page_Load(object sender, EventArgs e)
        {
            int ProjectApproval_Id = Convert.ToInt32(Request.QueryString["ERPProjectId"]);
            HtmlGenericControl h4 = (HtmlGenericControl)Master.FindControl("lblHeader");
            h4.InnerText = "Cost Master";
            string ss = Convert.ToString(Session["formdate"]);

            if (Convert.ToString(Session["formdate"]) != "Select" && Convert.ToString(Session["formdate"]) != "" && Convert.ToInt32(Session["formdate"]) > 0)
            {
                BindParamsGrid(Convert.ToInt32(Session["formdate"]));
            }
            else
            {
                BindParamsGrid(4);
            }

            if (Convert.ToString(ddldomain.SelectedValue) != "Select" && Convert.ToString(ddldomain.SelectedValue) != "" && Convert.ToInt32(ddldomain.SelectedValue) > 0)
            {
                BindParamsGrid(Convert.ToInt32(ddldomain.SelectedValue));
            }
            else
            {
                BindParamsGrid(4);
            }
            if (!IsPostBack)
            {
                Session["ProjectId"] = "0";
                Session["formdate"] = "0";
                BindDomain();
                bindBDM();
                BindProjects();

                if (Convert.ToString(Request.Url).Contains("ERPProjectId"))
                {
                    dvSummary.Style.Add("display", "");
                    bindgrid();
                    GetAllInformationByProjectAprovalID();
                    GetInformationSalesonEdit();
                    btnsbmit.Text = "Update";
                    BindFTECosting(Convert.ToInt32(Request.QueryString["ERPProjectId"]));
                }
                else
                {
                    //Tab1.Style.Add("margin-top", "-70px");
                }
                if (Convert.ToString(Request.QueryString["ERPProjectId"]) != "" && Convert.ToString(Request.QueryString["ERPProjectId"]) != null)
                {
                    rdButtons.Style.Add("display", "none");
                    GetAllInformationSales();
                }
                else
                {
                    rdButtons.Style.Add("display", "");
                }
            }
            if (Convert.ToString(Request.QueryString["ERPProjectId"]) == "426" || Convert.ToString(ddldomain.SelectedValue) == "36" || Convert.ToString(ddldomain.SelectedValue) == "34" || Convert.ToString(ddldomain.SelectedValue) == "7" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "87" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "435" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "440" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "40" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "414" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "50" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "51" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "391" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "184" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "205" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "203" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "400" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "205" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "373" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "353" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "123" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "354" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "203" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "392" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "386" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "280" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "337" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "388" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "373" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "385" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "393" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "39" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "395" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "352" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "155")
            {
                if (Convert.ToString(Request.QueryString["ERPProjectId"]) == "353" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "155" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "440" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "435" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "123" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "354" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "392" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "386" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "280" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "393" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "434" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "442")
                {
                    tdRate.InnerHtml = "<b>Rate Per FTE : </b>";
                }
                else if (Convert.ToString(Request.QueryString["ERPProjectId"]) == "184" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "205" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "203" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "400" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "205" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "373" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "373" || Convert.ToString(Request.QueryString["ERPProjectId"]) == "385")
                {
                    tdRate.InnerHtml = "<b>Hourly Rate : </b>";
                }
                else
                {
                    tdRate.InnerHtml = "<b>Base Rate : </b>";
                }
                this.tbpnl3.Visible = false;
                this.tbpnlFTE.Visible = true;
                this.TabPanel1.Visible = false;
                if (Convert.ToString(Request.QueryString["ERPProjectId"]) == "39")
                {
                    tblOtherFTE.Style.Add("display", "none");
                    tbl861007.Style.Add("display", "");
                    tblWholeloan.Style.Add("display", "none");
                }
                else
                {
                    tblOtherFTE.Style.Add("display", "");
                    tbl861007.Style.Add("display", "none");
                    tblWholeloan.Style.Add("display", "none");
                }
            }
            else if (Convert.ToString(Request.QueryString["ERPProjectId"]) == "620")
            {
                this.tbpnl3.Visible = false;
                this.tbpnlFTE.Visible = true;
                this.TabPanel1.Visible = false;
                tblOtherFTE.Style.Add("display", "none");
                tbl861007.Style.Add("display", "none");
                tblWholeloan.Style.Add("display", "");
                grdCostWholeLoan.DataSource = GetCostingDetailsWHoleLoan();
                grdCostWholeLoan.DataBind();
            }
            else
            {
                this.tbpnl3.Visible = true;
                this.tbpnlFTE.Visible = false;
                this.TabPanel1.Visible = true;
            }

            //  this.tbpnl34.Visible = false;


            try
            {
                if (rdcardYes.Checked == true)
                {
                    rfvProjectName.Enabled = true;
                    rfvddlProjectName.Enabled = false;
                    rfvProjectProcess.Enabled = false;
                }
                else
                {
                    rfvProjectName.Enabled = false;
                    rfvddlProjectName.Enabled = true;
                    rfvProjectProcess.Enabled = true;
                }
                int start = grdBillingParams.PageIndex * grdBillingParams.SettingsPager.PageSize;
                int end = (grdBillingParams.PageIndex + 1) * grdBillingParams.SettingsPager.PageSize;
                GridViewDataColumn column1 = grdBillingParams.Columns["IBV_Comment"] as GridViewDataColumn;
                GridViewDataColumn column2 = grdBillingParams.Columns["IBV_Additional"] as GridViewDataColumn;
                GridViewDataColumn column3 = grdBillingParams.Columns["IBV_Remark"] as GridViewDataColumn;
                GridViewDataColumn column4 = grdBillingParams.Columns["IBV_ChargeType"] as GridViewDataColumn;
                GridViewDataColumn column5 = grdBillingParams.Columns["IBV_CommentFromBDM"] as GridViewDataColumn;
                for (int i = start; i < end; i++)
                {
                    try
                    {
                        DropDownList ddlCompany = (DropDownList)grdBillingParams.FindRowCellTemplateControl(i, column1, "ddlCompany");
                        DropDownList ddlLocation = (DropDownList)grdBillingParams.FindRowCellTemplateControl(i, column2, "ddlLocation");
                        TextBox txtRemark = (TextBox)grdBillingParams.FindRowCellTemplateControl(i, column3, "txtRemark");
                        TextBox txtCommentFromBDM = (TextBox)grdBillingParams.FindRowCellTemplateControl(i, column5, "txtCommentFromBDM");
                        DropDownList ddlChargeType = (DropDownList)grdBillingParams.FindRowCellTemplateControl(i, column4, "ddlChargeType");
                        int id = Convert.ToInt32(grdBillingParams.GetRowValues(i, grdBillingParams.KeyFieldName));
                        list.Add(new Record(id, ddlCompany.SelectedValue, ddlLocation.SelectedValue, txtRemark.Text, ddlChargeType.SelectedValue, txtCommentFromBDM.Text.Trim()));
                    }
                    catch
                    { }
                }

            }
            catch (Exception ex)
            {
                if (ex.Message != "Input string was not in a correct format.") { }
            }

            try
            {
                int start = grdCostingParameters.PageIndex * grdCostingParameters.SettingsPager.PageSize;
                int end = (grdCostingParameters.PageIndex + 1) * grdCostingParameters.SettingsPager.PageSize;
                GridViewDataColumn column1 = grdCostingParameters.Columns["IBV_Comment"] as GridViewDataColumn;
                GridViewDataColumn column2 = grdCostingParameters.Columns["IBV_Additional"] as GridViewDataColumn;
                GridViewDataColumn column3 = grdCostingParameters.Columns["IBV_Remark"] as GridViewDataColumn;
                GridViewDataColumn column4 = grdCostingParameters.Columns["IBV_ChargeType"] as GridViewDataColumn;

                for (int i = start; i < end; i++)
                {
                    try
                    {
                        DropDownList ddlCompany = (DropDownList)grdCostingParameters.FindRowCellTemplateControl(i, column1, "ddlCompany");
                        DropDownList ddlLocation = (DropDownList)grdCostingParameters.FindRowCellTemplateControl(i, column2, "ddlLocation");
                        TextBox txtRemark = (TextBox)grdCostingParameters.FindRowCellTemplateControl(i, column3, "txtRemark");
                        DropDownList ddlChargeType = (DropDownList)grdCostingParameters.FindRowCellTemplateControl(i, column4, "ddlChargeType");
                        int id = Convert.ToInt32(grdCostingParameters.GetRowValues(i, grdCostingParameters.KeyFieldName));
                        listCosting.Add(new RecordCosting(id, ddlCompany.SelectedValue, ddlLocation.SelectedValue, txtRemark.Text, ddlChargeType.SelectedValue));
                    }
                    catch
                    { }
                }

            }
            catch (Exception ex)
            {
                if (ex.Message != "Input string was not in a correct format.") { }
            }


        }

        public void BindFTECosting(int ProjectID)
        {
            if (ProjectID == 39)
            {
                DataTable dt = GetCostingDetails861(ProjectID);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        txtfirst1200Rate.Text = Convert.ToString(dt.Rows[0]["Rate"]);
                        txtAdditionalBallotsRate.Text = Convert.ToString(dt.Rows[0]["RateForOthers"]);
                        txtWireTransferCharges.Text = Convert.ToString(dt.Rows[0]["ConditionalValue"]);
                    }
                }
            }
            else
            {
                DataTable dt = GetCostingDetailsFTE(ProjectID);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        txtHourlyRate.Text = Convert.ToString(dt.Rows[0]["Rate"]);
                    }
                }
            }
        }

        public DataTable GetallbillingParameters(int DID)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_getallbillingParameters");
            SQLHelper.AddParamToSQLCmd(cmd, "@DomainId", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, DID);
            //SQLHelper.AddParamToSQLCmd(cmd, "@ProjectID", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, PID);
            DataTable dt = SQLHelper.ExecuteDataTableCmd_billing(cmd);
            return dt;
        }

        public void BindParamsGrid(int DomainID)
        {
        }

        public void bindgrid()
        {
            DataTable dt = bllMaster.BindVerifyProjectById(int.Parse(Request.QueryString["ERPProjectId"]));
            ASPxReoprt.DataSource = dt;
            ASPxReoprt.DataBind();
        }

        #region Freight



        #endregion

        #region BindMethods
        public void BindProjects()
        {

            ddlprojNo.DataSource = bllMaster.GetAllProjectByUserRights();
            ddlprojNo.DataTextField = "ProjectName";
            ddlprojNo.DataValueField = "ProjectName";
            ddlprojNo.DataBind();
            ddlprojNo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));

        }
        public void bindBDM()
        {
            DataTable dtbdm = new DataTable();
            dtbdm = bllMaster.getallMarketingEmployee();
            ddlBDM.DataSource = dtbdm;
            ddlBDM.DataValueField = "EmployeeID";
            ddlBDM.DataTextField = "Code";
            ddlBDM.DataBind();
            ddlBDM.Items.Insert(0, new ListItem("Select"));
        }
        public void BindDomain()
        {
            DataTable dtdomain = new DataTable();
            dtdomain = bllMaster.getalldomains(Convert.ToInt32(HttpContext.Current.User.Identity.Name));
            ddldomain.DataSource = dtdomain;
            ddldomain.DataValueField = "DomainID";
            ddldomain.DataTextField = "DomainName";
            ddldomain.DataBind();
            ddldomain.Items.Insert(0, new ListItem("Select"));
        }
        #endregion

        #region ClientInfo
        protected void btnsbmit_Click(object sender, EventArgs e)
        {
            int ProjectApproval_Id = Convert.ToInt32(Request.QueryString["ERPProjectId"]);
            bool NewProject = rdcardYes.Checked;
            if (btnsbmit.Text == "Submit")
            {
                Hashtable InsertprojectApproval = new Hashtable();
                InsertprojectApproval["Domain_Id"] = ddldomain.SelectedValue;
                InsertprojectApproval["Company_Id"] = "0";//txtcompnayId.Text;
                if (NewProject == true)
                {
                    InsertprojectApproval["ProjectName"] = txtProjectname.Text;
                    InsertprojectApproval["Type"] = NewProject;
                }
                else
                {
                    InsertprojectApproval["ProjectName"] = ddlProjects.SelectedItem.Text;
                    InsertprojectApproval["ProcessName"] = txtProcess.Text;
                    InsertprojectApproval["Type"] = NewProject;
                }
                InsertprojectApproval["Company_Name"] = txtcompanyname.Text;
                InsertprojectApproval["Contact_Person"] = txtContactPerson.Text;// ddlContactperson.SelectedItem.Text;
                InsertprojectApproval["Phonenumber"] = txtphonenumber.Text;// ddlphonenumber.SelectedItem.Text;
                InsertprojectApproval["Email-Id"] = txtemail.Text;// ddlEmails.SelectedItem.Text;
                InsertprojectApproval["Url"] = txturl.Text;
                InsertprojectApproval["Address"] = txtaddress.Text;
                InsertprojectApproval["Result"] = txtresult.Text;
                InsertprojectApproval["AddedBy"] = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                int ReturnValue = bllMaster.InsertProjectApprovalRequest(InsertprojectApproval);
                if (ReturnValue > 0)
                {
                    dvError.Style.Add("display", "");
                    dvError.Attributes.Add("class", "alert alert-success background-success");
                    dvError.InnerHtml = "Project information added successfully!";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    txtProjectname.Focus();
                    // BindProjectInformationGrid();
                    hdndomain.Value = ddldomain.SelectedValue;

                    if (ddldomain.SelectedValue == "19")
                        Response.Redirect("~/ProjectDetailsTyping.aspx?ERPProjectId=" + ReturnValue + "&DomainID=" + ddldomain.SelectedValue);
                    else if (ddldomain.SelectedValue == "2")
                        Response.Redirect("~/ProjectDetailsFreight.aspx?ERPProjectId=" + ReturnValue + "&DomainID=" + ddldomain.SelectedValue);
                    else
                        Response.Redirect("~/ProjectDetails.aspx?ERPProjectId=" + ReturnValue + "&DomainID=" + ddldomain.SelectedValue);
                    //clear();
                }
                else if (ReturnValue != -1)
                {
                    dvError.Style.Add("display", "");
                    dvError.Attributes.Add("class", "alert alert-warning background-warning");
                    dvError.InnerHtml = "Project information already exist!";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                }
                else if (ReturnValue == 0)
                {
                    dvError.Style.Add("display", "");
                    dvError.Attributes.Add("class", "alert alert-danger background-danger");
                    dvError.InnerHtml = "Error In Project Information Inserting!";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                }
            }
            else
            {
                Hashtable InsertprojectApproval = new Hashtable();
                InsertprojectApproval["ProjectApproval_Id"] = ProjectApproval_Id;
                InsertprojectApproval["Company_Id"] = "0";
                InsertprojectApproval["ProjectName"] = txtProjectname.Text;
                InsertprojectApproval["Company_Name"] = txtcompanyname.Text;
                InsertprojectApproval["Contact_Person"] = txtContactPerson.Text;
                InsertprojectApproval["Phonenumber"] = txtphonenumber.Text;
                InsertprojectApproval["Email-Id"] = txtemail.Text;
                InsertprojectApproval["Url"] = txturl.Text;
                InsertprojectApproval["Address"] = txtaddress.Text;
                InsertprojectApproval["Result"] = txtresult.Text;
                InsertprojectApproval["UpdatedBY"] = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                InsertprojectApproval["ERPProjectId"] = Convert.ToInt32(Request.QueryString["ERPProjectId"]);
                int ReturnValue = bllMaster.UpdateProjectApprovalRequest(InsertprojectApproval);
                if (ReturnValue == 0)
                {
                    dvError.Style.Add("display", "");
                    dvError.Attributes.Add("class", "alert alert-success background-success");
                    dvError.InnerHtml = "Project Information Updated successfully!";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    txtProjectname.Focus();
                }
            }
        }

        public void GetAllInformationByProjectAprovalID()
        {
            int ApprovalId = Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"]));

            DataTable dtpaid = bllMaster.GetbyProjectApproval_IdReq(ApprovalId);
            if (dtpaid.Rows.Count > 0)
            {
                ddldomain.SelectedValue = Convert.ToString(dtpaid.Rows[0]["PAI_DomainId"]);
                txtProjectname.Text = Convert.ToString(dtpaid.Rows[0]["PAI_Project_Name"]);
                if (Convert.ToString(dtpaid.Rows[0]["PAI_ProcessName"]) != "")
                {
                    ddlProjects.DataSource = bllMaster.GetAllProjectByUserRights();
                    ddlProjects.DataTextField = "ProjectName";
                    ddlProjects.DataValueField = "ProjectName";
                    ddlProjects.DataBind();
                    ddlProjects.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
                    trnewprocess.Style.Add("display", "");
                    ddlProjects.SelectedValue = Convert.ToString(dtpaid.Rows[0]["PAI_Project_Name"]) + "-" + Convert.ToString(dtpaid.Rows[0]["PAI_ProcessName"]);
                    tdprojtxt.Style.Add("display", "none");
                    txtProjectname.Style.Add("display", "none");
                    txtProcess.Text = Convert.ToString(dtpaid.Rows[0]["PAI_ProcessName"]);
                    ddlprojNo.SelectedValue = Convert.ToString(dtpaid.Rows[0]["PAI_Project_Name"]) + "-" + Convert.ToString(dtpaid.Rows[0]["PAI_ProcessName"]);
                }
                else
                {
                    trnewprocess.Style.Add("display", "none");
                    tdprojtxt.Style.Add("display", "");
                    txtProjectname.Style.Add("display", "");
                }
                ///Showing Grid based on Domain
                if (ddldomain.SelectedValue == "9") //UW
                {
                    this.tbpnl3.Visible = false;
                }
                else if (ddldomain.SelectedValue == "4") //QC
                {
                    this.tbpnl3.Visible = true;
                }
                else if (ddldomain.SelectedValue == "19") //Commitment
                {
                    this.tbpnl3.Visible = true;
                }
                else
                {
                    ///Code for Freight 
                }

                txtcompanyname.Text = Convert.ToString(dtpaid.Rows[0]["PAI_Company_Name"]);
                txtContactPerson.Text = Convert.ToString(dtpaid.Rows[0]["PAI_Contact_Person"]);
                txtphonenumber.Text = Convert.ToString(dtpaid.Rows[0]["PAI_Phone_Number"]);
                txtemail.Text = Convert.ToString(dtpaid.Rows[0]["PAI_Email_Id"]);
                txturl.Text = Convert.ToString(dtpaid.Rows[0]["PAI_Url"]);
                txtaddress.Text = Convert.ToString(dtpaid.Rows[0]["PAI_Address"]);
                txtresult.Text = Convert.ToString(dtpaid.Rows[0]["PAI_Remark"]);
            }
        }
        public void BindProjectInformationGrid()
        {
            DataTable dt = bllMaster.ViewAllProjectApp();
            grdNewProAppReq.DataSource = dt;
            grdNewProAppReq.DataBind();
            //clear();
        }
        public void clear()
        {
            txtContactPerson.Text = String.Empty;
            txtemail.Text = string.Empty;
            txtphonenumber.Text = string.Empty;
            txtProjectname.Text = " ";
            txtcompanyname.Text = " ";
            txturl.Text = " ";
            txtaddress.Text = " ";
            txtresult.Text = " ";

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
            string PAIId = "";
            if (e.ButtonID == "Edit")
            {
                PAIId = grdNewProAppReq.GetRowValues(e.VisibleIndex, "PAI_Id").ToString();
                ASPxGridView.RedirectOnCallback("~/Accounts/ProjectDetails.aspx?ProjectApproval_Id=" + PAIId);

            }

        }
        public DataTable GetProjectApprovalInformation(int ApprovalId)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_Billing_GetProjectApprovalInformation");
            SQLHelper.AddParamToSQLCmd(cmd, "@PAI_Id", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.Input, ApprovalId);
            DataTable dt = SQLHelper.ExecuteDataTableCmd_billing(cmd);
            return dt;
        }
        #endregion

        #region Sales
        public void GetAllInformationSales()
        {
            int ApprovalId = Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"]));
            try
            {
                if (txtProcess.Text != "")
                {
                    ddlprojNo.SelectedValue = txtProjectname.Text + "-" + txtProcess.Text;
                }
                else
                {
                    ddlprojNo.SelectedValue = Convert.ToString(txtProjectname.Text);
                }

            }
            catch { }
            DataTable dtpaid = GetProjectApprovalInformation(ApprovalId);
            if (dtpaid != null)
            {
                if (dtpaid.Rows.Count > 0)
                {

                    txtRateRevisionDate.Text = Convert.ToString(dtpaid.Rows[0]["RateRevisionDate"]);
                    ddlprojNo.SelectedValue = Convert.ToString(dtpaid.Rows[0]["IBS_ProjectNo"]);
                    txtProcessNameSales.Text = Convert.ToString(dtpaid.Rows[0]["IBS_ProcessName"]);
                    ddlBDM.SelectedValue = Convert.ToString(dtpaid.Rows[0]["IBS_BDM"]);
                    if (Convert.ToString(dtpaid.Rows[0]["IBS_RequestedDate"]) != "")
                    {
                        txtRequestedDate.Text = Convert.ToDateTime(dtpaid.Rows[0]["IBS_RequestedDate"]).ToString("dd-MMM-yyyy");
                    }
                    txtProjectScope.Text = Convert.ToString(dtpaid.Rows[0]["IBS_ScopeOfProject"]);
                    ddnda.SelectedValue = Convert.ToString(dtpaid.Rows[0]["IBS_NDASigned"]) == "" ? "Select" : (Convert.ToString(dtpaid.Rows[0]["IBS_NDASigned"]) == "True" ? "1" : "0");
                    if (ddnda.SelectedValue == "1")
                    {
                        if (Convert.ToString(dtpaid.Rows[0]["IBS_DateOfNDAAgreement"]) != "")
                        {
                            txtDateOfAgreement.Text = Convert.ToDateTime(dtpaid.Rows[0]["IBS_DateOfNDAAgreement"]).ToString("dd-MMM-yyyy");
                        }
                        if (Convert.ToString(dtpaid.Rows[0]["IBS_ExpirationDateofNDAAgreement"]) != "")
                        {
                            txtExpirationDateOfAgreement.Text = Convert.ToDateTime(dtpaid.Rows[0]["IBS_ExpirationDateofNDAAgreement"]).ToString("dd-MMM-yyyy");
                        }
                        ddlNDASignedByClient.SelectedValue = Convert.ToString(dtpaid.Rows[0]["IBS_NDASignedByClient"]) == "" ? "Select" : (Convert.ToString(dtpaid.Rows[0]["IBS_NDASignedByClient"]) == "True" ? "1" : "0");
                        ddlNDASignedByInfinity.SelectedValue = Convert.ToString(dtpaid.Rows[0]["IBS_NDASignedBYInfinity"]) == "" ? "Select" : (Convert.ToString(dtpaid.Rows[0]["IBS_NDASignedByInfinity"]) == "True" ? "1" : "0");
                    }
                    ddsla.SelectedValue = Convert.ToString(dtpaid.Rows[0]["IBS_SLASigned"]) == "" ? "Select" : (Convert.ToString(dtpaid.Rows[0]["IBS_SLASigned"]) == "True" ? "1" : "0");

                    if (ddsla.SelectedValue == "1")
                    {
                        if (Convert.ToString(dtpaid.Rows[0]["IBS_DateOfSLAAgreement"]) != "")
                        {
                            txtDateOfSLAAgreement.Text = Convert.ToDateTime(dtpaid.Rows[0]["IBS_DateOfSLAAgreement"]).ToString("dd-MMM-yyyy");
                        }
                        if (Convert.ToString(dtpaid.Rows[0]["IBS_ExpirationDateofSLAAgreement"]) != "")
                        {
                            txtExpirationDateOfSLAAgreement.Text = Convert.ToDateTime(dtpaid.Rows[0]["IBS_ExpirationDateofSLAAgreement"]).ToString("dd-MMM-yyyy");
                        }
                        ddlSLASignedByClient.SelectedValue = Convert.ToString(dtpaid.Rows[0]["IBS_SLASignedByClient"]) == "" ? "Select" : (Convert.ToString(dtpaid.Rows[0]["IBS_SLASignedByClient"]) == "True" ? "1" : "0");
                        ddlSLASignedByInfinity.SelectedValue = Convert.ToString(dtpaid.Rows[0]["IBS_SLASignedByInfinity"]) == "" ? "Select" : (Convert.ToString(dtpaid.Rows[0]["IBS_SLASignedByInfinity"]) == "True" ? "1" : "0");
                    }

                    ddlProjectStatus.SelectedValue = Convert.ToString(dtpaid.Rows[0]["IBS_ProjectStatus"]);
                    if (Convert.ToString(dtpaid.Rows[0]["IBS_ProjectStatus"]) == "Live Stopped")
                    {
                        trlivestopped.Style.Add("display", "");
                        txtStoppedDate.Text = Convert.ToString(dtpaid.Rows[0]["IBS_StoppedDate"]);
                        txtStoppedRemark.Text = Convert.ToString(dtpaid.Rows[0]["IBS_StoppedRemark"]);
                    }
                    else
                    {
                        trlivestopped.Style.Add("display", "none");
                    }
                    txtExpectedVolume.Text = Convert.ToString(dtpaid.Rows[0]["IBS_ExpectedVolume"]);
                    if (Convert.ToString(dtpaid.Rows[0]["IBS_ExpectedStartDate"]) != "")
                        txtExpectedStartDate.Text = Convert.ToDateTime(dtpaid.Rows[0]["IBS_ExpectedStartDate"]).ToString("dd-MMM-yyyy");
                    ddlProjectDuration.SelectedValue = Convert.ToString(dtpaid.Rows[0]["IBS_ProjectDuration"]);
                    txtSalesRemark.Text = Convert.ToString(dtpaid.Rows[0]["IBS_Remark"]);
                    hdnSLAPath.Value = Convert.ToString(dtpaid.Rows[0]["IBS_NDAFilePath"]);
                    hdnMSAPath.Value = Convert.ToString(dtpaid.Rows[0]["IBS_MSAFilePath"]);
                }
            }
        }
        protected void btnSalesInformation_Click(object sender, EventArgs e)
        {
            if (btnSalesInformation.Text == "Submit")
            {
                Hashtable htParam = new Hashtable();
                htParam.Add("ProjectApproval_Id", Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"])));
                htParam.Add("ProcessName", txtProcessNameSales.Text);
                htParam.Add("ProjectName", ddlprojNo.SelectedItem.Text);
                htParam.Add("BDM", Convert.ToInt32(ddlBDM.SelectedValue));
                if (txtRequestedDate.Text != "")
                {
                    htParam.Add("RequestedDate", txtRequestedDate.Text);
                }
                else
                {
                    htParam.Add("RequestedDate", DateTime.Now.ToString("dd-MMM-yyyy"));
                }
                htParam.Add("ScopeOfProject", txtProjectScope.Text);
                bool nda = ddnda.SelectedValue == "1" ? true : false;
                if (ddnda.SelectedIndex == 0)
                    htParam.Add("NDASigned", null);
                else if (ddnda.SelectedIndex == 1)
                {
                    htParam.Add("NDASigned", ddnda.SelectedValue);
                    if (txtDateOfAgreement.Text != "")
                    {
                        htParam.Add("DateOfNDAAgreement", txtDateOfAgreement.Text);
                    }
                    if (txtExpirationDateOfAgreement.Text != "")
                    {
                        htParam.Add("ExpirationDateofNDAAgreement", txtExpirationDateOfAgreement.Text);
                    }
                    htParam.Add("NDASignedByClient", ddlNDASignedByClient.SelectedValue == "1" ? true : false);
                    htParam.Add("NDASignedBYInfinity", ddlNDASignedByInfinity.SelectedValue == "1" ? true : false);
                }
                else
                {
                    htParam.Add("NDASigned", false);
                    //  htParam.Add("DateOfNDAAgreement", "");
                    //  htParam.Add("ExpirationDateofNDAAgreement", "");
                    htParam.Add("NDASignedByClient", null);
                    htParam.Add("NDASignedBYInfinity", null);
                }
                if (ddsla.SelectedIndex == 0)
                    htParam.Add("SLASigned", null);
                else if (ddsla.SelectedIndex == 1)
                {
                    htParam.Add("SLASigned", ddsla.SelectedValue);
                    if (txtDateOfSLAAgreement.Text != "")
                    {
                        htParam.Add("DateOfSLAAgreement", txtDateOfSLAAgreement.Text);
                    }
                    if (txtExpirationDateOfSLAAgreement.Text != "")
                    {
                        htParam.Add("ExpirationDateofSLAAgreement", txtExpirationDateOfSLAAgreement.Text);
                    }
                    htParam.Add("SLASignedByClient", ddlSLASignedByClient.SelectedValue == "1" ? true : false);
                    htParam.Add("SLASignedByInfinity", ddlSLASignedByInfinity.SelectedValue == "1" ? true : false);
                }
                else
                {
                    htParam.Add("SLASigned", false);
                    // htParam.Add("DateOfSLAAgreement", "");
                    // htParam.Add("ExpirationDateofSLAAgreement", "");
                    htParam.Add("SLASignedByClient", null);
                    htParam.Add("SLASignedByInfinity", null);
                }
                htParam.Add("ProjectStatus", ddlProjectStatus.SelectedValue);
                if (ddlProjectStatus.SelectedValue == "Live Stopped")
                {
                    htParam.Add("StoppedDate", txtStoppedDate.Text.Trim());
                    htParam.Add("StoppedRemark", txtStoppedRemark.Text.Trim());

                }

                htParam.Add("ExpectedVolume", txtExpectedVolume.Text);
                if (txtExpectedStartDate.Text != "")
                {
                    htParam.Add("ExpectedStartDate", txtExpectedStartDate.Text);
                }
                htParam.Add("ProjectDuration", ddlProjectDuration.SelectedValue);
                htParam.Add("Remark", txtSalesRemark.Text);
                htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
                htParam.Add("ERPProjectId", Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"])));
                string projName = ddlprojNo.SelectedItem.Text;
                if (!Directory.Exists(Server.MapPath(@"~/ProjectDocuments/" + projName + "/NDA")))
                {
                    Directory.CreateDirectory(Server.MapPath(@"~/ProjectDocuments/" + projName + "/NDA"));
                }
                try
                {
                    if (fpNDA.HasFile)
                    {
                        string fileName = "";
                        fileName = Path.GetFileName(fpNDA.FileName);// + DateTime.Now.ToString("ddMMyyyyhhmmss");
                        fileName = DateTime.Now.ToString("ddMMyyyyhhmmss") + fileName;
                        fpNDA.SaveAs(Server.MapPath(@"~/ProjectDocuments/" + projName + "/NDA" + "/" + fileName));
                        htParam["LogoPathNDA"] = Convert.ToString(@"~/ProjectDocuments/" + projName + "/NDA" + "/" + fileName);
                    }
                    else
                    {
                        htParam["LogoPathNDA"] = "";
                    }
                }
                catch { }

                if (!Directory.Exists(Server.MapPath(@"~/ProjectDocuments/" + projName + "/MSA")))
                {
                    Directory.CreateDirectory(Server.MapPath(@"~/ProjectDocuments/" + projName + "/MSA"));
                }
                try
                {
                    if (fpSLA.HasFile)
                    {
                        string fileName = "";
                        fileName = Path.GetFileName(fpSLA.FileName);// + DateTime.Now.ToString("ddMMyyyyhhmmss");
                        fileName = DateTime.Now.ToString("ddMMyyyyhhmmss") + fileName;
                        fpSLA.SaveAs(Server.MapPath(@"~/ProjectDocuments/" + projName + "/MSA" + "/" + fileName));
                        htParam["LogoPathMSA"] = Convert.ToString(@"~/ProjectDocuments/" + projName + "/MSA" + "/" + fileName);
                    }
                    else
                    {
                        htParam["LogoPathMSA"] = "";
                    }
                }
                catch { }
                htParam.Add("RateRevisionDate", txtRateRevisionDate.Text);
                int ReturnValue = bllMaster.InsertSalesInformation(htParam);
                if (ReturnValue > 0)
                {
                    dvError.Style.Add("display", "");
                    dvError.Attributes.Add("class", "alert alert-success background-success");
                    dvError.InnerHtml = "Sales Information Inserted successfully!";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    new SendMail().SendProjectCreationEmail(Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"])));
                    bindgrid();
                }
                else
                {
                    dvError.Style.Add("display", "");
                    dvError.Attributes.Add("class", "alert alert-danger background-danger");
                    dvError.InnerHtml = "Error occured while submitting sales information!";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                }
            }
            else
            {
                Hashtable htParam = new Hashtable();
                htParam.Add("ProjectApproval_Id", Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"])));
                htParam.Add("ProcessName", txtProcessNameSales.Text);
                htParam.Add("ProjectName", ddlprojNo.SelectedItem.Text);
                htParam.Add("BDM", Convert.ToInt32(ddlBDM.SelectedValue));
                if (txtRequestedDate.Text != "")
                {
                    htParam.Add("RequestedDate", txtRequestedDate.Text);
                }
                else
                {
                    htParam.Add("RequestedDate", DateTime.Now.ToString("dd-MMM-yyyy"));
                }
                htParam.Add("ScopeOfProject", txtProjectScope.Text);
                bool nda = ddnda.SelectedValue == "1" ? true : false;
                if (ddnda.SelectedIndex == 0)
                    htParam.Add("NDASigned", null);
                else if (ddnda.SelectedIndex == 1)
                {

                    htParam.Add("NDASigned", ddnda.SelectedValue);
                    if (txtDateOfAgreement.Text != "")
                    {
                        htParam.Add("DateOfNDAAgreement", txtDateOfAgreement.Text);
                    }
                    if (txtExpirationDateOfAgreement.Text != "")
                    {
                        htParam.Add("ExpirationDateofNDAAgreement", txtExpirationDateOfAgreement.Text);
                    }
                    htParam.Add("NDASignedByClient", ddlNDASignedByClient.SelectedValue == "1" ? true : false);
                    htParam.Add("NDASignedBYInfinity", ddlNDASignedByInfinity.SelectedValue == "1" ? true : false);
                }
                else
                {
                    htParam.Add("NDASigned", false);
                    //  htParam.Add("DateOfNDAAgreement", "");
                    //  htParam.Add("ExpirationDateofNDAAgreement", "");
                    htParam.Add("NDASignedByClient", null);
                    htParam.Add("NDASignedBYInfinity", null);
                }
                if (ddsla.SelectedIndex == 0)
                    htParam.Add("SLASigned", null);
                else if (ddsla.SelectedIndex == 1)
                {


                    htParam.Add("SLASigned", ddsla.SelectedValue);
                    if (txtDateOfSLAAgreement.Text != "")
                    {
                        htParam.Add("DateOfSLAAgreement", txtDateOfSLAAgreement.Text);
                    }
                    if (txtExpirationDateOfSLAAgreement.Text != "")
                    {
                        htParam.Add("ExpirationDateofSLAAgreement", txtExpirationDateOfSLAAgreement.Text);
                    }
                    htParam.Add("SLASignedByClient", ddlSLASignedByClient.SelectedValue == "1" ? true : false);
                    htParam.Add("SLASignedByInfinity", ddlSLASignedByInfinity.SelectedValue == "1" ? true : false);
                }
                else
                {
                    htParam.Add("SLASigned", false);
                    // htParam.Add("DateOfSLAAgreement", "");
                    // htParam.Add("ExpirationDateofSLAAgreement", "");
                    htParam.Add("SLASignedByClient", null);
                    htParam.Add("SLASignedByInfinity", null);
                }
                htParam.Add("ProjectStatus", ddlProjectStatus.SelectedValue);
                if (ddlProjectStatus.SelectedValue == "Live Stopped")
                {
                    htParam.Add("StoppedDate", txtStoppedDate.Text.Trim());
                    htParam.Add("StoppedRemark", txtStoppedRemark.Text.Trim());

                }
                htParam.Add("ExpectedVolume", txtExpectedVolume.Text);
                if (txtExpectedStartDate.Text != "")
                {
                    htParam.Add("ExpectedStartDate", txtExpectedStartDate.Text);
                }
                htParam.Add("ProjectDuration", ddlProjectDuration.SelectedValue);
                htParam.Add("Remark", txtSalesRemark.Text);
                htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
                htParam.Add("ERPProjectId", Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"])));
                string projName = ddlprojNo.SelectedItem.Text;
                if (!Directory.Exists(Server.MapPath(@"~/ProjectDocuments/" + projName + "/NDA")))
                {
                    Directory.CreateDirectory(Server.MapPath(@"~/ProjectDocuments/" + projName + "/NDA"));
                }
                try
                {
                    if (fpNDA.HasFile)
                    {
                        string fileName = "";
                        fileName = Path.GetFileName(fpNDA.FileName);// + DateTime.Now.ToString("ddMMyyyyhhmmss");
                        fileName = DateTime.Now.ToString("ddMMyyyyhhmmss") + fileName;
                        fpNDA.SaveAs(Server.MapPath(@"~/ProjectDocuments/" + projName + "/NDA" + "/" + fileName));
                        htParam["LogoPathNDA"] = Convert.ToString(@"~/ProjectDocuments/" + projName + "/NDA" + "/" + fileName);
                    }
                    else
                    {
                        htParam["LogoPathNDA"] = "";
                    }
                }
                catch { }

                if (!Directory.Exists(Server.MapPath(@"~/ProjectDocuments/" + projName + "/MSA")))
                {
                    Directory.CreateDirectory(Server.MapPath(@"~/ProjectDocuments/" + projName + "/MSA"));
                }
                try
                {
                    if (fpSLA.HasFile)
                    {
                        string fileName = "";
                        fileName = Path.GetFileName(fpSLA.FileName);// + DateTime.Now.ToString("ddMMyyyyhhmmss");
                        fileName = DateTime.Now.ToString("ddMMyyyyhhmmss") + fileName;
                        fpSLA.SaveAs(Server.MapPath(@"~/ProjectDocuments/" + projName + "/MSA" + "/" + fileName));
                        htParam["LogoPathMSA"] = Convert.ToString(@"~/ProjectDocuments/" + projName + "/MSA" + "/" + fileName);
                    }
                    else
                    {
                        htParam["LogoPathMSA"] = "";
                    }
                }
                catch { }
                htParam.Add("RateRevisionDate", txtRateRevisionDate.Text);
                int ReturnValue = bllMaster.UpdateSalesInformation(htParam);
                if (ReturnValue > 0)
                {
                    dvError.Style.Add("display", "");
                    dvError.Attributes.Add("class", "alert alert-success background-success");
                    dvError.InnerHtml = "Sales information updated successfully!";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    bindgrid();
                }
                else
                {
                    dvError.Style.Add("display", "");
                    dvError.Attributes.Add("class", "alert alert-danger background-danger");
                    dvError.InnerHtml = "Error occured while submitting sales information!";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                }
            }
        }
        public void GetInformationSalesonEdit()
        {
            int ApprovalId = Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"]));
            DataTable dtpaid = bllMaster.GetProjectApprovalInformation(ApprovalId);
            if (dtpaid != null)
            {
                if (dtpaid.Rows.Count > 0)
                {
                    try
                    {
                        txtRateRevisionDate.Text = Convert.ToString(dtpaid.Rows[0]["RateRevisionDate"]);
                    }
                    catch { }
                    ddlprojNo.SelectedValue = Convert.ToString(dtpaid.Rows[0]["IBS_ProjectNo"]);
                    ddlBDM.SelectedValue = Convert.ToString(dtpaid.Rows[0]["IBS_BDM"]);
                    if (Convert.ToString(dtpaid.Rows[0]["IBS_RequestedDate"]) != "")
                    {
                        txtRequestedDate.Text = Convert.ToDateTime(dtpaid.Rows[0]["IBS_RequestedDate"]).ToString("dd-MMM-yyyy");
                    }
                    txtProjectScope.Text = Convert.ToString(dtpaid.Rows[0]["IBS_ScopeOfProject"]);
                    ddnda.SelectedValue = Convert.ToString(dtpaid.Rows[0]["IBS_NDASigned"]) == "" ? "Select" : (Convert.ToString(dtpaid.Rows[0]["IBS_NDASigned"]) == "True" ? "1" : "0");
                    if (ddnda.SelectedValue == "1")
                    {
                        if (Convert.ToString(dtpaid.Rows[0]["IBS_DateOfNDAAgreement"]) != "")
                        {
                            txtDateOfAgreement.Text = Convert.ToDateTime(dtpaid.Rows[0]["IBS_DateOfNDAAgreement"]).ToString("dd-MMM-yyyy");
                        }
                        if (Convert.ToString(dtpaid.Rows[0]["IBS_ExpirationDateofNDAAgreement"]) != "")
                        {
                            txtExpirationDateOfAgreement.Text = Convert.ToDateTime(dtpaid.Rows[0]["IBS_ExpirationDateofNDAAgreement"]).ToString("dd-MMM-yyyy");
                        }
                        ddlNDASignedByClient.SelectedValue = Convert.ToString(dtpaid.Rows[0]["IBS_NDASignedByClient"]) == "" ? "Select" : (Convert.ToString(dtpaid.Rows[0]["IBS_NDASignedByClient"]) == "True" ? "1" : "0");
                        ddlNDASignedByInfinity.SelectedValue = Convert.ToString(dtpaid.Rows[0]["IBS_NDASignedBYInfinity"]) == "" ? "Select" : (Convert.ToString(dtpaid.Rows[0]["IBS_NDASignedByInfinity"]) == "True" ? "1" : "0");
                    }
                    ddsla.SelectedValue = Convert.ToString(dtpaid.Rows[0]["IBS_SLASigned"]) == "" ? "Select" : (Convert.ToString(dtpaid.Rows[0]["IBS_SLASigned"]) == "True" ? "1" : "0");

                    if (ddsla.SelectedValue == "1")
                    {
                        if (Convert.ToString(dtpaid.Rows[0]["IBS_DateOfSLAAgreement"]) != "")
                        {
                            txtDateOfSLAAgreement.Text = Convert.ToDateTime(dtpaid.Rows[0]["IBS_DateOfSLAAgreement"]).ToString("dd-MMM-yyyy");
                        }
                        if (Convert.ToString(dtpaid.Rows[0]["IBS_ExpirationDateofSLAAgreement"]) != "")
                        {
                            txtExpirationDateOfSLAAgreement.Text = Convert.ToDateTime(dtpaid.Rows[0]["IBS_ExpirationDateofSLAAgreement"]).ToString("dd-MMM-yyyy");
                        }
                        ddlSLASignedByClient.SelectedValue = Convert.ToString(dtpaid.Rows[0]["IBS_SLASignedByClient"]) == "" ? "Select" : (Convert.ToString(dtpaid.Rows[0]["IBS_SLASignedByClient"]) == "True" ? "1" : "0");
                        ddlSLASignedByInfinity.SelectedValue = Convert.ToString(dtpaid.Rows[0]["IBS_SLASignedByInfinity"]) == "" ? "Select" : (Convert.ToString(dtpaid.Rows[0]["IBS_SLASignedByInfinity"]) == "True" ? "1" : "0");
                    }

                    ddlProjectStatus.SelectedValue = Convert.ToString(dtpaid.Rows[0]["IBS_ProjectStatus"]);
                    if (Convert.ToString(dtpaid.Rows[0]["IBS_ProjectStatus"]) == "Live Stopped")
                    {
                        trlivestopped.Style.Add("display", "");
                        txtStoppedDate.Text = Convert.ToString(dtpaid.Rows[0]["IBS_StoppedDate"]);
                        txtStoppedRemark.Text = Convert.ToString(dtpaid.Rows[0]["IBS_StoppedRemark"]);
                    }
                    else
                    {
                        trlivestopped.Style.Add("display", "none");
                    }
                    txtExpectedVolume.Text = Convert.ToString(dtpaid.Rows[0]["IBS_ExpectedVolume"]);
                    if (Convert.ToString(dtpaid.Rows[0]["IBS_ExpectedStartDate"]) != "")
                        txtExpectedStartDate.Text = Convert.ToDateTime(dtpaid.Rows[0]["IBS_ExpectedStartDate"]).ToString("dd-MMM-yyyy");
                    ddlProjectDuration.SelectedValue = Convert.ToString(dtpaid.Rows[0]["IBS_ProjectDuration"]);
                    txtSalesRemark.Text = Convert.ToString(dtpaid.Rows[0]["IBS_Remark"]);
                    btnSalesInformation.Text = "Update";
                    hdnSLAPath.Value = Convert.ToString(dtpaid.Rows[0]["IBS_NDAFilePath"]);
                    hdnMSAPath.Value = Convert.ToString(dtpaid.Rows[0]["IBS_MSAFilePath"]);
                }
            }
        }
        protected void LinkButtonSLA_Click(object sender, System.EventArgs e)
        {
            if (hdnMSAPath.Value != "")
            {
                string Filename = Convert.ToString(hdnMSAPath.Value);
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "attachment;filename=" + Filename.Substring(Filename.LastIndexOf("\\") + 1));
                Response.TransmitFile(Filename);
                Response.End();
            }
            else
            {
                dvError.Style.Add("display", "");
                dvError.Attributes.Add("class", "alert alert-warning background-warning");
                dvError.InnerHtml = "No document is uploaded!";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            }

        }
        protected void LinkButtonNDA_Click(object sender, System.EventArgs e)
        {
            if (hdnSLAPath.Value != "")
            {
                string Filename = Convert.ToString(hdnSLAPath.Value);
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "attachment;filename=" + Filename.Substring(Filename.LastIndexOf("\\") + 1));
                Response.TransmitFile(Filename);
                Response.End();
            }
            else
            {
                dvError.Style.Add("display", "");
                dvError.Attributes.Add("class", "alert alert-warning background-warning");
                dvError.InnerHtml = "No document is uploaded!";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            }

        }
        #endregion

        #region Billing Parameters
        protected void ddlCompany_Init(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            GridViewDataItemTemplateContainer container = ddl.NamingContainer as GridViewDataItemTemplateContainer;

            DataTable dt = new DataTable();

            dt = GetDDLValues();
            ddl.Items.Insert(0, new ListItem("Select"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddl.Items.Insert(i + 1, new ListItem(Convert.ToString(dt.Rows[i]["Text"]), Convert.ToString(dt.Rows[i]["Text"])));
            }

            if (Convert.ToString(DataBinder.Eval(container.DataItem, "IBV_Comment")) == "0" || Convert.ToString(DataBinder.Eval(container.DataItem, "IBV_Comment")) == "")
                ddl.SelectedIndex = 0;
            else
                ddl.SelectedValue = DataBinder.Eval(container.DataItem, "IBV_Comment").ToString();

        }
        protected void ddlLocation_Init(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            GridViewDataItemTemplateContainer container = ddl.NamingContainer as GridViewDataItemTemplateContainer;
            DataTable dt = new DataTable();
            //ddl.Items.Clear();
            dt = GetDDLValues();
            ddl.Items.Insert(0, new ListItem("Select"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ddl.Items.Insert(i + 1, new ListItem(Convert.ToString(dt.Rows[i]["Text"]), Convert.ToString(dt.Rows[i]["Text"])));
            }

            if (Convert.ToString(DataBinder.Eval(container.DataItem, "IBV_Additional")) == "0" || Convert.ToString(DataBinder.Eval(container.DataItem, "IBV_Additional")) == "")
                ddl.SelectedIndex = 0;
            else
                ddl.SelectedValue = DataBinder.Eval(container.DataItem, "IBV_Additional").ToString();
            if (container.VisibleIndex == 0)
            {
                ddl.Enabled = false;
            }
        }
        protected void grdBillingParams_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Number")
            {
                e.Value = string.Format("{0}", e.ListSourceRowIndex + 1);
            }
        }

        public DataTable GetDDLValues()
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_getDDLValues");
            // SQLHelper.AddParamToSQLCmd(cmd, "@ApprovalId", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.Input, ApprovalId);
            DataTable dt = SQLHelper.ExecuteDataTableCmd_billing(cmd);
            return dt;
        }

        public class Record
        {
            int id;
            string comment;
            string additional;
            string remark;
            string chargetype;
            string commentfrombdm;

            public Record(int id, string comment, string additional, string remark, string chargetype, string commentfrombdm)
            {
                this.id = id;
                this.comment = comment;
                this.additional = additional;
                this.remark = remark;
                this.chargetype = chargetype;
                this.commentfrombdm = commentfrombdm;
            }
            public int Id { get { return this.id; } }
            public string Comment { get { return comment; } }
            public string Additional { get { return additional; } }
            public string Remark { get { return remark; } }
            public string ChargeType { get { return chargetype; } }
            public string CommentFromBdm { get { return commentfrombdm; } }

        }

        public class RecordUW
        {
            int id;
            string comment;
            string billingType;
            string remark;
            string chargetype;
            string commentfrombdm;
            string uWVerification;

            public RecordUW(int id, string comment, string BillingType, string remark, string chargetype, string commentfrombdm, string UWVerification)
            {
                this.id = id;
                this.comment = comment;
                this.billingType = BillingType;
                this.remark = remark;
                this.chargetype = chargetype;
                this.commentfrombdm = commentfrombdm;
                this.uWVerification = UWVerification;
            }
            public int Id { get { return this.id; } }
            public string Comment { get { return comment; } }
            public string BillingType { get { return billingType; } }
            public string Remark { get { return remark; } }
            public string ChargeType { get { return chargetype; } }
            public string CommentFromBdm { get { return commentfrombdm; } }
            public string UWVerification { get { return uWVerification; } }

        }

        public class RecordCosting
        {
            int id;
            string comment;
            string additional;
            string remark;
            string chargetype;

            public RecordCosting(int id, string comment, string additional, string remark, string chargetype)
            {
                this.id = id;
                this.comment = comment;
                this.additional = additional;
                this.remark = remark;
                this.chargetype = chargetype;
            }
            public int Id { get { return this.id; } }
            public string Comment { get { return comment; } }
            public string Additional { get { return additional; } }
            public string Remark { get { return remark; } }
            public string ChargeType { get { return chargetype; } }

        }

        public class RecordCostingUW
        {

            int id;
            string comment;
            string billingType;
            string remark;
            string chargetype;
            string commentfrombdm;
            string uWVerification;

            public RecordCostingUW(int id, string comment, string BillingType, string remark, string chargetype, string commentfrombdm, string UWVerification)
            {
                this.id = id;
                this.comment = comment;
                this.billingType = BillingType;
                this.remark = remark;
                this.chargetype = chargetype;
                this.commentfrombdm = commentfrombdm;
                this.uWVerification = UWVerification;
            }
            public int Id { get { return this.id; } }
            public string Comment { get { return comment; } }
            public string BillingType { get { return billingType; } }
            public string Remark { get { return remark; } }
            public string ChargeType { get { return chargetype; } }
            public string CommentFromBdm { get { return commentfrombdm; } }
            public string UWVerification { get { return uWVerification; } }



        }



        protected void grdBillingParams_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters == "update")
            {
                for (int i = 0; i < list.Count; i++)
                {
                    string Id = Convert.ToString(list[i].Id);
                    string Comment = Convert.ToString(list[i].Comment);
                    string Additional = Convert.ToString(list[i].Additional);
                    string Remark = Convert.ToString(list[i].Remark);
                    string ChargeType = Convert.ToString(list[i].ChargeType);
                    string CommentFromBdm = Convert.ToString(list[i].CommentFromBdm);

                    if (Comment == "Select")
                    {
                        ((ASPxGridView)sender).JSProperties["cp_message"] = "4";
                        ((ASPxGridView)sender).JSProperties["cp_Field"] = Convert.ToString(grdBillingParams.GetRowValues(i, "IBP_ParameterName"));
                        return;
                    }
                }
                for (int i = 0; i < list.Count; i++)
                {
                    string Id = Convert.ToString(list[i].Id);
                    string Comment = Convert.ToString(list[i].Comment);
                    string Additional = Convert.ToString(list[i].Additional);
                    string Remark = Convert.ToString(list[i].Remark);
                    string ChargeType = Convert.ToString(list[i].ChargeType);
                    string CommentFromBdm = Convert.ToString(list[i].CommentFromBdm);

                    if (Comment != "")
                    {
                        if (Comment == "Yes" && Remark == "")
                        {
                            ((ASPxGridView)sender).JSProperties["cp_message"] = "2";
                            ((ASPxGridView)sender).JSProperties["cp_Field"] = Convert.ToString(grdBillingParams.GetRowValues(i, "IBP_ParameterName"));
                            break;
                        }
                        if (Additional == "Yes" && ChargeType == "Select")
                        {
                            ((ASPxGridView)sender).JSProperties["cp_message"] = "3";
                            ((ASPxGridView)sender).JSProperties["cp_Field"] = Convert.ToString(grdBillingParams.GetRowValues(i, "IBP_ParameterName"));
                            break;
                        }
                        Hashtable Htparam = new Hashtable();
                        Htparam.Add("IBV_ParameterId", Convert.ToString(Id));
                        Htparam.Add("IBV_Comment", Convert.ToString(Comment));
                        Htparam.Add("IBV_Additional", Convert.ToString(Additional));
                        Htparam.Add("IBV_Remark", Convert.ToString(Remark));
                        if (i == 0)
                            Htparam.Add("IBV_ChargeType", Convert.ToString("Fix Amount"));
                        else
                            Htparam.Add("IBV_ChargeType", Convert.ToString(ChargeType));
                        Htparam.Add("IBV_CommentFromBDM", Convert.ToString(CommentFromBdm));
                        Htparam.Add("AddedBy", Convert.ToInt32(HttpContext.Current.User.Identity.Name));
                        Htparam.Add("ProjectId", Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"])));
                        int result = bllMaster.InsertBillParameter(Htparam);
                        if (result > 0)
                        {
                            ((ASPxGridView)sender).JSProperties["cp_message"] = "1";
                        }
                        else
                        {
                            ((ASPxGridView)sender).JSProperties["cp_message"] = "0";
                        }
                    }
                }

            }
        }
        protected void txtRemark_Init(object sender, System.EventArgs e)
        {
            TextBox ddl = sender as TextBox;
            GridViewDataItemTemplateContainer container = ddl.NamingContainer as GridViewDataItemTemplateContainer;
            DataTable dt = new DataTable();
            if (Convert.ToString(DataBinder.Eval(container.DataItem, "IBV_Remark")) == "0" || Convert.ToString(DataBinder.Eval(container.DataItem, "IBV_Remark")) == "")
                ddl.Text = "";
            else
                ddl.Text = DataBinder.Eval(container.DataItem, "IBV_Remark").ToString();

        }
        public DataTable GetBillingPArAM()
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_getallbillingParameters");
            SQLHelper.AddParamToSQLCmd(cmd, "@DomainId", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.Input, ddldomain.SelectedValue);

            SQLHelper.AddParamToSQLCmd(cmd, "@ProjectID", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.Input, Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"])));
            DataTable dt = SQLHelper.ExecuteDataTableCmd_billing(cmd);
            grdBillingParams.DataSource = dt;
            grdBillingParams.DataBind();
            return dt;
        }
        #endregion

        #region Unused Methods
        public void BindProjects(int DomainID)
        {
            DataTable dt = bllMaster.GetAllProjectByDomainWise(DomainID, Convert.ToInt32(HttpContext.Current.User.Identity.Name));
            if (dt.Rows.Count > 0)
            {
                ddlProjects.DataSource = dt;
                ddlProjects.DataTextField = "ProjectName";
                ddlProjects.DataValueField = "ProjectId";
                ddlProjects.DataBind();
                ddlProjects.Items.Insert(0, new ListItem("Select"));
            }
        }

        protected void ddldomain_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindProjects(Convert.ToInt32(ddldomain.SelectedValue));
            if (rdcardYes.Checked == false)
            {
                trnewprocess.Style.Add("display", "");
                tdprojtxt.Style.Add("display", "none");
                txtProjectname.Style.Add("display", "none");
            }
            //BindGridDomainWise();

            if (Convert.ToString(ddldomain.SelectedValue) != "Select" && Convert.ToString(ddldomain.SelectedValue) != "" && Convert.ToInt32(ddldomain.SelectedValue) > 0)
            {
                BindParamsGrid(Convert.ToInt32(ddldomain.SelectedValue));
            }
            else
            {
                BindParamsGrid(4);
            }
        }

        protected void grdAllFeedbcak_BeforePerformDataSelect(object sender, EventArgs e)
        {

        }

        #endregion

        protected void btnBack_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("~/Accounts/ProjectDetailsMaster.aspx");
        }

        protected void grdBillingParams_BeforePerformDataSelect(object sender, EventArgs e)
        {
            if (Convert.ToString(Request.QueryString["ERPProjectId"]) != "" && Convert.ToString(Request.QueryString["ERPProjectId"]) != null)
            {
                Session["ProjectId"] = Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"]));
            }
            else { Session["ProjectId"] = 0; }
            if (ddldomain.SelectedValue != "Select")
            {
                Session["formdate"] = ddldomain.SelectedValue;

            }
            else
            {
                Session["formdate"] = 4;
            }

            //Session["todate"] = txtToDate.Text.Trim();
        }

        protected void grdCostingParameters_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters == "databind")
                grdCostingParameters.DataBind();
            if (e.Parameters == "update")
            {
                //DeleteExisingCostingWithParameters(Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"])));
                string Unique = DateTime.Now.ToString("ddMMyyyyhhmmss");
                string Date = DateTime.Now.ToString("dd-MMM-yyyy");
                string FHARate = "";
                string MultiRate = "";
                string FHAMultiRate = "";
                string FHAMultiIncRate = "";
                string FHAIncRate = "";
                string MultiIncRate = "";
                string FHAUpdateRate = "";
                string MultiUpdateRate = "";
                string FHAMultiUpdateRate = "";
                for (int i = 0; i < grdCostingParameters.VisibleRowCount; i++)
                {
                    int IBP_Id = Convert.ToInt32(grdCostingParameters.GetRowValues(i, "IBP_Id"));
                    DropDownList ddl = (DropDownList)grdCostingParameters.FindRowCellTemplateControl(i, null, "ddlBillingColumns");
                    DropDownList ddlComment = (DropDownList)grdCostingParameters.FindRowCellTemplateControl(i, null, "ddlCompany");
                    DropDownList ddlAdditional = (DropDownList)grdCostingParameters.FindRowCellTemplateControl(i, null, "ddlLocation");
                    DropDownList ddlChargeType = (DropDownList)grdCostingParameters.FindRowCellTemplateControl(i, null, "ddlChargeType");
                    TextBox txtRemark = (TextBox)grdCostingParameters.FindRowCellTemplateControl(i, null, "txtCostingRemark");
                    string ddlValue = ddl.SelectedValue;
                    string Comment = ddlComment.SelectedValue;
                    string Additional = ddlAdditional.SelectedValue;
                    string Id = Convert.ToString(IBP_Id);
                    string ChargeType = Convert.ToString(ddlChargeType.SelectedValue);
                    string Remark = Convert.ToString(txtRemark.Text);
                    if (Comment == "Select")
                    {
                        ((ASPxGridView)sender).JSProperties["cp_message"] = "4";
                        ((ASPxGridView)sender).JSProperties["cp_Field"] = Convert.ToString(grdBillingParams.GetRowValues(i, "IBP_ParameterName"));
                        return;
                    }
                    if (Comment == "Yes" && Remark == "" && Additional == "Yes")
                    {
                        ((ASPxGridView)sender).JSProperties["cp_message"] = "2";
                        ((ASPxGridView)sender).JSProperties["cp_Field"] = Convert.ToString(grdBillingParams.GetRowValues(i, "IBP_ParameterName"));
                        return;
                    }
                    if (Comment == "Yes" && ddlValue == "Select")
                    {
                        ((ASPxGridView)sender).JSProperties["cp_message"] = "3";
                        ((ASPxGridView)sender).JSProperties["cp_Field"] = Convert.ToString(grdBillingParams.GetRowValues(i, "IBP_ParameterName"));
                        return;
                    }

                }
                for (int i = 0; i < grdCostingParameters.VisibleRowCount; i++)
                {
                    int IBP_Id = Convert.ToInt32(grdCostingParameters.GetRowValues(i, "IBP_Id"));
                    DropDownList ddl = (DropDownList)grdCostingParameters.FindRowCellTemplateControl(i, null, "ddlBillingColumns");
                    DropDownList ddlComment = (DropDownList)grdCostingParameters.FindRowCellTemplateControl(i, null, "ddlCompany");
                    DropDownList ddlAdditional = (DropDownList)grdCostingParameters.FindRowCellTemplateControl(i, null, "ddlLocation");
                    DropDownList ddlChargeType = (DropDownList)grdCostingParameters.FindRowCellTemplateControl(i, null, "ddlChargeType");
                    TextBox txtRemark = (TextBox)grdCostingParameters.FindRowCellTemplateControl(i, null, "txtCostingRemark");
                    string ddlValue = ddl.SelectedValue;
                    string Comment = ddlComment.SelectedValue;
                    string Additional = ddlAdditional.SelectedValue;
                    string Id = Convert.ToString(IBP_Id);
                    string ChargeType = Convert.ToString(ddlChargeType.SelectedValue);
                    string Remark = Convert.ToString(txtRemark.Text);
                    if (Comment != "Select")
                    {

                        Hashtable Htparam = new Hashtable();
                        Htparam.Add("IBV_ParameterId", Convert.ToString(Id));
                        Htparam.Add("IBV_Comment", Convert.ToString(Comment));
                        Htparam.Add("IBV_Additional", Convert.ToString(Additional));
                        Htparam.Add("IBV_Remark", Convert.ToString(Remark));
                        Htparam.Add("IBV_ChargeType", Convert.ToString(ChargeType));
                        Htparam.Add("AddedBy", Convert.ToInt32(HttpContext.Current.User.Identity.Name));
                        Htparam.Add("ProjectId", Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"])));
                        Htparam.Add("Unique", Convert.ToString(Unique));
                        Htparam.Add("Date", Convert.ToString(Date));
                        int result = InsertBillParameterCosting(Htparam);
                        if (result > 0)
                        {
                            ((ASPxGridView)sender).JSProperties["cp_message"] = "1";
                        }
                        else
                        {
                            ((ASPxGridView)sender).JSProperties["cp_message"] = "0";
                        }
                    }
                }
                for (int i = 0; i < grdCostingParameters.VisibleRowCount; i++)
                {
                    int IBP_Id = Convert.ToInt32(grdCostingParameters.GetRowValues(i, "IBP_Id"));
                    DropDownList ddlComment = (DropDownList)grdCostingParameters.FindRowCellTemplateControl(i, null, "ddlCompany");
                    DropDownList ddl = (DropDownList)grdCostingParameters.FindRowCellTemplateControl(i, null, "ddlBillingColumns");
                    DropDownList ddlChargeType = (DropDownList)grdCostingParameters.FindRowCellTemplateControl(i, null, "ddlChargeType");
                    string IsApplicable = ddlComment.SelectedValue;
                    TextBox txtRemark = (TextBox)grdCostingParameters.FindRowCellTemplateControl(i, null, "txtCostingRemark");
                    string BaseRate = ((TextBox)grdCostingParameters.FindRowCellTemplateControl(0, null, "txtCostingRemark")).Text.Trim();
                    if (BaseRate == "")
                        BaseRate = "0";

                    string UpdateBaseRate = "0";
                    try
                    {
                        UpdateBaseRate = ((TextBox)grdCostingParameters.FindRowCellTemplateControl(4, null, "txtCostingRemark")).Text.Trim();
                        if (UpdateBaseRate == "")
                            UpdateBaseRate = "0";
                    }
                    catch
                    {
                        UpdateBaseRate = "0";
                    }

                    try
                    {
                        FHARate = ((TextBox)grdCostingParameters.FindRowCellTemplateControl(11, null, "txtCostingRemark")).Text.Trim();
                        if (FHARate == "")
                            FHARate = "0";
                    }
                    catch
                    {
                        FHARate = "0";
                    }

                    try
                    {
                        MultiRate = ((TextBox)grdCostingParameters.FindRowCellTemplateControl(15, null, "txtCostingRemark")).Text.Trim();
                        if (MultiRate == "")
                            MultiRate = "0";
                    }
                    catch
                    {
                        MultiRate = "0";
                    }

                    try
                    {
                        FHAMultiUpdateRate = ((TextBox)grdCostingParameters.FindRowCellTemplateControl(9, null, "txtCostingRemark")).Text.Trim();
                        if (FHAMultiUpdateRate == "")
                            FHAMultiUpdateRate = "0";
                    }
                    catch
                    {
                        FHAMultiUpdateRate = "0";
                    }
                    try
                    {
                        FHAUpdateRate = ((TextBox)grdCostingParameters.FindRowCellTemplateControl(13, null, "txtCostingRemark")).Text.Trim();
                        if (FHAUpdateRate == "")
                            FHAUpdateRate = "0";
                    }
                    catch
                    {
                        FHAUpdateRate = "0";
                    }

                    try
                    {
                        MultiUpdateRate = ((TextBox)grdCostingParameters.FindRowCellTemplateControl(17, null, "txtCostingRemark")).Text.Trim();
                        if (MultiUpdateRate == "")
                            MultiUpdateRate = "0";
                    }
                    catch
                    {
                        MultiUpdateRate = "0";
                    }

                    string ChargeType = ddlChargeType.SelectedValue;
                    string ddlValue = ddl.SelectedValue;
                    if (IsApplicable != "Select")
                    {
                        //if (ChargeType != "Select")
                        {
                            //if (ddlValue != "Select")
                            {
                                string CostingRemark = Convert.ToString(txtRemark.Text);
                                Hashtable htParam = new Hashtable();
                                htParam.Add("ProjectId", Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"])));
                                htParam.Add("IBP_Id", IBP_Id);
                                htParam.Add("CostingColumn", ddlValue);
                                if (Convert.ToString(ChargeType) == "Fix Amount")
                                {
                                    htParam.Add("RemarkCost", Convert.ToString(CostingRemark));
                                    //FHAIncRate = Convert.ToString(Convert.ToDouble(CostingRemark));
                                    //MultiIncRate = Convert.ToString(Convert.ToDouble(CostingRemark));
                                    //FHARate = Convert.ToString(Convert.ToDouble(CostingRemark));
                                    //MultiRate = Convert.ToString(Convert.ToDouble(CostingRemark));
                                }
                                else if (Convert.ToString(ChargeType) == "Incremental")
                                {
                                    if (IBP_Id == 6)
                                        htParam.Add("RemarkCost", Convert.ToString(Convert.ToDouble(CostingRemark) + Convert.ToDouble(UpdateBaseRate)));
                                    else if (IBP_Id == 8)
                                    {
                                        FHAIncRate = Convert.ToString(Convert.ToDouble(CostingRemark) + Convert.ToDouble(BaseRate));
                                        htParam.Add("RemarkCost", Convert.ToString(Convert.ToDouble(CostingRemark) + Convert.ToDouble(BaseRate)));
                                    }
                                    else if (IBP_Id == 29)
                                    {
                                        FHAMultiIncRate = Convert.ToString(Convert.ToDouble(CostingRemark) + Convert.ToDouble(BaseRate));
                                        htParam.Add("RemarkCost", Convert.ToString(Convert.ToDouble(CostingRemark) + Convert.ToDouble(BaseRate)));
                                    }
                                    else if (IBP_Id == 23)
                                        htParam.Add("RemarkCost", Convert.ToString(Convert.ToDouble(CostingRemark) + Convert.ToDouble(FHAIncRate)));
                                    else if (IBP_Id == 31)
                                        htParam.Add("RemarkCost", Convert.ToString(Convert.ToDouble(CostingRemark) + Convert.ToDouble(FHAMultiIncRate)));
                                    else if (IBP_Id == 9)
                                    {
                                        MultiIncRate = Convert.ToString(Convert.ToDouble(CostingRemark) + Convert.ToDouble(BaseRate));
                                        htParam.Add("RemarkCost", Convert.ToString(Convert.ToDouble(CostingRemark) + Convert.ToDouble(BaseRate)));
                                    }
                                    else if (IBP_Id == 24)
                                        htParam.Add("RemarkCost", Convert.ToString(Convert.ToDouble(CostingRemark) + Convert.ToDouble(MultiIncRate)));
                                    else if (IBP_Id == 26)
                                        htParam.Add("RemarkCost", Convert.ToString(Convert.ToDouble(CostingRemark) + Convert.ToDouble(FHAUpdateRate)));
                                    else if (IBP_Id == 28)
                                        htParam.Add("RemarkCost", Convert.ToString(Convert.ToDouble(CostingRemark) + Convert.ToDouble(MultiUpdateRate)));
                                    else if (IBP_Id == 33)
                                        htParam.Add("RemarkCost", Convert.ToString(Convert.ToDouble(CostingRemark) + Convert.ToDouble(FHAMultiUpdateRate)));
                                    else
                                        htParam.Add("RemarkCost", Convert.ToString(Convert.ToDouble(CostingRemark) + Convert.ToDouble(BaseRate)));
                                }
                                else if (Convert.ToString(ChargeType) == "Percentage")
                                {
                                    if (IBP_Id == 6)
                                        htParam.Add("RemarkCost", Convert.ToString(Convert.ToDouble((Convert.ToDouble(UpdateBaseRate) * Convert.ToDouble(CostingRemark)) / 100) + Convert.ToDouble(UpdateBaseRate)));
                                    else if (IBP_Id == 8)
                                    {
                                        FHARate = Convert.ToString(Convert.ToDouble((Convert.ToDouble(BaseRate) * Convert.ToDouble(CostingRemark)) / 100) + Convert.ToDouble(BaseRate));
                                        htParam.Add("RemarkCost", Convert.ToString(Convert.ToDouble((Convert.ToDouble(BaseRate) * Convert.ToDouble(CostingRemark)) / 100) + Convert.ToDouble(BaseRate)));
                                    }
                                    else if (IBP_Id == 9)
                                    {
                                        MultiRate = Convert.ToString(Convert.ToDouble((Convert.ToDouble(BaseRate) * Convert.ToDouble(CostingRemark)) / 100) + Convert.ToDouble(BaseRate));
                                        htParam.Add("RemarkCost", Convert.ToString(Convert.ToDouble((Convert.ToDouble(BaseRate) * Convert.ToDouble(CostingRemark)) / 100) + Convert.ToDouble(BaseRate)));
                                    }
                                    else if (IBP_Id == 29)
                                    {
                                        FHAMultiRate = Convert.ToString(Convert.ToDouble((Convert.ToDouble(BaseRate) * Convert.ToDouble(CostingRemark)) / 100) + Convert.ToDouble(BaseRate));
                                        htParam.Add("RemarkCost", Convert.ToString(Convert.ToDouble((Convert.ToDouble(BaseRate) * Convert.ToDouble(CostingRemark)) / 100) + Convert.ToDouble(BaseRate)));
                                    }
                                    else if (IBP_Id == 23)
                                        htParam.Add("RemarkCost", Convert.ToString(Convert.ToDouble((Convert.ToDouble(FHARate) * Convert.ToDouble(CostingRemark)) / 100) + Convert.ToDouble(FHARate)));
                                    else if (IBP_Id == 24)
                                        htParam.Add("RemarkCost", Convert.ToString(Convert.ToDouble((Convert.ToDouble(MultiRate) * Convert.ToDouble(CostingRemark)) / 100) + Convert.ToDouble(MultiRate)));
                                    else if (IBP_Id == 26)
                                        htParam.Add("RemarkCost", Convert.ToString(Convert.ToDouble((Convert.ToDouble(FHAUpdateRate) * Convert.ToDouble(CostingRemark)) / 100) + Convert.ToDouble(FHAUpdateRate)));
                                    else if (IBP_Id == 28)
                                        htParam.Add("RemarkCost", Convert.ToString(Convert.ToDouble((Convert.ToDouble(MultiUpdateRate) * Convert.ToDouble(CostingRemark)) / 100) + Convert.ToDouble(MultiUpdateRate)));
                                    else if (IBP_Id == 31)
                                        htParam.Add("RemarkCost", Convert.ToString(Convert.ToDouble((Convert.ToDouble(FHAMultiRate) * Convert.ToDouble(CostingRemark)) / 100) + Convert.ToDouble(FHAMultiRate)));
                                    else if (IBP_Id == 31)
                                        htParam.Add("RemarkCost", Convert.ToString(Convert.ToDouble((Convert.ToDouble(FHAMultiUpdateRate) * Convert.ToDouble(CostingRemark)) / 100) + Convert.ToDouble(FHAMultiUpdateRate)));
                                    else
                                        htParam.Add("RemarkCost", Convert.ToString(Convert.ToDouble((Convert.ToDouble(BaseRate) * Convert.ToDouble(CostingRemark)) / 100) + Convert.ToDouble(BaseRate)));
                                }
                                else
                                    htParam.Add("RemarkCost", CostingRemark);
                                htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
                                int result = InsertCostingWithParameters(htParam);
                                if (result > 0)
                                {
                                    ((ASPxGridView)sender).JSProperties["cp_message"] = "1";
                                }
                                else
                                {
                                    ((ASPxGridView)sender).JSProperties["cp_message"] = "0";
                                }
                            }
                        }
                    }
                    else
                    {
                        ((ASPxGridView)sender).JSProperties["cp_message"] = "4";
                        ((ASPxGridView)sender).JSProperties["cp_Field"] = Convert.ToString(grdBillingParams.GetRowValues(i, "IBP_ParameterName"));
                        return;
                    }
                }
                try
                {
                    //  new SendMail().SendPriceEmail(Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"])), Convert.ToInt32(ddldomain.SelectedValue));
                }
                catch { }

            }
        }

        protected void grdCostingParameters_BeforePerformDataSelect(object sender, EventArgs e)
        {
            if (Convert.ToString(Request.QueryString["ERPProjectId"]) != "" && Convert.ToString(Request.QueryString["ERPProjectId"]) != null)
            {
                Session["ProjectId"] = Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"]));
            }
            else { Session["ProjectId"] = 0; }
            if (ddldomain.SelectedValue != "Select")
            {
                Session["formdate"] = ddldomain.SelectedValue;
            }
            else
            { Session["formdate"] = 4; }

        }

        protected void ddlBillingColumns_Init(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            GridViewDataItemTemplateContainer container = ddl.NamingContainer as GridViewDataItemTemplateContainer;
            if (Convert.ToString(DataBinder.Eval(container.DataItem, "IBV_CostingColumn")) == "0" || Convert.ToString(DataBinder.Eval(container.DataItem, "IBV_CostingColumn")) == "")
                ddl.SelectedIndex = 0;
            else
                ddl.SelectedValue = DataBinder.Eval(container.DataItem, "IBV_CostingColumn").ToString();
        }

        public DataTable GetColumndata(string ProjectId)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "GetColumndatabyprojectIdForCosting");
            SQLHelper.AddParamToSQLCmd(cmd, "@ProjectId", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, ProjectId);
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }

        public int DeleteExisingCostingWithParameters(int ProjectID)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_DeleteOldPriceDetails");
            SQLHelper.AddParamToSQLCmd(cmd, "@ProjectId", System.Data.SqlDbType.BigInt, 10, System.Data.ParameterDirection.Input, ProjectID);
            SQLHelper.AddParamToSQLCmd(cmd, "@ReturnValue", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.ReturnValue, null);
            SQLHelper.ExecuteNonQueryCmdBilling(cmd);

            int ReturnValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
            return ReturnValue;
        }

        public int InsertCostingWithParameters(Hashtable htParam)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_Billing_InsertCostingwithBillingParameters_Temp");
            SQLHelper.AddParamToSQLCmd(cmd, "@ProjectId", System.Data.SqlDbType.BigInt, 10, System.Data.ParameterDirection.Input, htParam["ProjectId"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@IBP_Id", System.Data.SqlDbType.BigInt, 10, System.Data.ParameterDirection.Input, htParam["IBP_Id"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@CostingColumn", System.Data.SqlDbType.NVarChar, 4000, System.Data.ParameterDirection.Input, htParam["CostingColumn"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@RemarkCost", System.Data.SqlDbType.NVarChar, 4000, System.Data.ParameterDirection.Input, htParam["RemarkCost"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@AddedBy", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.Input, htParam["AddedBy"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@ReturnValue", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.ReturnValue, null);
            SQLHelper.ExecuteNonQueryCmdBilling(cmd);

            int ReturnValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
            return ReturnValue;
        }

        public int InsertBillParameterCosting(Hashtable htParam)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "[usp_BillingInsertParametersCosting]");
            SQLHelper.AddParamToSQLCmd(cmd, "@IBV_ParameterId", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.Input, htParam["IBV_ParameterId"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@IBV_Comment", System.Data.SqlDbType.NVarChar, 500, System.Data.ParameterDirection.Input, htParam["IBV_Comment"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@IBV_Additional", System.Data.SqlDbType.NVarChar, 500, System.Data.ParameterDirection.Input, htParam["IBV_Additional"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@IBV_Remark", System.Data.SqlDbType.NVarChar, 500, System.Data.ParameterDirection.Input, htParam["IBV_Remark"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@IBV_ChargeType", System.Data.SqlDbType.NVarChar, 500, System.Data.ParameterDirection.Input, htParam["IBV_ChargeType"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@ProjectId", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.Input, htParam["ProjectId"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@Unique", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, htParam["Unique"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@Date", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, htParam["Date"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@AddedBy", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.Input, htParam["AddedBy"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@ReturnValue", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.ReturnValue, null);
            SQLHelper.ExecuteNonQueryCmdBilling(cmd);

            int ReturnValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
            return ReturnValue;
        }

        protected void txtCostingRemark_Init(object sender, EventArgs e)
        {
            TextBox ddl = sender as TextBox;
            GridViewDataItemTemplateContainer container = ddl.NamingContainer as GridViewDataItemTemplateContainer;
            DataTable dt = new DataTable();
            if (Convert.ToString(DataBinder.Eval(container.DataItem, "IBV_Remark")) == "0" || Convert.ToString(DataBinder.Eval(container.DataItem, "IBV_Remark")) == "")
                ddl.Text = "";
            else
                ddl.Text = DataBinder.Eval(container.DataItem, "IBV_Remark").ToString();
        }

        protected void ddlChargeType_Init(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            GridViewDataItemTemplateContainer container = ddl.NamingContainer as GridViewDataItemTemplateContainer;
            if (Convert.ToString(DataBinder.Eval(container.DataItem, "IBV_ChargeType")) == "0" || Convert.ToString(DataBinder.Eval(container.DataItem, "IBV_ChargeType")) == "")
                ddl.SelectedIndex = 0;
            else
                ddl.SelectedValue = DataBinder.Eval(container.DataItem, "IBV_ChargeType").ToString();
            if (container.VisibleIndex == 0)
                ddl.Enabled = false;
        }

        protected void ASPxReoprt_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Number")
            {
                e.Value = string.Format("{0}", e.ListSourceRowIndex + 1);
            }
        }

        protected void txtCommentFromBDM_Init(object sender, EventArgs e)
        {
            TextBox ddl = sender as TextBox;
            GridViewDataItemTemplateContainer container = ddl.NamingContainer as GridViewDataItemTemplateContainer;

            DataTable dt = new DataTable();
            if (Convert.ToString(DataBinder.Eval(container.DataItem, "IBV_CommentFromBDM")) == "0" || Convert.ToString(DataBinder.Eval(container.DataItem, "IBV_CommentFromBDM")) == "")
                ddl.Text = "";
            else
                ddl.Text = DataBinder.Eval(container.DataItem, "IBV_CommentFromBDM").ToString();
        }

        protected void ddlProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProjects.SelectedIndex > 0)
            {
                DataTable dtpaid = bllMaster.GetbyProjectApproval_IdReq(Convert.ToInt32(ddlProjects.SelectedValue));
                if (dtpaid != null)
                {
                    if (dtpaid.Rows.Count > 0)
                    {
                        ddldomain.SelectedValue = Convert.ToString(dtpaid.Rows[0]["PAI_DomainId"]);
                        txtProjectname.Text = Convert.ToString(dtpaid.Rows[0]["PAI_Project_Name"]);
                        //if (Convert.ToString(dtpaid.Rows[0]["PAI_ProcessName"]) != "")
                        //{
                        //    ddlProjects.DataSource = bllMaster.GetAllProjectByUserRights();
                        //    ddlProjects.DataTextField = "ProjectName";
                        //    ddlProjects.DataValueField = "ProjectName";
                        //    ddlProjects.DataBind();
                        //    ddlProjects.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
                        //    trnewprocess.Style.Add("display", "");
                        //    ddlProjects.SelectedValue = Convert.ToString(dtpaid.Rows[0]["PAI_Project_Name"]) + "-" + Convert.ToString(dtpaid.Rows[0]["PAI_ProcessName"]);
                        //    tdprojtxt.Style.Add("display", "none");
                        //    txtProjectname.Style.Add("display", "none");
                        //    txtProcess.Text = Convert.ToString(dtpaid.Rows[0]["PAI_ProcessName"]);
                        //    ddlprojNo.SelectedValue = Convert.ToString(dtpaid.Rows[0]["PAI_Project_Name"]) + "-" + Convert.ToString(dtpaid.Rows[0]["PAI_ProcessName"]);
                        //}
                        //else
                        //{
                        //    trnewprocess.Style.Add("display", "none");
                        //    tdprojtxt.Style.Add("display", "");
                        //    txtProjectname.Style.Add("display", "");
                        //}
                        //lblSalesProjectNumber.Text = Convert.ToString(dtpaid.Rows[0]["PAI_Project_Name"]);
                        //txtCostingProjectNumber.Text = Convert.ToString(dtpaid.Rows[0]["PAI_Project_Name"]);
                        txtcompanyname.Text = Convert.ToString(dtpaid.Rows[0]["PAI_Company_Name"]);
                        txtContactPerson.Text = Convert.ToString(dtpaid.Rows[0]["PAI_Contact_Person"]);
                        txtphonenumber.Text = Convert.ToString(dtpaid.Rows[0]["PAI_Phone_Number"]);
                        txtemail.Text = Convert.ToString(dtpaid.Rows[0]["PAI_Email_Id"]);
                        txturl.Text = Convert.ToString(dtpaid.Rows[0]["PAI_Url"]);
                        txtaddress.Text = Convert.ToString(dtpaid.Rows[0]["PAI_Address"]);

                    }
                }
            }
        }


        #region UW Billing

        public DataTable GetDDLValuesUW(int id)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_getDDLValues_UW");
            SQLHelper.AddParamToSQLCmd(cmd, "@IBPID", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, id);
            DataTable dt = SQLHelper.ExecuteDataTableCmd_billing(cmd);
            return dt;
        }

        protected void grdBillingParamsUW_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Number")
            {
                e.Value = string.Format("{0}", e.ListSourceRowIndex + 1);
            }
        }

        protected void grdBillingParamsUW_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            try
            {

                if (e.Parameters == "update")
                {

                    for (int i = 0; i < listUW.Count; i++)
                    {

                        string Id = Convert.ToString(list[i].Id);
                        string isApplicable = Convert.ToString(listUW[i].Comment);
                        string Additional = Convert.ToString(listUW[i].BillingType);
                        string Remark = Convert.ToString(listUW[i].Remark);
                        string ChargeType = Convert.ToString(listUW[i].ChargeType);
                        string CommentFromBdm = Convert.ToString(listUW[i].CommentFromBdm);
                        string UWVerification = Convert.ToString(listUW[i].UWVerification);

                        if (isApplicable == "Select")
                        {
                            // ((ASPxGridView)sender).JSProperties["cp_message"] = "4";
                            // ((ASPxGridView)sender).JSProperties["cp_Field"] = Convert.ToString(grdBillingParamsUW.GetRowValues(i, "IBP_ParameterName"));
                            //return;
                        }

                    }

                    for (int i = 0; i < listUW.Count; i++)
                    {
                        string Id = Convert.ToString(list[i].Id);
                        string isApplicable = Convert.ToString(listUW[i].Comment);
                        string Additional = Convert.ToString(listUW[i].BillingType);
                        string Remark = Convert.ToString(listUW[i].Remark);
                        string ChargeType = Convert.ToString(listUW[i].ChargeType);
                        string CommentFromBdm = Convert.ToString(listUW[i].CommentFromBdm);
                        string UWVerification = Convert.ToString(listUW[i].UWVerification);


                        if (isApplicable != "")
                        {

                            if (isApplicable == "Yes" && Remark == "")
                            {
                                //((ASPxGridView)sender).JSProperties["cp_message"] = "2";
                                //((ASPxGridView)sender).JSProperties["cp_Field"] = Convert.ToString(grdBillingParamsUW.GetRowValues(i, "IBP_ParameterName"));
                                //break;
                            }

                            if (Additional == "Yes" && ChargeType == "Select")
                            {
                                //((ASPxGridView)sender).JSProperties["cp_message"] = "3";
                                //((ASPxGridView)sender).JSProperties["cp_Field"] = Convert.ToString(grdBillingParamsUW.GetRowValues(i, "IBP_ParameterName"));
                                //break;
                            }

                            Hashtable Htparam = new Hashtable();
                            Htparam.Add("IBV_ParameterId", Convert.ToString(Id));
                            Htparam.Add("IBV_Comment", Convert.ToString(isApplicable));
                            Htparam.Add("IBV_Additional", Convert.ToString(Additional));
                            Htparam.Add("IBV_UWVerification", Convert.ToString(UWVerification));
                            Htparam.Add("IBV_Remark", Convert.ToString(Remark));
                            if (i == 0)
                                Htparam.Add("IBV_ChargeType", Convert.ToString("Fix Amount"));
                            else
                                Htparam.Add("IBV_ChargeType", Convert.ToString(ChargeType));
                            Htparam.Add("IBV_CommentFromBDM", Convert.ToString(CommentFromBdm));
                            Htparam.Add("AddedBy", Convert.ToInt32(HttpContext.Current.User.Identity.Name));
                            Htparam.Add("ProjectId", Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"])));
                            int result = bllMaster.InsertBillParameterUW(Htparam);
                            if (result > 0)
                            {
                                ((ASPxGridView)sender).JSProperties["cp_message"] = "1";
                            }
                            else
                            {
                                ((ASPxGridView)sender).JSProperties["cp_message"] = "0";
                            }



                        }


                    }


                }


            }

            catch { }
        }

        protected void grdBillingParamsUW_BeforePerformDataSelect(object sender, EventArgs e)
        {

        }

        protected void ddlIsApplicableUW_Init(object sender, EventArgs e)
        {
            try
            {

                DropDownList ddl = sender as DropDownList;
                GridViewDataItemTemplateContainer container = ddl.NamingContainer as GridViewDataItemTemplateContainer;

                DataTable dt = new DataTable();

                dt = GetDDLValues();
                ddl.Items.Insert(0, new ListItem("Select"));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ddl.Items.Insert(i + 1, new ListItem(Convert.ToString(dt.Rows[i]["Text"]), Convert.ToString(dt.Rows[i]["Text"])));
                }

                if (Convert.ToString(DataBinder.Eval(container.DataItem, "IBV_Comment")) == "0" || Convert.ToString(DataBinder.Eval(container.DataItem, "IBV_Comment")) == "")
                    ddl.SelectedIndex = 0;
                else
                    ddl.SelectedValue = DataBinder.Eval(container.DataItem, "IBV_Comment").ToString();

            }
            catch
            { }

        }

        protected void ddlUWVerification_Init(object sender, EventArgs e)
        {
            try
            {
                if (ddldomain.SelectedValue == "9") //UW
                {
                    DropDownList ddl = sender as DropDownList;
                    GridViewDataItemTemplateContainer container = ddl.NamingContainer as GridViewDataItemTemplateContainer;

                    DataTable dt = new DataTable();

                    dt = GetDDLValues();
                    ddl.Items.Insert(0, new ListItem("Select"));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ddl.Items.Insert(i + 1, new ListItem(Convert.ToString(dt.Rows[i]["Text"]), Convert.ToString(dt.Rows[i]["Text"])));
                    }

                    if (Convert.ToString(DataBinder.Eval(container.DataItem, "IBV_VendorBillingVerification")) == "0" || Convert.ToString(DataBinder.Eval(container.DataItem, "IBV_VendorBillingVerification")) == "")
                        ddl.SelectedIndex = 0;
                    else
                        ddl.SelectedValue = DataBinder.Eval(container.DataItem, "IBV_VendorBillingVerification").ToString();

                }
            }
            catch
            { }
        }

        protected void ddlBillingType_Init(object sender, EventArgs e)
        {
            if (ddldomain.SelectedValue == "9") //UW
            {
                DropDownList ddl = sender as DropDownList;
                GridViewDataItemTemplateContainer container = ddl.NamingContainer as GridViewDataItemTemplateContainer;

                DataTable dt = new DataTable();
                int IBP_id = Convert.ToInt32(DataBinder.Eval(container.DataItem, "IBP_Id"));
                dt = GetDDLValuesUW(IBP_id);
                if (dt != null)
                {
                    ddl.Items.Insert(0, new ListItem("Select"));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ddl.Items.Insert(i + 1, new ListItem(Convert.ToString(dt.Rows[i]["value"]), Convert.ToString(dt.Rows[i]["value"])));
                    }

                    if (Convert.ToString(DataBinder.Eval(container.DataItem, "IBV_BillingType")) == "0" || Convert.ToString(DataBinder.Eval(container.DataItem, "IBV_BillingType")) == "")
                        ddl.SelectedIndex = 0;
                    else
                        ddl.SelectedValue = DataBinder.Eval(container.DataItem, "IBV_BillingType").ToString();
                }
            }
        }

        protected void ddlChargeTypeUW_Init(object sender, EventArgs e)
        {

            DropDownList ddl = sender as DropDownList;
            GridViewDataItemTemplateContainer container = ddl.NamingContainer as GridViewDataItemTemplateContainer;
            if (Convert.ToString(DataBinder.Eval(container.DataItem, "IBV_ChargeType")) == "0" || Convert.ToString(DataBinder.Eval(container.DataItem, "IBV_ChargeType")) == "")
                ddl.SelectedIndex = 0;
            else
                ddl.SelectedValue = DataBinder.Eval(container.DataItem, "IBV_ChargeType").ToString();
            if (container.VisibleIndex == 0)
                ddl.Enabled = false;

        }
        #endregion


        #region UW BillingPricing
        protected void ddlChargeTypeUWPricing_Init(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            GridViewDataItemTemplateContainer container = ddl.NamingContainer as GridViewDataItemTemplateContainer;
            if (Convert.ToString(DataBinder.Eval(container.DataItem, "IBV_ChargeType")) == "0" || Convert.ToString(DataBinder.Eval(container.DataItem, "IBV_ChargeType")) == "")
                ddl.SelectedIndex = 0;
            else
                ddl.SelectedValue = DataBinder.Eval(container.DataItem, "IBV_ChargeType").ToString();
            if (container.VisibleIndex == 0)
                ddl.Enabled = false;

        }

        protected void grdCostingParametersUW_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Number")
            {
                e.Value = string.Format("{0}", e.ListSourceRowIndex + 1);
            }

        }

        protected void grdCostingParametersUW_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

        }

        protected void grdCostingParametersUW_BeforePerformDataSelect(object sender, EventArgs e)
        {

        }

        protected void ddlCompanyUWPricing_Init(object sender, EventArgs e)
        {
            try
            {

                DropDownList ddl = sender as DropDownList;
                GridViewDataItemTemplateContainer container = ddl.NamingContainer as GridViewDataItemTemplateContainer;

                DataTable dt = new DataTable();

                dt = GetDDLValues();
                ddl.Items.Insert(0, new ListItem("Select"));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ddl.Items.Insert(i + 1, new ListItem(Convert.ToString(dt.Rows[i]["Text"]), Convert.ToString(dt.Rows[i]["Text"])));
                }

                if (Convert.ToString(DataBinder.Eval(container.DataItem, "IBV_Comment")) == "0" || Convert.ToString(DataBinder.Eval(container.DataItem, "IBV_Comment")) == "")
                    ddl.SelectedIndex = 0;
                else
                    ddl.SelectedValue = DataBinder.Eval(container.DataItem, "IBV_Comment").ToString();

            }
            catch
            { }

        }
        protected void ddlBillingColumnsUWPricing_Init(object sender, EventArgs e)
        {

        }
        protected void ddlBillingTypeUWPricing_Init(object sender, EventArgs e)
        {
            if (ddldomain.SelectedValue == "9") //UW
            {
                DropDownList ddl = sender as DropDownList;
                GridViewDataItemTemplateContainer container = ddl.NamingContainer as GridViewDataItemTemplateContainer;

                DataTable dt = new DataTable();
                int IBP_id = Convert.ToInt32(DataBinder.Eval(container.DataItem, "IBP_Id"));
                dt = GetDDLValuesUW(IBP_id);
                if (dt != null)
                {
                    ddl.Items.Insert(0, new ListItem("Select"));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ddl.Items.Insert(i + 1, new ListItem(Convert.ToString(dt.Rows[i]["value"]), Convert.ToString(dt.Rows[i]["value"])));
                    }

                    if (Convert.ToString(DataBinder.Eval(container.DataItem, "IBV_BillingType")) == "0" || Convert.ToString(DataBinder.Eval(container.DataItem, "IBV_BillingType")) == "")
                        ddl.SelectedIndex = 0;
                    else
                        ddl.SelectedValue = DataBinder.Eval(container.DataItem, "IBV_BillingType").ToString();
                }
            }
        }

        protected void ddlVendorBillingUWPricing_Init(object sender, EventArgs e)
        {
            try
            {
                if (ddldomain.SelectedValue == "9") //UW
                {
                    DropDownList ddl = sender as DropDownList;
                    GridViewDataItemTemplateContainer container = ddl.NamingContainer as GridViewDataItemTemplateContainer;

                    DataTable dt = new DataTable();

                    dt = GetDDLValues();
                    ddl.Items.Insert(0, new ListItem("Select"));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ddl.Items.Insert(i + 1, new ListItem(Convert.ToString(dt.Rows[i]["Text"]), Convert.ToString(dt.Rows[i]["Text"])));
                    }

                    if (Convert.ToString(DataBinder.Eval(container.DataItem, "IBV_VendorBillingVerification")) == "0" || Convert.ToString(DataBinder.Eval(container.DataItem, "IBV_VendorBillingVerification")) == "")
                        ddl.SelectedIndex = 0;
                    else
                        ddl.SelectedValue = DataBinder.Eval(container.DataItem, "IBV_VendorBillingVerification").ToString();

                }
            }
            catch
            { }
        }
        #endregion

        public DataTable GetCostingDetailsFTE(int ProjectID)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetFTEProjectCosting");
            SQLHelper.AddParamToSQLCmd(cmd, "@ProjectID", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.Input, ProjectID);
            DataTable dt = SQLHelper.ExecuteDataTableCmd_billing(cmd);
            return dt;
        }
        public DataTable GetCostingDetails861(int ProjectID)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetFTEProjectCostingFor861");
            SQLHelper.AddParamToSQLCmd(cmd, "@ProjectID", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.Input, ProjectID);
            DataTable dt = SQLHelper.ExecuteDataTableCmd_billing(cmd);
            return dt;
        }

        public int InsertCostingWithParameters_FTE(Hashtable htParam)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "[usp_Billing_InsertCostingwithBillingParameters_FTE_Temp]");
            SQLHelper.AddParamToSQLCmd(cmd, "@ProjectId", System.Data.SqlDbType.BigInt, 10, System.Data.ParameterDirection.Input, htParam["ProjectId"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@CostingColumn", System.Data.SqlDbType.NVarChar, 4000, System.Data.ParameterDirection.Input, htParam["CostingColumn"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@RemarkCost", System.Data.SqlDbType.NVarChar, 4000, System.Data.ParameterDirection.Input, htParam["RemarkCost"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@AddedBy", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.Input, htParam["AddedBy"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@CostingColumnValue", System.Data.SqlDbType.NVarChar, 4000, System.Data.ParameterDirection.Input, htParam["CostingColumnValue"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@ChargeType", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, htParam["ChargeType"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@RateForOthers", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, htParam["RateForOthers"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@ReturnValue", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.ReturnValue, null);
            SQLHelper.ExecuteNonQueryCmdBilling(cmd);

            int ReturnValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
            return ReturnValue;
        }

        public DataTable GetCostingDetailsWHoleLoan()
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetWholeLoanCosting");
            DataTable dt = SQLHelper.ExecuteDataTableCmd_billing(cmd);
            return dt;
        }

        public int InsertCostingWithParameters_WholeLoan(Hashtable htParam)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "[up_InsertWholeLoanCost]");
            SQLHelper.AddParamToSQLCmd(cmd, "@ProjectID", System.Data.SqlDbType.BigInt, 10, System.Data.ParameterDirection.Input, htParam["ProjectID"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@BillTo", System.Data.SqlDbType.NVarChar, 4000, System.Data.ParameterDirection.Input, htParam["BillTo"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@Price", System.Data.SqlDbType.Decimal, 10, System.Data.ParameterDirection.Input, htParam["Price"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@AddedBy", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.Input, htParam["AddedBy"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@ReturnValue", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.ReturnValue, null);
            SQLHelper.ExecuteNonQueryCmdBilling(cmd);

            int ReturnValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
            return ReturnValue;
        }


        protected void btnFTESubmit_Click(object sender, EventArgs e)
        {
            Hashtable htParam = new Hashtable();
            htParam.Add("ProjectId", Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"])));
            htParam.Add("CostingColumn", "Order#");
            htParam.Add("RemarkCost", Convert.ToString(txtHourlyRate.Text));
            htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
            htParam.Add("CostingColumnValue", Convert.ToString(""));
            htParam.Add("ChargeType", "Fix Amount");
            htParam.Add("RateForOthers", "");
            int result = InsertCostingWithParameters_FTE(htParam);
            if (result > 0)
            {
                dvError.Style.Add("display", "");
                dvError.Attributes.Add("class", "alert alert-success background-success");
                dvError.InnerHtml = "Costing added successfully!";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            }
            else
            {
                dvError.Style.Add("display", "");
                dvError.Attributes.Add("class", "alert alert-danger background-danger");
                dvError.InnerHtml = "Error occured while adding costing!";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                txtProjectname.Focus();
            }
        }

        protected void btnSubmit861_Click(object sender, EventArgs e)
        {
            Hashtable htParam = new Hashtable();
            htParam.Add("ProjectId", Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"])));
            htParam.Add("CostingColumn", "Order#");
            htParam.Add("RemarkCost", Convert.ToString(txtfirst1200Rate.Text));
            htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
            htParam.Add("CostingColumnValue", Convert.ToString(txtWireTransferCharges.Text));
            htParam.Add("ChargeType", "Fix Amount");
            htParam.Add("RateForOthers", txtAdditionalBallotsRate.Text);
            int result = InsertCostingWithParameters_FTE(htParam);
            if (result > 0)
            {
                dvError.Style.Add("display", "");
                dvError.Attributes.Add("class", "alert alert-success background-success");
                dvError.InnerHtml = "Costing added successfully!";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            }
            else
            {
                dvError.Style.Add("display", "");
                dvError.Attributes.Add("class", "alert alert-danger background-danger");
                dvError.InnerHtml = "Error occured while adding costing!";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                txtProjectname.Focus();
            }
        }

        protected void btnwholeloansubmit_Click(object sender, EventArgs e)
        {
            Hashtable htParam = new Hashtable();
            htParam.Add("ProjectID", Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"])));
            htParam.Add("BillTo", txtBilledTo.Text.Trim());
            htParam.Add("Price", txtWholeloanPrice.Text);
            htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
            int result = InsertCostingWithParameters_WholeLoan(htParam);
            if (result > 0)
            {
                dvError.Style.Add("display", "");
                dvError.Attributes.Add("class", "alert alert-success background-success");
                dvError.InnerHtml = "Costing added successfully!";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                grdCostWholeLoan.DataSource = GetCostingDetailsWHoleLoan();
                grdCostWholeLoan.DataBind();
                txtWholeloanPrice.Text = "";
                txtBilledTo.Text = "";
            }
            else
            {
                dvError.Style.Add("display", "");
                dvError.Attributes.Add("class", "alert alert-danger background-danger");
                dvError.InnerHtml = "Error occured while adding costing!";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                txtProjectname.Focus();
            }
        }
    }
}