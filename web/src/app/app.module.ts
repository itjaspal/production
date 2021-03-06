import { CommonService } from './_service/common.service';
import { DropdownlistService } from './_service/dropdownlist.service';
import { NgModule, ApplicationRef } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import {
  MatAutocompleteModule,
  MatButtonModule,
  MatButtonToggleModule,
  MatCardModule,
  MatCheckboxModule,
  MatChipsModule,
  MatDatepickerModule,
  MatDialogModule,
  MatExpansionModule,
  MatGridListModule,
  MatIconModule,
  MatInputModule,
  MatListModule,
  MatMenuModule,
  MatNativeDateModule,
  MatPaginatorModule,
  MatProgressBarModule,
  MatProgressSpinnerModule,
  MatRadioModule,
  MatRippleModule,
  MatSelectModule,
  MatSidenavModule,
  MatSliderModule,
  MatSlideToggleModule,
  MatSnackBarModule,
  MatSortModule,
  MatTableModule,
  MatTabsModule,
  MatToolbarModule,
  MatTooltipModule,
  MatStepperModule,
  MAT_DATE_FORMATS,
  DateAdapter
} from '@angular/material';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
// Layout
import { LayoutComponent } from './layout/layout.component';
import { PreloaderDirective } from './layout/preloader.directive';
// Header
import { AppHeaderComponent } from './layout/header/header.component';
// Sidenav
import { AppSidenavComponent } from './layout/sidenav/sidenav.component';
import { ToggleOffcanvasNavDirective } from './layout/sidenav/toggle-offcanvas-nav.directive';
import { AutoCloseMobileNavDirective } from './layout/sidenav/auto-close-mobile-nav.directive';
import { AppSidenavMenuComponent } from './layout/sidenav/sidenav-menu/sidenav-menu.component';
import { AccordionNavDirective } from './layout/sidenav/sidenav-menu/accordion-nav.directive';
import { AppendSubmenuIconDirective } from './layout/sidenav/sidenav-menu/append-submenu-icon.directive';
import { HighlightActiveItemsDirective } from './layout/sidenav/sidenav-menu/highlight-active-items.directive';
// Customizer
import { AppCustomizerComponent } from './layout/customizer/customizer.component';
import { ToggleQuickviewDirective } from './layout/customizer/toggle-quickview.directive';
// Footer
import { AppFooterComponent } from './layout/footer/footer.component';
// Search Overaly
import { AppSearchOverlayComponent } from './layout/search-overlay/search-overlay.component';
import { SearchOverlayDirective } from './layout/search-overlay/search-overlay.directive';
import { OpenSearchOverlaylDirective } from './layout/search-overlay/open-search-overlay.directive';

// Sub modules
import { LayoutModule } from './layout/layout.module';
import { SharedModule } from './shared/shared.module';

//3rd party
import { NgxMatSelectSearchModule } from 'ngx-mat-select-search';
import { ZXingScannerModule } from '@zxing/ngx-scanner';

// hmr
import { removeNgStyles, createNewHosts } from '@angularclass/hmr';
import { AppMobileFooterComponent } from './layout/mobile-footer/mobile-footer.component';
import { AppMobileHeaderComponent } from './layout/mobile-header/mobile-header.component';

//http interceptor
import { HTTP_INTERCEPTORS, HttpClient, HttpClientModule } from '@angular/common/http';
import { HttpModule } from '@angular/http';
import { TokenInterceptor } from './_service/token.interceptor';

import { CustomDateAdapter, APP_DATE_FORMATS } from './_common/custom-date-adapter';

// Pages

import { MobileMenuComponent } from './mobile-menu/mobile-menu.component';
import { UserLoginComponent } from './user-login/user-login.component';
import { BranchGroupSearchComponent } from './master-branch-group/branch-group-search/branch-group-search.component';
import { BranchGroupCreateComponent } from './master-branch-group/branch-group-create/branch-group-create.component';
import { BranchGroupUpdateComponent } from './master-branch-group/branch-group-update/branch-group-update.component';
import { BranchGroupViewComponent } from './master-branch-group/branch-group-view/branch-group-view.component';
import { DepartmentSearchComponent } from './master-department/department-search/department-search.component';
import { DepartmentCreateComponent } from './master-department/department-create/department-create.component';
import { DepartmentUpdateComponent } from './master-department/department-update/department-update.component';
import { DepartmentViewComponent } from './master-department/department-view/department-view.component';

//service

