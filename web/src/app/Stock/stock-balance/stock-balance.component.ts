import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material';

import { DropdownlistService } from '../../_service/dropdownlist.service';
import { ActivatedRoute } from '@angular/router';
import { AuthenticationService } from '../../_service/authentication.service';
import { StockSearchView, StockView } from '../../_model/stock';
import { Dropdownlist } from '../../_model/dropdownlist';
import { AppSetting } from '../../_constants/app-setting';
import { CommonSearchView } from '../../_model/common-search-view';
import { StockService } from '../../_service/stock.service';

@Component({
  selector: 'app-stock-balance',
  templateUrl: './stock-balance.component.html',
  styleUrls: ['./stock-balance.component.scss']
})
export class StockBalanceComponent implements OnInit {

  constructor(
    private _stockSvc: StockService,
    private _ddlSvc: DropdownlistService,
    private _actRoute:ActivatedRoute,
    private _authSvc: AuthenticationService
  ) { }

  public toppingList: any = [];
  public model: StockSearchView = new StockSearchView();
  public ddlStatus: any;
  //public ddlBranch: Dropdownlist[] = [];
  //public ddlDepartment: Dropdownlist[] = [];  
  public pageSizeOptions: number[] = AppSetting.pageSizeOptions;
  public pageEvent: any;
  actions: any = {};

  public data: CommonSearchView<StockView> = new CommonSearchView<StockView>();

  async ngOnInit() {    
    this.actions = this._authSvc.getActionAuthorization(this._actRoute);

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

    if (event != null) {
      this.model.pageIndex = event.pageIndex;
      this.model.itemPerPage = event.pageSize;
    }

    this.data = await this._stockSvc.search(this.model);
  }

  delete(row: StockView) {

  }

  

}
