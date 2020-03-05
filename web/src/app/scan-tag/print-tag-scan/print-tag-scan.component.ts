import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AuthenticationService } from '../../_service/authentication.service';
import { MatDialog, MatSnackBar } from '@angular/material';
import { MessageService } from '../../_service/message.service';
import { Router, ActivatedRoute } from '@angular/router';
import { PrintTagService } from '../../_service/print-tag.service';
import { DatePipe } from '@angular/common';
import { RawmatSearchComponent } from '../rawmat-search/rawmat-search.component';
import { RawMatitemView, PrintTagSearchView, PrintTagView, PrintTagProcView, ProcessTagSearchView, ProcessTagNoSearch, RawMatScanSerchView } from '../../_model/print-tag';

@Component({
  selector: 'app-print-tag-scan',
  templateUrl: './print-tag-scan.component.html',
  styleUrls: ['./print-tag-scan.component.scss']
})
export class PrintTagScanComponent implements OnInit {
  actions: any;
  public model_search: PrintTagSearchView = new PrintTagSearchView();
  public model : PrintTagView = new PrintTagView();
  //public raw_model: PrintTagView = new PrintTagView();
  public item_model :RawMatitemView = new RawMatitemView();
  public del_model :PrintTagProcView = new PrintTagProcView();
  public tagno_search: ProcessTagSearchView = new ProcessTagSearchView();
  public model_tagsearch :ProcessTagNoSearch  = new ProcessTagNoSearch();
  public modelraw_search : RawMatScanSerchView = new RawMatScanSerchView();

  user: any;
  public datas: any = {};
  public datas_tag: any = {};
  public validationForm: FormGroup;
  public data: any = {};

  @ViewChild('qr') qrElement:ElementRef;
  ngAfterViewInit(){
    this.qrElement.nativeElement.focus();
  }


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
    this.tagList();
    
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
    this.model_search.user_id = this.user.username;
    this.model_search.req_date  = datePipe.transform(this.model_search.req_date, 'dd/MM/yyyy');

    if(this.model_search.qty==0)
    {
      this.model.qty = 10;
    }
    else
    {
      this.model.qty = this.model_search.qty;
    }
    
    this.datas =  await this._tagSvc.searchPrintTag(this.model_search);
    
    console.log(this.datas.datas);
    this.add(this.datas.datas);
    

  }

  async tagList() {  
    this.tagno_search.req_date = this.model_search.req_date;
    this.tagno_search.mc_code = this.user.user_mac.MC_CODE;

    this.datas_tag = await this._tagSvc.tagnoList(this.tagno_search);

    console.log(this.datas_tag);

  }
  

  // openSearchRawModal()
  // {
  //   const dialogRef = this._dialog.open(RawmatSearchComponent, {
  //     maxWidth: '100vw',
  //     maxHeight: '100vh',
  //     height: '100%',
  //     width: '100%'
  //   });

  //   dialogRef.afterClosed().subscribe(result => {
  //     if (result.length > 0) {
  //       this.add(result);
  //     }
  //   })
  // }

  async onQrEntered(_qr: string) {

    if (_qr == null || _qr == "") {
      return;
    }

    
    this.modelraw_search.process_tag_no = this.model.process_tag_no;
    this.modelraw_search.qr = _qr;

    console.log(this.modelraw_search);

    
    this.data = await this._tagSvc.searchrawscan(this.modelraw_search);

       
    this.qrElement.nativeElement.focus();
    
    console.log(this.data);
    //this.add(this.datas.datas);
    this.model.datas.push(this.data);
    
  }

  
  add(datas: any) {

    //const control = <FormArray>this.validationForm.controls['RawMatitemView'];
    //this.model.datas = [];
    console.log(this.model.datas);
    datas.forEach(product => {

        let newProd: RawMatitemView = new RawMatitemView();
        newProd.process_tag_no = product.process_tag_no;
        newProd.doc_no = product.doc_no;
        newProd.prod_code = product.prod_code;
        newProd.prod_name = product.prod_name;
        
        
        this.model.datas.push(newProd);
        

        //console.log(this.model.raw_item);

       
      
    });
  }

  add_search(datas: any) {

    //const control = <FormArray>this.validationForm.controls['RawMatitemView'];
    this.model.datas = [];
    console.log(this.model.datas);
    datas.forEach(product => {

        let newProd: RawMatitemView = new RawMatitemView();
        newProd.process_tag_no = product.process_tag_no;
        newProd.doc_no = product.doc_no;
        newProd.prod_code = product.prod_code;
        newProd.prod_name = product.prod_name;
        
        
        this.model.datas.push(newProd);
        

        //console.log(this.model.raw_item);

       
      
    });
  }


  close() {
    // window.history.back();
    this._router.navigateByUrl('/app/scantag/tagsearch/'+this._actRoute.snapshot.params.req_date);
    
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
    //this.model.datas = [];
    this.datas =  await this._tagSvc.AddTag(this.model_search);
    this.model.process_tag_no = this.datas.process_tag_no;

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

  async search_tagno()
  {
    var datePipe = new DatePipe("en-US");
    
    this.model_tagsearch.req_date = this._actRoute.snapshot.params.req_date;
    this.model_tagsearch.spring_grp = this._actRoute.snapshot.params.spring_grp;
    this.model_tagsearch.size_desc = this._actRoute.snapshot.params.size_code;
    this.model_tagsearch.qty = this._actRoute.snapshot.params.qty;
    this.model_tagsearch.wc_code = this.user.def_wc_code;
    this.model_tagsearch.mc_code = this.user.user_mac.MC_CODE;
    this.model_tagsearch.printer = this.user.def_printer;
    this.model_tagsearch.req_date  = datePipe.transform(this.model_tagsearch.req_date, 'dd/MM/yyyy');

    this.model_tagsearch.qty = this.model.qty;
    this.datas =  await this._tagSvc.searchtagno(this.model_tagsearch);
    
    this.add_search(this.datas.datas);
    
    console.log(this.model_tagsearch)
    //this.printTagData();
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
    
    
    //console.log(this.model_search)
    //this.printTagData();
  }

  isNewTag()
  {
    //console.log(this.datas.datas.lenght);
    if(this.datas.datas.lenght == 0)
    {
      return true;
    }
    else
    {
      return false;
    }
  }

  

  setDefaultPrint() {

    this._router.navigateByUrl('/app/defprinter');


  }

}
