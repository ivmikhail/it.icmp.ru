<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="ViewPage<CommentEditModel>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Редактирование комментария
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    
    <h1>Редактирование комментария</h1>

    <h3>Редактируется только в течение 5 минут после добавления</h3>                        

    <% Html.RenderPartial("EditorToolbar"); %>

    <% using (Html.BeginForm()) { %>

        <%= Html.HiddenFor(m => m.PostId) %>

        <%= Html.TextAreaFor(m => m.Text) %>
        <%= Html.ValidationMessageFor(m => m.Text) %>

        <input type="submit" value="изменить" />                

    <% } %>

</asp:Content>
