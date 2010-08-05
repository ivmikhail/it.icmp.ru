<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
    
<% Captcha captcha = Captchas.GetRandom(); %>

<label for="captchaanswer">IT-captcha: <%= captcha.Question%></label>

<input type="hidden" name="captchaquestion" value="<%=captcha.Id %>" />

<select name="captchaanswer">
    <option selected="selected" value="-1">Выберите ответ</option>

    <% foreach (var answer in captcha.CaptchaAnswers) { %>
        <option value="<%= answer.Id %>"><%= answer.Text %></option>
    <% } %>
</select>        

    

