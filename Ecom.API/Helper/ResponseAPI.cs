namespace Ecom.API.Helper
{
    public class ResponseAPI
    {
        public ResponseAPI(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetMassageFormStatusCode(statusCode);
        }
        private string GetMassageFormStatusCode(int statusCode)
        {
            return statusCode switch
            {
                200 => "Done",
                201 => "Created",
                204 => "No Content",
                400 => "Bad Request",
                401 => "Unauthorized",
                403 => "Forbidden",
                404 => "resource Not Found ",
                500 => "Internal Server Error",
                _ => "Unknown Status"
            };
        }

        public int StatusCode { get; set; }
        public string? Message { get; set; }
    }
}
