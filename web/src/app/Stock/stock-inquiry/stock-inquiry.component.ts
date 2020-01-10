import { Component, OnInit } from '@angular/core';
import { DropdownlistService } from '../../_service/dropdownlist.service';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { forkJoin } from 'rxjs';
import { MessageService } from '../../_service/message.service';
import { CommonSearchView } from '../../_model/common-search-view';
import { AppSetting } from '../../_constants/app-setting';
import { ProductGroupView } from '../../_model/productGroup';
import { Dropdownlist } from '../../_model/dropdownlist';
import { StockSearchView, StockView } from '../../_model/stock';
import { PageEvent } from '@angular/material';
import { StockService } from '../../_service/stock.service';


@Component({
  selector: 'app-stock-inquiry',
  templateUrl: './stock-inquiry.component.html',
  styleUrls: ['./stock-inquiry.component.scss']
})
export class StockInquiryComponent implements OnInit {

  
  pcs_barcode: string = "";
  bar_code: string ="";
  prod_code: string = "";
  prod_name: string = "";
  pdgrp_code:string = "";
  pdtype_code: string = "";
  pdbrnd_code:string = "";
  pddsgn_code:string = "";
  pdmodel_code: string = "";
  pdcolor_code: string ="";
  pdsize_code: string = "";

  


  //_stockSvc: any;

  constructor(
    private _activateRoute: ActivatedRoute,
    private _formBuilder: FormBuilder,
    private _ddlSvc: DropdownlistService,
    private _stockSvc: StockService,
    private _msgSvc: MessageService,
    private _router: Router
  ) { }

  //public data: any = {};
  
  public toppingList: any = [];
  public model: StockSearchView = new StockSearchView();
  public ddlProductGroup: Dropdownlist[] = [];
  public ddlProductType: Dropdownlist[] = [];
  public ddlProductBrand: Dropdownlist[] = [];
  public ddlProductDesign: Dropdownlist[] = [];
  public ddlProductModel: Dropdownlist[] = [];
  public ddlProductColor: Dropdownlist[] = [];
  public ddlProductSize: Dropdownlist[] = [];
  
  public data: CommonSearchView<StockView> = new CommonSearchView<StockView>();
  public pageSizeOptions: number[] = AppSetting.pageSizeOptions;
  public pageEvent: any;
  actions: any = {};

  
  async ngOnInit() {



    this.ddlProductGroup = await this._ddlSvc.getDdlProductGroup();
    this.ddlProductType = await this._ddlSvc.getDdlProductType();
    this.ddlProductBrand = await this._ddlSvc.getDdlProductBrand();
    this.ddlProductDesign = await this._ddlSvc.getDdlProductDesign();
    this.ddlProductModel = await this._ddlSvc.getDdlProductModel();
    this.ddlProductColor = await this._ddlSvc.getDdlProductColor();
    this.ddlProductSize = await this._ddlSvc.getDdlProductSize();

    if (sessionStorage.getItem('session-stock-search') != null) {
      this.model = JSON.parse(sessionStorage.getItem('session-stock-search'));
      this.search();
    }
    

  }

  async ngOnDestroy() {
    this.saveSession();
  }

  async saveSession() {
    sessionStorage.setItem('session-stock-search', JSON.stringify(this.model));
  }

  async search(event: PageEvent = null) {

    console.log(this.model);
    if (event != null) {
      this.model.pageIndex = event.pageIndex;
      this.model.itemPerPage = event.pageSize;
      //this.model.prod_code = this.prod_code;
      
    }
    
    if(this.model.pcs_barcode = "")
    {
      this.data = await this._stockSvc.search(this.model);
    }
    else
    {
      this.data = await this._stockSvc.searchpcs(this.model);
    }
    
  }

  delete(row: StockView) {

  }


}
