<%@ Page Title="" Language="C#" MasterPageFile="~/Accounts/Accounts.Master" AutoEventWireup="true" CodeBehind="ClientEmailConfiguration.aspx.cs" Inherits="BillingOther.Accounts.ClientEmailConfiguration" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Data.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Data" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvError" runat="server"></div>
    <table class="table table-border-style">
            <tr>
                <td>Project Number:</td>
                <td>
                    <asp:DropDownList ID="ddlProjects" runat="server" AutoPostBack="true" Width="200px" CssClass="form-control" OnSelectedIndexChanged="ddlProjects_SelectedIndexChanged"></asp:DropDownList>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" SetFocusOnError="true" runat="server"
                        ValidationGroup="user" ControlToValidate="ddlProjects" InitialValue="0" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Please Select Project"></asp:RequiredFieldValidator> </td>
            </tr>
                <tr>
                    <td>Client:</td>
                    <td>

                        <asp:DropDownList runat="server" ID="ddlClientList" Width="200px" CssClass="form-control"></asp:DropDownList>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator3" SetFocusOnError="true" runat="server"
                        ValidationGroup="user" ControlToValidate="ddlClientList" InitialValue="0" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Please Select Client Name"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            <tr>
                <td align="top">To:</td>
                <td>
                    <asp:TextBox ID="txtTo" runat="server" Width="400px" Height="40px" CssClass="form-control" TextMode="MultiLine" Style="resize: none;"></asp:TextBox>
                    <a style="font: 16px; color: red;">Note: Use comma( , ) for multiple EmailID</a>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTo"
                        ErrorMessage="Please enter To." Font-Size="12px" Display="Dynamic" Style="color: Red;"
                        vertical-align="top" SetFocusOnError="true" ValidationGroup="user"></asp:RequiredFieldValidator>
                </td>
            </tr>
             <tr>
                <td align="top">CC:</td>
                <td>
                    <asp:TextBox ID="txtCC" runat="server" Width="400px" Height="40px" CssClass="form-control" TextMode="MultiLine" Style="resize: none;"></asp:TextBox>
                    <a style="font: 16px; color: red;">Note: Use comma( , ) for multiple EmailID</a>

                </td>
            </tr>
            <tr>
                <td align="top">BCC:</td>
                <td>
                    <asp:TextBox ID="txtBCC" runat="server" Width="400px" Height="40px" CssClass="form-control" TextMode="MultiLine"  Style="resize: none;"></asp:TextBox><br />
                    <a style="font: 16px; color: red;">Note: Use comma( , ) for multiple EmailID</a>
                  
                </td>

            </tr>
       
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btnSend" runat="server" Text="Save" CssClass="btn btn-primary" ValidationGroup="user" OnClick="btnSend_Click"/>
                </td>
            </tr>
        </table>
        <div id="costgriddiv" runat="server" style="overflow: auto;">


            <dx:ASPxGridView ID="ASPxGridView1" runat="server" ClientInstanceName="Grid" AutoGenerateColumns="false" KeyFieldName="CEC_Id" Theme="Default" OnCustomButtonCallback="ASPxGridView1_CustomButtonCallback"  OnCustomUnboundColumnData="ASPxGridView1_CustomUnboundColumnData">
                <Settings ShowFilterRow="true" ShowFilterRowMenu="true" />
                <%-- <ClientSideEvents EndCallback="OnEndCallback" />--%>
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="Number" VisibleIndex="0" Caption="Sr.#" Width="20px" UnboundType="String" ReadOnly="true" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="CEC_Id" Width="50px" Visible="false"></dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn FieldName="Project_Name" Caption="Project #" Width="50px" ReadOnly="true" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="ClientName" Caption="Client Name" Width="35px" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="CEC_To" Caption="TO Address" Width="35px" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="CEC_CC" Caption="CC Address" Width="30px" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="CEC_BCC" Caption="BCC Address" Width="30px" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                    </dx:GridViewDataTextColumn>
                  
                    <dx:GridViewDataTextColumn FieldName="Added_By" Caption="Added By" Width="30px" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn FieldName="AddedDate" Caption="Added Date" Width="100px" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
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
