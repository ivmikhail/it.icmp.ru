<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" Title="Ykt IT Community | �������" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Repeater ID="RepeaterPosts" runat="server">
        <HeaderTemplate>
            <ul id="posts-list" class="list">
        </HeaderTemplate>
        <ItemTemplate>
            <li>
                <div class="post"> 
                    <div class="post-title">
                        <h2>
                            <a href='default.aspx?cat=<%# Eval("category.id")%>'><%# Eval("category.name")%></a> 
                            &rarr;
                            <a href='news.aspx?cat=<%# Eval("id")%>#cut'><%# Eval("title")%></a>
                        </h2>
                        <div class="post-desc">
                            <%# Eval("description")%>                        
                        </div>
                        <div class="post-read-link">
                            <a href='news.aspx?id=<%# Eval("id")%>#cut'>������ ����� &rarr;</a>
                        </div>
                    </div>
                    
                    <div class="post-info">
                        <%# Eval("createdate")%> / �����: <%# Eval("author.nick")%> / ����������: <%# Eval("views")%> / <a href='News.aspx?id=<%# Eval("id")%>#comments'>�����������(<%# Eval("commentscount")%>)</a>            
                    </div>
                <div>
            <li>
        </ItemTemplate>
        <FooterTemplate>
            </ul>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>

