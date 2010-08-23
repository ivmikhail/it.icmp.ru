using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using ITCommunity.Core;
using ITCommunity.DB;
using ITCommunity.Validators;
using ITCommunity.DB.Tables;


namespace ITCommunity.Models {

    public class MessageSendModel {

        [AnotherUserNick]
        [DisplayName("Кому написать")]
        [Required(ErrorMessage = "Введите ник получателя сообщения")]
        [UserNick]
        public string Receiver { get; set; }

        [DisplayName("Заголовок")]
        [Required(ErrorMessage = "Введите заголовок сообщения")]
        [StringLength(64, ErrorMessage = "Максимум можно написать 64 символа")]
        public string Title { get; set; }

        [DisplayName("Текст сообщения")]
        [Required(ErrorMessage = "Введите текст сообщения")]
        [StringLength(2048, ErrorMessage = "Максимум можно написать 2048 символа")]
        public string Text { get; set; }

        public MessageSendModel() {
            Title = "Без названия";
        }

        public Message ToMessage() {
            var message = new Message();

            message.Title = Title;
            message.Text = Text;
            message.SenderId = CurrentUser.User.Id;
            message.ReceiverId = Users.Get(Receiver).Id;

            return message;
        }
    }
}
