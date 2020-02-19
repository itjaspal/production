import { PrintTagService } from './../../_service/print-tag.service';
import { PrintTagView, RawMatitemView } from './../../_model/print-tag';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DatePipe } from '@angular/common';
import { PrintTagSearchView } from '../../_model/print-tag';
import { FormBuilder } from '@angular/forms';
import { AuthenticationService } from '../../_service/authentication.service';
import { MatDialog, MatSnackBar, PageEvent } from '@angular/material';
import { MessageService } from '../../_service/message.service';
import { RawmatSearchComponent } from '../rawmat-search/rawmat-search.component';



@Component({
  selector: 'app-print-tag',
  templateUrl: './print-tag.component.html',
  styleUrls: ['./print-tag.component.scss']
})
export class PrintTagComponent implements OnInit {
  actions: any;
  public model_search: PrintTagSearchView = new PrintTagSearchView();
  public model : PrintTagView = new PrintTagView();
  //public raw_model: PrintTagView = new PrintTagView();
  public item_model :RawMatitemView = new RawMatitemView();
  user: any;
  public data: any = {};
 

  constructor(
    private _fb: FormBuilder,
    private _authSvc: AuthenticationService,
    private _dialog: MatDialog,
    private _msgSvc: MessageService,
    private _router: Router,
    private _actRoute: ActivatedRoute,
    private snackBar: MatSnackBar,
    private _tagSvc: PrintTagService

  ) { }

  ngOnInit() {
    this.user = this._authSvc.getLoginUser();
    this.printTagData();

    console.log(this.user);
    
  }
  
  async printTagData() {  
    var datePipe = new DatePipe("en-US");
    
    this.model_search.req_date = this._actRoute.snapshot.params.req_date;
    this.model_search.spring_grp = this._actRoute.snapshot.params.spring_grp;
    this.model_search.size_desc = this._actRoute.snapshot.params.size_code;
    this.model_search.qty = this._actRoute.snapshot.params.qty;
    this.model_search.wc_code = this.user.def_wc_code;
    this.model_search.mc_code = this.user.user_mac.MC_CODE;
    //this.model.user_id = this.user.username;
    this.model_search.req_date  = datePipe.transform(this.model_search.req_date, 'dd/MM/yyyy');
    this.data =  await this._tagSvc.searchPrintTag(this.model_search);
    
    
    console.log(this.data)
  }

  openSearchRawModal()
  {
    const dialogRef = this._dialog.open(RawmatSearchComponent, {
      maxWidth: '100vw',
      maxHeight: '100vh',
      height: '100%',
      width: '100%'
      // data: {
      //   branchId: this.model.branchId,
      //   stockLocationId: this.model.stockLocationId,
      // }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result.length > 0) {
        this.add(result);
      }
    })
  }
  add(datas: any){
      
  }

  close() {
    window.history.back();
  }

  print() {
    let head = document.head;
    let style = document.createElement('style');
    style.type = 'text/css';
    style.media = 'print';

    style.appendChild(document.createTextNode('@page { size: A4 landscape; margin: 4mm 0;}'));

    head.appendChild(style);

    window.print();
  }

  setDefaultPrint() {

    this._router.navigateByUrl('/app/defprinter');

  }

}

