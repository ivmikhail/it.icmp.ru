<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="ViewPage<MessageListModel>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    <% if (Model.TotalCount == 0) { %>
        Нет отправленных сообщений
    <% } else if (Model.TotalCount == 1) { %>
        Одно отправленное сообщение
    <% } else { %>
        <%= Model.TotalCount %> отправленных сообщений
    <% } %>
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <h1>
        <% if (Model.TotalCount == 0) { %>
            Нет отправленных сообщений
        <% } else if (Model.TotalCount == 1) { %>
            Одно отправленное сообщение
        <% } else { %>
            <%= Model.TotalCount %> отправленных сообщений
        <% } %>
        <ul class="right-list meta">
            <li>
                <% Html.RenderPartial("Link/Message/DeleteAll", "sents"); %>
            </li>
        </ul>
    </h1>

    <% Html.RenderPartial("../Message/List", Model); %>

</asp:Content>
