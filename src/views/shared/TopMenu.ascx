<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%@ Import Namespace="ITCommunity.Core" %>
<%@ Import Namespace="ITCommunity.Models" %>
<%@ Import Namespace="ITCommunity.Modules" %>

<div id="top-menu">
    <div class="content">
        <% if (CurrentUser.isAuth) { %>
            <a href="/account/logout">выйти</a>
            <%= Greeting.Get() %>
        <% } else { %>
            <a href="/account/login">войти</a>
            <a href="/account/register">регисрация</a>
            <a href="/account/send">восстановление</a>
        <% } %>
    </div>
</div>
