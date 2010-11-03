<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="ViewPage<Post>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Model.TitleFormatted %>
</asp:Content>


<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <div class="block">
        <h1>
            <%= Model.TitleFormatted %>
        </h1>

        <div class="text">
            <% Html.RenderPartial("../Poll/Answers", Model.Entity); %>
            
            <hr id="cut" />

            <%= Model.TextFormatted %>
        </div>

        <% Html.RenderPartial("../Poll/UserAnswers", Model.Entity); %>

        <div class="meta">
            <ul class="left-list">
                <li>
                    <% Html.RenderPartial("../Poll/EndDate", Model.Entity); %>
                </li>
            </ul>
            <ul class="right-list">
                <li>
                    <% Html.RenderPartial("../Poll/IsOpen", Model.Entity); %>
                </li>
                <li>
                    <% Html.RenderPartial("../Poll/VotedUsersCount", Model.Entity); %>
                </li>
            </ul>

            <div class="clear"></div>

            <% Html.RenderPartial("../Post/ViewMeta", Model); %>
        </div>
    </div>
  
    <% Html.RenderPartial("../Post/Like", Model); %>
  
    <% Html.RenderPartial("../Post/Comments", Model); %>

</asp:Content>
