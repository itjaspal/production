import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AuthenticationService } from '../../_service/authentication.service';
import { MatDialog, MatSnackBar } from '@angular/material';
import { MessageService } from '../../_service/message.service';
import { Router, ActivatedRoute } from '@angular/router';
import { JobinprocessService } from '../../_service/job-inprocess.service';
import { DataEntrySearchView, JobInProcessScanFinView, JobInProcessSearchView } from '../../_model/job-inprocess';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-production-record-cancel',
  templateUrl: './production-record-cancel.component.html',
  styleUrls: ['./production-record-cancel.component.scss']
})
export class ProductionRecordCancelComponent implements OnInit {

  constructor(
    private _fb: FormBuilder,
    private _authSvc: AuthenticationService,
    private _dialog: MatDialog,
    private _msgSvc: MessageService,
    private _router: Router,
    private _actRoute: ActivatedRoute,
    private _jobInprocessSvc: JobinprocessService,
    private snackBar: MatSnackBar
  ) { }

  public validationForm: FormGroup;
  public user: any;
  public model: DataEntrySearchView = new DataEntrySearchView();
  public searchModel: JobInProcessSearchView = new JobInProcessSearchView();

  public data: any = {};
  public model_scan: JobInProcessScanFinView = new JobInProcessScanFinView();

  ngOnInit() {
    this.buildForm();
    
    var datePipe = new DatePipe("en-US");

    this.user = this._authSvc.getLoginUser();
    
    this.model.req_date = this._actRoute.snapshot.params.req_date;
    this.model.req_date  = datePipe.transform(this.model.req_date, 'dd/MM/yyyy');
    this.model.wc_code = this.user.def_wc_code;
    this.model.mc_code = this.user.user_mac.MC_CODE;
    this.model.user_id = this.user.username;
    this.model.spring_grp =this._actRoute.snapshot.params.spring_grp; 
    this.model.size_code =this._actRoute.snapshot.params.size_code;
    this.model.qty  = 1;
  }

  private buildForm() {
    this.validationForm = this._fb.group({
    qty: [null, []]
    });
  }

  async save() {

    this.searchModel.req_date = this.model.req_date;
    this.searchModel.wc_code = this.user.def_wc_code;
    this.searchModel.mc_code = this.user.user_mac.MC_CODE;
    this.searchModel.user_id = this.user.username;
    this.searchModel.spring_grp = this.model.spring_grp;
    this.searchModel.size_code = this.model.size_code ;

    //console.log(this.searchModel);
     this.data = await this._jobInprocessSvc.cancelpcs(this.model);
    
     this.model_scan = await this._jobInprocessSvc.searchcancelpcs(this.searchModel);

  }

  close() {
    
    //window.history.back();
    this._router.navigateByUrl('/app/scaninproc/inprocsearch/'+this._actRoute.snapshot.params.req_date);
  }

}
