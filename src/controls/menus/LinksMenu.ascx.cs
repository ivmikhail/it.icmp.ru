using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITCommunity {
	public partial class LinksMenu : UserControl {

		protected void Page_Load(object sender, EventArgs e) {
			if (!IsPostBack) {
				LoadLinks();
			}
		}

		private void LoadLinks() {
			RepeaterMenu.DataSource = MenuItem.GetByParent(0);
			RepeaterMenu.DataBind();
		}

		protected string IsBlank(object dataItem) {
			var item = (MenuItem)dataItem;

			if (item.OnNewWindow) {
				return "_blank";
			}

			return "_self";
		}

		protected void RepeaterMenu_ItemDataBound(object sender, RepeaterItemEventArgs e) {
			RepeaterItem item = e.Item;
			if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem) {
				Repeater RepeaterSubMenu = (Repeater)item.FindControl("RepeaterSubMenu");
				MenuItem current = (MenuItem)item.DataItem;
				RepeaterSubMenu.DataSource = MenuItem.GetByParent(current.Id);
				RepeaterSubMenu.DataBind();
			}
		}
	}
}
