<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="ViewPage<Rss>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    IT Community - <%= Model.Title %>
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <h1>
        <%= Model.Feed.Title.Text %>
        <ul class="right-list meta">
            <li>
                <% Html.RenderPartial("Link/Rss/Feed", Model); %>
            </li>
            <li>
                <% Html.RenderPartial("Link/Rss/Site", Model); %>
            </li>
        </ul>
    </h1>

    <% Html.RenderPartial("List", Model.Feed.Items.ToList()); %>

</asp:Content>
