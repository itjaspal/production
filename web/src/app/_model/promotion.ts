import { AppSetting } from '../_constants/app-setting';

export class PromotionSearchView {
    public pageIndex: number = 0;
    public itemPerPage: number = AppSetting.itemPerPage;
    public promotionCode: string = "";
    public promotionName: string = "";
    public fromDate: Date = undefined;
    public toDate: Date = undefined;
    public status: string = "A";
}

export class PromotionView {
    public promotionId: number = 0;
    public promotionCode: string = "";
    public promotionName: string = "";
    public effDate: Date = undefined;
    public endDate: Date = undefined;
    public remark: string = "";
    public statusId: string = "A";
    public createUser: string = "";
    public createDatetime: Date = undefined;
    public updateUser: string = "";
    public updateDatetime: Date = undefined;
    public statusName: string = "";
    public branchGroupPrvlgConditionViews: any = [];
    public branchPrvlgConditionViews: any = [];
}

export class PromotionBranchGroupPrvlgView {
    public branchGroupId: number = 0;
    public branchGroupCode: string = "";
    public branchGroupName: string = "";
}

export class PromotionBranchPrvlgView {
    public branchId: number = 0;
    public branchCode: string = "";
    public branchNameThai: string = "";
}