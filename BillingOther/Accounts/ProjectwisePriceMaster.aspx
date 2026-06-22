<%@ Page Title="" Language="C#" MasterPageFile="~/Accounts/Accounts.Master" AutoEventWireup="true" CodeBehind="ProjectwisePriceMaster.aspx.cs" Inherits="BillingOther.Accounts.ProjectwisePriceMaster" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="table-condensed">
        <tr>
            <td>
                <asp:DropDownList ID="ddlDomain" runat="server" CssClass="form-control" Width="250px"></asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" CssClass="btn btn-primary" />
            </td>
        </tr>
    </table>
    <br />
    <dx:ASPxButton runat="server" ID="btnCollapse1" Text="Collapse All Rows" UseSubmitBehavior="false"
        AutoPostBack="false" CssClass="btn btn-default">
        <ClientSideEvents Click="function() { grid.CollapseAll() }" />
    </dx:ASPxButton>
    &nbsp;&nbsp;
                    <dx:ASPxButton runat="server" ID="btnExpand1" Text="Expand All Rows" UseSubmitBehavior="false"
                        AutoPostBack="false" CssClass="btn btn-default">
                        <ClientSideEvents Click="function() { grid.ExpandAll() }" />
                    </dx:ASPxButton>
    &nbsp;&nbsp;
    <asp:Button ID="btnExportToExcel" runat="server" Text="Export To Excel" OnClick="btnExportToExcel_Click" CssClass="btn btn-default" /><br />
    <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grdPricing" FileName="Projectwise Price Details"></dx:ASPxGridViewExporter>
    <dx:ASPxGridView ID="grdPricing" runat="server" Width="100%" ClientInstanceName="grid" AutoGenerateColumns="false" KeyFieldName="ProjectId" Theme="Default" OnCustomColumnDisplayText="grdPricing_CustomColumnDisplayText" OnBeforeGetCallbackResult="grdPricing_BeforeGetCallbackResult">
        <SettingsPager Mode="ShowAllRecords"></SettingsPager>
        <Settings ShowGroupPanel="true" />
        <Styles Header-Font-Bold="true" GroupPanel-Font-Bold="true" GroupRow-Font-Bold="true"></Styles>
        <Columns>
            <dx:GridViewDataTextColumn FieldName="Number" VisibleIndex="0" Caption="Sr.#" Width="20px" UnboundType="String" ReadOnly="true" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" Visible="false"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="ProjectId" Visible="false"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="ProjectName" VisibleIndex="1" Caption="Project Number" ReadOnly="true" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="IBP_ParameterName" Caption="Parameter Name"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="IBV_Comment" Caption="Is Applicable?"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="IBV_Additional" Caption="Additional Charge" Width="60px"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="IBV_ChargeType" Caption="Charge Type" Width="145px"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="PriceFromBDM" Caption="Price by BDM" CellStyle-Font-Size="13px" Visible="false"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="LatestPrice" Caption="Latest Price" Width="60px"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="PreviousPrice" Caption="Previous Price" Width="60px"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="UpdatedDate" Caption="Price Updated On" Width="60px"></dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>
    <dx:ASPxGridView Visible="false" ID="grdProject" runat="server" Width="100%" ClientInstanceName="Grid" AutoGenerateColumns="false" KeyFieldName="ProjectId" Theme="Default" OnCustomButtonCallback="grdProject_CustomButtonCallback" OnCustomUnboundColumnData="grdProject_CustomUnboundColumnData">
        <SettingsPager Mode="ShowAllRecords">
        </SettingsPager>
        <Columns>
            <dx:GridViewDataTextColumn FieldName="Number" VisibleIndex="0" Caption="Sr.#" Width="20px" UnboundType="String" ReadOnly="true" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="ProjectId" Visible="false"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="ProjectName" VisibleIndex="1" Caption="Project Number" ReadOnly="true" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="DomainName" VisibleIndex="2" Caption="DomainName" Width="35px" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="ProjectStartDate" VisibleIndex="3" Caption="ProjectStartDate" Width="35px" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="BillingCycle" VisibleIndex="4" Caption="BillingCycle" Width="35px" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="BDM" VisibleIndex="5" Caption="BDM" ReadOnly="true" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="CRM" VisibleIndex="6" Caption="CRM" ReadOnly="true" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="UH" VisibleIndex="7" Caption="UH" ReadOnly="true" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="AddedBy" VisibleIndex="8" Caption="AddedBy" ReadOnly="true" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="AddedDate" VisibleIndex="9" Caption="AddedDate" ReadOnly="true" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
            <dx:GridViewCommandColumn Caption="View" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" CellStyle-HorizontalAlign="Center" ButtonType="link">
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton Text="View" ID="CostDetails"></dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn FieldName="DomainId" VisibleIndex="9" Caption="DomainId" Visible="false" ReadOnly="true" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>
</asp:Content>
