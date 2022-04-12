using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DBMSApi.Controllers.Viewmodels
{
    public class CreateIngredientViewModel
    {
        public string ingredientName { get; set; }
        public string? substituteNames { get; set; }
        public double estimatedPrice { get; set; }
    }
}
