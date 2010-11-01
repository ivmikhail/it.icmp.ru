<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="ViewPage<CaptchaEditModel>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Редактирование капчи
</asp:Content>

<asp:Content ID="Menu" ContentPlaceHolderID="MenuContent" runat="server">
    <% Html.RenderPartial("../Admin/Menu"); %>
</asp:Content>

<asp:Content ID="Sidebar" ContentPlaceHolderID="SidebarContent" runat="server">
    <% Html.RenderPartial("../Admin/Sidebar"); %>
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Редактирование капчи</h1>

    <% using (Html.BeginForm()) { %>
        <div class="block">

            <%= Html.HiddenFor(m => m.Id) %>

            <%= Html.LabelFor(m => m.Question) %>
            <%= Html.TextBoxFor(m => m.Question) %>
            <%= Html.ValidationMessageFor(m => m.Question) %>

            <div id="EditAnswers">
                <% Html.RenderPartial("EditAnswers", Model.Answers); %>
            </div>
            <%= Html.ValidationMessageFor(m => m.RightAnswerId) %>

            <input type="submit" value="сохранить" />

        </div>
    <% } %>


    <h2>Добавить ответ</h2>
    <% using (Ajax.BeginForm("AddAnswer", new AjaxOptions { UpdateTargetId = "EditAnswers" })) { %>
        <div>

            <%= Html.Hidden("CaptchaId", Model.Id) %>

            <%= Html.Label("Ответ") %>
            <%= Html.TextBox("Text") %>

            <input type="submit" value="добавить" />

        </div>
    <% } %>


</asp:Content>
