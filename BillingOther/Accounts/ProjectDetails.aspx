<%@ Page Title="" Language="C#" MasterPageFile="~/Accounts/Accounts.Master" AutoEventWireup="true" CodeBehind="ProjectDetails.aspx.cs" Inherits="BillingOther.Accounts.ProjectDetails" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Data.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Data" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            NDAChange();
            SLAChange()
        });
        function HideLabel() {
            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%# dvError.ClientID %>").value.style.display = "none";
            }, seconds * 1000);
        };
        function SLAChange() {
            var ddl = document.getElementById("<%=ddsla.ClientID %>");
            var index = ddl.selectedIndex;
            if (index == 1) {
                trsla.style.display = '';
            }

            else if (index == 3) {
                if (document.getElementById("<%= rdcardYes.ClientID %>").checked) {
                    alert('Please select either Yes or No.');
                    ddl.selectedIndex = 0;
                    trsla.style.display = 'none';
                }
                else
                    trsla.style.display = 'none';
            }
            else {
                trsla.style.display = 'none';
            }
        }

        // ****************** AADHAR Card  **************//
        function ShowHideCard(val) {
            if (val == 1) {

                document.getElementById("<%= trnewprocess.ClientID%>").style.display = 'none';
                document.getElementById("<%=  tdprojtxt.ClientID%>").style.display = '';
                document.getElementById("<%=  txtProjectname.ClientID%>").style.display = '';
                ValidatorEnable(document.getElementById("<%= rfvProjectName.ClientID %>"), true);
                ValidatorEnable(document.getElementById("<%= rfvddlProjectName.ClientID %>"), false);
                ValidatorEnable(document.getElementById("<%= rfvProjectProcess.ClientID %>"), false);
            }
            else {
                document.getElementById("<%= trnewprocess.ClientID%>").value = '';
                document.getElementById("<%=  tdprojtxt.ClientID%>").style.display = 'none';
                document.getElementById("<%= trnewprocess.ClientID%>").style.display = '';
                document.getElementById("<%=  txtProjectname.ClientID%>").style.display = 'none';
                ValidatorEnable(document.getElementById("<%= rfvProjectName.ClientID %>"), false);
                ValidatorEnable(document.getElementById("<%= rfvddlProjectName.ClientID %>"), true);
                ValidatorEnable(document.getElementById("<%= rfvProjectProcess.ClientID %>"), true);
            }
        }
        function NDAChange() {
            var ddl = document.getElementById("<%=ddnda.ClientID %>");
            var index = ddl.selectedIndex;
            if (index == 1) {
                NDA.style.display = '';
            }

            else {
                NDA.style.display = 'none';
            }
        }
        function OnEndCallback() {
            if (gridBilling.cp_message) {
                if (gridBilling.cp_message == "1") {
                    alert("Paramters updated successfully.");
                    gridBilling.cp_message = "";
                    gridCosting.PerformCallback('databind');
                }
                else if (gridBilling.cp_message == "0") {
                    alert("Paramters already exists!");
                    gridBilling.cp_message = "";
                }
                else if (gridBilling.cp_message == "2") {
                    alert("Please add price for " + gridBilling.cp_Field);
                    return;
                }
                else if (gridBilling.cp_message == "3") {
                    alert("Please selet charge type for " + gridBilling.cp_Field);
                    return;
                }
                else if (gridBilling.cp_message == "4") {
                    alert("Please selet Applicable column for " + gridBilling.cp_Field);
                    return;
                }
                else {
                    document.getElementById("<%= dvError.ClientID%>").style.display = "none";
                    gridBilling.cp_message = "";
                }
                HideLabel();
            }
            else {
                document.getElementById("<%= dvError.ClientID%>").style.display = "none";
                gridBilling.cp_message = "";
            }

        }

        function OnEndCallbackCosting() {
            if (gridCosting.cp_message) {
                if (gridCosting.cp_message == "1") {
                    alert("Pricing updated successfully.");
                    gridCosting.cp_message = "";
                }
                else if (gridCosting.cp_message == "0") {
                    alert("Error occured while updating price.");
                    gridCosting.cp_message = "";
                }
                else if (gridBilling.cp_message == "2") {
                    alert("Please add price for " + gridBilling.cp_Field);
                    return;
                }
                else if (gridCosting.cp_message == "3") {
                    alert("Please selet Billng Header for " + gridCosting.cp_Field);
                    return;
                }
                else if (gridCosting.cp_message == "4") {
                    alert("Please selet Applicable column for " + gridCosting.cp_Field);
                    return;
                }
                HideLabel();
            }
            else {
                document.getElementById("<%= dvError.ClientID%>").style.display = "none";
                gridCosting.cp_message = "";
            }
        }

        function GetLiveStopped() {
            var ddl = document.getElementById("<%= ddlProjectStatus.ClientID %>");
            var index = ddl.selectedIndex;
            var value = ddl.options[index].text;
            if (value == "Live Stopped") {
                document.getElementById("<%=trlivestopped.ClientID %>").style.display = '';
            }
            else if (value == "Live") {
                var ddsla = document.getElementById("<%= ddsla.ClientID %>");
                var indexsla = ddsla.selectedIndex;
                if (indexsla == 2) {
                    alert('You cannot select LIVE status as MSA is not signed.');
                    ddl.selectedIndex = 0;
                    document.getElementById("<%=trlivestopped.ClientID %>").style.display = 'none';
                }
            }
            else {
                document.getElementById("<%=trlivestopped.ClientID %>").style.display = 'none';
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvError" runat="server"></div>
    <asp:HiddenField ID="hdndomain" runat="server" />
    <asp:HiddenField ID="hdnSLAPath" runat="server" />

    <asp:HiddenField ID="hdnMSAPath" runat="server" />
    <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn btn-warning" Style="float: right; margin-right: 150px;"></asp:Button>
    <div style="width: 100%; display: none;" id="dvSummary" runat="server">
        <dx:ASPxGridView ID="ASPxReoprt" runat="server" ClientInstanceName="Grid" AutoGenerateColumns="false" KeyFieldName="ProjectId" Theme="Default" OnCustomUnboundColumnData="ASPxReoprt_CustomUnboundColumnData">
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <Columns>
                <dx:GridViewDataTextColumn FieldName="Number" VisibleIndex="0" Caption="Sr.#" Width="20px" UnboundType="String" ReadOnly="true" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="ProjectId" Visible="false"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="ProjectName" VisibleIndex="1" Caption="Project #" ReadOnly="true" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="DomainName" VisibleIndex="2" Caption="Domain" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="ProjectStartDate" VisibleIndex="3" Caption="Start Date" Width="35px" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="BillingCycle" VisibleIndex="4" Caption="Billing Cycle" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="BDM" VisibleIndex="5" Caption="BDM" ReadOnly="true" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="CRM" VisibleIndex="6" Caption="CRM" ReadOnly="true" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="UH" VisibleIndex="7" Caption="Unit Head" ReadOnly="true" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
            </Columns>
        </dx:ASPxGridView>
    </div>
    <div style="clear: both;"></div>
    <hr />
    <div class="card-content">
        <div class="card-body">
            <ul class="nav nav-tabs " role="tablist">
                <li class="nav-item active" id="tbpnl1" runat="server">
                    <a class="nav-link active" data-toggle="tab" href="#tab1" role="tab"><i class="fa fa-user"></i>&nbsp;&nbsp;Client Information</a>
                    <div class="slide"></div>
                </li>
                <li class="nav-item" id="tbpnl2" runat="server">
                    <a class="nav-link" data-toggle="tab" href="#tab2" role="tab"><i class="fa fa-envelope"></i>&nbsp;&nbsp;Sales Information</a>
                    <div class="slide"></div>
                </li>
                <li class="nav-item" id="tbpnl3" runat="server">
                    <a class="nav-link" data-toggle="tab" href="#tab3" role="tab"><i class="fa fa-paragraph"></i>&nbsp;&nbsp;Billing Parameters</a>
                    <div class="slide"></div>
                </li>
                <li class="nav-item" id="tbpnlFTE" runat="server">
                    <a class="nav-link" data-toggle="tab" href="#tab4" role="tab"><i class="fa fa-cog"></i>&nbsp;&nbsp;Set Pricing</a>
                    <div class="slide"></div>
                </li>
                <li class="nav-item" id="TabPanel1" runat="server">
                    <a class="nav-link" data-toggle="tab" href="#tab5" role="tab"><i class="fa fa-cog"></i>&nbsp;&nbsp;Set Pricing</a>
                    <div class="slide"></div>
                </li>
            </ul>
            <div class="tab-content card-block" style="border: solid 1px #6c757d; margin-left: 5px;">
                <div class="tab-pane active" id="tab1" role="tabpanel">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnsbmit" />
                        </Triggers>
                        <ContentTemplate>
                            <table class="table table-border-style">
                                <tr id="rdButtons" runat="server">
                                    <td colspan="4">
                                        <asp:RadioButton runat="server" ID="rdcardYes" Checked="true" Text="New Project" GroupName="rdaadharcard" onclick="ShowHideCard('1');" />
                                        <asp:RadioButton runat="server" ID="rdcardNo" Text="New Process" GroupName="rdaadharcard" onclick="ShowHideCard('2');" /></b></td>
                                </tr>
                                <tr>
                                    <td>Domain:
                                    </td>
                                    <td>

                                        <asp:DropDownList ID="ddldomain" runat="server" Width="200px" CssClass="form-control" OnSelectedIndexChanged="ddldomain_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" runat="server"
                                            ValidationGroup="Client" ControlToValidate="ddldomain" InitialValue="Select" ForeColor="Red" Display="Dynamic"
                                            ErrorMessage="Please Select Domain"></asp:RequiredFieldValidator>

                                    </td>
                                    <td id="tdprojtxt" runat="server">Project Number:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtProjectname" runat="server" Width="200px" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvProjectName" SetFocusOnError="true" runat="server"
                                            ValidationGroup="Client" ControlToValidate="txtProjectname" Display="Dynamic" ForeColor="Red"
                                            ErrorMessage="Please enter Project number"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>




                                <tr id="trnewprocess" runat="server" style="display: none;">
                                    <td>Project Number:</td>
                                    <td>

                                        <asp:DropDownList ID="ddlProjects" runat="server" Width="200px" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlProjects_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlProjectName" SetFocusOnError="true" runat="server"
                                            ValidationGroup="Client" ControlToValidate="ddlProjects" InitialValue="Select" ForeColor="Red" Display="Dynamic"
                                            ErrorMessage="Please Select Project"></asp:RequiredFieldValidator>

                                    </td>
                                    <td>Process:</td>
                                    <td>
                                        <asp:TextBox ID="txtProcess" runat="server" Width="200px" CssClass="form-control" MaxLength="3"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvProjectProcess" SetFocusOnError="true" runat="server"
                                            ValidationGroup="Client" ControlToValidate="txtProcess" Display="Dynamic" ForeColor="Red"
                                            ErrorMessage="Please enter Process"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>

                                <tr>
                                    <td>Company Name:                        
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtcompanyname" runat="server" Width="200px" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" SetFocusOnError="true" runat="server"
                                            ValidationGroup="Client" ControlToValidate="txtcompanyname" Display="Dynamic" ForeColor="Red"
                                            ErrorMessage="Please enter Company name"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>Contact Person:</td>
                                    <td>
                                        <asp:TextBox ID="txtContactPerson" runat="server" Width="200px" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" SetFocusOnError="true" runat="server"
                                            ValidationGroup="Client" ControlToValidate="txtContactPerson" Display="Dynamic" ForeColor="Red"
                                            ErrorMessage="Please enter Contact person"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Phone Number:</td>
                                    <td>
                                        <asp:TextBox ID="txtphonenumber" runat="server" Width="200px" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" SetFocusOnError="true" runat="server"
                                            ValidationGroup="Client" ControlToValidate="txtphonenumber" Display="Dynamic" ForeColor="Red"
                                            ErrorMessage="Please enter Phone no."></asp:RequiredFieldValidator>
                                        <asp:FilteredTextBoxExtender ID="fltPhoneNumber" runat="server" TargetControlID="txtphonenumber" FilterMode="ValidChars" ValidChars="0123456789()-+"></asp:FilteredTextBoxExtender>
                                    </td>
                                    <td>Email Id: 
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtemail" runat="server" Width="200px" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" SetFocusOnError="true" runat="server"
                                            ValidationGroup="Client" ControlToValidate="txtemail" Display="Dynamic" ForeColor="Red"
                                            ErrorMessage="Please enter Email"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator Font-Size="12px" ID="RegularExpressionValidator1" ControlToValidate="txtemail" runat="server" ForeColor="Red"
                                            ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.[a-z]{2,3})+)$" Display="Dynamic" SetFocusOnError="true" ErrorMessage="Invalid Email Format." ValidationGroup="Client"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Website Url:</td>
                                    <td>
                                        <asp:TextBox ID="txturl" runat="server" Width="200px" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" SetFocusOnError="true" runat="server"
                                            ValidationGroup="Client" ControlToValidate="txturl" Display="Dynamic" ForeColor="Red"
                                            ErrorMessage="Please enter URL"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator Font-Size="12px" ID="RegularExpressionValidator2" ControlToValidate="txturl" runat="server" ForeColor="Red"
                                            ValidationExpression="^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$" Display="Dynamic" SetFocusOnError="true" ErrorMessage="Invalid URL Format." ValidationGroup="Client"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>Address:</td>
                                    <td>
                                        <asp:TextBox ID="txtaddress" runat="server" Width="200px" CssClass="form-control" TextMode="MultiLine" Style="resize: none"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td>Remark: 
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtresult" runat="server" Width="350px" CssClass="form-control" TextMode="MultiLine" Style="resize: none"></asp:TextBox>
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td colspan="3" style="text-align: left;">

                                        <asp:Button ID="btnsbmit" runat="server" Text="Submit" ValidationGroup="Client" OnClick="btnsbmit_Click" CssClass="btn btn-primary"></asp:Button>
                                        <%--<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"  ValidationGroup="user" ShowSummary="False" />--%>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddldomain" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <div id="Div1" runat="server" style="overflow: auto; display: none;">
                        <dx:ASPxGridView ID="grdNewProAppReq" runat="server" AutoGenerateColumns="false" ClientInstanceName="grid" KeyFieldName="PAI_Id" EnableRowsCache="False" Theme="DevEx" OnCustomCallback="grdNewProAppReq_CustomCallback" OnCustomButtonCallback="grdNewProAppReq_CustomButtonCallback" OnCustomUnboundColumnData="grdNewProAppReq_CustomUnboundColumnData">
                            <Settings ShowFilterRow="true" ShowFilterRowMenu="true" />
                            <SettingsPager PageSize="10"></SettingsPager>
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="Number" VisibleIndex="0" Width="40px" Caption="Sr. No" UnboundType="String" ReadOnly="true"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="PAI_Project_Name" Caption="Project Number"></dx:GridViewDataTextColumn>
                                <%--<dx:GridViewDataTextColumn FieldName="WMMComp_Id" Caption="WMMComp-Id"></dx:GridViewDataTextColumn>--%>
                                <dx:GridViewDataTextColumn FieldName="PAI_Company_Name" Caption="Company Name"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="PAI_Contact_Person" Caption="Contact Person"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="PAI_Phone_Number" Caption="Phone Number"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="PAI_Email_Id" Caption="Email"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="PAI_Url" Caption="Website Url"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="PAI_Address" Caption="Address"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="PAI_Remark" Caption="Remark"></dx:GridViewDataTextColumn>
                                <dx:GridViewCommandColumn Width="50px" ButtonType="Image" Caption="Edit" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                                    <CustomButtons>
                                        <dx:GridViewCommandColumnCustomButton ID="Edit">
                                            <Image Url="~/Images/Edit.png" ToolTip="Edit" Height="16" Width="16"></Image>
                                        </dx:GridViewCommandColumnCustomButton>
                                    </CustomButtons>

                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                </dx:GridViewCommandColumn>
                            </Columns>
                        </dx:ASPxGridView>
                    </div>
                </div>
                <div class="tab-pane" id="tab2" role="tabpanel">
                    <table class="table table-border-style">
                        <tr>
                            <td>Project Number:
                            </td>
                            <td>
                                <%--   <asp:TextBox ID="lblSalesProjectNumber" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlprojNo" runat="server" Width="200px" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" SetFocusOnError="true" runat="server"
                                    ValidationGroup="Sales" ControlToValidate="ddlprojNo" InitialValue="Select" ForeColor="Red" Display="Dynamic"
                                    ErrorMessage="Please Select Project"></asp:RequiredFieldValidator>
                            </td>
                            <td>Process Name:
                            </td>
                            <td>
                                <asp:TextBox ID="txtProcessNameSales" Width="200px" runat="server" CssClass="form-control"></asp:TextBox></td>
                        </tr>
                        <tr>

                            <td>BDM:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlBDM" Width="200px" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" SetFocusOnError="true" runat="server"
                                    ValidationGroup="Sales" ControlToValidate="ddlBDM" InitialValue="Select" ForeColor="Red" Display="Dynamic"
                                    ErrorMessage="Please Select BDM"></asp:RequiredFieldValidator>
                            </td>
                            <td style="display: none;">Requested Date:
                            </td>
                            <td style="display: none;">
                                <asp:TextBox ID="txtRequestedDate" Width="200px" runat="server" placeholder="01-Jan-2016" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtRequestedDate" SkinID="we" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" SetFocusOnError="true" runat="server" Enabled="false"
                                    ValidationGroup="Sales" ControlToValidate="txtRequestedDate" Display="Dynamic" ForeColor="Red"
                                    ErrorMessage="Please enter Requested Date"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Scope of Project:
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtProjectScope" runat="server" Width="510px" CssClass="form-control" TextMode="MultiLine" Style="resize: none;"></asp:TextBox>
                            </td>


                        </tr>
                        <tr>
                            <td>NDA Signed:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddnda" Width="200px" runat="server" CssClass="form-control" onchange="NDAChange();">
                                    <asp:ListItem Value="Select">Select</asp:ListItem>
                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                    <asp:ListItem Value="0">No</asp:ListItem>
                                    <asp:ListItem Value="2">Not Applicable</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" SetFocusOnError="true" runat="server" ValidationGroup="Sales" ControlToValidate="ddnda" InitialValue="Select" Display="Dynamic" ForeColor="Red"
                                    ErrorMessage="Please select NDA Signed"></asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                            <td></td>

                        </tr>
                        <tr id="NDA" style="display: none;">
                            <td colspan="4">
                                <table class="table table-border-style" border="1" style="border-style: solid; border-width: 1px; text-align: center">
                                    <tr>
                                        <th>Date of the Agreement
                                        </th>
                                        <th>Expiration Date of the Agreement
                                        </th>
                                        <th>Signed by (Client)
                                        </th>
                                        <th>Signed by (Infinity)
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtDateOfAgreement" runat="server" placeholder="01-Jan-2016" Width="130px" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateOfAgreement" SkinID="we" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtExpirationDateOfAgreement" runat="server" placeholder="01-Jan-2016" Width="130px" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtExpirationDateOfAgreement" SkinID="we" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlNDASignedByClient" runat="server" Width="100px" CssClass="form-control">
                                                <asp:ListItem Value="NULL">Select</asp:ListItem>
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Value="0">No</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlNDASignedByInfinity" runat="server" Width="100px" CssClass="form-control">
                                                <asp:ListItem Value="NULL">Select</asp:ListItem>
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Value="0">No</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>Upload Document:
                                        </td>
                                        <td>
                                            <asp:FileUpload ID="fpNDA" runat="server" Width="200px" CssClass="form-control" /></td>
                                        <td>
                                            <asp:LinkButton ID="LinkButtonNDA" runat="server" Style="float: right; padding-right: 150px; font-size: 10px; font-weight: bold" OnClick="LinkButtonNDA_Click">View</asp:LinkButton>
                                        </td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>MSA Signed:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddsla" runat="server" Width="200px" CssClass="form-control" onchange="SLAChange();">
                                    <asp:ListItem Value="Select">Select</asp:ListItem>
                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                    <asp:ListItem Value="0">No</asp:ListItem>
                                    <asp:ListItem Value="2">Not Applicable</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" SetFocusOnError="true" runat="server" ValidationGroup="Sales" ControlToValidate="ddsla" InitialValue="Select" Display="Dynamic" ForeColor="Red"
                                    ErrorMessage="Please select MSA Signed"></asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr id="trsla" style="display: none;">
                            <td colspan="6">
                                <table class="table" border="1" style="border-style: solid; border-width: 1px; text-align: center">
                                    <tr>
                                        <th>Date of the Agreement
                                        </th>
                                        <th>Expiration Date of the Agreement
                                        </th>
                                        <th>Signed by (Client)
                                        </th>
                                        <th>Signed by (Infinity)
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtDateOfSLAAgreement" runat="server" placeholder="01-Jan-2016" Width="130px" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtDateOfSLAAgreement" SkinID="we" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtExpirationDateOfSLAAgreement" runat="server" placeholder="01-Jan-2016" Width="130px" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtExpirationDateOfSLAAgreement" SkinID="we" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlSLASignedByClient" runat="server" Width="100px" CssClass="form-control">
                                                <asp:ListItem Value="NULL">Select</asp:ListItem>
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Value="0">No</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlSLASignedByInfinity" runat="server" Width="100px" CssClass="form-control">
                                                <asp:ListItem Value="NULL">Select</asp:ListItem>
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Value="0">No</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Upload Document:
                                        </td>
                                        <td>
                                            <asp:FileUpload ID="fpSLA" runat="server" Width="200px" CssClass="form-control" /></td>
                                        <td>
                                            <asp:LinkButton ID="LinkButtonSLA" runat="server" Style="float: right; padding-right: 150px; font-size: 10px; font-weight: bold" OnClick="LinkButtonSLA_Click">View</asp:LinkButton>
                                        </td>
                                        <td></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>Project Status:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlProjectStatus" runat="server" Width="200px" CssClass="form-control" onchange="GetLiveStopped();">
                                    <asp:ListItem Value="Select">Select</asp:ListItem>
                                    <asp:ListItem Value="Live">Live</asp:ListItem>
                                    <asp:ListItem Value="Demo">Demo</asp:ListItem>
                                    <asp:ListItem Value="Test">Test</asp:ListItem>
                                    <asp:ListItem Value="Live Stopped">Live Stopped</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" SetFocusOnError="true" runat="server"
                                    ValidationGroup="Sales" ControlToValidate="ddlProjectStatus" InitialValue="Select" ForeColor="Red" Display="Dynamic"
                                    ErrorMessage="Please Select ProjectStatus"></asp:RequiredFieldValidator>
                            </td>
                            <td>Expected Volume:
                            </td>
                            <td>
                                <asp:TextBox ID="txtExpectedVolume" runat="server" Width="200px" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" SetFocusOnError="true" runat="server"
                                    ValidationGroup="Sales" ControlToValidate="txtExpectedVolume" Display="Dynamic" ForeColor="Red"
                                    ErrorMessage="Please enter Expected Volume"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="trlivestopped" runat="server" style="display: none;">
                            <td>Stopped Date:
                            </td>
                            <td>
                                <asp:TextBox ID="txtStoppedDate" runat="server" Width="200px" CssClass="form-control"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="txtStoppedDate" SkinID="we" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                            </td>
                            <td>Stopped Remark:</td>
                            <td>
                                <asp:TextBox ID="txtStoppedRemark" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Expected Start Date:
                            </td>
                            <td>
                                <asp:TextBox ID="txtExpectedStartDate" runat="server" Width="200px" placeholder="01-Jan-2016" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtExpectedStartDate" SkinID="we" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" SetFocusOnError="true" runat="server"
                                    ValidationGroup="Sales" ControlToValidate="txtExpectedStartDate" Display="Dynamic" ForeColor="Red"
                                    ErrorMessage="Please enter Expected Date"></asp:RequiredFieldValidator>
                            </td>
                            <td>Project Duration:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlProjectDuration" runat="server" Width="200px" CssClass="form-control">
                                    <asp:ListItem Value="Select">Select</asp:ListItem>
                                    <asp:ListItem Value="Short Term">Short Term</asp:ListItem>
                                    <asp:ListItem Value="Long Term">Long Term</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" SetFocusOnError="true" runat="server"
                                    ValidationGroup="Sales" ControlToValidate="ddlProjectDuration" InitialValue="Select" ForeColor="Red" Display="Dynamic"
                                    ErrorMessage="Please Select Project Duration"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <tr>

                                <td>Remark:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSalesRemark" runat="server" resize="none" CssClass="form-control" TextMode="MultiLine" Width="310px" Style="resize: none;"></asp:TextBox>
                                </td>
                                <td>Rate Revision Date:</td>
                                <td>
                                    <asp:TextBox ID="txtRateRevisionDate" runat="server" Width="200px" CssClass="form-control"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="txtRateRevisionDate" SkinID="we" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                </td>
                            </tr>
                        </tr>

                        <tr>
                            <td></td>
                            <td colspan="3" style="text-align: left;">
                                <asp:Button ID="btnSalesInformation" runat="server" Text="Submit" OnClick="btnSalesInformation_Click" ValidationGroup="Sales" CssClass="btn btn-primary"></asp:Button>

                            </td>
                        </tr>
                    </table>
                </div>
                <div class="tab-pane" id="tab3" role="tabpanel">
                    <div id="QcBilling" runat="server">
                        <dx:ASPxGridView ID="grdBillingParams" DataSourceID="ds2" runat="server" AutoGenerateColumns="false" ClientInstanceName="gridBilling" EnableRowsCache="False" KeyFieldName="IBP_Id" Theme="Office2010Silver"
                            OnCustomUnboundColumnData="grdBillingParams_CustomUnboundColumnData" Font-Size="11px" SettingsPager-PageSize="50" OnCustomCallback="grdBillingParams_CustomCallback" OnBeforePerformDataSelect="grdBillingParams_BeforePerformDataSelect">
                            <SettingsBehavior ConfirmDelete="true" />
                            <ClientSideEvents EndCallback="OnEndCallback" />
                            <SettingsEditing Mode="Inline"></SettingsEditing>
                            <Styles Header-Font-Bold="true"></Styles>
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="IBP_Id" Caption="AssetId" Visible="false"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Number" VisibleIndex="0" Caption="Sr. #" Width="20px" UnboundType="String" ReadOnly="true"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="IBP_ParameterName" Caption="Parameter Name"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="IBV_Comment" Caption="Is Applicable?" Width="70px">
                                    <DataItemTemplate>
                                        <asp:DropDownList ID="ddlCompany" runat="server" OnInit="ddlCompany_Init" Width="100px" CssClass="form-control"></asp:DropDownList>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="IBV_Additional" Caption="Additional Charge" Width="60px">
                                    <DataItemTemplate>
                                        <asp:DropDownList ID="ddlLocation" runat="server" OnInit="ddlLocation_Init" CssClass="form-control"></asp:DropDownList>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="IBV_ChargeType" Caption="Charge Type" Width="140px">
                                    <DataItemTemplate>
                                        <asp:DropDownList ID="ddlChargeType" runat="server" OnInit="ddlChargeType_Init" CssClass="form-control">
                                            <asp:ListItem Value="Select">Select</asp:ListItem>
                                            <asp:ListItem Value="Fix Amount">Fix Amount</asp:ListItem>
                                            <asp:ListItem Value="Incremental">Incremental</asp:ListItem>
                                            <asp:ListItem Value="Percentage">Percentage</asp:ListItem>
                                        </asp:DropDownList>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="IBV_Remark" Caption="Price" Width="50px">
                                    <DataItemTemplate>
                                        <asp:TextBox ID="txtRemark" runat="server" OnInit="txtRemark_Init" Width="65px" CssClass="form-control"></asp:TextBox>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="IBV_CommentFromBDM" Caption="Remark" Width="60px">
                                    <DataItemTemplate>
                                        <asp:TextBox ID="txtCommentFromBDM" runat="server" OnInit="txtCommentFromBDM_Init" CssClass="form-control" TextMode="MultiLine" Width="300px"></asp:TextBox>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:ASPxGridView>

                        <asp:SqlDataSource ID="ds2" runat="server" SelectCommand="usp_getallbillingParameters" SelectCommandType="StoredProcedure" ConnectionString="Data Source=23.111.175.186;Initial Catalog=InfinityBilling;Persist Security Info=True;User ID=sa;Password=#Cl0ud^$ecure4; Pooling=true; Min Pool Size=1; Max Pool Size=10; Connect Timeout=200; Packet Size=8192">
                            <SelectParameters>
                                <asp:SessionParameter Name="ProjectID" DbType="Int32" Direction="Input" SessionField="ProjectId" />
                                <asp:SessionParameter Name="DomainId" DbType="Int32" Direction="Input" SessionField="formdate" />
                            </SelectParameters>
                        </asp:SqlDataSource>

                    </div>
                    <br />
                    <dx:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False" Text="Update" CssClass="btn btn-primary">
                        <ClientSideEvents Click="function(s, e) {gridBilling.PerformCallback('update');}" />
                    </dx:ASPxButton>
                </div>
                <div class="tab-pane" id="tab4" role="tabpanel">
                    <table class="table table-border-style" id="tblOtherFTE" runat="server">
                        <tr>
                            <td id="tdRate" runat="server"><b>Rate:</b></td>
                            <td>
                                <asp:TextBox ID="txtHourlyRate" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnFTESubmit" runat="server" Text="Submit" OnClick="btnFTESubmit_Click" />
                            </td>
                        </tr>
                    </table>
                    <table class="table table-card" id="tbl861007" runat="server" style="display: none;">
                        <tr>
                            <td><b>Rate for First 1200 ballots:</b></td>
                            <td>
                                <asp:TextBox ID="txtfirst1200Rate" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td><b>Rate for Additional ballots:</b></td>
                            <td>
                                <asp:TextBox ID="txtAdditionalBallotsRate" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td><b>Wire Transfer Charges:</b></td>
                            <td>
                                <asp:TextBox ID="txtWireTransferCharges" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btnSubmit861" runat="server" Text="Submit" OnClick="btnSubmit861_Click" />
                            </td>
                        </tr>
                    </table>
                    <table class="table table-card" id="tblWholeloan" runat="server" style="display: none;">
                        <tr>
                            <td><b>Client:</b></td>
                            <td>
                                <asp:TextBox ID="txtBilledTo" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td><b>Price:</b></td>
                            <td>
                                <asp:TextBox ID="txtWholeloanPrice" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnwholeloansubmit" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btnwholeloansubmit_Click" />
                            </td>
                        </tr>
                    </table>
                    <hr />
                    <dx:ASPxGridView ID="grdCostWholeLoan" runat="server" AutoGenerateColumns="false" ClientInstanceName="grdCostWhole" EnableRowsCache="False" KeyFieldName="CostID" Theme="Office2010Silver"
                        OnCustomUnboundColumnData="grdBillingParams_CustomUnboundColumnData" Font-Size="11px" SettingsPager-PageSize="10">
                        <Styles Header-Font-Bold="true"></Styles>
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="CostID" Caption="CostID" Visible="false"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Number" VisibleIndex="0" Caption="Sr. #" Width="20px" UnboundType="String" ReadOnly="true"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="BilledTo" Caption="Bill To"></dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Price" Caption="Price"></dx:GridViewDataTextColumn>

                        </Columns>
                    </dx:ASPxGridView>
                </div>

                <div class="tab-pane" id="tab5" role="tabpanel">
                    <div id="QcBillingPricing" runat="server">
                        <dx:ASPxGridView ID="grdCostingParameters" runat="server" AutoGenerateColumns="false" ClientInstanceName="gridCosting" EnableRowsCache="False" KeyFieldName="IBP_Id" Theme="Office2010Silver"
                            OnCustomUnboundColumnData="grdBillingParams_CustomUnboundColumnData" Font-Size="11px" SettingsPager-PageSize="50" DataSourceID="dsCosting" OnCustomCallback="grdCostingParameters_CustomCallback" OnBeforePerformDataSelect="grdCostingParameters_BeforePerformDataSelect">
                            <ClientSideEvents EndCallback="OnEndCallbackCosting" />
                            <SettingsEditing Mode="Inline"></SettingsEditing>
                            <Styles Header-Font-Bold="true"></Styles>
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="IBP_Id" Caption="AssetId" Visible="false"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="IBV_ERPProjectId" Caption="ProjectID" Visible="false"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Number" VisibleIndex="0" Caption="Sr. #" Width="20px" UnboundType="String" ReadOnly="true"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="IBP_ParameterName" Caption="Parameter Name"></dx:GridViewDataTextColumn>
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

                                <dx:GridViewDataTextColumn FieldName="IBV_ChargeType" Caption="Charge Type" Width="140px">
                                    <DataItemTemplate>
                                        <asp:DropDownList ID="ddlChargeType" runat="server" OnInit="ddlChargeType_Init" CssClass="form-control">
                                            <asp:ListItem Value="Select">Select</asp:ListItem>
                                            <asp:ListItem Value="Fix Amount">Fix Amount</asp:ListItem>
                                            <asp:ListItem Value="Incremental">Incremental</asp:ListItem>
                                            <asp:ListItem Value="Percentage">Percentage</asp:ListItem>
                                        </asp:DropDownList>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>

                                <dx:GridViewDataTextColumn FieldName="IBV_Remark" Caption="Price">
                                    <DataItemTemplate>
                                        <asp:TextBox ID="txtCostingRemark" runat="server" OnInit="txtCostingRemark_Init" Width="65px" CssClass="form-control"></asp:TextBox>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="IBV_CommentFromBDM" Caption="Remark" Width="130px" CellStyle-Font-Size="13px"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="Billing Headers" Width="60px">
                                    <DataItemTemplate>
                                        <asp:DropDownList ID="ddlBillingColumns" runat="server" OnInit="ddlBillingColumns_Init" Width="130px" CssClass="form-control">
                                            <asp:ListItem Value="Select">Select</asp:ListItem>
                                            <asp:ListItem Value="Invoice#">Invoice#</asp:ListItem>
                                            <asp:ListItem Value="Dispatched Record">Dispatched Record</asp:ListItem>
                                            <asp:ListItem Value="No Of Records">No Of Records</asp:ListItem>
                                            <asp:ListItem Value="Order#">Order#</asp:ListItem>
                                            <asp:ListItem Value="Rush">Rush</asp:ListItem>
                                            <asp:ListItem Value="Weekday">Weekday</asp:ListItem>
                                            <asp:ListItem Value="Weekend">Weekend</asp:ListItem>
                                            <asp:ListItem Value="Product Type">Product Type</asp:ListItem>
                                            <asp:ListItem Value="All">All Update</asp:ListItem>
                                            <asp:ListItem Value="Update">Update</asp:ListItem>
                                            <asp:ListItem Value="Update 1">Update 1</asp:ListItem>
                                            <asp:ListItem Value="Update 2">Update 2</asp:ListItem>
                                            <asp:ListItem Value="Update 3">Update 3</asp:ListItem>
                                            <asp:ListItem Value="Update 4">Update 4</asp:ListItem>
                                            <asp:ListItem Value="Update 4">Update 5</asp:ListItem>
                                            <asp:ListItem Value="Status">Status</asp:ListItem>
                                            <asp:ListItem Value="After Hours">After Hours</asp:ListItem>
                                            <asp:ListItem Value="Checklist to Client">Checklist to Client</asp:ListItem>
                                            <asp:ListItem Value="Delivery Procedure to client">Delivery Procedure to client</asp:ListItem>
                                        </asp:DropDownList>
                                    </DataItemTemplate>

                                </dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        <asp:SqlDataSource ID="dsCosting" runat="server" SelectCommand="usp_getallbillingParametersForCosting" SelectCommandType="StoredProcedure" ConnectionString="Data Source=23.111.175.186;Initial Catalog=InfinityBilling;Persist Security Info=True;User ID=sa;Password=#Cl0ud^$ecure4; Pooling=true; Min Pool Size=1; Max Pool Size=10; Connect Timeout=200; Packet Size=8192">
                            <SelectParameters>
                                <asp:SessionParameter Name="ProjectID" DbType="Int32" Direction="Input" SessionField="ProjectId" />
                                <asp:SessionParameter Name="DomainId" DbType="Int32" Direction="Input" SessionField="formdate" />

                            </SelectParameters>
                        </asp:SqlDataSource>
                    </div>
                    <br />
                    <dx:ASPxButton ID="ASPxButton2" runat="server" AutoPostBack="False" Text="Update" CssClass="btn btn-primary">
                        <ClientSideEvents Click="function(s, e) {gridCosting.PerformCallback('update');}" />
                    </dx:ASPxButton>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
