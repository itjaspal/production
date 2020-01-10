import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { CustomerView } from '../_model/customer-view';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  constructor(private http: HttpClient) { }

  public async getSaleTarget(search) {
    return await this.http.post(environment.API_URL + 'dashboard/postGetSaleTarget', search).toPromise();
  }

  public async updateSaleTarget(model) {
    return await this.http.post(environment.API_URL + 'dashboard/postUpdateSaleTarget', model).toPromise();
  }

  public async postGetSaleTargetSummary(model) {
    return await this.http.post(environment.API_URL + 'dashboard/postGetSaleTargetSummary', model).toPromise();
  }

}
