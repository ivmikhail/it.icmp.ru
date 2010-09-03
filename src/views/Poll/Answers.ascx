<%@ Control Language="C#" Inherits="ViewUserControl<Poll>" %>


<% if (Model.IsVoted || Model.IsActive == false) { %>

    <img src="<%= Url.Action("pollchart", "post", new { id = Model.Id, isThumb = "true" })%>" alt="poll<%= Model.Id %>" />

<% } else { %>

    <% using (Html.BeginForm("VotePoll", "Post", new { id = Model.Id })) { %>

        <%= Html.Hidden("postId", Model.PostId) %>

        <% foreach (var answer in Model.PollAnswers) { %>
            <label>
                <% if (Model.IsMultiselect) { %>
                    <input type="checkbox" name="answers" value="<%= answer.Id %>" />
                <% } else { %>
                    <input type="radio" name="answers" value="<%= answer.Id %>" />
                <% } %>
                <%= answer.TextFormatted %>
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
