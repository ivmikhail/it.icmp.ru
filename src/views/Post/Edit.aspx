<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="ViewPage<PostEditModel>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Редактирование поста
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Редактирование поста</h1>

    <% if (Model.ShowPreview) Html.RenderPartial("../Post/Preview", Model); %>

    <% using (Html.BeginForm("edit", "post", null, FormMethod.Post, new { enctype = "multipart/form-data" })) { %>           
       
        <% Html.RenderPartial("EditForm", Model); %>

        <input type="submit" name="publish" value="изменить" />
        <input type="submit" name="preview" value="просмотреть" />

    <% } %>

</asp:Content>
