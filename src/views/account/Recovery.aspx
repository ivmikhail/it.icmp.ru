<%@ Page Title="" Language="C#" MasterPageFile="~/views/shared/Site.master" Inherits="System.Web.Mvc.ViewPage<ITCommunity.Models.RecoveryModel>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Сброс пароля.</h2>    
    <%= Html.ValidationSummary(true, "") %>

    <% using (Html.BeginForm()) { %>
        <div>
            <fieldset>
                              
                <div class="editor-label">
                    Ваш логин : <%= Html.DisplayFor(m => m.UserNick)%>                    
                </div>
                <div class="editor-field">                                 
                    <%= Html.HiddenFor(m => m.UserNick)%>   
                </div>                          
                
                <div class="editor-label">
                    <%= Html.LabelFor(m => m.NewPassword ) %>
                </div>
                <div class="editor-field">
                    <%= Html.PasswordFor(m => m.NewPassword) %>
                    <%= Html.ValidationMessageFor(m => m.NewPassword) %>
                </div>
                
                <div class="editor-label">
                    <%= Html.LabelFor(m => m.ConfirmPassword) %>
                </div>
                <div class="editor-field">
                    <%= Html.PasswordFor(m => m.ConfirmPassword) %>
                    <%= Html.ValidationMessageFor(m => m.ConfirmPassword) %>
                </div>
                <p>
                    <input type="submit" value="Изменить"/>
                </p>
            </fieldset>
        </div>
    <% } %>	
    

</asp:Content>
