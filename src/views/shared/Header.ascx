<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>


<noscript>
    Внимание! У вашего браузера отключен javascript, корректная работа сайта не гарантируется.
    <a href="http://adsense.google.com/support/bin/answer.py?hl=ru&amp;answer=12654" target="_blank">Узнать как включить >>></a>
</noscript>


<div class="down-arrow">
    <a href="#footer" title="Посмотреть что внизу">&darr;</a>
</div>


<div class="top-menu">
<%--
    <ul class="left">
        <li><a href="/posts" title="Посмотреть только посты">посты</a></li>
        <li><a href="/files" title="Посмотреть только файлы">файлы</a></li>
    </ul>
--%>
    <ul class="right">
        <% if (CurrentUser.isAuth) { %>
            <li>
                <%= Greeting.Get() %>, <a href="/user/view/<%= CurrentUser.User.Nick %>" title="Мое"><%= CurrentUser.User.Nick %></a>!
            </li>
            <li>
                <a href="/user/logout" title="Зачем выходить?">выйти</a>
            </li>
        <% } else { %>
            <li>
                <a href="/user/login" title="Приветствуем!">войти</a>
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
