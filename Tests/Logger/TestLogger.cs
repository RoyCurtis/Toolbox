using System;

namespace Tests.Logger
{
    class TestLogger : ILogger
    {
        public event Action<bool> Pause;

        bool paused;
        public bool Paused
        {
            get
            {
                return paused;
            }

            set
            {
                paused = value;
                if (Pause != null)
                        Pause(value);
            }
        }

        internal LogChannel LastSource;
        internal LogLevels  LastLevel;
        internal string     LastTag;
        internal string     LastMessage;
        internal object[]   LastArgs;

        public void Emit(LogChannel source, LogLevels level, string tag, string message, object[] args)
        {
            if (paused)
                return;

            LastSource  = source;
            LastLevel   = level;
            LastTag     = tag;
            LastMessage = message;
            LastArgs    = args;
        }
    }
}
