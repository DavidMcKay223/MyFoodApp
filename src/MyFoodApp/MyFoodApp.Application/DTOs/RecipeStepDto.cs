namespace MyFoodApp.Application.DTOs
{
    public class RecipeStepDto
    {
        public int StepId { get; set; }
        public int RecipeId { get; set; }
        public int StepNumber { get; set; }
        public required string Instruction { get; set; }

        // Navigation Property
        public RecipeDto? Recipe { get; set; }
    }
}
