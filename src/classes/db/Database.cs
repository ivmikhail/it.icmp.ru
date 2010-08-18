using System.Collections.Generic;
using System.Linq;

using ITCommunity.Core;


namespace ITCommunity.Db {

    public partial class Database {

        public static Database Connect() {
            return new Database(Config.ConnectionString);
        }
    }
}
