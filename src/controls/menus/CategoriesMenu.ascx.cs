using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITCommunity
{
	public partial class CategoriesMenu : System.Web.UI.UserControl
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
			List<Category> cats = new List<Category>();
			cats.Add(new Category(0, "Все новости", -1));
			cats.AddRange(Category.GetAll());
			NewsCategories.DataSource = cats;
			NewsCategories.DataBind();
		}
	}
}
