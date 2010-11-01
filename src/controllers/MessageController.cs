using System;
using System.Web.Mvc;

using ITCommunity.Core;
using ITCommunity.DB.Tables;
using ITCommunity.Models;


namespace ITCommunity.Controllers {

    public class MessageController : BaseController {

        [Authorize]
        public ActionResult Send(string receiver) {
            var model = new MessageSendModel { Receiver = receiver };

            return View(model);
        }

        [Authorize]
        public ActionResult Reply(int? id) {
            var message = Messages.Get(id.Value);

            if (message == null) {
                return NotFound();
            }

            var model = new MessageSendModel { 
                Receiver = message.Sender.Nick, 
                Title = "RE: " + message.Title
            };

            return View("Send", model);
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
        public ActionResult ReceivedList(int? page) {
            var model = new MessageReceivedListModel(page);

            Messages.ReadAllUnread(CurrentUser.User.Id);

            return View(model);
        }

        [Authorize]
        public ActionResult SentList(int? page) {
            var model = new MessageSentListModel(page);

            return View(model);
        }

        [Authorize]
        public ActionResult Delete(int? id) {
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
        public ActionResult DeleteAll(string messages) {
            if (messages.EndsWith("received", StringComparison.CurrentCultureIgnoreCase)) {
                Messages.SetDeletedAllReceived(CurrentUser.User.Id);
            } else if (messages.EndsWith("sent", StringComparison.CurrentCultureIgnoreCase)) {
                Messages.SetDeletedAllSent(CurrentUser.User.Id);
            } else {
                return NotFound();
            }

            return Redirect();
        }
    }
}
