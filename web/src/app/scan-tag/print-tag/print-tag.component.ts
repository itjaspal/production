import { PrintTagService } from './../../_service/print-tag.service';
import { PrintTagView, RawMatitemView, PrintTagAddView, PrintTagProcView } from './../../_model/print-tag';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DatePipe } from '@angular/common';
import { PrintTagSearchView } from '../../_model/print-tag';
import { FormBuilder, FormArray, FormGroup } from '@angular/forms';
import { AuthenticationService } from '../../_service/authentication.service';
import { MatDialog, MatSnackBar, PageEvent } from '@angular/material';
import { MessageService } from '../../_service/message.service';
import { RawmatSearchComponent } from '../rawmat-search/rawmat-search.component';
import { forEach } from '@angular/router/src/utils/collection';



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
  public del_model :PrintTagProcView = new PrintTagProcView();
  user: any;
  public datas: any = {};
  public validationForm: FormGroup;

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
    this.model_search.printer = this.user.def_printer;
    this.model_search.req_date  = datePipe.transform(this.model_search.req_date, 'dd/MM/yyyy');

    this.model.qty = this.model_search.qty;
    this.datas =  await this._tagSvc.searchPrintTag(this.model_search);
    
    this.add(this.datas.datas);
    
    console.log(this.model)
    //this.printTagData();
  }


  

  openSearchRawModal()
  {
    const dialogRef = this._dialog.open(RawmatSearchComponent, {
      maxWidth: '100vw',
      maxHeight: '100vh',
      height: '100%',
      width: '100%'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result.length > 0) {
        this.add(result);
      }
    })
  }
  
  add(datas: any) {

    //const control = <FormArray>this.validationForm.controls['RawMatitemView'];
    this.model.datas = [];
    console.log(this.model.datas);
    datas.forEach(product => {

        let newProd: RawMatitemView = new RawMatitemView();
        newProd.process_tag_no = this.datas.process_tag_no;
        newProd.doc_no = product.doc_no;
        newProd.prod_code = product.prod_code;
        newProd.prod_name = product.prod_name;
        
        
        this.model.datas.push(newProd);
        

        //console.log(this.model.raw_item);

       
      
    });
  }


  close() {
    window.history.back();
  }

  async print()
  {
  
    this.model.entity = this.model_search.entity;
    this.model.req_date = this.model_search.req_date;
    this.model.mc_code = this.model_search.mc_code;
    this.model.user_id = this.model_search.user_id;
    this.model.printer = this.model_search.printer;
    this.model.spring_grp = this.model_search.spring_grp;
    this.model.size_desc = this.model_search.size_desc;
    this.model.process_tag_no = this.datas.process_tag_no;

    console.log(this.model);
    this.datas = await this._tagSvc.PringTag(this.model);
    console.log(this.model);
    
    this.searchTagData();
  }

  async add_tag()
  {
    var datePipe = new DatePipe("en-US");
    
    this.model_search.req_date = this._actRoute.snapshot.params.req_date;
    this.model_search.spring_grp = this._actRoute.snapshot.params.spring_grp;
    this.model_search.size_desc = this._actRoute.snapshot.params.size_code;
    this.model_search.qty = this._actRoute.snapshot.params.qty;
    this.model_search.wc_code = this.user.def_wc_code;
    this.model_search.mc_code = this.user.user_mac.MC_CODE;
    this.model_search.printer = this.user.def_printer;
    this.model_search.req_date  = datePipe.transform(this.model_search.req_date, 'dd/MM/yyyy');

    this.model.qty = this.model_search.qty;
    this.datas =  await this._tagSvc.AddTag(this.model_search);
    
    this.add(this.datas.datas);
    
    console.log(this.datas)
  }

  async del_tag()
  {
    this.del_model.entity = this.model_search.entity;
    this.del_model.req_date = this.model_search.req_date;
    this.del_model.mc_code = this.datas.mc_code;
    this.del_model.process_tag_no = this.datas.process_tag_no;

    console.log(this.del_model);
     this.datas = await this._tagSvc.DeleteTag(this.del_model);
     
     this.model.datas = [];
     console.log(this.model.datas);
     this.printTagData();
     //this.datas = []; 
     
     
  }

  search_tag()
  {
    
  }

  async searchTagData() {  
    var datePipe = new DatePipe("en-US");
    
    this.model_search.req_date = this._actRoute.snapshot.params.req_date;
    this.model_search.spring_grp = this._actRoute.snapshot.params.spring_grp;
    this.model_search.size_desc = this._actRoute.snapshot.params.size_code;
    this.model_search.qty = this._actRoute.snapshot.params.qty;
    this.model_search.wc_code = this.user.def_wc_code;
    this.model_search.mc_code = this.user.user_mac.MC_CODE;
    this.model_search.printer = this.user.def_printer;
    this.model_search.req_date  = datePipe.transform(this.model_search.req_date, 'dd/MM/yyyy');

    this.model.qty = this.model_search.qty;
    this.datas =  await this._tagSvc.searchPrintTag(this.model_search);
    
    
    console.log(this.model_search)
    //this.printTagData();
  }

  // print() {
  //   let head = document.head;
  //   let style = document.createElement('style');
  //   style.type = 'text/css';
  //   style.media = 'print';

  //   style.appendChild(document.createTextNode('@page { size: A4 landscape; margin: 4mm 0;}'));

  //   head.appendChild(style);

  //   window.print();
  // }

  setDefaultPrint() {

    this._router.navigateByUrl('/app/defprinter');


  }

}

