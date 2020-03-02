import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AuthenticationService } from '../../_service/authentication.service';
import { MatDialog, MatSnackBar } from '@angular/material';
import { MessageService } from '../../_service/message.service';
import { Router, ActivatedRoute } from '@angular/router';
import { DatePipe } from '@angular/common';
import { JobinprocessService } from '../../_service/job-inprocess.service';
import { JobInProcessView, JobInProcessSearchView, JobInProcessScanFinView } from '../../_model/job-inprocess';
//import { ScanSendFinView } from '../../_model/job-send';

@Component({
  selector: 'app-production-scan',
  templateUrl: './production-scan.component.html',
  styleUrls: ['./production-scan.component.scss']
})
export class ProductionScanComponent implements OnInit {

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
    this.searchModel.mc_code = this.user.user_mac.MC_CODE;
    this.searchModel.user_id = this.user.username;
    this.searchModel.spring_grp = this._actRoute.snapshot.params.spring_grp;
    this.searchModel.size_code = this._actRoute.snapshot.params.size_code;
   


    //this.searchfinModel.req_date = this._actRoute.snapshot.params.req_date;
    
    
    console.log(this.searchModel);

    let pcs_barcode = await this._jobInprocessSvc.searchscanpcs(this.searchModel);

    if(pcs_barcode == null)
    {
      this._msgSvc.warningPopup("ไม่พบ PCS Barcode " + _qr + " ในระบบ");
    }

    // this.model = await this._jobInprocessSvc.searchscanpcs(this.searchModel);
    // console.log(this.model);

    this.model_scan = await this._jobInprocessSvc.searchfinpcs(this.searchModel);

    this.qrElement.nativeElement.focus();
    
    
    // if (pcs_barcode.length > 0) {
    //   this.itemSelected(pcs_barcode[0]);
    // } else {
    //   this._msgSvc.warningPopup("ไม่พบสินค้าบาร์โค้ด " + _qr + " ในระบบ");

    // }
    
  }

  close() {
    // console.log(this.searchModel);
    // window.history.back();
    this._router.navigateByUrl('/app/scaninproc/inprocsearch/'+this._actRoute.snapshot.params.req_date);
  }

  // async save() {
  //   console.log(this.model);
  //   console.log(this.searchModel);

  //   this.searchModel.pcs_barcode = this.model.pcs_barcode;

  //   this.data = await this._jobInprocessSvc.scanpcs(this.searchModel);
    
  //   this.model_scan = await this._jobInprocessSvc.searchfinpcs(this.searchModel);

  //   this.qrElement.nativeElement.focus();

  //   this.model.springtype_code =  "";
  //   this.model.pdsize_desc= "";
  //   this.model.qty= null;

  //   console.log(this.model);

  // }

}
