import { environment } from './../../environments/environment';
import { CommonSearchView } from './../_model/common-search-view';
import { SKUSearchView, SKUView } from './../_model/sku';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SKUService {

  constructor(private http: HttpClient) { }

  public async search(_model: SKUSearchView) {
    return await this.http.post<CommonSearchView<SKUView>>(environment.API_URL + 'master-sku/postSearch', _model).toPromise();
  }
 
  public async getInfo(_skuId: number) {
    return await this.http.get<SKUView>(environment.API_URL + 'master-sku/getInfo/' + _skuId).toPromise();
  }

  public async create(_model: SKUView) {
    return await this.http.post<number>(environment.API_URL + 'master-sku/postCreate', _model).toPromise();
  }

  public async update(_model: SKUView) {
    return await this.http.post<number>(environment.API_URL + 'master-sku/postUpdate', _model).toPromise();
  } 

}
