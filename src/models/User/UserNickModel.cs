using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using ITCommunity.Validators;


namespace ITCommunity.Models {

    public class UserNickModel {

        [UserNick]
        [Required(ErrorMessage = "Введите Ваш ник")]
        [DisplayName("Ваш ник")]
        public string UserNick { get; set; }

    }
}
