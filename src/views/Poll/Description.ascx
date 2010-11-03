<%@ Control Language="C#" Inherits="ViewUserControl<Post>" %>


<h2>
    <% Html.RenderPartial("Link/Post/Title", Model); %>
</h2>

<div class="text">
    <% Html.RenderPartial("../Poll/Answers", Model.Entity); %>
</div>

<div class="meta">
    <ul class="left-list">
        <li>
            <% Html.RenderPartial("../Poll/EndDate", Model.Entity); %>
        </li>
    </ul>
    <ul class="right-list">
        <li>
            <% Html.RenderPartial("../Poll/IsOpen", Model.Entity); %>
        </li>
        <li>
            <% Html.RenderPartial("../Poll/VotedUsersCount", Model.Entity); %>
        </li>
    </ul>

    <div class="clear"></div>

    <% Html.RenderPartial("../Post/Meta", Model); %>
    
</div>
