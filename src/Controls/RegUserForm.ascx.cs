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

public partial class RegUserForm : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["ReturnUrl"] != null)
            {
                ViewState["returnUrl"] = Uri.UnescapeDataString(Request.QueryString["ReturnUrl"]);
            } else if (HttpContext.Current.Request.UrlReferrer == null)
            {
                ViewState["returnUrl"] = FormsAuthentication.LoginUrl;
            } else
            {
                ViewState["returnUrl"] = HttpContext.Current.Request.UrlReferrer;
            }
        }
    }
    protected void RegisterButton_Click(object sender, EventArgs e)
    {
        if (!AllIsValid())
        {
            return;
        }

        User user = CurrentUser.Register(TextBoxLogin.Text.Trim(), TextBoxPass.Text, TextBoxEmail.Text.Trim().ToLower());

        if (user.Id > 0)
        {
            Response.Redirect(ViewState["returnUrl"].ToString());
        } else
        {
            RegisterFailed.IsValid = false;
        }
    }

    bool AllIsValid()
    {
        if (!RegIsValid())
        {
            return false;
        }
        if (TextBoxPass.Text != TextBoxPassConf.Text)
        {
            ConfirmPassword.IsValid = false;
            return false;
        }
        return LoginIsValid();
    }

    bool RegIsValid()
    {
        RequiredLogin.Validate();
        LoginValidator.Validate();
        RequiredEmail.Validate();
        EmailValidator.Validate();
        RequiredPass.Validate();
        ConfirmPassword.Validate();

        return  RequiredLogin.IsValid  &&
                LoginValidator.IsValid &&
                RequiredEmail.IsValid  &&
                EmailValidator.IsValid &&
                ConfirmPassword.IsValid;
    }

    bool LoginIsValid()
    {
        bool status = true;

        string login = TextBoxLogin.Text.Trim();
        User user = Users.GetUserByLogin(login);
        if (user.Id > 0)
        {
            status = AccountExist.IsValid = false;
        }

        return status;
    }
}
