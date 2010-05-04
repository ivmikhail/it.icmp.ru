using System;

namespace ITCommunity {

	public partial class MasterPage : System.Web.UI.MasterPage {

		protected void Page_Load(object sender, EventArgs e) {
			if (!IsPostBack) {
				HeaderTextLiteral.Text = HeaderText.GetRandom().Text;
				ThisYear.Text = DateTime.Now.Year.ToString();
			}
			YaknetCounter.Text = "";
			string url = Request.Url.ToString().ToLower();

			if (url == "http://it.icmp.ru/" || url == "http://it.icmp.ru/default.aspx" || url == "http://it.icmp.ru/default.aspx?cat=0" || url == "http://it.icmp.ru/default.aspx?cat=0&page=1" || url == "http://it.icmp.ru/default.aspx?page=1") {
				YaknetCounter.Text = "<a href=\"http://www.ykt.ru/yaknet/default.asp\" title=\"Перейти в рейтинговую систему\" target=\"_blank\" class=\"counter\"><img src=\"http://www.ykt.ru/yaknet/image.asp?id=IT_community\" alt=\"Рейтинг Ykt.Ru\" border=\"0\" width=\"50\" height=\"30\"/></a>";
			}
		}

		private int GetCatId() {
			int id = 0;
			Int32.TryParse(Request.QueryString["cat"], out id);
			return id;
		}

		private int GetPageNum() {
			int pageNum = 0;
			Int32.TryParse(Request.QueryString["page"], out pageNum);
			if (pageNum == 1) {
				pageNum = 0;
			}
			return pageNum;
		}
	}
}
