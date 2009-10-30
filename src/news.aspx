<%@ Page Language="C#" EnableViewState="true" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="news.aspx.cs" Inherits="ITCommunity.News" Title="Ykt It Community | " %>
<%@ Register src="~/controls/ItCaptcha.ascx"       tagname="ItCaptcha"  tagprefix="uc" %>
<%@ Register src="~/controls/PostManage.ascx"      tagname="PostManage" tagprefix="uc" %>
<%@ Register src="~/controls/TagsInfoControl.ascx" tagname="TagsInfo"   tagprefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="post"> 
        <div class="post-title">
            <h2>
                <asp:HyperLink ID="HyperLinkTitle" runat="server">HyperLink</asp:HyperLink>
                (<asp:Literal ID="LinksPostCategories" runat="server"></asp:Literal>)
            </h2>            
        </div>      
        <div class="post-desc">
             <asp:Literal ID="desc" runat="server" />           
        </div>
        <div id="cut" class="post-text">
             <asp:Literal ID="text" runat="server" />  
        </div>              
        <div class="post-info">
              <asp:Literal ID="date" runat="server" /> / 
              <asp:Literal ID="favorite" runat="server" /> /
              <asp:Literal ID="author" runat="server" /> / 
              просмотров:  <asp:Literal ID="views" runat="server" /> / 
              <asp:Literal ID="source" runat="server" />          
        </div>
         
        <uc:PostManage ID="PostManageControls" runat="server"/> 
        
        <div id="comments" class="post-comments">
            <h2>Комментарии(<asp:Literal ID="comments_count" runat="server" />)</h2>
                <asp:Repeater ID="RepeaterComments" runat="server" OnItemCommand="RepeaterComments_ItemCommand" OnItemDataBound="RepeaterComments_ItemDataBound" >
                    <HeaderTemplate>                        
                        <ul id="comments-list" class="list">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li>
                            <div id="comment-<%# Eval("id")%>" class="comment-content">
                                <p>
                                    <%# Eval("textformatted")%>
                                </p> 
                            </div>
                            <div class="comment-info">  
                                <%# Eval("createdate", "{0:dd MMMM yyyy, HH:mm}")%> / 
                                <a href="mailsend.aspx?receiver=<%# Eval("author.nick")%>" title="Отправить личное сообщение" class="post-author-link">
                                    <%# Eval("author.nick")%>
                                </a> 
                                &nbsp;
                                &nbsp;
                                <asp:LinkButton Visible="false" CommandArgument='<%# Eval("id") %>' ID="DeleteComment" runat="server" Text="удалить" CommandName="delete" />
                            </div>   
                       <li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                    </FooterTemplate>                
                </asp:Repeater>            
            <h2>Написать комментарий. Вы - <asp:Literal ID="author_login" runat="server" /></h2> 
            <uc:TagsInfo ID="TagInfo" runat="server"/>
            <uc:ItCaptcha ID="captcha" runat="server" Visible="false" EnableViewState="true"/>           
            <div id="write-comment">                            
                <asp:TextBox ID="TextBoxComment" runat="server" TextMode="MultiLine" Rows="5" Width="100%" MaxLength="512" ValidationGroup="Comment"/>                
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorComment" runat="server" ErrorMessage="Введите комментарий" ControlToValidate="TextBoxComment" ValidationGroup="Comment"/>
                <asp:LinkButton ID="LinkButtonAddComment" runat="server" OnClick="LinkButtonAddComment_Click" ValidationGroup="Comment">Добавить</asp:LinkButton>
            </div>
        </div>
     </div>
</asp:Content>

