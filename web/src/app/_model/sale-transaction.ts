import { AppSetting } from '../_constants/app-setting';
export class SaleTransactionSearchView {
    public pageIndex: number = 1;
    public itemPerPage: number = AppSetting.itemPerPage;
    public branchGroupId: number = 0;
    public branchId: number = 0;
    public docCode: string = "";
    public docNbr: string = "";
    public refDocNbr: string = "";
    public fromDate: any = null;
    public toDate: any = null;
    public pcSaleId: number = undefined;
    public stockLocationId: number = undefined;
    public statusId: string[] = [];
    public promotionCode: string = "";
}

export class SaleTransactionView {
    public saleTransactionId: number = 0;
    public branchId: number = 0;
    public branchCode: string = "";
    public branchName: string = "";
    public docCode: string = "";
    public stockLocationId: number = undefined;
    public stockLocationName: string = "";
    public customerId: number = 0;
    public pcSaleId: number = undefined;
    public pcSaleCode: string = "";
    public pcSaleName: string = "";
    public departmentId: number = null;
    public departmentName: string = "";
    public transactionDate: any = new Date();
    public deliveryDate: any = null;
    public refDocNbr: string = "";
    public docNbr: string = "";
    public promotionCode: string = "";
    public customerName: string = "";
    public addressName: string = "";
    public district: string = "";
    public subDistrict: string = "";
    public province: string = undefined;
    public zipCode: string = "";
    public tel: string = "";
    public remark: string = "";
    public totalQty: number = 0;
    public totalAmount: number = 0;
    public totalDiscountAmount: number = 0;
    public netAmount: number = 0;
    public returnProductReasonId: number = undefined;
    public returnReasonName: string = "";
    public statusId: string = "";
    public statusName: string = "";
    public createDateTime: any = null;
    public createUser: string = "";
    public updateDateTime: any = null;
    public updateUser: string = "";

    public returnDocTypeId: string = "MANUAL";
    public returnDocTypeName: string = "";

    public refReturnDocId: number = null;
    public refReturnDocNbr: string = "";

    public refReturnCustomerName: string = "";
    public refReturnDate: any = null;

    public memoText: string = "";

    public saleTransactionItems: SaleTransactionItemView[] = [];
}

export class SaleTransactionItemView {
    public saleTransactionItemId: number = 0;
    public saleTransactionId: number = 0;
    public itemNo: number = 0;
    public productId: number = 0;
    public barcode: string = "";
    public productCode: string = "";
    public productName: string = "";
    public unit: string = "";
    public unitPrice: number = 0;
    public skuCode: string = "";
    public realUnitPrice: number = 0;
    public qty: number = 0;
    public discountPcnt: number = 0;
    public discountAmount: number = 0;
    public totalAmount: number = 0;
    public itemRemark: string = "";
    public serialNo: string = "";
    public returnQty: number = 0;
    public netAmount: number = 0;
    public tmpReturnQty: number = 0;
    public refReturnTransactionItemId: number = 0;
    public actualAmount: number = 0;

    public productModelText: string = "";
    public productSizeText: string = "";
    public productColorText: string = "";
    public productDesignText: string = "";

    public onHandQty: number = 0;
    public intransitQty: number = 0;

    public remainQty: number = 0;
}


export class SaleProductSearchView {
    public branchId: number = 0;
    public isEdit: boolean = false;
    public itemPerPage: number = 0;
    public stockLocationId: number = 0;

    public hideSerialNo: boolean = false;
    public isSaleBed: boolean = false;

    public editItem: SaleTransactionItemView = null;
}

export class SaleReturnProductView {
    public productId: number = 0;
    public barcode: string = "";
    public productCode: string = "";
    public productName: string = "";
    public unit: string = "";
    public qty: number = 0;

    public refCosId: number = 0;
    public refCosDocNbr: string = "";

    public selected: boolean = false;
}