import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-edit-recipe',
  templateUrl: './edit-recipe.component.html',
  styleUrls: ['./edit-recipe.component.css'],
})
export class EditRecipeComponent implements OnInit {
  constructor(private api: ApiService, private route: ActivatedRoute) {}

  recipeId: number = 0;
  recipe: {
    recipeId: number;
    recipeName: string;
    description: string;
    ingredients: {
      ingredientName: string;
      ingredientId: number;
      ingredientAmount: number;
      ingredientUnit: string;
    }[];
  } | null = null;

  ingredientTypes: any[] = [];
  validIngredients: any[] = [];

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      this.recipeId = params['id'];
      this.loadRecipe();
    });
  }

  async loadRecipe() {
    let resp = await this.api.getRecipe(this.recipeId);

    if (resp == null) {
      alert('Error Getting Recipe');
    } else {
      this.recipe = resp;
      console.log(this.recipe);

      if (this.recipe != null) {
        let ingredientIds = this.recipe.ingredients.map((x) => {
          return x.ingredientId;
        });

        this.ingredientTypes = await this.api.getValidIngredients();

        // Remove ingredients for adding that they have already added
        this.validIngredients = this.ingredientTypes.filter((x) => {
          return ingredientIds.indexOf(x.ingredientId) == -1;
        });
      }
    }
  }

  async updateRecipe(name: string, description: string) {
    let resp = await this.api.updateRecipe(this.recipeId, name, description);

    if (resp == null) {
      alert("Error Updating Recipe");
    }
    else {
      this.loadRecipe();
    }
  }

  async addIngredient(ingredientId: string, amount: string, amountUnit: string) {
    let resp = await this.api.addRecipeIngredient(this.recipeId, Number.parseInt(ingredientId), Number.parseFloat(amount), amountUnit);

    if (resp) {
      this.loadRecipe();
    }
    else {
      alert("Error Adding Ingredient, more information in console");
    }
  }

  async deleteIngredient(ingredientId: number) {
    let resp = await this.api.deleteRecipeIngredient(this.recipeId, ingredientId);

    if (resp) {
      this.loadRecipe();
    }
    else {
      alert("Error Deleting Ingredient, more information in console");
    }
  }
}
