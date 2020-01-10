import { environment } from './../../environments/environment';
import { CommonSearchView } from './../_model/common-search-view';
import { ProductSearchView, ProductView } from './../_model/product';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http: HttpClient) { }

  public async search(_model: ProductSearchView) {
    return await this.http.post<CommonSearchView<ProductView>>(environment.API_URL + 'master-product/postSearch', _model).toPromise();
  }

  public async create(_model: ProductView) {
    return await this.http.post<number>(environment.API_URL + 'master-product/postCreate', _model).toPromise();
  }

  public async update(_model: ProductView) {
    return await this.http.post<number>(environment.API_URL + 'master-product/postUpdate', _model).toPromise();
  }

  public async getInfo(_productId: number) {
    return await this.http.get<ProductView>(environment.API_URL + 'master-product/getInfo/' + _productId).toPromise();
  }

  public async searchAssignBranch(_model: ProductSearchView) {
    return await this.http.post<CommonSearchView<ProductView>>(environment.API_URL + 'master-product/postSearchAssignBranch', _model).toPromise();
  }

  public async assignBranch(_model) {
    return await this.http.post<number>(environment.API_URL + 'master-product/postAssignBranch', _model).toPromise();
  }

  public async getAssignBranch(_branchId: number) {
    return await this.http.get<CommonSearchView<ProductView>>(environment.API_URL + 'master-product/getAssignBranch/' + _branchId).toPromise();
  }

  public async deleteAssignBranch(_model) {
    return await this.http.post<number>(environment.API_URL + 'master-product/postDeleteAssignBranch', _model).toPromise();
  }

  public async updateAssignBranch(_model) {
    return await this.http.post<number>(environment.API_URL + 'master-product/postUpdateAssignBranch', _model).toPromise();
  }

  public async searchAssignSKU(_model: ProductSearchView) {
    return await this.http.post<CommonSearchView<ProductView>>(environment.API_URL + 'master-product/postSearchAssignSKU', _model).toPromise();
  }

  public async assignSKU(_model) {
    return await this.http.post<number>(environment.API_URL + 'master-product/postAssignSKU', _model).toPromise();
  }

  public async getAssignSKU(_skuId: number) {
    return await this.http.get<CommonSearchView<ProductView>>(environment.API_URL + 'master-product/getAssignSKU/' + _skuId).toPromise();
  }

  public async deleteAssignSKU(_model) {
    return await this.http.post<number>(environment.API_URL + 'master-product/postDeleteAssignSKU', _model).toPromise();
  }
  
  public postInquiryProductByText(_model: ProductSearchView): Observable<ProductView> {
    return this.http.post<ProductView[]>(environment.API_URL + 'master-product/postInquiryProductByText', _model)
      .pipe(
        map((res: any) => {
          return res;
        })
      );
  }

  public postInquiryProduct(_model: ProductSearchView){
    return this.http.post<ProductView[]>(environment.API_URL + 'master-product/postInquiryProductByText', _model).toPromise();
  }

  public async postInquiryByManual(_model: ProductSearchView) {
    return await this.http.post<ProductView[]>(environment.API_URL + 'master-product/postInquiryByManual', _model).toPromise();
  }

  public async searchAssignAudit(_model: ProductSearchView) {
    return await this.http.post<CommonSearchView<ProductView>>(environment.API_URL + 'master-product/postSearchAssignAudit', _model).toPromise();
  }

}
