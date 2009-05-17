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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategories();
                CheckBoxAttached.Enabled = CurrentUser.User.Role == ITCommunity.User.Roles.Admin;
                ImageOptions.Text = Global.PostImageOptions;

                InitPostData();
            }
        }
        private void InitPostData()
        {
            User current_user = CurrentUser.User;
            Post post = Post.GetById(GetPostId());
            Post current_post = new Post();

            if (post.IsPostOwner(CurrentUser.User) || current_user.Role == ITCommunity.User.Roles.Admin)
            {
                current_post = post;
                Picture.DeleteTempFolderFiles(current_post);
                LoadImages(current_post);
            }

            List<Category> cats = current_post.Cats;
            if (cats.Count != 0)
            {
                //TODO: Категории не сделаны. Надо чтобы показывались все категории данной новости
                DropDownListCats.SelectedValue = cats[0].Id.ToString();
            }
            TextBoxTitle.Text = current_post.Title;
            TextBoxDesc.Text = current_post.Description;
            TextBoxText.Text = current_post.Text;
            TextBoxSource.Text = current_post.Source;
            CheckBoxAttached.Checked = current_post.Attached;

        }

        private void LoadCategories()
        {
            List<Category> cats = Category.GetAll();

            foreach (Category cat in cats)
            {
                DropDownListCats.Items.Add(new ListItem(cat.Name, cat.Id.ToString()));
            }
        }

        protected void LinkButtonAdd_Click(object sender, EventArgs e)
        {
            List<string> errors = ValidateData();
            if (errors.Count == 0)
            {
                Post newpost = Post.GetById(GetPostId());
                List<Category> cats = new List<Category>();
                cats.Add(Category.GetById(Convert.ToInt32(DropDownListCats.SelectedValue)));
                newpost.Cats = cats;
                newpost.Title = Server.HtmlEncode(TextBoxTitle.Text);
                // TODO: Сделать буйню которая будет ескейпить тока некоторые указанные теги
                newpost.Description = TextBoxDesc.Text;
                newpost.Text = TextBoxText.Text;
                newpost.Source = Server.HtmlEncode(TextBoxSource.Text);
                newpost.Author = CurrentUser.User;
                newpost.Attached = CheckBoxAttached.Checked;

                Post addedpost = new Post();
                if (newpost.Id > 0)
                {
                    newpost.Update();
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
                WriteErrors(errors, "Новость не добавлена");
            }
        }

        private List<string> ValidateData()
        {
            List<string> errors = new List<string>();
            if (DropDownListCats.SelectedValue == "-1")
            {
                errors.Add("Выберите категорию");
            }
            if (TextBoxTitle.Text.Length == 0 && TextBoxTitle.Text.Length < 32)
            {
                errors.Add("Количество символов в заголовке должно быть от 1 до 32.");
            }
            if (TextBoxDesc.Text.Length == 0 && TextBoxDesc.Text.Length < 512)
            {
                errors.Add("Количество символов в описании должно быть от 1 до 512.");
            }
            if (TextBoxText.Text.Length == 0 && TextBoxText.Text.Length < 2048)
            {
                errors.Add("Количество символов в теле новости должно быть от 1 до 2048.");
            }
            if (TextBoxSource.Text.Length > 256)
            {
                errors.Add("Количество символов в источнике должно быть от 0 до 256.");
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
            Post post = Post.GetById(GetPostId());
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
        }

        private void LoadImages(Post post)
        {
            List<Picture> pics = Picture.GetByPost(post);
            foreach (Picture pic in pics)
            {
                UploadedImagesList.Text += "<img src='" + pic.ThumbUrl + "' width='150' class='uploaded-image'/>";
            }            
        }
    }
}