import { environment } from '../../environments/environment';
import { CommonSearchView } from '../_model/common-search-view';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { JobSendSearchView, JobSendView } from '../_model/job-send';



@Injectable({
  providedIn: 'root' 
})
export class JobSendService {

  constructor(private http: HttpClient) { }
  
  public async searchcspring(_model: JobSendSearchView) {
    return await this.http.post<JobSendView>(environment.API_URL + 'scan-send/postSearchSpring', _model).toPromise();
  }

  // public async search(_model: JobInProcessSearchView) {
  //   return await this.http.post<CommonSearchView<JobInProcessView>>(environment.API_URL + 'stock/postSearch', _model).toPromise();
  // }

}