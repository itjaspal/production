import { AppSetting } from "../_constants/app-setting";


export class DisplayJobSearchView {
    public pageIndex: number = 1;
    public itemPerPage: number = AppSetting.itemPerPage;
    public entity: string = AppSetting.entity;
    public user_id: string = "";
    public mc_code: string = "";
    public wc_code: string = "";
}



export class DisplayJobView {
    public req_date: string = "";
    public pdsize_code: string = "";
    public pdsize_desc: string = "";
    public spring_grp: string = "";
    public plan_qty: number;
    public actual_qty: number;
    public diff_qty: number; 
    public pcs_barcode: number; 
}  

export class SearchSpringByDateSearchView {
    public pageIndex: number = 1;
    public itemPerPage: number = AppSetting.itemPerPage;
    public entity: string = AppSetting.entity;
    public user_id: string = "";
    public mc_code: string = "";
    public wc_code: string = "";
    public req_date: string = "";
}

export class SearchSpringHeaderView<T>{ 
    public pageIndex: number = 0;
    public itemPerPage: number = 0;
    public totalItem: number = 0;
    public req_date: Date;
    public spring_grp: string = "";
    public total_plan_qty: number = 0;
    public total_actual_qty: number = 0;
    public total_diff_qty: number = 0;
    public datas: Array<T> = [];  
}

export class SearchSpringDetailView {
    public req_date: Date;
    public pdsize_code: string = "";
    public pdsize_desc: string = "";
    public springtype_code: string = "";
    public plan_qty: number = 0;
    public actual_qty: number = 0;
    public diff_qty: number = 0;

}






