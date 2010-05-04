using System;
using System.Collections.Generic;
using System.Data;
using System.Timers;

namespace ITCommunity {

	public class RecoveryPass {

		#region Properties

		private int _id;
		private string _identifier;
		private int _user_id;
		private DateTime _cdate;


		public int Id {
			get { return _id; }
			set { _id = value; }
		}

		public string Identifier {
			get { return _identifier; }
			set { _identifier = value; }
		}

		public User User {
			get {
				return User.Get(_user_id);
			}
			set {
				_user_id = value.Id;
			}
		}

		public DateTime CreateDate {
			get { return _cdate; }
			set { _cdate = value; }
		}

		#endregion

		#region Constructors

		public RecoveryPass() {
			_id = -1;
			_identifier = "";
			_user_id = -1;
			_cdate = DateTime.Now;
		}

		public RecoveryPass(int id, string identifier, int user_id, DateTime cdate) {
			_id = id;
			_identifier = identifier;
			_user_id = user_id;
			_cdate = cdate;
		}

		#endregion

		#region Public static methods

		public static void Delete(string identifier) {
			Database.RecoveryDel(identifier);
		}

		public static RecoveryPass GetByIdentifier(string identifier) {
			return GetRecFromRow(Database.RecoveryGetByIdentifier(identifier));
		}

		public static RecoveryPass Add(string identifier, int user_id) {
			return GetRecFromRow(Database.RecoveryAdd(identifier, user_id));
		}

		public static void PurgeOldRecoveryTasks(object source, ElapsedEventArgs e) {
			Database.RecoveryPurgeOldTasks();
		}

		#endregion

		#region Private static methods

		private static List<RecoveryPass> GetRecFromTable(DataTable dt) {
			List<RecoveryPass> recs = new List<RecoveryPass>();
			for (int i = 0; i < dt.Rows.Count; i++) {
				recs.Add(GetRecFromRow(dt.Rows[i]));
			}
			return recs;
		}

		private static RecoveryPass GetRecFromRow(DataRow dr) {
			RecoveryPass rec;
			if (dr == null) {
				rec = new RecoveryPass();
			}
			else {
				rec = new RecoveryPass(
					Convert.ToInt32(dr["id"]),
					Convert.ToString(dr["identifier"]),
					Convert.ToInt32(dr["user_id"]),
					Convert.ToDateTime(dr["cdate"])
				);
			}
			return rec;
		}

		#endregion
	}
}
