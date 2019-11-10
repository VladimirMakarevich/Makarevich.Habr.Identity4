import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DataComponent } from './data/data.component';
import { HomeComponent } from './home/home.component';
import { AuthorizeGuard } from './api-authorization/authorize.guard';
import { LoginAccountComponent } from './account/components/account/login/login-account.component';
import { UnauthorizedComponent } from './account/components/unauthorized/unauthorized.component';
import { ForbiddenComponent } from './account/components/forbidden/forbidden.component';


const routes: Routes = [
  {path: '', component: HomeComponent, pathMatch: 'full'},
  {path: 'data', component: DataComponent, canActivate: [AuthorizeGuard]},
  {
    path: 'forbidden',
    component: ForbiddenComponent,
  },
  {
    path: 'unauthorized',
    component: UnauthorizedComponent,
  },
  {
    path: 'account/login',
    component: LoginAccountComponent,
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
