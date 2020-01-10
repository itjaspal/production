import { AppSetting } from '../_constants/app-setting';

export class DailySaleSearchView {
    public pageIndex: number = 1;
    public itemPerPage: number = AppSetting.itemPerPage;    
    public status: string = "PAP";
    public dailySaleDate: Date = new Date();    
    public branchId: number = undefined;
}

export class DailySaleView {
    public pageIndex: number = 1;
    public itemPerPage: number = AppSetting.itemPerPage;
    public dailySaleTransactionId: number = undefined;
    public branchId: number = undefined;
    public branchText: string = "";
    public docNbr: string = "";
    public dailySaleDate: Date = new Date();
    public createUserId: string = "";
    public createUserName: string = "";
    public createDateTime: Date = new Date();
    public updateUserId: string = "";
    public updateUserName: string = "";
    public updateDateTime: Date = undefined;
    public status: string = "PAL";
    public dailySaleSKUs: DailySaleSKUGroupView[] = [];
}

export class DailySaleSKUGroupView {
    public dailySaleTransactionId: number = undefined;
    public dailySaleTransactionSkuId: number = undefined;
    public skuId: number = undefined;
    public skuCode: string ="";
    public docNbr: string = "";
    public gpPcnt: number = 0;
    public discountPcnt: number = 0;
    public qty: number = 0;
    public netAmount: number = 0;
    public gpAmount: number = 0;
    public gpDiscountAmount: number = 0;
    public gpNetAmount: number = 0;    
    public dailySaleproducts : DailySaleProductView[] = [];
}

export class DailySaleProductView {
    public dailySaleTransactionItemId: number = undefined;
    public dailySaleTransactionSkuId: number = undefined;
    public productId: number = undefined;
    public productCode: string = "";
    public barcode: string = "";
    public productName: string = "";  
    public qty: number = 0;
    public netAmount: number = 0;
    public gpAmount: number = 0;   
}

export class DailySaleMinDate {
    public minDate : Date = undefined;
}