import { environment } from './../../environments/environment';
import { CommonSearchView } from './../_model/common-search-view';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { PromotionView, PromotionSearchView } from './../_model/promotion';

@Injectable({
    providedIn: 'root'
})
export class PromotionService {
    constructor(private http: HttpClient) { }

    public async search(_model: any) {
        return await this.http.post(environment.API_URL + 'promotion/postSearch', _model).toPromise();
    }

    public async save(_model: any) {
        return await this.http.post(environment.API_URL + 'promotion/postSave', _model).toPromise();
    }

    public async getInfo(_promotionId: number) {
        return await this.http.get(environment.API_URL + 'promotion/getInfo/' + _promotionId).toPromise();
    }

    public async getPromotionByBranch(_branchId: number) {
        return await this.http.get(environment.API_URL + 'promotion/getPromotionByBranch/'+_branchId).toPromise();
    }
    
    public async postIsExitingPromotion(_branchId: number, _promotionCode: string) {
        let model = {
            branchId: _branchId,
            promotionCode: _promotionCode
        }
        return await this.http.post<boolean>(environment.API_URL + 'promotion/postIsExitingPromotion', model).toPromise();
    }
}