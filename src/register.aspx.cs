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
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (CurrentUser.isAuth)
            {
                RegUser.Visible = false;
                AboutRegister.Text = "<h2>Вы уже зарегистрированы и авторизовались</h2><p>Если вы все-таки хотите зарегистрироватсья по новой, тогда разлогинтесь</p>";
            } else
            {
                AboutRegister.Text = @"<h2>После регистрации Вы вас ждут следующие вкусности:</h2>
                                <ul type='square'>
                                    <li>Электронные книги</li>
                                    <li>Видео курсы</li>
                                    <li>Тесты</li>
                                    <li>Файлы</li>
                                    <li>Вы можете опубликовать свою новость</li>
                                <ul>";
            }
        }
    }
}