<%@ Page Title="" Language="C#" MasterPageFile="~/Accounts/Accounts.Master" AutoEventWireup="true" CodeBehind="WholeLoanBilling.aspx.cs" Inherits="BillingOther.Accounts.WholeLoanBilling" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function HideLabel() {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%=dvError.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="overflow: auto;">
        <center>
            <div id="dvError" runat="server" style="display: none; padding-bottom: 20px;">
                <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
            </div>
        </center>
        <br />
        <div>
            <table class="table">
                <tr>
                    <td><b>Billing Month:</b></td>
                    <td>
                        <asp:DropDownList ID="ddlMonth" Height="24px" runat="server" ValidationGroup="user" Width="100px">
                            <asp:ListItem Value="January">January</asp:ListItem>
                            <asp:ListItem Value="February">February</asp:ListItem>
                            <asp:ListItem Value="March">March</asp:ListItem>
                            <asp:ListItem Value="April">April</asp:ListItem>
                            <asp:ListItem Value="May">May</asp:ListItem>
                            <asp:ListItem Value="June">June</asp:ListItem>
                            <asp:ListItem Value="July">July</asp:ListItem>
                            <asp:ListItem Value="August">August</asp:ListItem>
                            <asp:ListItem Value="September">September</asp:ListItem>
                            <asp:ListItem Value="October">October</asp:ListItem>
                            <asp:ListItem Value="November">November</asp:ListItem>
                            <asp:ListItem Value="December">December</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorMonth" ErrorMessage="Please select month." Display="None" Style="color: Red;"
                            ControlToValidate="ddlMonth" runat="server" InitialValue="Select" Font-Size="12px" ValidationGroup="user" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>
                    <td><b>Year:</b></td>
                    <td>
                        <asp:DropDownList ID="ddlYear" Height="24px" runat="server" ValidationGroup="user" Width="100px">
                        </asp:DropDownList>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorYear" ErrorMessage="Please select year." Display="None" Style="color: Red;"
                            ControlToValidate="ddlYear" runat="server" InitialValue="Select" Font-Size="12px" ValidationGroup="user" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td><b>Purchaser:</b></td>
                    <td>
                        <asp:TextBox ID="txtPurchaser" runat="server" CssClass="form-control" Width="250px"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtPurchaser" ErrorMessage="Please enter purchaser name" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>

                    <td><b>Seller:</b></td>
                    <td>
                        <asp:TextBox ID="txtSeller" runat="server" CssClass="form-control" Width="250px"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSeller" ErrorMessage="Please enter seller name" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td><b>Trade Name:</b></td>
                    <td>
                        <asp:TextBox ID="txtTradeName" runat="server" CssClass="form-control" Width="250px"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtTradeName" ErrorMessage="Please enter trade name" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>

                    <td><b>Paying Entity:</b></td>
                    <td>
                        <asp:TextBox ID="txtpayingEntity" runat="server" CssClass="form-control" Width="250px"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtpayingEntity" ErrorMessage="Please enter paying entity" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>


                    <td><b>Loan Count:</b></td>
                    <td>
                        <asp:TextBox ID="txtLoanCount" runat="server" CssClass="form-control" Width="250px"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLoanCount" ErrorMessage="Please enter loan count" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>

                    <td><b>Closing Date:</b></td>
                    <td>
                        <asp:TextBox ID="txtClosingDate" runat="server" CssClass="form-control" Width="250px"></asp:TextBox>
                        <asp:CalendarExtender ID="cal1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtClosingDate"></asp:CalendarExtender>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtClosingDate" ErrorMessage="Please enter closing date" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                </tr>

                <tr>
                    <td><b>Attachment:</b></td>
                    <td>
                        <asp:FileUpload ID="fpAttachment" runat="server" CssClass="form-control" Width="250px" />
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="fpAttachment" ErrorMessage="Please select attachment" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                    <td><b>Billed To:</b></td>
                    <td>
                        <asp:TextBox ID="txtBilledTo" runat="server" CssClass="form-control" Width="250px"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtBilledTo" ErrorMessage="Please enter billed to" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>

                </tr>
                <tr>
                    <td colspan="4" style="text-align: center;">
                        <asp:Button ID="btnSubmit" runat="server" Text="Import Data" OnClick="btnSubmit_Click" CssClass="btn btn-primary" />
                        <asp:Button ID="btnAddtoDatabase" runat="server" Text="Verify and add to database" Style="display: none;" OnClick="btnAddtoDatabase_Click" ValidationGroup="none" CssClass="btn btn-primary" />
                    </td>
                </tr>
            </table>
            <div style="width: 100%; overflow: auto;">
                <dx:ASPxGridView ID="grdVolume" runat="server" AutoGenerateColumns="true" EnableRowsCache="False" KeyFieldName="ProjectId" ClientInstanceName="grid"
                    Theme="Office2010Silver">
                    <Settings ShowFilterRow="True" ShowFilterRowMenu="True" />
                    <Settings ShowFooter="true" />
                    <Styles Header-Wrap="True"></Styles>
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="Number" VisibleIndex="1" Caption="Sr. No." Width="30px" FixedStyle="Left" UnboundType="String" ReadOnly="true"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ProjectId" Caption="ProjectId" Visible="false" />
                        <dx:GridViewDataTextColumn FieldName="Loan Number" Caption="Loan Number" />
                        <dx:GridViewDataTextColumn FieldName="PRP ID" Caption="PRP ID" />
                        <dx:GridViewDataTextColumn FieldName="Interim Servicer" Caption="Interim Servicer" />
                        <dx:GridViewDataTextColumn FieldName="Warehouse Bank" Caption="Warehouse Bank" />
                        <dx:GridViewDataTextColumn FieldName="Borrower" Caption="Property Address" />
                        <dx:GridViewDataTextColumn FieldName="Property City" Caption="Property City" />
                        <dx:GridViewDataTextColumn FieldName="Property State" Caption="Property State" />
                        <dx:GridViewDataTextColumn FieldName="Property Zip" Caption="Property Zip" />
                        <dx:GridViewDataTextColumn FieldName="Cut-off Date" Caption="Cut-off Date" />
                        <dx:GridViewDataTextColumn FieldName="Origination Date" Caption="Origination Date" />
                        <dx:GridViewDataTextColumn FieldName="Original Loan Amount" Caption="Original Loan Amount" />
                        <dx:GridViewDataTextColumn FieldName="Interest Bearing UPB" Caption="Interest Bearing UPB" />
                        <dx:GridViewDataTextColumn FieldName="Lien Position" Caption="Lien Position" />
                        <dx:GridViewDataTextColumn FieldName="Paid Through Date" Caption="Paid Through Date" />
                        <dx:GridViewDataTextColumn FieldName="Interest Rate" Caption="Interest Rate" />
                        <dx:GridViewDataTextColumn FieldName="Next Due Date" Caption="Next Due Date" />
                        <dx:GridViewDataTextColumn FieldName="Bid Percentage" Caption="Bid Percentage" CellStyle-Wrap="True" />
                        <dx:GridViewDataTextColumn FieldName="Purchase Price" Caption="Purchase Price" CellStyle-Wrap="True" />
                        <dx:GridViewDataTextColumn FieldName="Purchase Price Percentage" Caption="Purchase Price Percentage" CellStyle-Wrap="True" />
                        <dx:GridViewDataTextColumn FieldName="Per Diem" Caption="Per Diem" CellStyle-Wrap="True" />
                        <dx:GridViewDataTextColumn FieldName="Days Interest" Caption="Days Interest" CellStyle-Wrap="True" />
                        <dx:GridViewDataTextColumn FieldName="Accrued Interest" Caption="Accrued Interest" CellStyle-Wrap="True" />
                        <dx:GridViewDataTextColumn FieldName="Total Wire" Caption="Total Wire" CellStyle-Wrap="True" />
                    </Columns>
                </dx:ASPxGridView>
                <asp:SqlDataSource ID="dswholeloan" runat="server" SelectCommand="usp_GetWhleLoanBilling" SelectCommandType="StoredProcedure" ConnectionString="Data Source=192.168.11.11,8989;Initial Catalog=InfinityERP;Persist Security Info=True;User ID=sa;Password=idt15central; Pooling=true; Min Pool Size=1; Max Pool Size=10; Connect Timeout=200; Packet Size=8192"></asp:SqlDataSource>
            </div>
        </div>
    </div>
</asp:Content>
