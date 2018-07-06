namespace PingDong.Application.Logging
{
    public class LoggingEvent
    {
        // Information

        // Success
        public const int Success = 100;
        // Entering a process
        public const int Entering = 101;



        // Exception or Error

        // Unhandle
        public const int UnhandleException = 1000;

        public const int ReceiveInvalidData = 1001;

        public const int CreateDuplicatedData = 1002;

        public const int ObjectNotFound = 1003;



        // Operation

        // Get Objects
        public const int GetObjects = 10000;
    }
}
