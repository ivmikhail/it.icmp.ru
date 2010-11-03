using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using ITCommunity.Validators;


namespace ITCommunity.Models {

    [PropertiesMustMatch("Password", "ConfirmPassword", ErrorMessage = "Пароли не совпадают")]
    public class UserRegisterModel : CaptchaModel {

        [DisplayName("Ваш ник")]
        [Required(ErrorMessage = "Введите Ваш ник")]
        [UniqueNick]
        [RegularExpression(@"^[A-Za-z0-9_\-\.]{2,20}$", ErrorMessage = "Плохой ник, выберите другой")]
        public string UserNick { get; set; }

        [DisplayName("Email адрес")]
        [Email]
        [Required(ErrorMessage = "Введите e-mail")]
        [UniqueEmail]
        public string Email { get; set; }

        [DisplayName("Пароль")]
        [Required(ErrorMessage = "Введите пароль")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Введите хотя бы 3 символа")]
        public string Password { get; set; }

        [DisplayName("Повтор пароля")]
        [Required(ErrorMessage = "Введите повтор пароля")]
        public string ConfirmPassword { get; set; }

    }
}
