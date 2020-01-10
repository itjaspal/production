import { AppSetting } from '../_constants/app-setting';

export class SKUSearchView {
    public pageIndex: number = 1;
    public itemPerPage: number = AppSetting.itemPerPage;
    public skuCode: string = "";
    public skuName: string = "";
    public status: string = "A";
    public branchGroupId: number = undefined;
    public branchId: number = undefined;
}

export class SKUView {
    public skuId: number = undefined;
    public skuCode: string = "";
    public skuName: string = "";
    public gpPcnt: number = 0;
    public discountPcnt: number = 0;
    public branchId: number = undefined;
    public statusText: string = "";
    public status: string = "";
}