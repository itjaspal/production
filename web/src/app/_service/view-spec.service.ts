import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthenticationService } from './authentication.service';
import { ViewSpecSearchView, ViewSpecView, CommonViewSpecView, ViewSpecSearchByPcsView } from '../_model/view-spec';
import { environment } from '../../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class ViewSpecService {

  public scanSearchPcsModel: ViewSpecSearchByPcsView = new ViewSpecSearchByPcsView();

  constructor(
    private http: HttpClient,
    private _authSvc: AuthenticationService,
    
  ) { 

  } 

  public async searchSpecDrawing(_model: ViewSpecSearchView) {

     console.log('searchSpecDrawing req_date : '+ _model.req_date);
     console.log('searchSpecDrawing pdsize_code : '+ _model.pdsize_code);
     console.log('searchSpecDrawing springtype_code : '+ _model.springtype_code);

     return await this.http.post<CommonViewSpecView<ViewSpecView>>(environment.API_URL + 'spec/postdrawning', _model).toPromise(); 
  }

  public async searchSpecDrawingByPcs(scanPcsBarcode: string) {
    
    this.scanSearchPcsModel.pcs_barcode = scanPcsBarcode;
    return await this.http.post<CommonViewSpecView<ViewSpecView>>(environment.API_URL + 'spec/postdrawningpcs', this.scanSearchPcsModel).toPromise();
  }


}
