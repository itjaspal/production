import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AuthenticationService } from '../../_service/authentication.service';
import { MatDialog, MatSnackBar } from '@angular/material';
import { MessageService } from '../../_service/message.service';
import { Router, ActivatedRoute } from '@angular/router';
import { JobinprocessService } from '../../_service/job-inprocess.service';
import { JobInProcessView, JobInProcessSearchView, JobInProcessScanFinView, DataEntrySearchView, JobInProcessScanView } from '../../_model/job-inprocess';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-production-record-entry',
  templateUrl: './production-record-entry.component.html',
  styleUrls: ['./production-record-entry.component.scss']
})
export class ProductionRecordEntryComponent implements OnInit {

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
  public datas: any = {};
  public count = 0;

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
    // console.log(this.model);
    // console.log(this.searchModel);

    // this.searchModel.pcs_barcode = this.model.pcs_barcode;
    this.searchModel.req_date = this.model.req_date;
    this.searchModel.wc_code = this.user.def_wc_code;
    this.searchModel.mc_code = this.user.user_mac.MC_CODE;
    this.searchModel.user_id = this.user.username;
    this.searchModel.spring_grp = this.model.spring_grp;
    this.searchModel.size_code = this.model.size_code ;

    //console.log(this.searchModel);
    this.data = await this._jobInprocessSvc.updatepcs(this.model);
    
     //this.model_scan = await this._jobInprocessSvc.searchfinpcs(this.searchModel);


     console.log(this.data.datas);
     //this.add(this.data.datas);
     this.add(this.data.datas);

  }

  add(datas: any) {

    //const control = <FormArray>this.validationForm.controls['RawMatitemView'];
    //this.model.datas = [];
    console.log(datas);
    
    datas.forEach(product => {

      console.log(datas);

        let newProd: JobInProcessScanView = new JobInProcessScanView();
        newProd.pcs_barcode = product.pcs_barcode;
        newProd.prod_code = product.prod_code;
     
        
        this.model_scan.datas.push(newProd);

        //console.log(this.model_scan.datas);
    });
    
    this.count = this.model_scan.datas.length;
  }

  close() {
    
    //window.history.back();
    this._router.navigateByUrl('/app/scaninproc/inprocsearch/'+this._actRoute.snapshot.params.req_date);
  }

}
