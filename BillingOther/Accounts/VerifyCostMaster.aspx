<%@ Page Title="" Language="C#" MasterPageFile="~/Accounts/Accounts.Master" AutoEventWireup="true" CodeBehind="VerifyCostMaster.aspx.cs" Inherits="BillingOther.Accounts.VerifyCostMaster" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Data.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Data" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>  function HideLabel() {

            var seconds = 5;
            setTimeout(function () {
                document.getElementById("<%# dvError.ClientID %>").style.display = "none";
                document.getElementById("<%# dvError.ClientID %>").innerHTML = "";
            }, seconds * 1000);
        };</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvError" runat="server"></div>
      <div class="card-content">
        <div class="card-body">
            <ul class="nav nav-tabs " role="tablist">
                <li class="nav-item active" id="tbpnl1" runat="server">
                    <a class="nav-link active" data-toggle="tab" href="#tab1" role="tab"><i class="fa fa-rupee"></i>&nbsp;&nbsp;New Price</a>
                    <div class="slide"></div>
                </li>
                <li class="nav-item" id="tbpnl2" runat="server">
                    <a class="nav-link" data-toggle="tab" href="#tab2" role="tab"><i class="fa fa-rupee"></i>&nbsp;&nbsp;Modified Price</a>
                    <div class="slide"></div>
                </li>
            </ul>
            <div class="tab-content card-block" style="border: solid 1px #6c757d; margin-left: 5px;">
                <div class="tab-pane active" id="tab1" role="tabpanel">
                    <dx:ASPxPopupControl ID="popupEditOrder" HeaderText="Send To Accounts" runat="server" ClientInstanceName="clientpopupEditOrder" ScrollBars="Vertical" CloseAction="CloseButton" Height="600px" Modal="True" Width="1000px"
                        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
                        <ContentCollection>
                            <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:ASPxPopupControl>
                    <div id="costgriddiv" runat="server" style="overflow: auto;">
                        <asp:Label ID="lblRecords" runat="server" CssClass="form-control-default"></asp:Label>
                        <dx:ASPxGridView ID="ASPxReoprt" runat="server" Width="100%" ClientInstanceName="Grid" AutoGenerateColumns="false" KeyFieldName="ProjectId" Theme="Default" OnCustomButtonCallback="ASPxReoprt_CustomButtonCallback" OnCustomUnboundColumnData="ASPxReoprt_CustomUnboundColumnData">
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="Number" VisibleIndex="0" Caption="Sr.#" Width="20px" UnboundType="String" ReadOnly="true" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="ProjectId" Visible="false"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="ProjectName" VisibleIndex="1" Caption="Project #" ReadOnly="true" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="DomainName" VisibleIndex="2" Caption="Domain" Width="35px" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="ProjectStartDate" VisibleIndex="3" Caption="Start Date" Width="35px" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="BillingCycle" VisibleIndex="4" Caption="Billing Cycle" Width="35px" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="BDM" VisibleIndex="5" Caption="BDM" ReadOnly="true" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="CRM" VisibleIndex="6" Caption="CRM" ReadOnly="true" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="UH" VisibleIndex="7" Caption="Unit Head" ReadOnly="true" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="AddedBy" VisibleIndex="8" Caption="Added By" ReadOnly="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="AddedDate" VisibleIndex="9" Caption="Added Date" ReadOnly="true" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
                                <dx:GridViewCommandColumn Caption="View" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" CellStyle-HorizontalAlign="Center" ButtonType="link">
                                    <CustomButtons>
                                        <dx:GridViewCommandColumnCustomButton Text="View" ID="CostDetails"></dx:GridViewCommandColumnCustomButton>
                                    </CustomButtons>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="DomainId" VisibleIndex="9" Caption="DomainId" Visible="false" ReadOnly="true" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>

                            </Columns>
                        </dx:ASPxGridView>
                    </div>
                </div>
                <div class="tab-pane" id="tab2" role="tabpanel">
                    <div id="Div1" runat="server" style="overflow: auto;">
                        <asp:Label ID="Label1" runat="server"></asp:Label>
                        <dx:ASPxGridView ID="ASPxGridView1" runat="server" Width="100%" ClientInstanceName="Grid" AutoGenerateColumns="false" KeyFieldName="ProjectId" Theme="Office2010Silver" OnCustomButtonCallback="ASPxGridView1_CustomButtonCallback" OnCustomUnboundColumnData="ASPxGridView1_CustomUnboundColumnData">
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="Number" VisibleIndex="0" Caption="Sr.#" Width="20px" UnboundType="String" ReadOnly="true" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="ProjectId" Visible="false"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="ProjectName" VisibleIndex="1" Caption="Project #" ReadOnly="true" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="DomainName" VisibleIndex="2" Caption="Domain" Width="35px" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="ProjectStartDate" VisibleIndex="3" Caption="Start Date" Width="35px" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="BillingCycle" VisibleIndex="4" Caption="Billing Cycle" Width="35px" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="BDM" VisibleIndex="5" Caption="BDM" ReadOnly="true" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="CRM" VisibleIndex="6" Caption="CRM" ReadOnly="true" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="UH" VisibleIndex="7" Caption="Unit Head" ReadOnly="true" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="AddedBy" VisibleIndex="8" Caption="Modified By" ReadOnly="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="AddedDate" VisibleIndex="9" Caption="Modified Date" ReadOnly="true" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>
                                <dx:GridViewCommandColumn Caption="View" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" CellStyle-HorizontalAlign="Center" ButtonType="link">
                                    <CustomButtons>
                                        <dx:GridViewCommandColumnCustomButton Text="View" ID="ViewModifiedRates"></dx:GridViewCommandColumnCustomButton>
                                    </CustomButtons>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="DomainId" VisibleIndex="9" Caption="DomainId" Visible="false" ReadOnly="true" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"></dx:GridViewDataTextColumn>

                            </Columns>
                        </dx:ASPxGridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
