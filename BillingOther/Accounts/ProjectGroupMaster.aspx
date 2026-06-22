<%@ Page Title="" Language="C#" MasterPageFile="~/Accounts/Accounts.Master" AutoEventWireup="true" CodeBehind="ProjectGroupMaster.aspx.cs" Inherits="BillingOther.Accounts.ProjectGroupMaster" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <script type="text/javascript">
        $(function () {
            $('[id*=ddlProjectNumber]').multiselect({
                includeSelectAllOption: false
            });
        });
        function HideLabel() {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=dvError.ClientID %>").style.display = "none";
            }, seconds * 1000);
        }
      </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvError" runat="server"></div>
 <table class="table table-responsive-sm">
        <tr>
            <td><b>Group Number:</b></td>
            <td>
                <asp:TextBox ID="txtGroupNumber" runat="server" Width="200px" CssClass="form-control"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfvGrp" runat="server" ControlToValidate="txtGroupNumber" ErrorMessage="Please enter group number." Display="Dynamic" ForeColor="Red" ValidationGroup="grp"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td><b>Project Number:</b></td>
            <td>
                <asp:ListBox ID="ddlProjectNumber" runat="server" Width="200px" SelectionMode="Multiple" CssClass="list-inline"></asp:ListBox>
            </td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary" />
            </td>
            <td></td>
        </tr>
    </table>
    <div>
        <dx:ASPxGridView ID="grdProjectGroup" runat="server" ClientInstanceName="grid" AutoGenerateColumns="true" Theme="Default" OnCustomUnboundColumnData="grdProjectGroup_CustomUnboundColumnData">
            <Settings ShowFilterRow="true" ShowFilterRowMenu="true" />
            <SettingsPager Mode="ShowPager"></SettingsPager>
            <Styles Header-Wrap="True"></Styles>
            <Columns>
                <dx:GridViewDataTextColumn FieldName="Number" VisibleIndex="0" Caption="Sr. #" Width="40px" UnboundType="String" ReadOnly="true"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="GroupNumber" Caption="Group Number" />
                <dx:GridViewDataTextColumn FieldName="ProjectName" Caption="Project Number" />
                <dx:GridViewDataTextColumn FieldName="AddedByName" Caption="Added By" />
                <dx:GridViewDataTextColumn FieldName="AddedDate" Caption="Added Date/ Time" />

            </Columns>
        </dx:ASPxGridView>
    </div>
    <link href="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />

    <script src="../js/bootstrap-multiselect.js" type="text/javascript"></script>
</asp:Content>
