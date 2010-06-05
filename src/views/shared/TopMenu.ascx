<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="ITCommunity.Core" %>
<%@ Import Namespace="ITCommunity.Models" %>
<%@ Import Namespace="ITCommunity.Module" %>
<div id="top-menu">
    <div class="content">
        <% if (CurrentUser.isAuth) { %>
            <h3>
                <%= Greeting.Get() %>
            </h3>
            <a href="/account/logout">выйти</a>
        <% } else { %>
            <a href="/account/login">войти</a>
            <a href="/account/register">регисрация</a>
            <a href="/account/send">восстановление</a>
        <%} %>
    </div>
</div>
