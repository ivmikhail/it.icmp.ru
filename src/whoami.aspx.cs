using System;
using System.Web;
using System.Web.UI;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace ITCommunity {

	public partial class WhoamiPage : Page {

		protected void Page_Load(object sender, EventArgs e) {
			if (!IsPostBack) {
				LoadData();
			}
		}

		private List<KeyValuePair<string, string>> GetData() {
			List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>>();
			NameValueCollection serverVars = HttpContext.Current.Request.ServerVariables;

			List<string> needValues = GetHeaders();

			foreach (string key in needValues) {
				values.Add(new KeyValuePair<string, string>(key, serverVars.Get(key)));
			}
			/*
			foreach (string key in serverVars)
			{
				values.Add(new KeyValuePair<string, string>(key, serverVars.Get(key)));
			}
			 */
			return values;
		}

		private List<string> GetHeaders() {
			var list = new List<string>();

			list.AddRange(new string[] {
				"REMOTE_ADDR",
				"REMOTE_HOST",

				"HTTP_USER_AGENT",
				"HTTP_ACCEPT",
				"HTTP_ACCEPT_CHARSET",
				"HTTP_ACCEPT_ENCODING",
				"HTTP_ACCEPT_LANGUAGE",
				"HTTP_REFERER",
				"HTTP_TE",
				"HTTP_X_FORWARDED_FOR",
				"HTTP_UA_CPU",
				"HTTP_VIA"
			});

			return list;

		}

		private void LoadData() {
			RepeaterData.DataSource = GetData();
			RepeaterData.DataBind();
		}
	}
}
