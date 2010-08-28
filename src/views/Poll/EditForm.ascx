<%@ Control Language="C#" Inherits="ViewUserControl<PostEditPollModel>" %>


<h2>
    <%= Html.LabelFor(m => m.Topic) %>
    <%= Html.TextBoxFor(m => m.Topic)%>
    <%= Html.ValidationMessageFor(m => m.Topic)%>
</h2>

<% if (CurrentUser.IsAdmin) { %>
    <label>
        <%= Html.CheckBoxFor(m => m.IsAttached)%> прикрепленный опрос?
    </label>
<% } %>
<label>
    <%= Html.CheckBoxFor(m => m.IsCommentable, new { @checked = "checked" })%> разрешить комментарии?
</label>

<label>
    <%= Html.CheckBoxFor(m => m.IsMultiselect)%> разрешить выбирать несколько ответов?
</label>

<label>
    <%= Html.CheckBoxFor(m => m.IsOpen)%> показывать кто как проголосовал?
</label>

<%= Html.LabelFor(m => m.ActiveDays) %>
<div class="meta">
    необязательно, если ничего не введете, то опрос будет активен всегда
</div>
<%= Html.TextBoxFor(m => m.ActiveDays)%>
<%= Html.ValidationMessageFor(m => m.ActiveDays)%>

<%= Html.LabelFor(m => m.Answers) %>
<%= Html.TextAreaFor(m => m.Answers)%>
<%= Html.ValidationMessageFor(m => m.Answers)%>
