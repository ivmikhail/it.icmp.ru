﻿<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="ViewPage<MessageListModel>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    <% if (Model.TotalCount == 0) { %>
        Нет сообщений
    <% } else if (Model.TotalCount == 1) { %>
        Одно сообщение
    <% } else { %>
        Сообщений: <%= Model.TotalCount %> 
    <% } %>
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <h1>
        <% if (Model.TotalCount == 0) { %>
            Нет сообщений
        <% } else if (Model.TotalCount == 1) { %>
            Одно сообщение
        <% } else { %>
            Сообщений: <%= Model.TotalCount %> 
            <ul class="right-list meta">
                <li>
                    <% Html.RenderPartial("Link/Message/DeleteAll", "received"); %>
                </li>
            </ul>
        <% } %>
    </h1>

    <% Html.RenderPartial("../Message/List", Model); %>

</asp:Content>
