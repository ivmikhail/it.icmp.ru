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

namespace ITCommunity
{
    public partial class AccessDenyPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LiteralMessage.Text = "Для получения доступа попробуйте авторизоваться слева или <a href='register.aspx' title='Зарегистрировать нового пользователя'>зарегистрироваться</a>.<br /><br /> Возможно вы и не должны иметь доступ к этой странице...";
                // На всякий случай, забаненный не может авторизоваться
                if (CurrentUser.User.Role == ITCommunity.User.Roles.Banned)
                {
                    LiteralMessage.Text = "Ваш аккаунт заблокирован(забанен)";
                }
            }

        }
    }
}
