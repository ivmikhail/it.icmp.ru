<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="ViewPage<CaptchaListModel>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Список IT капч
</asp:Content>

<asp:Content ID="Menu" ContentPlaceHolderID="MenuContent" runat="server">
    <% Html.RenderPartial("../Admin/Menu"); %>
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <% using (Html.BeginForm("add", "captcha")) { %>
        <div class="block">
            
            <h1>Добавить капчу</h1>
            <label for="Question">Вопрос капчи</label>
            <%= Html.TextBox("Question") %>

            <input type="submit" value="добавить" />

        </div>
    <% } %>

    <h1>Список IT капч</h1>

    <ul>
        <% foreach (var captcha in Model.List) { %>
            <li class="light-block">
                <ul class="left-list">
                    <li>
                        <%= captcha.Question %>
                    </li>
                    <li class="meta">
                        <% foreach (var answer in captcha.CaptchaAnswers) {
                               if (answer.IsRight) { %>
                                <%= answer.Text%>
                            <% }
                        } %>
                    </li>
                </ul>
                <ul class="right-list meta">
                    <li>
                        <% Html.RenderPartial("Link/Captcha/Edit", captcha); %>
                    </li>
                    <li>
                        <% Html.RenderPartial("Link/Captcha/Delete", captcha); %>
                    </li>
                </ul>
                <div class="clear"></div>
            </li>
        <% } %>
    </ul>

    <% Html.RenderPartial("Pagination", Model); %>

</asp:Content>
