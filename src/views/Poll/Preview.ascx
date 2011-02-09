<%@ Control Language="C#" Inherits="ViewUserControl<PollEditModel>" %>


<h1>
    <%= Model.ToPost().TitleFormatted%>
    <span class="meta">не сохранено</span>
</h1>

<div class="text">

    <% if (Model.Answers != null) { %>
        <% foreach (var answer in Model.Answers.Trim().Split('\n')) { %>
            <%if (answer.Trim().Length > 0) { %>
                <label>
                    <% if (Model.IsMultiselect) { %>
                        <input type="checkbox" name="answers" />
                    <% } else { %>
                        <input type="radio" name="answers" />
                    <% } %>
                    <%= HttpUtility.HtmlEncode(answer)%>
                </label> 
            <% } %>
        <% } %>
    <% } %>

    <input type="submit" value="проголосовать" disabled="disabled" />

    <hr id="cut" />

    <%= Model.ToPost().TextFormatted%>

</div>
