import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { AuthenticationService } from '../../_service/authentication.service';
import { PageEvent } from '@angular/material';
import { DomSanitizer } from '@angular/platform-browser';
import { ViewSpecSearchView, CommonViewSpecView, ViewSpecView } from '../../_model/view-spec';
import { ViewSpecService } from '../../_service/view-spec.service';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Pipe, PipeTransform } from '@angular/core';
import { DatePipe } from '@angular/common'





@Component({
  selector: 'app-view-spce-drawing',
  templateUrl: './view-spce-drawing.component.html',
  styleUrls: ['./view-spce-drawing.component.scss']
})
export class ViewSpceDrawingComponent implements OnInit {

  public model: ViewSpecSearchView = new ViewSpecSearchView();
  //imageBase64
  public dataViewSpec: CommonViewSpecView<ViewSpecView> = new CommonViewSpecView<ViewSpecView>();
  
  public user: any;
  public imageSource: any;
  public datePipe = new DatePipe('en-US');
  public vSum: number;
  public validationForm: FormGroup;


  @ViewChild('scan_pcs_barcode') scan_pcs_barcode: ElementRef;
        
      
  //public element: HTMLElement;



  public scanPcsModel: any = { 
      scan_pcs_barcode: "",
  }
 
  
  constructor(
    private _viewSpecSvc: ViewSpecService,
    private _authSvc: AuthenticationService,
    private sanitizer: DomSanitizer,
    private _activateRoute: ActivatedRoute,
    private _formBuilder: FormBuilder,
    
  ) {
  }

  
 
  async ngOnInit() { 

      this.buildForm();
      this.user = this._authSvc.getLoginUser();

      sessionStorage.setItem('spect-drawing-reqDate', this.datePipe.transform(this._activateRoute.snapshot.params.reqDate, 'dd/MM/yyyy'));
      console.log("sessionStorage.getItem('spect-drawing-pcsBarcode') " + sessionStorage.getItem('spect-drawing-pcsBarcode'))
      this.scan_pcs_barcode.nativeElement.value = sessionStorage.getItem('spect-drawing-pcsBarcode');
      this.scanPcsModel.scan_pcs_barcode = this.scan_pcs_barcode.nativeElement.value;
      

      this.searchViewSpec();
      this.imageSource = this.sanitizer.bypassSecurityTrustResourceUrl(`data:image/png;base64,`); 

  }

  buildForm() {
    this.validationForm = this._formBuilder.group({
      scan_pcs_barcode: ['']
    });
  }

  ngOnDestroy() {
    //console.log("Close Program ");
    //sessionStorage.removeItem('spect-drawing-pcsBarcode');
  }

 

  async searchViewSpec(event: PageEvent = null) { 
      if (event != null) {
        this.model.pageIndex = event.pageIndex;
        this.model.itemPerPage = event.pageSize;
      } 
      sessionStorage.removeItem('session-image-base64'); 
      sessionStorage.setItem('session-image-base64', this.dataViewSpec.drawing_name);
      sessionStorage.setItem('spect-drawing-pcsBarcode', this.scan_pcs_barcode.nativeElement.value);
      

      //console.log("this.scan_pcs_barcode.nativeElement.value : " + this.scan_pcs_barcode.nativeElement.value); 

      if (this.scan_pcs_barcode.nativeElement.value == "") {
          console.log("searchSpecDrawing");

          this.model.req_date        = this.datePipe.transform(this._activateRoute.snapshot.params.reqDate, 'dd/MM/yyyy');//"15/02/2020";
          this.model.pdsize_code     = this._activateRoute.snapshot.params.sizeCode;
          this.model.springtype_code = this._activateRoute.snapshot.params.springCode;

          this.dataViewSpec =  await this._viewSpecSvc.searchSpecDrawing(this.model);
          //image = this.dataViewSpec.drawing_name;
      } else if (this.scan_pcs_barcode.nativeElement.value != "") { 
          console.log("searchSpecDrawingByPcs : " + this.scan_pcs_barcode.nativeElement.value);
          this.dataViewSpec =  await this._viewSpecSvc.searchSpecDrawingByPcs(this.scan_pcs_barcode.nativeElement.value);
      } 
      this.imageSource = this.sanitizer.bypassSecurityTrustResourceUrl(`data:image/png;base64, ${this.dataViewSpec.drawing_name}`); 
      
  }

  async searchViewSpecByPcs(event: PageEvent = null) {  
      if (event != null) {
        this.model.pageIndex = event.pageIndex;
        this.model.itemPerPage = event.pageSize;
      } 
      //console.log("scan_pcs_barcode : " + this.scanPcsModel.scan_pcs_barcode);
      //console.log("this.scan_pcs_barcode.nativeElement.value : " + this.scan_pcs_barcode.nativeElement.value)
      sessionStorage.removeItem('session-image-base64');
      sessionStorage.setItem('session-image-base64', this.dataViewSpec.drawing_name);
      console.log("searchSpecDrawingByPcs : " + this.scanPcsModel.scan_pcs_barcode);
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


