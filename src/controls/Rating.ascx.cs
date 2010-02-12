using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITCommunity {
	public partial class RatingControl : System.Web.UI.UserControl {
		private Rating _rating = new Rating();

		public int EntityId {
			get {
				return _rating.EntityId;
			}
			set {
				_rating.EntityId = value;
			}
		}

		public EntityType Type {
			get {
				return _rating.Type;
			}
			set {
				_rating.Type = value;
			}
		}

		public int Value {
			get {
				return _rating.Value;
			}
		}

		protected void Page_Load(object sender, EventArgs e) {
			if (!IsPostBack) {
				_rating = Rating.Get(EntityId, Type);
				RatingUpdatePanel.DataBind();
			}
		}

		protected void IncRatingClick(object sender, EventArgs e) {
			AddRating(((LinkButton)sender).CommandArgument, true);
		}

		protected void DecRatingClick(object sender, EventArgs e) {
			AddRating(((LinkButton)sender).CommandArgument, false);
		}

		private void AddRating(string commandArgument, bool isInc) {
			if (!CurrentUser.isAuth) {
				return;
			}

			string[] args = commandArgument.Split(',');
			int entityId = Convert.ToInt32(args[0]);
			EntityType entityType = (EntityType)Enum.Parse(typeof(EntityType), args[1]);
			int value = isInc ? 1 : -1;
			// Сюда писать формулы рейтингов
			switch (entityType) {
				case EntityType.Comment:
				case EntityType.Post:
				case EntityType.User:
				default:
					_rating = Rating.Add(entityId, entityType, CurrentUser.User.Id, value);
					break;
			}
			RatingUpdatePanel.DataBind();
		}
	}
}