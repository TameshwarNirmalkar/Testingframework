using System.Threading;
using AutoDesk.Framework.Environment;

namespace AutoDesk.Framework.Helper
{
    /// <summary>
    /// SleepHelper is a helper class to manage sleeps in the current thread execution.
    /// </summary>
    public class SleepHelper
    {
        /// <summary>
        /// Sleeps thread execution during the desiarabled seconds.
        /// </summary>
        /// <param name="seconds">the seconds to sleep</param>
        public static void Sleep(int seconds)
        {
            if (EnvironmentReader.Sleep)
            {
                Thread.Sleep(seconds * 1000);
            }
        }
    }
}
