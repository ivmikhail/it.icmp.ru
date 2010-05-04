using System;

namespace ITCommunity {
	public partial class RatingControl : System.Web.UI.UserControl {

		private Rating _rating = new Rating();
		private string _message = "";
		private int _entityAuthorId = -1;

		public int EntityId {
			get { return _rating.EntityId; }
			set { _rating.EntityId = value; }
		}

		public Rating.EntityType Type {
			get { return _rating.Type; }
			set { _rating.Type = value; }
		}

		public string Value {
			get {
				if (_rating.Value > 0) {
					return "+" + _rating.Value.ToString();
				}
				else {
					return _rating.Value.ToString();
				}
			}
		}

		public string ValueSign {
			get {
				if (_rating.Value > 0) {
					return "positive";
				}
				else if (_rating.Value < 0) {
					return "negative";
				}
				else {
					return "none";
				}
			}
		}

		public string Message {
			get { return _message; }
		}

		public int EntityAuthorId {
			get { return _entityAuthorId; }
			set { _entityAuthorId = value; }
		}

		public bool ButtonsVisible {
			get { return RatingButtons.Visible; }
			set { RatingButtons.Visible = value; }
		}

		protected void Page_Load(object sender, EventArgs e) {
			RatingMessage.Visible = false;
			_rating = Rating.Get(EntityId, Type);
			RatingPanel.DataBind();
		}

		protected void IncRatingClick(object sender, EventArgs e) {
			AddRating(true);
		}

		protected void DecRatingClick(object sender, EventArgs e) {
			AddRating(false);
		}

		private void AddRating(bool isInc) {
			if (!CurrentUser.isAuth) {
				_message = "авторизуйтесь";
			}
			else {
				if (CurrentUser.User.Id == EntityAuthorId) {
					_message = "за свое нельзя голосовать";
				}
				else if (IsUserVoted()) {
					_message = "вы уже голосовали";
				}
				else if (!CanUserToVote()) {
					_message = "пока вы не можете голосовать";
				}
				else {
					int value = isInc ? 1 : -1;
					// Сюда писать формулы рейтингов
					switch (Type) {
						case Rating.EntityType.Comment:
						case Rating.EntityType.Post:
						case Rating.EntityType.User:
						default:
							_rating = Rating.Add(EntityId, Type, CurrentUser.User.Id, value);
							break;
					}
					_message = "ваш голос принят";
				}
			}
			_message = ", " + _message;
			RatingMessage.Visible = true;
			RatingButtons.Visible = false;
			DataBind();
		}

		private bool IsUserVoted() {
			if (CurrentUser.isAuth) {
				return Rating.IsUserVoted(EntityId, Type, CurrentUser.User.Id);
			}
			return false;
		}

		private bool CanUserToVote() {
			ITCommunity.User user = CurrentUser.User;
			if (user.PostsCount > 0) {
				return true;
			}
			DateTime date = DateTime.Now.AddDays(-7);
			if (date.CompareTo(user.CreateDate) > 0) {
				return true;
			}
			return false;
		}
	}
}
