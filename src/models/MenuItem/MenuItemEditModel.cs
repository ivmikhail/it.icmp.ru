using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using ITCommunity.DB;
using ITCommunity.DB.Tables;


namespace ITCommunity.Models {

    public class MenuItemEditModel {

        [DisplayName("Родительский пункт")]
        public int? ParentId { get; set; }

        [DisplayName("Название ссылки")]
        [Required(ErrorMessage = "Введите название ссылки")]
        public string Name { get; set; }

        [DisplayName("URL ссылки")]
        [Required(ErrorMessage = "Введите URL ссылки")]
        public string Url { get; set; }

        [DisplayName("Порядок сортировки")]
        [Required(ErrorMessage = "Введите порядок сортировки")]
        public int Sort { get; set; }
        public bool IsTargetBlank { get; set; }

        public MenuItemEditModel() {
            ParentId = 0;
            Url = "none";
            Sort = 0;
            IsTargetBlank = true;
        }

        public MenuItemEditModel(int id) {
            var item = MenuItems.Get(id);

            ParentId = item.ParentId;
            Name = item.Name;
            Url = item.Url;
            Sort = item.Sort;
            IsTargetBlank = item.IsTargetBlank;
        }

        public MenuItem ToMenuItem() {
            var item = new MenuItem();

            item.ParentId = (ParentId == null) ? 0 : ParentId.Value;
            item.Name = Name;
            item.Url = Url;
            item.Sort = Sort;
            item.IsTargetBlank = IsTargetBlank;

            return item;
        }

        public static List<SelectListItem> ParentIds {
            get {
                var parentIds = new List<SelectListItem>();
                foreach (var parent in MenuItems.GetRoot()) {
                    parentIds.Add(new SelectListItem { 
                        Text = parent.Name,
                        Value = parent.Id.ToString() 
                    });
                }
                return parentIds;
            }
        }
    }
}
