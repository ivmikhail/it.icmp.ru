<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ViewPage<dynamic>" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
	Нет доступа  (ошибка 403)
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <h1>У Вас нет доступа к этой странице</h1>

    <div class="text">
        Такое впечатление, что Вы хотите что-то сломать...
    </div>

</asp:Content>

<asp:Content ID="Sidebar" ContentPlaceHolderID="SidebarContent" runat="server">
    <h2>
        Если вы считаете что нужно известить администраторов, то можете написать письмо по адресу <a href="mailto:it.icmp.ru@gmail.com" title="Написать письмо">it.icmp.ru@gmail.com</a>
    </h2>
</asp:Content>