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

    <ul>
        <% if (Model.Parent != null) { %>
            <li class="light-block">
                <img src="<%= Url.Content("~/content/img/browser/up.ico") %>" alt="up.ico" class="middle" />
	            <% Html.RenderPartial("Link/Browser/Parent", Model.Parent); %>	
            </li>
        <% } %>

        <% foreach (var child in Model.Children) { %>
            <li class="light-block">
                <img src="<%= Url.Content("~/content/img/browser/" + Html.Icon(child.Extension)) %>" alt="<%= Html.Icon(child.Extension) %>" class="middle" />
                <%if (child.IsDir) { %>
                    <% Html.RenderPartial("Link/Browser/Dir", child); %>	
                <% } else {%>
                    <% Html.RenderPartial("Link/Browser/File", child); %>	
                    <span class="meta">
                        <%= Html.FileSize(child.Size) %>
                    </span>
                <% } %>
            </li>
        <% } %>
    </ul>
</asp:Content>
