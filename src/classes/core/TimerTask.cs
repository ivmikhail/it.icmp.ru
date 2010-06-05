using System;
using System.Timers;

namespace ITCommunity.Core {

    public class TimerTask {

        private readonly Timer _timer;
        private Action _action;

        public TimerTask(double interval, Action action) {
            _action = action;

            _timer = new Timer(interval);
            _timer.Elapsed += new ElapsedEventHandler(this.Handler);
            _timer.Enabled = true;
        }

        private void Handler(object source, ElapsedEventArgs e) {
            _action.Invoke();
        }
    }
}
