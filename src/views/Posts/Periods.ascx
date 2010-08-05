<%@ Control Language="C#" Inherits="ViewUserControl<PostsModel>" %>
<%-- ооочень долго геморроился здесь, не смог читабильнее написать --%>


<%
    var action = (Model.Sorting == PostsModel.SortBy.Views) ? "popular" : "discussible";
%>

<div>
    <ul class="left">
        <li>
            <% if (Model.PeriodDays == PostsModel.Period.Day) { %>
                день
            <% } else { %>
                <%= Html.ActionLink("день", action, "posts", new { period = PostsModel.Period.Day }, null)%>
            <% } %>
        </li>
        <li>
            <% if (Model.PeriodDays == PostsModel.Period.Week) { %>
                неделю
            <% } else { %>
                <%= Html.ActionLink("неделю", action, "posts", new { period = PostsModel.Period.Week }, null)%>
            <% } %>
        </li>
        <li>
            <% if (Model.PeriodDays == PostsModel.Period.Month) { %>
                месяц
            <% } else { %>
                <%= Html.ActionLink("месяц", action, "posts", new { period = PostsModel.Period.Month }, null)%>
            <% } %>
        </li>
        <li>
            <% if (Model.PeriodDays == PostsModel.Period.Year) { %>
                год
            <% } else { %>
                <%= Html.ActionLink("год", action, "posts", new { period = PostsModel.Period.Year }, null)%>
            <% } %>
        </li>
        <li>
            <% if (Model.PeriodDays == PostsModel.Period.All) { %>
                все время
            <% } else { %>
                <%= Html.ActionLink("все время", action, "posts", new { period = PostsModel.Period.All }, null)%>
            <% } %>
        </li>
    </ul>
    <div class="clear"></div>
</div>
