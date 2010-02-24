using System;
using System.Collections.Generic;
using System.Web.UI;

namespace ITCommunity {
	public partial class CategoriesMenu : UserControl {

		protected void Page_Load(object sender, EventArgs e) {
			if (!IsPostBack) {
				LoadCategories();
			}
		}

		private void LoadCategories() {
			List<Category> cats = new List<Category>();
			cats.Add(new Category(0, "Все новости", -1));
			cats.AddRange(Category.GetAll());
			NewsCategories.DataSource = cats;
			NewsCategories.DataBind();
		}
	}
}
