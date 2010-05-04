using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace ITCommunity {

	public class DatabaseLinq : DatabaseLinqBase {

		public DatabaseLinq()
			: base(Global.GetConnectionString()) {
				ObjectTrackingEnabled = false;
		}
	}
}