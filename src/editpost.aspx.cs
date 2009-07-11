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
                ImageOptions.Text = "Размер до " + Global.ConfigStringParam("PostImgWidth") + "x" + Global.ConfigStringParam("PostImgHeight") + "; обьем до " + (Math.Round((decimal.Parse(Global.ConfigStringParam("PostImgSize"))) / 1024, 2)).ToString() + "кб; тип файла изображение(jpeg, gif и т.д).";

                InitPostData();
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
                CatNamesLiteral.Text += "<a href='#' onclick='deleteCategory(this);return false;' class='delete-category' title='Убрать' id='" + cat.Id + "' </a>" + cat.Name + "</a> ";

                if (SelectedCategoriesIds.Value != "")
                {
                    SelectedCategoriesIds.Value += ",";
                }
                SelectedCategoriesIds.Value += cat.Id;
            }
            TextBoxTitle.Text = current_post.Title;
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
            List<string> errors = ValidateData();
            List<Category> cats = GetCategoriesFromPost();
            if (cats.Count == 0)
            {
                errors.Add("Категория не выбрана"); //TODO: Наверно надо переделать? 
            }
            if (errors.Count == 0)
            {
                Post newpost = current_post;
                newpost.Categories = cats;
                newpost.Title = Server.HtmlEncode(TextBoxTitle.Text);

                List<string> post_content = SplitPostContent(TextAreaPostText.Text);
                newpost.Description = post_content[0];
                newpost.Text = post_content[1];

                newpost.Source = Server.HtmlEncode(TextBoxSource.Text);
                newpost.Author = CurrentUser.User;
                newpost.Attached = CheckBoxAttached.Checked;

                Post addedpost = new Post();
                if (newpost.Id > 0)
                {
                    newpost.UpdateWithCategories();
                    addedpost = newpost;
                }
                else
                {
                    newpost.Author = CurrentUser.User;
                    addedpost = Post.Add(newpost);
                }
                Picture.FixImages(addedpost);
                Response.Redirect("default.aspx");
            } else
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
            if (text.Length == 0 || text.Length > 8000)
            {
                errors.Add("Количество символов в тексте(полное описание) новости должно быть от 1 до 8000.");
            }
            if (TextBoxSource.Text.Length > 1024)
            {
                errors.Add("Количество символов в источнике должно быть от 0 до 1024.");
            }

            return errors;
        }
        private void WriteErrors(List<string> errors, string title)
        {
            string text = "<h2>" + title + "</h2><ul class='list'>";
            foreach (string message in errors)
            {
                text += "<li>" + message + "</li>";
            }
            text += "</ul>";
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
            Post post = current_post;
            if(post.Id < 1)
            {
                post.Author = CurrentUser.User;
            }
            Picture pic = Picture.UploadImage(UploadImage.PostedFile, post);
            if (pic.Name == "")
            {
                UploadImageError.Text = "Картинка не добавилась. Видимо плохая картинка.";
            } else
            {
                UploadedImagesList.Text += "<img src='" + pic.ThumbUrl + "' width='150' class='uploaded-image'/>";
            }
            AttachImageButton.Focus();
        }

        private void LoadImages(Post post)
        {
            List<Picture> pics = Picture.GetByPost(post);
            foreach (Picture pic in pics)
            {
                UploadedImagesList.Text += "<img src='" + pic.ThumbUrl + "' width='150' class='uploaded-image'/>";
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
            } else
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