using System;
using System.Web.UI;

namespace ITCommunity {

	public partial class Notepadadd : Page {

		protected void LinkButtonAdd_Click(object sender, EventArgs e) {
			var note = new Note();
			note.Title = NoteTitle.Text;
			note.Text = NoteText.Text;
			note.UserId = CurrentUser.User.Id;

			Note.Add(note);
			Response.Redirect("notepad.aspx");
		}

	}
}