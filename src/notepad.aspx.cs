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

namespace ITCommunity
{

    public partial class Notepad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int delete_id = GetDel();
            if (delete_id > 0)
            {
                Note.Delete(delete_id);
            }
            int records_count = 0;
            RepeaterNotes.DataSource = Note.Get(GetPage(), Global.ConfigNumParam("MaxNotesCount"), CurrentUser.User.Id, ref records_count);
            RepeaterNotes.DataBind();
            NotesPager.DataBind("notepad.aspx", "", "page", GetPage(), records_count, Global.ConfigNumParam("MaxNotesCount"));
        }
        private int GetPage()
        {
            int page_num;
            Int32.TryParse(Request.QueryString["page"], out page_num);
            return page_num == 0 ? 1 : page_num;
        }
        private int GetDel()
        {
            int id;
            Int32.TryParse(Request.QueryString["del"], out id);
            return id;
        }
    }
}