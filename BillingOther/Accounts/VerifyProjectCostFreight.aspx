<%@ Page Title="" Language="C#" MasterPageFile="~/Accounts/Accounts.Master" AutoEventWireup="true" CodeBehind="VerifyProjectCostFreight.aspx.cs" Inherits="BillingOther.Accounts.VerifyProjectCostFreight" %>

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
    <asp:HiddenField ID="hdProductBaseLabel" runat="server" />
    <asp:HiddenField ID="hdProductOtherLabel" runat="server" />
    <asp:HiddenField ID="hdOrderBaseLabel" runat="server" />
    <asp:HiddenField ID="hdOrderOtherLabel" runat="server" />
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
    <script>
        function GetConfiguration(ddl) {
            var trBaseRate = document.getElementById("<%= trBaseRate.ClientID %>");
            var trOrderType = document.getElementById("<%= trOrderType.ClientID %>");
            var index = ddl.selectedIndex;
            if (index == 1) {
                trBaseRate.style.display = '';
                trOrderType.style.display = 'none';
            }
            else if (index == 2) {
                trBaseRate.style.display = 'none';
                trOrderType.style.display = '';
            }
        }
    </script>
    <table class="table table-border-style" style="border: solid 1px black; display: none;" id="tblOther" runat="server">
        <tr>
            <td><b>Billing Type:</b></td>
            <td>
                <asp:DropDownList ID="ddlBillingBase" runat="server" CssClass="form-control" Width="200px" onchange="GetConfiguration(this);">
                    <asp:ListItem Value="Select">Select</asp:ListItem>
                    <asp:ListItem Value="Base Rate">Base Rate Only</asp:ListItem>
                    <asp:ListItem Value="Product Type">Product Type</asp:ListItem>
                    <asp:ListItem Value="Invoice Type">Invoice Type</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfvBillingBase" runat="server" ControlToValidate="ddlBillingBase" ErrorMessage="Please select Billing Type" InitialValue="Select" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr id="trBaseRate" runat="server" style="display: none;">
            <td colspan="3">
                <table class="table table-border-style">
                    <tr>
                        <th>Parameter Name</th>
                        <th>Is Applicable?</th>
                        <th>Charge Type</th>
                        <th>Price</th>
                        <th>Billing Header</th>
                    </tr>
                    <tr>
                        <td>Base Rate</td>
                        <td>
                            <asp:DropDownList ID="ddlIsApplicableBaseRate" runat="server" Width="200px" CssClass="form-control">
                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                <asp:ListItem Value="No">No</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlChargeTypeBaseRate" runat="server" Width="200px" CssClass="form-control">
                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                <asp:ListItem Value="Fix Amount">Fix Amount</asp:ListItem>
                                <asp:ListItem Value="Incremental">Incremental</asp:ListItem>
                                <asp:ListItem Value="Percentage">Percentage</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPriceBaseRate" runat="server" CssClass="form-control" Width="200px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlBillingHeaderBaseRate" runat="server" Width="200px" CssClass="form-control">
                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                <asp:ListItem Value="Dispatched Record">Dispatched Record</asp:ListItem>
                                <asp:ListItem Value="No Of Records">No Of Records</asp:ListItem>
                                <asp:ListItem Value="Order#">Order#</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>Rush</td>
                        <td>
                            <asp:DropDownList ID="ddlIsApplicableBaseRateRush" runat="server" Width="200px" CssClass="form-control">
                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                <asp:ListItem Value="No">No</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlChargeTypeBaseRateRush" runat="server" Width="200px" CssClass="form-control">
                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                <asp:ListItem Value="Fix Amount">Fix Amount</asp:ListItem>
                                <asp:ListItem Value="Incremental">Incremental</asp:ListItem>
                                <asp:ListItem Value="Percentage">Percentage</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPriceBaseRateRush" runat="server" CssClass="form-control" Width="200px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlBillingHeaderBaseRateRush" runat="server" Width="200px" CssClass="form-control">
                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                <asp:ListItem Value="Order#">Order#</asp:ListItem>
                                <asp:ListItem Value="Rush">Rush</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td># Characters > 800</td>
                        <td>
                            <asp:DropDownList ID="ddlIsApplicableBaseRateCharacter" runat="server" Width="200px" CssClass="form-control">
                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                <asp:ListItem Value="No">No</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlChargeTypeBaseRateCharacter" runat="server" Width="200px" CssClass="form-control">
                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                <asp:ListItem Value="Fix Amount">Fix Amount</asp:ListItem>
                                <asp:ListItem Value="Incremental">Incremental</asp:ListItem>
                                <asp:ListItem Value="Percentage">Percentage</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPriceBaseRateCharacter" runat="server" CssClass="form-control" Width="200px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlBillingHeaderBaseRateCharacter" runat="server" Width="200px" CssClass="form-control">
                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                <asp:ListItem Value="# of Character"># of Character</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr id="trOrderType" runat="server" style="display: none;">
            <td colspan="3">
                <script>
                    function GetOrderTypeInfo(ddlOrder) {
                        var lblBaseOrder = document.getElementById("<%= lblOrderTypeBase.ClientID %>");
                                        var lblOtherOrder = document.getElementById("<%= lblOrderTypeOther.ClientID %>");
                                        var hdOrderBaseLabel = document.getElementById("<%= hdOrderBaseLabel.ClientID %>");
                                        var hdOrderOtherLabel = document.getElementById("<%= hdOrderOtherLabel.ClientID %>");
                        var index = ddlOrder.selectedIndex;
                        var ddlText = ddlOrder.options[index].text;
                        for (var i = 0; i < ddlOrder.length; i++) {
                            if (ddlOrder.options[i].text == ddlText && ddlOrder.options[i].text != "Select") {
                                lblBaseOrder.innerHTML = ddlText;
                                hdOrderBaseLabel.value = ddlText;
                            }
                            if (ddlOrder.options[i].text != ddlText && ddlOrder.options[i].text != "Select") {
                                lblOtherOrder.innerHTML = ddlOrder.options[i].text;
                                hdOrderOtherLabel.value = ddlOrder.options[i].text;
                            }
                        }
                    }
                </script>
                <table class="table table-border-style" border="1">
                    <tr>
                        <td><b>Base Invoice Type:</b></td>
                        <td>
                            <asp:DropDownList ID="ddlBaseOrderType" runat="server" CssClass="form-control" Width="200px" onchange="GetOrderTypeInfo(this);">
                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                <asp:ListItem Value="International">International</asp:ListItem>
                                <asp:ListItem Value="Standard">Standard</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <table class="table table-border-style" border="1">
                    <tr>
                        <th>Parameter Name</th>
                        <th>Is Applicable?</th>
                        <th>Charge Type</th>
                        <th>Price</th>
                        <th>Billing Header</th>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblOrderTypeBase" runat="server" CssClass="form-control"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlIsApplicableOrderBase" runat="server" Width="200px" CssClass="form-control">
                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                <asp:ListItem Value="No">No</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlChargeTypeOrderBase" runat="server" Width="200px" CssClass="form-control">
                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                <asp:ListItem Value="Fix Amount">Fix Amount</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPriceOrderBase" runat="server" CssClass="form-control" Width="200px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlBillingHeaderOrderBase" runat="server" Width="200px" CssClass="form-control">
                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                <asp:ListItem Value="Invoice Type">Invoice Type</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblOrderTypeOther" runat="server" CssClass="form-control"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlIsApplicableOrderOther" runat="server" Width="200px" CssClass="form-control">
                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                <asp:ListItem Value="No">No</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlChargeTypeOrderOther" runat="server" Width="200px" CssClass="form-control">
                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                <asp:ListItem Value="Fix Amount">Fix Amount</asp:ListItem>
                                <asp:ListItem Value="Incremental">Incremental</asp:ListItem>
                                <asp:ListItem Value="Percentage">Percentage</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPriceOrderOther" runat="server" CssClass="form-control" Width="200px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlBillingHeaderOrderOther" runat="server" Width="200px" CssClass="form-control">
                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                <asp:ListItem Value="Invoice Type">Invoice Type</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td># Characters > 800</td>
                        <td>
                            <asp:DropDownList ID="ddlIsApplicableOrderCharacter" runat="server" Width="200px" CssClass="form-control">
                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                <asp:ListItem Value="No">No</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlChargeTypeOrderCharacter" runat="server" Width="200px" CssClass="form-control">
                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                <asp:ListItem Value="Fix Amount">Fix Amount</asp:ListItem>
                                <asp:ListItem Value="Incremental">Incremental</asp:ListItem>
                                <asp:ListItem Value="Percentage">Percentage</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPriceOrderCharacter" runat="server" CssClass="form-control" Width="200px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlBillingHEaderOrderCharacter" runat="server" Width="200px" CssClass="form-control">
                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                <asp:ListItem Value="# of Character"># of Character</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button ID="btnShubmitTyping" runat="server" Text="Submit" OnClick="btnShubmitTyping_Click" CssClass="btn btn-primary" />
            </td>
            <td></td>
        </tr>
    </table>
    <table class="table table-border-style" id="tbl771" runat="server" style="display: none; border: solid 1px black;">
        <tr>
            <th>Parameter</th>
            <th>Charge Type</th>
            <th>Price</th>
            <th>Billing Header</th>
        </tr>
        <tr>
            <td>No of Pages - Scan</td>
            <td>
                <asp:DropDownList ID="ddlChargeType771ScanPages" runat="server" Width="200px" CssClass="form-control">
                    <asp:ListItem Value="Select">Select</asp:ListItem>
                    <asp:ListItem Value="Fix Amount">Fix Amount</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="txtPrice771ScanPages" runat="server" CssClass="form-control" Width="200px"></asp:TextBox>
            </td>
            <td>
                <asp:DropDownList ID="ddlBillingHeader771ScanPages" runat="server" Width="200px" CssClass="form-control">
                    <asp:ListItem Value="Select">Select</asp:ListItem>
                    <asp:ListItem Value="No of Pages">No of Pages</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>No of Records - Scan</td>
            <td>
                <asp:DropDownList ID="ddlChargeType771ScanRecords" runat="server" Width="200px" CssClass="form-control">
                    <asp:ListItem Value="Select">Select</asp:ListItem>
                    <asp:ListItem Value="Fix Amount">Fix Amount</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="txtPrice771ScanRecords" runat="server" CssClass="form-control" Width="200px"></asp:TextBox>
            </td>
            <td>
                <asp:DropDownList ID="ddlBillingHeader771ScanRecords" runat="server" Width="200px" CssClass="form-control">
                    <asp:ListItem Value="Select">Select</asp:ListItem>
                    <asp:ListItem Value="No of Records">No of Records</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>No of Records - Mail</td>
            <td>
                <asp:DropDownList ID="ddlChargeType771MailRecords" runat="server" Width="200px" CssClass="form-control">
                    <asp:ListItem Value="Select">Select</asp:ListItem>
                    <asp:ListItem Value="Fix Amount">Fix Amount</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="txtPrice771MailRecords" runat="server" CssClass="form-control" Width="200px"></asp:TextBox>
            </td>
            <td>
                <asp:DropDownList ID="ddlBillingHeader771MailRecords" runat="server" Width="200px" CssClass="form-control">
                    <asp:ListItem Value="Select">Select</asp:ListItem>
                    <asp:ListItem Value="No of Records">No of Records</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button ID="btnSubmit771" runat="server" Text="Submit" OnClick="btnSubmit771_Click" ValidationGroup="Typing" CssClass="btn btn-primary" />
            </td>
            <td></td>
            <td></td>
        </tr>
    </table>
</asp:Content>
