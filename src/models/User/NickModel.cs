using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace ITCommunity.Models.User {

    public class NickModel {

        [Required(ErrorMessage = "Введите Ваш ник")]
        [DisplayName("Ваш ник")]
        public string UserNick { get; set; }

    }
}
