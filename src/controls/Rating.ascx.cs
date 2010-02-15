using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITCommunity {
	public partial class RatingControl : System.Web.UI.UserControl {
		private Rating _rating = new Rating();
		private string _message = "";

		public int EntityId {
			get { return _rating.EntityId; }
			set { _rating.EntityId = value; }
		}

		public EntityType Type {
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

		protected void Page_Load(object sender, EventArgs e) {
			if (!IsPostBack) {
				_rating = Rating.Get(EntityId, Type);
				if (CurrentUser.isAuth) {
					RatingValue.Visible = IsUserVoted();
				}
				RatingUpdatePanel.DataBind();
			}
			Visible = false;
		}

		protected void IncRatingClick(object sender, EventArgs e) {
			AddRating(true);
		}

		protected void DecRatingClick(object sender, EventArgs e) {
			AddRating(false);
		}

		private void AddRating(bool isInc) {
			if (!CurrentUser.isAuth) {
				_message = "Вам нужно авторизоваться, чтобы голосовать.";
			}
			else {
				if (!IsUserVoted()) {
					int value = isInc ? 1 : -1;
					// Сюда писать формулы рейтингов
					switch (Type) {
						case EntityType.Comment:
						case EntityType.Post:
						case EntityType.User:
						default:
							_rating = Rating.Add(EntityId, Type, CurrentUser.User.Id, value);
							break;
					}
					_message = "Ваш голос принят.";
				}
				else {
					_rating = Rating.Get(EntityId, Type);
					_message = "Извините, но вы уже голосовали.";
				}
				RatingValue.Visible = true;
			}
			RatingMessage.Visible = true;
			RatingUpdatePanel.DataBind();
		}

		private bool IsUserVoted() {
			if (CurrentUser.isAuth) {
				return Rating.IsUserVoted(EntityId, Type, CurrentUser.User.Id);
			}
			return false;
		}
	}
}
