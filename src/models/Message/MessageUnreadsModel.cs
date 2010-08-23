using System.Collections.Generic;

using ITCommunity.Core;
using ITCommunity.DB;
using ITCommunity.DB.Tables;


namespace ITCommunity.Models {

    public class MessageUnreadListModel : MessageListModel {

        public MessageUnreadListModel(int? page)
            : base(page) {
        }

        protected override List<Message> GetMessages() {
            return Messages.GetUnread(CurrentUser.User.Id, Page, PerPage, ref TotalCount);
        }
    }
}
