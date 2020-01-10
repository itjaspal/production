import { AppSetting } from '../_constants/app-setting';

export class ProductSearchView {
    public pageIndex: number = 1;
    public itemPerPage: number = AppSetting.itemPerPage;
    public productCode: string = "";
    public barcode: string = "";
    public productName: string = "";
    public productGroupId: string = "";
    public productTypeId: string = "";
    public productModelId: string = "";
    public productBrandId: string = "";
    public productDesignId: string = "";
    public productColorId: string = "";
    public productSizeId: string = "";
    public productUomId: string = "";
    public status: string = "A";
    

    public productModelText: string = "";
    public productColorText: string = "";
    public productSizeText: string = "";
    public productDesignText: string = "";
    public isSaleBed: boolean = true;

    // 0 = productCode, productName, barcode
    // 1 = model
    // 2 = color
    // 3 = size 
    public searchType: number = 0;
    public stockLocationId: number = null;
}

export class ProductView {
    public productId: number = undefined;
    public productCode: string = "";
    public pcsBarcode: string = "";
    public barcode: string = "";
    public productName: string = "";
    public productGroupId: string = undefined;
    public productTypeId: string = undefined;
    public productModelId: string = undefined;
    public productBrandId: string = undefined;
    public productDesignId: string = undefined;
    public productColorId: string = undefined;
    public productSizeId: string = undefined;
    public productUomId: string = undefined;
    public status: string = "A";
    public statusText: string = "";
    public productUomText: string = "";
    public productGroupText: string = "";
    public productTypeText: string = "";
    public productModelText: string = "";
    public productBrandText: string = "";
    public productDesignText: string = "";
    public productColorText: string = "";
    public productSizeText: string = "";
    public isSelected: boolean = false;
    public branchId: number = undefined;
    public unitPrice: number = 0;
    public unit: string = "";
    public discountPcnt: number = 0;
    public productBranchId: number = undefined;
    
    public onHandQty: number = 0;
    public intransitQty: number = 0;

    public productSkuBranchViews: ProductSkuBranchView[];
}

export class ProductSkuBranchView
{
    public skuText: string = "";
    public branchText: string = ""; 
}