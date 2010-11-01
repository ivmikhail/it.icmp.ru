<%@ Control Language="C#" Inherits="ViewUserControl<SyndicationItem>" %>


<a href="<%=Model.Links[0].GetAbsoluteUri() %>" title="Перейти на страницу информации">
    <%= Model.Title.Text %></a>
