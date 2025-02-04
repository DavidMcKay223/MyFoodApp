# Directory: Common

## File: Error.cs

```C#
namespace MyFoodApp.Application.Common
{
    public class Error
    {
        public required string Code { get; set; }
        public required string Message { get; set; }
    }
}

```

## File: Response.cs

```C#
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

```

## File: SearchDto.cs

```C#
namespace MyFoodApp.Application.Common
{
    public abstract class SearchDto<TSortBy, TSortOrder>
        where TSortBy : Enum
        where TSortOrder : Enum
    {
        public int PageNumber { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Boolean UsePagination { get; set; } = true;
        public TSortBy? SortBy { get; set; }
        public TSortOrder? SortOrder { get; set; }
    }
}

```

