import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { AuthenticationService } from '../../_service/authentication.service';
import { ActivatedRoute, Router } from '@angular/router';
import { JobSendService } from '../../_service/job-send.service';
import { JobSendSearchView, JobSendView } from '../../_model/job-send';
import { AppSetting } from '../../_constants/app-setting';
import { PageEvent } from '@angular/material';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-send-search',
  templateUrl: './send-search.component.html',
  styleUrls: ['./send-search.component.scss']
})
export class SendSearchComponent implements OnInit {

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


  //public data: CommonSearchView<StockView> = new CommonSearchView<StockView>();

  async ngOnInit() {
    this.actions = this._authSvc.getActionAuthorization(this._actRoute);
    this.user = this._authSvc.getLoginUser();
    this.model.wc_code =  this.user.def_wc_code;
    this.model.mc_code =  this.user.user_mac.MC_CODE;
    this.model.req_date = this._actRoute.snapshot.params.req_date;
    
    this.req_date.nativeElement.value = this.model.req_date;
    this.search();
    
    
  }

//   async searchSpringData(event: PageEvent = null) {  

//     if (event != null) {
//       this.model.pageIndex = event.pageIndex;
//       this.model.itemPerPage = event.pageSize;
//     }
//     this.data = await this._jobSendSvc.searchcspring(this.model);
//  }  

 
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
    //this.model.req_date  = datePipe.transform(this._actRoute.snapshot.params.req_date, 'dd/MM/yyyy').toString();
    this.model.req_date  = datePipe.transform(this.model.req_date, 'dd/MM/yyyy').toString();
    //this.model.req_date = this.req_date.nativeElement.value;
    this.data = await this._jobSendSvc.searchcspring(this.model);
    this.req_date.nativeElement.value = this.model.req_date;
    
    
  }

  close() {
    //window.history.back();
    //console.log(this.model.req_date);
    this._router.navigateByUrl('/app/mobile-navigator');
  }

  delete(row: JobSendView) {

  }

}
