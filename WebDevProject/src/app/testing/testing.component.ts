import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-testing',
  templateUrl: './testing.component.html',
  styleUrls: ['./testing.component.css']
})
export class TestingComponent implements OnInit {

  constructor(private api: ApiService) { }

  ngOnInit(): void {
  }

  async doTheThing() {
    // Create three users
    let register1 = this.api.tryRegister('TestLiam', 'test@gmail.com', 'password123');
    let register2 = this.api.tryRegister('TestJamie', 'test1@gmail.com', 'password123');
    let register3 = this.api.tryRegister('TestDarcy', 'test2@gmail.com', 'password123');

    await register1;
    await register2;
    await register3;

    // Sign In to one user to get authorization
    await this.api.trySignIn('TestLiam', 'password123');

    // Create House
    let house: any = await this.api.createHouse('Test House 1');
    
    // Don't have to join house when you are the creator
    //await this.api.joinHouse(house.houseId);

    // Create Ingredient Types
    let ingredientType1 = await this.api.createIngredientType('Test Ingredient 1');
    let ingredientType2 = await this.api.createIngredientType('Test Ingredient 2');
    let ingredientType3 = await this.api.createIngredientType('Test Ingredient 3');

    // Add Ingredients to current user
    await this.api.addUserIngredient(ingredientType1.ingredientId, 2, 'schmeckles', 2.0);
    await this.api.addUserIngredient(ingredientType2.ingredientId, 2, 'schmeckles', 2.0);

    // Create Recipe
    let recipe1 = await this.api.createRecipe('Test Recipe 1', 'Sample Description 1');
    let recipe2 = await this.api.createRecipe('Test Recipe 2', 'Sample Description 2');
    let recipe3 = await this.api.createRecipe('Test Recipe 3', 'Sample Description 3');

    // Add Ingredients to recipes
    await this.api.addRecipeIngredient(recipe1.recipeId, ingredientType1.ingredientId, 2.0, 'schmeckles');
    await this.api.addRecipeIngredient(recipe2.recipeId, ingredientType2.ingredientId, 1.0, 'schmeckles');

    await this.api.addRecipeIngredient(recipe3.recipeId, ingredientType1.ingredientId, 1.0, 'schmeckles');
    await this.api.addRecipeIngredient(recipe3.recipeId, ingredientType2.ingredientId, 1.0, 'schmeckles');
    await this.api.addRecipeIngredient(recipe3.recipeId, ingredientType3.ingredientId, 1.0, 'schmeckles');

    // Sign out and add other user
    await this.api.signOut(false);
    await this.api.trySignIn('TestDarcy', 'password123');
    await this.api.joinHouse(house.houseId);

    await this.api.addUserIngredient(ingredientType2.ingredientId, 2, 'schmeckles', 2.0);

    await this.api.signOut(false);

    alert("Created Sample Data");
  }

}
