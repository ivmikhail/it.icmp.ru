using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

using ITCommunity.Core;
using ITCommunity.DB;
using ITCommunity.Validators;


namespace ITCommunity.Models {

    public class HeaderAddModel {

        [DisplayName("Текст хидера")]
        [HeaderText]
        [Required(ErrorMessage = "Введите текст хидера")]
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
