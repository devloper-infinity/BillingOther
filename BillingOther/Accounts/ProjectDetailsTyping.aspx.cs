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
    public partial class ProjectDetailsTyping : System.Web.UI.Page
    {
        bllTracking bllMaster = new bllTracking();

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl h4 = (HtmlGenericControl)Master.FindControl("lblHeader");
            h4.InnerText = "Cost Master";
            int ProjectApproval_Id = Convert.ToInt32(Request.QueryString["ERPProjectId"]);
            
            string ss = Convert.ToString(Session["formdate"]);


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
                    //aBack.HRef = "~/ProjectDetails.aspx";
                    btnsbmit.Text = "Update";
                }
                else
                {
                   // Tab1.Style.Add("margin-top", "-70px");
                }
                if (Convert.ToString(Request.QueryString["ERPProjectId"]) != "" && Convert.ToString(Request.QueryString["ERPProjectId"]) != null)
                {
                    rdButtons.Style.Add("display", "none");
                    GetAllInformationSales();
                    BindBaseProductType(Convert.ToString(Request.QueryString["ERPProjectId"]));
                    BindTypingCosting(Convert.ToInt32(Request.QueryString["ERPProjectId"]));
                }
                else
                {
                    rdButtons.Style.Add("display", "");
                }
            }
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
            }
            catch (Exception ex)
            {
                if (ex.Message != "Input string was not in a correct format.") { }
            }


        }

        public void BindBaseProductType(string ProjectID)
        {
            ddlBaseProductType.DataSource = GetProductTypeByProject(Convert.ToInt32(ProjectID));
            ddlBaseProductType.DataValueField = "Product_Type";
            ddlBaseProductType.DataTextField = "Product_Type";
            ddlBaseProductType.DataBind();
            ddlBaseProductType.Items.Insert(0, new ListItem("Select"));
        }

        public void BindTypingCosting(int ProjectId)
        {
            DataTable dtCrt = GetBillingCriteria(ProjectId);
            if (dtCrt != null)
            {
                if (dtCrt.Rows.Count > 0)
                {
                    ddlBillingCriteria.SelectedValue = Convert.ToString(dtCrt.Rows[0]["Criteria"]);
                }
            }
            DataTable dt = GetCostingDetailsTyping(ProjectId);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    string BillingBase = Convert.ToString(dt.Rows[0]["BillingBase"]);
                    if (BillingBase == "Base")
                    {
                        #region Base Rate Only
                        ddlBillingBase.SelectedIndex = 1;
                        trBaseRate.Style.Add("display", "");
                        trProductType.Style.Add("display", "none");
                        trOrderType.Style.Add("display", "none");
                        trCharacter.Style.Add("display", "none");
                        tbl379003.Style.Add("display", "none");

                        string find = "ProjectColumn in ('Order#','Dispatched Record','No Of Records') and AdditionalConditions=0 ";
                        DataRow[] foundRows = dt.Select(find);
                        if (foundRows.Length > 0)
                        {
                            ddlIsApplicableBaseRate.SelectedValue = "Yes";
                            ddlChargeTypeBaseRate.SelectedValue = Convert.ToString(foundRows[0]["Updatelevel"]);
                            txtPriceBaseRate.Text = Convert.ToString(foundRows[0]["RateForOthers"]);
                            ddlBillingHeaderBaseRate.SelectedValue = Convert.ToString(foundRows[0]["ProjectColumn"]);
                        }
                        find = "ProjectColumn = '# of Character'";
                        DataRow[] foundRows2 = dt.Select(find);
                        if (foundRows2.Length > 0)
                        {
                            ddlIsApplicableBaseRateCharacter.SelectedValue = "Yes";
                            ddlChargeTypeBaseRateCharacter.SelectedValue = Convert.ToString(foundRows2[0]["Updatelevel"]);
                            txtPriceBaseRateCharacter.Text = Convert.ToString(foundRows2[0]["RateForOthers"]);
                            ddlBillingHeaderBaseRateCharacter.SelectedValue = Convert.ToString(foundRows2[0]["ProjectColumn"]);
                        }
                        else
                        {
                            ddlIsApplicableBaseRateCharacter.SelectedValue = "No";
                            ddlChargeTypeBaseRateCharacter.SelectedValue = Convert.ToString("Select");
                            txtPriceBaseRateCharacter.Text = Convert.ToString("");
                            ddlBillingHeaderBaseRateCharacter.SelectedValue = Convert.ToString("Select");
                        }
                        find = "ProjectColumn = 'Rush'";
                        DataRow[] foundRows3 = dt.Select(find);
                        if (foundRows3.Length > 0)
                        {
                            ddlIsApplicableBaseRateRush.SelectedValue = "Yes";
                            ddlChargeTypeBaseRateRush.SelectedValue = Convert.ToString(foundRows3[0]["Updatelevel"]);
                            txtPriceBaseRateRush.Text = Convert.ToString(foundRows3[0]["RateForOthers"]);
                            ddlBillingHeaderBaseRateRush.SelectedValue = Convert.ToString(foundRows3[0]["ProjectColumn"]);
                        }
                        else
                        {
                            ddlIsApplicableBaseRateRush.SelectedValue = "No";
                            ddlChargeTypeBaseRateRush.SelectedValue = Convert.ToString("Select");
                            txtPriceBaseRateRush.Text = Convert.ToString("");
                            ddlBillingHeaderBaseRateRush.SelectedValue = Convert.ToString("Select");
                        }
                        //}
                        #endregion
                    }
                    else if (BillingBase == "Product Type")
                    {
                        #region Product Type
                        ddlBillingBase.SelectedIndex = 2;
                        string BaseProductType = "";
                        trBaseRate.Style.Add("display", "none");
                        trProductType.Style.Add("display", "");
                        trOrderType.Style.Add("display", "none");
                        trCharacter.Style.Add("display", "none");
                        tbl379003.Style.Add("display", "none");
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (Convert.ToString(dt.Rows[i]["ConditionalValue"]) == "Purchase")
                            {
                                ddlIsApplicablePurchase.SelectedValue = "Yes";
                                ddlChargeTypePurchase.SelectedValue = Convert.ToString(dt.Rows[i]["Updatelevel"]);
                                txtPricePurchase.Text = Convert.ToString(dt.Rows[i]["RateForOthers"]);
                                ddlBillingHeaderPurchase.SelectedValue = Convert.ToString(dt.Rows[i]["ConditionalColumn"]);
                            }
                            else if (Convert.ToString(dt.Rows[i]["ConditionalValue"]) == "Refinance")
                            {
                                ddlIsApplicableRefinance.SelectedValue = "Yes";
                                ddlChargeTypeRefinance.SelectedValue = Convert.ToString(dt.Rows[i]["Updatelevel"]);
                                txtPriceRefinance.Text = Convert.ToString(dt.Rows[i]["RateForOthers"]);
                                ddlBillingHeaderRefinance.SelectedValue = Convert.ToString(dt.Rows[i]["ConditionalColumn"]);
                            }
                            else if (Convert.ToString(dt.Rows[i]["ConditionalValue"]) == "Current Owner")
                            {
                                ddlIsApplicableCurrentOwner.SelectedValue = "Yes";
                                ddlChargeTypeCurrentOwner.SelectedValue = Convert.ToString(dt.Rows[i]["Updatelevel"]);
                                txtPriceCurrentOwner.Text = Convert.ToString(dt.Rows[i]["RateForOthers"]);
                                ddlBillingHeaderCurrentOwner.SelectedValue = Convert.ToString(dt.Rows[i]["ConditionalColumn"]);
                            }
                            else if (Convert.ToString(dt.Rows[i]["ConditionalValue"]) == "Full Search")
                            {
                                ddlIsApplicableFullSearch.SelectedValue = "Yes";
                                ddlChargeTypeFullSearch.SelectedValue = Convert.ToString(dt.Rows[i]["Updatelevel"]);
                                txtPriceFullSearch.Text = Convert.ToString(dt.Rows[i]["RateForOthers"]);
                                ddlBillingHeaderFullSearch.SelectedValue = Convert.ToString(dt.Rows[i]["ConditionalColumn"]);
                            }
                            else if (Convert.ToString(dt.Rows[i]["ConditionalValue"]) == "Two Owner")
                            {
                                ddlIsApplicableTwoOwner.SelectedValue = "Yes";
                                ddlChargeTypeTwoOwner.SelectedValue = Convert.ToString(dt.Rows[i]["Updatelevel"]);
                                txtPriceTwoOwner.Text = Convert.ToString(dt.Rows[i]["RateForOthers"]);
                                ddlBillingHeaderTwoOwner.SelectedValue = Convert.ToString(dt.Rows[i]["ConditionalColumn"]);
                            }
                            else if (Convert.ToString(dt.Rows[i]["ConditionalValue"]) == "" && Convert.ToString(dt.Rows[i]["ProjectColumn"]) == "# of Character")
                            {
                                ddlIsApplicableCharacter.SelectedValue = "Yes";
                                ddlChargeTypeCharacter.SelectedValue = Convert.ToString(dt.Rows[i]["Updatelevel"]);
                                txtPriceCharacter.Text = Convert.ToString(dt.Rows[i]["RateForOthers"]);
                                ddlBillingHeaderCharacterNew.SelectedValue = Convert.ToString(dt.Rows[i]["ConditionalColumn"]);
                            }
                        }
                        #region OLD Code for Costing
                        //string find = "ProjectColumn in ('Order#') and AdditionalConditions=1 and Updatelevel='Fix Amount'";
                        //DataRow[] foundRows = dt.Select(find);
                        //if (foundRows.Length > 0)
                        //{
                        //    ddlBaseProductType.SelectedValue = Convert.ToString(foundRows[0]["ConditionalValue"]);
                        //    hdProductBaseLabel.Value = Convert.ToString(foundRows[0]["ConditionalValue"]);
                        //    lblProdcutBase.Text = Convert.ToString(foundRows[0]["ConditionalValue"]);
                        //    ddlIsApplicableProductBase.SelectedValue = "Yes";
                        //    ddlChargeTypeProductBase.SelectedValue = Convert.ToString(foundRows[0]["Updatelevel"]);
                        //    txtPriceProductBase.Text = Convert.ToString(foundRows[0]["RateForOthers"]);
                        //    ddlBillingHEaderProductBase.SelectedValue = Convert.ToString(foundRows[0]["ConditionalColumn"]);
                        //}
                        //find = "ProjectColumn in ('Order#') and AdditionalConditions=1 and ConditionalValue not in ('" + ddlBaseProductType.SelectedValue + "')";
                        //DataRow[] foundRows1 = dt.Select(find);
                        //if (foundRows1.Length > 0)
                        //{
                        //    lblProdcutOther.Text = Convert.ToString(foundRows1[0]["ConditionalValue"]);
                        //    hdProductOtherLabel.Value = Convert.ToString(foundRows1[0]["ConditionalValue"]);
                        //    ddlIsApplicableProductOther.SelectedValue = "Yes";
                        //    ddlChargeTypeProductOther.SelectedValue = Convert.ToString(foundRows1[0]["Updatelevel"]);
                        //    txtPriceProductOther.Text = Convert.ToString(foundRows1[0]["RateForOthers"]);
                        //    ddlBillingHEaderProductOther.SelectedValue = Convert.ToString(foundRows1[0]["ConditionalColumn"]);
                        //}
                        //find = "ProjectColumn = '# of Character'";
                        //DataRow[] foundRows2 = dt.Select(find);
                        //if (foundRows2.Length > 0)
                        //{
                        //    ddlIsApplicableProductCharacter.SelectedValue = "Yes";
                        //    ddlChargeTypeProductCharacter.SelectedValue = Convert.ToString(foundRows2[0]["Updatelevel"]);
                        //    txtPriceProductCharacter.Text = Convert.ToString(foundRows2[0]["RateForOthers"]);
                        //    ddlBillingHeaderproductCharacter.SelectedValue = Convert.ToString(foundRows2[0]["ProjectColumn"]);
                        //}
                        //else
                        //{
                        //    ddlIsApplicableProductCharacter.SelectedValue = "No";
                        //    ddlChargeTypeProductCharacter.SelectedValue = Convert.ToString("Select");
                        //    txtPriceProductCharacter.Text = Convert.ToString("");
                        //    ddlBillingHeaderproductCharacter.SelectedValue = Convert.ToString("Select");
                        //}
                        #endregion
                        //}
                        #endregion
                    }
                    else if (BillingBase == "Order Type")
                    {
                        #region Order Type
                        ddlBillingBase.SelectedIndex = 3;

                        trBaseRate.Style.Add("display", "none");
                        trProductType.Style.Add("display", "none");
                        trOrderType.Style.Add("display", "");
                        trCharacter.Style.Add("display", "none");
                        tbl379003.Style.Add("display", "none");

                        string find = "ProjectColumn in ('Order#') and AdditionalConditions=1 and Updatelevel='Fix Amount'";
                        DataRow[] foundRows = dt.Select(find);
                        if (foundRows.Length > 0)
                        {
                            ddlBaseOrderType.SelectedValue = Convert.ToString(foundRows[0]["ConditionalValue"]);
                            hdOrderBaseLabel.Value = Convert.ToString(foundRows[0]["ConditionalValue"]);
                            lblOrderTypeBase.Text = Convert.ToString(foundRows[0]["ConditionalValue"]);
                            ddlIsApplicableOrderBase.SelectedValue = "Yes";
                            ddlChargeTypeOrderBase.SelectedValue = Convert.ToString(foundRows[0]["Updatelevel"]);
                            txtPriceOrderBase.Text = Convert.ToString(foundRows[0]["RateForOthers"]);
                            ddlBillingHeaderOrderBase.SelectedValue = Convert.ToString(foundRows[0]["ConditionalColumn"]);
                        }
                        find = "ProjectColumn in ('Order#') and AdditionalConditions=1 and ConditionalValue not in ('" + ddlBaseOrderType.SelectedValue + "')";
                        DataRow[] foundRows1 = dt.Select(find);
                        if (foundRows1.Length > 0)
                        {
                            lblOrderTypeOther.Text = Convert.ToString(foundRows1[0]["ConditionalValue"]);
                            hdOrderOtherLabel.Value = Convert.ToString(foundRows1[0]["ConditionalValue"]);
                            ddlIsApplicableOrderOther.SelectedValue = "Yes";
                            ddlChargeTypeOrderOther.SelectedValue = Convert.ToString(foundRows1[0]["Updatelevel"]);
                            txtPriceOrderOther.Text = Convert.ToString(foundRows1[0]["RateForOthers"]);
                            ddlBillingHeaderOrderOther.SelectedValue = Convert.ToString(foundRows1[0]["ConditionalColumn"]);
                        }
                        find = "ProjectColumn = '# of Character'";
                        DataRow[] foundRows2 = dt.Select(find);
                        if (foundRows2.Length > 0)
                        {
                            ddlIsApplicableOrderCharacter.SelectedValue = "Yes";
                            ddlChargeTypeOrderCharacter.SelectedValue = Convert.ToString(foundRows2[0]["Updatelevel"]);
                            txtPriceOrderCharacter.Text = Convert.ToString(foundRows2[0]["RateForOthers"]);
                            ddlBillingHEaderOrderCharacter.SelectedValue = Convert.ToString(foundRows2[0]["ProjectColumn"]);
                        }
                        else
                        {
                            ddlIsApplicableOrderCharacter.SelectedValue = "No";
                            ddlChargeTypeOrderCharacter.SelectedValue = Convert.ToString("Select");
                            txtPriceOrderCharacter.Text = Convert.ToString("");
                            ddlBillingHEaderOrderCharacter.SelectedValue = Convert.ToString("Select");
                        }
                        //}
                        #endregion
                    }
                    else if (BillingBase == "Character Based")
                    {
                        #region Character Based
                        ddlBillingBase.SelectedIndex = 4;

                        trBaseRate.Style.Add("display", "none");
                        trProductType.Style.Add("display", "none");
                        trOrderType.Style.Add("display", "none");
                        trCharacter.Style.Add("display", "");
                        tbl379003.Style.Add("display", "none");
                        string find = "ProjectColumn ='# of Character'";
                        DataRow[] foundRows = dt.Select(find);
                        if (foundRows.Length > 0)
                        {
                            txtPerCharacter.Text = Convert.ToString(foundRows[0]["RateForOthers"]);
                            txtPricePerCharacter.Text = Convert.ToString(foundRows[0]["Rate"]);
                            ddlBillingHeaderCharacter.SelectedValue = Convert.ToString(foundRows[0]["ProjectColumn"]);
                        }
                        #endregion
                    }
                    else if (BillingBase == "Page Based")
                    {
                        #region Character Based
                        string find = "ProjectColumn in ('No of Pages') and AdditionalConditions=0 and Updatelevel='Fix Amount'";
                        DataRow[] foundRows = dt.Select(find);
                        ddlBillingBase.SelectedIndex = 5;

                        trBaseRate.Style.Add("display", "none");
                        trProductType.Style.Add("display", "none");
                        trOrderType.Style.Add("display", "none");
                        trCharacter.Style.Add("display", "none");
                        tbl379003.Style.Add("display", "");
                        if (foundRows.Length > 0)
                        {
                            txtPrice771ScanPages.Text = Convert.ToString(dt.Rows[0]["RateForOthers"]);
                            ddlChargeType771ScanPages.SelectedValue = Convert.ToString(dt.Rows[0]["Updatelevel"]);
                            ddlBillingHeader771ScanPages.SelectedValue = Convert.ToString(dt.Rows[0]["ProjectColumn"]);
                        }
                        #endregion
                    }
                }
            }
        }

        #region Database New - For Typing specific projects
        public DataTable GetProductTypeByProject(int ProjectID)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "WBT_usp_GetProductTypeByProject");
            SQLHelper.AddParamToSQLCmd(cmd, "@ProjectID", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.Input, ProjectID);
            DataTable dt = SQLHelper.ExecuteDataTableCmd(cmd);
            return dt;
        }

        public DataTable GetCostingDetailsTyping(int ProjectID)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetTypingProjectCosting");
            SQLHelper.AddParamToSQLCmd(cmd, "@ProjectID", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.Input, ProjectID);
            DataTable dt = SQLHelper.ExecuteDataTableCmd_billing(cmd);
            return dt;
        }
        #endregion

        public void bindgrid()
        {

            DataTable dt = bllMaster.BindVerifyProjectById(int.Parse(Request.QueryString["ERPProjectId"]));
            ASPxReoprt.DataSource = dt;
            ASPxReoprt.DataBind();
            //clear();
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
                    //lblSalesProjectNumber.Text = txtProjectname.Text;
                    dvError.Style.Add("display", "");
                    dvError.Attributes.Add("class", "alert alert-success background-success");
                    dvError.InnerHtml = "Project information added successfully!";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    txtProjectname.Focus();                                     
                    hdndomain.Value = ddldomain.SelectedValue;
                    Response.Redirect("~/ProjectDetails.aspx?ERPProjectId=" + ReturnValue + "&DomainID=" + ddldomain.SelectedValue);
                    //clear();
                }
                else if (ReturnValue != -1)
                {
                    dvError.Style.Add("display", "");
                    dvError.Attributes.Add("class", "alert alert-danger background-danger");
                    dvError.InnerHtml = "Project information already exist!";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);                   
                }
                else if (ReturnValue == 0)
                {
                    dvError.Style.Add("display", "");
                    dvError.Attributes.Add("class", "alert alert-danger background-danger");
                    dvError.InnerHtml = "Error occured while adding project information!";
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
                    dvError.InnerHtml = "Project information updated successfully!";
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
                ASPxGridView.RedirectOnCallback("~/ProjectDetails.aspx?ProjectApproval_Id=" + PAIId);

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
                    dvError.InnerHtml = "Sales information added successfully!";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);                   
                    new SendMail().SendProjectCreationEmail(Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"])));
                    bindgrid();
                }
                else
                {
                    dvError.Style.Add("display", "");
                    dvError.Attributes.Add("class", "alert alert-danger background-danger");
                    dvError.InnerHtml = "Error occured while adding sales information!";
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
                    dvError.InnerHtml = "Error occured while adding sales information!";
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
                dvError.Attributes.Add("class", "alert alert-danger background-danger");
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
                dvError.Attributes.Add("class", "alert alert-danger background-danger");
                dvError.InnerHtml = "No document is uploaded!";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);               
            }

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

        }

        protected void grdAllFeedbcak_BeforePerformDataSelect(object sender, EventArgs e)
        {

        }

        #endregion

        protected void btnBack_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("~/Accounts/ProjectDetailsMaster.aspx");
        }

        protected void ASPxReoprt_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Number")
            {
                e.Value = string.Format("{0}", e.ListSourceRowIndex + 1);
            }
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

        public int InsertCostingWithParameters_Typing(Hashtable htParam)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_Billing_InsertCostingwithBillingParameters_Typing_Temp");
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

        public int InsertBillingBase(Hashtable htParam)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_InsertBillingBase");
            SQLHelper.AddParamToSQLCmd(cmd, "@ProjectId", System.Data.SqlDbType.BigInt, 10, System.Data.ParameterDirection.Input, htParam["ProjectId"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@BillingBase", System.Data.SqlDbType.NVarChar, 500, System.Data.ParameterDirection.Input, htParam["BillingBase"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@ReturnValue", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.ReturnValue, null);
            SQLHelper.ExecuteNonQueryCmdBilling(cmd);

            int ReturnValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
            return ReturnValue;
        }

        public int InsertBillingCriteria(Hashtable htParam)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_InsertBillingCriteria");
            SQLHelper.AddParamToSQLCmd(cmd, "@ProjectId", System.Data.SqlDbType.BigInt, 10, System.Data.ParameterDirection.Input, htParam["ProjectId"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@BillingCriteria", System.Data.SqlDbType.NVarChar, 100, System.Data.ParameterDirection.Input, htParam["BillingCriteria"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@ReturnValue", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.ReturnValue, null);
            SQLHelper.ExecuteNonQueryCmdBilling(cmd);

            int ReturnValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
            return ReturnValue;
        }

        public DataTable GetBillingCriteria(int ProjectID)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetBillingCriteria");
            SQLHelper.AddParamToSQLCmd(cmd, "@ProjectId", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.Input, ProjectID);
            DataTable dt = SQLHelper.ExecuteDataTableCmd_billing(cmd);
            return dt;
        }

        protected void btnShubmitTyping_Click(object sender, EventArgs e)
        {
            int result = 0;
            Hashtable htBill = new Hashtable();
            htBill.Add("ProjectId", Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"])));
            htBill.Add("BillingBase", Convert.ToString(ddlBillingBase.SelectedValue));
            int ReturnValue = InsertBillingBase(htBill);
            Hashtable htCrt = new Hashtable();
            htCrt.Add("ProjectId", Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"])));
            htCrt.Add("BillingCriteria", Convert.ToString(ddlBillingCriteria.SelectedValue));
            int ReturnValue1 = InsertBillingCriteria(htCrt);
            if (ddlBillingBase.SelectedItem.Text == "Base Rate Only")
            {
                #region Base Rate Only
                if (ddlIsApplicableBaseRate.SelectedValue == "Yes")
                {
                    Hashtable htParam = new Hashtable();
                    htParam.Add("ProjectId", Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"])));
                    htParam.Add("CostingColumn", ddlBillingHeaderBaseRate.SelectedValue);
                    htParam.Add("RemarkCost", Convert.ToString(txtPriceBaseRate.Text));
                    htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
                    htParam.Add("CostingColumnValue", Convert.ToString(""));
                    htParam.Add("ChargeType", ddlChargeTypeBaseRate.SelectedValue);
                    htParam.Add("RateForOthers", Convert.ToString(txtPriceBaseRate.Text));
                    result = InsertCostingWithParameters_Typing(htParam);
                }
                if (ddlIsApplicableBaseRateRush.SelectedValue == "Yes")
                {
                    Hashtable htParam = new Hashtable();
                    htParam.Add("ProjectId", Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"])));
                    htParam.Add("CostingColumn", ddlBillingHeaderBaseRateRush.SelectedValue);
                    htParam.Add("RemarkCost", Convert.ToString(txtPriceBaseRateRush.Text));
                    htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
                    htParam.Add("CostingColumnValue", Convert.ToString(""));
                    htParam.Add("ChargeType", ddlChargeTypeBaseRateRush.SelectedValue);
                    htParam.Add("RateForOthers", Convert.ToString(txtPriceBaseRateRush.Text));
                    result = InsertCostingWithParameters_Typing(htParam);
                }
                if (ddlIsApplicableBaseRateCharacter.SelectedValue == "Yes")
                {
                    if (ddlChargeTypeBaseRateCharacter.SelectedValue == "Fix Amount")
                    {
                        Hashtable htParam = new Hashtable();
                        htParam.Add("ProjectId", Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"])));
                        htParam.Add("CostingColumn", ddlBillingHeaderBaseRateCharacter.SelectedValue);
                        htParam.Add("RemarkCost", Convert.ToString(txtPriceBaseRateCharacter.Text));
                        htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
                        htParam.Add("CostingColumnValue", Convert.ToString(""));
                        htParam.Add("ChargeType", ddlChargeTypeBaseRateCharacter.SelectedValue);
                        htParam.Add("RateForOthers", Convert.ToString(txtPriceBaseRateCharacter.Text));
                        result = InsertCostingWithParameters_Typing(htParam);
                    }
                    else if (ddlChargeTypeBaseRateCharacter.SelectedValue == "Incremental")
                    {
                        decimal rate = 0;
                        rate = Convert.ToDecimal(txtPriceBaseRate.Text) + Convert.ToDecimal(txtPriceBaseRateCharacter.Text);
                        Hashtable htParam = new Hashtable();
                        htParam.Add("ProjectId", Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"])));
                        htParam.Add("CostingColumn", ddlBillingHeaderBaseRateCharacter.SelectedValue);
                        htParam.Add("RemarkCost", Convert.ToString(rate));
                        htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
                        htParam.Add("CostingColumnValue", Convert.ToString(""));
                        htParam.Add("ChargeType", ddlChargeTypeBaseRateCharacter.SelectedValue);
                        htParam.Add("RateForOthers", Convert.ToString(txtPriceBaseRateCharacter.Text));
                        result = InsertCostingWithParameters_Typing(htParam);
                    }
                    else if (ddlChargeTypeBaseRateCharacter.SelectedValue == "Percentage")
                    {
                        decimal rate = 0;
                        rate = ((Convert.ToDecimal(txtPriceBaseRate.Text) * Convert.ToDecimal(txtPriceBaseRateCharacter.Text)) / 100) + Convert.ToDecimal(txtPriceBaseRate.Text);
                        Hashtable htParam = new Hashtable();
                        htParam.Add("ProjectId", Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"])));
                        htParam.Add("CostingColumn", ddlBillingHeaderBaseRateCharacter.SelectedValue);
                        htParam.Add("RemarkCost", Convert.ToString(rate));
                        htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
                        htParam.Add("CostingColumnValue", Convert.ToString(""));
                        htParam.Add("ChargeType", ddlChargeTypeBaseRateCharacter.SelectedValue);
                        htParam.Add("RateForOthers", Convert.ToString(txtPriceBaseRateCharacter.Text));
                        result = InsertCostingWithParameters_Typing(htParam);
                    }
                }
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
                #endregion
            }
            else if (ddlBillingBase.SelectedItem.Text == "Product Type")
            {
                #region Product Type
                if (ddlIsApplicablePurchase.SelectedValue == "Yes")
                {
                    Hashtable htParam = new Hashtable();
                    htParam.Add("ProjectId", Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"])));
                    htParam.Add("CostingColumn", ddlBillingHeaderPurchase.SelectedValue);
                    htParam.Add("RemarkCost", Convert.ToString(txtPricePurchase.Text));
                    htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
                    htParam.Add("CostingColumnValue", "Purchase");
                    htParam.Add("ChargeType", ddlChargeTypePurchase.SelectedValue);
                    htParam.Add("RateForOthers", Convert.ToString(txtPricePurchase.Text));
                    result = InsertCostingWithParameters_Typing(htParam);
                }
                if (ddlIsApplicableRefinance.SelectedValue == "Yes")
                {
                    Hashtable htParam = new Hashtable();
                    htParam.Add("ProjectId", Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"])));
                    htParam.Add("CostingColumn", ddlBillingHeaderRefinance.SelectedValue);
                    htParam.Add("RemarkCost", Convert.ToString(txtPriceRefinance.Text));
                    htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
                    htParam.Add("CostingColumnValue", "Refinance");
                    htParam.Add("ChargeType", ddlChargeTypeRefinance.SelectedValue);
                    htParam.Add("RateForOthers", Convert.ToString(txtPriceRefinance.Text));
                    result = InsertCostingWithParameters_Typing(htParam);
                }
                if (ddlIsApplicableCurrentOwner.SelectedValue == "Yes")
                {
                    Hashtable htParam = new Hashtable();
                    htParam.Add("ProjectId", Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"])));
                    htParam.Add("CostingColumn", ddlBillingHeaderCurrentOwner.SelectedValue);
                    htParam.Add("RemarkCost", Convert.ToString(txtPriceCurrentOwner.Text));
                    htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
                    htParam.Add("CostingColumnValue", "Current Owner");
                    htParam.Add("ChargeType", ddlChargeTypeCurrentOwner.SelectedValue);
                    htParam.Add("RateForOthers", Convert.ToString(txtPriceCurrentOwner.Text));
                    result = InsertCostingWithParameters_Typing(htParam);
                }
                if (ddlIsApplicableFullSearch.SelectedValue == "Yes")
                {
                    Hashtable htParam = new Hashtable();
                    htParam.Add("ProjectId", Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"])));
                    htParam.Add("CostingColumn", ddlBillingHeaderFullSearch.SelectedValue);
                    htParam.Add("RemarkCost", Convert.ToString(txtPriceFullSearch.Text));
                    htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
                    htParam.Add("CostingColumnValue", "Full Search");
                    htParam.Add("ChargeType", ddlChargeTypeFullSearch.SelectedValue);
                    htParam.Add("RateForOthers", Convert.ToString(txtPriceFullSearch.Text));
                    result = InsertCostingWithParameters_Typing(htParam);
                }
                if (ddlIsApplicableTwoOwner.SelectedValue == "Yes")
                {
                    Hashtable htParam = new Hashtable();
                    htParam.Add("ProjectId", Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"])));
                    htParam.Add("CostingColumn", ddlBillingHeaderTwoOwner.SelectedValue);
                    htParam.Add("RemarkCost", Convert.ToString(txtPriceTwoOwner.Text));
                    htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
                    htParam.Add("CostingColumnValue", "Two Owner");
                    htParam.Add("ChargeType", ddlChargeTypeTwoOwner.SelectedValue);
                    htParam.Add("RateForOthers", Convert.ToString(txtPriceTwoOwner.Text));
                    result = InsertCostingWithParameters_Typing(htParam);
                }
                if (ddlIsApplicableCharacter.SelectedValue == "Yes")
                {
                    Hashtable htParam = new Hashtable();
                    htParam.Add("ProjectId", Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"])));
                    htParam.Add("CostingColumn", ddlBillingHeaderCharacterNew.SelectedValue);
                    htParam.Add("RemarkCost", Convert.ToString(txtPriceCharacter.Text));
                    htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
                    htParam.Add("CostingColumnValue", "");
                    htParam.Add("ChargeType", ddlChargeTypeCharacter.SelectedValue);
                    htParam.Add("RateForOthers", Convert.ToString(txtPriceCharacter.Text));
                    result = InsertCostingWithParameters_Typing(htParam);
                }
                
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
                #endregion
            }
            else if (ddlBillingBase.SelectedItem.Text == "Order Type")
            {
                #region Order Type
                if (ddlIsApplicableOrderBase.SelectedValue == "Yes")
                {
                    Hashtable htParam = new Hashtable();
                    htParam.Add("ProjectId", Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"])));
                    htParam.Add("CostingColumn", ddlBillingHeaderOrderBase.SelectedValue);
                    htParam.Add("RemarkCost", Convert.ToString(txtPriceOrderBase.Text));
                    htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
                    htParam.Add("CostingColumnValue", Convert.ToString(hdOrderBaseLabel.Value));
                    htParam.Add("ChargeType", ddlChargeTypeOrderBase.SelectedValue);
                    htParam.Add("RateForOthers", Convert.ToString(txtPriceOrderBase.Text));
                    result = InsertCostingWithParameters_Typing(htParam);
                }
                if (ddlIsApplicableOrderOther.SelectedValue == "Yes")
                {
                    if (ddlChargeTypeOrderOther.SelectedValue == "Fix Amount")
                    {
                        Hashtable htParam = new Hashtable();
                        htParam.Add("ProjectId", Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"])));
                        htParam.Add("CostingColumn", ddlBillingHeaderOrderOther.SelectedValue);
                        htParam.Add("RemarkCost", Convert.ToString(txtPriceOrderOther.Text));
                        htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
                        htParam.Add("CostingColumnValue", Convert.ToString(hdOrderOtherLabel.Value));
                        htParam.Add("ChargeType", ddlChargeTypeOrderOther.SelectedValue);
                        htParam.Add("RateForOthers", Convert.ToString(txtPriceOrderOther.Text));
                        result = InsertCostingWithParameters_Typing(htParam);
                    }
                    else if (ddlChargeTypeOrderOther.SelectedValue == "Incremental")
                    {
                        decimal rate = 0;
                        rate = Convert.ToDecimal(txtPriceOrderBase.Text) + Convert.ToDecimal(txtPriceOrderOther.Text);
                        Hashtable htParam = new Hashtable();
                        htParam.Add("ProjectId", Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"])));
                        htParam.Add("CostingColumn", ddlBillingHeaderOrderOther.SelectedValue);
                        htParam.Add("RemarkCost", Convert.ToString(rate));
                        htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
                        htParam.Add("CostingColumnValue", Convert.ToString(hdOrderOtherLabel.Value));
                        htParam.Add("ChargeType", ddlChargeTypeOrderOther.SelectedValue);
                        htParam.Add("RateForOthers", Convert.ToString(txtPriceOrderOther.Text));
                        result = InsertCostingWithParameters_Typing(htParam);
                    }
                    else if (ddlChargeTypeOrderOther.SelectedValue == "Percentage")
                    {
                        decimal rate = 0;
                        rate = ((Convert.ToDecimal(txtPriceOrderBase.Text) * Convert.ToDecimal(txtPriceOrderOther.Text)) / 100) + Convert.ToDecimal(txtPriceOrderBase.Text);
                        Hashtable htParam = new Hashtable();
                        htParam.Add("ProjectId", Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"])));
                        htParam.Add("CostingColumn", ddlBillingHeaderOrderOther.SelectedValue);
                        htParam.Add("RemarkCost", Convert.ToString(rate));
                        htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
                        htParam.Add("CostingColumnValue", Convert.ToString(hdOrderOtherLabel.Value));
                        htParam.Add("ChargeType", ddlChargeTypeOrderOther.SelectedValue);
                        htParam.Add("RateForOthers", Convert.ToString(txtPriceOrderOther.Text));
                        result = InsertCostingWithParameters_Typing(htParam);
                    }
                }
                if (ddlIsApplicableOrderCharacter.SelectedValue == "Yes")
                {
                    if (ddlChargeTypeOrderCharacter.SelectedValue == "Fix Amount")
                    {
                        Hashtable htParam = new Hashtable();
                        htParam.Add("ProjectId", Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"])));
                        htParam.Add("CostingColumn", ddlBillingHEaderOrderCharacter.SelectedValue);
                        htParam.Add("RemarkCost", Convert.ToString(txtPriceOrderCharacter.Text));
                        htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
                        htParam.Add("CostingColumnValue", Convert.ToString(""));
                        htParam.Add("ChargeType", ddlChargeTypeOrderCharacter.SelectedValue);
                        htParam.Add("RateForOthers", Convert.ToString(txtPriceOrderCharacter.Text));
                        result = InsertCostingWithParameters_Typing(htParam);
                    }
                    else if (ddlChargeTypeOrderCharacter.SelectedValue == "Incremental")
                    {
                        decimal rate = 0;
                        rate = Convert.ToDecimal(txtPriceOrderBase.Text) + Convert.ToDecimal(txtPriceOrderCharacter.Text);
                        Hashtable htParam = new Hashtable();
                        htParam.Add("ProjectId", Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"])));
                        htParam.Add("CostingColumn", ddlBillingHEaderOrderCharacter.SelectedValue);
                        htParam.Add("RemarkCost", Convert.ToString(rate));
                        htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
                        htParam.Add("CostingColumnValue", Convert.ToString(""));
                        htParam.Add("ChargeType", ddlChargeTypeOrderCharacter.SelectedValue);
                        htParam.Add("RateForOthers", Convert.ToString(txtPriceOrderCharacter.Text));
                        result = InsertCostingWithParameters_Typing(htParam);
                    }
                    else if (ddlChargeTypeOrderCharacter.SelectedValue == "Percentage")
                    {
                        decimal rate = 0;
                        rate = ((Convert.ToDecimal(txtPriceOrderBase.Text) * Convert.ToDecimal(txtPriceOrderCharacter.Text)) / 100) + Convert.ToDecimal(txtPriceOrderBase.Text);
                        Hashtable htParam = new Hashtable();
                        htParam.Add("ProjectId", Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"])));
                        htParam.Add("CostingColumn", ddlBillingHEaderOrderCharacter.SelectedValue);
                        htParam.Add("RemarkCost", Convert.ToString(rate));
                        htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
                        htParam.Add("CostingColumnValue", Convert.ToString(""));
                        htParam.Add("ChargeType", ddlChargeTypeOrderCharacter.SelectedValue);
                        htParam.Add("RateForOthers", Convert.ToString(txtPriceOrderCharacter.Text));
                        result = InsertCostingWithParameters_Typing(htParam);
                    }
                }
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
                #endregion
            }
            else if (ddlBillingBase.SelectedItem.Text == "Character Based")
            {
                #region Character Based
                Hashtable htParam = new Hashtable();
                htParam.Add("ProjectId", Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"])));
                htParam.Add("CostingColumn", ddlBillingHeaderCharacter.SelectedValue);
                htParam.Add("RemarkCost", Convert.ToString(txtPricePerCharacter.Text));
                htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
                htParam.Add("CostingColumnValue", Convert.ToString(""));
                htParam.Add("ChargeType", "Fix Amount");
                htParam.Add("RateForOthers", Convert.ToString(txtPerCharacter.Text));
                result = InsertCostingWithParameters_Typing(htParam);
                #endregion
            }
            else if (ddlBillingBase.SelectedItem.Text == "Page Based")
            {
                #region Page Based
                Hashtable htParam = new Hashtable();
                htParam.Add("ProjectId", Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"])));
                htParam.Add("CostingColumn", ddlBillingHeader771ScanPages.SelectedValue);
                htParam.Add("RemarkCost", Convert.ToString(txtPrice771ScanPages.Text));
                htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
                htParam.Add("CostingColumnValue", Convert.ToString(""));
                htParam.Add("ChargeType", "Fix Amount");
                htParam.Add("RateForOthers", Convert.ToString(txtPrice771ScanPages.Text));
                result = InsertCostingWithParameters_Typing(htParam);
                #endregion
            }
            BindTypingCosting(Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"])));
        }

        protected void ddlChargeType_Typing_Init(object sender, EventArgs e)
        {

        }

        protected void txtRemark_Typing_Init(object sender, EventArgs e)
        {

        }

        protected void grdBillingParams_Typing_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
        {

        }

        protected void grdBillingParams_Typing_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

        }

        protected void grdBillingParams_Typing_BeforePerformDataSelect(object sender, EventArgs e)
        {

        }


    }
}