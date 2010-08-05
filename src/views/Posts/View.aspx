<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="ViewPage<Post>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Model.TitleFormatted %>
</asp:Content>


<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <div class="post">
        <h2>
            <%= Model.TitleFormatted %>
        </h2>

        <div class="post-description">
            <%= Model.DescriptionFormatted %>
            <hr id="cut" />
            <%= Model.TextFormatted %>
        </div>

         <div class="post-meta">
            <div class="menu">
                <ul class="left">
                    <li><span class="date"><%= Model.CreateDate.ToString("dd MMMM yyyy, HH:mm") %></span></li>
                    <li><% Html.RenderPartial("UserLink", Model.GetAuthor()); %></li>
                </ul>

                <ul class="right">
                    <li>просмотров: <b class="views-count">~<%= Model.ViewsCount%></b></li>
                    <li>рейтинг: <b class="rating-none">0</b></li>
                </ul>
            </div>

            <div class="menu">
                <ul class="left">
                    <% foreach (var category in Model.Categories) { %>
                        <li><% Html.RenderPartial("CategoryLink", category); %></li>
                    <% } %>
                </ul>

                <ul class="right">
                    <% if (CurrentUser.isAuth) {
                        if (Model.IsFavorite) { %>
                            <li>
                                <%= Html.ActionLink(
                                    "убрать",
                                    "deletefavorite",
                                    "posts",
                                    new { id = Model.Id },
                                    new { title = "Убрать из избранных" }
                                )%>
                            </li>
                        <% } else { %>
                            <li>
                                <%= Html.ActionLink(
                                    "добавить",
                                    "addfavorite",
                                    "posts",
                                    new { id = Model.Id },
                                    new { title = "Добавить в избранные" }
                                )%>
                            </li>
                        <%} %>
                    <% } %>
                    <% if (Model.Source != null) { %>
                        <li><a href="<%= Model.Source %>" title="<%= Model.Source %>">источник</a></li>
                    <% } %>
                </ul>
            </div>
        </div>
        <div class="clear"></div>
    </div>


    <h2 id="comments">Комментарии (<%= Model.CommentsCount %>)</h2>

    <ul>
        <% foreach (var comment in  Model.Comments) { %>
            <li id="comment-<%= comment.Id %>" class="menu comment ">
                <ul class="left">
                    <li>
                        <% Html.RenderPartial("UserLink", comment.getAuthor()); %>
                    </li>
                    <li class="date meta">
                        <%= comment.CreateDate.ToString("dd MMMM yyyy, HH:mm") %>
                    </li>
                </ul>
                
                <ul class="right meta">
                    <li>
                        <% if (comment.Editable) { %>
                            <%= Html.ActionLink("редактировать", "edit", "comments", new { id = comment.Id }, null) %>
                        <% } %>
                    </li>
                    <li>
                        <a href="/posts/view/<%= comment.PostId %>#comment-<%= comment.Id %>" title="<%= comment.howMuchTimeHasPassed() %>" class="page-link">#</a>
                    </li>
                </ul>

                <div class="clear"></div>
                
                <div class="comment-text">
                    <%= comment.TextFormatted %> 
                </div>
            </li>
        <% } %>
    </ul>


    <h3 id="add-comment"><%= CurrentUser.User.Nick %>, напиши комментарий!</h3>                        
<%-- 
    <% Html.RenderPartial("EditorToolbar", "adding-comment"); %>
--%>
    <% using (Html.BeginForm("add", "comments", new { id = Model.Id })) { %>

        <%if (CurrentUser.User.IsAnonymous) {
              Html.RenderPartial("ITCaptcha");
        } %>
        <textarea id="adding-comment" name="addedcomment"></textarea>

        <input type="submit" value="добавить" />                

    <% } %>

</asp:Content>
