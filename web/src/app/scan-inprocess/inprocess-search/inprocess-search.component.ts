import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { DisplayJobSearchView, SearchSpringByDateSearchView, DisplayJobView } from '../../_model/displayJob';
import { CommonSearchView } from '../../_model/common-search-view';
import { DatePipe } from '@angular/common';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { DisplayJobService } from '../../_service/displayJob.service';
import { AuthenticationService } from '../../_service/authentication.service';
import { Router, ActivatedRoute } from '@angular/router';
import { PageEvent } from '@angular/material';
//import moment = require('moment'); 

@Component({
  selector: 'app-inprocess-search',
  templateUrl: './inprocess-search.component.html',
  styleUrls: ['./inprocess-search.component.scss']
})
export class InprocessSearchComponent implements OnInit {

  public model: DisplayJobSearchView = new DisplayJobSearchView();
  public modelByDate: SearchSpringByDateSearchView = new SearchSpringByDateSearchView();
  public data: CommonSearchView<DisplayJobView> = new CommonSearchView<DisplayJobView>();
  public user: any;
  public datePipe = new DatePipe('en-US');
  public validationForm: FormGroup;
  actions: any = {};
  
  @ViewChild('req_date') req_date: ElementRef;

  constructor(
    private _displayJobMacSvc: DisplayJobService,
    private _authSvc: AuthenticationService,
    private _formBuilder: FormBuilder,
    private _actRoute: ActivatedRoute,
    private _router: Router
  ) {
  }

 
  ngOnInit() {
    //this.buildForm();
    this.actions = this._authSvc.getActionAuthorization(this._actRoute);
    this.user = this._authSvc.getLoginUser();
    
    var datePipe = new DatePipe("en-US");
    this.modelByDate.req_date  = datePipe.transform(this._actRoute.snapshot.params.req_date, 'dd/MM/yyyy').toString();

    this.req_date.nativeElement.value = this.modelByDate.req_date;
    this.springSearch();

}

// buildForm() {
//   this.validationForm = this._formBuilder.group({
//     req_date: ['', Validators.compose([Validators.required, YourValidator.dateVaidator])]
//   });
// }

close() {
  //window.history.back();
  this._router.navigateByUrl('/app/mobile-navigator');
}

async springSearch(event: PageEvent = null) {  

  if (event != null) {
    this.model.pageIndex = event.pageIndex;
    this.model.itemPerPage = event.pageSize;
  }

 
      
      this.modelByDate.mc_code  = this.user.user_mac.MC_CODE;
      this.modelByDate.user_id  = this.user.username;
      this.modelByDate.wc_code  = this.user.def_wc_code;
      this.modelByDate.req_date = this._actRoute.snapshot.params.req_date;
     
      this.modelByDate.req_date = this.req_date.nativeElement.value;
      //this.modelByDate.req_date  = datePipe.transform(this.modelByDate.req_date, 'dd/MM/yyyy').toString();
      this.data.datas  = [];

     

      this.data =  await this._displayJobMacSvc.searchJobMacCurrentByDate(this.modelByDate);
      this.req_date.nativeElement.value = this.modelByDate.req_date;

}  

async save() { 

 //let result = await this._productSvc.update(this.model);

 //await this._msgSvc.successPopup("บันทึกข้อมูลเรียบร้อย");
 //this._router.navigateByUrl('/app/product');

 
}

}

// export class YourValidator {
//   static dateVaidator(AC: AbstractControl) {

//     if (AC && AC.value && !moment(AC.value, 'YYYY-MM-DD',true).isValid()) {
//       return {'dateVaidator': true};
//     }
//     return null; 
//   }

// }
