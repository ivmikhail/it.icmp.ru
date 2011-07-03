<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<noscript>
    <div>
        Внимание! У вашего браузера отключен javascript, корректная работа сайта не гарантируется.
        <br />
        <a href="http://adsense.google.com/support/bin/answer.py?hl=ru&amp;answer=12654">Узнать как включить &rarr;</a>
    </div>
</noscript>

<div class="top-menu">

    <ul class="left-list">
        <li>
            <% Html.RenderPartial("Link/Post/Rss"); %>               
        </li>
        <li>
            <% Html.RenderPartial("Link/Post/List"); %>
        </li>
<%--
        <li>
            <% Html.RenderPartial("Link/Rss/Load"); %>
        </li>
--%>
        <li class="menu">
            <% Html.RenderPartial("../MenuItem/Menu"); %>
        </li>

    </ul>

    <ul class="right-list">
        <% if (CurrentUser.IsAuth) { %>
            <li>
                <%= Greeting.Get() %>, <% Html.RenderPartial("Link/User/CurrentProfile"); %>
            </li>
<%--
            <li>
                <% Html.RenderPartial("Link/User/Settings"); %>
            </li>
--%>
            <% if (CurrentUser.IsAuth) { %>
                <li>
                    <% Html.RenderPartial("Link/Message/CurrentReceivedList"); %>
                    <% if (CurrentUser.User.UnreadMessagesCount > 0) { %>
                    <span class="blink important-link">(<%= CurrentUser.User.UnreadMessagesCount %>) </span>
                    <% } %>
                </li>
            <% } %>
            <% if (CurrentUser.IsAdmin) { %>
                <li>
                    <% Html.RenderPartial("Link/Admin/Index"); %>
                </li>
            <% } %>
            <li>
                <% Html.RenderPartial("Link/User/Logout"); %>
            </li>
        <% } else { %>
            <li>
                <% Html.RenderPartial("Link/User/Login"); %>
            </li>
        <% } %>
        
    </ul>

</div>

<div class="header-text">
	<div class="header-logo">
		<a href="/" title="IT Community"><img alt="IT-Community" src="/content/img/design/itc-logo.png" /></a>
	</div>
	<span class="header-greeting"><%= Headers.GetRandom().Text %></span>
</div>
