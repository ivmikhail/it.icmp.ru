﻿<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "забыл пароль",
    "forgotpassword",
    "user",
    null,
    new { title = "Помочь?" }
)%>
