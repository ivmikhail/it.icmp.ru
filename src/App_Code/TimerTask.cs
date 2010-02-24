using System;
using System.Collections.Generic;
using System.Web;
using System.Timers;

namespace ITCommunity {
	public class TimerTask {
		private readonly Timer _timer;

		public TimerTask(double interval, ElapsedEventHandler handler) {
			_timer = new Timer();
			_timer.Interval = interval;
			_timer.Elapsed += new ElapsedEventHandler(handler);
			_timer.Enabled = true;
		}
	}
}
