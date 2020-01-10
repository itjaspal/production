import { AppSetting } from '../_constants/app-setting';

export class ProductAttributeSearchView {
    public pageIndex: number = 1;
    public itemPerPage: number = AppSetting.itemPerPage;
    public productAttributeTypeCode: string = "";
    public code: string = "";
    public name: string = "";    
    public productAttributeList: any = [];    
    public status : string = "";
}

export class ProductAttributeView {
    public productAttributeId: number = undefined;
    public productAttributeTypeCode: string = "";
    public code: string = "";
    public name: string = "";   
    public status: string = "";
}