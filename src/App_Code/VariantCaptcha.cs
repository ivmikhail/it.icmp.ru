using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ITCommunity;

namespace ITCommunity
{

    /// <summary>
    /// Summary description for VariantCaptcha
    /// </summary>
    public class VariantCaptcha
    {
        private readonly String _question;
        public String Question
        {
            get
            {
                return _question;
            }
        }

        private readonly List<String> _variants;
        public List<String> Variants
        {
            get
            {
                return _variants;
            }
        }

        private readonly int _rightAnswer;
        public int RightAnswer
        {
            get
            {
                return _rightAnswer;
            }
        }
        public VariantCaptcha(String question, List<String> variants, int rigthAnswer)
        {
            this._question = question;
            this._variants = variants;
            this._rightAnswer = rigthAnswer;
        }

        public static VariantCaptcha GetItCaptcha()
        {
            DataTable dt = Database.CaptchaGet();
            List<String> variants = new List<string>();
            int rightId = -1;
            string answer = null;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dt.Rows[i]["isAnswer"]))
                {
                    answer = dt.Rows[i]["text"].ToString();
                    continue;
                }
                variants.Add(dt.Rows[i]["text"].ToString());
                if (Convert.ToBoolean(dt.Rows[i]["isRight"]))
                {
                    rightId = variants.Count - 1;
                }
            }
            if (answer == null || rightId == -1)
            {
                return null;
            }
            return new VariantCaptcha(answer, variants, rightId);
        }
    }
}
