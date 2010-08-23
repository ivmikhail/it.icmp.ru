using System;
using System.Web.Mvc;

using ITCommunity.Core;
using ITCommunity.DB.Tables;
using ITCommunity.Models;


namespace ITCommunity.Controllers {

    public class MessageController : BaseController {

        [Authorize]
        public ActionResult Read(int? id) {
            if (id == null) {
                return NotFound();
            }

            var message = Messages.Get(id.Value);

            if (message == null) {
                return NotFound();
            }

            message.IsReceiverRead = true;
            Messages.UpdateStatuses(message);

            return Redirect();
        }

        [Authorize]
        public ActionResult Send(string receiver) {
            var model = new MessageSendModel { Receiver = receiver };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Send(MessageSendModel model) {
            if (ModelState.IsValid) {
                var message = model.ToMessage();

                Messages.Add(message);

                return View("Sent");
            }

            return View(model);
        }

        [Authorize]
        public ActionResult UnreadList(int? page) {
            var model = new MessageUnreadListModel(page);

            return View(model);
        }

        [Authorize]
        public ActionResult ReadList(int? page) {
            var model = new MessageReadListModel(page);

            return View(model);
        }

        [Authorize]
        public ActionResult SentList(int? page) {
            var model = new MessageSentListModel(page);

            return View(model);
        }

        [Authorize]
        public ActionResult Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var message = Messages.Get(id.Value);

            if (message == null) {
                return NotFound();
            }

            if (message.SenderId == CurrentUser.User.Id) {
                message.DeletedForSender = true;
            } else {
                message.DeletedForReceiver = true;
            }

            Messages.UpdateStatuses(message);

            return Redirect();
        }

        [Authorize]
        public ActionResult ReadAll() {
            Messages.ReadAllUnread(CurrentUser.User.Id);

            return Redirect();
        }

        [Authorize]
        public ActionResult DeleteAll(string messages) {
            if (messages.EndsWith("unread", StringComparison.CurrentCultureIgnoreCase)) {
                Messages.SetDeletedAllUnread(CurrentUser.User.Id);
            } else if (messages.EndsWith("read", StringComparison.CurrentCultureIgnoreCase)) {
                Messages.SetDeletedAllRead(CurrentUser.User.Id);
            } else if (messages.EndsWith("sent", StringComparison.CurrentCultureIgnoreCase)) {
                Messages.SetDeletedAllSent(CurrentUser.User.Id);
            } else {
                return NotFound();
            }

            return Redirect();
        }
    }
}
