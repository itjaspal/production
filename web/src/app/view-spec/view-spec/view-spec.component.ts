import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { DisplayJobSearchView, SearchSpringByDateSearchView, SearchSpringHeaderView, SearchSpringDetailView } from '../../_model/displayJob';
import { DisplayJobService } from '../../_service/displayJob.service';
import { AuthenticationService } from '../../_service/authentication.service';
import { PageEvent } from '@angular/material';
import { DatePipe } from '@angular/common'
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {AbstractControl} from '@angular/forms';
import * as moment from 'moment';


@Component({
  selector: 'app-view-spec',
  templateUrl: './view-spec.component.html',
  styleUrls: ['./view-spec.component.scss']
})
export class ViewSpecComponent implements OnInit {

  public model: DisplayJobSearchView = new DisplayJobSearchView();
  public modelByDate: SearchSpringByDateSearchView = new SearchSpringByDateSearchView();
  public data: SearchSpringHeaderView<SearchSpringDetailView> = new SearchSpringHeaderView<SearchSpringDetailView>();
  //public data: CommonSearchView<DisplayJobView> = new CommonSearchView<DisplayJobView>();
  
  public user: any;
  public datePipe = new DatePipe('en-US');
  public validationForm: FormGroup;

  @ViewChild('req_date') req_date: ElementRef;

  constructor(
    private _displayJobMacSvc: DisplayJobService,
    private _authSvc: AuthenticationService,
    private _formBuilder: FormBuilder,
  ) {
  } 

 
  ngOnInit() {
    this.buildForm();
    this.user = this._authSvc.getLoginUser();
    this.springSearch();
} 

buildForm() {
  this.validationForm = this._formBuilder.group({
    req_date: ['', Validators.compose([Validators.required, YourValidator.dateVaidator])]
  });
}

close() {
 window.history.back(); 
}

async springSearch(event: PageEvent = null) {  

  if (event != null) {
    this.model.pageIndex = event.pageIndex;
    this.model.itemPerPage = event.pageSize;
  }

  //console.log("this.req_date.nativeElement.value : " + this.req_date.nativeElement.value)
  //console.log("this.validationForm.valid :  " + this.validationForm.valid)

  if ((!this.validationForm.valid)&&(this.req_date.nativeElement.value == "")) {
     console.log("searchSpring");
     this.data =  await this._displayJobMacSvc.searchSpring(this.model);

  } else if ((this.validationForm.valid)&&(this.req_date.nativeElement.value != "")) {
     console.log("searchSpring By Date");
     this.modelByDate.req_date = this.req_date.nativeElement.value;
     this.data =  await this._displayJobMacSvc.searchSpringByDate(this.modelByDate);
     this.req_date.nativeElement.value = this.modelByDate.req_date;
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

