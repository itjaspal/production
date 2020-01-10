import { Observable } from 'rxjs';
import { environment } from './../../environments/environment';
import { CommonSearchView } from './../_model/common-search-view';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SaleTransactionView, SaleTransactionSearchView } from '../_model/sale-transaction';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class SaleTransactionService {

  constructor(private http: HttpClient) { }


  public async postInquirySaleTransaction(_model: SaleTransactionSearchView){
    return await this.http.post<CommonSearchView<SaleTransactionView>>(environment.API_URL + 'sale/postInquirySaleTransaction', _model).toPromise();
  }

  public async postCreateSaleTransaction(_model: SaleTransactionView) {
    return await this.http.post<number>(environment.API_URL + 'sale/postCreateSaleTransaction', _model).toPromise();
  }

  public async getInquirySaleTransactionInfo(_saleTransactionId) {
    return await this.http.get<SaleTransactionView>(environment.API_URL + 'sale/getInquirySaleTransactionInfo/' + _saleTransactionId).toPromise();
  }
  
  public async getInquiryReturnSaleBedLinenInfo(_saleTransactionId) {
    return await this.http.get<SaleTransactionView>(environment.API_URL + 'sale/getInquiryReturnSaleBedLinenInfo/' + _saleTransactionId).toPromise();
  }

  public async postUpdateSaleTransaction(_model: SaleTransactionView) {
    return await this.http.post<number>(environment.API_URL + 'sale/postUpdateSaleTransaction', _model).toPromise();
  }

  public async postUpdateToReady(_model: any) {
    return await this.http.post<number>(environment.API_URL + 'sale/postUpdateToReady', _model).toPromise();
  }
  
  public async postCancelSaleTransaction(_model: any) {
    return await this.http.post<number>(environment.API_URL + 'sale/postCancelSaleTransaction', _model).toPromise();
  }
  
  public async postCreateReturnSaleBedLinen(_model: SaleTransactionView) {
    return await this.http.post<number>(environment.API_URL + 'sale/postCreateReturnSaleBedLinen', _model).toPromise();
  }
  
  public async postUpdateReturnSaleBedLinen(_model: SaleTransactionView) {
    return await this.http.post<number>(environment.API_URL + 'sale/postUpdateReturnSaleBedLinen', _model).toPromise();
  }

  public postInquirySaleTransactionByText(_model: any): Observable<SaleTransactionView> {
    return this.http.post<SaleTransactionView[]>(environment.API_URL + 'sale/postInquirySaleTransactionByText', _model)
      .pipe(
        map((res: any) => {
          return res;
        })
      );
  }

}
