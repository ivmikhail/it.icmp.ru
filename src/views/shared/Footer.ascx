<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<a href="#header" class="up-arrow" title="Посмотреть что наверху">&uarr;</a>

<div class="bottom-menu">

    <% foreach (var item in MenuItems.GetRoot()) { %>
        <dl>
            <dt><%= item.Name %></dt>

            <% foreach (var child in item.Childs) { %>
                <dd>
                    <% Html.RenderPartial("Link/MenuItem/Url", child); %>
                </dd>
            <% } %>
        </dl>
    <% } %>

    <div class="clear"></div>
</div>

<div class="footer-bottom">

    <ul class="counters">
        <li></li>
    </ul>

    <div class="copy">
        <b>IT Community &copy; 2005 - <%= DateTime.Now.Year %></b>
        <p>Данный ресурс поддерживается сообществом АЙТИ коммунистов г.Якутска.</p>
        <p>Проект некоммерческий.</p>
    </div>

    <div class="clear"></div>
</div>

