using System.Collections.Generic;

using ITCommunity.Core;
using ITCommunity.DB;
using ITCommunity.DB.Tables;


namespace ITCommunity.Models {

    public class MessageReceivedListModel : MessageListModel {

        public MessageReceivedListModel(int? page)
            : base(page) {
        }

        protected override List<Message> GetMessages() {
            return Messages.GetPagedReceived(CurrentUser.User.Id, Page, PerPage, ref TotalCount);
        }
    }
}
