using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace ITCommunity.Models {

    public class UserLoginModel : UserNickModel {

        [DisplayName("Пароль")]
        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

    }
}
