using ITCommunity.Core;


namespace ITCommunity.DB {

    public partial class Rfc {

        public string RelativeUrl {
            get {
                string num = "";
                if (this.Number.StartsWith("000")) {
                    num = Number.Substring(3);
                } else if (this.Number.StartsWith("00")) {
                    num = Number.Substring(2);
                } else if (this.Number.StartsWith("0")) {
                    num = Number.Substring(1);
                } else {
                    num = Number;
                }
                return "/" + Config.RfcFolder + "/rfc" + num + ".txt";
            }
        }
    }
}