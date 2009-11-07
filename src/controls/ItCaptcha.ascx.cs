using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ITCommunity;

namespace ITCommunity
{
    public partial class ItCaptcha : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                setNewQuestion();
            }
        }
        private void setNewQuestion()
        {
            VariantCaptcha ic = null;
            int count = 0;
            while (ic == null && count<10) {
                ic = VariantCaptcha.GetItCaptcha();
                count++;
            }
            if (ic == null) {
                throw new Exception("cannot find right ItCaptcha");
            }
            lblQuestion.Text = ic.Question;
            ddlVariants.Items.Clear();
            ddlVariants.Items.Add(new ListItem("Выберите вариант", "-1"));
            for (int i = 0; i < ic.Variants.Count; i++)
            {
                ddlVariants.Items.Add(new ListItem(ic.Variants[i], i.ToString()));
            }
            hdnRightAnswer.Value = ic.RightAnswer.ToString();
        }
        public bool IsRightAnswer()
        {
            int rightAnswer;
            int answer;
           return Int32.TryParse(hdnRightAnswer.Value, out rightAnswer) && Int32.TryParse(ddlVariants.SelectedValue, out answer) && rightAnswer == answer;
        }
        public void SetErrorMessageVisible()
        {
            lblErrorMessage.Visible = true;
            setNewQuestion();
        }
    }
}