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

public partial class Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (CurrentUser.isAuth)
        {
            RegUser.Visible = false;
            AboutRegister.Text = "Вы уже зарегистрированы.";
        } else
        {
            AboutRegister.Text = @"<h2>При регистрации Вы получите доступ к следующим разделам сайта:</h2>
                                <ul type='square'>
                                    <li>Электронные книги</li>
                                    <li>Видео курсы</li>
                                    <li>Тесты</li>
                                    <li>Файлы</li>
                                    <li>Появится возможность производить поиск по разделам</li>
                                    <li>Оставлять комменты к новостям</li>
                                    <li>Оставлять отзывы</li>
                                <ul>";
        }

    }
}
