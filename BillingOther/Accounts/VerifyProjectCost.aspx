<%@ Page Title="" Language="C#" MasterPageFile="~/Accounts/Accounts.Master" AutoEventWireup="true" CodeBehind="VerifyProjectCost.aspx.cs" Inherits="BillingOther.Accounts.VerifyProjectCost" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Data.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Data" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>  function HideLabel() {
            // alert("Hi");
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%# dvError.ClientID %>").value.style.display = "none";
      }, seconds * 1000);
        };

        function OnEndCallback() {
            if (grid.cp_message) {
                if (grid.cp_message == "1") {
                    alert("Paramters updated successfully.");
                    grid.cp_message = "";
                    grid.PerformCallback('databind');
                }
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvError" runat="server"></div>

    <asp:Button ID="Button1" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn btn-warning" Style="float: right; margin-right: 150px;" ValidationGroup="1"></asp:Button>
    <div style="width: 100%;">
        <dx:ASPxGridView ID="ASPxReoprt" runat="server" ClientInstanceName="Grid" AutoGenerateColumns="false" KeyFieldName="ProjectId" Theme="Default" OnCustomUnboundColumnData="ASPxReoprt_CustomUnboundColumnData">
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <Columns>
                <dx:GridViewDataTextColumn FieldName="Number" VisibleIndex="0" Caption="Sr.#" Width="20px" UnboundType="String" ReadOnly="true" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="ProjectId" Width="50px" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="ProjectName" VisibleIndex="1" Caption="Project #" Width="50px" ReadOnly="true" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="DomainName" VisibleIndex="2" Caption="Domain" Width="35px" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="ProjectStartDate" VisibleIndex="3" Caption="Start Date" Width="35px" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="BillingCycle" VisibleIndex="4" Caption="Billing Cycle" Width="35px" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="BDM" VisibleIndex="5" Caption="BDM" Width="50px" ReadOnly="true" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="CRM" VisibleIndex="6" Caption="CRM" Width="50px" ReadOnly="true" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="UH" VisibleIndex="7" Caption="Unit Head" Width="50px" ReadOnly="true" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
            </Columns>
        </dx:ASPxGridView>
    </div>
    <hr />
    <div id="dvGen" runat="server" style="display: none;">
        <dx:ASPxGridView ID="grdBillingParams" runat="server" AutoGenerateColumns="false" ClientInstanceName="grid" EnableRowsCache="False" KeyFieldName="IBP_Id" Theme="Default"
            OnCustomUnboundColumnData="grdBillingParams_CustomUnboundColumnData" SettingsPager-PageSize="50" DataSourceID="ds2" OnCustomCallback="grdBillingParams_CustomCallback" OnBeforePerformDataSelect="grdBillingParams_BeforePerformDataSelect">
            <ClientSideEvents EndCallback="OnEndCallback" />
            <SettingsEditing Mode="Inline"></SettingsEditing>
            <Styles Header-Font-Bold="true"></Styles>
            <Columns>
                <dx:GridViewDataTextColumn FieldName="IBP_Id" Caption="AssetId" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Number" VisibleIndex="0" Caption="Sr. #" Width="20px" UnboundType="String" ReadOnly="true"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="IBP_ParameterName" Caption="Parameter Name" CellStyle-Font-Size="13px"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="IBV_Comment" Caption="Is Applicable?">
                    <DataItemTemplate>
                        <asp:DropDownList ID="ddlCompany" runat="server" OnInit="ddlCompany_Init" Width="100px" CssClass="form-control"></asp:DropDownList>
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="IBV_Additional" Caption="Additional Charge" Width="60px">
                    <DataItemTemplate>
                        <asp:DropDownList ID="ddlLocation" runat="server" OnInit="ddlLocation_Init" CssClass="form-control"></asp:DropDownList>
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="IBV_ChargeType" Caption="Charge Type" Width="145px">
                    <DataItemTemplate>
                        <asp:DropDownList ID="ddlChargeType" runat="server" OnInit="ddlChargeType_Init" CssClass="form-control">
                            <asp:ListItem Value="Select">Select</asp:ListItem>
                            <asp:ListItem Value="Fix Amount">Fix Amount</asp:ListItem>
                            <asp:ListItem Value="Incremental">Incremental</asp:ListItem>
                            <asp:ListItem Value="Percentage">Percentage</asp:ListItem>
                        </asp:DropDownList>
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="PriceFromBDM" Caption="Price by BDM" CellStyle-Font-Size="13px"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="IBV_Remark" Caption="Latest Price" Width="60px">
                    <DataItemTemplate>
                        <asp:TextBox ID="txtRemark" runat="server" OnInit="txtRemark_Init" Width="65px" CssClass="form-control"></asp:TextBox>
                    </DataItemTemplate>

                </dx:GridViewDataTextColumn>
            </Columns>
        </dx:ASPxGridView>
        <asp:SqlDataSource ID="ds2" runat="server" SelectCommand="[usp_getallbillingParametersForCostingForCM]" SelectCommandType="StoredProcedure" ConnectionString="Data Source=23.111.175.186;Initial Catalog=InfinityBilling;Persist Security Info=True;User ID=sa;Password=#Cl0ud^$ecure4; Pooling=true; Min Pool Size=1; Max Pool Size=10; Connect Timeout=200; Packet Size=8192">
            <SelectParameters>
                <asp:SessionParameter Name="ProjectID" DbType="Int32" Direction="Input" SessionField="ProjectId" />
                <asp:SessionParameter Name="DomainId" DbType="Int32" Direction="Input" SessionField="formdate" />

            </SelectParameters>
        </asp:SqlDataSource>
        <br />

        <dx:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False" Text="Approve" CssClass="btn btn-primary">
            <ClientSideEvents Click="function(s, e) {grid.PerformCallback('update');}" />
        </dx:ASPxButton>
    </div>
    <div id="dvFTE" runat="server" style="display: none;">
        <table class="table-border-style" id="tblOtherFTE" runat="server">
            <tr>
                <td id="tdRate" runat="server"><b>Hourly Rate:</b></td>
                <td>
                    <asp:TextBox ID="txtHourlyRate" runat="server" CssClass="form-control"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnFTESubmit" runat="server" Text="Submit" OnClick="btnFTESubmit_Click" CssClass="btn btn-primary" />
                </td>
            </tr>
        </table>
        <table class="table-responsive-sm" id="tbl861007" runat="server" style="display: none;">
            <tr>
                <td><b>Rate for First 1200 ballots:</b></td>
                <td>
                    <asp:TextBox ID="txtfirst1200Rate" runat="server" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td><b>Rate for Additional ballots:</b></td>
                <td>
                    <asp:TextBox ID="txtAdditionalBallotsRate" runat="server" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td><b>Wire Transfer Charges:</b></td>
                <td>
                    <asp:TextBox ID="txtWireTransferCharges" runat="server" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btnSubmit861" runat="server" Text="Submit" OnClick="btnSubmit861_Click" CssClass="btn btn-primary" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
