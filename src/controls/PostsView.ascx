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
                            <asp:Image ID="AttachedImage" runat="server" ImageUrl="../media/img/design/attached.jpg" Visible="false" CssClass="attached-image" AlternateText="������ �������" />
                            <a href='news.aspx?id=<%# Eval("id")%>'><%# Eval("title")%></a>     
                            <asp:Repeater ID="RepeaterPostCategories" runat="server">
                                <HeaderTemplate>
                                    (
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <a href='default.aspx?cat=<%# Eval("id")%>' title="���������� ������� ���� ���������" class="post-category-link"><%# Eval("name")%></a> 
                                </ItemTemplate>
                                <FooterTemplate>
                                    )
                                </FooterTemplate>
                                <SeparatorTemplate>,</SeparatorTemplate>
                            </asp:Repeater>                       
                        </h2>                        
                    </div>
                    <div class="post-desc">
                        <%# (Eval("descriptionformatted").ToString() == "") ? Eval("textformatted") : Eval("descriptionformatted") %>                        
                    </div>
                    <div class="post-read-link">
                        <a href='news.aspx?id=<%# Eval("id")%>#cut'>������ ����� &rarr;</a>
                    </div>                    
                    <div class="post-info">
                        <%# Eval("createdate", "{0:dd MMMM yyyy, HH:mm}")%> /                      
                        <%# Eval("favoritesaction") %> /
                        �����: <a href="mailsend.aspx?receiver=<%# Eval("author.nick")%>" title="��������� ������ ��������� ������" class="post-author-link"><%# Eval("author.nick")%></a> /
                        ����������: <%# Eval("views")%> /
                        <a href='news.aspx?id=<%# Eval("id")%>#comments' title="���������� �����������" class="post-comments-link">�����������(<%# Eval("commentscount")%>)</a>
                    </div>
                <div>
            </li>
        </ItemTemplate>
        <FooterTemplate>
            </ul>
        </FooterTemplate>
</asp:Repeater>