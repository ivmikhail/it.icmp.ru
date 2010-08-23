<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="ViewPage<HeaderAddModel>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Добавление текста для хидера
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Добавление текста для хидера</h1>

    <% using (Html.BeginForm()) { %>

        <div class="meta">
            Вы можете написать любой текст длиной <span class="info">до <%= ITCommunity.DB.Header.MaxLength%> символов</span>, который будет появляться наверху (в header) сайта. 
            <br />
		    Тексты появляются в случайном порядке.
            <br />
		    Текст будет <span class="info">жить <%= ITCommunity.DB.Header.ShowingHours%> часов</span> после добавления.
		    <br />
		    Запрещена любая реклама, нецензурщина и оскорбления. Несоответствующие требованиям тексты будут удаляться.
		    <br />
		    Желательно чтобы текст мог помещаться на одной строке.
        </div>

        <%= Html.LabelFor(m => m.Text) %>
        <%= Html.TextBoxFor(m => m.Text, new { maxlength = ITCommunity.DB.Header.MaxLength })%>
        <%= Html.ValidationMessageFor(m => m.Text)%>

        <input type="submit" value="добавить" />                

    <% } %>

</asp:Content>
