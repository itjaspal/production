import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../_service/authentication.service';

@Component({
  selector: 'app-home-auto',
  templateUrl: './home-auto.component.html',
  styleUrls: ['./home-auto.component.scss']
})
export class HomeAutoComponent implements OnInit {

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
