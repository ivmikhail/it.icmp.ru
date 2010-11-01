<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="ViewPage<Rss>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    IT Community - <%= Model.Title %>
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <h1>
        <%= Model.Title %>
        <ul class="right-list meta">
            <li>
                <% Html.RenderPartial("Link/Rss/Feed", Model); %>
            </li>
            <li>
                <% Html.RenderPartial("Link/Rss/Site", Model); %>
            </li>
        </ul>
    </h1>

    <div class="meta">
        <%= Model.Description %>
    </div>

    <% if (Model.Feed == null) { %>
        <h2>Упс, ничего нет</h2>
        <div class="info">
            возможно проблема в url-е rss или в не правильном <a href="http://validator.w3.org/feed/check.cgi?url=<%= Url.Encode(Model.Uri) %>" title="Проверить">формате</a> rss
        </div>
    <% } else { %>
        <ul>
            <% foreach (var item in Model.Feed.Items) { %>
            <li class="block">
                <% Html.RenderPartial("../Rss/Description", item); %>
            </li>
            <% } %>
        </ul>
    <% } %>

</asp:Content>
