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
using BillingOther.App_Code.DAL;
using BillingOther.App_Code.BLL;

namespace BillingOther.Accounts
{
    public partial class VerifyProjectCostFreight : System.Web.UI.Page
    {
        bllTracking bllMaster = new bllTracking();
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl h4 = (HtmlGenericControl)Master.FindControl("lblHeader");
            h4.InnerText = "Verify Project Cost";

            if (Convert.ToString(Request.Url).Contains("ProjectId"))
            {
                if (Convert.ToInt32(Request.QueryString["ProjectId"]) == 48)
                {
                    tbl771.Style.Add("display", "");
                    tblOther.Style.Add("display", "none");
                }
                else
                {
                    tbl771.Style.Add("display", "none");
                    tblOther.Style.Add("display", "");
                }
            }
            if (!Page.IsPostBack)
            {
                bindgrid();
                BindBillingHeaderBase(Convert.ToInt32(Request.QueryString["ProjectId"]));
                BindFreightCosting(int.Parse(Request.QueryString["ProjectId"]));
            }

        }
        public void BindBillingHeaderBase(int ProjectID)
        {
            if (ProjectID == 46 || ProjectID == 227)
            {
                ddlBillingHeaderBaseRate.Items.Clear();
                ddlBillingHeaderBaseRate.Items.Insert(0, new ListItem("Select"));
                ddlBillingHeaderBaseRate.Items.Insert(1, new ListItem("No of Invoices Delivered"));
            }

        }
        public void bindgrid()
        {

            DataTable dt = bllMaster.BindVerifyProjectById(int.Parse(Request.QueryString["ProjectId"]));
            ASPxReoprt.DataSource = dt;
            ASPxReoprt.DataBind();
            //clear();
        }

        protected void ASPxReoprt_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Number")
            {
                e.Value = string.Format("{0}", e.ListSourceRowIndex + 1);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            //Session["CostId"] = null;
            Response.Redirect("~/Accounts/VerifyCostMaster.aspx");
        }

        public void BindFreightCosting(int ProjectId)
        {
            DataTable dt = GetCostingDetailsFreight(ProjectId);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    string BillingBase = Convert.ToString(dt.Rows[0]["BillingBase"]);
                    if (ProjectId == 48)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //No of Pages - Scan
                            if (Convert.ToString(dt.Rows[i]["ProjectColumn"]) == "No of Pages")
                            {
                                ddlChargeType771ScanPages.SelectedValue = Convert.ToString(dt.Rows[i]["Updatelevel"]);
                                txtPrice771ScanPages.Text = Convert.ToString(dt.Rows[i]["Rate"]);
                                ddlBillingHeader771ScanPages.SelectedValue = Convert.ToString(dt.Rows[i]["ProjectColumn"]);
                            }
                            else if (Convert.ToString(dt.Rows[i]["ProjectColumn"]) == "No of Records" && Convert.ToString(dt.Rows[i]["ConditionalValue"]) == "Scan")
                            {
                                //No of Records - Scan
                                ddlChargeType771ScanRecords.SelectedValue = Convert.ToString(dt.Rows[i]["Updatelevel"]);
                                txtPrice771ScanRecords.Text = Convert.ToString(dt.Rows[i]["Rate"]);
                                ddlBillingHeader771ScanRecords.SelectedValue = Convert.ToString(dt.Rows[i]["ProjectColumn"]);
                            }
                            else if (Convert.ToString(dt.Rows[i]["ProjectColumn"]) == "No of Records" && Convert.ToString(dt.Rows[i]["ConditionalValue"]) == "Mail")
                            {
                                //No of Records - Mail
                                ddlChargeType771MailRecords.SelectedValue = Convert.ToString(dt.Rows[i]["Updatelevel"]);
                                txtPrice771MailRecords.Text = Convert.ToString(dt.Rows[i]["Rate"]);
                                ddlBillingHeader771MailRecords.SelectedValue = Convert.ToString(dt.Rows[i]["ProjectColumn"]);
                            }
                        }
                    }
                    else if (BillingBase == "Base")
                    {
                        ddlBillingBase.SelectedIndex = 1;
                        trBaseRate.Style.Add("display", "");
                        trOrderType.Style.Add("display", "none");

                        string find = "ProjectColumn in ('Order#','Dispatched Record','No Of Records','No of Invoices Delivered') and AdditionalConditions=0 ";
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
                    }

                    else if (BillingBase == "Invoice Type")
                    {
                        ddlBillingBase.SelectedIndex = 3;

                        trBaseRate.Style.Add("display", "none");
                        trOrderType.Style.Add("display", "");

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
                        if (foundRows.Length > 0)
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

        public DataTable GetCostingDetailsFreight(int ProjectID)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "usp_GetFreightProjectCosting");
            SQLHelper.AddParamToSQLCmd(cmd, "@ProjectID", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.Input, ProjectID);
            DataTable dt = SQLHelper.ExecuteDataTableCmd_billing(cmd);
            return dt;
        }
        #endregion

        public int InsertCostingWithParameters_Freight(Hashtable htParam)
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

        protected void btnShubmitTyping_Click(object sender, EventArgs e)
        {
            Hashtable htParam = new Hashtable();
            htParam.Add("ProjectId", Convert.ToInt32(Convert.ToString(Request.QueryString["ProjectId"])));
            htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
            int result = ApproveBillParameter_Freight(htParam);
            if (result > 0)
            {
                dvError.Style.Add("display", "");
                dvError.Attributes.Add("class", "alert alert-success background-success");
                dvError.InnerHtml = "Costing approved successfully!";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);              
            }
            else
            {
                dvError.Style.Add("display", "");
                dvError.Attributes.Add("class", "alert alert-danger background-danger");
                dvError.InnerHtml = "Error occured while approving costing!";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);                
            }
            BindFreightCosting(Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"])));
        }

        public int ApproveBillParameter_Freight(Hashtable htParam)
        {
            string ProcedureName = "";
            if (Convert.ToString(htParam["ProjectId"]) == "227")
                ProcedureName = "usp_ApproveBillingParametersForCM_Freight736";
            else
                ProcedureName = "usp_ApproveBillingParametersForCM_Freight";
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, ProcedureName);
            SQLHelper.AddParamToSQLCmd(cmd, "@ProjectId", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.Input, htParam["ProjectId"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@AddedBy", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.Input, htParam["AddedBy"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@ReturnValue", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.ReturnValue, null);
            SQLHelper.ExecuteNonQueryCmdBilling(cmd);

            int ReturnValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
            return ReturnValue;
        }

        protected void btnSubmit771_Click(object sender, EventArgs e)
        {
            Hashtable htParam = new Hashtable();
            htParam.Add("ProjectId", Convert.ToInt32(Convert.ToString(Request.QueryString["ProjectId"])));
            htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
            int result = ApproveBillParameter_Freight(htParam);
            if (result > 0)
            {
                dvError.Style.Add("display", "");
                dvError.Attributes.Add("class", "alert alert-success background-success");
                dvError.InnerHtml = "Costing approved successfully!";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);             
            }
            else
            {
                dvError.Style.Add("display", "");
                dvError.Attributes.Add("class", "alert alert-danger background-danger");
                dvError.InnerHtml = "Error occured while approving costing!";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);              
            }
            BindFreightCosting(Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"])));

        }
    }
}