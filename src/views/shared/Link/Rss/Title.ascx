<%@ Control Language="C#" Inherits="ViewUserControl<SyndicationItem>" %>


<a href="<%=Model.Links[0].GetAbsoluteUri() %>">
    <%= Model.Title.Text %></a>
