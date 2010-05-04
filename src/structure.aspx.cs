using System;
using System.Web.UI;

namespace ITCommunity {

	public partial class StructurePage : Page {

		protected void Page_Load(object sender, EventArgs e) {
			this.SqlDataSourceCategories.ConnectionString = this.SqlDataSourceMenu.ConnectionString = Global.GetConnectionString();

			if (!IsPostBack) {
				RestoreNewCatData();
				RestoreNewMenuItemData();
			}
		}

		protected void LinkButtonDropCache_Click(object sender, EventArgs e) {
			DropCache();
			ResetCacheText.Visible = true;
		}

		private void DropCache() {
			AppCache.Remove(Config.Get("CategoriesCacheName"));
			AppCache.Remove(Config.Get("MenuCacheName"));
		}

		protected void LinkButtonAddCat_Click(object sender, EventArgs e) {
			Category.Add(new Category(-1, TextBoxCatName.Text, Convert.ToInt32(TextBoxCatSort.Text)));
			RestoreNewCatData();
			gv_categories.DataBind();
		}

		protected void LinkButtonAddMenu_Click(object sender, EventArgs e) {
			MenuItem.Add(new MenuItem(-1, Convert.ToInt32(TextBoxMenuParent.Text), TextBoxMenuUrl.Text, Convert.ToInt32(TextBoxMenuSort.Text), TextBoxMenuName.Text, IsBlankCheckBox.Checked));
			gv_menuitems.DataBind();
			RestoreNewMenuItemData();
		}

		private void RestoreNewCatData() {
			TextBoxCatName.Text = "";
			TextBoxCatSort.Text = "0";
		}

		private void RestoreNewMenuItemData() {
			TextBoxMenuParent.Text = "0";
			TextBoxMenuUrl.Text = "";
			TextBoxMenuSort.Text = "0";
			TextBoxMenuName.Text = "";
			IsBlankCheckBox.Checked = false;
		}
	}
}
