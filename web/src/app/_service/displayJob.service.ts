import { environment } from '../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonSearchView } from '../_model/common-search-view';
import { AuthenticationService } from './authentication.service';
import { DisplayJobSearchView, DisplayJobView, SearchSpringDetailView, SearchSpringHeaderView, SearchSpringByDateSearchView } from '../_model/displayJob';



@Injectable({
  providedIn: 'root' 
}) 
export class DisplayJobService {

  public user: any;

  constructor(
    private http: HttpClient,
    private _authSvc: AuthenticationService,

  ) { 
    
   // this.user = this._authSvc.getLoginUser();

  }

  public async searchJobByMacCurrent(_model: DisplayJobSearchView) { 

    console.log('searchJobByMacCurrent _model.mc_code: '+ _model.mc_code);
    console.log('searchJobByMacCurrent _model.user_id : '+ _model.user_id);
    console.log('searchJobByMacCurrent _model.wc_code : '+ _model.wc_code); 

    return await this.http.post<CommonSearchView<DisplayJobView>>(environment.API_URL + 'job/postSearchcurrent', _model).toPromise(); 
 
  } 

  public async searchJobByMacPending(_model: DisplayJobSearchView) {

        console.log('searchJobByMacPending _model.mc_code: '+ _model.mc_code);
        console.log('searchJobByMacPending _model.user_id : '+ _model.user_id);
        console.log('searchJobByMacPending _model.wc_code : '+ _model.wc_code);
       

       return await this.http.post<CommonSearchView<DisplayJobView>>(environment.API_URL + 'job/postSearchpending', _model).toPromise(); 

}

public async searchJobByMacForward(_model: DisplayJobSearchView) {

      console.log('searchJobByMacForward _model.mc_code: '+ _model.mc_code);
      console.log('searchJobByMacForward _model.user_id : '+ _model.user_id);
      console.log('searchJobByMacForward _model.wc_code : '+ _model.wc_code);

      return await this.http.post<CommonSearchView<DisplayJobView>>(environment.API_URL + 'job/postSearchforward', _model).toPromise(); 

}

public async searchSpring(_model: DisplayJobSearchView) {

  console.log('searchSpring _model.mc_code: '+ _model.mc_code);
  console.log('searchSpring _model.user_id : '+ _model.user_id);
  console.log('searchSpring _model.wc_code : '+ _model.wc_code);

  return await this.http.post<SearchSpringHeaderView<SearchSpringDetailView>>(environment.API_URL + 'spring/postSearchspring', _model).toPromise(); 

}

public async searchSpringByDate(_model: SearchSpringByDateSearchView) {

  console.log('searchSpringByDate _model.mc_code: '+ _model.mc_code);
  console.log('searchSpringByDate _model.user_id : '+ _model.user_id);
  console.log('searchSpringByDate _model.wc_code : '+ _model.wc_code);

  return await this.http.post<SearchSpringHeaderView<SearchSpringDetailView>>(environment.API_URL + 'spring/postSearchspringdate', _model).toPromise(); 

}

public async searchJobMacCurrentByDate(_model: SearchSpringByDateSearchView) {

  console.log('searchJobMacCurrentByDate _model.mc_code: '+ _model.mc_code);
  console.log('searchJobMacCurrentByDate _model.user_id : '+ _model.user_id);
  console.log('searchJobMacCurrentByDate _model.wc_code : '+ _model.wc_code); 

  return await this.http.post<CommonSearchView<DisplayJobView>>(environment.API_URL + 'job/postSearchdate', _model).toPromise(); 

} 

}