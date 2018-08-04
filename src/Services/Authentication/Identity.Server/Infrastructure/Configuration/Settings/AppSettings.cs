namespace PingDong.Newmoon.IdentityServer.Configuration
{
    public class AppSettings
    {
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
