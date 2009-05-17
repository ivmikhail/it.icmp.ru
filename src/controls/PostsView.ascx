<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PostsView.ascx.cs" Inherits="ITCommunity.controls_PostsView" %>
<asp:Repeater ID="RepeaterPosts" runat="server" OnItemDataBound="RepeaterPosts_ItemDataBound">
        <HeaderTemplate>
            <ul id="posts-list" class="list">
        </HeaderTemplate>
        <ItemTemplate>
            <li>
                <div class="post"> 
                    <div class="post-title">
                        <h2>
                            <a href='news.aspx?id=<%# Eval("id")%>'><%# Eval("title")%></a>     
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
                        <%# Eval("createdate")%> /                          
                        <%# Eval("favoritesaction") %>  / 
                        автор: <a href="mailsend.aspx?receiver=<%# Eval("author.nick")%>" title="Отправить личное сообщение"><%# Eval("author.nick")%></a> / 
                        просмотров: <%# Eval("views")%> / 
                        <a href='news.aspx?id=<%# Eval("id")%>#comments' title="Посмотреть комментарии">комментарии(<%# Eval("commentscount")%>)</a>
                    </div>
                <div>
            </li>
        </ItemTemplate>
        <FooterTemplate>
            </ul>
        </FooterTemplate>
</asp:Repeater>