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
using ITCommunity;
using System.Text.RegularExpressions;

namespace ITCommunity
{

    public partial class RfcPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        private void LoadRfc(string query)
        {
            /*
            if (query.Length > 4)
            {
                query = query.Substring(0, 4);
            }
             */
            List<Rfc> finded;
            if (Regex.IsMatch(query, "[0-9]+") && query.Length <= 4)
            {
                finded = Rfc.GetByNum(query);
            } else
            {
                finded = Rfc.Search(query);
            }
            if (finded.Capacity > 0)
            {
                NotFoundText.Visible = false;
            } else
            {
                NotFoundText.Visible = true;
            }

            RepeaterRfc.DataSource = finded;
            RepeaterRfc.DataBind();

        }
        protected void LinkButtonSearch_Click(object sender, EventArgs e)
        {
            string query = TextBoxSearch.Text;
            LoadRfc(query);
        }

        public static string FormURL(string number)
        {
            if(number.StartsWith("000"))
            {
                number = number.Substring(3);
            } else if (number.StartsWith("00"))
            {
                number = number.Substring(2);
            } else if (number.StartsWith("0")) 
            {
                number = number.Substring(1);
            }

            return "/" + Global.ConfigStringParam("RfcFolder") + "/rfc" + number + ".txt";
        }
    }
}