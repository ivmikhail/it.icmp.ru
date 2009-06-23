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

            List<Category> cats = current_post.Categories;
            foreach (Category cat in cats)
            {
                CatNamesLiteral.Text += "<a href='#' onclick='deleteCategory(this);return false;' class='delete-category' title='������' id='" + cat.Id + "' </a>" + cat.Name + "</a> ";

                if (SelectedCategoriesIds.Value != "")
                {
                    SelectedCategoriesIds.Value += ",";
                }
                SelectedCategoriesIds.Value += cat.Id;
            }
            TextBoxTitle.Text = current_post.Title;
            TextAreaPostText.Value = current_post.Description + current_post.Text;
            TextBoxSource.Text = current_post.Source;
            CheckBoxAttached.Checked = current_post.Attached;

        }

        private void LoadCategories()
        {
            List<Category> cats = Category.GetAll();
            DropDownListCats.Items.Add(new ListItem("�������...", "-1"));
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
                errors.Add("��������� �� �������"); //TODO: ������� ���� ����������? 
            }
            if (errors.Count == 0)
            {
                Post newpost = Post.GetById(GetPostId());
                newpost.Categories = cats;
                newpost.Title = Server.HtmlEncode(TextBoxTitle.Text);
                // TODO: ������� ����� ������� �� ����� ��������� ���� ��������� ��������� ����

                string post_content = TextAreaPostText.Value;
                int index = post_content.ToLower().IndexOf("<hr>");
                string post_desc = "";
                string post_text = "";
                if (index > 0)
                {
                    post_desc = post_content.Substring(0, index);
                    post_text = post_content.Substring(index);
                } else
                {
                    post_desc = "";
                    post_text = post_content;
                }

                newpost.Description = post_desc;
                newpost.Text = post_text;
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
                //LiteralPostText.Text = HiddenPostText.Value;
                WriteErrors(errors, "������� �� ���������");
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
            if (TextBoxTitle.Text.Length == 0 && TextBoxTitle.Text.Length < 64)
            {
                errors.Add("���������� �������� � ��������� ������ ���� �� 1 �� 64.");
            }
            if (TextAreaPostText.Value.Length == 0 && TextAreaPostText.Value.Length < 8000)
            {
                errors.Add("���������� �������� � ���� ������� ������ ���� �� 1 �� 8000.");
            }
            if (TextBoxSource.Text.Length > 1024)
            {
                errors.Add("���������� �������� � ��������� ������ ���� �� 0 �� 1024.");
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
                UploadImageError.Text = "�������� �� ����������. ������ ������ ��������.";
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
    }
}