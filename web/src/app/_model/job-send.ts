import { AppSetting } from "../_constants/app-setting";

export class JobSendSearchView {
    public pageIndex: number = 1;
    public itemPerPage: number = AppSetting.itemPerPage;
    //public ic_entity: string = "";
    public prod_code: string = "";    
    public prod_name: string = "";    
    public bar_code: string = "";    
    public pcs_barcode: string = "";    
    public pdgrp_code: string = "";
    public pdtype_code: string = "";
    public pdbrnd_code: string = "";
    public pddsgn_code: string = "";
    public pdmodel_code: string = "";
    public pdcolor_code: string = "";
    public pdsize_code: string = "";
    public status: string = "";
    //public wh_code: string ="";
    
    
}

export class JobSendView {
    public prod_code: string = "";    
    public prod_name: string = "";    
    public bar_code: string = "";    
    public pcs_barcode: string = "";    
    public pdgrp_code: string = "";
    public pdtype_code: string = "";
    public pdbrnd_code: string = "";
    public pddsgn_code: string = "";
    public pdmodel_code: string = "";
    public pdcolor_code: string = "";
    public pdsize_code: string = "";
    public status: string = "";
    
}
