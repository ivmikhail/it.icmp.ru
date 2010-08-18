using System.Collections.Generic;

using ITCommunity.Core;
using ITCommunity.Db;
using ITCommunity.Db.Tables;


namespace ITCommunity.Models {

    public class MessageSentsModel : MessageListModel {

        public MessageSentsModel(int? page)
            : base(page) {
        }

        protected override List<Message> GetMessages() {
            return Messages.GetSents(CurrentUser.User.Id, Page, PerPage, ref TotalCount);
        }
    }
}
