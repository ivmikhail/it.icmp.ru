using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using ITCommunity.Validators;


namespace ITCommunity.Models {

    public class UserNickModel {

        [DisplayName("Ваш ник")]
        [Required(ErrorMessage = "Введите Ваш ник")]
        [UserNick]
        public string UserNick { get; set; }

    }
}
