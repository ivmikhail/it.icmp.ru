<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="ViewPage<CategoryEditModel>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Категории постов
</asp:Content>

<asp:Content ID="Menu" ContentPlaceHolderID="MenuContent" runat="server">
    <% Html.RenderPartial("../Admin/Menu"); %>
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <div class="block">
        <h1>Добавление или редактирование категории</h1>

        <% using (Html.BeginForm()) { %>

            <%= Html.LabelFor(m => m.Name) %>
            <%= Html.TextBoxFor(m => m.Name)%>
            <%= Html.ValidationMessageFor(m => m.Name)%>

            <%= Html.LabelFor(m => m.Sort) %>
            <%= Html.TextBoxFor(m => m.Sort)%>
            <%= Html.ValidationMessageFor(m => m.Sort)%>

            <input type="submit" value="сохранить" />

        <% } %>
    </div>

    <h1>Категории постов</h1>

    <ul>
        <% foreach (var category in Categories.All) { %>
            <li class="light-block">
                <ul class="left-list">
                    <li>
                        <% Html.RenderPartial("Link/Category/Posts", category); %>
                    </li>
                    <li class="meta">
                        порядок сортировки: <span class="info"><%= category.Sort %></span>
                    </li>
                </ul>
                <ul class="right-list meta">
                    <li>
                        <% Html.RenderPartial("Link/Category/Edit", category); %>
                    </li>
                    <li>
                        <% Html.RenderPartial("Link/Category/Delete", category); %>
                    </li>
                </ul>
                <div class="clear"></div>
            </li>
        <% } %>
    </ul>

</asp:Content>
