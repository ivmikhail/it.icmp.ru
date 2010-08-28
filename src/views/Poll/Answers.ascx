﻿<%@ Control Language="C#" Inherits="ViewUserControl<Poll>" %>


<% if (Model.IsVoted || Model.IsActive == false) { %>

    <ul>
        <% foreach (var answer in Model.PollAnswers) { %>
            <li>
                <%= answer.Text %>
                <span class="meta">
                    голосов:
                    <span class="info">
                        <%= answer.Votes.Count %>
                    </span>
                </span>
            </li>
        <% } %>
    </ul>

<% } else { %>

    <% using (Html.BeginForm("VotePoll", "Post", new { id = Model.Id })) { %>

        <%= Html.Hidden("PostId", Model.PostId) %>

        <% foreach (var answer in Model.PollAnswers) { %>
            <label>
                <% if (Model.IsMultiselect) { %>
                    <input type="checkbox" name="answers" value="<%= answer.Id %>" />
                <% } else { %>
                    <input type="radio" name="answers" value="<%= answer.Id %>" />
                <% } %>
                <%= answer.Text %>
            </label>
        <% } %>

        <input type="submit" value="проголосовать" <% if (CurrentUser.IsAuth == false) { %>disabled="disabled" <% } %>/>
        <% if (CurrentUser.IsAuth == false) { %>
            <div class="meta">
                для голосования нужно <% Html.RenderPartial("Link/User/Login"); %>
            </div>
        <% } %>

    <% } %>

<% } %>
