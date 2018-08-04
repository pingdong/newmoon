namespace PingDong.Newmoon.Events.Configuration
{
    /// <summary>
    /// Options of application
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Api Version
        /// </summary>
        public string ApiVersion { get; set; }
        /// <summary>
        /// Version
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Application Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The interval of HealthCheck
        /// </summary>
        public int HealthCheckInterval { get; set; }

        /// <summary>
        /// Base Address
        /// </summary>
        public string BaseUri { get; set; }

        /// <summary>
        /// External Services
        /// </summary>
        public ExternalServices ExternalServices { get; set; }
    }
}
