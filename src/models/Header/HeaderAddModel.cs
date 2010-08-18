using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

using ITCommunity.Core;
using ITCommunity.Db;
using ITCommunity.Validators;


namespace ITCommunity.Models {

    public class HeaderAddModel {

        [Required(ErrorMessage = "Введите текст хидера")]
        [HeaderText]
        [DisplayName("Текст хидера")]
        public string Text { get; set; }

        public Header ToHeader() {
            var header = new Header();

            header.Text =  HttpUtility.HtmlEncode(Text);
            header.EndDate = DateTime.Now.AddHours(Header.ShowingHours);
            header.UserId = CurrentUser.User.Id;

            return header;
        }
    }
}
