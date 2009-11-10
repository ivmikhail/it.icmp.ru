using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITCommunity
{
	public partial class LinksMenu : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				LoadLinks();
			}
		}

		private void LoadLinks()
		{
			RepeaterMenu.DataSource = MenuItem.GetByParent(0);
			RepeaterMenu.DataBind();
		}

		protected String IsBlank(object dataItem)
		{
			string result = "_self";
			bool isInNew = (bool)DataBinder.Eval(dataItem, "newWindow");

			if (isInNew)
			{
				result = "_blank";
			}

			return result;
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
	}
}
