import { AppSetting } from "../_constants/app-setting";

export class PrintTagSearchView {
    public entity: string = AppSetting.entity;    
    public req_date: string = "";  
    public wc_code: string = "";  
    public mc_code: string = "";       
    public spring_grp: string = "";  
    public size_desc: string = "";  
    public qty :number;
    public user_id: string = "";  
    public printer: string = ""; 
}

export class PrintTagView {
    public entity: string = "";
    public process_tag_no : number = 1;   
    public req_date: string = "";  
    public wc_code: string = "";  
    public mc_code: string = "";    
    public spring_grp: string = "";  
    public size_desc: string = "";  
    public qty :number = 0;
    public fin_date: string = "";  
    public printer: string = "";  
    public user_id : string = "";  
    public datas: RawMatitemView[] = [];
}

export class RawMatitemView {
    public process_tag_no : number = 1; 
    public doc_no :string = "";
    public prod_code :string = "";
    public prod_name :string = "";

}

export class SpringTagSearchView
{
    public pageIndex: number = 1;
    public itemPerPage: number = AppSetting.itemPerPage;
    public entity : string = AppSetting.entity; 
    public user_id : string = "";
    public mc_code : string = "";
    public wc_code : string = "";

}

export class SpringTagView
{
    public req_date : string = "";
    public spring_grp : string = "";
    public total_plan_qty : number = 0;
    public total_actual_qty : number = 0;
    public total_diff_qty : number = 0;
    public datas : RawMatitemView[] = [];

}

export class SpringTagDataView
{
    public pageIndex: number = 1;
    public itemPerPage: number = AppSetting.itemPerPage;
    public totalItem : number = 0
    public req_date : string = "";
    public pdsize_code : string = "";
    public pdsize_desc : string = "";
    public spring_grp : string = "";
    public plan_qty : number = 0;
    public actual_qty : number = 0;
    public diff_qty : number = 0;
}

export class RawProductSearchView {
    // public isEdit: boolean = false;
    // public itemPerPage: number = 0;

    // public editItem: RawMatitemView = null;
    public pageIndex: number = 1;
    public itemPerPage: number = AppSetting.itemPerPage;
    public doc_date : string = "";
}

export class RawMatView{
    public pageIndex: number = 1;
    public itemPerPage: number = AppSetting.itemPerPage;
    public totalItem : number = 0
    public datas : RawProductView[] = [];
}
export class RawProductView {
    public doc_no : string = "";
    public prod_code : string = "";
    public prod_name : string = "";
}

export class PrintTagAddView{
    public entity : string = AppSetting.entity;
    public req_date : string = "";
    public mc_code : string = "";
    public process_tag_no : number = 0;
    public doc_no: string = "";
    public prod_code : string = "";
    public prod_name : string = "";
    public qr : string = "";
}


export class PrintTagProcView{
    public entity : string = AppSetting.entity;
    public req_date : string = "";
    public mc_code : string = "";
    public process_tag_no : number = 0;
   
}

export class ProcessTagSearchView{
    public entity : string = AppSetting.entity;
    public req_date : string = "";
    public mc_code : string = "";
}

export class ProcessTagView{
    public process_tag_no : number = 0;
}

export class ProcessTagNoSearch{
    public entity: string = AppSetting.entity;    
    public req_date: string = "";  
    public wc_code: string = "";  
    public mc_code: string = "";       
    public spring_grp: string = "";  
    public size_desc: string = "";  
    public qty :number; 
    public printer: string = ""; 
    public process_tag_no : number = 0;
}

export class RawMatScanSerchView{
    public process_tag_no : number = 0;  
    public qr : string = "";  
}
    