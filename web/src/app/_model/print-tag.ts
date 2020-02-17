import { AppSetting } from "../_constants/app-setting";

export class PrintTagSearchView {
    public entity: string = AppSetting.entity;    
    public wc_code: string = "";  
    public mc_code: string = "";    
    public req_date: string = "";  
    public spring_type: string = "";  
    public size_code: string = "";  
    public qty :number;
    public user_id: string = "";  
    public printer: string = ""; 
}

export class PrintTagView {
    public entity: string = AppSetting.entity;    
    public wc_code: string = "";  
    public mc_code: string = "";    
    public req_date: string = "";  
    public sprin_type: string = "";  
    public size_code: string = "";  
    public user_id: string = "";  
}