<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<noscript>
    Внимание! У вашего браузера отключен javascript, корректная работа сайта не гарантируется.
    <a href="http://adsense.google.com/support/bin/answer.py?hl=ru&amp;answer=12654" target="_blank">Узнать как включить >>></a>
</noscript>


<div class="down-arrow">
    <a href="#footer" title="Посмотреть что внизу">&darr;</a>
</div>


<div class="top-menu">
    <ul class="right">
        <% if (CurrentUser.isAuth) { %>
            <li>
                <%= Greeting.Get() %>, <% Html.RenderPartial("Link/User/CurrentProfile"); %></a>!
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
    <a href="/" title="Здесь мог бы быть твой текст">
        <span class="it">IT</span><span class="community">.Community;</span>
        <span class="text">// <%= Html.Encode(Headers.GetRandom().Text) %></span>
    </a>
</div>
