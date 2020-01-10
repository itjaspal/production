import { environment } from './../../environments/environment';
import { CommonSearchView } from './../_model/common-search-view';
import { DailySaleView, DailySaleSearchView, DailySaleMinDate } from './../_model/dailySale';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DailySaleService {

  constructor(private http: HttpClient) { }

  public async generateDailySales(_model: DailySaleView) {
    return await this.http.post<DailySaleView>(environment.API_URL + 'dailySale/postGenerateDailySale', _model).toPromise();
  }

  public async sendDailySales(_model: DailySaleView) {
    return await this.http.post<DailySaleView>(environment.API_URL + 'dailySale/postSendDailySale', _model).toPromise();
  }

  public async search(_model: DailySaleSearchView) {
    return await this.http.post<CommonSearchView<DailySaleView>>(environment.API_URL + 'dailySale/postSearch', _model).toPromise();
  }
 
  public async getInfo(_id: number) {
    return await this.http.get<DailySaleView>(environment.API_URL + 'dailySale/getInfo/' + _id).toPromise();
  }

  public async postCheckPendingDailySalesBill(_model: DailySaleSearchView) {
    return await this.http.post<CommonSearchView<DailySaleView>>(environment.API_URL + 'dailySale/postCheckPendingDailySalesBill', _model).toPromise();
  }
 
  public async postRetriveDailySaleMinDate(_model: any) {
    return await this.http.post(environment.API_URL + 'dailySale/postRetriveDailySaleMinDate',_model).toPromise();
  }

  public async searchSKU(_model: DailySaleSearchView) {
    return await this.http.post(environment.API_URL + 'dailySale/postSearchDailySaleSKU', _model).toPromise();
  }
}