import { AuthenticationService } from './../_service/authentication.service';
import { Component, OnInit } from '@angular/core';
import { InformationMessageService } from './../_service/information-message.service';
import { PromotionService } from './../_service/promotion.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styles: []
})
export class HomeComponent implements OnInit {
  public user: any;
  public data: any = {
    datas: []
  };
  public model: any = {
    departmentId: 0,
    branchId: 0,
    username: ""
  };
  branchList: any[] = [];

  public pro: any = {
    datas: []
  };

  constructor(
    private _authSvc: AuthenticationService,
    private _informationMessageSvc: InformationMessageService,
    private _promotionSvc: PromotionService
  ) { }

  async ngOnInit() {
    // this.user = this._authSvc.getLoginUser();
    // this.branchList = this._authSvc.getUserBranches();

    // if (this.user.isPC) {
    //   this.model.branchId = this.user.branch.branchId;
    //   this.model.departmentId = 0;
    //   this.model.username = this.user.username;
    // } else {
    //   this.model.branchId = 0;
    //   this.model.departmentId = 0;
    //   this.model.username = this.user.username;
    // }

  
  }

}
