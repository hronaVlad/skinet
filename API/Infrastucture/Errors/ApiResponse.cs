namespace API.Infrastucture.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            this.StatusCode = statusCode;
            this.Message = message ?? GetDefaultMessage();

        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetDefaultMessage() => this.StatusCode switch
        {
            400 => "A bad request, you have made",
            401 => "Authorized, you are not",
            404 => "Resource found, it was not",
            500 => "Internal error occured",
            _ => null
        };
    }
}