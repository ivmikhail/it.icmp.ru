<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="ViewPage<PollEditModel>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Редактирование опроса
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Редактирование опроса</h1>
 
    <% using (Html.BeginForm("edit", "poll", null, FormMethod.Post, new { enctype = "multipart/form-data" })) { %>
        <h2>
            <%= Html.LabelFor(m => m.Topic) %>
            <div class="meta">нельзя редактировать</div>
            <%= Html.TextBoxFor(m => m.Topic, new { disabled = "disabled" })%>
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
            <%= Html.CheckBoxFor(m => m.IsMultiselect, new { disabled = "disabled" })%> разрешить выбирать несколько ответов?
            <div class="meta">нельзя редактировать</div>
        </label>
        <label>
            <%= Html.CheckBoxFor(m => m.IsOpen, new { disabled = "disabled" })%> показывать кто как проголосовал?
            <div class="meta">нельзя редактировать</div>
        </label>

        <%= Html.LabelFor(m => m.ActiveDays)%>
        <div class="meta">
            необязательно, если ничего не введете, то опрос будет активен всегда
        </div>
        <%= Html.TextBoxFor(m => m.ActiveDays)%>
        <%= Html.ValidationMessageFor(m => m.ActiveDays)%>
    
        <%= Html.LabelFor(m => m.Answers) %>
        <div class="meta">нельзя редактировать</div>
        <%= Html.TextAreaFor(m => m.Answers, new { disabled = "disabled" })%>
        <%= Html.ValidationMessageFor(m => m.Answers) %>
    
    <%= Html.LabelFor(m => m.Text)%>
        <% Html.RenderPartial("EditorToolbar"); %>
        <%= Html.TextAreaFor(m => m.Text, new { @class = "large" })%>
        <%= Html.ValidationMessageFor(m => m.Text)%>

        <% Html.RenderPartial("../Picture/Upload", Model); %>
  
        <label>Категории</label>
        <div class="meta">
            выберите в какие категории будет входить опрос.
            <% if (Poll.Category != null) { %>
                <span class="info">Категория "<%= Poll.Category.Name%>" будет выбрана в любом случае</span>
            <% } %>
        </div>
        <div id="select-categories">
            <% Html.RenderPartial("../Post/EditCategories", PostEditCategoriesModel.Current); %>    
        </div>

        <input type="submit" value="сохранить опрос" />

    <% } %>
    
</asp:Content>
