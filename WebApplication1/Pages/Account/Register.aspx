<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="kuroneko.Pages.Account.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3><b>Register:</b></h3>

    <asp:Label ID="lblUsername" runat="server" Text="UserName:"></asp:Label>
    <br />
    <br />
    <asp:TextBox ID="txtUserName" runat="server" Width="300px" CssClass="inputs"></asp:TextBox>
    <br />
    <br />
    <asp:Label ID="lblPassword" runat="server" Text="Password:"></asp:Label>
    <br />
    <br />
    <asp:TextBox ID="txtPassword" runat="server" Width="300px" CssClass="inputs" TextMode="Password"></asp:TextBox>
    <br />
    <br />
    <asp:Label ID="lblConfirmPassword" runat="server" Text="ConfirmPassword:"></asp:Label>
    <br />
    <br />
    <asp:TextBox ID="txtConfimPassword" runat="server" Width="300px" CssClass="inputs" TextMode="Password"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="button" OnClick="Button1_Click" />
    <br />
    <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
</asp:Content>
