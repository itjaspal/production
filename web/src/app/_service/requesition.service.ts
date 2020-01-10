import { CommonSearchView } from './../_model/common-search-view';
import { SaleReturnProductView } from './../_model/sale-transaction';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { RequesitionView, RequesitionSearchView, RequesitionItemView } from '../_model/requesition';

@Injectable({
  providedIn: 'root'
})
export class RequesitionService {

  constructor(private http: HttpClient) { }

  public postInquiryRequesitionByDocNbr(_model: any): Observable<RequesitionView> {
    return this.http.post<RequesitionView[]>(environment.API_URL + 'request/postInquiryRequesitionByDocNbr', _model)
      .pipe(
        map((res: any) => {
          return res;
        })
      );
  }

  public async postInquiryRequesitionItemByDocNbr(model: RequesitionSearchView) {
    return await this.http.post<SaleReturnProductView[]>(environment.API_URL + 'request/postInquiryRequesitionItemByDocNbr', model).toPromise();
  }

  public async getInquiryConsignmentItemById(id: any, stockLocationId: number) {
    return await this.http.get<SaleReturnProductView>(environment.API_URL + 'request/getInquiryConsignmentItemById/' + id + '/' + stockLocationId).toPromise();
  }

  public async postInquiryRequesition(model: RequesitionSearchView) {
    return await this.http.post<CommonSearchView<RequesitionView>>(environment.API_URL + 'request/postInquiryRequesition', model).toPromise();
  }

  public async postCreateRequesition(model: RequesitionView) {
    return await this.http.post<number>(environment.API_URL + 'request/postCreateRequesition', model).toPromise();
  }

  public async postUpdateRequesition(model: RequesitionView) {
    return await this.http.post<number>(environment.API_URL + 'request/postUpdateRequesition', model).toPromise();
  }

  public async postCancelRequesition(model: any) {
    return await this.http.post(environment.API_URL + 'request/postCancelRequesition', model).toPromise();
  }

  public async getInquiryRequesitionInfo(id: any) {
    return await this.http.get<RequesitionView>(environment.API_URL + 'request/getInquiryRequesitionInfo/' + id).toPromise();
  }

  public async postUpdateToReady(model: any) {
    return await this.http.post(environment.API_URL + 'request/postUpdateToReady', model).toPromise();
  }

  public async postInquiryReceiveTransfer(model: RequesitionSearchView) {
    return await this.http.post<CommonSearchView<RequesitionView>>(environment.API_URL + 'request/postInquiryReceiveTransfer', model).toPromise();
  }
}
