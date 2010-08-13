<%@ Control Language="C#" Inherits="ViewUserControl<Post>" %>


<a
    href="/post/view/<%= Model.Id %>#comments"
    title="Посмотреть комментарии">
    комментарии (<%= Model.CommentsCount%>)
</a>
