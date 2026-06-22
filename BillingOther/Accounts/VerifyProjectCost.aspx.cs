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
    public partial class VerifyProjectCost : System.Web.UI.Page
    {
        List<Record> list = new List<Record>();
        bllTracking bllMaster = new bllTracking();
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl h4 = (HtmlGenericControl)Master.FindControl("lblHeader");
            h4.InnerText = "Verify Project Cost";
            if (!Page.IsPostBack)
            {
                if (Convert.ToString(Request.QueryString["ProjectId"]) == "426" || Convert.ToString(Request.QueryString["ProjectId"]) == "87" || Convert.ToString(Request.QueryString["ProjectId"]) == "184" || Convert.ToString(Request.QueryString["ProjectId"]) == "440" || Convert.ToString(Request.QueryString["ProjectId"]) == "435" || Convert.ToString(Request.QueryString["ProjectId"]) == "205" || Convert.ToString(Request.QueryString["ProjectId"]) == "203" || Convert.ToString(Request.QueryString["ProjectId"]) == "400" || Convert.ToString(Request.QueryString["ProjectId"]) == "353" || Convert.ToString(Request.QueryString["ProjectId"]) == "123" || Convert.ToString(Request.QueryString["ProjectId"]) == "354" || Convert.ToString(Request.QueryString["ProjectId"]) == "203" || Convert.ToString(Request.QueryString["ProjectId"]) == "392" || Convert.ToString(Request.QueryString["ProjectId"]) == "386" || Convert.ToString(Request.QueryString["ProjectId"]) == "280" || Convert.ToString(Request.QueryString["ProjectId"]) == "337" || Convert.ToString(Request.QueryString["ProjectId"]) == "388" || Convert.ToString(Request.QueryString["ProjectId"]) == "373" || Convert.ToString(Request.QueryString["ProjectId"]) == "385" || Convert.ToString(Request.QueryString["ProjectId"]) == "393" || Convert.ToString(Request.QueryString["ProjectId"]) == "395" || Convert.ToString(Request.QueryString["ProjectId"]) == "352" || Convert.ToString(Request.QueryString["ProjectId"]) == "155")
                {
                }
                else
                {
                    bindgrid();
                }
                try
                {
                    BindFTECosting(int.Parse(Request.QueryString["ProjectId"]));
                }
                catch { }

            }
            if (Convert.ToString(Request.QueryString["ProjectId"]) == "426" || Convert.ToString(Request.QueryString["DomainId"]) == "36" || Convert.ToString(Request.QueryString["DomainId"]) == "34" || Convert.ToString(Request.QueryString["DomainId"]) == "7" || Convert.ToString(Request.QueryString["ProjectId"]) == "87" || Convert.ToString(Request.QueryString["ProjectId"]) == "40" || Convert.ToString(Request.QueryString["ProjectId"]) == "414" || Convert.ToString(Request.QueryString["ProjectId"]) == "435" || Convert.ToString(Request.QueryString["ProjectId"]) == "440" || Convert.ToString(Request.QueryString["ProjectId"]) == "50" || Convert.ToString(Request.QueryString["ProjectId"]) == "155" || Convert.ToString(Request.QueryString["ProjectId"]) == "51" || Convert.ToString(Request.QueryString["ProjectId"]) == "391" || Convert.ToString(Request.QueryString["ProjectId"]) == "184" || Convert.ToString(Request.QueryString["ProjectId"]) == "205" || Convert.ToString(Request.QueryString["ProjectId"]) == "203" || Convert.ToString(Request.QueryString["ProjectId"]) == "400" || Convert.ToString(Request.QueryString["ProjectId"]) == "353" || Convert.ToString(Request.QueryString["ProjectId"]) == "123" || Convert.ToString(Request.QueryString["ProjectId"]) == "354" || Convert.ToString(Request.QueryString["ProjectId"]) == "203" || Convert.ToString(Request.QueryString["ProjectId"]) == "392" || Convert.ToString(Request.QueryString["ProjectId"]) == "386" || Convert.ToString(Request.QueryString["ProjectId"]) == "280" || Convert.ToString(Request.QueryString["ProjectId"]) == "337" || Convert.ToString(Request.QueryString["ProjectId"]) == "388" || Convert.ToString(Request.QueryString["ProjectId"]) == "373" || Convert.ToString(Request.QueryString["ProjectId"]) == "385" || Convert.ToString(Request.QueryString["ProjectId"]) == "393" || Convert.ToString(Request.QueryString["ProjectId"]) == "39" || Convert.ToString(Request.QueryString["ProjectId"]) == "395" || Convert.ToString(Request.QueryString["ProjectId"]) == "352")
            {
                if (Convert.ToString(Request.QueryString["ProjectId"]) == "353" || Convert.ToString(Request.QueryString["ProjectId"]) == "123" || Convert.ToString(Request.QueryString["ProjectId"]) == "435" || Convert.ToString(Request.QueryString["ProjectId"]) == "440" || Convert.ToString(Request.QueryString["ProjectId"]) == "155" || Convert.ToString(Request.QueryString["ProjectId"]) == "354" || Convert.ToString(Request.QueryString["ProjectId"]) == "392" || Convert.ToString(Request.QueryString["ProjectId"]) == "386" || Convert.ToString(Request.QueryString["ProjectId"]) == "280" || Convert.ToString(Request.QueryString["ProjectId"]) == "393" || Convert.ToString(Request.QueryString["ProjectId"]) == "434" || Convert.ToString(Request.QueryString["ProjectId"]) == "442")
                {
                    tdRate.InnerHtml = "<b>Rate Per FTE : </b>";
                }
                else if (Convert.ToString(Request.QueryString["ProjectId"]) == "184" || Convert.ToString(Request.QueryString["ProjectId"]) == "205" || Convert.ToString(Request.QueryString["ProjectId"]) == "203" || Convert.ToString(Request.QueryString["ProjectId"]) == "400" || Convert.ToString(Request.QueryString["ProjectId"]) == "205" || Convert.ToString(Request.QueryString["ProjectId"]) == "373" || Convert.ToString(Request.QueryString["ProjectId"]) == "373" || Convert.ToString(Request.QueryString["ProjectId"]) == "385")
                {
                    tdRate.InnerHtml = "<b>Hourly Rate : </b>";
                }
                else
                {
                    tdRate.InnerHtml = "<b>Base Rate : </b>";
                }
                dvFTE.Style.Add("display", "");
                dvGen.Style.Add("display", "none");
                if (Convert.ToString(Request.QueryString["ProjectId"]) == "39")
                {
                    tblOtherFTE.Style.Add("display", "none");
                    tbl861007.Style.Add("display", "");
                }
                else
                {
                    tblOtherFTE.Style.Add("display", "");
                    tbl861007.Style.Add("display", "none");
                }
            }
            else
            {
                dvFTE.Style.Add("display", "none");
                dvGen.Style.Add("display", "");
            }
            try
            {
                if (Convert.ToString(Request.QueryString["ProjectId"]) != "87" && Convert.ToString(Request.QueryString["ProjectId"]) != "426")
                {
                    int start = grdBillingParams.PageIndex * grdBillingParams.SettingsPager.PageSize;
                    int end = (grdBillingParams.PageIndex + 1) * grdBillingParams.SettingsPager.PageSize;
                    GridViewDataColumn column1 = grdBillingParams.Columns["IBV_Comment"] as GridViewDataColumn;
                    GridViewDataColumn column2 = grdBillingParams.Columns["IBV_Additional"] as GridViewDataColumn;
                    GridViewDataColumn column3 = grdBillingParams.Columns["IBV_Remark"] as GridViewDataColumn;
                    GridViewDataColumn column4 = grdBillingParams.Columns["IBV_ChargeType"] as GridViewDataColumn;

                    for (int i = start; i < end; i++)
                    {
                        try
                        {
                            DropDownList ddlCompany = (DropDownList)grdBillingParams.FindRowCellTemplateControl(i, column1, "ddlCompany");
                            DropDownList ddlLocation = (DropDownList)grdBillingParams.FindRowCellTemplateControl(i, column2, "ddlLocation");
                            TextBox txtRemark = (TextBox)grdBillingParams.FindRowCellTemplateControl(i, column3, "txtRemark");
                            DropDownList ddlChargeType = (DropDownList)grdBillingParams.FindRowCellTemplateControl(i, column4, "ddlChargeType");
                            int id = Convert.ToInt32(grdBillingParams.GetRowValues(i, grdBillingParams.KeyFieldName));
                            list.Add(new Record(id, ddlCompany.SelectedValue, ddlLocation.SelectedValue, txtRemark.Text, ddlChargeType.SelectedValue));
                        }
                        catch
                        { }
                    }
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
        }
        protected void grdBillingParams_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Number")
            {
                e.Value = string.Format("{0}", e.ListSourceRowIndex + 1);
            }
        }

        protected void ddlChargeType_Init(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            GridViewDataItemTemplateContainer container = ddl.NamingContainer as GridViewDataItemTemplateContainer;
            if (Convert.ToString(DataBinder.Eval(container.DataItem, "IBV_ChargeType")) == "0" || Convert.ToString(DataBinder.Eval(container.DataItem, "IBV_ChargeType")) == "")
                ddl.SelectedIndex = 0;
            else
                ddl.SelectedValue = DataBinder.Eval(container.DataItem, "IBV_ChargeType").ToString();
        }

        protected void grdBillingParams_BeforePerformDataSelect(object sender, EventArgs e)
        {
            if (Convert.ToString(Request.QueryString["ProjectId"]) != "426")
            {
                if (Convert.ToString(Request.QueryString["ProjectId"]) != "")
                {
                    Session["ProjectId"] = Convert.ToInt32(Convert.ToString(Request.QueryString["ProjectId"]));
                }
                else { Session["ProjectId"] = 0; }
                Session["formdate"] = Convert.ToInt32(Convert.ToString(Request.QueryString["DomainId"]));
            }
            else
            {
                Session["ProjectId"] = 0;
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

            public Record(int id, string comment, string additional, string remark, string chargetype)
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
            SQLHelper.AddParamToSQLCmd(cmd, "@DomainId", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.Input, Convert.ToInt32(Convert.ToString(Request.QueryString["DomainId"])));

            SQLHelper.AddParamToSQLCmd(cmd, "@ProjectID", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.Input, Convert.ToInt32(Convert.ToString(Request.QueryString["ProjectId"])));
            DataTable dt = SQLHelper.ExecuteDataTableCmd_billing(cmd);
            grdBillingParams.DataSource = dt;
            grdBillingParams.DataBind();
            return dt;
        }
        #endregion
        public void bindgrid()
        {

            DataTable dt = bllMaster.BindVerifyProjectById(int.Parse(Request.QueryString["ProjectId"]));
            ASPxReoprt.DataSource = dt;
            ASPxReoprt.DataBind();
            //clear();
        }
        protected void ASPxReoprt_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {

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

        protected void btnFTESubmit_Click(object sender, EventArgs e)
        {
            Hashtable htParam = new Hashtable();
            htParam.Add("ProjectId", Convert.ToInt32(Convert.ToString(Request.QueryString["ProjectId"])));
            htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
            int result = ApproveBillParameter_FTE(htParam);
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
            BindFTECosting(Convert.ToInt32(Convert.ToString(Request.QueryString["ProjectId"])));
        }

        public int ApproveBillParameter_FTE(Hashtable htParam)
        {
            SqlCommand cmd = SQLHelper.GetCommand(System.Data.CommandType.StoredProcedure, "[usp_ApproveBillingParametersForCM_FTE]");
            SQLHelper.AddParamToSQLCmd(cmd, "@ProjectId", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.Input, htParam["ProjectId"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@AddedBy", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.Input, htParam["AddedBy"]);
            SQLHelper.AddParamToSQLCmd(cmd, "@ReturnValue", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.ReturnValue, null);
            SQLHelper.ExecuteNonQueryCmdBilling(cmd);

            int ReturnValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
            return ReturnValue;
        }

        protected void btnSubmit861_Click(object sender, EventArgs e)
        {
            Hashtable htParam = new Hashtable();
            htParam.Add("ProjectId", Convert.ToInt32(Convert.ToString(Request.QueryString["ProjectId"])));
            htParam.Add("AddedBy", int.Parse(HttpContext.Current.User.Identity.Name.ToString()));
            int result = ApproveBillParameter_FTE(htParam);
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
            BindFTECosting(Convert.ToInt32(Convert.ToString(Request.QueryString["ProjectId"])));
        }
    }
}