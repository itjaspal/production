//import { UserLoginComponent } from './user-login/user-login.component';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
//import { PCSelectBranchComponent } from './pc-select-branch/pc-select-branch.component';
//import { StockBalanceComponent } from './stock/stock-balance/stock-balance.component';
// import { StockInquiryComponent } from './Stock/stock-inquiry/stock-inquiry.component';
// import { PcsHistoryComponent } from './Stock/pcs-history/pcs-history.component';
// import { DocumentDetailComponent } from './Stock/document-detail/document-detail.component';
// import { StockLocationComponent } from './Stock/stock-location/stock-location.component';
// import { PcsDetailComponent } from './Stock/pcs-detail/pcs-detail.component';
// import { StockBalancesComponent } from './Stock/stock-balances/stock-balances.component';
// import { StockLedgerComponent } from './Stock/stock-ledger/stock-ledger.component';


const AppRoutes: Routes = [
   { path: '', component: HomeComponent },
  //  { path: 'pcsHistory', component: PcsHistoryComponent },
  //  { path: 'docDetail', component: DocumentDetailComponent },
  //  { path: 'stockLocation', component: StockLocationComponent },
  //  { path: 'pcsDetail', component: PcsDetailComponent },
  //  { path: 'stockBalances', component: StockBalancesComponent },
  //  { path: 'stockLedger', component: StockLedgerComponent },
   { path: '', redirectTo: '', pathMatch: 'full' },
   { path: '**', redirectTo: '', pathMatch: 'full' },
   

  //{ path: '', redirectTo: '/app/stock', pathMatch: 'full' },
  //{ path: 'login', component: UserLoginComponent },

 // { path: 'stock', component: StockInquiryComponent },
 // { path: 'pcsHistory', component: PcsHistoryComponent },
  //{ path: 'stock', component: StockBalanceComponent },
  //{ path: 'select-branch', component: PCSelectBranchComponent },
  

  //{ path: '**', redirectTo: '/app/stock', pathMatch: 'full' },
];

export const AppRoutingModule = RouterModule.forRoot(AppRoutes, { useHash: true });

