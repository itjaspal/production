import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { AuthenticationService } from '../../_service/authentication.service';
import { PageEvent } from '@angular/material';
import { DatePipe } from '@angular/common'
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {AbstractControl} from '@angular/forms';
import * as moment from 'moment';
import { JobSendSearchView } from '../../_model/job-send';
import { JobSendService } from '../../_service/job-send.service';
import { ActivatedRoute, Router } from '@angular/router';


@Component({
  selector: 'app-view-spec',
  templateUrl: './view-spec.component.html',
  styleUrls: ['./view-spec.component.scss']
})
export class ViewSpecComponent implements OnInit {

  public model: JobSendSearchView = new JobSendSearchView();
  public data: any = {};

  
  public user: any;
  public datePipe = new DatePipe('en-US');
  public validationForm: FormGroup;

  @ViewChild('req_date') req_date: ElementRef;

  constructor(
    private _jobSendSvc: JobSendService,
    private _authSvc: AuthenticationService,
    private _formBuilder: FormBuilder,
    private _router: Router,
  ) {
  } 

 
ngOnInit() {
    this.buildForm();

    this.req_date.nativeElement.value = sessionStorage.getItem('spect-drawing-reqDate');
    
    
    this.user = this._authSvc.getLoginUser();
    this.model.wc_code =  this.user.def_wc_code;
    this.model.mc_code =  this.user.user_mac.MC_CODE;
    this.springSearch();
} 

buildForm() {
  this.validationForm = this._formBuilder.group({
    req_date: ['', Validators.compose([Validators.required, YourValidator.dateVaidator])]
  });
}

close() {
  this._router.navigateByUrl('/app/mobile-navigator');  
}

ngOnDestroy() {
  //console.log("Close Program ");
  sessionStorage.removeItem('spect-drawing-reqDate');
  sessionStorage.removeItem('spect-drawing-pcsBarcode');
}

async springSearch(event: PageEvent = null) {  

  if (event != null) {
    this.model.pageIndex = event.pageIndex;
    this.model.itemPerPage = event.pageSize;
  }

  //console.log("this.req_date.nativeElement.value : " + this.req_date.nativeElement.value)
  //console.log("this.validationForm.valid :  " + this.validationForm.valid)
  sessionStorage.setItem('spect-drawing-reqDate', this.req_date.nativeElement.value);
  if (this.req_date.nativeElement.value == "") {
     console.log("viewSpceSearch");
     
     this.model.req_date = "";
     this.data =  await this._jobSendSvc.searchcspring(this.model);

  } else if (this.req_date.nativeElement.value != "") { 
     console.log("viewSpceSearch By Date");
     this.model.req_date = this.req_date.nativeElement.value;
     this.data =  await this._jobSendSvc.searchcspring(this.model);
     this.req_date.nativeElement.value = this.model.req_date;

  }

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

