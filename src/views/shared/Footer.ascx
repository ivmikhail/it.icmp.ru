<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<div class="bottom-menu">

    <% foreach (var item in MenuItems.GetRoot()) { %>
        <dl>
            <dt><%= item.Name %></dt>

            <% foreach (var child in item.Children) { %>
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
        <li>
            <% Html.RenderPartial("Counter/Yaknet"); %>
        </li>
    </ul>

    <div class="copy">
        <b>IT Community &copy; 2005 - <%= DateTime.Now.Year %></b>
        <p>Данный ресурс поддерживается сообществом АЙТИ коммунистов г.Якутска.</p>
        <p>Проект некоммерческий.</p>
    </div>

    <div class="clear"></div>
</div>

