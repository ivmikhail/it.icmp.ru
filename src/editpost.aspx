<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="editpost.aspx.cs" Inherits="ITCommunity.EditPost" Title="Ykt IT Community | Добавление новости" EnableViewState="true" %>
<%@ Register src="~/controls/TagsInfoControl.ascx" tagname="TagsInfo"   tagprefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="server">
<script type="text/javascript">
    window.addEvent('domready', function(){ 
        
        // Вставка загруженного изображения             
	    $$('.uploaded-image').each(function(el) {	
	        el.addEvent('click', function(e){
	                //$('<%= TextAreaPostText.ClientID %>').insertAtCursor("<a href='" + (this.src).replace("thumb", "full") + "' target='_blank' title='Посмотреть картинку в оригинальном размере'><img src='"+ this.src +"' /></a>", false);  
	                $('<%= TextAreaPostText.ClientID %>').insertAtCursor("[url=" + (this.src).replace("thumb", "full") + "][img]" + this.src + "[/img]" + "[/url]", false);  
			    });
		});		
		
	    
		// Выбор категорий
		
	    var cat_dropdown = $('<%= DropDownListCats.ClientID %>');
	    var cat_names    = $('<%= SelectedCategoriesNames.ClientID %>'); 
	    var cat_ids      = $('<%= SelectedCategoriesIds.ClientID %>');
	    
		cat_dropdown.addEvent('change', function(e) {
		    var cat_name = cat_dropdown.options[cat_dropdown.selectedIndex].text;
		    var cat_id = cat_dropdown.options[cat_dropdown.selectedIndex].value;
		    		    
		    if(cat_dropdown.options[0].value == -1)
		    {
		        cat_dropdown.remove(0);
		    }
		    
		    if(cat_id > 0)
		    {
		        if(CategoryIsSelected(cat_ids, cat_id))
		        {		    
		            alert('Категория уже выбрана');
		        } else {
		            if(cat_ids.value != "")
		            {
		                cat_ids.value += ",";
		            }		    
		            cat_ids.value += cat_id;
                    cat_names.innerHTML += "<a href='#' id='" + cat_id + "' onclick='deleteCategory(this);return false;' class='delete-category' title='Убрать'>" + cat_name + "</a> ";
		        }
		    }
		});		
		
	});	
	
	function CategoryIsSelected(cat_ids, cat_id)
	{		
	    var status = false;
	    var cats = cat_ids.value.split(",");
	    
	    for(i = 0; i < cats.length; i++)
	    {
	        if(cats[i] == cat_id)
	        {		        
	            status = true;
	            break;
	        }
	    };	    
	    return status;
	}
	
    function deleteCategory(el)
    {    
	    var cat_ids = $('<%= SelectedCategoriesIds.ClientID %>');    
	    var cats = cat_ids.value.split(",");
	    
	    var true_cats = new Array();
	    
	    for(i = 0, j = 0 ; i < cats.length; i++, j++)
	    {
	        if(cats[i] != el.id)
	        {   
	            true_cats[j] = cats[i];
	        }
	    };	            
	    cat_ids.value = true_cats.join(",");        
        $(el.id).destroy();
    }
    
</script>
<div id="add_post">
    <ul class="list">
        <li>
            <h2>Категория</h2>
            <h3>Выберите категорию(можно несколько)</h3>
            <label>
                <asp:DropDownList ID="DropDownListCats" runat="server" CssClass="input-text"/>
            </label>
            <h3>Категории данной новости</h3>
            <p class="note">                   
                Категории выбирайте в последнюю очередь(состояние не сохраняется)
            </p>
            <div id="SelectedCategoriesNames" runat="server">
                <asp:Literal ID="CatNamesLiteral" runat="server"/>
            </div>            
            <asp:HiddenField ID="SelectedCategoriesIds" runat="server"  />
        </li>
        <li>        
            <h2>
                <label>
                    Прикреплено <asp:CheckBox ID="CheckBoxAttached" runat="server" Enabled="false"/>
                </label>                
            </h2>
            <p class="note">                   
                Администраторы могу "прикреплять" важные посты. Важный пост всегда виден сверху.
            </p>
        </li>
        <li>
            <h2>Заголовок</h2>
            <label>
                <asp:TextBox ID="TextBoxTitle" runat="server" Columns="20" CssClass="input-text" MaxLength="128" />  
             </label>
        </li>    
        <li>        
            <h2>Текст новости</h2>  
            <p class="note">                   
                Описание и текст разделяется разделителем([hr]). Если разделитель не вставите, то на главной появится весь текст новости, иначе только описание.
            </p>
            <uc:TagsInfo ID="TagInfo" runat="server"/>
            <asp:TextBox ID="TextAreaPostText" runat="server" Rows="50" Width="100%" MaxLength="10000" TextMode="MultiLine" />
        </li>        
        <li>
            <h2>Источник(откуда стырили)</h2>
            <label>                
                <asp:TextBox ID="TextBoxSource" runat="server" CssClass="input-text" MaxLength="1024"/> 
            </label>
        </li>            
        <li>
            <div class="error-message">
                <asp:Literal ID="AddPostMessages" runat="server" />
            </div>
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
                Не добавленные в новость картинки будут удалены автоматически. Чтобы добавить картинку в пост кликните на неё.
            </p>
            <span id="uploadImagesMessage" class="error-message">                
               <asp:Literal ID="UploadImageError" runat="server"/>
            </span>
            
            <hr />
            
            <div id="uploadImagesList">
               <asp:Literal ID="UploadedImagesList" runat="server"/>
            </div>        
        </li>        
        <li style="clear:both;">
            &nbsp;
        </li>
        <li class="big-button">
            <asp:LinkButton ID="LinkButtonAdd" runat="server" OnClick="LinkButtonAdd_Click">Добавить</asp:LinkButton>
        </li>
    </ul>
</div>
</asp:Content>

