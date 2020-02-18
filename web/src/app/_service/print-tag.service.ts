import { environment } from '../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
    providedIn: 'root' 
  })
  export class PrintTagService {
  
    constructor(private http: HttpClient) { }
    
    // public async searchcspring(_model: JobSendSearchView) {
    //   return await this.http.post<JobSendView>(environment.API_URL + 'scan-send/postSearchSpring', _model).toPromise();
    // }
  }  