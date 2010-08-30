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

    public class PostController : BaseController {

        public ActionResult View(int? id) {
            if (id == 0) {
                return NotFound();
            }

            var post = Posts.Get(id.Value);

            if (post != null) {
                if (CurrentUser.User.Id != post.AuthorId) {
                    Posts.IncViews(post);
                }
                if (post.EntityType == Post.EntityTypes.Poll) {
                    return View("../Poll/View", post);
                }
                return View(post);
            }

            return NotFound();
        }

        public ActionResult PollChart(int? id, bool? isThumb) {
            var poll = Polls.Get(id.Value);
            if (poll == null) {
                return NotFound();
            }

            int maxLength = 10;
            int maxThumbLength = 7;
            int width = 800;
            int height = 400;
            int thumbWidth = 400;
            int thumbHeight = 200;

            var chart = new Chart();
            chart.BackColor = Color.White;

            if (isThumb != null && isThumb.Value) {
                maxLength = maxThumbLength;
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

            foreach (var answer in poll.PollAnswers) {
                if (answer.Votes.Count > 0) {
                    votes.Points.Add(new DataPoint {
                        IsValueShownAsLabel = true,
                        LegendText = answer.Text,
                        YValues = new double[] { answer.Votes.Count }
                    });
                }
            }
            chart.Legends.Add(new Legend { 
                IsTextAutoFit = true
            });

            chart.Series.Add(votes);

            ChartArea area = new ChartArea("VotesArea");
            area.BackColor = Color.White;
            chart.ChartAreas.Add(area);

            using (var ms = new MemoryStream()) {
                chart.SaveImage(ms, ChartImageFormat.Png);
                ms.Seek(0, SeekOrigin.Begin);

                return File(ms.ToArray(), "image/png", "poll" + id.Value.ToString() + ".png");
            }
        }

        public ActionResult List(int? page) {
            var model = new PostListModel(PostListModel.SortBy.Date, page);

            return View("ListPage", model);
        }

        public ActionResult PopularList(string period, int? page) {
            var model = new PostListModel(PostListModel.SortBy.Views, period, page);

            return View(model);
        }

        public ActionResult DiscussibleList(string period, int? page) {
            var model = new PostListModel(PostListModel.SortBy.Comments, period, page);

            return View(model);
        }

        public ActionResult Search(string query) {
            return View();
        }

        [Authorize]
        public ActionResult Add() {
            PostEditCategoriesModel.Current.Clear();
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Add(PostEditModel model) {
            if (ModelState.IsValid) {
                var post = model.ToPost();

                post = Posts.Add(post);

                if (CurrentUser.IsAdmin == false) {
                    var user = CurrentUser.User;
                    user.HeadersCounter++;
                    Users.Update(user);
                }

                return RedirectToAction("view", new { id = post.Id });
            }

            return View(model);
        }

        [Authorize]
        public ActionResult AddPoll() {
            PostEditCategoriesModel.Current.Clear();
            if (Poll.Category != null) {
                PostEditCategoriesModel.Current.IsAttached[Poll.Category.Id] = true;
            }
            return View("../Poll/Add");
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddPoll(PostEditPollModel model) {
            if (ModelState.IsValid) {
                var poll = model.ToPoll();

                poll = Polls.Add(poll);
                model.EntityId = poll.Id;
                model.EntityType = Post.EntityTypes.Poll;

                return Add(model);
            }

            return View("../Poll/Add", model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult VotePoll(int? id, int? postId, int[] answers) {
            if (id == null && postId == null) {
                return NotFound();
            }

            if (answers != null) {
                var poll = Polls.Get(id.Value);

                foreach (var answer in answers) {
                    var vote = new Vote {
                        AnswerId = answer,
                        UserId = CurrentUser.User.Id
                    };

                    Polls.AddVote(vote);

                    if (poll.IsMultiselect == false) {
                        break;
                    }
                }
            }

            return RedirectToAction("view", new { id = postId.Value });
        }

        [Authorize]
        public ActionResult Edit(int? id) {
            if (id == 0) {
                return NotFound();
            }

            var post = Posts.Get(id.Value);

            if (CurrentUser.IsAdmin == false && post.AuthorId != CurrentUser.User.Id) {
                return AccessDenied();
            }

            var model = new PostEditModel(post);

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(int? id, PostEditModel model) {
            if (id == 0) {
                return NotFound();
            }

            var post = Posts.Get(id.Value);

            if (CurrentUser.IsAdmin == false && post.AuthorId != CurrentUser.User.Id) {
                return AccessDenied();
            }

            if (ModelState.IsValid) {
                var editedPost = model.ToPost();
                editedPost.Id = post.Id;

                Posts.Update(editedPost);

                return RedirectToAction("view", new { id = post.Id });
            }

            return View(model);
        }

        [Authorize]
        public ActionResult ToggleCategory(int? categoryId) {
            if (!Request.IsAjaxRequest()) {
                return AccessDenied();
            }

            if (categoryId == null) {
                return NotFound();
            }

            PostEditCategoriesModel.Current.IsAttached[categoryId.Value] = !PostEditCategoriesModel.Current.IsAttached[categoryId.Value];

            return PartialView("EditCategories", PostEditCategoriesModel.Current);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id) {
            if (id == 0) {
                return NotFound();
            }

            var post = Posts.Get(id.Value);
            if (post == null) {
                return NotFound();
            }

            Posts.Delete(id.Value);

            return Redirect("/");
        }
    }
}
