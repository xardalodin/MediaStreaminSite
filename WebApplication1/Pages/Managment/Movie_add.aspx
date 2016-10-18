<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Movie_add.aspx.cs" Inherits="kuroneko.Pages.Managment.Movie_add" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Add new Media</h3>

    <table>
            <tr>
                <td style="width: 80px; font-weight: bold;">Filename:</td>
                <td >
                    <asp:DropDownList ID="ddlMovies" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMovies_SelectedIndexChanged"></asp:DropDownList>                              
                     <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                </td>                     
            </tr>
                  
         
    </table>
  
    <video width="720" height="480" controls="controls" preload="none" autoplay="autoplay">
        <source id="videoTag" runat="server" type="video/mp4"/>
        <p> video is not supported </p>
    </video>

</asp:Content>
