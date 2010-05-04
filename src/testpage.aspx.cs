using System;
using System.Web;
using System.Web.UI;

namespace ITCommunity {

	public partial class TestPage : Page {

		protected void Button_Click(object sender, EventArgs e) {
			Output.Text = BBCodeParser.Format(HttpUtility.HtmlEncode(Input.Text));
		}
	}
}