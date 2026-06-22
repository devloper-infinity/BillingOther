<%@ Page Title="" Language="C#" MasterPageFile="~/Accounts/Accounts.Master" AutoEventWireup="true" CodeBehind="RateRevisionUpdates.aspx.cs" Inherits="BillingOther.Accounts.RateRevisionUpdates" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Data.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Data" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Button ID="btnExpoetToExcel" runat="server" Text="Export To Excel" OnClick="btnExpoetToExcel_Click" ValidationGroup="user" Font-Bold="true" Font-Size="Larger" Height="27px" />
    <dx:ASPxGridViewExporter runat="server" ID="grdExportExceldata" GridViewID="grdTaxDetails" FileName="ILS Excel Report"></dx:ASPxGridViewExporter>
    <dx:ASPxGridView ID="grdTaxDetails" runat="server" AutoGenerateColumns="true" EnableRowsCache="false" KeyFieldName="OrderID" Theme="Office2010Silver" Width="100%" OnCustomUnboundColumnData="grdExcelReport_CustomUnboundColumnData">
        <SettingsPager Mode="ShowAllRecords"></SettingsPager>
        <Columns>
            <dx:GridViewDataTextColumn FieldName="Number" VisibleIndex="0" Caption="Sr. #" Width="40px" UnboundType="String" ReadOnly="True" ShowInCustomizationForm="True" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="ProjectName" Caption="Project #" CellStyle-Wrap="True" VisibleIndex="1" CellStyle-ForeColor="Black" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                <HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                <CellStyle HorizontalAlign="Center" ForeColor="Black"></CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="AgreementDate" Caption="AgreementDate" VisibleIndex="2" CellStyle-ForeColor="Black" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                <HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                <CellStyle HorizontalAlign="Center" ForeColor="Black"></CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="RateRevisionClause" Caption="Rate Revision Clause" VisibleIndex="2" CellStyle-ForeColor="Black" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                <HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                <CellStyle HorizontalAlign="Center" ForeColor="Black"></CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Type" Caption="Rate Type" VisibleIndex="2" CellStyle-ForeColor="Black" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                <HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                <CellStyle HorizontalAlign="Center" ForeColor="Black"></CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewBandColumn Caption="Current Rate" HeaderStyle-Font-Bold="true">
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="LatestRate" Caption="Latest Rate (In $)" VisibleIndex="2" CellStyle-ForeColor="Black" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                        <CellStyle HorizontalAlign="Center" ForeColor="Black"></CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="UpdatedOn" Caption="Updated On" VisibleIndex="2" CellStyle-ForeColor="Black" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                        <CellStyle HorizontalAlign="Center" ForeColor="Black"></CellStyle>
                    </dx:GridViewDataTextColumn>
                </Columns>
            </dx:GridViewBandColumn>
            <dx:GridViewBandColumn Caption="Rate History" HeaderStyle-Font-Bold="true">
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="PreviousRate" Caption="Rate (In $)" VisibleIndex="2" CellStyle-ForeColor="Black" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                        <CellStyle HorizontalAlign="Center" ForeColor="Black"></CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="PreviousRate2" Caption="Rate (In $)" VisibleIndex="2" CellStyle-ForeColor="Black" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                        <CellStyle HorizontalAlign="Center" ForeColor="Black"></CellStyle>
                    </dx:GridViewDataTextColumn>
                </Columns>
            </dx:GridViewBandColumn>
        </Columns>
    </dx:ASPxGridView>
</asp:Content>
