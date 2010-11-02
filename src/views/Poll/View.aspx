﻿<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="ViewPage<Post>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Model.TitleFormatted %>
</asp:Content>


<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <div class="block">
        <h1>
            <%= Model.TitleFormatted %>
        </h1>

        <div class="text">
            <% Html.RenderPartial("../Poll/Answers", Model.Entity); %>
            
            <hr id="cut" />

            <%= Model.TextFormatted %>
        </div>

        <% Html.RenderPartial("../Poll/UserAnswers", Model.Entity); %>

        <div class="meta">
            <ul class="left-list">
                <li>
                    <% Html.RenderPartial("../Poll/EndDate", Model.Entity); %>
                </li>
            </ul>
            <ul class="right-list">
                <li>
                    <% Html.RenderPartial("../Poll/IsOpen", Model.Entity); %>
                </li>
                <li>
                    <% Html.RenderPartial("../Poll/VotedUsersCount", Model.Entity); %>
                </li>
            </ul>
            <ul class="left-list">
                <li class="info">
                    <%= Html.Date(Model.CreateDate) %>
                </li>
                <li>
                    <% Html.RenderPartial("Link/User/Profile", Model.Author); %>
                </li>
            </ul>

            <ul class="right-list">
                <li>просмотров: <b class="info">~<%= Model.ViewsCount %></b> </li>
                <% if (CurrentUser.IsAuth) { %>
                    <li>
                        <% if (Model.AuthorId == CurrentUser.User.Id) { %>
                            <% Html.RenderPartial("../Rating/AuthorRating", Model.Rating); %>
                        <% } else { %>
                            <% Html.RenderPartial("../Rating/Rate", Model.Rating); %>
                        <% } %>
                    </li>
                <% } %>
            </ul>

            <ul class="left-list">
                <% foreach (var category in Model.Categories) { %>
                    <li>
                        <% Html.RenderPartial("Link/Category/Posts", category); %>
                    </li>
                <% } %>
            </ul>

            <ul class="right-list">
                <% if (CurrentUser.IsAdmin) { %>
                    <li>
                        <% Html.RenderPartial("Link/Poll/Delete", Model.Entity); %>
                    </li>
                <% } %>
                <% if (Model.Editable) { %>
                    <li>
                        <% Html.RenderPartial("Link/Poll/Edit", Model.Entity); %>
                    </li>
                <% } %>
                <% if (CurrentUser.IsAuth) { %>
                    <% if (Model.IsFavorite) { %>
                        <li>
                            <% Html.RenderPartial("Link/Favorite/Delete", Model); %>
                        </li>
                    <% } else { %>
                        <li>
                            <% Html.RenderPartial("Link/Favorite/Add", Model); %>
                        </li>
                    <% } %>
                <% } %>
            </ul>

        </div>
        <div class="clear"></div>
    </div>

    <% if (Model.IsCommentable) { %>

        <h2 id="comments">Комментарии (<%= Model.CommentsCount%>)</h2>

        <% Html.RenderPartial("../Comment/List", Model.Comments.ToList()); %>

        <% if (CurrentUser.IsAuth) { %>
            <% Html.RenderPartial("../Comment/Add", new CommentEditModel { PostId = Model.Id }); %>
        <% } else { %>
            <% Html.RenderPartial("../Comment/AnonymousAdd", new AnonymousCommentAddModel { PostId = Model.Id }); %>
        <% } %>

    <% } else { %>
        <h2 class="none-active">Комментарии отключены</h2>
    <% } %>

</asp:Content>