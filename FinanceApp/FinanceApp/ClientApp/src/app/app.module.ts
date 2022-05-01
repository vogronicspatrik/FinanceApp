import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HttpClientModule, HTTP_INTERCEPTORS, HttpClient} from '@angular/common/http';
import {RouterModule} from '@angular/router';
import {TranslateLoader, TranslateModule} from '@ngx-translate/core';
import {TranslateHttpLoader} from '@ngx-translate/http-loader';
import {MaterialModule} from "./shared/modules/material/material.module";
import {ColorSketchModule} from 'ngx-color/sketch';

import {AppComponent} from './app.component';
import {NavMenuComponent} from './nav-menu/nav-menu.component';
import {HomeComponent} from './home/home.component';
import {ApiAuthorizationModule} from 'src/api-authorization/api-authorization.module';
import {AuthorizeGuard} from 'src/api-authorization/authorize.guard';
import {AuthorizeInterceptor} from 'src/api-authorization/authorize.interceptor';
import {SidebarComponent} from './sidebar/sidebar.component';
import {WalletDashboardComponent} from './wallet-dashboard/wallet-dashboard.component';
import {SettingsComponent} from './settings/settings.component';
import {ValuableItemsDashboardComponent} from './valuable-items-dashboard/valuable-items-dashboard.component';
import {FixCostsDashboardComponent} from './fix-costs-dashboard/fix-costs-dashboard.component';
import {HeaderComponent} from './sub-components/header/header.component';
import {NoopAnimationsModule} from '@angular/platform-browser/animations';
import {WalletComponent} from './sub-components/wallet/wallet.component';
import {AddWalletComponent} from './sub-components/add-wallet/add-wallet.component';
import {WalletTransactionsComponent} from './wallet-transactions/wallet-transactions.component';
import {AddTransactionComponent} from './sub-components/add-transaction/add-transaction.component';

import {
  NgxMatDatetimePickerModule,
  NgxMatNativeDateModule,
  NgxMatTimepickerModule
} from '@angular-material-components/datetime-picker';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    SidebarComponent,
    SettingsComponent,
    WalletDashboardComponent,
    ValuableItemsDashboardComponent,
    FixCostsDashboardComponent,
    HeaderComponent,
    WalletComponent,
    AddWalletComponent,
    WalletTransactionsComponent,
    AddTransactionComponent
  ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    ApiAuthorizationModule,
    MaterialModule,
    ColorSketchModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: httpTranslateLoader,
        deps: [HttpClient]
      }
    }),
    RouterModule.forRoot([
      {path: 'dashboard', component: HomeComponent, canActivate: [AuthorizeGuard]},
      {path: 'settings', component: SettingsComponent, canActivate: [AuthorizeGuard]},
      {path: 'wallets', component: WalletDashboardComponent, canActivate: [AuthorizeGuard]},
      {path: 'wallets/:id', component: WalletTransactionsComponent, canActivate: [AuthorizeGuard]},
      {path: 'investments/stocks', component: HomeComponent, canActivate: [AuthorizeGuard]},
      {path: 'valuable_items', component: ValuableItemsDashboardComponent, canActivate: [AuthorizeGuard]},
      {path: 'fix_costs', component: FixCostsDashboardComponent, canActivate: [AuthorizeGuard]},
      {path: '', redirectTo: 'dashboard', pathMatch: 'full'}
    ]),
    NoopAnimationsModule,
    NgxMatDatetimePickerModule,
    NgxMatNativeDateModule,
    NgxMatTimepickerModule,
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}

export function httpTranslateLoader(http: HttpClient) {
  return new TranslateHttpLoader(http);
}
