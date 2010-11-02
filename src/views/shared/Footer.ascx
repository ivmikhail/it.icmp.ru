<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<div class="bottom-menu">

    <% Html.RenderPartial("../MenuItem/Menu"); %>

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

