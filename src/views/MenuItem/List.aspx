<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="ViewPage<MenuItemEditModel>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Менюшка подвала
</asp:Content>

<asp:Content ID="Menu" ContentPlaceHolderID="MenuContent" runat="server">
    <% Html.RenderPartial("../Admin/Menu"); %>
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <div class="block">
        <h1>Добавление или редактирование ссылки</h1>

        <% using (Html.BeginForm()) { %>

            <%= Html.LabelFor(m => m.ParentId) %>
            <%= Html.DropDownListFor(m => m.ParentId, MenuItemEditModel.ParentIds, "новый родительский пункт")%>
            <%= Html.ValidationMessageFor(m => m.ParentId)%>

            <%= Html.LabelFor(m => m.Name) %>
            <%= Html.TextBoxFor(m => m.Name)%>
            <%= Html.ValidationMessageFor(m => m.Name)%>

            <%= Html.LabelFor(m => m.Url) %>
            <%= Html.TextBoxFor(m => m.Url)%>
            <%= Html.ValidationMessageFor(m => m.Url)%>

            <%= Html.LabelFor(m => m.Sort) %>
            <%= Html.TextBoxFor(m => m.Sort)%>
            <%= Html.ValidationMessageFor(m => m.Sort)%>
            
            <label>
            <%= Html.CheckBoxFor(m => m.IsTargetBlank)%> открывать в новом окне?
            </label>

            <input type="submit" value="сохранить" />

        <% } %>
    </div>

    <h1>Ссылки менюшки</h1>

    <ul>
        <% foreach (var rootItem in MenuItems.GetRoot()) { %>
            <li>
                <div class="block">
                    <ul class="left-list">
                        <li>
                            <%= rootItem.Name %>
                        </li>
                        <li class="meta">
                             ID: <span class="info"><%= rootItem.Id %></span>
                        </li>
                        <li class="meta">
                            порядок сортировки: <span class="info"><%= rootItem.Sort %></span>
                        </li>
                    </ul>
                    <ul class="right-list meta">
                        <li>
                            <% Html.RenderPartial("Link/MenuItem/AddChild", rootItem); %>
                        </li>
                        <li>
                            <% Html.RenderPartial("Link/MenuItem/Edit", rootItem); %>
                        </li>
                        <li>
                            <% Html.RenderPartial("Link/MenuItem/Delete", rootItem); %>
                        </li>
                    </ul>
                    <div class="clear"></div>

                    <ul>
                        <% foreach (var child in rootItem.Children) { %>
                            <li>
                                <ul class="left-list">
                                    <li>
                                        <% Html.RenderPartial("Link/MenuItem/Url", child); %>
                                    </li>
                                    <li class="meta">
                                        порядок сортировки: <span class="info"><%= child.Sort %></span>
                                    </li>
                                </ul>
                                <ul class="right-list meta">
                                    <li>
                                        <% Html.RenderPartial("Link/MenuItem/Edit", child); %>
                                    </li>
                                    <li>
                                        <% Html.RenderPartial("Link/MenuItem/Delete", child); %>
                                    </li>
                                </ul>
                                <div class="clear"></div>
                            </li>
                        <% } %>
                    </ul>

                </div>
            </li>
        <% } %>
    </ul>

</asp:Content>
