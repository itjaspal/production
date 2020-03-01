import { environment } from '../../environments/environment';
import { CommonSearchView } from '../_model/common-search-view';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { JobSendSearchView, JobSendView, ScanPcsSearchView, ScanPcsView, ScanSendFinSearchView, ScanSendFinView, ScanSendProcView } from '../_model/job-send';
import { DataEntrySearchView } from '../_model/job-inprocess';



@Injectable({
  providedIn: 'root' 
})
export class JobSendService {

  constructor(private http: HttpClient) { }
  
  public async searchcspring(_model: JobSendSearchView) {
    return await this.http.post<JobSendView>(environment.API_URL + 'scan-send/postSearchSpring', _model).toPromise();
  }

  // Process Scan / Cancel
  public async searchscanpcs(_model: ScanPcsSearchView) {
    return await this.http.post<ScanPcsView>(environment.API_URL + 'scan-send/postSearchScanPcs', _model).toPromise();
  }

  public async searchscancancelpcs(_model: ScanPcsSearchView) {
    return await this.http.post<ScanPcsView>(environment.API_URL + 'scan-send/postSearchScanCancelPcs', _model).toPromise();
  }

  //List PCS ท่ี่มีการ Scan แล้ว
  public async searchfinpcs(_model: ScanSendFinSearchView) {
    return await this.http.post<ScanSendFinView>(environment.API_URL + 'scan-send/postSerachFinPcs', _model).toPromise();
  }

  public async searchcanpcs(_model: ScanSendFinSearchView) {
    return await this.http.post<ScanSendFinView>(environment.API_URL + 'scan-send/postSerachCanPcs', _model).toPromise();
  }

  
  // public async scanpcs(_model: ScanSendProcView) {
  //   return await this.http.post(environment.API_URL + 'scan-send/postScanPcs', _model).toPromise();  
  // }

  // public async cancelpcs(_model: ScanSendProcView) {
  //   return await this.http.post(environment.API_URL + 'scan-send/postCancelPcs', _model).toPromise();  
  // }

  public async updatepcs(_model: DataEntrySearchView) {
    return await this.http.post(environment.API_URL + 'scan-send/postUpdatePcs', _model).toPromise();  
  }

  public async cancelpcs(_model: DataEntrySearchView) {
    return await this.http.post(environment.API_URL + 'scan-send/postCancelPcs', _model).toPromise();  
  }





}