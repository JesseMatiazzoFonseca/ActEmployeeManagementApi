using Newtonsoft.Json;

namespace Domain.Exceptions
{
    public class CustomException : Exception
    {
        public object Payload { get; set; }
        public string Error { get; set; }
        public CustomException(string message) : base(message)
        {
        }
        public CustomException(string message, object payload) : base(message)
        {
            Payload = payload;
        }
        public CustomException(string message, object payload, string error) : base(message)
        {
            Payload = IsJson((string)payload) ? payload : JsonConvert.SerializeObject(payload);
            Error = error;
        }
        private static bool IsJson(string input)
        {
            input = input.Trim();
            return input.StartsWith("{") && input.EndsWith("}")
                || input.StartsWith("[") && input.EndsWith("]");
        }
    }
}
