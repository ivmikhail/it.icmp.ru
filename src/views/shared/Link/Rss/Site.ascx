<%@ Control Language="C#" Inherits="ViewUserControl<Rss>" %>


<a 
    href="<%= Model.Feed.Links[0].GetAbsoluteUri() %>" title="Посетить сайт">
    источник</a>