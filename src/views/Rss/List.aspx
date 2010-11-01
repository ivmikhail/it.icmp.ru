<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="ViewPage<RssEditModel>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Загружаемые rss-ки
</asp:Content>

<asp:Content ID="Menu" ContentPlaceHolderID="MenuContent" runat="server">
    <% Html.RenderPartial("../Admin/Menu"); %>
</asp:Content>

<asp:Content ID="Sidebar" ContentPlaceHolderID="SidebarContent" runat="server">
    <% Html.RenderPartial("../Admin/Sidebar"); %>
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <div class="block">
        <h1>Добавление или редактирование rss-ки</h1>

        <% using (Html.BeginForm()) { %>
            <%= Html.LabelFor(m => m.Title) %>
            <%= Html.TextBoxFor(m => m.Title)%>
            <%= Html.ValidationMessageFor(m => m.Title)%>

            <%= Html.LabelFor(m => m.Uri) %>
            <%= Html.TextBoxFor(m => m.Uri)%>
            <%= Html.ValidationMessageFor(m => m.Uri)%>

            <%= Html.LabelFor(m => m.Sort) %>
            <%= Html.TextBoxFor(m => m.Sort)%>
            <%= Html.ValidationMessageFor(m => m.Sort)%>

            <input type="submit" value="сохранить" />

        <% } %>
    </div>

    <h1>Загружаемые rss-ки</h1>

    <ul>
        <% foreach (var rss in Rsses.All) { %>
            <li class="light-block">
                <ul class="left-list">
                    <li>
                        <% Html.RenderPartial("Link/Rss/LoadFeed", rss); %>
                    </li>
                    <li class="meta">
                        порядок сортировки: <span class="info"><%= rss.Sort%></span>
                    </li>
                </ul>
                <ul class="right-list meta">
                    <li>
                        <% Html.RenderPartial("Link/Rss/Edit", rss); %>
                    </li>
                    <li>
                        <% Html.RenderPartial("Link/Rss/Delete", rss); %>
                    </li>
                </ul>
                <div class="clear"></div>
            </li>
        <% } %>
    </ul>

</asp:Content>
