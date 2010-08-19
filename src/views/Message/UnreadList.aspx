<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="ViewPage<MessageListModel>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    <% if (Model.TotalCount == 0) { %>
        Нет новых сообщений
    <% } else if (Model.TotalCount == 1) { %>
        Одно новое сообщение
    <% } else { %>
        <%= Model.TotalCount %> новых сообщений
    <% } %>
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <h1>
        <% if (Model.TotalCount == 0) { %>
            Нет новых сообщений
        <% } else if (Model.TotalCount == 1) { %>
            Одно новое сообщение
        <% } else { %>
            <%= Model.TotalCount %> новых сообщений
        <% } %>
        <ul class="right-list meta">
            <li>
                <% Html.RenderPartial("Link/Message/ReadAll"); %>
            </li>
            <li>
                <% Html.RenderPartial("Link/Message/DeleteAll", "unreads"); %>
            </li>
        </ul>
    </h1>

    <% Html.RenderPartial("../Message/List", Model); %>

</asp:Content>
