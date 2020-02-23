import { Component, OnInit, Inject, ChangeDetectorRef } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, PageEvent } from '@angular/material';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AuthenticationService } from '../../_service/authentication.service';
import { MessageService } from '../../_service/message.service';
import { RawProductSearchView, RawMatitemView, RawProductView, RawMatView, PrintTagAddView } from '../../_model/print-tag';
import { Observable } from 'rxjs';
import { PrintTagService } from '../../_service/print-tag.service';
import { DatePipe } from '@angular/common';


@Component({
  selector: 'app-rawmat-search',
  templateUrl: './rawmat-search.component.html',
  styleUrls: ['./rawmat-search.component.scss']
})
export class RawmatSearchComponent implements OnInit {
  

  constructor(
    public dialogRef: MatDialogRef<any>,
    @Inject(MAT_DIALOG_DATA) public data: RawProductSearchView,
    private _fb: FormBuilder,
    private _msg: MessageService,
    private _authSvc: AuthenticationService,
    private _tagSvc : PrintTagService
  ) {

    this.user = this._authSvc.getLoginUser();
    //let entityPrefix = this.user.branch.branch.entityCode.substring(0, 1);
    //this.isSaleBed = (entityPrefix.toUpperCase() == 'B');

   
  }

  public validationForm: FormGroup;
  user:any = {};
  public model : RawProductSearchView  = new RawProductSearchView();
  //datas: any[] = [];
  datas: any = {};
  public model_add : PrintTagAddView  = new PrintTagAddView();
 
  public model_raw: any = {
    //process_tag_no: "",
    doc_no: "",
    prod_code: "",
    prod_name: ""
    
  }

  ngOnInit() {
    this.buildForm();
    //this.model_add.entity = this.datas.entity;
    //this.model_add.req_date = this.data.req_date;
    console.log(this.model);
     


  }

  private buildForm() {
    this.validationForm = this._fb.group({
    doc_date: [null, []]
    });
  }

  async search() {
    var datePipe = new DatePipe("en-US");
    this.model.doc_date  = datePipe.transform(this.model.doc_date, 'dd/MM/yyyy');
    
    
    this.datas = await this._tagSvc.searchRawItem(this.model);

    //console.log(this.datas)

  }


  // private buildForm() {
  //   this.validationForm = this._fb.group({
  //   doc_date: [null, []]
  //   });
  // }

  // async search() {

  //   this.datas = await this._tagSvc.searchRawItem(this.model);

  // }


  close() {
    this.dialogRef.close([]);
  }

 
  async add() {

    let addList: any = this.datas.datas.filter(x => x.selected);

    //this.datas = await this._tagSvc.searchRawItem(this.model);
    console.log(addList);

    this.dialogRef.close(addList);
  }

}
