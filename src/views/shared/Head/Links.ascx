<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<link rel="alternate"  type="application/rss+xml" href="<%= Url.Action("feed", "rss") %>" />
    
<link rel="shortcut icon" href="<%= Url.Content("~/favicon.ico?v=0.1") %>" type="image/vnd.microsoft.icon" />

<% var v = "0.2"; %>
<link rel="stylesheet" type="text/css" href="<%= Url.Content("~/content/css/reset.css?v=" + v) %>" />
<link rel="stylesheet" type="text/css" href="<%= Url.Content("~/content/css/tags.css?v=" + v) %>" />
<link rel="stylesheet" type="text/css" href="<%= Url.Content("~/content/css/main.css?v=" + v) %>" />
<link rel="stylesheet" type="text/css" href="<%= Url.Content("~/content/css/header.css?v=" + v) %>" />
<link rel="stylesheet" type="text/css" href="<%= Url.Content("~/content/css/menu.css?v=" + v) %>" />
<link rel="stylesheet" type="text/css" href="<%= Url.Content("~/content/css/footer.css?v=" + v) %>" />
<link rel="stylesheet" type="text/css" href="<%= Url.Content("~/content/css/pagination.css?v=" + v) %>" />
<link rel="stylesheet" type="text/css" href="<%= Url.Content("~/content/css/links.css?v=" + v) %>" />
<link rel="stylesheet" type="text/css" href="<%= Url.Content("~/content/css/validation.css?v=" + v) %>" />
<link rel="stylesheet" type="text/css" href="<%= Url.Content("~/content/css/bbcode-info.css?v=" + v) %>" />
<link rel="stylesheet" type="text/css" href="<%= Url.Content("~/content/css/sidebar.css?v=" + v) %>" />
<link rel="stylesheet" type="text/css" href="<%= Url.Content("~/content/css/highlight-vs.css?v=" + v) %>" />

<!--[if IE 6]><link rel="stylesheet" type="text/css" href="<%= Url.Content("~/content/css/ie6-fix.css?v=" + v) %>" /><![endif]-->
<!--[if IE 7]><link rel="stylesheet" type="text/css" href="<%= Url.Content("~/content/css/ie7-fix.css?v=" + v) %>" /><![endif]-->

<script type="text/javascript" src="<%= Url.Content("~/content/js/highlight.pack.js?v=" + v) %>"></script>
<script type="text/javascript" src="<%= Url.Content("~/content/js/jquery-1.3.2.js?v=" + v) %>"></script>
<script type="text/javascript" src="<%= Url.Content("~/content/js/MicrosoftAjax.debug.js?v=" + v) %>"></script>
<script type="text/javascript" src="<%= Url.Content("~/content/js/MicrosoftMvcAjax.debug.js?v=" + v) %>"></script>

<script type="text/javascript" src="<%= Url.Content("~/content/js/it-community.js?v=" + v) %>"></script>
