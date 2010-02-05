<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="editpost.aspx.cs" Inherits="ITCommunity.EditPost" Title="Ykt IT Community | Добавление новости" EnableViewState="true" %>
<%@ Register src="~/controls/BBCodeInfo.ascx"      tagname="BBCodeInfo"    tagprefix="uc" %>
<%@ Register src="~/controls/EditorToolbar.ascx"   tagname="EditorToolbar" tagprefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="server">
	<script type="text/javascript">
		var currentTextAreaId = '<%= TextAreaPostText.ClientID %>';

		window.addEvent('domready', function() {
			// Вставка загруженного изображения
			$$('.uploaded-image').each(function(el) {
				el.addEvent('click', function(e) {
					$(currentTextAreaId).insertAtCursor("[popup=" + (this.src).replace("thumb", "full") + "][img]" + this.src + "[/img]" + "[/popup]", false);
				});
			});

			var cat_dropdown = $('<%= DropDownListCats.ClientID %>');
			var cat_names = $('<%= SelectedCategoriesNames.ClientID %>');
			var cat_ids = $('<%= SelectedCategoriesIds.ClientID %>');

			cat_dropdown.addEvent('change', function(e) {
				var cat_name = cat_dropdown.options[cat_dropdown.selectedIndex].text;
				var cat_id = cat_dropdown.options[cat_dropdown.selectedIndex].value;

				if (cat_dropdown.options[0].value == -1) {
					cat_dropdown.remove(0);
				}

				if (cat_id > 0) {
					if (CategoryIsSelected(cat_ids, cat_id)) {
						alert('Категория уже выбрана');
					} else {
						if (cat_ids.value != "") {
							cat_ids.value += ",";
						}
						cat_ids.value += cat_id;
						cat_names.innerHTML += "<a href='#' id='" + cat_id + "' onclick='deleteCategory(this);return false;' class='delete-category' title='Убрать'>" + cat_name + "</a> ";
					}
				}
			});
		});

		function setCurrentTextArea(el) {
			currentTextAreaId = el.id;
			return false;
		}
		function CategoryIsSelected(cat_ids, cat_id) {
			var status = false;
			var cats = cat_ids.value.split(",");

			for (i = 0; i < cats.length; i++) {
				if (cats[i] == cat_id) {
					status = true;
					break;
				}
			};
			return status;
		}

		function deleteCategory(el) {
			var cat_ids = $('<%= SelectedCategoriesIds.ClientID %>');
			var cats = cat_ids.value.split(",");

			var true_cats = new Array();

			for (i = 0, j = 0; i < cats.length; i++, j++) {
				if (cats[i] != el.id) {
					true_cats[j] = cats[i];
				}
			};
			cat_ids.value = true_cats.join(",");
			$(el.id).destroy();
			return false;
		}

	</script>

	<div id="edit-post">
		<h2>Категория</h2>

		<label class="dropdown-list-select">
			<span class="label-title">Выберите категорию (можно несколько)</span>
			<asp:DropDownList ID="DropDownListCats" runat="server" />
		</label>

		<h3>Категории данной новости</h3>
		<div id="SelectedCategoriesNames" runat="server">
			<asp:Literal ID="CatNamesLiteral" runat="server" />
		</div>
		<asp:HiddenField ID="SelectedCategoriesIds" runat="server" />

		<label class="checkbox-input">
			<span class="label-title">Прикреплено</span>
			<asp:CheckBox ID="CheckBoxAttached" runat="server" Enabled="false"/>
			<span class="note">
				Администраторы могу "прикреплять" важные посты. Важный пост всегда виден сверху.
			</span>
		</label>

		<h2>Заголовок</h2>
		<label class="textbox-input">
			<asp:TextBox ID="TextBoxTitle" runat="server" Columns="20" MaxLength="128" />
		</label>
		
		<h2>Краткое описание</h2>
		<div class="note">
			Обязательное поле
		</div>
		<label class="textbox-textarea">
			<asp:TextBox ID="TextAreaPostDesc" runat="server" Rows="7" MaxLength="2000" TextMode="MultiLine" OnFocus="setCurrentTextArea(this);" />
		</label>
		
		<h2>Текст новости</h2>	
		<uc:BBCodeInfo ID="BBCodeInfo" runat="server"/>
		<uc:EditorToolbar ID="EditorToolbarText" runat="server" />
		<label class="textbox-textarea">
			<asp:TextBox ID="TextAreaPostText" runat="server" Rows="50" MaxLength="20000" TextMode="MultiLine" OnFocus="setCurrentTextArea(this);"/>
		</label>

		<h2>Источник (откуда стырили)</h2>
		<label class="textbox-input">
			<asp:TextBox ID="TextBoxSource" runat="server" MaxLength="1024"/> 
		</label>

		<h2>Картинки</h2>
		<div class="note">
			Не добавленные в новость картинки будут удалены автоматически. Чтобы добавить картинку в пост кликните на неё.
		</div>
		<div id="upload-images-list">
			<asp:Literal ID="UploadedImagesList" runat="server"/>
		</div>

		<h2>Загрузить картинку</h2>
		<asp:Literal ID="ImageOptions" runat="server"/>
		<asp:Literal ID="UploadImageError" runat="server" Visible="false">
			<div id="uploadImagesMessage" class="error">
				Картинка не добавилась. Видимо плохая картинка.
			</div>
		</asp:Literal>
		<label class="file-upload-input">
			<asp:FileUpload ID="UploadImage" runat="server" />
			<asp:LinkButton ID="AttachImageButton" runat="server" OnClick="AttachImageButton_Click">Загрузить</asp:LinkButton>
		</label>

		<div class="note">
			Нажимая кнопку добавить, вы соглашаетесь с <a href="http://it.icmp.ru:3000/wiki/itc/Правила_постинга" target="_blank" title="Правила постинга">правилами</a>
		</div>

		<asp:Literal ID="AddPostMessages" runat="server" />

		<div class="big-button">
			<asp:LinkButton ID="LinkButtonAdd" runat="server" OnClick="LinkButtonAdd_Click">Добавить</asp:LinkButton>
		</div>
	</div>
</asp:Content>
