# Directory: Components\Pages\Recipes\Steps

## File: Display.razor

```C#
@using MyFoodApp.Application.DTOs

@code {
    [Parameter]
    public required List<RecipeStepDto> Steps { get; set; }

    private Dictionary<RecipeStepDto, bool> _expandedState = new();

    protected override void OnParametersSet()
    {
        InitializeExpandedState();
    }

    private void InitializeExpandedState()
    {
        foreach (var step in Steps)
        {
            if (!_expandedState.ContainsKey(step))
            {
                _expandedState[step] = true;
            }
        }
    }
}

@if (Steps != null)
{
    <div class="card mb-3 shadow-lg">
        <div class="card-header bg-primary text-white d-flex justify-content-between align-items-center">
            <h3 class="mb-0">
                <i class="fas fa-list-ol me-2"></i>
                Steps (@Steps.Count)
            </h3>
        </div>

        <div class="card-body">
            @if (Steps.Count == 0)
            {
                <div class="text-center py-4 text-muted">
                    <i class="fas fa-inbox fa-3x mb-3"></i>
                    <p class="mb-0">No steps added yet</p>
                </div>
            }
            else
            {
                <ul class="list-unstyled">
                    @foreach (var step in Steps)
                    {
                        <li class="mb-3">
                            <div class="step-card card shadow-sm">
                                <div class="card-header bg-light d-flex justify-content-between align-items-center">
                                    <button class="btn btn-link text-dark text-decoration-none"
                                            @onclick="@(() => _expandedState[step] = !_expandedState[step])">
                                        <i class="fas @(_expandedState[step] ? "fa-chevron-down" : "fa-chevron-right") me-2"></i>
                                        <strong>Step @step.StepNumber</strong>
                                    </button>
                                </div>

                                <div class="collapse @(_expandedState[step] ? "show" : "")">
                                    <div class="card-body">
                                        <div class="form-group">
                                            <label for="step-@step.StepNumber">Instruction</label>
                                            <textarea id="step-@step.StepNumber"
                                                      class="form-control"
                                                      rows="3"
                                                      @bind="step.Instruction" readonly></textarea>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </li>
                    }
                </ul>
            }
        </div>
    </div>
}

```

## File: Edit.razor

```C#
@using MyFoodApp.Application.DTOs

@code {
    [Parameter]
    public required List<RecipeStepDto> Steps { get; set; }

    private void AddStep()
    {
        Steps.Add(new RecipeStepDto() { Instruction = "", StepNumber = Steps.Count + 1 });
    }

    private void RemoveStep(int index)
    {
        Steps.RemoveAt(index);
    }
}

@if (Steps != null)
{
    <div class="card mb-3">
        <div class="card-header">Steps</div>
        <div class="card-body">
            @for (int i = 0; i < Steps.Count; i++)
            {
                var index = i;
                <div class="row g-3 mb-3 align-items-end">
                    <div class="col-md-2">
                        <div class="form-floating">
                            <InputNumber @bind-Value="Steps[index].StepNumber" class="form-control" />
                            <label>Step Number</label>
                            <ValidationMessage For="@(() => Steps[index].StepNumber)" />
                        </div>
                    </div>
                    <div class="col-md-8">
                        <div class="form-floating">
                            <InputTextArea @bind-Value="Steps[index].Instruction" class="form-control" />
                            <label>Instruction</label>
                            <ValidationMessage For="@(() => Steps[index].Instruction)" />
                        </div>
                    </div>
                    <div class="col-md-2">
                        <button type="button" class="btn btn-danger" @onclick="() => RemoveStep(index)">
                            Remove
                        </button>
                    </div>
                </div>
            }
            <button type="button" class="btn btn-primary" @onclick="AddStep">Add Step</button>
        </div>
    </div>
}

```

