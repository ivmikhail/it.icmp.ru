using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using ITCommunity.DB;
using ITCommunity.DB.Tables;


namespace ITCommunity.Models {

    public class CategoryEditModel {

        [DisplayName("Название категории")]
        [Required(ErrorMessage = "Введите название категории")]
        public string Name { get; set; }

        [DisplayName("Порядок сортировки")]
        [Required(ErrorMessage = "Введите порядок сортировки")]
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
