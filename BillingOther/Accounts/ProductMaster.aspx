<%@ Page Title="" Language="C#" MasterPageFile="~/Accounts/Accounts.Master" AutoEventWireup="true" CodeBehind="ProductMaster.aspx.cs" Inherits="BillingOther.Accounts.ProductMaster" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Data.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Data" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvError" runat="server"></div>
    <div>
        <table class="table table-border-style">
            <tr>
                <td><b>Project Number</b></td>
                <td>
                    <asp:DropDownList ID="ddlProjects" runat="server" AutoPostBack="true"  CssClass="form-control" Width="200px"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" runat="server"
                        ValidationGroup="Client" ControlToValidate="ddlProjects" InitialValue="0" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Please Select Project"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td><b>Product Type</b></td>
                <td>
                    <asp:TextBox runat="server" ID="txtProductType" CssClass="form-control" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" SetFocusOnError="true" runat="server"
                        ValidationGroup="Client" ControlToValidate="txtProductType" Display="Dynamic" ForeColor="Red"
                        ErrorMessage="Please enter Product type"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" ValidationGroup="Client" OnClick="btnSave_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div id="costgriddiv" runat="server" style="overflow: auto;">
        <dx:ASPxGridView ID="ASPxGridView1" runat="server" ClientInstanceName="Grid" AutoGenerateColumns="false" KeyFieldName="IPM_Id" Theme="Default" OnCustomButtonCallback="ASPxGridView1_CustomButtonCallback" OnCustomUnboundColumnData="ASPxGridView1_CustomUnboundColumnData">
            <Settings ShowFilterRow="true" ShowFilterRowMenu="true" />
            <%-- <ClientSideEvents EndCallback="OnEndCallback" />--%>
            <Columns>
                <dx:GridViewDataTextColumn FieldName="Number" VisibleIndex="0" Caption="Sr.#" Width="20px" UnboundType="String" ReadOnly="true" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="IPM_Id" Width="50px" Visible="false"></dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn FieldName="Project_Name" VisibleIndex="1" Caption="Project #" Width="50px" ReadOnly="true" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="ProductType" VisibleIndex="2" Caption="Product Type" Width="35px" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn FieldName="Added_By" VisibleIndex="3" Caption="Added By" Width="30px" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>

                <dx:GridViewDataTextColumn FieldName="AddedDate" VisibleIndex="4" Caption="Added Date" Width="100px" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
                <dx:GridViewCommandColumn Width="50px" ButtonType="Image" Caption="Edit" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                    <CustomButtons>
                        <dx:GridViewCommandColumnCustomButton ID="Edit">
                            <Image Url="~/Images/Edit.png" ToolTip="Edit Record" Height="16" Width="16"></Image>
                        </dx:GridViewCommandColumnCustomButton>
                    </CustomButtons>

                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                </dx:GridViewCommandColumn>
                <dx:GridViewCommandColumn Width="50px" ButtonType="Image" Caption="Delete" Visible="false" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                    <CustomButtons>
                        <dx:GridViewCommandColumnCustomButton ID="Delete">
                            <Image Url="~/Images/Delete.png" ToolTip="Delete Record" Height="16" Width="16"></Image>
                        </dx:GridViewCommandColumnCustomButton>
                    </CustomButtons>

                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                </dx:GridViewCommandColumn>

            </Columns>
        </dx:ASPxGridView>
    </div>
</asp:Content>
