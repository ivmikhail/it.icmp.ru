<%@ Control Language="C#" Inherits="ViewUserControl<MessageListModel>" %>


<% if (Model.List.Count == 0) { %>

    <h2>Упс, ничего нет</h2>

<% } else { %>

    <ul>
        <% foreach (var message in Model.List) { %>
            <li class="block">

                <h2><%= message.TitleFormatted%></h2>

                <div class="text">
                    <%= message.TextFormatted%> 
                </div>

                <ul class="left-list meta">
                    <li class="info">
                        <%= message.CreateDate.ToString("dd MMMM yyyy, HH:mm")%>
                    </li>
                    <li>
                        <% Html.RenderPartial("Link/User/Profile", message.Sender); %>
                        &rarr;
                        <% Html.RenderPartial("Link/User/Profile", message.Receiver); %>
                    </li>
                </ul>
                
                <ul class="right-list meta">
                    <li>
                        <% Html.RenderPartial("Link/Message/Delete", message); %>
                    </li>
                    <% if (message.ReceiverId == CurrentUser.User.Id) { %>
                        <% if (!message.IsReceiverRead) { %>
                            <li>
                                <% Html.RenderPartial("Link/Message/Read", message); %>
                            </li>
                        <% } %>
                        <li>
                            <% Html.RenderPartial("Link/Message/Answer", message); %>
                        </li>
                    <% } %>
                </ul>

                <div class="clear"></div>
            </li>
        <% } %>
    </ul>

    <% Html.RenderPartial("Pagination", Model); %>

<% } %>
