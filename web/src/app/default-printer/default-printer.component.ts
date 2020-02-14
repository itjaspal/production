import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-default-printer',
  templateUrl: './default-printer.component.html',
  styleUrls: ['./default-printer.component.scss']
})
export class DefaultPrinterComponent implements OnInit {

  constructor() { }

  ngOnInit() {
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