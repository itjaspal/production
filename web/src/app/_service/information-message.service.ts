import { environment } from '../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Injectable({
    providedIn: 'root'
})
export class InformationMessageService {
    constructor(private http: HttpClient) { }

    public async search(_model: any) {
        return await this.http.post(environment.API_URL + 'informationMessage/postSearch', _model).toPromise();
    }

    public async save(_model: any) {
        return await this.http.post(environment.API_URL + 'informationMessage/postSave', _model).toPromise();
    }

    public async getInfo(_Id: number) {
        return await this.http.get(environment.API_URL + 'informationMessage/getInfo/' + _Id).toPromise();
    }   

    public async pcView(_model: any) {
        return await this.http.post(environment.API_URL + 'informationMessage/postPcView', _model).toPromise();
    }
}