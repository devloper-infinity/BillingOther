<%@ Page Title="" Language="C#" MasterPageFile="~/Accounts/Accounts.Master" AutoEventWireup="true" CodeBehind="SendAccountDetails.aspx.cs" Inherits="BillingOther.Accounts.SendAccountDetails" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Data.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Data" TagPrefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <table class="table table-border-style">
            <tr>
                <td><b>Domain:</b></td>
                <td>
                    <asp:DropDownList ID="ddldomain" runat="server" AutoPostBack="true" Width="200px" OnSelectedIndexChanged="ddldomain_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" runat="server"
                        ValidationGroup="Client" ControlToValidate="ddldomain" InitialValue="Select" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Please Select Domain"></asp:RequiredFieldValidator>
                </td>
                <td><b>Project Number:</b></td>
                <td>
                    <asp:DropDownList ID="ddlProjects" runat="server" AutoPostBack="true" Width="200px" CssClass="form-control"></asp:DropDownList>
                </td>
                <td><b>Billing Period:</b></td>
                <td>
                    <asp:DropDownList ID="ddlPeriod" runat="server" CssClass="form-control" Width="200px">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" Width="100px" ValidationGroup="Client" CssClass="btn btn-primary" /></td>
            </tr>
            <tr>
            </tr>
        </table>
    </div>
   
        <asp:Label ID="lblRecords" runat="server" CssClass="form-control" Font-Bold="true"></asp:Label>
    <dx:ASPxGridView ID="grdBillingDetails" runat="server" Width="100%" AutoGenerateColumns="false" ClientInstanceName="grid" KeyFieldName="TrackingSheetID" Theme="Default" OnCustomButtonCallback="grdBilling_CustomButtonCallback" OnCustomUnboundColumnData="grdBillingDetails_CustomUnboundColumnData">
        <SettingsPager Mode="ShowAllRecords"></SettingsPager>
        <SettingsBehavior ConfirmDelete="True" />
        <Columns>
            <dx:GridViewDataTextColumn FieldName="Number" VisibleIndex="0" Width="40px" Caption="Sr. No." UnboundType="String" ReadOnly="True" ShowInCustomizationForm="True">
                <HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Project ID" FieldName="ProjectID" Visible="false">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Project #" FieldName="ProjectName">
                <HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Billing Period" FieldName="BillingPeriod" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                <HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                <CellStyle HorizontalAlign="Center"></CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Type" FieldName="TYPE" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                <HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                <CellStyle HorizontalAlign="Center"></CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Status" FieldName="Status" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                <HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                <CellStyle HorizontalAlign="Center"></CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Billing Sent Date" FieldName="AddedDate" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                <HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                <CellStyle HorizontalAlign="Center"></CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewCommandColumn Caption="View Details" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" CellStyle-HorizontalAlign="Center" ButtonType="link">
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton Text="View Details" ID="SendAccounts"></dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>
        </Columns>
    </dx:ASPxGridView>
</asp:Content>
