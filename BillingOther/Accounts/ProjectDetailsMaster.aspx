<%@ Page Title="" Language="C#" MasterPageFile="~/Accounts/Accounts.Master" AutoEventWireup="true" CodeBehind="ProjectDetailsMaster.aspx.cs" Inherits="BillingOther.Accounts.ProjectDetailsMaster" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Data.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Data" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function HideLabel() {

            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%# dvError.ClientID %>").style.display = "none";
                document.getElementById("<%# dvError.ClientID %>").innerHTML = "";
            }, seconds * 1000);
        };   </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvError" runat="server"></div>
    <asp:Button ID="btnAddNew" runat="server" Text="Add New Project" OnClick="btnAddNew_Click" CssClass="btn btn-primary" />
    <div style="overflow:auto; margin-top:10px;">
        <dx:aspxgridview id="grdNewProAppReq" runat="server" autogeneratecolumns="false" clientinstancename="grid" keyfieldname="PAI_Id" enablerowscache="False" theme="Default" 
            oncustomcallback="grdNewProAppReq_CustomCallback" oncustombuttoncallback="grdNewProAppReq_CustomButtonCallback" oncustomunboundcolumndata="grdNewProAppReq_CustomUnboundColumnData">
            <Settings ShowFilterRow="true" ShowFilterRowMenu="true" />
           <SettingsPager PageSize="10"></SettingsPager>
            <Columns>
                <dx:GridViewDataTextColumn FieldName="Number" VisibleIndex="0" Caption="Sr. #" UnboundType="String" ReadOnly="true" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="DomainName" Caption="Domain" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="PAI_Project_Name" Caption="Project Number" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="PAI_ProcessName" Caption="Process" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="PAI_Company_Name" Caption="Company Name" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="PAI_Contact_Person" Caption="Contact Person" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="PAI_Phone_Number" Caption="Phone Number" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="PAI_Address" Caption="Address" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="PAI_Remark" Caption="Remark" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="PAI_ErpProjectID" Caption="PAI_ErpProjectID" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="PAI_DomainId" Caption="PAI_DomainId" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewCommandColumn Width="50px" ButtonType="Image" Caption="Edit" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                    <CustomButtons>
                        <dx:GridViewCommandColumnCustomButton ID="Edit">
                            <Image Url="~/Images/Edit.png" ToolTip="Edit" Height="16" Width="16"></Image>
                        </dx:GridViewCommandColumnCustomButton>
                    </CustomButtons>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                </dx:GridViewCommandColumn>
            </Columns>
        </dx:aspxgridview>
    </div>
    
</asp:Content>
