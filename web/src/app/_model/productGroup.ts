import { AppSetting } from "../_constants/app-setting";

export class ProductGroupSearchView {
    public pageIndex: number = 1;
    public itemPerPage: number = AppSetting.itemPerPage;
    public pdgrp_code: string ="";
    public pdgrp_name: string ="";
    public status: string ="";

    
    
}

export class ProductGroupView {
    public pdgrp_code: string ="";
    public pdgrp_name: string ="";
    public status: string ="";
}
