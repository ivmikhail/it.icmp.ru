using ITCommunity.Core;


namespace ITCommunity.DB {

    public partial class Header {

        public static int MaxLength {
            get { return Config.HeaderMaxLength; }
        }

        public static int ShowingHours {
            get { return Config.HeaderShowingHours; }
        }

        public static int RequiredPostsCount {
            get { return Config.HeaderRequiredPostsCount; }
        }

        partial void OnLoaded() {
            var loadUser = User;
        }
    }
}
