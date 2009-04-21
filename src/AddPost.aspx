<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddPost.aspx.cs" Inherits="AddPost" Title="Ykt IT Community | Добавление новости" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="server">
<div id="add_post">
    <ul class="list">
        <li>
            <h2>Выберите категорию</h2>
            <label>
                <asp:DropDownList ID="DropDownListCats" runat="server" CssClass="input-text"/>
            </label>
        </li>
        <li>        
            <h2>
                <label>
                    Прикреплено <asp:CheckBox ID="CheckBoxAttached" runat="server" />
                </label>
            </h2>
        </li>
        <li>
            <h2>Заголовок</h2>
            <label>
                <asp:TextBox ID="TextBoxTitle" runat="server" Columns="20" CssClass="input-text" MaxLength="32"/>  
             </label>
        </li>
        <li>
            <h2>Краткое описание</h2>
            <label>               
                <asp:TextBox ID="TextBoxDesc" runat="server" TextMode="MultiLine" Rows="10" Width="100%" MaxLength="512"/>  
            </label>
        </li>        
        <li>
            <h2>Текст новости</h2>          
            <label>               
                <asp:TextBox ID="TextBoxText" runat="server" TextMode="MultiLine" Rows="25" Width="100%" MaxLength="2048"/>  
            </label>
        </li>        
        <li>
            <h2>Источник(откуда стырили)</h2>
            <label>                
                <asp:TextBox ID="TextBoxSource" runat="server" CssClass="input-text" MaxLength="256"/> 
            </label>
        </li>    
        <li>
            <h2>Картинки</h2>
            <label>
               
            </label>  
        </li>
        <li>
            <div class="error-message">
                <asp:Literal ID="AddPostMessages" runat="server" />
            </div>
        </li>
        <li>
            <asp:LinkButton ID="LinkButtonAdd" runat="server" OnClick="LinkButtonAdd_Click">Добавить</asp:LinkButton>
        </li>
    </ul>
</div>
</asp:Content>

