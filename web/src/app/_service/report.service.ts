import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { saveAs } from 'file-saver';
import { map } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class ReportService {
    constructor(private http: HttpClient) { }

    public async saleTransactionReport(_model: any) {
        return await this.http.post(environment.API_URL + 'report/postSaleTransactionReport', _model).toPromise();
    }

    public async saleTransactionByProductReport(_model: any) {
        return await this.http.post(environment.API_URL + 'report/postSaleTransactionByProductReport', _model).toPromise();
    }

    public async productDetailReport(_model: any) {
        return await this.http.post(environment.API_URL + 'report/postProductDetailReport', _model).toPromise();
    }

    public async addReturnTransactionReport(_model: any) {
        return await this.http.post(environment.API_URL + 'report/postAddReturnTransactionReport', _model).toPromise();
    }

    public async productAtofDateReport(_model: any) {
        return await this.http.post(environment.API_URL + 'report/postProductAtofDateReport', _model).toPromise();
    }

    public async productMoveMentReport(_model: any) {
        return await this.http.post(environment.API_URL + 'report/postProductMoveMentReport', _model).toPromise();
    }

    public async dailySaleReport(_model: any) {
        return await this.http.post(environment.API_URL + 'report/postDailySaleReport', _model).toPromise();
    }

    private export(url, param, filename) {
        url = environment.API_URL + url;

        this.http.post(url, param, { responseType: 'blob' })
            .pipe(
                map((response: any) => {
                    if (response instanceof Response) {
                        return response.blob();
                    }
                    return response;
                })
            ).subscribe(data => saveAs(data, filename),
                error => console.log(error)
            );
    }

    public saleTransactionReportExport(_model) {
        this.export("report/postSaleTransactionReportExport", _model, "report.xlsx");
    }

    public async stockAuditReport(_model: any) {
        return await this.http.post(environment.API_URL + 'report/postStockAuditReport', _model).toPromise();
    }

    public async requestOrderReport(_model: any) {
        return await this.http.post(environment.API_URL + 'report/postReqOrderReport', _model).toPromise();
    }

    public async requestReturnReport(_model: any) {
        return await this.http.post(environment.API_URL + 'report/postReqReturnReport', _model).toPromise();
    }

    public async productTransaferReport(_model: any) {
        return await this.http.post(environment.API_URL + 'report/postProductTransferReport', _model).toPromise();
    }
}