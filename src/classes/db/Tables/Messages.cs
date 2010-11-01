using System;
using System.Collections.Generic;
using System.Linq;

using ITCommunity.Core;


namespace ITCommunity.DB.Tables {

    public static class Messages {

        public static Message Add(Message message) {
            using (var db = Database.Connect()) {
                db.Messages.InsertOnSubmit(message);
                db.SubmitChanges();

                return message;
            }
        }

        public static void UpdateStatuses(Message editedMessage) {
            using (var db = Database.Connect()) {
                var message = (
                    from msg in db.Messages
                    where msg.Id == editedMessage.Id
                    select msg
                ).SingleOrDefault();

                message.DeletedForReceiver = editedMessage.DeletedForReceiver;
                message.DeletedForSender = editedMessage.DeletedForSender;
                message.IsReceiverRead = editedMessage.IsReceiverRead;

                db.SubmitChanges();
            }
        }

        public static Message Get(int id) {
            using (var db = Database.Connect()) {
                var message = (
                    from msg in db.Messages
                    where msg.Id == id
                    select msg
                ).SingleOrDefault();

                return message;
            }
        }

        public static List<Message> GetPagedReceived(int userId, int page, int count, ref int totalCount) {
            using (var db = Database.Connect()) {
                var messages =
                    from msg in db.Messages
                    where 
                        msg.ReceiverId == userId &&
                        msg.DeletedForReceiver == false
                    orderby msg.CreateDate descending
                    select msg;

                return messages.Paged(page, count, ref totalCount);
            }
        }

        public static int GetUnreadCount(int userId) {
            using (var db = Database.Connect()) {
                var messages =
                    from msg in db.Messages
                    where
                        msg.ReceiverId == userId &&
                        msg.DeletedForReceiver == false &&
                        msg.IsReceiverRead == false
                    select msg;

                return messages.Count();
            }
        }

        public static List<Message> GetPagedSent(int userId, int page, int count, ref int totalCount) {
            using (var db = Database.Connect()) {
                var messages =
                    from msg in db.Messages
                    where 
                        msg.SenderId == userId &&
                        msg.DeletedForSender == false
                    orderby msg.CreateDate descending
                    select msg;

                return messages.Paged(page, count, ref totalCount);
            }
        }

        public static void SetDeletedAllReceived(int userId) {
            using (var db = Database.Connect()) {
                var messages =
                    from msg in db.Messages
                    where
                        msg.ReceiverId == userId &&
                        msg.DeletedForReceiver == false
                    orderby msg.CreateDate descending
                    select msg;

                foreach (var message in messages) {
                    message.DeletedForReceiver = true;
                }

                db.SubmitChanges();
            }
        }

        public static void ReadAllUnread(int userId) {
            using (var db = Database.Connect()) {
                var messages =
                    from msg in db.Messages
                    where
                        msg.ReceiverId == userId &&
                        msg.DeletedForReceiver == false &&
                        msg.IsReceiverRead == false
                    orderby msg.CreateDate descending
                    select msg;

                foreach (var message in messages) {
                    message.IsReceiverRead = true;
                }

                db.SubmitChanges();
            }
        }

        public static void SetDeletedAllSent(int userId) {
            using (var db = Database.Connect()) {
                var messages =
                    from msg in db.Messages
                    where
                        msg.SenderId == userId &&
                        msg.DeletedForSender == false
                    orderby msg.CreateDate descending
                    select msg;

                foreach (var message in messages) {
                    message.DeletedForSender = true;
                }

                db.SubmitChanges();
            }
        }
    }
}
