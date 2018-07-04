namespace PingDong.Newmoon.Events.Configuration
{
    /// <summary>
    /// Options of application
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Version
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Application Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// External Services
        /// </summary>
        public ExternalServices ExternalServices { get; set; }
    }
}