import { LoaderService } from './_service/loader.service';
import { BranchSearchComponent } from './master-branch/branch-search/branch-search.component';
import { BranchSaveComponent } from './master-branch/branch-save/branch-save.component';
import { BranchViewComponent } from './master-branch/branch-view/branch-view.component';
import { MobileProfileComponent } from './mobile-profile/mobile-profile.component';
import { InqUserComponent } from './master-user/inq/inq-user.component';
import { UserService } from './_service/user.service';
import { CreateUserComponent } from './master-user/create/create-user.component';
import { PopupMessageComponent } from './modal/message/popup-message.component';
import { MessageService } from './_service/message.service';
import { ViewUserComponent } from './master-user/view/view-user.component';
import { UpdateUserComponent } from './master-user/update/update-user.component';
import { DisableControlDirective } from './_directive/disable-control.component';
import { ResetPasswordUserComponent } from './master-user/reset-password/reset-user.component';
import { UserRoleService } from './_service/user-role.service';
import { InqUserRoleComponent } from './master-user-role/inq/inq-user-role.component';
import { CreateUserRoleComponent } from './master-user-role/create/create-user-role.component';
import { ViewUserRoleComponent } from './master-user-role/view/view-user-role.component';
import { UpdateUserRoleComponent } from './master-user-role/update/update-user-role.component';

import { ChangePasswordComponent } from './master-user/change-password/change-password.component';
import { HomeComponent } from './home/home.component';

import { ListFilterPipe } from './_pipe/list-filter.pipe';
import { HighlightPipe } from './_pipe/highlight.pipe';

import { ConfirmMessageComponent } from './modal/confirm-message/confirm-message.component';

import { BuilderFilterPipe } from './_pipe/array-filter.pipe';

import { MenuGroupCreateComponent } from './master-menu-group/menu-group-create/menu-group-create.component';
import { MenuGroupSearchComponent } from './master-menu-group/menu-group-search/menu-group-search.component';
import { MenuGroupUpdateComponent } from './master-menu-group/menu-group-update/menu-group-update.component';
import { MenuGroupViewComponent } from './master-menu-group/menu-group-view/menu-group-view.component';
import { MenuGroupService } from './_service/menu-group.service';
import { MenuCreateComponent } from './master-menu/menu-create/menu-create.component';
import { MenuSearchComponent } from './master-menu/menu-search/menu-search.component';
import { MenuUpdateComponent } from './master-menu/menu-update/menu-update.component';
import { MenuViewComponent } from './master-menu/menu-view/menu-view.component';
import { MenuService } from './_service/menu.service';
//import { StockService } from './_service/stock.service';
import { DefaultPrinterService } from './_service/default-printer.service';
import { DefaultPrinterComponent } from './default-printer/default-printer.component';
import { ScanTagComponent } from './scan-tag/scan-tag/scan-tag.component';
import { PrintTagComponent } from './scan-tag/print-tag/print-tag.component';
import { DispalyJobComponent } from './display-job/dispaly-job/dispaly-job.component';
import { ViewSpecComponent } from './view-spec/view-spec/view-spec.component';
import { ScanInprocessComponent } from './scan-inprocess/scan-inprocess/scan-inprocess.component';
import { ScanSendComponent } from './scan-send/scan-send/scan-send.component';
import { HomeAutoComponent } from './home-auto/home-auto.component';
import { ProductionCancelComponent } from './scan-inprocess/production-cancel/production-cancel.component';
import { ProductionScanComponent } from './scan-inprocess/production-scan/production-scan.component';
import { SendProdCancelComponent } from './scan-send/send-prod-cancel/send-prod-cancel.component';
import { SendProdScanComponent } from './scan-send/send-prod-scan/send-prod-scan.component';
import { ViewSpceDrawingComponent } from './view-spec/view-spce-drawing/view-spce-drawing.component';
import { BranchSearchAssignProductComponent } from './master-branch/branch-search-assignProduct/branch-search-assignProduct.component';
import { RawmatSearchComponent } from './scan-tag/rawmat-search/rawmat-search.component';
import { ProductionRecordEntryComponent } from './scan-inprocess/production-record-entry/production-record-entry.component';
import { ProductionRecordCancelComponent } from './scan-inprocess/production-record-cancel/production-record-cancel.component';
import { ProdRecordEntryComponent } from './scan-send/prod-record-entry/prod-record-entry.component';
import { ProdRecordCancelComponent } from './scan-send/prod-record-cancel/prod-record-cancel.component';
import { DisplayJobService } from './_service/displayJob.service';
import { JobinprocessService } from './_service/job-inprocess.service';
import { JobSendService } from './_service/job-send.service';
import { PrintTagService } from './_service/print-tag.service';
import { ViewSpecService } from './_service/view-spec.service';
import { SendSearchComponent } from './scan-send/send-search/send-search.component';
import { InprocessSearchComponent } from './scan-inprocess/inprocess-search/inprocess-search.component';
import { TagSearchComponent } from './scan-tag/tag-search/tag-search.component';
import { ViewSpecPictComponent } from './view-spec/view-spec-pict/view-spec-pict.component';
import { PrintTagScanComponent } from './scan-tag/print-tag-scan/print-tag-scan.component';





