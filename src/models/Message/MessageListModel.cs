using System;
using System.Collections.Generic;

using ITCommunity.Core;
using ITCommunity.DB;
using ITCommunity.DB.Tables;


namespace ITCommunity.Models {

    public abstract class MessageListModel : PaginatedModel {

        public List<Message> List {
            get;
            private set;
        }

        public MessageListModel(int? page)
            : base(page) {
            PerPage = Config.MessagesPerPage;
            List = GetMessages();
        }

        protected virtual List<Message> GetMessages() {
            throw new NotImplementedException();
        }
    }
}
