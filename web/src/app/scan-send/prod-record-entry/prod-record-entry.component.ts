import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AuthenticationService } from '../../_service/authentication.service';
import { MatDialog, MatSnackBar } from '@angular/material';
import { MessageService } from '../../_service/message.service';
import { Router, ActivatedRoute } from '@angular/router';
import { JobSendService } from '../../_service/job-send.service';
import { ScanPcsView, ScanPcsSearchView, ScanSendFinSearchView, ScanSendProcView, ScanSendFinView, JobSendEntrySearchView, ScanSendDataView } from '../../_model/job-send';
import { DataEntrySearchView } from '../../_model/job-inprocess';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-prod-record-entry',
  templateUrl: './prod-record-entry.component.html',
  styleUrls: ['./prod-record-entry.component.scss']
})
export class ProdRecordEntryComponent implements OnInit {

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
  public model: DataEntrySearchView = new DataEntrySearchView();
  public searchModel: ScanSendFinSearchView = new ScanSendFinSearchView();
  //searchfinModel: ScanSendFinSearchView = new ScanSendFinSearchView();
  //scanModel : ScanSendProcView = new ScanSendProcView();
  //finModel : ScanSendFinSearchView = new  ScanSendFinSearchView();
  //public scan_data: any = {};
  public data: any = {};
  public model_scan: ScanSendFinView = new ScanSendFinView();
  public datas: any = {};
  public count = 0;
  
  ngOnInit() {
    var datePipe = new DatePipe("en-US");
    
    this.buildForm();
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

    if (this.model.qty ==  null || this.model.qty == 0) {
      this._msgSvc.warningPopup("ต้องใส่จำนวนรับมอบสินค้า");
    }
    
    if (this.model.qty > this._actRoute.snapshot.params.qty) {
      this._msgSvc.warningPopup("บันทึกจำนวนเกินส่งสอบ Quit");
    } 

     

    this.searchModel.req_date = this.model.req_date;
    this.searchModel.wc_code = this.user.def_wc_code;
    //this.searchModel.mc_code = this.user.user_mac.MC_CODE;
    this.searchModel.user_id = this.user.username;
    this.searchModel.springtype_code = this.model.spring_grp;
    this.searchModel.pdsize_code = this.model.size_code ;

    
    this.data = await this._jobSendSvc.updatepcs(this.model);
    
    console.log(this.data);
    //this.model_scan = await this._jobSendSvc.searchfinpcs(this.searchModel);

    this.model.qty= 1;

    this.add(this.data.datas);
  }

  add(datas: any) {

    datas.forEach(product => {
      let newProd: ScanSendDataView = new ScanSendDataView();
      newProd.pcs_barcode = product.pcs_barcode;
      newProd.prod_code = product.prod_code;

      this.model_scan.datas.push(newProd);
    });

    this.count = this.model_scan.datas.length;

  }


  close() {
    //window.history.back();
    this._router.navigateByUrl('/app/scansend/sendsearch/'+this._actRoute.snapshot.params.req_date);
  }
}
