import { environment } from '../../environments/environment';
import { CommonSearchView } from '../_model/common-search-view';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { JobInProcessSearchView, JobInProcessView } from '../_model/job-inprocess';



@Injectable({
  providedIn: 'root' 
})
export class JobinprocessService {

  constructor(private http: HttpClient) { }

  public async searchcurrent(_model: JobInProcessSearchView) {
    return await this.http.post<JobInProcessView>(environment.API_URL + 'job/postSearchcurrent', _model).toPromise();
  }

  public async search(_model: JobInProcessSearchView) {
    return await this.http.post<CommonSearchView<JobInProcessView>>(environment.API_URL + 'stock/postSearch', _model).toPromise();
  }

}