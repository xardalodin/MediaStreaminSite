<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="kuroneko.Pages.Account.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <h3><b>Login:</b></h3>
     <br />
    <asp:Label ID="lblusername" runat="server" Text="UserName:"></asp:Label>
    <br />
    <br />
    <asp:TextBox ID="txtUserName" runat="server" Width="300px" CssClass="inputs"></asp:TextBox>
    <br />
    <br />
    <asp:Label ID="lblpassword" runat="server" Text="Password:"></asp:Label>
    <br />
    <br />
    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="300px" CssClass="inputs"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="btnlogin" runat="server" Text="Login" OnClick="btnlogin_Click" />
    <br />
    <br />
    <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
    <br />
</asp:Content>
