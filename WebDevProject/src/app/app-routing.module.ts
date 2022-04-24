import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EditRecipeComponent } from './edit-recipe/edit-recipe.component';
import { GlobalguardGuard } from './globalguard.guard';
import { HouseComponent } from './house/house.component';
import { IngredientsComponent } from './ingredients/ingredients.component';
import { LoginComponent } from './login/login.component';
import { MyingredientsComponent } from './myingredients/myingredients.component';
import { RecipesComponent } from './recipes/recipes.component';
import { RegisterComponent } from './register/register.component';
import { TestingComponent } from './testing/testing.component';

const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'register',
    component: RegisterComponent,
  },
  {
    path: 'home',
    component: HouseComponent,
    canActivate: [GlobalguardGuard], // bit of a naming error...
  },
  {
    path: 'ingredients',
    component: MyingredientsComponent,
    canActivate: [GlobalguardGuard]
  },
  {
    path: 'ingredients/:id',
    component: IngredientsComponent,
    canActivate: [GlobalguardGuard]
  },
  {
    path: 'recipes',
    component: RecipesComponent,
    canActivate: [GlobalguardGuard]
  },
  {
    path: 'recipe/:id',
    component: EditRecipeComponent,
    canActivate: [GlobalguardGuard]
  },
  {
    path: 'testing',
    component: TestingComponent
  },
  {
    path: '',
    redirectTo: '/home',
    pathMatch: 'full',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
