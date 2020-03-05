
import { Component, OnInit } from '@angular/core';
import { DisplayJobService } from '../../_service/displayJob.service';
import { CommonSearchView } from '../../_model/common-search-view';
import { PageEvent } from '@angular/material';
import { AuthenticationService } from '../../_service/authentication.service';
import { DisplayJobSearchView, DisplayJobView } from '../../_model/displayJob';
import { ActivatedRoute, Router } from '@angular/router';
import { DatePipe } from '@angular/common'


@Component({
  selector: 'app-dispaly-job',
  templateUrl: './dispaly-job.component.html',
  styleUrls: ['./dispaly-job.component.scss']
})
export class DispalyJobComponent implements OnInit { 

  public model: DisplayJobSearchView = new DisplayJobSearchView();
  public dataCurrent: CommonSearchView<DisplayJobView> = new CommonSearchView<DisplayJobView>();
  public dataPending: CommonSearchView<DisplayJobView> = new CommonSearchView<DisplayJobView>();
  public dataForward: CommonSearchView<DisplayJobView> = new CommonSearchView<DisplayJobView>();
  
  public user: any; 
  public datePipe = new DatePipe('en-US'); 

  constructor(
    private _displayJobMacSvc: DisplayJobService,
    private _authSvc: AuthenticationService,
    private _activateRoute: ActivatedRoute,
    private _router: Router,
  ) {
  }

 
  ngOnInit() {
       this.user = this._authSvc.getLoginUser();
       this.searchJobMacCurrent();
       this.searchJobMacPending();
       this.searchJobMacForward();
  }

  close() {
    this._router.navigateByUrl('/app/mobile-navigator');  
  }

  async searchJobMacCurrent(event: PageEvent = null) {   

      if (event != null) {
        this.model.pageIndex = event.pageIndex;
        this.model.itemPerPage = event.pageSize;
      }
      this.model.mc_code = this.user.user_mac.MC_CODE;
      this.model.user_id = this.user.username;
      this.model.wc_code = this.user.def_wc_code;
      this.dataCurrent.datas  = [];

      this.dataCurrent =  await this._displayJobMacSvc.searchJobByMacCurrent(this.model);

      
   }  

  async searchJobMacPending(event: PageEvent = null) {  
    if (event != null) {
      this.model.pageIndex = event.pageIndex;
      this.model.itemPerPage = event.pageSize;
    } 

    this.model.mc_code = this.user.user_mac.MC_CODE;
    this.model.user_id = this.user.username;
    this.model.wc_code = this.user.def_wc_code;
    this.dataPending.datas  = [];

    this.dataPending =  await this._displayJobMacSvc.searchJobByMacPending(this.model);
  }

  async searchJobMacForward(event: PageEvent = null) {  
    if (event != null) {
      this.model.pageIndex = event.pageIndex;
      this.model.itemPerPage = event.pageSize;
    }
    this.model.mc_code = this.user.user_mac.MC_CODE;
    this.model.user_id = this.user.username;
    this.model.wc_code = this.user.def_wc_code;
    this.dataForward.datas  = [];
    
    this.dataForward =  await this._displayJobMacSvc.searchJobByMacForward(this.model);
  }

  async save() {

    //let result = await this._productSvc.update(this.model);

    //await this._msgSvc.successPopup("บันทึกข้อมูลเรียบร้อย");
    //this._router.navigateByUrl('/app/product'); 
    
  }

  getSumTotal(pViewData: string, pSumType: string, pReqDate: string, pSpringGrp: string) : number {
     var  vReqDate = this.datePipe.transform(pReqDate, 'dd/MM/yyyy');
     var  vSumTotal : number = 0;
     //console.log(" vReqDate : " + vReqDate);

     if (pViewData == "current") {

       for (let x of this.dataCurrent.datas) {
           if ((vReqDate == this.datePipe.transform(x.req_date, 'dd/MM/yyyy'))&&(pSpringGrp == x.spring_grp)) {
                if (pSumType == "plan") {
                  vSumTotal = vSumTotal + x.plan_qty;
                } else if (pSumType == "act") {
                  vSumTotal = vSumTotal + x.actual_qty;
                } else {
                  vSumTotal = vSumTotal + x.diff_qty;  
                } 
           }
       }

     }
     else if (pViewData == "pending"){

       for (let x of this.dataPending.datas) {
            if ((vReqDate == this.datePipe.transform(x.req_date, 'dd/MM/yyyy'))&&(pSpringGrp == x.spring_grp)) {
                if (pSumType == "plan") {
                  vSumTotal = vSumTotal + x.plan_qty;
                } else if (pSumType == "act") {
                  vSumTotal = vSumTotal + x.actual_qty;
                } else {
                  vSumTotal = vSumTotal + x.diff_qty;  
                }
            }
       }

     }
     else { // pViewData == "forward"

       for (let x of this.dataForward.datas) {
            if ((vReqDate == this.datePipe.transform(x.req_date, 'dd/MM/yyyy'))&&(pSpringGrp == x.spring_grp)) {
                if (pSumType == "plan") {
                  vSumTotal = vSumTotal + x.plan_qty;
                } else if (pSumType == "act") {
                  vSumTotal = vSumTotal + x.actual_qty;
                } else {
                  vSumTotal = vSumTotal + x.diff_qty;  
                }
           }
       }

     }
    
     return vSumTotal;
  }

}


