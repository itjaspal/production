import { environment } from './../../environments/environment';
import { CommonSearchView } from './../_model/common-search-view';
import { PCSaleSearchView, PCSaleView } from './../_model/pcsale';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PCSaleService {

  constructor(private http: HttpClient) { }

  public async search(_model: PCSaleSearchView) {
    return await this.http.post<CommonSearchView<PCSaleView>>(environment.API_URL + 'master-pcsale/postSearch', _model).toPromise();
  }

  public async create(_model: PCSaleView) {
    return await this.http.post<number>(environment.API_URL + 'master-pcsale/postCreate', _model).toPromise();
  }

  public async update(_model: PCSaleView) {
    return await this.http.post<number>(environment.API_URL + 'master-pcsale/postUpdate', _model).toPromise();
  }

  public async getInfo(_productId: number) {
    return await this.http.get<PCSaleView>(environment.API_URL + 'master-pcsale/getInfo/' + _productId).toPromise();
  }
  
  public async getAll(_branchId: number) {
    return await this.http.get<PCSaleView[]>(environment.API_URL + 'master-pcsale/getAll/' + _branchId).toPromise();
  }

}