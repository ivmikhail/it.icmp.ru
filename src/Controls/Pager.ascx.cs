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

public partial class Pager : System.Web.UI.UserControl
{
    private int total_pages;
    private string query_string;
    private int cat_id;
    private string pager_page;
    private int current_page;



    /// <summary>
    /// Идентификатор категории, если не нужно то ничего не присваиваем.
    /// </summary>
    public int CurrrentPage
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
    /// Идентификатор категории, если не нужно то ничего не присваиваем.
    /// </summary>
    public int CatId
    {
        get
        {
            return cat_id;
        }
        set
        {
            cat_id = value;
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
    /// Страница на котороый находится пейджер, например default.aspx
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
    protected void Page_Load(object sender, EventArgs e)
    {

        Links.Text = "<ul class='pager'>" + PrevLink() + Pages() + NextLink() + "</ul>";
     
    }
    private string Pages()
    {
        string result = String.Empty;
        for (int i = 0; i < total_pages; i++)
        {
            if (result.Length > 0)
            {
                result += "";
            }
            int page = i + 1;
            if (current_page == page)
            {
                result += "<li class='active'> " + page + "</li>";
            } else
            {
                result += "<li><a href='" + pager_page + "?" + query_string + "=" + page + CategoryParam() + "' title='Страница " + page + "'>" + page + "</a></li>";
            }
        }
        return result;
    }

    private string CategoryParam()
    {
        string cat = String.Empty;
        if (cat_id > 0)
        {
            cat = "&cat=" + cat_id;
        }
        return cat;
    }
    private string PrevLink()
    {
        string previous_link;
        if (current_page < 11)
        {
            previous_link = "<li class='previous-off'>Предыдущие</li>";
        } else
        {
            previous_link = "<li class='previous'><a href='" + pager_page + "?" + query_string + "=" + (current_page - 10) + CategoryParam() + "' title='Предыдущие 10 страниц'>Предыдущие</a></li>";

        }
        return previous_link;
    }

    private string NextLink()
    {
        string next_link;
        if (current_page > 9 && current_page <= total_pages)
        {
            next_link = "<li class='next-off'>Следующие</li>";
        } else
        {
            next_link = "<li class='next'><a href='" + pager_page + "?" + query_string + "=" + (current_page + 10) + CategoryParam() + "' title='Следующие 10 страниц'>Следующие</a></li>";

        }
        return next_link;
    }
}
