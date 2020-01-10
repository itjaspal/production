export class CommonSearchView<T>{
    public pageIndex: number = 0;
    public totalItem: number = 0;
    public itemPerPage: number = 0;
    public datas: Array<T> = [];
}
