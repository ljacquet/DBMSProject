import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-recipes',
  templateUrl: './recipes.component.html',
  styleUrls: ['./recipes.component.css']
})
export class RecipesComponent implements OnInit {

  constructor(private api: ApiService, private router: Router) { }

  recipes: any[] = [];

  async ngOnInit(): Promise<void> {
    this.recipes = await this.api.getRecipes();
    console.log(this.recipes);
  }

  async newRecipe(name: string, description: string) {
    if (name.length < 0 || description.length < 0) {
      alert("Please enter a name and description");
    }
    else {
      let resp = await this.api.createRecipe(name, description);

      if (resp == null) {
        alert("Error Creating Recipe");
      }
      else {
        this.router.navigate(['recipe', resp.recipeId]);
      }
    }
  }

  async deleteRecipe(recipeId: number) {
    let resp = await this.api.deleteRecipe(recipeId);

    if (resp == null) {
      alert("Error Deleting Recipe, see console for details");
    }
    else {
      this.recipes = await this.api.getRecipes();
    }
  }

}
