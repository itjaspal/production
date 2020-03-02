import { ScanInprocessComponent } from './../../scan-inprocess/scan-inprocess/scan-inprocess.component';
import { Component, OnInit, ViewChild, ElementRef, Directive } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AuthenticationService } from '../../_service/authentication.service';
import { MatDialog, MatSnackBar, PageEvent } from '@angular/material';
import { MessageService } from '../../_service/message.service';
import { Router, ActivatedRoute } from '@angular/router';
import { JobSendService } from '../../_service/job-send.service';
import { ScanPcsView, ScanPcsSearchView, ScanSendFinSearchView, ScanSendProcView, ScanSendFinView } from '../../_model/job-send';
import { DatePipe } from '@angular/common';
//import { qR } from '@angular/core/src/render3';

// @Directive({
//   selector :'[focusQR]'
// })

@Component({
  selector: 'app-send-prod-scan',
  templateUrl: './send-prod-scan.component.html',
  styleUrls: ['./send-prod-scan.component.scss']
  
})
export class SendProdScanComponent implements OnInit {

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
  public req_date : any= {}; 

  @ViewChild('qr') qrElement:ElementRef;
  ngAfterViewInit(){
    this.qrElement.nativeElement.focus();
  }

  ngOnInit() {
    this.buildForm();
    this.user = this._authSvc.getLoginUser();
    // this.req_date = this._actRoute.snapshot.params.req_date;
    // console.log(this.req_date);
    
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
    
    
    

    let pcs_barcode = await this._jobSendSvc.searchscanpcs(this.searchModel);

    if(pcs_barcode.length = 0)
    {
      this._msgSvc.warningPopup("ไม่พบ PCS Barcode " + _qr + " ในระบบ");
    }

    this.searchfinModel.wc_code = this.user.def_wc_code;
    this.searchfinModel.user_id = this.user.username;
    this.searchfinModel.req_date  = this.searchModel.req_date;
    this.searchfinModel.springtype_code = this._actRoute.snapshot.params.spring_grp;
    this.searchfinModel.pdsize_code = this._actRoute.snapshot.params.size_code;
   
    console.log(this.searchfinModel);
    

    this.model_scan = await this._jobSendSvc.searchfinpcs(this.searchfinModel);

    this.qrElement.nativeElement.focus();
    
    // if (pcs_barcode.length > 0) {
    //   this.itemSelected(pcs_barcode[0]);
    // } else {
    //   this._msgSvc.warningPopup("ไม่พบสินค้าบาร์โค้ด " + _qr + " ในระบบ");

    // }
    
  }

  close() {
    //window.history.back();
    // console.log(this._actRoute.snapshot.params.req_date);
    this._router.navigateByUrl('/app/scansend/sendsearch/'+this._actRoute.snapshot.params.req_date);
    
  }

  back()
  {

  }
  // async save() {
  //   console.log(this.model);
  //   console.log(this.searchModel.req_date);

  //   this.scanModel.wc_code = this.user.def_wc_code;;  
  //   this.scanModel.req_date  = this.searchModel.req_date;  
  //   this.scanModel.pcs_barcode  = this.model.pcs_barcode;  
  //   this.scanModel.spring_grp  = this.model.spring_grp;  
  //   this.scanModel.size_code  = this.model.size_desc;  
  //   this.scanModel.user_id  = this.user.username;  

  //   this.searchfinModel.wc_code = this.user.def_wc_code;
  //   this.searchfinModel.user_id = this.user.username;
  //   this.searchfinModel.req_date  = this.searchModel.req_date
   
  //   console.log(this.searchfinModel);
    
  //   //this.data = await this._jobSendSvc.scanpcs(this.scanModel);
    
  //   this.model_scan = await this._jobSendSvc.searchfinpcs(this.searchfinModel);

  //   this.qrElement.nativeElement.focus();

  //   this.model.spring_grp =  "";
  //   this.model.size_desc= "";
  //   this.model.qty= null;

  //   console.log(this.model_scan);

  // }


  
  

}
