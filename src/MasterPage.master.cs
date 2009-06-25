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
using ITCommunity;

namespace ITCommunity
{

    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowUserData();
            LoadCategories();
            LoadLastComments();
            LoadLinks();
            LoadPopularPosts();
            LoadLastRegistered();
            LoadTopPosters();
            LoadStat();
        }
        private void ShowUserData()
        {
            if (CurrentUser.isAuth)
            {
                UserProfile.Visible = true;
            } else
            {
                LoginForm.Visible = true;
            }
        }
        private void LoadLinks()
        {
            RepeaterMenu.DataSource = MenuItem.GetByParent(0);
            RepeaterMenu.DataBind();
        }
        private void LoadPopularPosts()
        {
            PopularPosts.DataSource = Post.GetTop(Global.ConfigNumParam("PopularPostsPeriod"), Global.ConfigNumParam("PopularPostsCount"));
            PopularPosts.DataBind();
        }
        private void LoadLastComments()
        {
            LastComments.DataSource = Comment.GetLasts(Global.ConfigNumParam("LastCommentsCount"));
            LastComments.DataBind();
        }
        private void LoadCategories()
        {
            List<Category> cats = new List<Category>();
            cats.Add(new Category(-1, "Все новости", -1));
            cats.AddRange(Category.GetAll());
            NewsCategories.DataSource = cats;
            NewsCategories.DataBind();
        }
        private void LoadTopPosters()
        {
            TopPosters.DataSource = User.GetTopPosters(Global.ConfigNumParam("TopPostersCount"));
            TopPosters.DataBind();
        }
        private void LoadLastRegistered()
        {
            LastRegistered.DataSource = User.GetLastRegistered(Global.ConfigNumParam("LastRegisteredCount"));
            LastRegistered.DataBind();
        }
        private void LoadStat()
        {
            List<KeyValuePair<string, string>> stats = User.GetStats();
            foreach (KeyValuePair<string, string> stat in stats)
            {
                if (stat.Key == "admins")
                {
                    TotalAdmins.Text = stat.Value;
                } else if (stat.Key == "posters")
                {
                    TotalPosters.Text = stat.Value;
                } else
                {
                    TotalUsers.Text = stat.Value;
                }
            }
        }
        protected void RepeaterMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;
            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater RepeaterSubMenu = (Repeater)item.FindControl("RepeaterSubMenu");
                MenuItem current = (MenuItem)item.DataItem;
                RepeaterSubMenu.DataSource = MenuItem.GetByParent(current.Id);
                RepeaterSubMenu.DataBind();
            }
        }
        protected String IsBlank(object dataItem) {

            string result = "_self";
            bool isInNew = (bool)DataBinder.Eval(dataItem, "newWindow");

            if(isInNew) {
                result = "_blank";
            }

            return result;
        }
        protected void LinkButtonSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect("search.aspx?q=" + TextBoxQuery.Text);
        }
    }

}