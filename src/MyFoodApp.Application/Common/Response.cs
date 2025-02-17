namespace MyFoodApp.Application.Common
{
    public class Response<T>
    {
        public T? Item { get; set; }
        public List<T> List { get; set; } = [];
        public int TotalItems { get; set; } = 0;
        public List<Error> ErrorList { get; set; } = [];
    }
}
