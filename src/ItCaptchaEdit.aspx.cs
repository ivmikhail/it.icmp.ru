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
        protected void Page_Load(object sender, EventArgs e) {
            SqlDataSource1.ConnectionString = Global.ConnectionString();
            if (Request.QueryString["new"] == null) {
                isNewQuestion = false;
            } else {
                isNewQuestion = true;
            }
            Int32.TryParse(Request.QueryString["id"], out id);
            if (id < 0 && !isNewQuestion) {
                return;
            }
            if (!IsPostBack) {
                if (isNewQuestion) {
                    txtQuestion.Text = "";
                } else {
                    txtQuestion.Text = Database.CaptchaGet().Rows[0]["text"].ToString();
                }
            }
            if(isNewQuestion) {
                btnAdd.Visible = true;
            } else {
                btnAdd.Visible = false;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e) {
            Database.CaptchaAnswerAdd(id);
            GridView1.DataBind();
        }
        protected void lnkSaveQuestion_Click(object sender, EventArgs e) {
            if(txtQuestion.Text.Trim().Length>0) {
                if (isNewQuestion) {
                    Database.CaptchaQuestionAdd(id);
                } else {
                    Database.CaptchaQuestionUpdate(id, txtQuestion.Text.Trim());
                }
            }
        }
}


}