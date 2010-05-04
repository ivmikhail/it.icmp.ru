using System;
using System.Collections.Generic;
using System.Data;

namespace ITCommunity {

	public class Rfc {

		#region Properties

		private int _id;
		private string _number;
		private string _title;

		public int Id {
			get { return _id; }
			set { _id = value; }
		}

		public string Number {
			get { return _number; }
			set { _number = value; }
		}

		public string Title {
			get { return _title; }
			set { _title = value; }
		}

		#endregion

		#region Constructors

		public Rfc() {
			_id = -1;
			_number = "0000";
			_title = "";
		}

		public Rfc(int id, string number, string title) {
			_id = id;
			_number = number;
			_title = title;
		}

		#endregion

		#region Public static methods

		public static List<Rfc> Search(string query) {
			return GetRfcFromTable(Database.RfcSearchByTitle(query));
		}

		public static List<Rfc> GetByNum(string number) {
			if (number.Length > 4) {
				throw new ArgumentOutOfRangeException(number, "Допустимое кол-во символов от 0 до 4");
			}
			string num = number;
			while (num.Length != 4) {
				num = "0" + num;
			}
			return GetRfcFromTable(Database.RfcGetByNum(num));
		}

		#endregion

		#region Private static methods

		private static List<Rfc> GetRfcFromTable(DataTable dt) {
			var rfc = new List<Rfc>();

			for (int i = 0; i < dt.Rows.Count; i++) {
				rfc.Add(GetRfcFromRow(dt.Rows[i]));
			}

			return rfc;
		}

		private static Rfc GetRfcFromRow(DataRow dr) {
			Rfc rfc;

			if (dr == null) {
				rfc = new Rfc();
			}
			else {
				rfc = new Rfc(
					Convert.ToInt32(dr["id"]),
					Convert.ToString(dr["number"]),
					Convert.ToString(dr["title"])
				);
			}

			return rfc;
		}

		#endregion

	}
}
