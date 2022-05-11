import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HttpClientModule, HTTP_INTERCEPTORS, HttpClient} from '@angular/common/http';
import {RouterModule} from '@angular/router';
import {TranslateLoader, TranslateModule} from '@ngx-translate/core';
import {TranslateHttpLoader} from '@ngx-translate/http-loader';
import {MaterialModule} from "./shared/modules/material/material.module";
import {ColorSketchModule} from 'ngx-color/sketch';
import {
  NgxMatDatetimePickerModule,
  NgxMatNativeDateModule,
  NgxMatTimepickerModule
} from '@angular-material-components/datetime-picker';

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
import {HeaderComponent} from './sub-components/header/header.component';
import {NoopAnimationsModule} from '@angular/platform-browser/animations';
import {WalletComponent} from './sub-components/wallet/wallet.component';
import {AddWalletComponent} from './sub-components/add-wallet/add-wallet.component';
import {WalletTransactionsComponent} from './wallet-transactions/wallet-transactions.component';
import {AddTransactionComponent} from './sub-components/add-transaction/add-transaction.component';
import {AddValuableItemComponent} from './sub-components/add-valuable-item/add-valuable-item.component';
import {ValuableItemComponent} from './sub-components/valuable-item/valuable-item.component';
import {RegularCashFlowDashboardComponent} from './regular-cash-flow-dashboard/regular-cash-flow-dashboard.component';
import { AddCashFlowItemComponent } from './sub-components/add-cash-flow-item/add-cash-flow-item.component';
import { CashFlowItemComponent } from './sub-components/cash-flow-item/cash-flow-item.component';
import {RecurrencyPeriodPipe} from "./models/RecurrencyPeriod";
import { LoanComponent } from './sub-components/loan/loan.component';
import { BondsDashboardComponent } from './bonds-dashboard/bonds-dashboard.component';
import { AddBondComponent } from './sub-components/add-bond/add-bond.component';
import { BondComponent } from './sub-components/bond/bond.component';
import {ReturnIntervalPipe} from "./models/Bond";
import {AddStockComponent} from './sub-components/add-stock/add-stock.component';
import {StockComponent} from './sub-components/stock/stock.component';
import {StockDashboardComponent} from './stock-dashboard/stock-dashboard.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    SidebarComponent,
    SettingsComponent,
    WalletDashboardComponent,
    ValuableItemsDashboardComponent,
    HeaderComponent,
    WalletComponent,
    AddWalletComponent,
    WalletTransactionsComponent,
    AddTransactionComponent,
    AddValuableItemComponent,
    ValuableItemComponent,
    RegularCashFlowDashboardComponent,
    AddCashFlowItemComponent,
    CashFlowItemComponent,
    RecurrencyPeriodPipe,
    ReturnIntervalPipe,
    LoanComponent,
    BondsDashboardComponent,
    AddBondComponent,
    BondComponent,
    AddStockComponent,
    StockDashboardComponent,
    StockComponent
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
      {path: 'investments/bonds', component: BondsDashboardComponent, canActivate: [AuthorizeGuard]},
      {path: 'valuable_items', component: ValuableItemsDashboardComponent, canActivate: [AuthorizeGuard]},
      {path: 'investments/stocks', component: StockDashboardComponent, canActivate: [AuthorizeGuard]},
      {path: 'regular_cash_flow', component: RegularCashFlowDashboardComponent, canActivate: [AuthorizeGuard]},
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
