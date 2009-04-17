<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="News.aspx.cs" Inherits="News" Title="Ykt It Community | " %>
<%@ Register src="~/controls/ItCaptcha.ascx" tagname="ItCaptcha" tagprefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="post"> 
        <div class="post-title">
            <h2>
                <asp:HyperLink ID="HyperLinkCategory" runat="server">HyperLink</asp:HyperLink>
                &rarr;
                <asp:HyperLink ID="HyperLinkTitle" runat="server">HyperLink</asp:HyperLink>
            </h2>
            <div class="post-desc">
                <asp:Literal ID="desc" runat="server" />           
            </div>
            <div id="cut" class="post-text">
                 <asp:Literal ID="text" runat="server" />  
            </div>
        </div>                    
        <div class="post-info">
              <asp:Literal ID="date" runat="server" /> / �����:  <asp:Literal ID="author" runat="server" /> / ����������:  <asp:Literal ID="views" runat="server" /> / ��������: <asp:Literal ID="source" runat="server" />          
         </div>
        <div id="comments" class="post-comments">
            <h2>�����������(<asp:Literal ID="comments_count" runat="server" />)</h2>
                <asp:Repeater ID="RepeaterComments" runat="server">
                    <HeaderTemplate>                        
                        <ul id="comments-list" class="list">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li class="comment">
                            <div class="comment-info">                                
                                <%# Eval("createdate")%> / �����: <%# Eval("author.nick")%>
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
            <h2>�������� �����������</h2>    
            ����� - <asp:Literal ID="author_login" runat="server" />
            <br />
            <br />
            <uc:ItCaptcha ID="captcha" runat="server" Visible="false"/>           
            <div id="write-comment"> 
                <asp:TextBox ID="TextBoxComment" runat="server" TextMode="MultiLine" Rows="5" Width="100%" MaxLength="512"/>                
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorComment" runat="server" ErrorMessage="������� �����������" ControlToValidate="TextBoxComment" />
                <asp:LinkButton ID="LinkButtonAddComment" runat="server" OnClick="LinkButtonAddComment_Click">��������</asp:LinkButton>
            </div>
        </div>
     </div>
</asp:Content>

