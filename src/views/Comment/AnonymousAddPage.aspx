﻿<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="ViewPage<AnonymousCommentAddModel>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Добавление комментария
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    
    <h1>Добавление комментария</h1>
    
    <% Html.RenderPartial("AnonymousAdd", Model); %>

</asp:Content>
