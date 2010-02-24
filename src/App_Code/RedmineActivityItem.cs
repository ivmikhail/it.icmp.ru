using System;

namespace ITCommunity {
	/// <summary>
	/// Данные в RSS активности проекта Redmine
	/// </summary>
	public class RedmineActivityItem {

		#region Properties

		private string _author;
		private string _title;
		private string _text;
		private string _url;

		public string Author {
			get { return _author; }
			set { this._author = value; }
		}

		public string Title {
			get { return _title; }
			set { _title = value; }
		}

		public string Text {
			get { return _text; }
			set { _text = value; }
		}

		public string Url {
			get { return _url; }
			set { _url = value; }
		}

		#endregion

		public RedmineActivityItem() {
		}
	}
}