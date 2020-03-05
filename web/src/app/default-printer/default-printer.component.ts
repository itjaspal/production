import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../_service/authentication.service';
import { DefaultPrinterService } from '../_service/default-printer.service';
import { DisplayDefaultPrinterView, DefaultPrinterView } from '../_model/default-printer';
import { DropdownlistService } from '../_service/dropdownlist.service';
import { forkJoin } from 'rxjs';
import { MessageService } from '../_service/message.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-default-printer',
  templateUrl: './default-printer.component.html',
  styleUrls: ['./default-printer.component.scss']
})
export class DefaultPrinterComponent implements OnInit {

  public user: any;
  public model: DisplayDefaultPrinterView = new DisplayDefaultPrinterView();
  public modelUpdate: DefaultPrinterView = new DefaultPrinterView();
  public ddlDefaultPrinter: any;
  public validationForm: FormGroup;
  //public ddlProductSize: any;

  constructor(
    private _authSvc: AuthenticationService,
    private _defaultPrinterSvc: DefaultPrinterService,
    private _ddlSvc: DropdownlistService,
    private _msgSvc: MessageService,
    private _formBuilder: FormBuilder,
    private _router: Router,

  ) { }

  ngOnInit() { 
    this.user = this._authSvc.getLoginUser();
    this.getDefaultPrinter();


    forkJoin([
      this._ddlSvc.getDdlDefaultPrinter()
    
    ]).subscribe(result => {
      this.ddlDefaultPrinter = result[0];
  
    });

    this.buildForm();
    
  }


  buildForm() {
    this.validationForm = this._formBuilder.group({
      serial_no: [null, [Validators.required]]
    });
}

  close() {
    this._router.navigateByUrl('/app/mobile-navigator');
  }

  async save() {
    var _valid = true;
    this.modelUpdate.user_id = this.user.username;
    this.modelUpdate.mc_code = this.user.user_mac.MC_CODE;

    console.log("this.modelUpdate.user_id: " + this.modelUpdate.user_id);
    console.log("this.modelUpdate.mc_code: " + this.modelUpdate.mc_code);

    let res = await this._defaultPrinterSvc.update(this.modelUpdate); 
    

    await this._msgSvc.successPopup("ทำการบันทึกข้อมูลเรียบร้อยแล้ว");
    this.getDefaultPrinter();
    //this._router.navigateByUrl('defprinter');
  
  }


  async getDefaultPrinter() {
    this.model = await this._defaultPrinterSvc.getDefaultPrinter(this.user.user_mac.MC_CODE);
    console.log(this.model);
  }

}
