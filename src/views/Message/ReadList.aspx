<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="ViewPage<MessageListModel>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    <% if (Model.TotalCount == 0) { %>
        Нет прочитанных сообщений
    <% } else if (Model.TotalCount == 1) { %>
        Одно прочитанное сообщение
    <% } else { %>
        <%= Model.TotalCount %> прочитанных сообщений
    <% } %>
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <h1>
        <% if (Model.TotalCount == 0) { %>
            Нет прочитанных сообщений
        <% } else if (Model.TotalCount == 1) { %>
            Одно прочитанное сообщение
        <% } else { %>
            <%= Model.TotalCount %> прочитанных сообщений
        <% } %>
        <ul class="right-list meta">
            <li>
                <% Html.RenderPartial("Link/Message/DeleteAll", "reads"); %>
            </li>
        </ul>
    </h1>

    <% Html.RenderPartial("../Message/List", Model); %>

</asp:Content>
