# Directory: Components\Shared

## File: CustomTimeInput.razor

```C#
@using System.Globalization
@using System.Text.RegularExpressions
@using System.Diagnostics.CodeAnalysis
@typeparam TValue
@inherits InputBase<TValue>

<input @bind-value="CurrentValueAsString" @onkeydown="OnKeyDown" @onkeyup="OnKeyUp" class="@CssClass" />

@code {
    private static readonly Regex TimeFormatRegex = new Regex(@"^(\d{1,2}):?(\d{0,2})$", RegexOptions.Compiled);

    private string _lastValue = String.Empty;

    protected override bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out TValue result, [NotNullWhen(false)] out string? validationErrorMessage)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            result = default!;
            validationErrorMessage = null;
            return true;
        }

        var match = TimeFormatRegex.Match(value);

        if (match.Success)
        {
            var minutes = match.Groups[1].Value;
            var seconds = match.Groups[2].Value;

            if (seconds.Length == 1)
            {
                seconds = "0" + seconds;
            }

            var formattedValue = $"{minutes}:{seconds}";

            if (typeof(TValue) == typeof(TimeOnly))
            {
                if (TimeOnly.TryParseExact(formattedValue, "m\\:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var timeOnly))
                {
                    result = (TValue)(object)timeOnly;
                    validationErrorMessage = null;
                    return true;
                }
            }
            else if (typeof(TValue) == typeof(TimeSpan))
            {
                if (TimeSpan.TryParseExact(formattedValue, "m\\:ss", CultureInfo.InvariantCulture, out var timeSpan))
                {
                    result = (TValue)(object)timeSpan;
                    validationErrorMessage = null;
                    return true;
                }
            }
        }

        result = default!;
        validationErrorMessage = "Invalid time format";
        return false;
    }

    protected override string? FormatValueAsString(TValue? value)
    {
        if (value is TimeOnly timeOnly)
        {
            return timeOnly.ToString("m\\:ss", CultureInfo.InvariantCulture);
        }
        else if (value is TimeSpan timeSpan)
        {
            return timeSpan.ToString("m\\:ss", CultureInfo.InvariantCulture);
        }

        return String.Empty;
    }

    private void OnKeyDown(KeyboardEventArgs e)
    {
        _lastValue = CurrentValueAsString ?? String.Empty;
    }

    private void OnKeyUp(KeyboardEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(CurrentValueAsString))
        {
            CurrentValueAsString = _lastValue;
        }
    }
}

```

## File: DisplayError.razor

```C#
@using MyFoodApp.Application.Common

@if (Errors.Any())
{
    <div class="alert alert-danger mb-3">
        <h5 class="alert-heading">Error(s) occurred:</h5>
        <ul>
            @foreach (var error in Errors)
            {
                <li>@error.Message</li>
            }
        </ul>
    </div>
}

@code {
    [Parameter]
    public List<Error> Errors { get; set; } = new();
}

```

## File: SearchButton.razor

```C#

<div class="mb-3">
    @if(OnAdd.HasDelegate)
    {
        <button class="btn btn-primary me-2" @onclick="OnAdd">New Item</button>
    }
    @if (OnSearch.HasDelegate)
    {
        <button class="btn btn-secondary me-2" @onclick="OnSearch">Search</button>
    }
    @if (OnReset.HasDelegate)
    {
        <button class="btn btn-danger me-2" @onclick="OnReset">Reset</button>
    }
</div>

@code {
    [Parameter]
    public EventCallback OnAdd { get; set; }

    [Parameter]
    public EventCallback OnSearch { get; set; }

    [Parameter]
    public EventCallback OnReset { get; set; }
}

```

## File: SearchPagination.razor

```C#
@typeparam TItem

@if (TotalItems > 0)
{
    <div class="d-flex justify-content-between align-items-center my-3">
        <div>
            <select class="form-select" @onchange="OnPageSizeChanged">
                @foreach (var size in PageSizes)
                {
                    <option value="@size" selected="@(size == PageSize)">@size</option>
                }
            </select>
        </div>
        <div>
            <button class="btn btn-primary" @onclick="PreviousPage" disabled="@IsFirstPage">Previous</button>
            <span class="mx-2">Page @(CurrentPage + 1) of @(TotalPages == 0 ? 1 : TotalPages)</span>
            <button class="btn btn-primary" @onclick="NextPage" disabled="@IsLastPage">Next</button>
        </div>
    </div>
}

@code {
    [Parameter] public int TotalItems { get; set; }
    [Parameter] public int PageSize { get; set; } = 10;
    [Parameter] public int CurrentPage { get; set; } = 0;
    [Parameter] public EventCallback<int> OnPageChanged { get; set; }
    [Parameter] public EventCallback<int> OnPageSizeChangedCallback { get; set; }

    private int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
    private bool IsFirstPage => CurrentPage == 0;
    private bool IsLastPage => (CurrentPage + 1) >= TotalPages;

    private List<int> PageSizes { get; set; } = new List<int> { 10, 20, 50, 100 };

    private async Task PreviousPage()
    {
        if ((CurrentPage + 1) > 1)
        {
            CurrentPage--;
            await OnPageChanged.InvokeAsync(CurrentPage);
        }
    }

    private async Task NextPage()
    {
        if ((CurrentPage + 1) < TotalPages)
        {
            CurrentPage++;
            await OnPageChanged.InvokeAsync(CurrentPage);
        }
    }

    private async Task OnPageSizeChanged(ChangeEventArgs e)
    {
        PageSize = int.Parse(e.Value!.ToString()!);
        CurrentPage = 0;
        await OnPageSizeChangedCallback.InvokeAsync(PageSize);
    }
}

```

