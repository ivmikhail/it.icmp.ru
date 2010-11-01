using System;
using System.Timers;


namespace ITCommunity.Core {

    public class TimerTask {

        private readonly Timer _timer;
        private Action _action;

        public TimerTask(double minutes, Action action) {
            _action = action;

            var milliseconds = minutes * 60 * 1000;

            _timer = new Timer(milliseconds);
            _timer.Elapsed += new ElapsedEventHandler(this.Handler);
            _timer.Enabled = true;
        }

        private void Handler(object source, ElapsedEventArgs e) {
            _action.Invoke();
        }
    }
}
