<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="ViewPage<MessageSendModel>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Отправка сообщении
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Отправка сообщении</h1>

    <% using (Html.BeginForm()) { %>

        <%= Html.LabelFor(m => m.Receiver) %>
        <%= Html.TextBoxFor(m => m.Receiver) %>
        <%= Html.ValidationMessageFor(m => m.Receiver)%>

        <%= Html.LabelFor(m => m.Title) %>
        <%= Html.TextBoxFor(m => m.Title)%>
        <%= Html.ValidationMessageFor(m => m.Title)%>

        <%= Html.LabelFor(m => m.Text) %>
        <% Html.RenderPartial("EditorToolbar"); %>
        <%= Html.TextAreaFor(m => m.Text)%>
        <%= Html.ValidationMessageFor(m => m.Text) %>

        <input type="submit" value="отправить" />                

    <% } %>

</asp:Content>
