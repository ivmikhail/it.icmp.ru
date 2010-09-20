using ITCommunity.Core;
using ITCommunity.DB.Tables;


namespace ITCommunity.DB {

    public partial class Rating {

        public enum EntityTypes {
            Comment = 0,
            Post = 1,
            User = 2
        }

        public string HtmlId {
            get { return EntityType.ToString() + EntityId.ToString() + "Rating"; }
        }

        public string Sign {
            get {
                if (Value > 0) {
                    return "positive";
                }
                if (Value < 0) {
                    return "negative";
                }
                return "none";
            }
        }

        public bool IsRated { get; set; }

        partial void OnLoaded() {
            IsRated = Ratings.IsRated(CurrentUser.User.Id, EntityId, EntityType);
        }

        public static int GetValue(EntityTypes type) {
            return Config.GetInt(type.ToString() + "RatingValue");
        }
    }
}
