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
    
    this.user = this._authSvc.getLoginUser();

  }

  public async searchJobByMacCurrent(_model: DisplayJobSearchView) {

        //this.user = this._authSvc.getLoginUser();

        console.log('Current cur_userID : '+ this.user.username);
        console.log('Current cur_deptCode : '+ this.user.dept_code);
        console.log('Current cur_mcCode : '+ this.user.user_mac.MC_CODE);
        console.log('Current cur_wcCode : '+ this.user.def_wc_code);

        _model.mc_code = this.user.user_mac.MC_CODE;
        _model.user_id = this.user.username;
        _model.wc_code = this.user.def_wc_code;

        return await this.http.post<CommonSearchView<DisplayJobView>>(environment.API_URL + 'job/postSearchcurrent', _model).toPromise(); 
 
  } 

  public async searchJobByMacPending(_model: DisplayJobSearchView) {

        console.log('pending cur_userID : '+ this.user.username);
        console.log('pending cur_deptCode : '+ this.user.dept_code);
        console.log('pending cur_mcCode : '+ this.user.user_mac.MC_CODE);
        console.log('pending cur_wcCode : '+ this.user.def_wc_code);

        _model.mc_code = this.user.user_mac.MC_CODE;
        //_model.mc_code = "PT001";
        _model.user_id = this.user.username;
        _model.wc_code = this.user.def_wc_code; 

        //this.user = this._authSvc.getLoginUser();
        return await this.http.post<CommonSearchView<DisplayJobView>>(environment.API_URL + 'job/postSearchpending', _model).toPromise(); 

}

public async searchJobByMacForward(_model: DisplayJobSearchView) {

        //this.user = this._authSvc.getLoginUser();

        console.log('Forward cur_userID : '+ this.user.username);
        console.log('Forward cur_deptCode : '+ this.user.dept_code);
        console.log('Forward cur_mcCode : '+ this.user.user_mac.MC_CODE);
        console.log('Forward cur_wcCode : '+ this.user.def_wc_code);

        _model.mc_code = this.user.user_mac.MC_CODE;
        _model.user_id = this.user.username;
        _model.wc_code = this.user.def_wc_code;

        return await this.http.post<CommonSearchView<DisplayJobView>>(environment.API_URL + 'job/postSearchforward', _model).toPromise(); 

}

public async searchSpring(_model: DisplayJobSearchView) {

  //this.user = this._authSvc.getLoginUser();

  console.log('searchSpring cur_userID : '+ this.user.username);
  console.log('searchSpring cur_deptCode : '+ this.user.dept_code);
  console.log('searchSpring cur_mcCode : '+ this.user.user_mac.MC_CODE);
  console.log('searchSpring cur_wcCode : '+ this.user.def_wc_code);

  _model.mc_code = this.user.user_mac.MC_CODE;
  _model.user_id = this.user.username;
  _model.wc_code = this.user.def_wc_code;

  return await this.http.post<SearchSpringHeaderView<SearchSpringDetailView>>(environment.API_URL + 'spring/postSearchspring', _model).toPromise(); 

}

public async searchSpringByDate(_model: SearchSpringByDateSearchView) {

  //this.user = this._authSvc.getLoginUser();

  console.log('searchSpringByDate cur_userID : '+ this.user.username);
  console.log('searchSpringByDate cur_deptCode : '+ this.user.dept_code);
  console.log('searchSpringByDate cur_mcCode : '+ this.user.user_mac.MC_CODE);
  console.log('searchSpringByDate cur_req_date : '+ _model.req_date);
  

  _model.mc_code = this.user.user_mac.MC_CODE; 
  _model.user_id = this.user.username;
  _model.wc_code = this.user.def_wc_code;

  return await this.http.post<SearchSpringHeaderView<SearchSpringDetailView>>(environment.API_URL + 'spring/postSearchspringdate', _model).toPromise(); 

}

public async searchJobMacCurrentByDate(_model: SearchSpringByDateSearchView) {

  //this.user = this._authSvc.getLoginUser(); Test

  console.log('searchJobMacCurrentByDate cur_userID : '+ this.user.username);
  console.log('searchJobMacCurrentByDate cur_deptCode : '+ this.user.dept_code);
  console.log('searchJobMacCurrentByDate cur_mcCode : '+ this.user.user_mac.MC_CODE);
  console.log('searchJobMacCurrentByDate cur_wcCode : '+ this.user.def_wc_code);  
  

  _model.mc_code = this.user.user_mac.MC_CODE;
  _model.user_id = this.user.username;
  _model.wc_code = this.user.def_wc_code; 

  return await this.http.post<CommonSearchView<DisplayJobView>>(environment.API_URL + 'job/postSearchdate', _model).toPromise(); 

} 

}