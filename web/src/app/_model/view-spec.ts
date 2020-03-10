import { AppSetting } from "../_constants/app-setting";

export class ViewSpecSearchView {
    public pageIndex: number = 1;
    public itemPerPage: number = AppSetting.specDrawItemPerPage;
    public entity: string = AppSetting.entity;
    public req_date: string = "";
    public springtype_code: string = "";
    public pdsize_code: string = ""; 
}

export class ViewSpecSearchByPcsView {
    public pageIndex: number = 1;
    public itemPerPage: number = AppSetting.specDrawItemPerPage;
    public pcs_barcode: string = "";
}

export class CommonViewSpecView<T>{ 
    public pageIndex: number = 0;
    public itemPerPage: number = 0;
    public totalItem: number = 0;
    public prod_code: string = "";
    public prod_name: string = "";
    public model_desc: string = "";
    public spring_type: string = "";
    public size_desc: string = "";
    public drawing_path: string = "";
    public drawing_name: any = "";
    public datas: Array<T> = [];
}

export class ViewSpecView {
    public bom_seq: number = 0;
    public bom_sub: string = "";
    public bom_name: string = "";
    public rm_seq: number = 0;
    public rm_code: string = "";
    public rm_name: string = "";
    public short_name: string = "";
    public uom_code: string = "";
    public unit_qty: number = 0;
    public item_name: string = "";
}

export class ViewSpecDrawingParamView {
    public pdsize_code: string = "";
    public springtype_code: string = "";
    public req_date: string = "";
    public isEdit: boolean = false;
    public hideSerialNo: boolean = false;
    public isSaleBed: boolean = false;

}

