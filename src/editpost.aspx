<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="editpost.aspx.cs" Inherits="ITCommunity.EditPost" Title="Ykt IT Community | Добавление новости"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="server">
<script type="text/javascript">

    window.addEvent('domready', function(){
    
	    $$('.uploaded-image').each(function(el) {
	
	        el.addEvent('click', function(e){
		         $('ctl00_ContentPlaceHolder1_TextBoxText').insertAtCursor("<a href='" + (this.src).replace("thumb", "full") + "' target='_blank' title='Посмотреть картинку в оригинальном размере'><img src='"+this.src+"' /></a>\n", false);        
		    });
		});
	});

</script>
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
                    Прикреплено <asp:CheckBox ID="CheckBoxAttached" runat="server" Enabled="false"/>
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
                <asp:TextBox ID="TextBoxDesc" runat="server" TextMode="MultiLine" Rows="20" Width="100%" MaxLength="512"/>  
            </label>
        </li>        
        <li>
            <h2>Текст новости</h2>      
            <asp:TextBox ID="TextBoxText" runat="server" TextMode="MultiLine" Rows="35" Width="100%" MaxLength="2048"/>  
        </li>        
        <li>
            <h2>Источник(откуда стырили)</h2>
            <label>                
                <asp:TextBox ID="TextBoxSource" runat="server" CssClass="input-text" MaxLength="256"/> 
            </label>
        </li>            
        <li>
            <div class="error-message">
                <asp:Literal ID="AddPostMessages" runat="server" />
            </div>
        </li>
        <li style="text-align:right;">
            <asp:LinkButton ID="LinkButtonAdd" runat="server" OnClick="LinkButtonAdd_Click">Добавить</asp:LinkButton>
        </li>
        <li>
            <h2>Картинки</h2>
            <p class="note">
               <asp:Literal ID="ImageOptions" runat="server"/>
            </p> 
            <h3>Выберите:</h3>                
            <asp:FileUpload ID="UploadImage" runat="server" />
            <asp:LinkButton ID="AttachImageButton" runat="server" OnClick="AttachImageButton_Click">Загрузить</asp:LinkButton>
            
            <h3>Загруженные:</h3>                     
            <p class="note">                   
                Не добавленные в новость картинки будут удалены автоматически
            </p>
            <span id="uploadImagesMessage" class="error-message">                
               <asp:Literal ID="UploadImageError" runat="server"/>
            </span>
            
            <hr />
            
            <div id="uploadImagesList">
               <asp:Literal ID="UploadedImagesList" runat="server"/>
            </div>        
        </li>
    </ul>
</div>
</asp:Content>

