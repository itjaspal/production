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
    private _productSvc: ProductService,
    private _fb: FormBuilder,
    private _authSvc: AuthenticationService,
    private _msgSvc: MessageService,
    private cdr: ChangeDetectorRef,
    private _tagSvc: PrintTagService,
  ) { 
    this.buildFrom();
    this.user = this._authSvc.getLoginUser();
    this.itemPerPage = (this.data.itemPerPage) ? this.data.itemPerPage : 10;
    this.isEdit = true;

    
   
    if (this.data.editItem) {

      this.model = Object.assign({}, this.data.editItem);
      this.action = 'SAVE';

      
      //this.searchModel.req_date = this.model.productName;
     
     // let _product: RawMatitemView = new RawMatitemView();
     
    

    } 
    //else {
    //   this.addFilterProduct("productDesc");
    //   this.addFilterProduct("modelDesc");
    //   this.addFilterProduct("designDesc");
    //   this.addFilterProduct("colorDesc");
    //   this.addFilterProduct("sizeDesc");
    // }
  }

  public user: any;
  public itemPerPage: number = 0;
  public isEdit: boolean = false;
  action: string = "SEARCH";
  debounceT: number = 500;


  public products: RawMatitemView[] = [];

  filteredSerialNo: Observable<string>;

  validationForm: FormGroup;

  public searchModel: RawProductSearchView = new RawProductSearchView();
  public model: RawMatitemView = new RawMatitemView();
  ngOnInit() {
  }


  buildFrom() {
    this.validationForm = this._fb.group({
      req_date: [null, []]
    });
  }

  async search(event: PageEvent = null)
  {
    
    
    //this.data = await this._tagSvc.searchcspring(this.model);
  }

}
