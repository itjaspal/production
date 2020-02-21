import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { AuthenticationService } from '../../_service/authentication.service';
import { PageEvent } from '@angular/material';
import { DomSanitizer } from '@angular/platform-browser';
import { ViewSpecSearchView, CommonViewSpecView, ViewSpecView } from '../../_model/view-spec';
import { ViewSpecService } from '../../_service/view-spec.service';
import { ActivatedRoute } from '@angular/router';
import { Pipe, PipeTransform } from '@angular/core';
import { DatePipe } from '@angular/common'





@Component({
  selector: 'app-view-spce-drawing',
  templateUrl: './view-spce-drawing.component.html',
  styleUrls: ['./view-spce-drawing.component.scss']
})
export class ViewSpceDrawingComponent implements OnInit {

  public model: ViewSpecSearchView = new ViewSpecSearchView();
  public dataViewSpec: CommonViewSpecView<ViewSpecView> = new CommonViewSpecView<ViewSpecView>();
  
  public user: any;
  public imageSource: any;
  public datePipe = new DatePipe('en-US');
  public vSum: number;

  @ViewChild('scanPcs') input55: ElementRef;
        
      
  //public element: HTMLElement;



  public scanPcsModel: any = { 
      scan_pcs_barcode: "",
  }
 
  
  constructor(
    private _viewSpecSvc: ViewSpecService,
    private _authSvc: AuthenticationService,
    private sanitizer: DomSanitizer,
    private _activateRoute: ActivatedRoute,
    
  ) {
  }

  
 
  async ngOnInit() {
      
      this.user = this._authSvc.getLoginUser();
   
      this.model.req_date        = this.datePipe.transform(this._activateRoute.snapshot.params.reqDate, 'dd/MM/yyyy');//"15/02/2020";
      this.model.pdsize_code     = this._activateRoute.snapshot.params.sizeCode;
      this.model.springtype_code = this._activateRoute.snapshot.params.springCode; 

      this.searchViewSpec();
      this.imageSource = this.sanitizer.bypassSecurityTrustResourceUrl(`data:image/png;base64,`); 

  }


  async searchViewSpec(event: PageEvent = null) { 
      if (event != null) {
        this.model.pageIndex = event.pageIndex;
        this.model.itemPerPage = event.pageSize;
      } 
      this.dataViewSpec =  await this._viewSpecSvc.searchSpecDrawing(this.model);
      this.imageSource = this.sanitizer.bypassSecurityTrustResourceUrl(`data:image/png;base64, ${this.dataViewSpec.drawing_name}`);  
    
  }

  async searchViewSpecByPcs(event: PageEvent = null) { 
      if (event != null) {
        this.model.pageIndex = event.pageIndex;
        this.model.itemPerPage = event.pageSize;
      } 
      //console.log("scan_pcs_barcode : " + this.scanPcsModel.scan_pcs_barcode);

      this.dataViewSpec =  await this._viewSpecSvc.searchSpecDrawingByPcs(this.scanPcsModel.scan_pcs_barcode);
      this.imageSource  = this.sanitizer.bypassSecurityTrustResourceUrl(`data:image/png;base64, ${this.dataViewSpec.drawing_name}`);

  }

  close() {
    window.history.back();
  }

  async save() {
    //let result = await this._productSvc.update(this.model);
    //await this._msgSvc.successPopup("บันทึกข้อมูลเรียบร้อย");
    //this._router.navigateByUrl('/app/product');
  }

}


