using System;
using System.Web.UI;

namespace ITCommunity {

	public partial class Notepad : Page {
	
		protected void Page_Load(object sender, EventArgs e) {
			int delete_id = GetDel();
			if (delete_id > 0) {
				Note.Delete(delete_id);
			}
			int records_count = 0;
			RepeaterNotes.DataSource = Note.GetPaged(GetPage(), Config.GetInt("MaxNotesCount"), CurrentUser.User.Id, ref records_count);
			RepeaterNotes.DataBind();
			NotesPager.DataBind(records_count, Config.GetInt("MaxNotesCount"));
		}

		private int GetPage() {
			int page_num;
			Int32.TryParse(Request.QueryString["page"], out page_num);
			return page_num == 0 ? 1 : page_num;
		}

		private int GetDel() {
			int id;
			Int32.TryParse(Request.QueryString["del"], out id);
			return id;
		}
	}
}