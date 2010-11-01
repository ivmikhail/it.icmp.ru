using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using ITCommunity.Core;
using ITCommunity.DB;
using ITCommunity.DB.Tables;
using ITCommunity.Validators;


namespace ITCommunity.Models {

    [PropertiesMustMatch("Password", "ConfirmPassword", ErrorMessage = "Пароли не совпадают")]
    public class UserEditModel {

        [DisplayName("e-mail адрес")]
        [Required(ErrorMessage = "Введите e-mail")]
        [UniqueEmail]
        public string Email { get; set; }

        [DisplayName("Новый пароль")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Введите хотя бы 3 символа")]
        public string Password { get; set; }

        [DisplayName("Повтор пароля")]
        public string ConfirmPassword { get; set; }

        [CurrentUserPassword]
        [DisplayName("Текущий пароль")]
        [Required(ErrorMessage = "Чтобы сохранить изменения введите ваш текущий пароль")]
        public string OldPassword { get; set; }

        public UserEditModel() {
            var user = Users.Get(CurrentUser.User.Id);
            Email = user.Email;
        }

        public User ToUser() {
            var user = Users.Get(CurrentUser.User.Id);

            if (String.IsNullOrEmpty(Email) == false) {
                user.Email = Email;
            }

            if (String.IsNullOrEmpty(Password) == false) {
                user.Password = CurrentUser.HashPassword(Password, user.Nick);
            }

            return user;
        }
    }
}