@NgModule({
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule.withConfig({ warnOnNgModelWithFormControl: 'never' }),
    BrowserAnimationsModule,
    MatAutocompleteModule,
    MatButtonModule,
    MatButtonToggleModule,
    MatCardModule,
    MatCheckboxModule,
    MatChipsModule,
    MatStepperModule,
    MatDatepickerModule,
    MatDialogModule,
    MatExpansionModule,
    MatGridListModule,
    MatIconModule,
    MatInputModule,
    MatListModule,
    MatMenuModule,
    MatNativeDateModule,
    MatPaginatorModule,
    MatProgressBarModule,
    MatProgressSpinnerModule,
    MatRadioModule,
    MatRippleModule,
    MatSelectModule,
    MatSidenavModule,
    MatSliderModule,
    MatSlideToggleModule,
    MatSnackBarModule,
    MatSortModule,
    MatTableModule,
    MatTabsModule,
    MatToolbarModule,
    MatTooltipModule,
    AppRoutingModule,
    NgbModule,

    // Sub modules
    LayoutModule,
    SharedModule,

    //3rd party
    NgxMatSelectSearchModule,
    ZXingScannerModule.forRoot()
  ],
  providers: [
    // AuthGuard,
    LoaderService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    },
    {
      provide: DateAdapter, useClass: CustomDateAdapter
    },
    {
      provide: MAT_DATE_FORMATS, useValue: APP_DATE_FORMATS
    },


    //OrganizationService,
    DropdownlistService,
    UserService,
    UserRoleService,
    MessageService,
    CommonService,
   
    MenuGroupService,
    MenuService,
    //StockService,
    DefaultPrinterService,
    DisplayJobService,
    JobinprocessService,
    JobSendService,
    PrintTagService,
    ViewSpecService,

  ],
  declarations: [
    DisableControlDirective,
    AppComponent,
    // Layout
    LayoutComponent,
    PreloaderDirective,
    // Header
    AppHeaderComponent,
    AppMobileHeaderComponent,
    // Sidenav
    AppSidenavComponent,
    ToggleOffcanvasNavDirective,
    AutoCloseMobileNavDirective,
    AppSidenavMenuComponent,
    AccordionNavDirective,
    AppendSubmenuIconDirective,
    HighlightActiveItemsDirective,
    // Customizer
    AppCustomizerComponent,
    ToggleQuickviewDirective,
    // Footer
    AppFooterComponent,
    AppMobileFooterComponent,
    // Search overlay
    AppSearchOverlayComponent,
    SearchOverlayDirective,
    OpenSearchOverlaylDirective,
    // pipe
    ListFilterPipe,
    //
    
    MobileMenuComponent,
    MobileProfileComponent,
    UserLoginComponent,
    BranchGroupSearchComponent,
    BranchGroupCreateComponent,
    BranchGroupUpdateComponent,
    BranchGroupViewComponent,
    BranchSearchAssignProductComponent,
    DepartmentSearchComponent,
    DepartmentCreateComponent,
    DepartmentUpdateComponent,
    DepartmentViewComponent,
    BranchSearchComponent,
    BranchSaveComponent,
    BranchViewComponent,

    //User Component
    InqUserComponent,
    CreateUserComponent,
    ViewUserComponent,
    UpdateUserComponent,
    ResetPasswordUserComponent,
    ChangePasswordComponent,

    //User Role Component
    InqUserRoleComponent,
    CreateUserRoleComponent,
    ViewUserRoleComponent,
    UpdateUserRoleComponent,

    
    PopupMessageComponent,
    ConfirmMessageComponent,
    
    HomeComponent,

    
    ListFilterPipe,

    
    HighlightPipe,

    

    BuilderFilterPipe,
    
    
    MenuGroupCreateComponent,
    MenuGroupSearchComponent,
    MenuGroupUpdateComponent,
    MenuGroupViewComponent,
    MenuCreateComponent,
    MenuSearchComponent,
    MenuUpdateComponent,
    MenuViewComponent,
    DefaultPrinterComponent,
    ScanTagComponent,
    PrintTagComponent,
    DispalyJobComponent,
    ViewSpecComponent,
    ScanInprocessComponent,
    ScanSendComponent,
    HomeAutoComponent,
    ProductionCancelComponent,
    ProductionScanComponent,
    SendProdCancelComponent,
    SendProdScanComponent,
    ViewSpceDrawingComponent,
    RawmatSearchComponent,
    ProductionRecordEntryComponent,
    ProductionRecordCancelComponent,
    ProdRecordEntryComponent,
    ProdRecordCancelComponent,
    SendSearchComponent,
    InprocessSearchComponent,
    TagSearchComponent,
    ViewSpecPictComponent,
    PrintTagScanComponent,

  
  
    
  ],
  entryComponents: [
    PopupMessageComponent,
    ConfirmMessageComponent,
    RawmatSearchComponent
  ],
  bootstrap: [AppComponent]
})

export class AppModule {
  constructor(public appRef: ApplicationRef) { }
  hmrOnInit(store) {
    console.log('HMR store', store);
  }
  hmrOnDestroy(store) {
    const cmpLocation = this.appRef.components.map((cmp) => cmp.location.nativeElement);
    // recreate elements
    store.disposeOldHosts = createNewHosts(cmpLocation);
    // remove styles
    removeNgStyles();
  }
  hmrAfterDestroy(store) {
    // display new elements
    store.disposeOldHosts();
    delete store.disposeOldHosts;
  }
}
