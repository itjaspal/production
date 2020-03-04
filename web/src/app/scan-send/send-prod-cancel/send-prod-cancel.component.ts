import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AuthenticationService } from '../../_service/authentication.service';
import { MatDialog, MatSnackBar } from '@angular/material';
import { MessageService } from '../../_service/message.service';
import { Router, ActivatedRoute } from '@angular/router';
import { JobSendService } from '../../_service/job-send.service';
import { ScanPcsView, ScanPcsSearchView, ScanSendFinSearchView, ScanSendProcView, ScanSendFinView, ScanSendDataView } from '../../_model/job-send';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-send-prod-cancel',
  templateUrl: './send-prod-cancel.component.html',
  styleUrls: ['./send-prod-cancel.component.scss']
})
export class SendProdCancelComponent implements OnInit {

  constructor(
    private _fb: FormBuilder,
    private _authSvc: AuthenticationService,
    private _dialog: MatDialog,
    private _msgSvc: MessageService,
    private _router: Router,
    private _actRoute: ActivatedRoute,
    private _jobSendSvc: JobSendService,
    private snackBar: MatSnackBar
  ) { }

  public validationForm: FormGroup;
  public user: any;
  public model: ScanPcsView = new ScanPcsView();
  searchModel: ScanPcsSearchView = new ScanPcsSearchView();
  searchfinModel: ScanSendFinSearchView = new ScanSendFinSearchView();
  scanModel : ScanSendProcView = new ScanSendProcView();
  finModel : ScanSendFinSearchView = new  ScanSendFinSearchView();
  //public scan_data: any = {};
  public data: any = {};
  public model_scan: ScanSendFinView = new ScanSendFinView();
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
    this.searchModel.user_id = this.user.username;

    //this.searchfinModel.req_date = this._actRoute.snapshot.params.req_date;
    // this.searchfinModel.wc_code = this.user.def_wc_code;
    // this.searchfinModel.user_id = this.user.username;
    // this.searchfinModel.req_date  = this.searchModel.req_date


    this.datas = await this._jobSendSvc.searchscancancelpcs(this.searchModel);
    console.log(this.datas)
    this.qrElement.nativeElement.focus();
    this.add(this.datas);
  }

  add(datas: any) {

    let newProd: ScanSendDataView = new ScanSendDataView();
    newProd.pcs_barcode = datas.pcs_barcode;
    newProd.prod_code = datas.prod_code;
 
    
    
    this.model_scan.datas.push(newProd);
    
    this.count = this.model_scan.datas.length;

  }


  close() {
    //window.history.back();
    this._router.navigateByUrl('/app/scansend/sendsearch/'+this._actRoute.snapshot.params.req_date);
  }

  

}
