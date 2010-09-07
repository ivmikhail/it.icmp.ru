<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="ViewPage<PostEditModel>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Добавление поста
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Добавление поста</h1>
 
    <% using (Html.BeginForm()) { %>           
       
        <% Html.RenderPartial("EditForm", Model); %>
    
        <input type="submit" value="добавить" />

    <% } %>
    <%--
    <div id="PictureUpload">
        <% Html.RenderPartial("../Picture/Upload"); %>
    </div>
    --%>
</asp:Content>
