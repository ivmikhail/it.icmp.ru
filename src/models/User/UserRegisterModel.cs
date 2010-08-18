using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using ITCommunity.Validators;


namespace ITCommunity.Models {

    [PropertiesMustMatch("Password", "ConfirmPassword", ErrorMessage = "Пароли не совпадают")]
    public class UserRegisterModel : CaptchaModel {

        [UniqueNick]
        [Required(ErrorMessage = "Введите Ваш ник")]
        [DisplayName("Ваш ник")]
        public string UserNick { get; set; }

        [Required(ErrorMessage = "Введите e-mail")]
        [DisplayName("Email адресс")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Введите хотя бы 3 символа")]
        [DisplayName("Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Введите повтор пароля")]
        [DisplayName("Повтор паролья")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }
}
