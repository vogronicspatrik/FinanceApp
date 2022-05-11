import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {LoginMenuComponent} from './login-menu/login-menu.component';
import {LoginComponent} from './login/login.component';
import {LogoutComponent} from './logout/logout.component';
import {RouterModule} from '@angular/router';
import {ApplicationPaths} from './api-authorization.constants';
import {HttpClientModule} from '@angular/common/http';
import {MaterialModule} from "../app/shared/modules/material/material.module";
import {AuthorizeGuard} from "./authorize.guard";

@NgModule({
  imports: [
    CommonModule,
    HttpClientModule,
    MaterialModule,
    RouterModule.forChild(
      [
        {path: ApplicationPaths.Register, component: LoginComponent},
        {path: ApplicationPaths.Profile, component: LoginComponent},
        {path: ApplicationPaths.Login, component: LoginComponent},
        {path: ApplicationPaths.LoginFailed, component: LoginComponent},
        {path: ApplicationPaths.LoginCallback, component: LoginComponent},
        {path: ApplicationPaths.LogOut, component: LogoutComponent},
        {path: ApplicationPaths.LoggedOut, component: LogoutComponent},
        {path: ApplicationPaths.LogOutCallback, component: LogoutComponent}
      ]
    )
  ],
  declarations: [LoginMenuComponent, LoginComponent, LogoutComponent],
  exports: [LoginMenuComponent, LoginComponent, LogoutComponent]
})
export class ApiAuthorizationModule {
}
