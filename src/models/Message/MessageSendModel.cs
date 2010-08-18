using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using ITCommunity.Core;
using ITCommunity.Db;
using ITCommunity.Validators;
using ITCommunity.Db.Tables;


namespace ITCommunity.Models {

    public class MessageSendModel {

        [UserNick]
        [AnotherUserNick]
        [Required(ErrorMessage = "Введите ник получателя сообщения")]
        [DisplayName("Кому написать")]
        public string Receiver { get; set; }

        [Required(ErrorMessage = "Введите заголовок сообщения")]
        [DisplayName("Заголовок")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Введите текст сообщения")]
        [DisplayName("Текст сообщения")]
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
