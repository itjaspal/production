import { AppSetting } from "../_constants/app-setting";

export class JobMachineSearchView {
    public pageIndex: number = 1;
    public itemPerPage: number = AppSetting.itemPerPage
    public entity : string ="";
    public user_id : string ="";
    public mc_code : string ="";
    public wc_code : string ="";
    
    
}

export class JobMachineView {
    public pageIndex: number = 1;
    public itemPerPage: number = AppSetting.itemPerPage
    public totalItem : number = 0;
    public datas: JobMachineReqView[] = [];
    
}

export class JobMachineReqView {
    public req_date : any = null;
    public pdsize_desc : string ="";
    public springtype_code : string ="";
    public plan_qty : number = 0;
    public actual_qty : number = 0;
    public diff_qty : number = 0;
    public pcs_barcode : string ="";
}
