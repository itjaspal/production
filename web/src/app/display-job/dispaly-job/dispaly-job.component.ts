
import { Component, OnInit } from '@angular/core';
import { DisplayJobService } from '../../_service/displayJob.service';
import { CommonSearchView } from '../../_model/common-search-view';
import { PageEvent } from '@angular/material';
import { AuthenticationService } from '../../_service/authentication.service';
import { DisplayJobSearchView, DisplayJobView } from '../../_model/displayJob';


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

  constructor(
    private _displayJobMacSvc: DisplayJobService,
    private _authSvc: AuthenticationService,
  ) {
  }

 
  ngOnInit() {
       this.user = this._authSvc.getLoginUser();
       this.searchJobMacCurrent();
       this.searchJobMacPending();
       this.searchJobMacForward();
  }

  close() {
    window.history.back();
  }

  async searchJobMacCurrent(event: PageEvent = null) {  

      if (event != null) {
        this.model.pageIndex = event.pageIndex;
        this.model.itemPerPage = event.pageSize;
      }
      this.dataCurrent =  await this._displayJobMacSvc.searchJobByMacCurrent(this.model);
   }  

  async searchJobMacPending(event: PageEvent = null) { 
    if (event != null) {
      this.model.pageIndex = event.pageIndex;
      this.model.itemPerPage = event.pageSize;
    } 
    this.dataPending =  await this._displayJobMacSvc.searchJobByMacPending(this.model);
  }

  async searchJobMacForward(event: PageEvent = null) {  
    if (event != null) {
      this.model.pageIndex = event.pageIndex;
      this.model.itemPerPage = event.pageSize;
    }
    this.dataForward =  await this._displayJobMacSvc.searchJobByMacForward(this.model);
  }

  async save() {

    //let result = await this._productSvc.update(this.model);

    //await this._msgSvc.successPopup("บันทึกข้อมูลเรียบร้อย");
    //this._router.navigateByUrl('/app/product');

    
  }

}


