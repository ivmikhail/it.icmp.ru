<%@ Control Language="C#" Inherits="ViewUserControl<ITCommunity.Models.Captcha.CaptchaModel>" %>


<input type="hidden" name="QuestionId" id="QuestionId" value="<%= Model.QuestionId %>" />

<label for="AnswerId">
    <%= Model.Question %>
</label>

<%= Html.DropDownListFor(m => m.AnswerId, Model.Answers, "Выберите ответ") %>
