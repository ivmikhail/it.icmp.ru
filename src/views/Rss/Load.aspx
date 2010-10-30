<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="ViewPage<List<SyndicationItem>>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    IT Community - RSS других ресурсов
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <h1>RSS других ресурсов</h1>

    <% Html.RenderPartial("List", Model); %>

</asp:Content>
