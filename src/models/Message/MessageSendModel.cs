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
        [DataType(DataType.Text)]
        [StringLength(64, ErrorMessage = "Максимум можно написать 64 символа")]
        [DisplayName("Заголовок")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Введите текст сообщения")]
        [StringLength(2048, ErrorMessage = "Максимум можно написать 2048 символа")]
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
