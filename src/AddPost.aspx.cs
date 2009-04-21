using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

public partial class AddPost : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadCategories();
        }
    }
    private void LoadCategories()
    {
        List<Category> cats = Category.GetAll();

        foreach(Category cat in cats)
        {
            DropDownListCats.Items.Add(new ListItem(cat.Name, cat.Id.ToString()));
        }
    }
    protected void LinkButtonAdd_Click(object sender, EventArgs e)
    {
        List<string> errors = ValidateData();
        if (errors.Count == 0)
        {
            Post newpost = new Post();
            List<Category> cats = new List<Category>();
            cats.Add(Category.GetById(Convert.ToInt32(DropDownListCats.SelectedValue)));
            newpost.Cats = cats;
            newpost.Title = Server.HtmlEncode(TextBoxTitle.Text);
            newpost.Description = Server.HtmlEncode(TextBoxDesc.Text);
            newpost.Text = Server.HtmlEncode(TextBoxText.Text);
            newpost.Source = Server.HtmlEncode(TextBoxSource.Text);
            newpost.Author = CurrentUser.User;
            newpost.Attached = CheckBoxAttached.Checked;

            Post.Add(newpost);
            Response.Redirect(FormsAuthentication.DefaultUrl);
        } else
        {
            WriteErrors(errors);
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
    private void WriteErrors(List<string> errors)
    {
        string text = "<ul class='list'>";
        foreach (string message in errors)
        {
            text += "<li>" + message+"</li>";
        }
        text += "</ul>";
        AddPostMessages.Text = text;
        LinkButtonAdd.Focus();
    }
}
