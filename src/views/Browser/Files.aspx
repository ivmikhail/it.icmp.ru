<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="ViewPage<BrowseItem>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
	Файлы - <%= Model.Name %>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">
        
    <h1>
        <% foreach (var parent in Model.Parents) { %>	        
		    <% Html.RenderPartial("Link/Browser/Dir", parent); %> /
		<% } %>
        <%= Model.Name %>
    </h1>

    <table class="browse-table" cellspacing="0">
        <% if (Model.Parent != null) { %>
            <tr>
                <td class="light-block file-icon">
                    <img src="<%= Url.Content("~/content/img/browser/up.ico") %>" alt="up.ico" class="middle" />
                </td>
                <td class="light-block" colspan="5">
                    <% Html.RenderPartial("Link/Browser/Parent", Model.Parent); %>
                </td>
            </tr>
        <% } %>

        <% foreach (var child in Model.Children) { %>
            <tr>
                <td class="light-block file-icon">
                    <img src="<%= Url.Content("~/content/img/browser/" + Html.Icon(child.Extension)) %>" alt="<%= Html.Icon(child.Extension) %>" class="middle" />
                </td>
                <%if (child.IsDir) { %>
                    <td class="light-block">
                        <% Html.RenderPartial("Link/Browser/Dir", child); %>
                    </td>
                    <td class="meta light-block file-date">
                        <%= Html.Date(child.ModifiedDate) %>
                    </td>
                    <td class="meta light-block file-desc">
                        <%= child.Description %>
                    </td>
                    <td class="meta light-block file-edit-desc">
                        <% Html.RenderPartial("Link/Browser/EditDesc", child); %>
                    </td>
                    <td class="meta light-block file-size">
                    </td>
                <% } else {%>
                    <td class="light-block">
                        <% Html.RenderPartial("Link/Browser/File", child); %>
                    </td>
                    <td class="meta light-block file-date">
                        <%= Html.Date(child.ModifiedDate) %>
                    </td>
                    <td class="meta light-block file-desc">
                        <%= child.Description %>
                    </td>
                    <td class="meta light-block file-edit-desc">
                        <% Html.RenderPartial("Link/Browser/EditDesc", child); %>
                    </td>
                    <td class="meta light-block file-size">
                        <%= Html.FileSize(child.Size) %>
                    </td>
                <% } %>
            </tr>
        <% } %>
    </table>
</asp:Content>
