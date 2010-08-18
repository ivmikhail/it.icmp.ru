using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using ITCommunity.Db;
using ITCommunity.Db.Tables;


namespace ITCommunity.Models {

    public class MenuItemEditModel {

        [Required(ErrorMessage = "Введите Id родидельской ссылки")]
        [DisplayName("Id родидельской ссылки")]
        public int ParentId { get; set; }

        [Required(ErrorMessage = "Введите название ссылки")]
        [DisplayName("Название ссылки")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите URL ссылки")]
        [DisplayName("URL ссылки")]
        public string Url { get; set; }

        [Required(ErrorMessage = "Введите порядок сортировки")]
        [DisplayName("Порядок сортировки")]
        public int Sort { get; set; }

        public MenuItemEditModel() {
            ParentId = 0;
            Url = "none";
            Sort = 0;
        }

        public MenuItemEditModel(int id) {
            var item = MenuItems.Get(id);

            ParentId = item.ParentId;
            Name = item.Name;
            Url = item.Url;
            Sort = item.Sort;
        }

        public MenuItem ToMenuItem() {
            var item = new MenuItem();

            item.ParentId = ParentId;
            item.Name = Name;
            item.Url = Url;
            item.Sort = Sort;

            return item;
        }
    }
}
