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
                    <li><% Html.RenderPartial("Link/User/Profile", Model.Author); %></li>
                </ul>

                <ul class="right">
                    <li>просмотров: <b class="views-count">~<%= Model.ViewsCount%></b></li>
                    <li>рейтинг: <b class="rating-none">0</b></li>
                </ul>
            </div>

            <div class="menu">
                <ul class="left">
                    <% foreach (var category in Model.Categories) { %>
                        <li><% Html.RenderPartial("Link/Category/Posts", category); %></li>
                    <% } %>
                </ul>

                <ul class="right">
                    <% if (CurrentUser.isAuth) {
                        if (CurrentUser.User.Id == Model.AuthorId) { %>
                            <li>
                                <% Html.RenderPartial("Link/Post/Edit", Model); %>
                            </li>
                        <% }

                        if (Model.IsFavorite) { %>
                            <li>
                                <% Html.RenderPartial("Link/Favorite/Delete", Model); %>
                            </li>
                        <% } else { %>
                            <li>
                                <% Html.RenderPartial("Link/Favorite/Add", Model); %>
                            </li>
                        <%} %>
                    <% } %>
                    <% if (Model.Source != "") { %>
                        <li><% Html.RenderPartial("Link/Post/Source", Model); %></li>
                    <% } %>
                </ul>
            </div>
        </div>
        <div class="clear"></div>
    </div>

    <% Html.RenderPartial("Comments", Model.Comments.ToList()); %>

    <% if (CurrentUser.isAuth) { %>
        <% Html.RenderPartial("../Comment/Add", new ITCommunity.Models.Comment.AddModel(Model.Id)); %>
    <% } else { %>
        <% Html.RenderPartial("../Comment/AnonymousAdd", new ITCommunity.Models.Comment.AnonymousAddModel(Model.Id)); %>
    <% } %>

</asp:Content>
