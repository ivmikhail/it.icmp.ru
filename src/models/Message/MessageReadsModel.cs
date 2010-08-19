using System.Collections.Generic;

using ITCommunity.Core;
using ITCommunity.Db;
using ITCommunity.Db.Tables;


namespace ITCommunity.Models {

    public class MessageReadListModel : MessageListModel {

        public MessageReadListModel(int? page)
            : base(page) {
        }

        protected override List<Message> GetMessages() {
            return Messages.GetRead(CurrentUser.User.Id, Page, PerPage, ref TotalCount);
        }
    }
}
