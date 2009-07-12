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
using System.Collections.Generic;
namespace ITCommunity {

    public partial class ItCaptchaEdit : System.Web.UI.Page {
        private bool isNewQuestion;
        private int id = -1;
        private List<TextBox> answers = new List<TextBox>();
        private List<CheckBox> isRights = new List<CheckBox>();
        private DataTable data;
        protected override void  OnInit(EventArgs e) {
 	        base.OnInit(e);
            if (Request.QueryString["new"] == null) {
                isNewQuestion = false;
            } else {
                isNewQuestion = true;
            }
            Int32.TryParse(Request.QueryString["id"], out id);
            if (id < 0 && !isNewQuestion) {
                return;
            }
            this.data = Database.CaptchaAnswersList(id);
            generateControls(data);
        }
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                this.txtQuestion.Text = data.Rows[0]["text"].ToString();
                for(int i = 1; i<data.Rows.Count; i++) {
                    answers[i - 1].Text = data.Rows[i]["text"].ToString();

                }
            }
        }

        private void generateControls(DataTable table) {
            int count = table.Rows.Count - 1;
            for (int i = 0; i < count; i++) {
                TextBox tb = new TextBox();
                tb.ID = "answer" + i;
                this.pnlAnsewrs.Controls.Add(tb);
                Label br = new Label();
                br.Text = "<br/>";
                this.pnlAnsewrs.Controls.Add(br);
                answers.Add(tb);
            }
            

        }
        protected void btnAdd_Click(object sender, EventArgs e) {
            TextBox tb = new TextBox();
            tb.ID = "answer" + answers.Count;
            tb.Text = "ddd";
            this.pnlAnsewrs.Controls.Add(tb);
            Label br = new Label();
            br.Text = "<br/>";
            this.pnlAnsewrs.Controls.Add(br);
            answers.Add(tb);
        }
}


}