<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>


<div class="up-arrow">
    <a href="#header" title="Посмотреть что наверху">&uarr;</a>
</div>       


<div class="bottom-menu">

    <% foreach (var item in MenuItems.GetRoot()) { %>
        <dl>
            <dt><%= item.Name%></dt>

            <% foreach (var child in item.Childs) { %>
                <dd><a href="<%= child.Url %>"><%= child.Name %></a></dd>
            <% } %>
        </dl>
    <% } %>

    <dl id="counters">
        <dt>СЧЕТЧИКИ</dt>
            
        <dd></dd>
    </dl>

    <div class="clear"></div>
</div>


<div class="copy">
    <b>IT Community &copy; 2005 - <%= DateTime.Now.Year %></b>
    <p>Данный ресурс поддерживается сообществом АЙТИ коммунистов г.Якутска.</p>
    <p>Проект некоммерческий.</p>
</div>

