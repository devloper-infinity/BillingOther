<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExcelViewer.aspx.cs" Inherits="BillingOther.Accounts.ExcelViewer" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Data.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Data" TagPrefix="dx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <dx:ASPxGridViewExporter ID="gridExport" FileName="Details" runat="server" GridViewID="grdTest"></dx:ASPxGridViewExporter>
         <dx:ASPxGridView ID="grdTest"  runat="server" AutoGenerateColumns="true" Styles-Header-BackColor="Brown"></dx:ASPxGridView>
    </div>
    </form>
</body>
</html>
