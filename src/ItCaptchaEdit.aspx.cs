using System;
using System.Data;
using System.Web.UI;

namespace ITCommunity {

	public partial class ItCaptchaEdit : Page {

		private int id = -1;
		protected void Page_Load(object sender, EventArgs e) {
			SqlDataSource1.ConnectionString = Global.GetConnectionString();
			if (Request.QueryString["new"] != null) {
				id = Convert.ToInt32(Database.CaptchaQuestionAdd());
				Response.Redirect("~/ItCaptchaEdit.aspx?id=" + id);
			}

			if (Int32.TryParse(Request.QueryString["del"], out id)) {
				Database.CaptchaDelete(id);
				Response.Redirect("~/itcaptchalist.aspx");
			}

			if (!Int32.TryParse(Request.QueryString["id"], out id)) {
				Response.Redirect("~/itcaptchalist.aspx");
			}
			if (!IsPostBack) {
				DataRow row = Database.CaptchaQuestionGet(id);
				if (row != null) {
					txtQuestion.Text = row["text"].ToString();
				}
				else {
					Response.Redirect("~/itcaptchalist.aspx");
				}
			}
		}

		protected void btnAdd_Click(object sender, EventArgs e) {
			Database.CaptchaAnswerAdd(id);
			GridView1.DataBind();
			GridView1.EditIndex = 0;
		}

		protected void lnkSaveQuestion_Click(object sender, EventArgs e) {
			if (txtQuestion.Text.Trim().Length > 0) {
				Database.CaptchaQuestionUpdate(id, txtQuestion.Text.Trim());
				Response.Redirect("~/itcaptchalist.aspx");
			}
		}
	}
}
