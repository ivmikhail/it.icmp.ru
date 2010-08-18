using System.Collections.Generic;

using ITCommunity.Core;
using ITCommunity.Db;
using ITCommunity.Db.Tables;


namespace ITCommunity.Models {

    public class MessageUnreadsModel : MessageListModel {

        public MessageUnreadsModel(int? page)
            : base(page) {
        }

        protected override List<Message> GetMessages() {
            return Messages.GetUnreads(CurrentUser.User.Id, Page, PerPage, ref TotalCount);
        }
    }
}
