import { AppSetting } from "../_constants/app-setting";

export class RequesitionSearchView {
    public pageIndex: number = 1;
    public itemPerPage: number = AppSetting.itemPerPage;
    public branchGroupId: number = 0;
    public branchId: number = 0;
    public docCode: string = "";
    public docNbr: string = "";
    public fromDate: any = null;
    public toDate: any = null;
    public pcSaleId: number = undefined;
    public stockLocationId: number = undefined;
    public targetBranchId: number = undefined;
    public statusId: string[] = [];
}

export class RequesitionView {
    public requesitionTransactionId: number = 0;
    public branchId: number = 0;
    public branchCode: string = "";
    public branchName: string = "";
    public entityCode: string = "";
    public docCode: string = "";
    public pcSaleId: number = null;
    public pcSaleName: string = "";
    public departmentId: number = null;
    public departmentName: string = "";
    public stockLocationId: number = null;
    public stockLocationName: string = "";
    public transactionDate: any = new Date();
    public reqDate: any = null;
    public docNbr: string = "";
    public refDocNbr: string = "";
    public remark: string = "";
    public totalQty: number = 0;
    public statusId: string = "";
    public statusName: string = "";
    public updateUser: string = "";
    public targetBranchId: number = null;
    public targetBranchName: string = "";
    public targetBranchCode: string = "";

    public requesitionItems: RequesitionItemView[] = [];
}

export class RequesitionItemView {
    public requesitionTransactionItemId: number = 0;
    public requesitionTransactionId: number = 0;
    public itemNo: number = 0;
    public productId: number = 0;
    public barcode: string = "";
    public productCode: string = "";
    public productName: string = "";
    public unit: string = "";
    public qty: number = 0;
    public itemRemark: string = "";
    public serialNo: string = "";

    public onHandQty: number = 0;
    public intransitQty: number = 0;

    public productModelText: string = "";
    public productColorText: string = "";
    public productSizeText: string = "";
    public productDesignText: string = "";
}

export class RequestProductSearchView {
    public branchId: number = 0;
    public isEdit: boolean = false;
    public itemPerPage: number = 0;
    public stockLocationId: number = 0;
    public includeSerial: boolean = true;

    public editItem: RequesitionItemView = null;
}