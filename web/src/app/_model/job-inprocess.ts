import { AppSetting } from "../_constants/app-setting";

export class JobInProcessSearchView {
    public pageIndex: number = 1;
    public itemPerPage: number = AppSetting.itemPerPage;
    public entity : string = AppSetting.entity;  
    public req_date : string = "";  
    public pcs_barcode: string = "";  
    public wc_code : string = "";  
    public mc_code : string = "";  
    public user_id : string = "";  
    public spring_grp : string = "";  
    public size_code : string = "";  
}

export class JobInProcessView {
    public pageIndex: number = 1;
    public itemPerPage: number = AppSetting.itemPerPage;
    public totalItem: number = 0;
    public pcs_barcode : string = "";  
    public pdsize_desc : string = "";  
    public springtype_code : string = "";  
    public qty : number ;
    public prod_code : string = ""; 
   // public datas: JobInProcessScanView[] = [];
}

export class JobInProcessScanFinView
{
    public pageIndex: number = 1;
    public itemPerPage: number = AppSetting.itemPerPage;
    public totalItem : number = 0;
    public datas: JobInProcessScanView[] = [];
}

export class JobInProcessScanView
{
    public pcs_barcode : string = "";  
    public pdmodel_code : string = "";  
    public prod_code : string = "";  
    public prod_name : string = "";  
    public qty : number = 0;
}

export class DataEntrySearchView
{
    public entity : string = AppSetting.entity;  
    public req_date : string = "";  
    public wc_code : string = "";  
    public mc_code : string = "";  
    public user_id : string = "";  
    public spring_grp : string = "";  
    public size_code : string = "";  
    public qty : number = 1;  
}
   
