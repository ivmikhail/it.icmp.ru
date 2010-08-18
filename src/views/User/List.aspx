<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="ViewPage<UserListModel>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Юзеры
</asp:Content>

<asp:Content ID="Menu" ContentPlaceHolderID="MenuContent" runat="server">
    <% Html.RenderPartial("../Admin/Menu"); %>
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <h1>
        Юзеры
        <span class="meta">всего таких: <%= Model.TotalCount %></span>
        <ul class="right-list meta">
            <li>
                <% Html.RenderPartial("Link/User/Role/Admins"); %>
            </li>
            <li>
                <% Html.RenderPartial("Link/User/Role/Posters"); %>
            </li>
            <li>
                <% Html.RenderPartial("Link/User/Role/Users"); %>
            </li>
            <li>
                <% Html.RenderPartial("Link/User/Role/Banneds"); %>
            </li>
        </ul>
    </h1>

    <div class="meta block">
	    <h3>Существует 4 роли пользователей:</h3>
	    <ul>
		    <li>
			    <b>banned</b> - аккаунт забанен (залогиниться нельзя).
		    </li>
		    <li>
			    <b>user</b> - простой пользователь, может голосовать, комментировать без капчи, доступные закрытые разделы сайтов. Readonly короче.
		    </li>        
		    <li>
			    <b>poster</b> - права <b>user</b> + может добавлять/редактировать свои новости (является ролью по умолчанию)
		    </li>
		    <li>
			    <b>admin</b> - права <b>poster</b> + может управлять опросами, пользователями, может аттачить посты, редактировать любые посты. Так же управляет менюшкой, капчей, категориями новостей
		    </li>
	    </ul>
    </div>

    <ul>
        <% foreach (var user in Model.List) { %>
            <li class="block">
                <ul class="left-list meta">
                    <li>
                        <% Html.RenderPartial("Link/User/Profile", user); %>
                    </li>
                    <li>
                        <span class="info"><%= user.CreateDate.ToString("dd MMMM yyyy")%></span>
                    </li>
                    <li>
                        e-mail: <span class="info"><%= user.Email %></span>
                    </li>
                </ul>
                <ul class="right-list meta">
                    <li>
                        <% if (user.Role != ITCommunity.Db.User.Roles.Admin) { %>
                            <% Html.RenderPartial("Link/User/Role/ToAdmin", user); %>
                        <% } else { %>
                            админ
                        <% } %>
                    </li>
                    <li>
                        <% if (user.Role != ITCommunity.Db.User.Roles.Poster) { %>
                            <% Html.RenderPartial("Link/User/Role/ToPoster", user); %>
                        <% } else { %>
                            постер
                        <% } %>
                    </li>
                    <li>
                        <% if (user.Role != ITCommunity.Db.User.Roles.User) { %>
                            <% Html.RenderPartial("Link/User/Role/ToUser", user); %>
                        <% } else { %>
                            юзер
                        <% } %>
                    </li>
                    <li>
                        <% if (user.Role != ITCommunity.Db.User.Roles.Banned) { %>
                            <% Html.RenderPartial("Link/User/Role/ToBanned", user); %>
                        <% } else { %>
                            забанен
                        <% } %>
                    </li>
                </ul>
                <div class="clear"></div>
            </li>
        <% } %>
    </ul>

    <% Html.RenderPartial("Pagination", Model); %>

</asp:Content>
