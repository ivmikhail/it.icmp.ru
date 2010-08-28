<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="ViewPage<PostEditPollModel>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Добавление опроса
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Добавление опроса</h1>
 
    <% using (Html.BeginForm()) { %>
       
        <% Html.RenderPartial("../Poll/EditForm", Model); %>
    
        <input type="submit" value="добавить опрос" />

    <% } %>
    
</asp:Content>
