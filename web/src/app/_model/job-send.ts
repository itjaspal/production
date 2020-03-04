import { AppSetting } from "../_constants/app-setting";

export class JobSendSearchView {
    public pageIndex: number = 1;
    public itemPerPage: number = AppSetting.itemPerPage;   
    public entity: string = AppSetting.entity;    
    public wc_code: string = "";  
    public mc_code: string = "";    
    public req_date: string = "";    
    
}

export class JobSendView {
    public pageIndex : number = 1;
    public itemPerPage: number = AppSetting.itemPerPage;
    public totalItem : number = 0;
    public req_date : string = "";  
    public mc_code : string = "";  
    public spring_grp : string = "";  
    public total_plan_qty : number = 0;
    public total_inact_qty : number = 0;
    public total_qp_qty : number = 0;
    public total_act_qty : number = 0;
    public total_diff_qty : number = 0;
    public springData: SpringDataView[] = [];
}

export class SpringDataView{
    public req_date : Date = undefined;
    public springtype_code : string = "";  
    public pdsize_code : string = "";  
    public pdsize_desc : string = "";  
    public plan_qty : number = 0;
    public inact_qty : number = 0;
    public qp_qty : number = 0;
    public act_qty : number = 0;
    public diff_qty : number = 0;
}

export class ScanPcsView
{
    public pcs_barcode : string = "";  
    public spring_grp : string = "";  
    public size_desc : string = "";  
    public qty : string = "";  
    public prod_code : string = "";
}

export class ScanSendFinSearchView
{
    public pageIndex : number = 1;
    public itemPerPage: number = AppSetting.itemPerPage;
    public entity : string = AppSetting.entity;   
    public wc_code : string = "";  
    public req_date : string = "";  
    public user_id: string = "";  
    public springtype_code: string = "";  
    public pdsize_code: string = "";  
}

export class ScanSendFinView
{
    public pageIndex : number = 1;
    public itemPerPage: number = AppSetting.itemPerPage;
    public totalItem : number = 0;
    public datas: ScanSendDataView[] = [];
}

export class ScanSendDataView
{
    public pcs_barcode : string = "";  
    public model_desc : string = "";  
    public prod_code : string = "";  
    public prod_name : string = "";  
}

export class ScanSendProcView
{
    public entity : string = AppSetting.entity; 
    public wc_code : string = "";  
    public req_date : string = "";  
    public pcs_barcode : string = "";  
    public spring_grp : string = "";  
    public size_code : string = "";  
    public user_id : string = "";  
}

export class ScanPcsSearchView
{
    public pageIndex : number = 1;
    public itemPerPage: number = AppSetting.itemPerPage;
    public entity : string = AppSetting.entity;
    public req_date : string = "";  
    public wc_code : string = "";  
    public pcs_barcode : string = "";  
    public user_id : string = ""; 
    
}

export class JobSendEntrySearchView {
    public entity: string = AppSetting.entity;    
    public wc_code: string = "";  
    public mc_code: string = "";    
    public req_date: string = "";   
    public user_id : string = "";  
    public spring_grp : string = "";  
    public size_code : string = "";   
    
}
