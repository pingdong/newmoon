namespace PingDong.Web.Http
{
    /// <summary>
    /// Pagination
    /// </summary>
    public class JsonPagination
    {
        /// <summary>
        /// Current Page Index
        /// </summary>
        public int CurrentPageIndex { get; set; }
        /// <summary>
        /// Total pages
        /// </summary>
        public int TotalPages { get; set; }
        /// <summary>
        /// Count of data records
        /// </summary>
        public int Count { get; set; }
    }
}
