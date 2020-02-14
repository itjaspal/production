import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { AuthenticationService } from '../../_service/authentication.service';
import { MatDialog, MatSnackBar } from '@angular/material';
import { MessageService } from '../../_service/message.service';
import { Router, ActivatedRoute } from '@angular/router';
import { JobSendService } from '../../_service/job-send.service';
import { ScanSendFinView , ScanPcsView, ScanPcsSearchView, ScanSendFinSearchView } from '../../_model/job-send';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-send-prod-scan',
  templateUrl: './send-prod-scan.component.html',
  styleUrls: ['./send-prod-scan.component.scss']
})
export class SendProdScanComponent implements OnInit {

  constructor(
    //private _fb: FormBuilder,
    private _authSvc: AuthenticationService,
    private _dialog: MatDialog,
    private _msgSvc: MessageService,
    private _router: Router,
    private _actRoute: ActivatedRoute,
    private _jobSendSvc: JobSendService,
    private snackBar: MatSnackBar
  ) { }

  //public validationForm: FormGroup;
  public user: any;
  public model: ScanPcsView = new ScanPcsView();
  searchModel: ScanPcsSearchView = new ScanPcsSearchView();
  searchfinModel: ScanSendFinSearchView = new ScanSendFinSearchView();
  public scan_data: any = {};
  public data: any = {};
  
  ngOnInit() {
    
    this.user = this._authSvc.getLoginUser();
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
    

    //this.searchfinModel.req_date = this._actRoute.snapshot.params.req_date;
    this.searchfinModel.wc_code = this.user.def_wc_code;
    this.searchfinModel.user_id = this.user.username;
    this.searchfinModel.req_date  = this.searchModel.req_date
    
    

    let pcs_barcode = await this._jobSendSvc.searchscanpcs(this.searchModel);

    if(pcs_barcode.length = 0)
    {
      this._msgSvc.warningPopup("ไม่พบสินค้าบาร์โค้ด " + _qr + " ในระบบ");
    }

    this.model = await this._jobSendSvc.searchscanpcs(this.searchModel);
    console.log(this.scan_data);


    this.data = await this._jobSendSvc.searchfinpcs(this.searchfinModel);
    // if (pcs_barcode.length > 0) {
    //   this.itemSelected(pcs_barcode[0]);
    // } else {
    //   this._msgSvc.warningPopup("ไม่พบสินค้าบาร์โค้ด " + _qr + " ในระบบ");

    // }
  }

  close() {
    window.history.back();
  }

  save() {
    console.log(this.model);
    console.log(this.searchModel.req_date);
  }

}
