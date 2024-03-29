﻿<%@ Control Language="C#" Inherits="ViewUserControl<Poll>" %>


<% if (Model.IsVoted) { %>

    <h2>Варианты ответов:</h2>

    <ul>
        <% foreach (var answer in Model.PollAnswers) { %>
            <li class="light-block">
                <h3>
                    <%= answer.TextFormatted %>
                    <% if (answer.Votes.Count > 0) { %>
                        <span class="meta">
                            <%= answer.Percent.ToString(".##")%>% (<%= answer.Votes.Count %>)
                        </span>
                    <% } %>
                </h3>
                <% if (Model.IsOpen == true) { %>
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
                <% } %>
            </li>
        <% } %>
    </ul>

<% } %>
