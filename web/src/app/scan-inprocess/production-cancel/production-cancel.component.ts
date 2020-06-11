import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AuthenticationService } from '../../_service/authentication.service';
import { MatDialog, MatSnackBar } from '@angular/material';
import { MessageService } from '../../_service/message.service';
import { Router, ActivatedRoute } from '@angular/router';
import { JobinprocessService } from '../../_service/job-inprocess.service';
import { JobInProcessView, JobInProcessSearchView, JobInProcessScanFinView, JobInProcessScanView } from '../../_model/job-inprocess';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-production-cancel',
  templateUrl: './production-cancel.component.html',
  styleUrls: ['./production-cancel.component.scss']
})
export class ProductionCancelComponent implements OnInit {

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
  public model: JobInProcessView = new JobInProcessView();
  searchModel: JobInProcessSearchView = new JobInProcessSearchView();
  public data: any = {};
  public model_scan: JobInProcessScanFinView = new JobInProcessScanFinView();
  public datas: any = {};
  public count = 0;
  
  @ViewChild('qr') qrElement:ElementRef;
  ngAfterViewInit(){
    this.qrElement.nativeElement.focus();
  }

  ngOnInit() {
    this.buildForm();
    this.user = this._authSvc.getLoginUser();
  
    
    
  }

  private buildForm() {
    this.validationForm = this._fb.group({
    qr: [null, []]
    });
  }

  async onQrEntered(_qr: string) {

    if (_qr == null || _qr == "") {
      return;
    }

    var datePipe = new DatePipe("en-US");
    
    this.searchModel.req_date = this._actRoute.snapshot.params.req_date;
    this.searchModel.wc_code = this.user.def_wc_code;
    this.searchModel.pcs_barcode = _qr;
    this.searchModel.req_date  = datePipe.transform(this.searchModel.req_date, 'dd/MM/yyyy');
    this.searchModel.mc_code = this.user.mc_code;
    this.searchModel.user_id = this.user.username;
    this.searchModel.spring_grp = this._actRoute.snapshot.params.spring_grp;
    this.searchModel.springtype_code = this._actRoute.snapshot.params.springtype_code;
    this.searchModel.size_code = this._actRoute.snapshot.params.size_code;
   


    //this.searchfinModel.req_date = this._actRoute.snapshot.params.req_date;
    
    
    console.log(this.searchModel);

    this.datas = await this._jobInprocessSvc.searchscancancelpcs(this.searchModel);


    this.qrElement.nativeElement.focus();
    
    this.add(this.datas);
   
    
  }

  add(datas: any) {

    //const control = <FormArray>this.validationForm.controls['RawMatitemView'];
    //this.model.datas = [];
    //console.log(this.datas);

    
    //datas.forEach(product => {

        let newProd: JobInProcessScanView = new JobInProcessScanView();
        newProd.pcs_barcode = datas.pcs_barcode;
        newProd.prod_code = datas.prod_code;
     
        
        
        this.model_scan.datas.push(newProd);
        
        this.count = this.model_scan.datas.length;
        console.log(this.datas);

       
      
    //});
  }

  close() {
    //window.history.back();
    this._router.navigateByUrl('/app/scaninproc/inprocsearch/'+this._actRoute.snapshot.params.req_date);
  }

  // async save() {
  //   console.log(this.model);
  //   console.log(this.searchModel);

  //   this.searchModel.pcs_barcode = this.model.pcs_barcode;
    
  //   this.data = await this._jobInprocessSvc.cancelpcs(this.searchModel);
    
  //   this.model_scan = await this._jobInprocessSvc.searchcancelpcs(this.searchModel);

  //   this.qrElement.nativeElement.focus();

  //   this.model.springtype_code =  "";
  //   this.model.pdsize_desc= "";
  //   this.model.qty= null;

  //   console.log(this.model_scan);

  // }

}
