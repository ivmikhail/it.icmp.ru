<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<ul class="left-list">
    <% foreach (var file in BrowseItem.GetList(BrowseItem.GetRealPathOfLink(""), true))
       { %>    
        <li>
            <% Html.RenderPartial("Link/Browse/Dir", file); %>	            
        </li>
    <% } %>    
</ul>