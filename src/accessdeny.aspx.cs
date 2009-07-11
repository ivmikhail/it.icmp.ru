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
                LiteralMessage.Text = "��� ��������� ������� ���������� �������������� ����� ��� <a href='register.aspx' title='���������������� ������ ������������'>������������������</a>.<br /><br /> �������� �� � �� ������ ����� ������ � ���� ��������...";
                // �� ������ ������, ���������� �� ����� ��������������
                if (CurrentUser.User.Role == ITCommunity.User.Roles.Banned)
                {
                    LiteralMessage.Text = "��� ������� ������������(�������)";
                }
            }

        }
    }
}
