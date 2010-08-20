<%@ Control Language="C#" Inherits="ViewUserControl<List<CaptchaAnswer>>" %>


<label>Ответы</label>

<ul>
    <% foreach (var answer in Model) { %>
        <li>
            <ul class="left-list">
                <li>
                    <label class="radio">
                        <%= Html.RadioButton("RightAnswerId", answer.Id, answer.IsRight)%>
                        <%= answer.Text %>
                    </label>
                </li>
            </ul>
            <ul class="right-list meta">
                <li>
                    <%= Ajax.ActionLink(
                        "удалить",
                        "deleteanswer",
                        "captcha",
                        new { id = answer.Id },
                        new AjaxOptions { UpdateTargetId = "EditAnswers" }
                    )%>
                </li>
            </ul>
            <div class="clear"></div>
        </li>
    <% } %>
</ul>
