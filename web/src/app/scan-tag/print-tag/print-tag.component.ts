import { PrintTagView } from './../../_model/print-tag';
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
  public model: PrintTagSearchView = new PrintTagSearchView();
  public raw_model: PrintTagView = new PrintTagView();
  user: any;
 

  constructor(
    private _fb: FormBuilder,
    private _authSvc: AuthenticationService,
    private _dialog: MatDialog,
    private _msgSvc: MessageService,
    private _router: Router,
    private _actRoute: ActivatedRoute,
    private snackBar: MatSnackBar

  ) { }

  ngOnInit() {
    this.user = this._authSvc.getLoginUser();
    this.printTagData();

    console.log(this.user);
    
  }
  
  async printTagData() {  
    var datePipe = new DatePipe("en-US");
    
    this.model.req_date = this._actRoute.snapshot.params.req_date;
    this.model.spring_type = this._actRoute.snapshot.params.spring_grp;
    this.model.size_code = this._actRoute.snapshot.params.size_code;
    this.model.qty = this._actRoute.snapshot.params.qty;
    this.model.wc_code = this.user.def_wc_code;
    this.model.mc_code = this.user.user_mac.MC_CODE;
    this.model.user_id = this.user.username;
    this.model.req_date  = datePipe.transform(this.model.req_date, 'dd/MM/yyyy');

    console.log(this.model)
  }

  openSearchRawModal(_isEdit: boolean = false, _editItem: PrintTagView = null, _index: number = -1) {
    const dialogRef = this._dialog.open(RawmatSearchComponent, {
      maxWidth: '100vw',
      maxHeight: '100vh',
      height: '100%',
      width: '100%',
     
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        let raw = result.product;
        //check isExiting
        if (_index >= 0) {
          this.raw_model.raw_item[_index].doc_no = raw.doc_no;
          this.raw_model.raw_item[_index].prod_code = raw.prod_code;
          this.raw_model.raw_item[_index].prod_name = raw.prod_name;
          
        } else {
          this.raw_model.raw_item.push(raw);
        }
        //this.calculate();
      }
    });
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

