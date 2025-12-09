namespace DanielSteginkUtils.Loggers
{
    /// <summary>
    /// Stores logging utilities for the rest of the library
    /// </summary>
    internal static class Logging
    {
        /// <summary>
        /// Custom logging for the helper
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="message"></param>
        /// <param name="performLogging"></param>
        internal static void Log(string prefix, string message, bool performLogging = false)
        {
            if (performLogging)
            {
                DanielSteginkUtils.Instance.Log($"{prefix} - {message}");
            }
        }
    }
}