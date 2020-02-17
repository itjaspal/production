import { environment } from '../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonSearchView } from '../_model/common-search-view';
import { AuthenticationService } from './authentication.service';
import { DisplayJobSearchView, DisplayJobView } from '../_model/displayJob';


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

        //this.user = this._authSvc.getLoginUser();

        console.log('pending cur_userID : '+ this.user.username);
        console.log('pending cur_deptCode : '+ this.user.dept_code);
        console.log('pending cur_mcCode : '+ this.user.user_mac.MC_CODE);
        console.log('pending cur_wcCode : '+ this.user.def_wc_code);

        _model.mc_code = this.user.user_mac.MC_CODE;
        //_model.mc_code = "PT001";
        _model.user_id = this.user.username;
        _model.wc_code = this.user.def_wc_code;

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

}