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
    public springtype_code: string = "";
    public plan_qty: string = "";
    public actual_qty: string = "";
    public diff_qty: string = "";
    public pcs_barcode: string = ""; 
} 