<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" Title="Ykt IT Community | Главная" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Repeater ID="RepeaterPosts" runat="server">
        <HeaderTemplate>
            <ul id="posts_list" class="list">
        </HeaderTemplate>
        <ItemTemplate>
            <li>
                <div class="post"> 
                    <div class="post_desc">
                        <h2>
                            <a href='default.aspx?cat=<%# Eval("category.id")%>'><%# Eval("category.name")%></a> 
                            &rarr;
                            <a href='post.aspx?cat=<%# Eval("id")%>#cut'><%# Eval("title")%></a>
                        </h2>
                        <%# Eval("description")%>
                        <br />
                        <br />
                        <a href='Post.aspx?id=<%# Eval("id")%>#cut'>Читать далее &rarr;</a>
                    </div>
                    
                    <div class="post_info">
                     <%# Eval("createdate")%> / автор: <%# Eval("author.nick")%> / <a href='Post.aspx?id=<%# Eval("id")%>#comments'>комментарии</a> / просмотров <%# Eval("views")%>             
                    </div>
                <div>
            <li>
        </ItemTemplate>
        <FooterTemplate>
            </ul>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>

