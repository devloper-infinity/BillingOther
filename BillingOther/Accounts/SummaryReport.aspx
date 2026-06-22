<%@ Page Title="" Language="C#" MasterPageFile="~/Accounts/Accounts.Master" AutoEventWireup="true" CodeBehind="SummaryReport.aspx.cs" Inherits="BillingOther.Accounts.SummaryReport" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Data.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Data" TagPrefix="dx" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        var keyValue;
        function OnMoreInfoClick(contentUrl) {
            clientPopupControl.SetContentUrl(contentUrl);
            clientPopupControl.Show();
        }

        function close() {
            clientPopupControl.Hide();
        }
        function OnMoreInfoClickInvDetails(element, key) {
            CallbackPanelInvDetails.SetContentHtml("");
            popupSendInvReport.ShowAtElement(element);
            keyValue = key;
        }
        function popupInvReport_Shown(s, e) {
            CallbackPanelInvDetails.PerformCallback(keyValue);
        }
        function OnMoreInfoClickTaxDetails(element, key) {
            CallbackPanelTaxDetails.SetContentHtml("");
            popupExcelReport.ShowAtElement(element);
            keyValue = key;
        }
        function popupExcelReport_Shown(s, e) {
            CallbackPanelTaxDetails.PerformCallback(keyValue);
        }
        function OnMoreInfoClickViewEmailTemplate(element, key) {
            CallbackPanelViewEmailTemplate.SetContentHtml("");
            popupViewEmailTemplate.ShowAtElement(element);
            keyValue = key;
        }
        function popupViewEmailTemplate_Shown(s, e) {
            CallbackPanelViewEmailTemplate.PerformCallback(keyValue);
        }
        function OnMoreInfoClickOrderFinalSummary(element, key) {
            CallbackPanelOrderFinalSummary.SetContentHtml("");
            popupOrderFinalSummary.ShowAtElement(element);
            keyValue = key;
        }

        function popupOrderFinalSummary_Shown(s, e) {
            CallbackPanelOrderFinalSummary.PerformCallback(keyValue);
        }
        function OnMoreInfoClickSendTestEmail(element, key) {
            CallbackPanelTestEmail.SetContentHtml("");
            popupSendTestEmail.ShowAtElement(element);
            keyValue = key;
        }
        function popupInvTestEmail_Shown(s, e) {
            CallbackPanelTestEmail.PerformCallback(keyValue);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvError" runat="server"></div>
    <div>
        <div id="Div1" runat="server" style="padding-bottom: 20px;">
            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
        </div>
        <dx:ASPxPopupControl ID="popupSendInvReport" ClientInstanceName="popupSendInvReport" runat="server"
            AllowDragging="true"
            PopupVerticalAlign="WindowCenter" PopupVerticalOffset="100" PopupHorizontalAlign="WindowCenter" HeaderText="Update Invoice" Width="700px" Height="200px">
            <HeaderStyle Font-Bold="true" />
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                    <dx:ASPxCallbackPanel ID="ASPxCallbackPanel1" runat="server" OnCallback="ASPxCallbackPanel1_Callback" ClientInstanceName="CallbackPanelInvDetails" Width="250px" Height="100px" RenderMode="Table">
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent1" runat="server">
                                <asp:Button ID="Button1" runat="server" Text="Send Mail" CssClass="btn btn-primary" OnClick="Button1_Click" />
                                <table class="table-condensed">
                                    <tr>
                                        <td>
                                            <div id="dvEmailTemplate" runat="server"></div>
                                        </td>
                                        <%-- <td>Client</td>
                                        <td>
                                          
                                            <asp:DropDownList runat="server" ID="ddlClientList" onchange="GetSelectedClient(this)" ></asp:DropDownList>
                                        </td>--%>
                                    </tr>
                                </table>

                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxCallbackPanel>
                </dx:PopupControlContentControl>
            </ContentCollection>
            <ClientSideEvents Shown="popupInvReport_Shown" />
        </dx:ASPxPopupControl>
        <dx:ASPxPopupControl ID="popupExcelReport" ClientInstanceName="popupExcelReport" runat="server" AllowDragging="true" PopupHorizontalAlign="WindowCenter" HeaderText="Follow Up Remark" PopupVerticalAlign="WindowCenter" Width="700px">
            <HeaderStyle Font-Bold="true" />
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    <dx:ASPxCallbackPanel ID="CallbackPanelTaxDetails" runat="server" OnCallback="CallbackPanelTaxDetails_Callback" ClientInstanceName="CallbackPanelTaxDetails" Width="700px" Height="300px" RenderMode="Table">
                        <PanelCollection>
                            <dx:PanelContent ID="pnlViewTaxDetails" runat="server">
                                <table class="table-condensed">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblerrorRemark" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>Remark</td>
                                        <td>
                                            <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Width="200px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" runat="server"
                                                ValidationGroup="test" ControlToValidate="txtRemark" Display="None" ForeColor="Red"
                                                ErrorMessage="Please enter Remark"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnUpdateRemark" runat="server" Text="Update Remark" ValidationGroup="test" CssClass="btn btn-primary" OnClick="btnUpdateRemark_Click" /></td>
                                        <td></td>
                                    </tr>
                                </table>
                                <dx:ASPxGridViewExporter runat="server" ID="grdExportExceldata" GridViewID="grdTaxDetails" FileName="ILS Excel Report"></dx:ASPxGridViewExporter>
                                <dx:ASPxGridView ID="grdTaxDetails" runat="server" AutoGenerateColumns="true" EnableRowsCache="false" KeyFieldName="OrderID" Theme="Office2010Silver" Width="100%" OnCustomCallback="grdTaxDetails_CustomCallback" OnCustomUnboundColumnData="grdTaxDetails_CustomUnboundColumnData">
                                    <Settings HorizontalScrollBarMode="Visible" />
                                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                    <Settings VerticalScrollBarMode="Auto" />
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="Number" VisibleIndex="0" Caption="Sr. #" Width="40px" UnboundType="String" ReadOnly="True" ShowInCustomizationForm="True"></dx:GridViewDataTextColumn>

                                        <dx:GridViewDataTextColumn FieldName="Remark" Caption="Remark" Width="400px" CellStyle-Wrap="True" VisibleIndex="1" CellStyle-ForeColor="Black" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                                            <HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Center" ForeColor="Black"></CellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="AddedBy" Caption="User" Width="100px" VisibleIndex="2" CellStyle-ForeColor="Black" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                                            <HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Center" ForeColor="Black"></CellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn PropertiesTextEdit-DisplayFormatString="dd-MMM-yyyy" FieldName="Addeddate" Caption="AddedDate" Width="100px" VisibleIndex="3" CellStyle-ForeColor="Black" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">

                                            <CellStyle HorizontalAlign="Center" ForeColor="Black"></CellStyle>
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                </dx:ASPxGridView>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxCallbackPanel>
                </dx:PopupControlContentControl>
            </ContentCollection>
            <ClientSideEvents Shown="popupExcelReport_Shown" />
        </dx:ASPxPopupControl>
        <dx:ASPxPopupControl ID="ASPxPopupControl3" ClientInstanceName="popupViewEmailTemplate" runat="server" AllowDragging="true" PopupHorizontalAlign="WindowCenter" HeaderText="Send Email" PopupVerticalAlign="WindowCenter" Width="700px">
            <HeaderStyle Font-Bold="true" />
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl5" runat="server">
                    <dx:ASPxCallbackPanel ID="ASPxCallbackPanelViewEmailTemplate" runat="server" OnCallback="ASPxCallbackPanelViewEmailTemplate_Callback" ClientInstanceName="CallbackPanelViewEmailTemplate" Width="250px" Height="100px" RenderMode="Table">
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent3" runat="server">
                                <table class="table-condensed" border="1" width="100%">
                                    <tr>
                                        <td align="top">To:</td>
                                        <td>
                                            <asp:Label ID="txtTo" runat="server"></asp:Label>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="top">CC:</td>
                                        <td>
                                            <asp:Label ID="txtCC" runat="server"></asp:Label>


                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="top">BCC:</td>
                                        <td>
                                            <asp:Label ID="txtBCC" runat="server"></asp:Label>

                                        </td>

                                    </tr>
                                    <tr>
                                        <td align="top">Attachment:</td>
                                        <td>
                                            <asp:LinkButton ID="lnkAttachment" runat="server" OnClick="lnkAttachment_Click">View</asp:LinkButton>

                                        </td>

                                    </tr>
                                    <tr>
                                        <td align="top">Sent Date:</td>
                                        <td>
                                            <asp:Label ID="lblDateSent" runat="server"></asp:Label>

                                        </td>

                                    </tr>
                                    <div id="dvHtml" runat="server">
                                    </div>
                                    <tr id="Tr1" runat="server" visible="false">
                                        <td>Subject:
                                        </td>
                                        <td>
                                            <asp:Label ID="txtSubject" runat="server"></asp:Label>
                                        </td>
                                    </tr>

                                    <%--</tr>                  <td>
                                            <asp:Button ID="btnPreview" runat="server" Text="Preview Mail" style="font-weight: bold; font-size: 14px; padding: 5px 10px;" OnClick="btnPreview_Click" /></td>
                                        <td></td>
                                    </tr>--%>
                                </table>

                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxCallbackPanel>
                </dx:PopupControlContentControl>
            </ContentCollection>
            <ClientSideEvents Shown="popupViewEmailTemplate_Shown" />
        </dx:ASPxPopupControl>
        <dx:ASPxPopupControl ID="popupOrderFinalSummary" ClientInstanceName="popupOrderFinalSummary" runat="server" AllowDragging="true"
            PopupVerticalAlign="WindowCenter" PopupVerticalOffset="100" PopupHorizontalAlign="WindowCenter" HeaderText="Update Invoice" Width="700px" Height="200px">
            <HeaderStyle Font-Bold="true" />
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl10" runat="server">
                    <dx:ASPxCallbackPanel ID="CallbackPanelOrderFinalSummary" runat="server" OnCallback="CallbackPanelOrderFinalSummary_Callback" ClientInstanceName="CallbackPanelOrderFinalSummary" Height="200px" RenderMode="Table" Width="850px">
                        <PanelCollection>
                            <dx:PanelContent ID="pnlViewOrderFinalSummary" runat="server">
                                <table class="table">
                                    <tr>
                                        <td><b>Project #:</b></td>
                                        <td>
                                            <asp:Label ID="lblProjectPopup" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><b>Billing Period:</b></td>
                                        <td>
                                            <asp:Label ID="lblInvoiceNoPopup" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><b>Invoice received by client (date):</b></td>
                                        <td>
                                            <dx:ASPxDateEdit ID="dxInvrecieved" runat="server">
                                                <ClientSideEvents GotFocus="function (s, e) { window.setTimeout(function(){ s.ShowDropDown(); }, 10); }" />
                                            </dx:ASPxDateEdit>
                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                        ControlToValidate="dxInvrecieved" SetFocusOnError="true" ErrorMessage="Please Enter Invoice received by client (date)."
                        ForeColor="Red" Display="Dynamic" ValidationGroup="Client"></asp:RequiredFieldValidator>--%>
                                           
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><b>No dispute confirmed by client (date) :</b></td>
                                        <td>
                                            <dx:ASPxDateEdit ID="dxInvConfirm" runat="server">
                                                <ClientSideEvents GotFocus="function (s, e) { window.setTimeout(function(){ s.ShowDropDown(); }, 10); }" />
                                            </dx:ASPxDateEdit>

                                            <%--                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                        ControlToValidate="dxInvConfirm" SetFocusOnError="true" ErrorMessage="Please Enter No dispute confirmed by client (date)."
                        ForeColor="Red" Display="Dynamic" ValidationGroup="Client"></asp:RequiredFieldValidator>--%>
                                           
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><b>Communication regarding receipt of invoice :</b></td>
                                        <td>
                                            <asp:DropDownList ID="ddlReciep" runat="server">
                                                <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                                <asp:ListItem Text="By Mail" Value="By Mail"></asp:ListItem>
                                                <asp:ListItem Text="By Call" Value="By Call"></asp:ListItem>
                                            </asp:DropDownList>
                                            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" SetFocusOnError="true" runat="server"
                        ValidationGroup="Client" ControlToValidate="ddlReciep" InitialValue="0" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Please Select receipt of invoice"></asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><b>Remark :</b></td>
                                        <td>
                                            <asp:TextBox ID="txtInvRemark" runat="server" Width="500px" TextMode="MultiLine" Style="resize: none;"></asp:TextBox>
                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                        ControlToValidate="txtInvRemark" SetFocusOnError="true" ErrorMessage="Please Enter Remark."
                        ForeColor="Red" Display="Dynamic" ValidationGroup="Client"></asp:RequiredFieldValidator>--%>
                                           
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><b>Completed Date:</b></td>
                                        <td>
                                            <dx:ASPxDateEdit ID="dxDate" runat="server"></dx:ASPxDateEdit>
                                            <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                        ControlToValidate="dxDate" SetFocusOnError="true" ErrorMessage="Please Enter Completed Date."
                        ForeColor="Red" Display="Dynamic" ValidationGroup="Client"></asp:RequiredFieldValidator>--%>
                                         
                                        </td>
                                    </tr>

                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="btnUpdateInvoice" runat="server" Text="Update Invoice" CssClass="btn btn-primary" OnClick="btnUpdateInvoice_Click" /></td>
                                        <td></td>
                                    </tr>
                                </table>

                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxCallbackPanel>
                </dx:PopupControlContentControl>
            </ContentCollection>
            <ClientSideEvents Shown="popupOrderFinalSummary_Shown" />
        </dx:ASPxPopupControl>
        <dx:ASPxPopupControl ID="popupSendTestEmail" ClientInstanceName="popupSendTestEmail" runat="server" AllowDragging="true" PopupHorizontalAlign="WindowCenter" HeaderText="Send Email" PopupVerticalAlign="WindowCenter" Width="700px">
            <HeaderStyle Font-Bold="true" />
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
                    <dx:ASPxCallbackPanel ID="ASPxCallbackPanel2" runat="server" OnCallback="ASPxCallbackPanel2_Callback" ClientInstanceName="CallbackPanelTestEmail" Width="250px" Height="100px" RenderMode="Table">
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent2" runat="server">
                                <table class="table-condensed">
                                    <tr>
                                        <td>Email ID</td>
                                        <td>

                                            <asp:TextBox ID="txttestEmailids" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" SetFocusOnError="true" runat="server"
                                                ValidationGroup="SendTestEmail" ControlToValidate="txttestEmailids" Display="Dynamic" ForeColor="Red"
                                                ErrorMessage="Please enter email id"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnsendEmail" runat="server" Text="Send Mail" CssClass="btn btn-primary" OnClick="btnsendEmail_Click" ValidationGroup="SendTestEmail" /></td>
                                        <td></td>
                                    </tr>
                                </table>

                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxCallbackPanel>
                </dx:PopupControlContentControl>
            </ContentCollection>
            <ClientSideEvents Shown="popupInvTestEmail_Shown" />
        </dx:ASPxPopupControl>

        <table class="table-condensed" style="display: none;">
            <tr>
                <td><b>Project Group:</b>
                </td>
                <td>
                    <asp:DropDownList ID="ddlprojectGroup" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlprojectGroup_SelectedIndexChanged">
                    </asp:DropDownList>
                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" runat="server"
                        ValidationGroup="Client" ControlToValidate="ddlprojectGroup" InitialValue="0" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Please Select Project Group"></asp:RequiredFieldValidator>--%>
                </td>
                <td width="150px"><b>Billing Period:</b></td>
                <td>
                    <asp:DropDownList ID="ddlPeriod" runat="server" Width="180px" Height="25px">
                    </asp:DropDownList>
                    <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" SetFocusOnError="true" runat="server"
                        ValidationGroup="Client" ControlToValidate="ddlPeriod" InitialValue="Select" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Please Select Billing Period"></asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr>
                <%-- <td>Project Number :
                </td>
                <td>
                    <asp:DropDownList ID="ddlProjNo" runat="server" class="form-control">
                    </asp:DropDownList>
                </td>--%>
                <td>
                    <asp:Button ID="btnsubmit" runat="server" Text="Show" OnClick="btnsubmit_Click" CssClass="btn btn-primary" ValidationGroup="Client"></asp:Button>
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td>
                    <CR:CrystalReportViewer ID="ILSReport" runat="server" Width="350px" AutoDataBind="true"
                        ToolPanelView="None" Height="50px" SeparatePages="False" />

                </td>
            </tr>
        </table>

        <table class="table table-responsive">
            <tr>
                <td><b>Month: </b></td>
                <td>
                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control">
                        <asp:ListItem Value="Select">Select</asp:ListItem>
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
                </td>
                <td><b>Year: </b></td>
                <td>
                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control">
                        <asp:ListItem Value="Select">Select</asp:ListItem>
                        <asp:ListItem Value="2019">2019</asp:ListItem>
                        <asp:ListItem Value="2020">2020</asp:ListItem>
                        <asp:ListItem Value="2021">2021</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnShowFilter" runat="server" Text="Show" OnClick="btnShowFilter_Click" CssClass="btn btn-primary" />
                </td>
                <td>
                    <asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick="btnExport_Click" CssClass="btn btn-primary" />
                </td>
                <td>
                    <asp:Button ID="btnExportToPDF" runat="server" Text="Export To PDF" OnClick="btnExportToPDF_Click" CssClass="btn btn-primary" />
                </td>
            </tr>
        </table>

        <dx:ASPxPopupControl ID="popupControl" runat="server" ClientInstanceName="clientPopupControl" CloseAction="CloseButton" Height="10px" Modal="True" Width="10px" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
        <dx:ASPxGridView ID="grdSummaryDetails" runat="server" AutoGenerateColumns="false" ClientInstanceName="grid" KeyFieldName="GroupNumber;BillingPeriod" Theme="Default" OnCustomUnboundColumnData="grdSummaryDetails_CustomUnboundColumnData">
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <Columns>
                <dx:GridViewDataTextColumn FieldName="Number" VisibleIndex="0" Width="40px" Caption="Sr. No." UnboundType="String" ReadOnly="True" ShowInCustomizationForm="True">
                    <HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                    <CellStyle HorizontalAlign="Center"></CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Project Group" FieldName="GroupNumber">
                    <HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                    <CellStyle HorizontalAlign="Center"></CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Billing Period" FieldName="BillingPeriod" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" Width="150px">
                    <HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                    <CellStyle HorizontalAlign="Center"></CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Order Count" FieldName="InvoiceOrderCount" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                    <HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                    <CellStyle HorizontalAlign="Center"></CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Total Amount" FieldName="InvoiceTotal" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                    <HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                    <CellStyle HorizontalAlign="Center"></CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Report" ShowInCustomizationForm="True" Width="80px" CellStyle-ForeColor="Black" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                    <DataItemTemplate>
                        <dx:ASPxHyperLink ID="hyperLink3" runat="server" OnInit="lnkDisp_Init" ImageUrl="~/Images/pdf.gif"></dx:ASPxHyperLink>
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Invoice Generated Date" FieldName="AddedDate" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" Width="150px">
                    <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Wrap="True"></HeaderStyle>
                    <CellStyle HorizontalAlign="Center"></CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Is Invoice Sent To Client?" FieldName="InvoiceSent" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" Width="150px">
                    <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Wrap="True"></HeaderStyle>
                    <CellStyle HorizontalAlign="Center"></CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Invoice Delivered Date" FieldName="DeliveredDate" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" Width="150px">
                    <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Wrap="True"></HeaderStyle>
                    <CellStyle HorizontalAlign="Center"></CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Invoice Read Date" FieldName="ReadDate" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" Width="150px">
                    <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Wrap="True"></HeaderStyle>
                    <CellStyle HorizontalAlign="Center"></CellStyle>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn Caption="Send To Client" ShowInCustomizationForm="True" CellStyle-ForeColor="Black" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                    <DataItemTemplate>
                        <a href="#" onclick="OnMoreInfoClickInvDetails(this, '<%# Container.KeyValue %>')">Send To Client</a>
                    </DataItemTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataTextColumn Caption="Update Invoice" ShowInCustomizationForm="True" Width="150px" CellStyle-ForeColor="Black" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                    <DataItemTemplate>
                        <a href="#" onclick="OnMoreInfoClickOrderFinalSummary(this, '<%# Container.KeyValue %>')">Update Invoice</a>
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn Caption="FollowUp Remark" Visible="false" ShowInCustomizationForm="True" CellStyle-ForeColor="Black" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                    <DataItemTemplate>
                        <a href="javascript:void(0);" onclick="OnMoreInfoClickTaxDetails(this, '<%# Container.KeyValue %>')">FollowUp Remark</a>
                    </DataItemTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Visible="false" Caption="Email Template" ShowInCustomizationForm="True" CellStyle-ForeColor="Black" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                    <DataItemTemplate>
                        <a href="javascript:void(0);" onclick="OnMoreInfoClickViewEmailTemplate(this, '<%# Container.KeyValue %>')">View</a>
                    </DataItemTemplate>
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Visible="false" Caption="Send Test Email" ShowInCustomizationForm="True" CellStyle-ForeColor="Black" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                    <DataItemTemplate>
                        <a href="javascript:void(0);" onclick="OnMoreInfoClickSendTestEmail(this, '<%# Container.KeyValue %>')">Send </a>
                    </DataItemTemplate>
                </dx:GridViewDataColumn>
            </Columns>
        </dx:ASPxGridView>




        <div style="display: none;">
            <dx:ASPxGridViewExporter runat="server" ID="grdExport" GridViewID="grdForExport" FileName="Billing Summary Report"></dx:ASPxGridViewExporter>
            <dx:ASPxGridView ID="grdForExport" runat="server" AutoGenerateColumns="false" ClientInstanceName="grid" KeyFieldName="GroupNumber;BillingPeriod" Theme="Office2010Silver" OnCustomUnboundColumnData="grdForExport_CustomUnboundColumnData">
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="Number" VisibleIndex="0" Width="40px" Caption="Sr. No." UnboundType="String" ReadOnly="True" ShowInCustomizationForm="True">
                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                        <CellStyle HorizontalAlign="Center"></CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Project Group" FieldName="GroupNumber">
                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                        <CellStyle HorizontalAlign="Center"></CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Billing Period" FieldName="BillingPeriod" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" Width="150px">
                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                        <CellStyle HorizontalAlign="Center"></CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Order Count" FieldName="InvoiceOrderCount" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                        <CellStyle HorizontalAlign="Center"></CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Total Amount" FieldName="InvoiceTotal" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
                        <CellStyle HorizontalAlign="Center"></CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Invoice Generated Date" FieldName="AddedDate" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" Width="150px">
                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Wrap="True"></HeaderStyle>
                        <CellStyle HorizontalAlign="Center"></CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Is Invoice Sent To Client?" FieldName="InvoiceSent" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" Width="150px">
                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Wrap="True"></HeaderStyle>
                        <CellStyle HorizontalAlign="Center"></CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Invoice Delivered Date" FieldName="DeliveredDate" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" Width="150px">
                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Wrap="True"></HeaderStyle>
                        <CellStyle HorizontalAlign="Center"></CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Invoice Read Date" FieldName="ReadDate" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" Width="150px">
                        <HeaderStyle HorizontalAlign="Center" Font-Bold="True" Wrap="True"></HeaderStyle>
                        <CellStyle HorizontalAlign="Center"></CellStyle>
                    </dx:GridViewDataTextColumn>
                </Columns>
            </dx:ASPxGridView>
        </div>
    </div>
</asp:Content>
