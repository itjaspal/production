import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { DisplayJobService } from '../../_service/displayJob.service';
import { AuthenticationService } from '../../_service/authentication.service';
import { DisplayJobSearchView, DisplayJobView, SearchSpringByDateSearchView } from '../../_model/displayJob';
import { PageEvent } from '@angular/material';
import { DatePipe } from '@angular/common'
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {AbstractControl} from '@angular/forms';
import * as moment from 'moment';
import { CommonSearchView } from '../../_model/common-search-view';
import { Router, ActivatedRoute } from '@angular/router';



@Component({
  selector: 'app-scan-tag',
  templateUrl: './scan-tag.component.html',
  styleUrls: ['./scan-tag.component.scss']
})
export class ScanTagComponent implements OnInit {

  public model: DisplayJobSearchView = new DisplayJobSearchView();
  public modelByDate: SearchSpringByDateSearchView = new SearchSpringByDateSearchView();
 // public data: SearchSpringHeaderView<SearchSpringDetailView> = new SearchSpringHeaderView<SearchSpringDetailView>();
  public data: CommonSearchView<DisplayJobView> = new CommonSearchView<DisplayJobView>();
  public user: any;
  public datePipe = new DatePipe('en-US');
  public validationForm: FormGroup;
 
  @ViewChild('req_date') req_date: ElementRef;


  constructor(
    private _displayJobMacSvc: DisplayJobService,
    private _authSvc: AuthenticationService,
    private _formBuilder: FormBuilder,
    private _actRoute: ActivatedRoute,
    private _router: Router
  ) {
  }

 
  async ngOnInit() {
      this.buildForm();
      this.user = this._authSvc.getLoginUser();
      //this.req_date.nativeElement.value = this.modelByDate.req_date;
      this.springSearch();
      console.log(this._actRoute.snapshot.params.req_date)

      // var datePipe = new DatePipe("en-US");

      // if(this._actRoute.snapshot.params.req_date != null)
      // {
      //     this.modelByDate.mc_code  = this.user.user_mac.MC_CODE;
      //    this.modelByDate.user_id  = this.user.username;
      //    this.modelByDate.wc_code  = this.user.def_wc_code;
      //    this.modelByDate.req_date = this._actRoute.snapshot.params.req_date;
      //    this.modelByDate.req_date  = datePipe.transform(this.modelByDate.req_date, 'dd/MM/yyyy');
      //    this.data.datas  = [];
      //   console.log(this.modelByDate)

      //    this.data =  await this._displayJobMacSvc.searchJobMacCurrentByDate(this.modelByDate);
      //    this.req_date.nativeElement.value = this.modelByDate.req_date;
      // }
        

  }

  buildForm() {
    this.validationForm = this._formBuilder.group({
      req_date: ['', Validators.compose([Validators.required, YourValidator.dateVaidator])]
    });
  }  


 async springSearch(event: PageEvent = null) {  

      if (event != null) {
        this.model.pageIndex = event.pageIndex;
        this.model.itemPerPage = event.pageSize;
      }

      console.log("this.req_date.nativeElement.value : " + this.req_date.nativeElement.value)
      console.log("this.validationForm.valid :  " + this.validationForm.valid)

      //if ((!this.validationForm.valid)&&(this.req_date.nativeElement.value == "")) {
      if (this.req_date.nativeElement.value == "") {  
         console.log("searchSpring");

         this.model.mc_code = this.user.user_mac.MC_CODE;
         this.model.user_id = this.user.username;
         this.model.wc_code = this.user.def_wc_code;
         this.data.datas  = [];

         this.data =  await this._displayJobMacSvc.searchJobByMacCurrent(this.model);

     // } else if ((this.validationForm.valid)&&(this.req_date.nativeElement.value != "")) {
      } else if (this.req_date.nativeElement.value != "") {  
         console.log("searchSpring By Date");

         this.modelByDate.mc_code  = this.user.user_mac.MC_CODE;
         this.modelByDate.user_id  = this.user.username;
         this.modelByDate.wc_code  = this.user.def_wc_code;
         this.modelByDate.req_date = this.req_date.nativeElement.value;
         this.data.datas  = [];

         this.data =  await this._displayJobMacSvc.searchJobMacCurrentByDate(this.modelByDate);
         this.req_date.nativeElement.value = this.modelByDate.req_date;
      }
  }   

  



close() {
  //window.history.back();
  //this._router.navigateByUrl('/app/scantag/'+this.req_date.nativeElement.value);
  this._router.navigateByUrl('/app/mobile-navigator');
}

async save() {

 //let result = await this._productSvc.update(this.model);

 //await this._msgSvc.successPopup("บันทึกข้อมูลเรียบร้อย");
 //this._router.navigateByUrl('/app/product');

 
}

}

export class YourValidator {
  static dateVaidator(AC: AbstractControl) {

    if (AC && AC.value && !moment(AC.value, 'YYYY-MM-DD',true).isValid()) {
      return {'dateVaidator': true};
    }
    return null; 
  }
} 
