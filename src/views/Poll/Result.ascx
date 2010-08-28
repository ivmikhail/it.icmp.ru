<%@ Control Language="C#" Inherits="ViewUserControl<Poll>" %>

<% if (Model.VotesCount == 0) { %>
    <h2 class="none-active">
        Голосов пока нет
    </h2>
<% } else { %>
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
                        <span class="meta">
                            голосов:
                            <span class="info">
                                <%= answer.Votes.Count %>
                            </span>
                        </span>
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

<% } %>
