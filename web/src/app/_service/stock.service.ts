import { environment } from '../../environments/environment';
import { CommonSearchView } from '../_model/common-search-view';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { StockSearchView, StockView } from '../_model/stock';


@Injectable({
  providedIn: 'root' 
})
export class StockService {

  constructor(private http: HttpClient) { }

  public async search(_model: StockSearchView) {
    return await this.http.post<CommonSearchView<StockView>>(environment.API_URL + 'stock/postSearch', _model).toPromise();
  }

  public async searchpcs(_model: StockSearchView) {
    return await this.http.post<CommonSearchView<StockView>>(environment.API_URL + 'stock/postSearchpcs', _model).toPromise();
  }


}