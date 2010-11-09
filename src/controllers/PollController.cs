using System.Drawing;
using System.IO;
using System.Web.Mvc;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

using ITCommunity.Core;
using ITCommunity.DB;
using ITCommunity.DB.Tables;
using ITCommunity.Models;


namespace ITCommunity.Controllers {

    public class PollController : BasePostController {

        [Authorize]
        [HttpPost]
        public ActionResult Vote(int? id, int[] answers) {
            var poll = Polls.Get(id.Value);
            if (poll == null) {
                return NotFound();
            }
            if (answers == null) {
                return Redirect();
            }

            if (poll.IsMultiselect == false && answers.Length > 1) {
                Logger.Log.Error("Кто-то хочет смухливать в опросе" + Logger.GetUserInfo());
                return Forbidden();
            }
            if (Polls.IsUserVoted(id.Value, CurrentUser.User.Id)) {
                Logger.Log.Error("Кто-то хочет смухливать в опросе" + Logger.GetUserInfo());
                return Forbidden();
            }

            foreach (var answerId in answers) {
                if (poll.ContainsAnswer(answerId)) {
                    var vote = new Vote {
                        AnswerId = answerId,
                        UserId = CurrentUser.User.Id
                    };
                    Polls.AddVote(vote);
                } else {
                    Logger.Log.Error("Кто-то хочет смухливать в опросе" + Logger.GetUserInfo());
                    return Forbidden();
                }
            }

            var post = Posts.GetByEntity(id.Value);
            if (post == null) {
                return Redirect();
            } else {
                return RedirectToAction("view", "post", new { id = post.Id });
            }
        }

        public ActionResult Chart(int? id, bool? isThumb) {
            var poll = Polls.Get(id.Value);
            if (poll == null) {
                return NotFound();
            }

            int width = Config.PollWidth;
            int height = Config.PollHeight;
            int thumbWidth = Config.PollThumbWidth;
            int thumbHeight = Config.PollThumbHeight;

            var chart = new Chart();
            chart.BackColor = Color.White;

            if (isThumb != null && isThumb.Value) {
                chart.Width = Unit.Pixel(thumbWidth);
                chart.Height = Unit.Pixel(thumbHeight);
            } else {
                chart.Width = Unit.Pixel(width);
                chart.Height = Unit.Pixel(height);
            }

            var votes = new Series("Votes");

            votes.ChartArea = "VotesArea";
            votes.ChartType = SeriesChartType.Pie;
            votes.Font = new Font("Verdana", 8.25f, FontStyle.Regular);
            votes.CustomProperties = "PieStartAngle=270";

            foreach (var answer in poll.PollAnswers) {
                if (answer.Votes.Count > 0) {
                    votes.Points.Add(new DataPoint {
                        Label = "#PERCENT",
                        LegendText = answer.Text + " #PERCENT (" + answer.Votes.Count + ")",
                        YValues = new double[] { answer.Votes.Count }
                    });
                }
            }
            chart.Legends.Add(new Legend {
                IsTextAutoFit = true,
                IsEquallySpacedItems = true,
                AutoFitMinFontSize = 8,
                Docking = Docking.Right
            });

            chart.Series.Add(votes);

            ChartArea area = new ChartArea("VotesArea");
            area.BackColor = Color.White;
            area.Position = new ElementPosition(0, 0, 50, 100);
            chart.ChartAreas.Add(area);

            using (var ms = new MemoryStream()) {
                chart.SaveImage(ms, ChartImageFormat.Png);
                ms.Seek(0, SeekOrigin.Begin);

                return File(ms.ToArray(), "image/png", "poll" + id.Value.ToString() + ".png");
            }
        }

        [Authorize]
        public ActionResult Add() {
            PostEditCategoriesModel.Current.Clear();
            if (Poll.Category != null) {
                PostEditCategoriesModel.Current.IsAttached[Poll.Category.Id] = true;
            }
            var model = new PollEditModel();
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Add(PollEditModel model) {
            if (Upload(model, Post.DefaultPicturesPath)) {
                return View(model);
            }

            if (ModelState.IsValid) {
                var poll = model.ToPoll();
                poll = Polls.Add(poll);

                model.EntityId = poll.Id;
                model.EntityType = Post.EntityTypes.Poll;

                return base.BaseAdd(model);
            }

            return View(model);
        }

        [Authorize]
        public ActionResult Edit(int? id) {
            var post = Posts.Get(id.Value);
            if (post == null || post.EntityType != Post.EntityTypes.Poll) {
                return NotFound();
            }
            var poll = (Poll)post.Entity;

            if (post.Editable == false) {
                return Forbidden();
            }

            var model = new PollEditModel(post);

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(int? id, PollEditModel model) {
            var post = Posts.Get(id.Value);
            if (post == null || post.EntityType != Post.EntityTypes.Poll) {
                return NotFound();
            }
            var poll = (Poll)post.Entity;

            if (post.Editable == false) {
                return Forbidden();
            }

            if (ModelState.IsValid) {
                var editedPoll = model.ToPoll();
                editedPoll.Id = id.Value;
                Polls.Update(editedPoll);

                return base.BaseEdit(post.Id, model);
            }

            return View("Edit", model);
        }

        [Authorize(Roles = "admin")]
        public override ActionResult Delete(int? id) {
            Polls.Delete(id.Value);

            var post = Posts.GetByEntity(id.Value);

            return base.Delete(post.Id);
        }
    }
}
