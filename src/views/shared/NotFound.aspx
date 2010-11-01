<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ViewPage<dynamic>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Страница не найдена (ошибка 404)
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Упс, ничего не найдено</h1>
    <div class="text">
        Страница(или файл) расположенная по данному адресу не найдена, это могло произойти по одной из следующих причин:
        <ul>
            <li>Страница перемещена на другой адрес</li>
            <li>Этой страницы больше не существует, вы получили очень старую ссылку</li>
            <li>Возможно вы неправильно набрали адрес</li>
        </ul>
        Мы советуем вам воспользоваться поиском по сайту, либо попробовать найти страницу(файл) через меню. Удачи!
    </div>
</asp:Content>

<asp:Content ID="Sidebar" ContentPlaceHolderID="SidebarContent" runat="server">
    <h2>
        Если вы считаете что нужно известить администраторов, то напишите письмо на адрес <a href="mailto:it.icmp.ru@gmail.com" title="Написать письмо">it.icmp.ru@gmail.com</a>
    </h2>
</asp:Content>
