using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using ITCommunity;

namespace ITCommunity {
	public partial class EditPost : System.Web.UI.Page {
		Post current_post = new Post();

		protected void Page_Load(object sender, EventArgs e) {

			if (!IsPostBack) {

				LoadCategories();
				CheckBoxAttached.Enabled = CurrentUser.IsAdmin;
				ImageOptions.Text = "<div class=\"note\">Размер до " + Config.String("PostImgWidth") + "x" + Config.String("PostImgHeight") + "; обьем до " + (Math.Round((decimal.Parse(Config.String("PostImgSize"))) / 1024, 2)).ToString() + "кб; тип файла изображение(jpeg, gif и т.д).</div>";
				
                bool isEditPost = GetPostId() > 0;
                if (isEditPost) {
					LinkButtonAdd.Text = "Изменить";
				}
				else {
					LinkButtonAdd.Text = "Добавить";
                    EditableInfo.Text  = "После добавления пост можно будет редактировать/удалить в течении " + Config.Num("EditablePeriod") + " сек.";
				}
				InitPostData();

				EditorToolbarText.InputId = TextAreaPostText.ClientID;
			}

		}
		private void InitPostData() {
			User current_user = CurrentUser.User;
			Post post = Post.GetById(GetPostId());

			if (post.IsCurrentUserCanEdit || CurrentUser.IsAdmin) {
				current_post = post;
				Picture.DeleteTempFolderFiles(current_post);
				LoadImages(current_post);
			}

			InitPostCategories(current_post.Categories);

			TextBoxTitle.Text        = current_post.Title;
			TextAreaPostDesc.Text    = current_post.Description;
			TextAreaPostText.Text    = current_post.Text;
			TextBoxSource.Text       = current_post.Source;
			CheckBoxAttached.Checked = current_post.Attached;
		}

		private void InitPostCategories(List<Category> categories) {
			foreach (Category cat in categories) {
				CatNamesLiteral.Text += "<a href=\"#\" onclick=\"deleteCategory(this);return false;\" class=\"delete-category\" title=\"Убрать\" id=\"" + cat.Id + "\">" + cat.Name + "</a> ";

				if (SelectedCategoriesIds.Value != "") {
					SelectedCategoriesIds.Value += ",";
				}
				SelectedCategoriesIds.Value += cat.Id;
			}
		}

		private void LoadCategories() {
			List<Category> cats = Category.GetAll();
			DropDownListCats.Items.Add(new ListItem("Выбрать...", "-1"));
			foreach (Category cat in cats) {
				DropDownListCats.Items.Add(new ListItem(cat.Name, cat.Id.ToString()));
			}
		}

		protected void LinkButtonAdd_Click(object sender, EventArgs e) {
			Post editable_post = Post.GetById(GetPostId());
            bool isNewPost = editable_post.Id < 1;

			List<string> errors = ValidateData();
			List<Category> cats = GetCategoriesFromPost();
			if (cats.Count == 0) {
				errors.Add("Категория не выбрана"); //TODO: Наверно надо переделать? 
			}
            if (!isNewPost && !editable_post.IsCurrentUserCanEdit && CurrentUser.IsAdmin)
            {
                errors.Add("Редактирование запрещено, возможно истекло время редактирования");
            }
			if (errors.Count == 0) {
				editable_post.Categories = cats;
				editable_post.Title = TextBoxTitle.Text;

				editable_post.Description = TextAreaPostDesc.Text;
				editable_post.Text = TextAreaPostText.Text;

				editable_post.Source = TextBoxSource.Text;
				editable_post.Attached = CheckBoxAttached.Checked;

				Post inserted_post = new Post();
				if (isNewPost) 
				{

                    editable_post.Author = CurrentUser.User;
                    inserted_post = Post.Add(editable_post);
                    // Увеличиваем счетчик
                    User current = CurrentUser.User;
                    current.HeaderTextCounter++;
                    current.Update();
				}
				else 
				{
                    editable_post.UpdateWithCategories();
                    inserted_post = editable_post;
				}
				Picture.FixImages(inserted_post);
				Response.Redirect("news.aspx?id=" + inserted_post.Id);
			}
			else {
				InitPostCategories(cats);
				WriteErrors(errors, "Новость не добавлена/обновлена");
			}
		}

