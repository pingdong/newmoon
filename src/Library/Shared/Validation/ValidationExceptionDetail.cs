namespace PingDong.Validation
{
    public class ValidationExceptionDetail
    {
        public ValidationExceptionDetail(string message)
        {
            this.Message = message;
        }

        public string Message { get; set; }
    }
}
