namespace Models
{
    public class ApiResponse
    {
        private ApiResponse()
        {

        }
        public bool Succeeded { get; set; }
        public string Message { get; set; }

        public static ApiResponse Error(string message) => new ApiResponse { Message = message, Succeeded = false };
        public static ApiResponse Success() => new ApiResponse { Message = null, Succeeded = true };
    }
}
