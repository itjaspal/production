import { environment } from '../../environments/environment';
import { CommonSearchView } from '../_model/common-search-view';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { JobInProcessSearchView, JobInProcessView, JobInProcessScanFinView, DataEntrySearchView } from '../_model/job-inprocess';



@Injectable({
  providedIn: 'root' 
})
export class JobinprocessService {

  constructor(private http: HttpClient) { }

  
  public async searchscanpcs(_model: JobInProcessSearchView) {
    return await this.http.post<JobInProcessView>(environment.API_URL + 'job-inprocess/postSearchScanPcs', _model).toPromise();
  }

  public async searchscancancelpcs(_model: JobInProcessSearchView) {
    return await this.http.post<JobInProcessView>(environment.API_URL + 'job-inprocess/postSearchScanCancelPcs', _model).toPromise();
  }

  public async searchfinpcs(_model: JobInProcessSearchView) {
    return await this.http.post<JobInProcessScanFinView>(environment.API_URL + 'job-inprocess/postSerachFinPcs', _model).toPromise();
  }

  public async searchcancelpcs(_model: JobInProcessSearchView) {
    return await this.http.post<JobInProcessScanFinView>(environment.API_URL + 'job-inprocess/postSerachCancelPcs', _model).toPromise();
  }

  public async searchcanpcs(_model: JobInProcessSearchView) {
    return await this.http.post<JobInProcessScanFinView>(environment.API_URL + 'job-inprocess/postSerachCanPcs', _model).toPromise();
  }

  // public async updatepcs(_model: DataEntrySearchView) {
  //   return await this.http.post(environment.API_URL + 'job-inprocess/postUpdatePcs', _model).toPromise();  
  // }
  public async updatepcs(_model: DataEntrySearchView) {
    return await this.http.post<JobInProcessScanFinView>(environment.API_URL + 'job-inprocess/postUpdatePcs', _model).toPromise();
  }

  public async cancelpcs(_model: DataEntrySearchView) {
    return await this.http.post(environment.API_URL + 'job-inprocess/postCancelPcs', _model).toPromise();  
  }


  

  


}