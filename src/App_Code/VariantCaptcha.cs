using System;
using System.Collections.Generic;
using System.Data;

namespace ITCommunity {
	/// <summary>
	/// Summary description for VariantCaptcha
	/// </summary>
	public class VariantCaptcha {

		#region Properties

		private readonly string _question;
		private readonly List<string> _variants;
		private readonly int _rightAnswer;

		public string Question {
			get { return _question; }
		}

		public List<string> Variants {
			get { return _variants; }
		}

		public int RightAnswer {
			get { return _rightAnswer; }
		}

		#endregion

		public VariantCaptcha(string question, List<string> variants, int rigthAnswer) {
			this._question = question;
			this._variants = variants;
			this._rightAnswer = rigthAnswer;
		}

		public static VariantCaptcha GetItCaptcha() {
			DataTable dt = Database.CaptchaGet();
			List<string> variants = new List<string>();
			int rightId = -1;
			string answer = null;
			for (int i = 0; i < dt.Rows.Count; i++) {
				if (Convert.ToBoolean(dt.Rows[i]["isAnswer"])) {
					answer = dt.Rows[i]["text"].ToString();
					continue;
				}
				variants.Add(dt.Rows[i]["text"].ToString());
				if (Convert.ToBoolean(dt.Rows[i]["isRight"])) {
					rightId = variants.Count - 1;
				}
			}
			if (answer == null || rightId == -1) {
				return null;
			}
			return new VariantCaptcha(answer, variants, rightId);
		}
	}
}
