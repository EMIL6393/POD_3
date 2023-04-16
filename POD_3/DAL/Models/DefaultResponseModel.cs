namespace POD_3.DAL.Models
{
    public class DefaultResponseModel<T>
    {
        public int StatusCode { get; set; }
        public T? Data { get; set; }
        public List<ApiError>? Errors { get; set; }
        public string Message { get; set; } = null!;


    }
    public class ApiError
    {
        public string Title { get; set;}
        public string Message { get; set;}
    }
}
