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
        List<PostCategory> cats = PostCategories.GetAll();

        foreach(PostCategory cat in cats)
        {
            DropDownListCats.Items.Add(new ListItem(cat.Name, cat.Id.ToString()));
        }
    }
    protected void LinkButtonAdd_Click(object sender, EventArgs e)
    {
        Post newpost = new Post();       
        
        newpost.Category    = PostCategories.GetById(Convert.ToInt32(DropDownListCats.SelectedValue)); 
        newpost.Title       = Server.HtmlEncode(TextBoxTitle.Text);
        newpost.Description = Server.HtmlEncode(TextBoxDesc.Text);
        newpost.Text        = Server.HtmlEncode(TextBoxText.Text);
        newpost.Source      = Server.HtmlEncode(TextBoxSource.Text);
        newpost.Author      = CurrentUser.User;
        newpost.Attached    = CheckBoxAttached.Checked;

        Posts.Add(newpost);
        Response.Redirect(FormsAuthentication.DefaultUrl);
    }
}
