<%@ Control Language="C#" Inherits="ViewUserControl<Poll>" %>

<div class="center">
    <img src="<%= Url.Action("pollchart", "post", new { Model.Id })%>" />
</div>

<% if (Model.IsOpen) { %>

    <h2>Кто как голосовал</h2>

    <ul>
        <% foreach (var answer in Model.PollAnswers) { %>
            <li>
                <div class="info">
                    <%= answer.Text %>
                </div>

                <% if (answer.Votes.Count == 0) { %>
                    <div class="none-active">
                        голосов нет
                    </div>
                <% } else { %>
                <ul class="left-list">
                    <% foreach (var vote in answer.Votes) { %>
                        <li>
                            <% Html.RenderPartial("Link/User/Profile", Users.Get(vote.UserId)); %>
                        </li>
                    <% } %>
                </ul>
                <% } %>

                <div class="clear"></div>
            </li>
        <% } %>
    </ul>

<% } %>