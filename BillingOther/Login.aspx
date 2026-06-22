<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="BillingOther.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" class="body-full-height">
<head runat="server">
    <title>Infinity IPS - Billing</title>
    <link rel="stylesheet" type="text/css" id="theme" href="css/theme-blue.css" />
</head>
<body>
    <div class="login-container lightmode">
        <div class="login-box animated fadeInDown">
            <div class="login-logo">
                
            </div>
            <div class="login-body">
                <div class="login-title"><strong>Sign in</strong> to start your session</div>
                <form id="form1" runat="server" class="form-horizontal" method="post">
                    <div id="dvError" runat="server"></div>
                    <div class="form-group">
                        <div class="col-md-12">
                            <asp:TextBox ID="txtUserName" runat="server" placeholder="Username" Style="text-transform: uppercase;" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv_txtUserName" runat="server" ControlToValidate="txtUserName" ErrorMessage="Please enter username" Display="Dynamic" Style="color: red; font-size: 12px;"></asp:RequiredFieldValidator>

                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-12">
                            <asp:TextBox ID="txtPassword" runat="server" placeholder="Password" TextMode="Password" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv_txtPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Please enter password" Display="Dynamic" Style="color: red; font-size: 12px;"></asp:RequiredFieldValidator>

                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-6">
                            <asp:CheckBox ID="chkRemember" runat="server"></asp:CheckBox> &nbsp; Remember me
                        </div>
                        <div class="col-md-6">
                            <asp:Button ID="btnLogin" runat="server" Text="Sign In" CssClass="btn btn-info btn-block" OnClick="btnSubmit_Click" />
                        </div>
                    </div>
                   


                </form>
            </div>
            <div class="login-footer">
                <div class="pull-left">
                    Infinity IPS &copy; 2022 
                   
                </div>

            </div>
        </div>

    </div>

</body>
</html>
