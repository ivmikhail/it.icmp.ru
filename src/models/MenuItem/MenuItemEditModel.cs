﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using ITCommunity.DB;
using ITCommunity.DB.Tables;
using System.Collections.Generic;
using System.Web.Mvc;


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

            item.ParentId = (ParentId == null) ? 0 : ParentId.Value;
            item.Name = Name;
            item.Url = Url;
            item.Sort = Sort;

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
