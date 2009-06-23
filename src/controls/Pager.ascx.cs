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

    public partial class Pager : System.Web.UI.UserControl
    {
        private int total_pages;
        private string query_string;
        private string pager_page;
        private string page_params;
        private int current_page;
        private int total_records;
        private int records_per_page;
        
        //TODO: наверное надо очеловечить инициализацию
        /// <summary>
        /// Обязательно надо вызвать данный метод
        /// </summary>
        /// <param name="page">Страница на которой находится пейджер, например default.aspx</param>
        /// <param name="page_params">GET параметры страницы на которой находится пейджер, будут прицеплены в конце урла. В формате &cat_id=9&jopa=true</param>
        /// <param name="query">Параметр в урле, например page(page=1)</param>
        /// <param name="current_page">Номер текущей страницы</param>
        /// <param name="total_records">Всего найденных записей</param>
        /// <param name="records_per_page"></param>
        public void Fill(string page, string page_params, string query, int current_page, int total_records, int records_per_page)
        {
            this.pager_page = page;
            this.page_params = page_params;
            this.query_string = query;
            this.current_page = current_page;
            this.total_records = total_records;
            this.records_per_page = records_per_page;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (records_per_page == 0)
            {
                total_pages = 1;
            } else
            {
                total_pages = Convert.ToInt32(Math.Ceiling((decimal)total_records / records_per_page));
            }
            
            if (total_pages > 1)
            {
                Links.Text = "<ul class='pager'>" + PrevLink() + Pages() + NextLink() + "</ul>";
            } else
            {
                Links.Text = "";
            }

        }
        private string Pages()
        {
            int start = (current_page / 10) * 10;
            if (start != 0)
            {
                start -= 1;
            }
            int end = start + 10;
            string result = String.Empty;
            while (start < end)
            {
                if (start == total_pages)
                {
                    break;
                }
                if (result.Length > 0)
                {
                    result += "";
                }
                int page = start + 1;
                if (current_page == page)
                {
                    result += "<li class='active'> " + page + "</li>";
                } else
                {
                    result += "<li><a href='" + pager_page + "?" + query_string + "=" + page + page_params + "' title='Страница " + page + "'>" + page + "</a></li>";
                }
                start++;
            }
            return result;
        }

        private string PrevLink()
        {
            string previous_link;
            if (current_page == 1)
            {
                previous_link = "<li class='previous-off'>Предыдущая</li>";
            } else
            {
                previous_link = "<li class='previous'><a href='" + pager_page + "?" + query_string + "=" + (current_page - 1) + page_params + "' title='Предыдущая страница'>Предыдущая</a></li>";

            }
            return previous_link;
        }

        private string NextLink()
        {
            string next_link;
            if (current_page == total_pages)
            {
                next_link = "<li class='next-off'>Следующая</li>";
            } else
            {
                next_link = "<li class='next'><a href='" + pager_page + "?" + query_string + "=" + (current_page + 1) + page_params + "' title='Следующая страница'>Следующая</a></li>";

            }
            return next_link;
        }
    }
}