		private List<Category> GetCategoriesFromPost() {
			List<Category> cats = new List<Category>();
			string[] cat_ids = SelectedCategoriesIds.Value.Split(',');

			foreach (string string_cat_id in cat_ids) {
				int cat_id = -1;
				Int32.TryParse(string_cat_id, out cat_id);
				if (cat_id > 0 && Category.IsExist(cat_id)) {
					Category category = Category.GetById(cat_id);
					if (!cats.Contains(category)) {
						cats.Add(category);
					}
				}
			}
			return cats;
		}

		private List<string> ValidateData() {
			List<string> errors = new List<string>();

			if (TextBoxTitle.Text.Length == 0 || TextBoxTitle.Text.Length > 128) {
				errors.Add("Количество символов в заголовке должно быть от 1 до 128.");
			}
			if (TextAreaPostDesc.Text.Length == 0 || TextAreaPostDesc.Text.Length > 2000) {
				errors.Add("Количество символов в описании(краткое описание) новости должно быть от 1 до 2000.");
			}
			// if (TextAreaPostText.Text.Length == 0 || TextAreaPostText.Text.Length > 20000)
			//{
			//	errors.Add("Количество символов в тексте(полное описание) новости должно быть от 1 до 20000.");
			//}
			if (TextBoxSource.Text.Length > 1024) {
				errors.Add("Количество символов в источнике должно быть от 0 до 1024.");
			}

			return errors;
		}
		private void WriteErrors(List<string> errors, string title) {
			string text = "<div class=\"error\"><h2>" + title + "</h2><ul>";
			foreach (string message in errors) {
				text += "<li>" + message + "</li>";
			}
			text += "</ul></div>";
			AddPostMessages.Text = text;
			LinkButtonAdd.Focus();
		}
		private int GetPostId() {
			int id;
			Int32.TryParse(Request.QueryString["id"], out id);
			return id;
		}

		protected void AttachImageButton_Click(object sender, EventArgs e) {
			Post post = Post.GetById(GetPostId());
			if (post.Id < 1) {
				post.Author = CurrentUser.User;
			}
			Picture pic = Picture.UploadImage(UploadImage.PostedFile, post);
			if (pic.Name == "") {
				UploadImageError.Visible = true;
			}
			else {
				UploadedImagesList.Text += "<img src=\"" + pic.ThumbUrl + "\" width=\"150\" alt=\"картинка\" class=\"uploaded-image\"/>";
			}

			//Сохраняем выбранные категории

			string selectedIds = SelectedCategoriesIds.Value;
			string selectedCatsNames = "";
			if (selectedIds != "") {
				string[] catIds = selectedIds.Split(',');
				foreach (string stringCatId in catIds) {
					int cat_id = -1;
					Int32.TryParse(stringCatId, out cat_id);

					Category cat = Category.GetById(Convert.ToInt32(cat_id));
					if (cat.Id > 0) {
						selectedCatsNames += "<a href=\"#\" id=\"" + cat.Id + "\" onclick=\"deleteCategory(this);\" class=\"delete-category\" title=\"Убрать\">" + cat.Name + "</a> ";
					}
				}
			}
			SelectedCategoriesNames.InnerHtml = selectedCatsNames;

			AttachImageButton.Focus();
		}

		private void LoadImages(Post post) {
			List<Picture> pics = Picture.GetByPost(post);
			foreach (Picture pic in pics) {
				UploadedImagesList.Text += "<img src=\"" + pic.ThumbUrl + "\" width=\"150\" alt=\"Загруженная картинка\" class=\"uploaded-image\"/>";
			}
		}
	}
}
