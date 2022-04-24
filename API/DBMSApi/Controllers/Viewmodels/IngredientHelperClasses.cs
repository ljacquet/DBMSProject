using DBMSApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DBMSApi.Controllers.Viewmodels
{
    public class CreateIngredientViewModel
    {
        public string ingredientName { get; set; }
        public string? substituteNames { get; set; }
        public double estimatedPrice { get; set; }
    }

    public class IngredientResponseModel
    {
        // Ingredient Info
        public int ingredientId { get; set; }
        public string ingredientName { get; set; }
        // Roomate Ingredient Info
        public double price { get; set; }
        public double quantity { get; set; }
        public string quantityUnit { get; set; }
        // Roomate Info
        public string ownerName { get; set; }
        public int roomateId { get; set; }

        public static IngredientResponseModel ingredientResponseBuilder(Ingredient i, RoomateIngredient ri, Roomate r)
        {
            return new IngredientResponseModel()
            {
                ingredientId = i.ingredientId,
                ingredientName = i.ingredientName,
                price = (double)(ri.price == null ? 0.0 : ri.price),
                quantity = ri.quantity,
                quantityUnit = ri.quantityUnit,
                ownerName = r.username,
                roomateId = r.roomateId
            };
        }
    }
}
