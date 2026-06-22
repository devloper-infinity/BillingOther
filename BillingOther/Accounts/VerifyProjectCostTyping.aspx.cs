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
    public partial class VerifyProjectCostTyping : System.Web.UI.Page
    {
        bllTracking bllMaster = new bllTracking();
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl h4 = (HtmlGenericControl)Master.FindControl("lblHeader");
            h4.InnerText = "Verify Project Cost";
            if (!Page.IsPostBack)
            {
                bindgrid();
                //BindCostDetails(int.Parse(Request.QueryString["ProjectId"]));
                BindBaseProductType(Convert.ToString(Request.QueryString["ProjectId"]));
                BindTypingCosting(int.Parse(Request.QueryString["ProjectId"]));
            }

        }

        protected void grdBillingParams_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters == "update")
            {
                for (int i = 0; i < 10; i++)
                {
                    string Id = "";// Convert.ToString(list[i].Id);
                    string Comment = "";//Convert.ToString(list[i].Comment);
                    string Additional = "";//Convert.ToString(list[i].Additional);
                    string Remark = "";// Convert.ToString(list[i].Remark);
                    string ChargeType = "";// Convert.ToString(list[i].ChargeType);

                    if (Comment != "")
                    {
                        Hashtable Htparam = new Hashtable();
                        Htparam.Add("IBV_ParameterId", Convert.ToString(Id));
                        Htparam.Add("IBV_Comment", Convert.ToString(Comment));
                        Htparam.Add("IBV_Additional", Convert.ToString(Additional));
                        Htparam.Add("IBV_Remark", Convert.ToString(Remark));
                        Htparam.Add("IBV_ChargeType", Convert.ToString(ChargeType));
                        Htparam.Add("AddedBy", Convert.ToInt32(HttpContext.Current.User.Identity.Name));
                        Htparam.Add("ProjectId", Convert.ToInt32(Convert.ToString(Request.QueryString["ProjectId"])));
                        int result = bllMaster.ApproveBillParameter(Htparam);
                        //if (result >= 1)
                        if (result > 0)
                        {
                            ((ASPxGridView)sender).JSProperties["cp_message"] = "1";
                            break;
                        }
                        else
                        {
                            ((ASPxGridView)sender).JSProperties["cp_message"] = "0";
                        }

                    }
                }
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
            DataTable dt = GetCostingDetailsTyping(ProjectId);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    string BillingBase = Convert.ToString(dt.Rows[0]["BillingBase"]);
                    if (BillingBase == "Base")
                    {
                        ddlBillingBase.SelectedIndex = 1;
                        trBaseRate.Style.Add("display", "");
                        trProductType.Style.Add("display", "none");
                        trOrderType.Style.Add("display", "none");

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
                    }
                    else if (BillingBase == "Product Type")
                    {
                        ddlBillingBase.SelectedIndex = 2;

                        trBaseRate.Style.Add("display", "none");
                        trProductType.Style.Add("display", "");
                        trOrderType.Style.Add("display", "none");
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
                      
                    }
                    else if (BillingBase == "Order Type")
                    {
                        ddlBillingBase.SelectedIndex = 3;

                        trBaseRate.Style.Add("display", "none");
                        trProductType.Style.Add("display", "none");
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
                    }
                    else if (BillingBase == "Character Based")
                    {
                        ddlBillingBase.SelectedIndex = 4;

                        trBaseRate.Style.Add("display", "none");
                        trProductType.Style.Add("display", "none");
                        trOrderType.Style.Add("display", "none");
                        trCharacter.Style.Add("display", "");
                        string find = "ProjectColumn ='# of Character'";
                        DataRow[] foundRows = dt.Select(find);
                        if (foundRows.Length > 0)
                        {
                            txtPerCharacter.Text = Convert.ToString(foundRows[0]["RateForOthers"]);
                            txtPricePerCharacter.Text = Convert.ToString(foundRows[0]["Rate"]);
                            ddlBillingHeaderCharacter.SelectedValue = Convert.ToString(foundRows[0]["ProjectColumn"]);
                        }
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

        protected void btnShubmitTyping_Click(object sender, EventArgs e)
        {
           
            Hashtable htParam = new Hashtable();
            htParam.Add("ProjectId", Convert.ToInt32(Convert.ToString(Request.QueryString["ProjectId"])));
            htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
            int result = ApproveBillParameter_Typing(htParam);
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

            BindTypingCosting(Convert.ToInt32(Convert.ToString(Request.QueryString["ERPProjectId"])));
        }

        public int ApproveBillParameter_Typing(Hashtable htParam)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "[usp_ApproveBillingParametersForCM_Typing]");
            SQLHelper.AddParamToSQLCmd(cmd, "@ProjectId", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.Input, htParam["ProjectId"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@AddedBy", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.Input, htParam["AddedBy"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@ReturnValue", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.ReturnValue, null);
            SQLHelper.ExecuteNonQueryCmdBilling(cmd);

            int ReturnValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
            return ReturnValue;
        }
    }
}