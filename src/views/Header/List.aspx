<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="ViewPage<HeaderListModel>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Хидер тексты
</asp:Content>

<asp:Content ID="Menu" ContentPlaceHolderID="MenuContent" runat="server">
    <% Html.RenderPartial("../Admin/Menu"); %>
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <h1>
        Хидер тексты
        <ul class="right-list meta">
            <li>
                <% Html.RenderPartial("Link/Header/Add"); %>
            </li>
        </ul>
    </h1>

    <ul>
        <% foreach (var header in Model.List) { %>
            <li class="block">
                <h2 class="<% if (header.EndDate < DateTime.Now) { %>none-active<% } %>"><%= header.Text %></h2>
                <ul class="left-list meta">
                    <li>
                        <% Html.RenderPartial("Link/User/Profile", header.User); %>
                    </li>
                    <li>
                        добавил <span class="info"><%= header.CreateDate.ToString("dd MMMM yyyy, HH:mm") %></span>
                    </li>
                    <li>
                        <% if (header.EndDate > DateTime.Now) { %>
                            показывается
                        <% } else { %>
                            показывался
                        <% } %>
                        до <span class="info"><%= header.EndDate.Value.ToString("dd MMMM yyyy, HH:mm") %></span>
                    </li>
                </ul>
                <ul class="right-list meta">
                    <% if (header.EndDate > DateTime.Now) { %>
                        <li>
                            <% Html.RenderPartial("Link/Header/Stop", header); %>
                        </li>
                    <% } else { %>
                        <li>
                            <% Html.RenderPartial("Link/Header/Show", header); %>
                        </li>
                    <% } %>
                    <li>
                        <% Html.RenderPartial("Link/Header/Delete", header); %>
                    </li>
                </ul>
                <div class="clear"></div>
            </li>
        <% } %>
    </ul>

    <% Html.RenderPartial("Pagination", Model); %>

</asp:Content>
