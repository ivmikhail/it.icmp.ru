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
using System.Collections.Specialized;

namespace ITCommunity
{
	public partial class Pager : System.Web.UI.UserControl
	{
		public static string QueryName = "page";

		protected string PagePath;
		protected string QueryString;

		private int currentPage;
		private int totalRecords;
		private int recordsPerPage;
		private int totalPages;

		/// <summary>
		/// Обязательно надо вызвать данный метод
		/// </summary>
		/// <param name="totalRecords">Всего найденных записей</param>
		/// <param name="recordsPerPage"></param>
		public void DataBind(int totalRecords, int recordsPerPage)
		{
			PagePath = Request.Path;

			NameValueCollection queryString = new NameValueCollection(Request.QueryString);
			if (!int.TryParse(queryString[QueryName], out currentPage))
			{
				currentPage = 1;
			}
			queryString.Remove(QueryName);
			
			QueryString = "";
			for (int i = 0; i < queryString.Count; i++)
			{
				QueryString += queryString.Keys[i] + "=" + queryString[i] + "&";
			}
			
			this.totalRecords = totalRecords;
			this.recordsPerPage = recordsPerPage;

			if (recordsPerPage == 0)
			{
				totalPages = 1;
			}
			else
			{
				totalPages = Convert.ToInt32(Math.Ceiling((decimal)totalRecords / recordsPerPage));
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (totalPages > 1)
			{
				int start = (currentPage / 10) * 10 + 1;
				int end = start + 10;

				List<int> beforePagesList = new List<int>();
				for (int i = start; i < currentPage; i++)
				{
					beforePagesList.Add(i);
				}

				BeforePages.DataSource = beforePagesList;
				BeforePages.DataBind();

				CurrentPageText.Text = currentPage.ToString();

				List<int> afterPagesList = new List<int>();
				for (int i = currentPage + 1; (i < end) && (i <= totalPages); i++)
				{
					afterPagesList.Add(i);
				}
				AfterPages.DataSource = afterPagesList;
				AfterPages.DataBind();

				LoadPreviousPage();
				LoadNextPage();
			}
			else
			{
				PagerPanel.Visible = false;
			}
		}

		private void LoadPreviousPage()
		{
			if (currentPage == 1)
			{
				PreviousPageText.Text = "<li class='previous-off'>Предыдущая</li>";
			}
			else
			{
				PreviousPageText.Text = "<li class='previous'><a href='" + PagePath + "?" + QueryString + QueryName + "=" + (currentPage - 1) + "' title='Предыдущая страница'>Предыдущая</a></li>";
			}
		}

		private void LoadNextPage()
		{
			if (currentPage == totalPages)
			{
				NextPageText.Text = "<li class='next-off'>Следующая</li>";
			}
			else
			{
				NextPageText.Text = "<li class='next'><a href='" + PagePath + "?" + QueryString + QueryName + "=" + (currentPage + 1) + "' title='Следующая страница'>Следующая</a></li>";
			}
		}
	}
}
