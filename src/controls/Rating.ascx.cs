﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITCommunity {
	public partial class RatingControl : System.Web.UI.UserControl {
		private Rating _rating = new Rating();
		private string _message = "";
		private int _entityAuthorId = -1;

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

		public int EntityAuthorId {
			get { return _entityAuthorId; }
			set { _entityAuthorId = value; }
		}

		protected void Page_Load(object sender, EventArgs e) {
			RatingMessage.Visible = false;
			_rating = Rating.Get(EntityId, Type);
			RatingUpdatePanel.DataBind();
//			Visible = false;
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
				else if (!IsUserVoted()) {
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
					_message = "ваш голос принят";
				}
				else {
					_rating = Rating.Get(EntityId, Type);
					_message = "вы уже голосовали";
				}
			}
            if (_message != "")
            {
                _message = ", " + _message;
            }
			RatingMessage.Visible = true;
			RatingButtons.Visible = false;
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
