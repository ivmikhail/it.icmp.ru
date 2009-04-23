/*
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
 */
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

public partial class AddPost : System.Web.UI.Page, ICallbackEventHandler
{
    protected string returnValue = String.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadCategories();
            if (User.IsInRole("1"))
            {
                CheckBoxAttached.Enabled = true;
            }
            ImageOptions.Text = Global.PostImageOptions;
        }

        /*
        string[] filePaths = null;
        if (!String.IsNullOrEmpty(list.Value))
        {
            filePaths = list.Value.Split('|');
        }
        */
        // register the callback script 
        string sbReference = ClientScript.GetCallbackEventReference(this, "arg", "ReceiveServerData", "context");
        string cbScript = String.Empty;

        // check if the script is already registered or not 
        if (!ClientScript.IsClientScriptBlockRegistered("CallServer"))
        {
            cbScript = @" function CallServer(arg,context) { " + sbReference + "}";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "CallServer", cbScript, true);
        }
    }

    /// <summary>
    /// Загружаем картинку
    /// </summary>
    /// <returns>Урл по которому доступна картинка</returns>
    public string GetCallbackResult()
    {
        string result = "";
        Picture pic = Picture.UploadImage(returnValue, CurrentUser.User.Id, 0);
        if (pic.Name != "")
        {
            result = pic.ThumbUrl;
        }
        return result;
    }

    /// <summary>
    /// Получаем что отправил нам клиент
    /// </summary>
    /// <param name="eventArgument">Путь к файлу на компе юзера</param>
    public void RaiseCallbackEvent(string eventArgument)
    {
        if (!String.IsNullOrEmpty(eventArgument))
        {
            returnValue = eventArgument;
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
            Post current = Post.Add(newpost);

            //После добавления новости переименовываюм временную папку postimages/userid/0 в postimages/userid/postid
            Picture.FixImages(current, CurrentUser.User.Id);
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
            text += "<li>" + message+"</li>";
        }
        text += "</ul>";
        AddPostMessages.Text = text;
        LinkButtonAdd.Focus();
    }
}
