<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="notepad.aspx.cs" Inherits="ITCommunity.Notepad" Title="Ykt IT Community | Блокнот" %>
<%@ Register Src="~/controls/Pager.ascx" TagName="Pager" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h1>Блокнот</h1>
<a href='notepadadd.aspx'>добавить запись</a>
<asp:Repeater ID="RepeaterNotes" runat="server">
        <HeaderTemplate>
            <ul id="notes-list" class="list">
        </HeaderTemplate>
        <ItemTemplate>
            <li>
                <div class="note"> 
                        <h2>
                            <a href='notepad.aspx?id=<%# Eval("id")%>#cut'><%# Eval("title")%></a>
                        </h2>
                    </div>
                    
                    <div class="note-info">
                        <%# Eval("createdate", "{0:dd MMMM yyyy, HH:mm}")%> / <a href='notepad.aspx?del=<%# Eval("id")%>'>Удалить</a>
                    </div>
                <div>
            </li>
        </ItemTemplate>
        <FooterTemplate>
            </ul>
        </FooterTemplate>
    </asp:Repeater>
    
<uc:Pager id="NotesPager" runat="server" />
</asp:Content>


