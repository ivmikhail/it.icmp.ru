<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>

<link rel="alternate"  type="application/rss+xml" href="<%= Url.Action("feed", "rss") %>" />    
<link rel="shortcut icon" href="<%= Url.Content("~/favicon.ico?v=0.1") %>" type="image/vnd.microsoft.icon" />
<link rel="stylesheet" type="text/css" href="<%= Url.Content("~/content/css/reset.css?v=0.1") %>" />
<link rel="stylesheet" type="text/css" href="<%= Url.Content("~/content/css/tags.css?v=0.1") %>" />
<link rel="stylesheet" type="text/css" href="<%= Url.Content("~/content/css/main.css?v=0.1") %>" />
<link rel="stylesheet" type="text/css" href="<%= Url.Content("~/content/css/header.css?v=0.1") %>" />
<link rel="stylesheet" type="text/css" href="<%= Url.Content("~/content/css/menu.css?v=0.1") %>" />
<link rel="stylesheet" type="text/css" href="<%= Url.Content("~/content/css/footer.css?v=0.1") %>" />
<link rel="stylesheet" type="text/css" href="<%= Url.Content("~/content/css/pagination.css?v=0.1") %>" />
<link rel="stylesheet" type="text/css" href="<%= Url.Content("~/content/css/links.css?v=0.1") %>" />
<link rel="stylesheet" type="text/css" href="<%= Url.Content("~/content/css/validation.css?v=0.1") %>" />
<link rel="stylesheet" type="text/css" href="<%= Url.Content("~/content/css/bbcode-info.css?v=0.1") %>" />
<link rel="stylesheet" type="text/css" href="<%= Url.Content("~/content/css/sidebar.css?v=0.1") %>" />
<link rel="stylesheet" type="text/css" href="<%= Url.Content("~/content/css/highlight-vs.css?v=0.1") %>" />
<!--[if IE 6]><link rel="stylesheet" type="text/css" href="<%= Url.Content("~/content/css/ie6-fix.css?v=0.1") %>" /><![endif]-->
<!--[if IE 7]><link rel="stylesheet" type="text/css" href="<%= Url.Content("~/content/css/ie7-fix.css?v=0.1") %>" /><![endif]-->

<script type="text/javascript" src="<%= Url.Content("~/content/js/highlight.pack.js?v=0.1") %>"></script>
<script type="text/javascript" src="<%= Url.Content("~/content/js/jquery-1.3.2.js?v=0.1") %>"></script>
<script type="text/javascript" src="<%= Url.Content("~/content/js/MicrosoftAjax.debug.js?v=0.1") %>"></script>
<script type="text/javascript" src="<%= Url.Content("~/content/js/MicrosoftMvcAjax.debug.js?v=0.1") %>"></script>

<script type="text/javascript" src="<%= Url.Content("~/content/js/it-community.js?v=0.1") %>"></script>
