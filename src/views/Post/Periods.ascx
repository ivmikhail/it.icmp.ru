<%@ Control Language="C#" Inherits="ViewUserControl<PostListModel>" %>
<%-- ооочень долго геморроился здесь, не смог читабильнее написать --%>


<%
    var action = (Model.Sorting == PostListModel.SortBy.Views) ? "populars" : "discussibles";
%>

<ul class="right-list meta">
    <li>
        <% if (Model.PeriodDays == PostListModel.Periods.Day) { %>
            день
        <% } else { %>
            <%= Html.ActionLink("день", action, "post", new { period = "day" }, null) %>
        <% } %>
    </li>
    <li>
        <% if (Model.PeriodDays == PostListModel.Periods.Week) { %>
            неделю
        <% } else { %>
            <%= Html.ActionLink("неделю", action, "post", new { period = "week" }, null) %>
        <% } %>
    </li>
    <li>
        <% if (Model.PeriodDays == PostListModel.Periods.Month) { %>
            месяц
        <% } else { %>
            <%= Html.ActionLink("месяц", action, "post", new { period = "month" }, null) %>
        <% } %>
    </li>
    <li>
        <% if (Model.PeriodDays == PostListModel.Periods.Year) { %>
            год
        <% } else { %>
            <%= Html.ActionLink("год", action, "post", new { period = "year" }, null)%>
        <% } %>
    </li>
    <li>
        <% if (Model.PeriodDays == PostListModel.Periods.All) { %>
            все время
        <% } else { %>
            <%= Html.ActionLink("все время", action, "post", new { period = "all" }, null) %>
        <% } %>
    </li>
</ul>
