import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GlobalguardGuard } from './globalguard.guard';
import { HouseComponent } from './house/house.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';

const routes: Routes = [
  {
    path: 'login', component: LoginComponent
  },
  {
    path: 'register', component: RegisterComponent
  },
  {
    path: 'home', component: HouseComponent, canActivate: [GlobalguardGuard] // bit of a naming error... 
  },
  {
    path: '', redirectTo: '/home', pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
