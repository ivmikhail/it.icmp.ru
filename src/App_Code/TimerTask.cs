using System;
using System.Collections.Generic;
using System.Web;
using System.Timers;

namespace ITCommunity {
    public class TimerTask {
        private readonly Timer timer;
        public TimerTask(double interval, ElapsedEventHandler handler) {
            this.timer = new Timer();
            this.timer.Interval = interval;
            this.timer.Elapsed += new ElapsedEventHandler(handler);
            this.timer.Enabled = true;
        }
    }
}