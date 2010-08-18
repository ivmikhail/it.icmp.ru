using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using ITCommunity.Db;
using ITCommunity.Db.Tables;


namespace ITCommunity.Models {

    public class CategoryEditModel {

        [Required(ErrorMessage = "Введите название категории")]
        [DisplayName("Название категории")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите порядок сортировки")]
        [DisplayName("Порядок сортировки")]
        public int Sort { get; set; }

        public CategoryEditModel() {
        }

        public CategoryEditModel(int id) {
            var category = Categories.Get(id);

            Name = category.Name;
            Sort = category.Sort;
        }

        public Category ToCategory() {
            var category = new Category();

            category.Name = Name;
            category.Sort = Sort;

            return category;
        }
    }
}
