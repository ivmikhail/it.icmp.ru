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

namespace ITCommunity
{
	public partial class EditPost : System.Web.UI.Page
	{
		Post current_post = new Post();
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				LoadCategories();
				CheckBoxAttached.Enabled = (CurrentUser.User.Role == ITCommunity.User.Roles.Admin);
				ImageOptions.Text = "<div class='note'>Размер до " + Global.ConfigStringParam("PostImgWidth") + "x" + Global.ConfigStringParam("PostImgHeight") + "; обьем до " + (Math.Round((decimal.Parse(Global.ConfigStringParam("PostImgSize"))) / 1024, 2)).ToString() + "кб; тип файла изображение(jpeg, gif и т.д).</div>";
				if (GetPostId() > 0)
				{
					LinkButtonAdd.Text = "Изменить";
				}
				else
				{
					LinkButtonAdd.Text = "Добавить";
				}
				InitPostData();

                EditorToolbar.InputId = TextAreaPostText.ClientID;
			}

		}
		private void InitPostData()
		{
			User current_user = CurrentUser.User;
			Post post = Post.GetById(GetPostId());

			if (post.IsPostOwner(CurrentUser.User) || current_user.Role == ITCommunity.User.Roles.Admin)
			{
				current_post = post;
				Picture.DeleteTempFolderFiles(current_post);
				LoadImages(current_post);
			}

			List<Category> cats = current_post.Categories;
			foreach (Category cat in cats)
			{
				CatNamesLiteral.Text += "<a href='#' onclick='deleteCategory(this);return false;' class='delete-category' title='Убрать' id='" + cat.Id + "'>" + cat.Name + "</a> ";

				if (SelectedCategoriesIds.Value != "")
				{
					SelectedCategoriesIds.Value += ",";
				}
				SelectedCategoriesIds.Value += cat.Id;
			}
			TextBoxTitle.Text = HttpUtility.HtmlDecode(current_post.Title);
			TextAreaPostText.Text = current_post.Description + current_post.Text;
			TextBoxSource.Text = current_post.Source;
			CheckBoxAttached.Checked = current_post.Attached;

		}

		private void LoadCategories()
		{
			List<Category> cats = Category.GetAll();
			DropDownListCats.Items.Add(new ListItem("Выбрать...", "-1"));
			foreach (Category cat in cats)
			{
				DropDownListCats.Items.Add(new ListItem(cat.Name, cat.Id.ToString()));
			}
		}

		protected void LinkButtonAdd_Click(object sender, EventArgs e)
		{
			Post editable_post = Post.GetById(GetPostId());
			List<string> errors = ValidateData();
			List<Category> cats = GetCategoriesFromPost();
			if (cats.Count == 0)
			{
				errors.Add("Категория не выбрана"); //TODO: Наверно надо переделать? 
			}
			if (errors.Count == 0)
			{
				editable_post.Categories = cats;
				editable_post.Title = HttpUtility.HtmlEncode(TextBoxTitle.Text);

				List<string> post_content = SplitPostContent(TextAreaPostText.Text);
				editable_post.Description = post_content[0];
				editable_post.Text = post_content[1];

				editable_post.Source = Server.HtmlEncode(TextBoxSource.Text);
				editable_post.Attached = CheckBoxAttached.Checked;

				Post inserted_post = new Post();
				if (editable_post.Id > 0) //пост редактируют
				{
					editable_post.UpdateWithCategories();
					inserted_post = editable_post;
				}
				else // Пост только что создали
				{
					editable_post.Author = CurrentUser.User;
					inserted_post = Post.Add(editable_post);
					// Увеличиваем счетчик
					User current = CurrentUser.User;
					current.HeaderTextCounter++;
					current.Update();
				}
				Picture.FixImages(inserted_post);
				Response.Redirect("news.aspx?id=" + inserted_post.Id);
			}
			else
			{
				SelectedCategoriesIds.Value = "";
				CatNamesLiteral.Text = "";
				WriteErrors(errors, "Новость не добавлена");
			}
		}

		private List<Category> GetCategoriesFromPost()
		{
			List<Category> cats = new List<Category>();
			string[] cat_ids = SelectedCategoriesIds.Value.Split(',');

			foreach (string string_cat_id in cat_ids)
			{
				int cat_id = -1;
				Int32.TryParse(string_cat_id, out cat_id);
				if (cat_id > 0 && Category.IsCategoryExist(cat_id))
				{
					Category category = Category.GetById(cat_id);
					if (!cats.Contains(category))
					{
						cats.Add(category);
					}
				}
			}
			return cats;
		}

		private List<string> ValidateData()
		{
			List<string> errors = new List<string>();

			List<string> post_content = SplitPostContent(TextAreaPostText.Text);
			string desc = post_content[0];
			string text = post_content[1];

			if (TextBoxTitle.Text.Length == 0 || TextBoxTitle.Text.Length > 128)
			{
				errors.Add("Количество символов в заголовке должно быть от 1 до 128.");
			}
			if (desc.Length > 2000)
			{
				errors.Add("Количество символов в описании(краткое описание) новости должно быть до 2000.");
			}
			if (text.Length == 0 || text.Length > 16000)
			{
				errors.Add("Количество символов в тексте(полное описание) новости должно быть от 1 до 16000.");
			}
			if (TextBoxSource.Text.Length > 1024)
			{
				errors.Add("Количество символов в источнике должно быть от 0 до 1024.");
			}

			return errors;
		}
		private void WriteErrors(List<string> errors, string title)
		{
			string text = "<div  class='error'><h2>" + title + "</h2><ul>";
			foreach (string message in errors)
			{
				text += "<li>" + message + "</li>";
			}
			text += "</ul></div>";
			AddPostMessages.Text = text;
			LinkButtonAdd.Focus();
		}
		private int GetPostId()
		{
			int id;
			Int32.TryParse(Request.QueryString["id"], out id);
			return id;
		}

		protected void AttachImageButton_Click(object sender, EventArgs e)
		{
			Post post = Post.GetById(GetPostId());
			if (post.Id < 1)
			{
				post.Author = CurrentUser.User;
			}
			Picture pic = Picture.UploadImage(UploadImage.PostedFile, post);
			if (pic.Name == "")
			{
				UploadImageError.Visible = true;
			}
			else
			{
				UploadedImagesList.Text += "<img src='" + pic.ThumbUrl + "' width='150' alt='картинка' class='uploaded-image'/>";
			}

			//Сохраняем выбранные категории

			string selectedIds = SelectedCategoriesIds.Value;
			string selectedCatsNames = "";
			if (selectedIds != "")
			{
				string[] catIds = selectedIds.Split(',');
				foreach (string stringCatId in catIds)
				{
					int cat_id = -1;
					Int32.TryParse(stringCatId, out cat_id);

					Category cat = Category.GetById(Convert.ToInt32(cat_id));
					if (cat.Id > 0)
					{
						selectedCatsNames += "<a href='#' id='" + cat.Id + "' onclick='deleteCategory(this);return false;' class='delete-category' title='Убрать'>" + cat.Name + "</a> ";
					}
				}
			}
			SelectedCategoriesNames.InnerHtml = selectedCatsNames;

			AttachImageButton.Focus();
		}

		private void LoadImages(Post post)
		{
			List<Picture> pics = Picture.GetByPost(post);
			foreach (Picture pic in pics)
			{
				UploadedImagesList.Text += "<img src='" + pic.ThumbUrl + "' width='150' alt='Загруженная картинка' class='uploaded-image'/>";
			}
		}

		/// <summary>
		/// Сплитит новость на краткое описание и текст
		/// </summary>
		/// <param name="content">Тело новости</param>
		/// <returns>Возвращает лист строк. 0 - краткое описание, 1 - текст</returns>
		private List<string> SplitPostContent(string content)
		{
			List<string> result = new List<string>();

			int index = content.ToLower().IndexOf("[hr]");
			string post_desc = "";
			string post_text = "";
			if (index > 0)
			{
				post_desc = content.Substring(0, index);
				post_text = content.Substring(index);
			}
			else
			{
				post_desc = "";
				post_text = content;
			}

			result.Add(post_desc);
			result.Add(post_text);

			return result;
		}
	}
}
