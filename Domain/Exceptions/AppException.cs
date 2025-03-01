namespace Domain.Exceptions
{
    public class AppException : Exception
    {
        public int Code { get; set; }

        public AppException(string message) : base(message)
        {
        }

        public AppException(int code, string message) : base(message)
        {
            this.Code = code;
        }

        public AppException(int code, string message, Exception ex) : base(message, ex)
        {
            this.Code = code;
        }
    }
}
