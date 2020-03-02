import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { DisplayJobSearchView, SearchSpringByDateSearchView, DisplayJobView } from '../../_model/displayJob';
import { CommonSearchView } from '../../_model/common-search-view';
import { DatePipe } from '@angular/common';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { DisplayJobService } from '../../_service/displayJob.service';
import { AuthenticationService } from '../../_service/authentication.service';
import { ActivatedRoute, Router } from '@angular/router';
import { YourValidator } from '../../view-spec/view-spec/view-spec.component';
import { PageEvent } from '@angular/material';

@Component({
  selector: 'app-tag-search',
  templateUrl: './tag-search.component.html',
  styleUrls: ['./tag-search.component.scss']
})
export class TagSearchComponent implements OnInit {

  public model: DisplayJobSearchView = new DisplayJobSearchView();
  public modelByDate: SearchSpringByDateSearchView = new SearchSpringByDateSearchView();
 // public data: SearchSpringHeaderView<SearchSpringDetailView> = new SearchSpringHeaderView<SearchSpringDetailView>();
  public data: CommonSearchView<DisplayJobView> = new CommonSearchView<DisplayJobView>();
  public user: any;
  //public datePipe = new DatePipe('en-US');
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

 
  async ngOnInit() {
      this.buildForm();
      this.actions = this._authSvc.getActionAuthorization(this._actRoute);
      this.user = this._authSvc.getLoginUser();
     
      var datePipe = new DatePipe("en-US");
      this.modelByDate.req_date  = datePipe.transform(this._actRoute.snapshot.params.req_date, 'dd/MM/yyyy').toString();

      console.log(this.modelByDate.req_date);
      this.req_date.nativeElement.value = this.modelByDate.req_date;
      this.springSearch();
      

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


         this.modelByDate.mc_code  = this.user.user_mac.MC_CODE;
         this.modelByDate.user_id  = this.user.username;
         this.modelByDate.wc_code  = this.user.def_wc_code;
         this.modelByDate.req_date = this.req_date.nativeElement.value;
         this.data.datas  = [];

         this.data =  await this._displayJobMacSvc.searchJobMacCurrentByDate(this.modelByDate);
         this.req_date.nativeElement.value = this.modelByDate.req_date;
      
  }   

  



close() {
  //window.history.back();
  this._router.navigateByUrl('/app/mobile-navigator');
}



}


