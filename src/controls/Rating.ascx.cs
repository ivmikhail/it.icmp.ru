using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITCommunity {
	public partial class RatingControl : System.Web.UI.UserControl {
		private Rating _rating = new Rating();

		public int Id {
			get {
				return _rating.Id;
			}
		}

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
			}
		}

	}
}