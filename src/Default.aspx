<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" Title="Ykt IT Community | Главная" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2>Новости</h2>
    <asp:Repeater ID="RepeaterPosts" runat="server" OnItemDataBound="RepeaterPosts_ItemDataBound">
        <HeaderTemplate>
            <ul id="posts-list" class="list">
        </HeaderTemplate>
        <ItemTemplate>
            <li>
                <div class="post"> 
                    <div class="post-title">
                        <h2>
                            <a href='news.aspx?id=<%# Eval("id")%>#cut'><%# Eval("title")%></a>
                            
                            <asp:Repeater ID="RepeaterPostCategories" runat="server">
                                <HeaderTemplate>
                                    (
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <a href='default.aspx?cat=<%# Eval("id")%>'><%# Eval("name")%></a> 
                                </ItemTemplate>
                                <FooterTemplate>
                                    )
                                </FooterTemplate>
                                <SeparatorTemplate>,</SeparatorTemplate>
                            </asp:Repeater>
                        </h2>
                        <div class="post-desc">
                            <%# Eval("description")%>                        
                        </div>
                        <div class="post-read-link">
                            <a href='news.aspx?id=<%# Eval("id")%>#cut'>Читать далее &rarr;</a>
                        </div>
                    </div>
                    
                    <div class="post-info">
                        <%# Eval("createdate")%> / автор: <%# Eval("author.nick")%> / просмотров: <%# Eval("views")%> / <a href='News.aspx?id=<%# Eval("id")%>#comments'>комментарии(<%# Eval("commentscount")%>)</a>            
                    </div>
                <div>
            </li>
        </ItemTemplate>
        <FooterTemplate>
            </ul>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>

