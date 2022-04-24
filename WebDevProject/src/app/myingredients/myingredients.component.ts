import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-myingredients',
  templateUrl: './myingredients.component.html',
  styleUrls: ['./myingredients.component.css']
})
export class MyingredientsComponent implements OnInit {

  ingredientTypes: any[] = [];
  validIngredients: any[] = [];
  ingredients: any[] = [];

  constructor(private api: ApiService) { }

  async ngOnInit(): Promise<void> {
    // load valid ingredients
    this.updateIngredients();

  }

  private async updateIngredients() {
    this.ingredients = await this.api.getMyIngredients();

    // Get ids for comparison
    let ingredientIds = this.ingredients.map(x => x.ingredientId);

    this.ingredientTypes = await this.api.getValidIngredients();

    // Remove ingredients for adding that they have already added
    this.validIngredients =  (this.ingredientTypes).filter((x) => {
      return ingredientIds.indexOf(x.ingredientId) == -1;
    });
    
    console.log(this.ingredients);
  }

  async addIngredient(ingredientId: string, amount: string, amountUnit: string, price: string) {
    await this.api.addUserIngredient(Number.parseInt(ingredientId), Number.parseFloat(amount), amountUnit, Number.parseFloat(price));
    this.updateIngredients(); // Realistically there are better ways to do this but we are short for time
  }

  async updateIngredient(ingredientId: string, amount: string, amountUnit: string, price: string) {
    await this.api.updateUserIngredient(Number.parseInt(ingredientId), Number.parseFloat(amount), amountUnit, Number.parseFloat(price));
    this.updateIngredients(); // Realistically there are better ways to do this but we are short for time
  }

  async deleteIngredient(ingredientId: string) {
    await this.api.deleteUserIngredient(Number.parseInt(ingredientId));
    this.updateIngredients(); // Realistically there are better ways to do this but we are short for time
  }

  async createIngredientType(ingredientName: string) {
    await this.api.createIngredientType(ingredientName);
    this.updateIngredients();
  }

  async deleteIngredientType(ingredientId: number) {
    await this.api.deleteIngredientType(ingredientId);
    this.updateIngredients();
  }
}
