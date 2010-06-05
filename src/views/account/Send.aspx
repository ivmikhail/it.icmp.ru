<%@ Page Title="" Language="C#" MasterPageFile="~/views/shared/Site.master" Inherits="System.Web.Mvc.ViewPage<ITCommunity.Models.SendModel>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
    <h2>Восстановление пароля.</h2>
    <p>
        Введите ниже ваш логин, ссылка для сброса пароля будет отправлена на ваш e-mail указанный при регистрации
    </p>
    <%= Html.ValidationSummary(true, "") %>

    <% using (Html.BeginForm()) { %>
        <div>
            <fieldset>
                <legend>Account information</legend>                
                <div class="editor-label">
                    <%= Html.LabelFor(m => m.UserName) %>
                </div>
                <div class="editor-field">
                    <%= Html.TextBoxFor(m => m.UserName) %>
                    <%= Html.ValidationMessageFor(m => m.UserName) %>
                </div>
                <p>
                    <input type="submit" value="Восстановить"/>
                </p>
            </fieldset>
        </div>
    <% } %>	
</asp:Content>
