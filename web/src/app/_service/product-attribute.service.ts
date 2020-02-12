import { environment } from './../../environments/environment';
import { CommonSearchView } from './../_model/common-search-view';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ProductAttributeSearchView, ProductAttributeView } from '../_model/productAttribute';


@Injectable({
  providedIn: 'root'
})
export class ProductAttributeService {

  constructor(private http: HttpClient) { }

  public async search(_model: ProductAttributeSearchView) {
    return await this.http.post<CommonSearchView<ProductAttributeView>>(environment.API_URL + 'master-product-attribute/postSearch', _model).toPromise();
  }

  public async create(_model: ProductAttributeView) {
    return await this.http.post(environment.API_URL + 'master-product-attribute/postCreate', _model).toPromise();
  }

  public async update(_model: ProductAttributeView) {
    return await this.http.post(environment.API_URL + 'master-product-attribute/postUpdate', _model).toPromise();
  }

  public async getInfo(_productAttributeId: number) {
    return await this.http.get<ProductAttributeView>(environment.API_URL + 'master-product-attribute/getInfo/' + _productAttributeId).toPromise();
  }

}
