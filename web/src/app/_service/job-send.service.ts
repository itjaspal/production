import { environment } from '../../environments/environment';
import { CommonSearchView } from '../_model/common-search-view';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { JobSendSearchView, JobSendView, ScanPcsSearchView, ScanPcsView, ScanSendFinSearchView, ScanSendFinView } from '../_model/job-send';



@Injectable({
  providedIn: 'root' 
})
export class JobSendService {

  constructor(private http: HttpClient) { }
  
  public async searchcspring(_model: JobSendSearchView) {
    return await this.http.post<JobSendView>(environment.API_URL + 'scan-send/postSearchSpring', _model).toPromise();
  }

  public async searchscanpcs(_model: ScanPcsSearchView) {
    return await this.http.post<ScanPcsView>(environment.API_URL + 'scan-send/postSearchScanPcs', _model).toPromise();
  }

  public async searchfinpcs(_model: ScanSendFinSearchView) {
    return await this.http.post<ScanSendFinView>(environment.API_URL + 'scan-send/postSerachFinPcs', _model).toPromise();
  }



}