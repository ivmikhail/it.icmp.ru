using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using ITCommunity.Validators;


namespace ITCommunity.Models.User {

    [PropertiesMustMatch("Password", "ConfirmPassword", ErrorMessage = "Пароли не совпадают")]
    public class NewPasswordModel : NickModel {

        [Required(ErrorMessage = "Введите новый пароль")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Введите хотя бы 3 символа")]
        [DisplayName("Новый пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Введите повтор пароля")]
        [DisplayName("Повтор паролья")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }

}
