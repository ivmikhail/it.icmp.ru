<%@ Control Language="C#" Inherits="ViewUserControl<CaptchaModel>" %>


<%-- Если сделать через Hidden или HiddenFor, то ViewState не позволяет сменить id капчи на новый --%>
<input type="hidden" name="QuestionId" id="QuestionId" value="<%= Model.QuestionId %>" />

<label for="AnswerId">
    <%= Model.Question %>
    <div class="meta">проверка на айтишность</div>
</label>

<%= Html.DropDownListFor(m => m.AnswerId, Model.Answers, "Выберите ответ") %>
<%= Html.ValidationMessageFor(m => m.AnswerId) %>
