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
        <% if (CurrentUser.IsAuth) { %>
            <li>
                <% Html.RenderPartial("Link/Message/CurrentReceivedList"); %>
                <% if (CurrentUser.User.UnreadMessagesCount > 0) { %>
                    <span class="blink important-link">
                        (<%= CurrentUser.User.UnreadMessagesCount %>)
                    </span>
                <% } %>
            </li>
            <li>
                <% Html.RenderPartial("Link/Browser/Files"); %>
            </li>
<%--
            <li>
                <% Html.RenderPartial("Link/User/Settings"); %>
            </li>
--%>
            <% if (CurrentUser.IsAdmin) { %>
                <li>
                   <% Html.RenderPartial("Link/Admin/Index"); %>
                </li>
            <% } %>
        <% } %>
    </ul>

    <ul class="right-list">
        <% if (CurrentUser.IsAuth) { %>
            <li>
                <%= Greeting.Get() %>, <% Html.RenderPartial("Link/User/CurrentProfile"); %>
            </li>
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
    <a href="/" title="IT Community">
        <span class="it">IT</span>.<span class="community">Community</span>&lt;<span class="in">in</span> <span class="beta">Beta</span>&gt;;
        <span class="text">// <%= Headers.GetRandom().Text %></span>
    </a>
</div>
