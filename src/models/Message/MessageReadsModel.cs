using System.Collections.Generic;

using ITCommunity.Core;
using ITCommunity.Db;
using ITCommunity.Db.Tables;


namespace ITCommunity.Models {

    public class MessageReadsModel : MessageListModel {

        public MessageReadsModel(int? page)
            : base(page) {
        }

        protected override List<Message> GetMessages() {
            return Messages.GetReads(CurrentUser.User.Id, Page, PerPage, ref TotalCount);
        }
    }
}
