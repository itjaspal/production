import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { CustomerView } from '../_model/customer-view';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(private http: HttpClient) { }

  public postInquiryCustomerByText(_type: string, _txt: string): Observable<CustomerView> {
    return this.http.post<CustomerView[]>(environment.API_URL + 'master-customer/postInquiryCustomerByText', {
      type:_type,
      txt: _txt
    })
      .pipe(
        map((res: any) => {
          return res;
        })
      );
  }
}
