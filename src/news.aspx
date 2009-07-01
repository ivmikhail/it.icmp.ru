<%@ Page Language="C#" EnableViewState="true" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="news.aspx.cs" Inherits="ITCommunity.News" Title="Ykt It Community | " Trace="true"%>
<%@ Register src="~/controls/ItCaptcha.ascx" tagname="ItCaptcha" tagprefix="uc" %>
<%@ Register src="~/controls/PostManage.ascx" tagname="PostManage" tagprefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="post"> 
        <div class="post-title">
            <h2>
                <asp:HyperLink ID="HyperLinkTitle" runat="server">HyperLink</asp:HyperLink>
                (<asp:Literal ID="LinksPostCategories" runat="server"></asp:Literal>)
            </h2>
            <div class="post-desc">
                <asp:Literal ID="desc" runat="server" />           
            </div>
            <div id="cut" class="post-text">
                 <asp:Literal ID="text" runat="server" />  
            </div>
        </div>                    
        <div class="post-info">
              <asp:Literal ID="date" runat="server" /> / автор:  <asp:Literal ID="author" runat="server" /> / просмотров:  <asp:Literal ID="views" runat="server" /> / <asp:Literal ID="source" runat="server" />          
         </div>
         
         <uc:PostManage ID="PostManageControls" runat="server"/> 
        
        <div id="comments" class="post-comments">
            <h2>Комментарии(<asp:Literal ID="comments_count" runat="server" />)</h2>
                <asp:Repeater ID="RepeaterComments" runat="server" >
                    <HeaderTemplate>                        
                        <ul id="comments-list" class="list">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li class="comment">
                            <div class="comment-info">                                
                                <%# Eval("createdate")%> / автор: <%# Eval("author.nick")%> / <asp:LinkButton ID="LinkButtonDelComment" runat="server">удалить</asp:LinkButton>
                            </div>                    
                            <div class="comment-content">
                                <p>
                                    <%# Eval("text")%>
                                </p> 
                            </div>
                       <li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                    </FooterTemplate>                
                </asp:Repeater>            
            <h2>Написать комментарий</h2>    
            Автор - <asp:Literal ID="author_login" runat="server" />
            <br />
            <br />
            <uc:ItCaptcha ID="captcha" runat="server" Visible="false" EnableViewState="true"/>           
            <div id="write-comment"> 
                <asp:TextBox ID="TextBoxComment" runat="server" TextMode="MultiLine" Rows="5" Width="100%" MaxLength="512" ValidationGroup="Comment"/>                
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorComment" runat="server" ErrorMessage="Введите комментарий" ControlToValidate="TextBoxComment" ValidationGroup="Comment"/>
                <asp:LinkButton ID="LinkButtonAddComment" runat="server" OnClick="LinkButtonAddComment_Click" ValidationGroup="Comment">Добавить</asp:LinkButton>
            </div>
        </div>
     </div>
</asp:Content>

