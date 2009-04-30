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



        /// <summary>
        /// Идентификатор категории, если не нужно то ничего не присваиваем.
        /// </summary>
        public int CurrentPage
        {
            get
            {
                return current_page;
            }
            set
            {
                current_page = value;
            }
        }
        /// <summary>
        /// Всего страниц
        /// </summary>
        public int TotalPages
        {
            get
            {
                return total_pages;
            }
            set
            {
                total_pages = value;
            }
        }
        /// <summary>
        /// Параметр в урле, например page(page=1)
        /// </summary>
        public string PageQueryString
        {
            get
            {
                return query_string;
            }
            set
            {
                query_string = value;
            }
        }
        /// <summary>
        /// Страница на которой находится пейджер, например default.aspx
        /// </summary>
        public string PagerPage
        {
            get
            {
                return pager_page;
            }
            set
            {
                pager_page = value;
            }
        }
        /// <summary>
        /// GET параметры страницы на которой находится пейджер, будут прицеплены в конце урла. В формате &cat_id=9&jopa=true
        /// </summary>
        public string PageParams
        {
            get
            {
                return page_params;
            }
            set
            {
                page_params = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
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