using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace ITCommunity.Models.User {
  
    public class LoginModel : NickModel {

        [Required(ErrorMessage = "Введите пароль")]
        [DisplayName("Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Запомнить?")]
        public bool RememberMe { get; set; }

    }
}
