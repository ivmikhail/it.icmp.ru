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
	public partial class EditorToolbar : System.Web.UI.UserControl
    {
        private string inputId;

        public string InputId {
            get {
                return inputId; 
            }
            set {
                inputId = value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                Input.Text = inputId;
            }
        }
    }
}
