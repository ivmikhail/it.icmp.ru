<%@ Control Language="C#" Inherits="ViewUserControl<Poll>" %>


<% if (Model.IsVoted && Model.IsOpen == true) { %>

    <h2>Кто как голосовал:</h2>

    <ul>
        <% foreach (var answer in Model.PollAnswers) { %>
            <li class="light-block">
                <h3>
                    <%= answer.TextFormatted %>
                </h3>
                <% if (answer.Votes.Count == 0) { %>
                    нет голосов
                <% } else { %>
                    <ul class="left-list">
                        <% foreach (var vote in answer.Votes) { %>
                            <li>
                                <% Html.RenderPartial("Link/User/Profile", vote.User); %>
                            </li>
                        <% } %>
                    </ul>
                    <div class="clear"></div>
                <% } %>
            </li>
        <% } %>
    </ul>

<% } %>
