using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using ITCommunity.DB;
using ITCommunity.DB.Tables;


namespace ITCommunity.Models {

    public class RssEditModel {

        [DisplayName("Название rss-ки")]
        [Required(ErrorMessage = "Введите название rss-ки")]
        public string Title { get; set; }

        [DisplayName("Url для загрузки rss-ки")]
        [Required(ErrorMessage = "Введите название rss-ки")]
        public string Uri { get; set; }

        [DisplayName("Порядок сортировки")]
        [Required(ErrorMessage = "Введите порядок сортировки")]
        public int Sort { get; set; }


        public RssEditModel() {
        }

        public RssEditModel(int id) {
            var rss = Rsses.Get(id);

            Title = rss.Title;
            Uri = rss.Uri;
            Sort = rss.Sort;
        }

        public Rss ToRss() {
            var rss = new Rss();

            rss.Title = Title;
            rss.Uri = Uri;
            rss.Sort = Sort;

            return rss;
        }
    }
}
