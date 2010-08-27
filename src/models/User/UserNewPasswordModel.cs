using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using ITCommunity.DB;
using ITCommunity.Validators;


namespace ITCommunity.Models {

    [PropertiesMustMatch("Password", "ConfirmPassword", ErrorMessage = "Пароли не совпадают")]
    public class UserNewPasswordModel : UserNickModel {

        [DisplayName("Новый пароль")]
        [Required(ErrorMessage = "Введите новый пароль")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Введите хотя бы 3 символа")]
        public string Password { get; set; }

        [DisplayName("Повтор пароля")]
        [Required(ErrorMessage = "Введите повтор пароля")]
        public string ConfirmPassword { get; set; }

        public UserNewPasswordModel() : base() {
        }
        public UserNewPasswordModel(Recovery recovery) {
            UserNick = recovery.User.Nick;
        }
    }
}
