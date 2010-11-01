<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<ul class="left-list">
    <% foreach (var item in BrowseItem.Root) { %>
        <li>
            <% Html.RenderPartial("Link/Browser/Dir", item); %>	            
        </li>
    <% } %>    
</ul>