using System.Web.Mvc;

using ITCommunity.Core;
using ITCommunity.Db;
using ITCommunity.Db.Tables;
using ITCommunity.Models;
using System;


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

            return Redirect(Request.UrlReferrer.ToString());
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
        public ActionResult Unreads(int? page) {
            var model = new MessageUnreadsModel(page);

            return View(model);
        }

        [Authorize]
        public ActionResult Reads(int? page) {
            var model = new MessageReadsModel(page);

            return View(model);
        }

        [Authorize]
        public ActionResult Sents(int? page) {
            var model = new MessageSentsModel(page);

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

            return Redirect(Request.UrlReferrer.ToString());
        }

        [Authorize]
        public ActionResult ReadAll() {
            Messages.ReadAllUnreads(CurrentUser.User.Id);

            return Redirect(Request.UrlReferrer.ToString());
        }

        [Authorize]
        public ActionResult DeleteAll(string messages) {
            if (messages.EndsWith("unreads", StringComparison.CurrentCultureIgnoreCase)) {
                Messages.SetDeletedAllUnreads(CurrentUser.User.Id);
            } else if (messages.EndsWith("reads", StringComparison.CurrentCultureIgnoreCase)) {
                Messages.SetDeletedAllReads(CurrentUser.User.Id);
            } else if (messages.EndsWith("sents", StringComparison.CurrentCultureIgnoreCase)) {
                Messages.SetDeletedAllSents(CurrentUser.User.Id);
            } else {
                return NotFound();
            }

            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}
