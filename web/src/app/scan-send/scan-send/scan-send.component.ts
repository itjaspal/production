import { Component, OnInit } from '@angular/core';
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
  
  public user: any;
  public toppingList: any = [];
  public model: JobSendSearchView = new JobSendSearchView();
  public pageSizeOptions: number[] = AppSetting.pageSizeOptions;
  public pageEvent: any;
  actions: any = {};
  public data: any = {};


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
    this.data = await this._jobSendSvc.searchcspring(this.model);
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
    
    var datePipe = new DatePipe("en-US");
    console.log(datePipe.transform(this.model.req_date, 'dd/MM/yyyy'));

    this.model.req_date  = datePipe.transform(this.model.req_date, 'dd/MM/yyyy').toString();
    this.data = await this._jobSendSvc.searchcspring(this.model);

    console.log(this.data)
  }

  close() {
    //window.history.back();
    this._router.navigateByUrl('/app/home');
  }

  delete(row: JobSendView) {

  }
}
