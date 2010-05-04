using System;
using System.Web.UI;

namespace ITCommunity {

	public partial class NotFoundPage : Page {

		protected void Page_Load(object sender, EventArgs e) {
			Response.StatusCode = 404;
			if (!IsPostBack) {
				string req_path = Request.QueryString["aspxerrorpath"];
				if (!string.IsNullOrEmpty(req_path)) {
					LiteralReferrerUrl.Text = "Запрошенный ресурс - " + Global.SiteAddress + req_path;
				}
			}
		}
	}
}
