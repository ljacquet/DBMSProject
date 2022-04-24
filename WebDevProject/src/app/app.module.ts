import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthenticationComponent } from './authentication/authentication.component';
import { HouseComponent } from './house/house.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { HttpClientModule } from '@angular/common/http';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { IngredientsComponent } from './ingredients/ingredients.component';
import { MyingredientsComponent } from './myingredients/myingredients.component';
import { RecipesComponent } from './recipes/recipes.component';
import { EditRecipeComponent } from './edit-recipe/edit-recipe.component';
import { TestingComponent } from './testing/testing.component';

@NgModule({
  declarations: [
    AppComponent,
    AuthenticationComponent,
    HouseComponent,
    LoginComponent,
    RegisterComponent,
    IngredientsComponent,
    MyingredientsComponent,
    RecipesComponent,
    EditRecipeComponent,
    TestingComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    HttpClientModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
