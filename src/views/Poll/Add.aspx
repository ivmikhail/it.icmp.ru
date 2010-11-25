<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="ViewPage<PollEditModel>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Добавление опроса
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Добавление опроса</h1>
 
    <% using (Html.BeginForm("add", "poll", null, FormMethod.Post, new { enctype = "multipart/form-data" })) { %>
       
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
        <span class="meta">один вариант на одной строке</span>
        <%= Html.TextAreaFor(m => m.Answers) %>
        <%= Html.ValidationMessageFor(m => m.Answers) %>

        <%= Html.LabelFor(m => m.Text)%>
        <% Html.RenderPartial("EditorToolbar"); %>
        <%= Html.TextAreaFor(m => m.Text, new { @class = "large" })%>
        <%= Html.ValidationMessageFor(m => m.Text)%>
  
        <% Html.RenderPartial("../Picture/Upload", Model); %>
  
        <label>Категории</label>
        <%= Html.ValidationMessageFor(m => m.IsSetCategory)%>
        <div class="meta">
            Выберите в какие категории будет входить пост. Пожалуйста выбирайте <b class="info">максимум 4 категории</b>.
            <% if (Poll.Category != null) { %>
                <span class="info">Категория "<%= Poll.Category.Name %>" будет выбрана в любом случае</span>
            <% } %>
        </div>
        <div id="select-categories">
            <% Html.RenderPartial("../Post/EditCategories", PostEditCategoriesModel.Current); %>    
        </div>
    
        <input type="submit" value="добавить опрос" />

    <% } %>
    
</asp:Content>
