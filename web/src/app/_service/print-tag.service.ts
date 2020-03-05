import { environment } from '../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RawProductSearchView, RawMatView, PrintTagSearchView, PrintTagView, PrintTagAddView, PrintTagProcView, ProcessTagSearchView, ProcessTagView, ProcessTagNoSearch, RawMatScanSerchView, RawProductView } from '../_model/print-tag';
import { CommonSearchView } from '../_model/common-search-view';

@Injectable({
    providedIn: 'root' 
  })
  export class PrintTagService {
  
    constructor(private http: HttpClient) { }
    
    public async searchPrintTag(_model: PrintTagSearchView) {
      return await this.http.post<PrintTagView[]>(environment.API_URL + 'print-tag/postSearchPrintTag', _model).toPromise();
    }
    
    public async searchRawItem(_model: RawProductSearchView) {
      return await this.http.post<RawMatView[]>(environment.API_URL + 'print-tag/postSearchRawData', _model).toPromise();
    }

    public async PringTag(_model: PrintTagView) {
      return await this.http.post(environment.API_URL + 'print-tag/postPrintTag', _model).toPromise();  
    }

    public async AddTag(_model: PrintTagSearchView) {
      return await this.http.post(environment.API_URL + 'print-tag/postAddTag', _model).toPromise();  
    }


    public async DeleteTag(_model: PrintTagProcView) {
      return await this.http.post(environment.API_URL + 'print-tag/postDeleteTag', _model).toPromise();  
    }
  
    public async tagnoList(_model: ProcessTagSearchView) {
      return await this.http.post<CommonSearchView<ProcessTagView>>(environment.API_URL + 'print-tag/postSearchTagNoList', _model).toPromise();
    }

    public async searchtagno(_model: ProcessTagNoSearch) {
      return await this.http.post<PrintTagView[]>(environment.API_URL + 'print-tag/postSearchTagNo', _model).toPromise();
    }

    public async searchrawscan(_model: RawMatScanSerchView) {
      return await this.http.post<RawProductView[]>(environment.API_URL + 'print-tag/postSearchRawScan', _model).toPromise();
    }
    
  }  