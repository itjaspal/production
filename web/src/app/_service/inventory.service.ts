import { ProductView } from './../_model/product';
import { StockLocation, SerialNoSearchView } from './../_model/stock-location';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from './../../environments/environment';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InventoryService {

  constructor(private http: HttpClient) { }

  public async getInquirySaleStockLocation(_entityType: number = 0) {
    return await this.http.get<StockLocation[]>(environment.API_URL + 'inventory/getInquirySaleStockLocation/' + _entityType).toPromise();
  }

  public async getInquiryStockLocationByEntityCode(_entityCode: string) {
    return await this.http.get<StockLocation[]>(environment.API_URL + 'inventory/getInquiryStockLocationByEntityCode/' + _entityCode).toPromise();
  }

  public async getInquiryControlStockLocation(_entityType: number = 0) {
    return await this.http.get<StockLocation[]>(environment.API_URL + 'inventory/getInquiryControlStockLocation/' + _entityType).toPromise();
  }

  public async getInquiryControlStockLocationByEntityCode(_entityCode: string) {
    return await this.http.get<StockLocation[]>(environment.API_URL + 'inventory/getInquiryControlStockLocationByEntityCode/' + _entityCode).toPromise();
  }

  public async getInquiryControlStockLocationByBranchId(_branchId: number) {
    return await this.http.get<StockLocation[]>(environment.API_URL + 'inventory/getInquiryControlStockLocationByBranchid/' + _branchId).toPromise();
  }

  public postInquirySerialNoByText(_model: SerialNoSearchView): Observable<string> {
    return this.http.post<string[]>(environment.API_URL + 'inventory/postInquirySerialNoByText', _model)
      .pipe(
        map((res: any) => {
          return res;
        })
      );
  }

  public postInquiryStockLocationBalance(_branchId, _stockLocationId, _products) {
    let model = {
      branchId: _branchId,
      stockLocationId: _stockLocationId,
      products: _products
    }
    return this.http.post<ProductView[]>(environment.API_URL + 'inventory/postInquiryStockLocationBalance', model).toPromise();
  }

  public async updateStockAudit(_model) {
    return await this.http.post<number>(environment.API_URL + 'inventory/postUpdateStockAudit', _model).toPromise();
  }

  public async updateStockAdjust(_model) {
    return await this.http.post<number>(environment.API_URL + 'inventory/postUpdateStockAdjust', _model).toPromise();
  }

  public async checkStockInfo(_model) {
    return await this.http.post<number>(environment.API_URL + 'inventory/postCheckStockInfo', _model).toPromise();
  }

  public async searchStockAudit(_model) {
    return await this.http.post<any>(environment.API_URL + 'inventory/postSearchStockAudit', _model).toPromise();
  }

  public async viewStockAuditItem(_model) {
    return await this.http.post<any>(environment.API_URL + 'inventory/postViewStockAuditItem', _model).toPromise();
  }

}
