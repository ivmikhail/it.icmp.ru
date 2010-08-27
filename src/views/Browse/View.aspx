<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="ViewPage<BrowseModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Browse
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
           
        <%if(Model.RootDir!=null){ %>
	        <% Html.RenderPartial("Link/Browse/RootDir", Model); %>	
        <%} %>	                
        <% foreach (var path in Model.Pathes) { %>	        
		/<% Html.RenderPartial("Link/Browse/Pathes", path); %>		
		<% } %>

        <ul>
        <% foreach (var file in Model.Files) { %>
            <li>
                <img src="<%=Url.Content("~/content/img/browser/"+file.Icon)%>" alt='<%=file.Name%>' />
                <%if (file.IsDir){ %>
                    <% Html.RenderPartial("Link/Browse/Dir", file); %>	
                <%} else{%>                
                    <% Html.RenderPartial("Link/Browse/File", file); %>	
                <%} %>
                <%=file.Size %>
            </li>
        <% } %>
        </ul>

</asp:Content>
