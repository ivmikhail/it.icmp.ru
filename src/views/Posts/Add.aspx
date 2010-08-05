<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="ViewPage<AddPostModel>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Добавление поста
</asp:Content>


<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        function removeCategoryHandler() {
            var li = $(this).parent();
            var cats = $('#all-categories');
            var op = cats.children(':first').clone().removeAttr('selected');
            op.text($(this).text());
            op.attr('value', li.children('input').attr('value'));

            cats.append(op);
            li.remove();
            return false;
        }

        $(document).ready(function () {
            $('#categories a').each(function () {
                $(this).click(removeCategoryHandler);
            });

            $('#all-categories').change(function () {
                var cat = $('.base-category').clone().removeClass('hidden').removeClass('base-category');
                var catName = this.options[this.selectedIndex].text;

                $('a', cat).text(catName).click(removeCategoryHandler);
                $('input', cat).attr('value', this.value);

                $(this.options[this.selectedIndex]).remove();
                $('#categories').append(cat);
            })
        });
    </script>

    <h1>Добавление поста</h1>
 
    <% using (Html.BeginForm()) { %>           
       
        <h2>
            <label for="Title">Заголовок поста</label>
            <%= Html.TextBoxFor(m => m.Title)%>
        </h2>

        <label for="Description">Описание</label>
        <%= Html.TextAreaFor(m => m.Description, new { @class = "post-description" })%>
        
        <label for="Text">Текст</label>
        <%= Html.TextAreaFor(m => m.Text, new { @class = "post-text" })%>

        <label for="all-categories">Категории</label>
        <select id="all-categories">
            <option selected="selected" value="-1">выберите категорию</option>

            <% foreach (var category in Categories.GetAll()) { %>        
                <option value="<%= category.Id %>"><%= category.Name %></option>
            <% } %>
        </select>

        <ul id="categories" class="left">
            <li class="hidden base-category">
                <a href="#" title="Убрать категорию"></a>
                <input type="hidden" name="CategoriesIds[]" />
            </li>
            <% foreach (var category in Model.Categories) { %>        
                <li>
                    <a href="#" title="Убрать категорию"><%= category.Name%></a>
                    <input type="hidden" name="CategoriesIds[]" value="<%= category.Id %>" />
                </li>
            <% } %>
        </ul>
        <div class="clear"></div>
    
        <label for="Source">Источник</label>
        <%= Html.TextBoxFor(m => m.Source)%>
    
        <input type="submit" value="добавить" />

    <% } %>

</asp:Content>
