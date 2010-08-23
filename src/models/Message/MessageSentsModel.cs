using System.Collections.Generic;

using ITCommunity.Core;
using ITCommunity.DB;
using ITCommunity.DB.Tables;


namespace ITCommunity.Models {

    public class MessageSentListModel : MessageListModel {

        public MessageSentListModel(int? page)
            : base(page) {
        }

        protected override List<Message> GetMessages() {
            return Messages.GetSent(CurrentUser.User.Id, Page, PerPage, ref TotalCount);
        }
    }
}
