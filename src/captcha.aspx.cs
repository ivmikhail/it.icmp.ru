using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Diagnostics;

public partial class captcha : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {
        Debug.Print("load");
    }
    protected void btnCheck_Click(object sender, EventArgs e) {
        if (ucItCaptcha.IsRightAnswer()) {
            lblCheck.Text = "Да";
            ucItCaptcha.Visible = false;
        } else {
            lblCheck.Text = "Дуррак";
            ucItCaptcha.SetErrorMessageVisible();
        }
    }
}
