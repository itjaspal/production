import { Component, OnInit, Inject, ChangeDetectorRef } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, PageEvent } from '@angular/material';
import { ProductService } from '../../_service/product.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AuthenticationService } from '../../_service/authentication.service';
import { MessageService } from '../../_service/message.service';
import { RawProductSearchView, RawMatitemView } from '../../_model/print-tag';
import { Observable } from 'rxjs';
import { PrintTagService } from '../../_service/print-tag.service';

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
  //isSaleBed: boolean = false;

  datas: any[] = [];

  public model : RawProductSearchView  = new RawProductSearchView();

  public model_raw: any = {
    process_tag_no: "",
    doc_no: "",
    prod_code: "",
    prod_name: ""
    
  }

  ngOnInit() {
     this.buildForm();
    //this.model.branchId = this.data.branchId;
    //this.model.stockLocationId = this.data.stockLocationId;

  }


  private buildForm() {
    this.validationForm = this._fb.group({
    doc_date: [null, []]
    });
  }

  async search() {

    this.datas = await this._tagSvc.searchRawItem(this.model);

  }


  close() {
    this.dialogRef.close([]);
  }

  async openSearchRawModal() {

    this.datas = await this._tagSvc.searchRawItem(this.model);

  }
  add() {

    // console.log(this.datas)
    let addList: any = this.datas.filter(x => x.selected);
    this.dialogRef.close(addList);
  }

}
