import { environment } from '../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RawProductSearchView, RawProductView, PrintTagSearchView, PrintTagView } from '../_model/print-tag';

@Injectable({
    providedIn: 'root' 
  })
  export class PrintTagService {
  
    constructor(private http: HttpClient) { }
    
    public async searchPrintTag(_model: PrintTagSearchView) {
      return await this.http.post<PrintTagView[]>(environment.API_URL + 'print-tag/postSearchPrintTag', _model).toPromise();
    }
    
    public async searchRawItem(_model: RawProductSearchView) {
      return await this.http.post<RawProductView[]>(environment.API_URL + 'print-tag/postSearchRawData', _model).toPromise();
    }

    
  }  