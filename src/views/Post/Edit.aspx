<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="ViewPage<ITCommunity.Models.Post.EditModel>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Добавление поста
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Добавление поста</h1>
 
    <% using (Html.BeginForm()) { %>           
       
        <h2>
            <%= Html.LabelFor(m => m.Title) %>
            <%= Html.TextBoxFor(m => m.Title) %>
            <%= Html.ValidationMessageFor(m => m.Title) %>
        </h2>

        <%= Html.LabelFor(m => m.Description)%>
        <%= Html.TextAreaFor(m => m.Description, new { @class = "post-description" })%>
        <%= Html.ValidationMessageFor(m => m.Description)%>
        
        <%= Html.LabelFor(m => m.Text)%>
        <%= Html.TextAreaFor(m => m.Text, new { @class = "post-text" })%>
        <%= Html.ValidationMessageFor(m => m.Text)%>

        <label>Категории (выберите в какие категории будет входить пост)</label>

        <div id="select-categories">
            <% Html.RenderPartial("EditCategories", ITCommunity.Models.Post.EditCategoriesModel.Current); %>    
        </div>
        
        <%= Html.LabelFor(m => m.Source)%>
        <%= Html.TextBoxFor(m => m.Source)%>
    
        <input type="submit" value="сохранить" />

    <% } %>

</asp:Content>
