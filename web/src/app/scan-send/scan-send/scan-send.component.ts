import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { AuthenticationService } from '../../_service/authentication.service';
import { ActivatedRoute, Router } from '@angular/router';
import { JobSendService } from '../../_service/job-send.service';
import { JobSendSearchView, JobSendView } from '../../_model/job-send';
import { AppSetting } from '../../_constants/app-setting';
import { DatePipe } from '@angular/common';
import { PageEvent } from '@angular/material';


@Component({
  selector: 'app-scan-send',
  templateUrl: './scan-send.component.html',
  styleUrls: ['./scan-send.component.scss']
})
export class ScanSendComponent implements OnInit {

  constructor(
    private _authSvc: AuthenticationService,
    private _actRoute: ActivatedRoute,
    private _jobSendSvc: JobSendService,
    private _router: Router
  ) { }

  @ViewChild('req_date') req_date: ElementRef;
  
  public user: any;
  public toppingList: any = [];
  public model: JobSendSearchView = new JobSendSearchView();
  public pageSizeOptions: number[] = AppSetting.pageSizeOptions;
  public pageEvent: any;
  actions: any = {};
  public data: any = {};
  public datePipe = new DatePipe("en-US");

  //public data: CommonSearchView<StockView> = new CommonSearchView<StockView>();

  async ngOnInit() {
    this.actions = this._authSvc.getActionAuthorization(this._actRoute);
    this.user = this._authSvc.getLoginUser();
    this.model.wc_code =  this.user.def_wc_code;
    this.model.mc_code =  this.user.user_mac.MC_CODE;
    this.model.req_date = "";
    

    this.searchSpringData(); 
    
  }

  async searchSpringData(event: PageEvent = null) {  

    if (event != null) {
      this.model.pageIndex = event.pageIndex;
      this.model.itemPerPage = event.pageSize;
    }
    //this.model.req_date  = this.datePipe.transform(this.model.req_date, 'dd/MM/yyyy').toString();
    
    this.data = await this._jobSendSvc.searchcspring(this.model);
    
    console.log(this.model.req_date)
    this.req_date.nativeElement.value = this.model.req_date;
 }  

 
  async ngOnDestroy() {
    this.saveSession();
  }

  async saveSession() {
    sessionStorage.setItem('session-scansend-search', JSON.stringify(this.model));
  }

  async search(event: PageEvent = null)
  {
    if (event != null) {
      this.model.pageIndex = event.pageIndex;
      this.model.itemPerPage = event.pageSize;
    }
    this.model.wc_code =  this.user.def_wc_code;
    this.model.mc_code =  this.user.user_mac.MC_CODE;
    
    //var datePipe = new DatePipe("en-US");
    //console.log(datePipe.transform(this.model.req_date, 'dd/MM/yyyy')); 
    console.log(this.model.req_date)
    this.model.req_date  = this.req_date.nativeElement.value;
    console.log(this.model.req_date)

    this.data = await this._jobSendSvc.searchcspring(this.model);

    this.req_date.nativeElement.value = this.model.req_date;
    
  }

  close() {
    //window.history.back();
    this._router.navigateByUrl('/app/mobile-navigator');
  }

  delete(row: JobSendView) {

  }
}
