import { AppSetting } from '../_constants/app-setting';

export class PCSaleSearchView {
    public pageIndex: number = 1;
    public itemPerPage: number = AppSetting.itemPerPage;
    public pcCode: string = "";
    public pcName: string = "";
    public branchId: number = undefined;
    public branchGroupId: number = undefined;
    public departmentId: number = undefined; 
    public status: string ="A";
}

export class PCSaleView {
    public pcSaleId: number = undefined;
    public pcCode: string = "";
    public pcName: string = "";
    public tel: string = "";   
    public email: string = "";   
    public lineId: string = "";   
    public facebook: string = "";       
    public branchId: number = undefined;
    public departmentId: number = undefined; 
    public status: string = "A";
    public branchText: string = "";
    public departmentText: string = "";
    public statusText: string = "";
}
