
namespace ITCommunity.Db {

    public partial class Database {

        public static Database Connect() {
            return new Database(Global.GetConnectionString());
        }
    }
}
